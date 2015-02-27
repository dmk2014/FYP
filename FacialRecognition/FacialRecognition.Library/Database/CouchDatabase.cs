using MyCouch;
using System;
using System.Collections.Generic;

namespace FacialRecognition.Library.Database
{
    public class CouchDatabase : IDatabase
    {
        private MyCouchClient c_Couch;
 
        public CouchDatabase(String Host, int Port, String Database)
        {
            var _uri = new MyCouchUriBuilder("http://" + Host + ":" + Port)
                .SetDbName(Database);

            c_Couch = new MyCouchClient(_uri.Build());
        }

        public bool Store(Models.Person Person)
        {
            var _post = c_Couch.Entities.PostAsync(Person);

            var _result = _post.Result;

            return _result.IsSuccess;
        }

        public bool Update(Models.Person Person)
        {
            throw new NotImplementedException();
        }

        public Models.Person Retrieve(string ID)
        {
            throw new NotImplementedException();
        }

        public List<Models.Person> RetrieveAll()
        {
            throw new NotImplementedException();
        }
    }
}