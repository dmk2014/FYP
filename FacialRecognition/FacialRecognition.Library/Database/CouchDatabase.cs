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
            var _response = c_Couch.Entities.PostAsync(Person);
            throw new NotImplementedException();
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