using DreamSeat;
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

        public CouchDatabase(string host, int port, string database)
        {
            this.Couch = new CouchClient();
            this.InitializeDatabase(database);
        }

        private void InitializeDatabase(string databaseName)
        {
            // Ensure database exists
            this.CreateDatabase(databaseName);

            // Ensure view to retrieve all documents exists
            if (!this.Database.DocumentExists(DesignDocuments + DesignDocumentName))
            {
                var designDocument = new CouchDesignDocument(DesignDocumentName);
                var couchView = new CouchView(AllDocumentsViewDefinition);

                designDocument.Views.Add(AllDocumentsViewName, couchView);
                this.Database.CreateDocument(designDocument);
            }
        }

        public void CreateDatabase(string databaseName)
        {
            if (!this.Couch.HasDatabase(databaseName))
            {
                this.Couch.CreateDatabase(databaseName);
            }

            this.Database = this.Couch.GetDatabase(databaseName);
        }

        private void ValidateDatabaseName(string databaseName)
        {
            // TODO
        }

        public void DeleteDatabase(string databaseName)
        {
            if (this.Couch.HasDatabase(databaseName))
            {
                this.Couch.DeleteDatabase(databaseName);
            }
        }

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

        private void AddAttachments(string documentID, List<Image> images)
        {
            for (int i = 0; i < images.Count; i++)
            {
                var attachmentName = i + ".bmp";
                var imageAsByteArray = this.ImageToByteArray(images[i]);

                this.Database.AddAttachment(documentID, new MemoryStream(imageAsByteArray), attachmentName);
            }
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

        private List<Image> RetrieveAttachments(string personID)
        {
            var images = new List<Image>();
            var person = this.Database.GetDocument<CouchDocument>(personID);

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
    }
}