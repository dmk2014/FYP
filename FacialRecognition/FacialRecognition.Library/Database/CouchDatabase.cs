using LoveSeat;
using System;
using System.Collections.Generic;

namespace FacialRecognition.Library.Database
{
    public class CouchDatabase : IDatabase
    {
        private CouchClient c_Couch;
        private LoveSeat.CouchDatabase c_Database;
 
        public CouchDatabase(String Host, int Port, String Database)
        {
            c_Couch = new CouchClient(Host, Port, null, null, false, AuthenticationType.Basic);

            this.ConfigureDatabase("");
        }

        public void ConfigureDatabase(String Database)
        {
            if (!c_Couch.HasDatabase(Database))
            {
                c_Couch.CreateDatabase(Database);
            }

            c_Database = c_Couch.GetDatabase(Database);
        }

        public Boolean Store(Models.Person Person)
        {
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