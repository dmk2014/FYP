using FacialRecognition.Library.Database;
using FacialRecognition.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace FacialRecognition.Test
{
    [TestClass]
    public class CouchDB_CRUD_Test
    {
        IDatabase c_DB;
        private readonly String DATABASE_NAME = "testing";
        private readonly Image TEST_IMAGE = FacialRecognition.Test.Properties.Resources.FacialImage;

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
        public void TestUpdateAPerson()
        {
            var _person = new Person();
            _person.Id = "update123";
            _person.Forename = "Stack";
            _person.Surname = "Overflow";

            c_DB.Store(_person);

            //Person stored. DB generates Rev. Now lets change the person and update them
            _person.Surname = "Trace";
            c_DB.Update(_person);

            //Retrieve the item, and cast it so we can verify the revision number
            var _result = (PersonCouchDB)c_DB.Retrieve(_person.Id);
            Assert.IsTrue(_result.Rev.StartsWith("2"));
        }

        [TestMethod]
        public void TestUpdateNonExisitingPersonCreatesANewDocument()
        {
            var _person = new Person();
            _person.Id = "doesntexist";
            _person.Forename = "Mike";
            _person.Surname = "Fratello";

            c_DB.Update(_person);

            //Assert creation
            var _result = c_DB.Retrieve(_person.Id);

            Assert.AreEqual(_result.Id, _person.Id);
            Assert.AreEqual(_result.Forename, _person.Forename);
            Assert.AreEqual(_result.Surname, _person.Surname);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestRetrieveNonExistingPersonCausesException()
        {
            var _person = new Person();
            _person.Id = "abc123xyznotindb";

            c_DB.Retrieve(_person.Id);
        }

        [TestMethod]
        public void TestRetrieveAPerson()
        {
            var _result = c_DB.Retrieve("person1");

            Assert.AreEqual("person1", _result.Id);
            Assert.AreEqual("Unit", _result.Forename);
            Assert.AreEqual("Test", _result.Surname);
            Assert.IsInstanceOfType(_result, typeof(Person));
        }

        [TestMethod]
        public void TestRetrieveAll()
        {
            var _result = c_DB.RetrieveAll();

            Assert.IsTrue(_result.Count > 0);
        }

        [TestMethod]
        public void TestStorePersonWithAttachment()
        {
            var _person = new Person();
            _person.Id = "personattach";
            _person.Forename = "Unit";
            _person.Surname = "Test";
            _person.Images.Add(this.TEST_IMAGE);

            c_DB.Store(_person);
        }

        [TestMethod]
        public void TestUpdatePersonWithAttachment()
        {
            var _person = new Person();
            _person.Id = "personattachupdate";
            _person.Forename = "Unit";
            _person.Surname = "Test";
            _person.Images.Add(this.TEST_IMAGE);

            c_DB.Store(_person);

            //Now we will add another attachment and update the doc
            _person.Images.Add(this.TEST_IMAGE);
            c_DB.Update(_person);
        }

        [TestMethod]
        public void TestRetrievePersonWithAttachment()
        {
            var _person = new Person();
            _person.Id = "personattachretrieve";
            _person.Forename = "Unit";
            _person.Surname = "Test";
            _person.Images.Add(this.TEST_IMAGE);

            c_DB.Store(_person);

            var _result = c_DB.Retrieve(_person.Id);

            Assert.IsTrue(_result.Images.Count == 1);
            Assert.AreEqual(_result.Images[0].Size, this.TEST_IMAGE.Size);
        }

        [TestMethod]
        public void TestRetrieveAllWithAttachments()
        {
            var _person = new Person();
            _person.Id = "personattachretrieveall";
            _person.Forename = "Unit";
            _person.Surname = "Test";

            _person.Images.Add(this.TEST_IMAGE);
            c_DB.Store(_person);

            var _result = c_DB.RetrieveAll();

            var _attemptToFindPerson = _result.Find(p => p.Id == _person.Id);

            Assert.AreNotEqual(null, _attemptToFindPerson);
            Assert.IsTrue(_attemptToFindPerson.Images.Count == 1);
        }
    }
}