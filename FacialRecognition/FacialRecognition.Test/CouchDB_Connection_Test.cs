using FacialRecognition.Library.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FacialRecognition.Test
{
    [TestClass]
    public class CouchDB_Connection_Test
    {
        IDatabase _db;
        private const String DATABASE_NAME = "testing";

        [TestCleanup]
        public void TestCleanup()
        {
            _db.DeleteDatabase(DATABASE_NAME);
        }

        /// <summary>
        /// Test connectivity with CouchDB. Test should be passed
        /// if no exception are thrown while connecting
        /// </summary>
        [TestMethod]
        public void TestCouchConnection()
        {
            _db = new CouchDatabase("localhost", 5984, DATABASE_NAME);
        }
    }
}