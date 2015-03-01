using DreamSeat;
using FacialRecognition.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FacialRecognition.Library.Database
{
    public class CouchDatabase : IDatabase
    {
        private DreamSeat.CouchClient c_Couch;
        private DreamSeat.CouchDatabase c_Database;
        private const String VIEW_DOCUMENT_IDENTIFIER = "people";
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
            if (!c_Database.DocumentExists(VIEW_DOCUMENT_IDENTIFIER))
            {
                var _designDoc = new CouchDesignDocument(VIEW_DOCUMENT_IDENTIFIER);
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

            c_Database.CreateDocument<Models.PersonCouchDB>(_couchModel);

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
                return true;
            }
            else
            {
                return this.Store(Person);
            }
        }

        public Models.Person Retrieve(string ID)
        {
            if (c_Database.DocumentExists(ID))
            {
                return c_Database.GetDocument<PersonCouchDB>(ID);
            }
            else
            {
                throw new Exception("The specified document was not found in the database");
            }
        }

        public List<Models.Person> RetrieveAll()
        {
            var _result = new List<Models.Person>();

            var _allPeopleViewResult = c_Database.GetView(VIEW_DOCUMENT_IDENTIFIER, ALL_DOCUMENTS_VIEW_NAME).ToString();

            var _deserializedResult = JsonConvert.DeserializeObject<dynamic>(_allPeopleViewResult);

            //Rows is an array containing all keys/value pairs emitted by the view
            var _viewRows = _deserializedResult.rows;

            for (int i = 0; i < _viewRows.Count; i++)
            {
                var _currentDocument = _viewRows[i];
                var _documentValues = _currentDocument.value;

                var _curPerson = new PersonCouchDB();
                _curPerson.Id = _documentValues.id;
                _curPerson.Rev = _documentValues._rev;
                _curPerson.Forename = _documentValues.forename;
                _curPerson.Surname = _documentValues.surname;
                _result.Add(_curPerson);
            }

            return _result;
        }
    }
}