using FacialRecognition.Library.Database;
using FacialRecognition.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FacialRecognition.Test
{
    [TestClass]
    public class CouchDB_CRUD_Test
    {
        IDatabase c_DB;
        private const String DATABASE_NAME = "testing";

        [TestInitialize]
        public void Setup()
        {
            c_DB = new CouchDatabase("localhost", 5984, DATABASE_NAME);

            var _person = new Person();
            _person.Id = "person1";
            _person.Forename = "Unit";
            _person.Surname = "Test";

            var _result = c_DB.Store(_person);
        }

        [TestCleanup]
        public void Cleanup()
        {
            c_DB.DeleteDatabase(DATABASE_NAME);
        }

        [TestMethod]
        public void TestStoreAPerson()
        {
            var _person = new Person();
            _person.Id = "testID";
            _person.Forename = "Unit";
            _person.Surname = "Test";

            var _result = c_DB.Store(_person);
            Assert.IsTrue(_result);
        }

        [TestMethod]
        public void TestRetrieveAll()
        {
            var _result = c_DB.RetrieveAll();

            Assert.IsTrue(_result.Count > 0);
        }
    }
}