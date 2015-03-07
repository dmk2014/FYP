using DreamSeat;
using FacialRecognition.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Text;

namespace FacialRecognition.Library.Database
{
    public class CouchDatabase : IDatabase
    {
        private DreamSeat.CouchClient c_Couch;
        private DreamSeat.CouchDatabase c_Database;
        private const String DESIGN_DOCUMENTS = "_design/";
        private const String DESIGN_DOCUMENT_NAME = "people";
        private const String ALL_DOCUMENTS_VIEW_NAME = "all";

        public CouchDatabase(String Host, int Port, String Database)
        {
            c_Couch = new CouchClient();

            this.InitializeDatabase(Database);
        }

        private void InitializeDatabase(String Database)
        {
            //Ensure DB exists
            this.CreateDatabase(Database);

            //Ensure View to retrieve all documents exists
            if (!c_Database.DocumentExists(DESIGN_DOCUMENTS + DESIGN_DOCUMENT_NAME))
            {
                var _designDoc = new CouchDesignDocument(DESIGN_DOCUMENT_NAME);
                var _view = new CouchView("function(doc) {emit(null, doc)}");

                _designDoc.Views.Add(ALL_DOCUMENTS_VIEW_NAME, _view);
                c_Database.CreateDocument(_designDoc);
            }
        }

        public void CreateDatabase(String Database)
        {
            if (!c_Couch.HasDatabase(Database))
            {
                c_Couch.CreateDatabase(Database);
            }

            c_Database = c_Couch.GetDatabase(Database);
        }

        private void ValidateDatabaseName(String Database)
        {
            //TODO
        }

        public void DeleteDatabase(String Database)
        {
            if (c_Couch.HasDatabase(Database))
            {
                c_Couch.DeleteDatabase(Database);
            }
        }

        public Boolean Store(Models.Person Person)
        {
            var _couchModel = new PersonCouchDB();
            _couchModel.Id = Person.Id;
            _couchModel.Forename = Person.Forename;
            _couchModel.Surname = Person.Surname;

            var _result = c_Database.CreateDocument<Models.PersonCouchDB>(_couchModel);

            //Add attachments if applicable
            if (Person.Images.Count > 0)
            {
                this.AddAttachments(_result.Id, Person.Images);
            }
            
            return true;
        }

        public bool Update(Models.Person Person)
        {
            if (c_Database.DocumentExists(Person.Id))
            {
                //Retrieve existing document to acquire revision number
                var _existingDoc = (PersonCouchDB)this.Retrieve(Person.Id);
                
                var _couchModel = new PersonCouchDB();
                _couchModel.Id = Person.Id;
                _couchModel.Rev = _existingDoc.Rev;
                _couchModel.Forename = Person.Forename;
                _couchModel.Surname = Person.Surname;

                c_Database.UpdateDocument<PersonCouchDB>(_couchModel);

                //Attachment
                if (Person.Images.Count > 0)
                {
                    this.AddAttachments(_couchModel.Id, Person.Images);
                }

                return true;
            }
            else
            {
                return this.Store(Person);
            }
        }

        private void AddAttachments(String DocumentID, List<Image> Images)
        {
            for (int _i = 0; _i < Images.Count; _i++)
            {
                c_Database.AddAttachment(DocumentID, new MemoryStream(this.ImageToByteArray(Images[_i])), _i + ".bmp");
            }
        }

        private byte[] ImageToByteArray(Image Image)
        {
            //Reference: http://stackoverflow.com/questions/17352061/fastest-way-to-convert-image-to-byte-array
            var _stream = new MemoryStream();
            Image.Save(_stream, System.Drawing.Imaging.ImageFormat.Bmp);
            return _stream.ToArray();
        }

        public Models.Person Retrieve(string ID)
        {
            if (c_Database.DocumentExists(ID))
            {
                var _result = c_Database.GetDocument<PersonCouchDB>(ID);
                _result.Images = this.RetrieveAttachments(ID);
                
                return _result;
            }
            else
            {
                throw new Exception("The specified document was not found in the database");
            }
        }

        private List<Image> RetrieveAttachments(String PersonID)
        {
            var _result = new List<Image>();
            var _person = c_Database.GetDocument<CouchDocument>(PersonID);

            if (_person.HasAttachment)
            {
                var _attachmentNames = _person.GetAttachmentNames();

                foreach (var _name in _attachmentNames)
                {
                    var _attachmentStream = c_Database.GetAttachment(PersonID, _name);
                    var _image = Image.FromStream(_attachmentStream);
                    _result.Add(_image);
                }
            }

            return _result;
        }

        public List<Models.Person> RetrieveAll()
        {
            var _result = new List<Models.Person>();

            var _allPeopleViewResult = c_Database.GetView(DESIGN_DOCUMENT_NAME, ALL_DOCUMENTS_VIEW_NAME).ToString();

            var _deserializedResult = JsonConvert.DeserializeObject<dynamic>(_allPeopleViewResult);

            //Rows is an array containing all keys/value pairs emitted by the view
            var _viewRows = _deserializedResult.rows;

            for (int i = 0; i < _viewRows.Count; i++)
            {
                var _currentDocument = _viewRows[i];
                var _documentValues = _currentDocument.value;

                var _curPerson = new PersonCouchDB();
                _curPerson.Id = (String)_documentValues.id;
                _curPerson.Rev = (String)_documentValues._rev;
                _curPerson.Forename = (String)_documentValues.forename;
                _curPerson.Surname = (String)_documentValues.surname;
                _curPerson.Images = this.RetrieveAttachments(_curPerson.Id);
                _result.Add(_curPerson);
            }

            return _result;
        }
    }
}