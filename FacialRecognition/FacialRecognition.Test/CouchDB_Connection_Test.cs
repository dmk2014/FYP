using FacialRecognition.Library.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FacialRecognition.Test
{
    [TestClass]
    public class CouchDB_Connection_Test
    {
        IDatabase CouchDatabase;
        private const String DatabaseName = "testing";

        [TestCleanup]
        public void TestCleanup()
        {
            CouchDatabase.DeleteDatabase(DatabaseName);
        }

        /// <summary>
        /// Test connectivity with CouchDB. Test should be passed
        /// if no exception are thrown while connecting
        /// </summary>
        [TestMethod]
        public void TestCouchConnection()
        {
            CouchDatabase = new CouchDatabase("localhost", 5984, DatabaseName);
        }
    }
}