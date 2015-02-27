using System;
using System.Collections.Generic;

namespace FacialRecognition.Library.Database
{
    class CouchDatabase : IDatabase
    {
        public bool Store(Models.Person Person)
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