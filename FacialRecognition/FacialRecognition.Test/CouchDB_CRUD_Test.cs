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

        [TestInitialize]
        public void Setup()
        {
            c_DB = new CouchDatabase("localhost", 5984, "people");
        }

        [TestMethod]
        public void TestStoreAPerson()
        {
            var _person = new Person();
            _person._id = "testID";
            _person.Forename = "Unit";
            _person.Surname = "Test";

            c_DB.Store(_person);
        }
    }
}