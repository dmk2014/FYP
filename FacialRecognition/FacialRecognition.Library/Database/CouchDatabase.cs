using System;
using System.Collections.Generic;

using DreamSeat;

namespace FacialRecognition.Library.Database
{
    public class CouchDatabase : IDatabase
    {
        private DreamSeat.CouchClient c_Couch;
        private DreamSeat.CouchDatabase c_Database;

        public CouchDatabase(String Host, int Port, String Database)
        {
            c_Couch = new CouchClient();

            this.ConfigureDatabase(Database);
        }

        private void ConfigureDatabase(String Database)
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