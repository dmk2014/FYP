using FacialRecognition.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace FacialRecognition.Library.Database
{
    public class CouchDatabase : IDatabase
    {
        private DreamSeat.CouchClient Couch;
        private DreamSeat.CouchDatabase Database;
        private const string DesignDocuments = "_design/";
        private const string DesignDocumentName = "people";
        private const string AllDocumentsViewDefinition = "function(doc) {emit(null, doc)}";
        private const string AllDocumentsViewName = "all";

        /// <summary>
        /// Connect to CouchDB at the specified host, port and supplied database name.
        /// </summary>
        /// <param name="host">The host where CouchDB is running.</param>
        /// <param name="port">The port where CouchDB is running.</param>
        /// <param name="databaseName">The Couch database to use. Created if it doesn't exist. Must start with a
        ///     lowercase letter and contain only lowercase or _$()+-/ characters.</param>
        public CouchDatabase(string host, int port, string databaseName)
        {
            this.Couch = new DreamSeat.CouchClient();
            this.InitializeDatabase(databaseName);
        }

        private void InitializeDatabase(string databaseName)
        {
            // Ensure database exists
            this.CreateDatabase(databaseName);

            // Get database reference
            this.Database = this.Couch.GetDatabase(databaseName);

            // Ensure view to retrieve all documents exists
            if (!this.Database.DocumentExists(DesignDocuments + DesignDocumentName))
            {
                var designDocument = new DreamSeat.CouchDesignDocument(DesignDocumentName);
                var couchView = new DreamSeat.CouchView(AllDocumentsViewDefinition);

                designDocument.Views.Add(AllDocumentsViewName, couchView);
                this.Database.CreateDocument(designDocument);
            }
        }

        /// <summary>
        /// Create a Couch database using the specified name.
        /// </summary>
        /// <param name="databaseName">The Couch database to use. Created if it doesn't exist. Must start with a
        ///     lowercase letter and contain only lowercase or _$()+-/ characters.</param>
        public void CreateDatabase(string databaseName)
        {
            if (!this.Couch.HasDatabase(databaseName))
            {
                this.Couch.CreateDatabase(databaseName);
            }
        }

        /// <summary>
        /// Deletes a Couch database of the specified name.
        /// </summary>
        /// <param name="databaseName">The name of the Couch database to be deleted.</param>
        public void DeleteDatabase(string databaseName)
        {
            if (this.Couch.HasDatabase(databaseName))
            {
                this.Couch.DeleteDatabase(databaseName);
            }
        }

        /// <summary>
        /// Store a person object in CouchDB.
        /// </summary>
        /// <param name="person">The person to be stored.</param>
        /// <returns>Boolean value indicating if the operation was successful.</returns>
        public bool Store(Person person)
        {
            var couchModel = new PersonCouchDB();
            couchModel.Id = person.Id;
            couchModel.Forename = person.Forename;
            couchModel.Surname = person.Surname;

            var result = this.Database.CreateDocument<Models.PersonCouchDB>(couchModel);

            // Store the objects images as document attachment in CouchDB
            if (person.Images.Count > 0)
            {
                this.AddAttachments(result.Id, person.Images);
            }
            
            return true;
        }

        /// <summary>
        /// Update a person object in CouchDB.
        /// </summary>
        /// <param name="person">The person to be updated.</param>
        /// <returns>Boolean value indicating if the operation was successful.</returns>
        public bool Update(Person person)
        {
            if (this.Database.DocumentExists(person.Id))
            {
                // Retrieve existing document to acquire revision number
                var existingDocument = (PersonCouchDB)this.Retrieve(person.Id);
                
                var couchModel = new PersonCouchDB();
                couchModel.Id = person.Id;
                couchModel.Rev = existingDocument.Rev;
                couchModel.Forename = person.Forename;
                couchModel.Surname = person.Surname;

                this.Database.UpdateDocument<PersonCouchDB>(couchModel);

                // Store the objects images as document attachment in CouchDB
                if (person.Images.Count > 0)
                {
                    this.AddAttachments(couchModel.Id, person.Images);
                }

                return true;
            }
            else
            {
                return this.Store(person);
            }
        }

        /// <summary>
        /// Retrieve a single person from the database using their document identifier.
        /// </summary>
        /// <param name="ID">The document identifier of the person to retrieve.</param>
        /// <returns>The retrieved person object.</returns>
        public Person Retrieve(string ID)
        {
            if (this.Database.DocumentExists(ID))
            {
                var person = this.Database.GetDocument<PersonCouchDB>(ID);
                person.Images = this.RetrieveAttachments(ID);
                
                return person;
            }
            else
            {
                throw new Exception("The specified document was not found in the database");
            }
        }

        /// <summary>
        /// Retrieve all people stored in the the database.
        /// </summary>
        /// <returns>A list of all people stored in the database. Each item is a person object.</returns>
        public List<Person> RetrieveAll()
        {
            var result = new List<Models.Person>();

            var allPeopleViewResult = this.Database.GetView(DesignDocumentName, AllDocumentsViewName).ToString();

            var deserializedResult = JsonConvert.DeserializeObject<dynamic>(allPeopleViewResult);

            // Rows is an array containing all keys/value pairs emitted by the view
            var viewRows = deserializedResult.rows;

            for (int i = 0; i < viewRows.Count; i++)
            {
                var currentDocument = viewRows[i];
                var documentValues = currentDocument.value;

                var curPerson = new PersonCouchDB();
                curPerson.Id = (string)documentValues._id;
                curPerson.Rev = (string)documentValues._rev;
                curPerson.Forename = (string)documentValues.forename;
                curPerson.Surname = (string)documentValues.surname;
                curPerson.Images = this.RetrieveAttachments(curPerson.Id);
                result.Add(curPerson);
            }

            return result;
        }

        private void AddAttachments(string documentID, List<Image> images)
        {
            for (int i = 0; i < images.Count; i++)
            {
                var attachmentName = i + ".bmp";
                var imageAsByteArray = this.ImageToByteArray(images[i]);

                this.Database.AddAttachment(documentID, new MemoryStream(imageAsByteArray), attachmentName);
            }
        }

        private List<Image> RetrieveAttachments(string personID)
        {
            var images = new List<Image>();
            var person = this.Database.GetDocument<DreamSeat.CouchDocument>(personID);

            if (person.HasAttachment)
            {
                var attachmentNames = person.GetAttachmentNames();

                foreach (var name in attachmentNames)
                {
                    var attachmentStream = this.Database.GetAttachment(personID, name);
                    var image = Image.FromStream(attachmentStream);
                    images.Add(image);
                }
            }

            return images;
        }

        private byte[] ImageToByteArray(Image image)
        {
            // Reference: http://stackoverflow.com/questions/17352061/fastest-way-to-convert-image-to-byte-array
            // Save the image to a memory stream
            var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            // Return the stream as a byte array
            return stream.ToArray();
        }
    }
}