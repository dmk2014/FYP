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
        IDatabase CouchDatabase;
        private readonly String DatabaseName = "testing";
        private readonly Image TestImage = FacialRecognition.Test.Properties.Resources.FacialImage;

        [TestInitialize]
        public void Setup()
        {
            CouchDatabase = new CouchDatabase("localhost", 5984, DatabaseName);

            var _person = new Person();
            _person.Id = "person1";
            _person.Forename = "Unit";
            _person.Surname = "Test";

            var _result = CouchDatabase.Store(_person);
        }

        [TestCleanup]
        public void Cleanup()
        {
            CouchDatabase.DeleteDatabase(DatabaseName);
        }

        [TestMethod]
        public void TestStoreAPerson()
        {
            var _person = new Person();
            _person.Id = "testID";
            _person.Forename = "Unit";
            _person.Surname = "Test";

            var _result = CouchDatabase.Store(_person);
            Assert.IsTrue(_result);
        }

        [TestMethod]
        public void TestUpdateAPerson()
        {
            var _person = new Person();
            _person.Id = "update123";
            _person.Forename = "Stack";
            _person.Surname = "Overflow";

            CouchDatabase.Store(_person);

            //Person stored. DB generates Rev. Now lets change the person and update them
            _person.Surname = "Trace";
            CouchDatabase.Update(_person);

            //Retrieve the item, and cast it so we can verify the revision number
            var _result = (PersonCouchDB)CouchDatabase.Retrieve(_person.Id);
            Assert.IsTrue(_result.Rev.StartsWith("2"));
        }

        [TestMethod]
        public void TestUpdateNonExisitingPersonCreatesANewDocument()
        {
            var _person = new Person();
            _person.Id = "doesntexist";
            _person.Forename = "Mike";
            _person.Surname = "Fratello";

            CouchDatabase.Update(_person);

            //Assert creation
            var _result = CouchDatabase.Retrieve(_person.Id);

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

            CouchDatabase.Retrieve(_person.Id);
        }

        [TestMethod]
        public void TestRetrieveAPerson()
        {
            var _result = CouchDatabase.Retrieve("person1");

            Assert.AreEqual("person1", _result.Id);
            Assert.AreEqual("Unit", _result.Forename);
            Assert.AreEqual("Test", _result.Surname);
            Assert.IsInstanceOfType(_result, typeof(Person));
        }

        [TestMethod]
        public void TestRetrieveAll()
        {
            var _result = CouchDatabase.RetrieveAll();

            Assert.IsTrue(_result.Count > 0);
        }

        [TestMethod]
        public void TestStorePersonWithAttachment()
        {
            var _person = new Person();
            _person.Id = "personattach";
            _person.Forename = "Unit";
            _person.Surname = "Test";
            _person.Images.Add(this.TestImage);

            CouchDatabase.Store(_person);
        }

        [TestMethod]
        public void TestUpdatePersonWithAttachment()
        {
            var _person = new Person();
            _person.Id = "personattachupdate";
            _person.Forename = "Unit";
            _person.Surname = "Test";
            _person.Images.Add(this.TestImage);

            CouchDatabase.Store(_person);

            //Now we will add another attachment and update the doc
            _person.Images.Add(this.TestImage);
            CouchDatabase.Update(_person);
        }

        [TestMethod]
        public void TestRetrievePersonWithAttachment()
        {
            var _person = new Person();
            _person.Id = "personattachretrieve";
            _person.Forename = "Unit";
            _person.Surname = "Test";
            _person.Images.Add(this.TestImage);

            CouchDatabase.Store(_person);

            var _result = CouchDatabase.Retrieve(_person.Id);

            Assert.IsTrue(_result.Images.Count == 1);
            Assert.AreEqual(_result.Images[0].Size, this.TestImage.Size);
        }

        [TestMethod]
        public void TestRetrieveAllWithAttachments()
        {
            var _person = new Person();
            _person.Id = "personattachretrieveall";
            _person.Forename = "Unit";
            _person.Surname = "Test";

            _person.Images.Add(this.TestImage);
            CouchDatabase.Store(_person);

            var _result = CouchDatabase.RetrieveAll();

            var _attemptToFindPerson = _result.Find(p => p.Id == _person.Id);

            Assert.AreNotEqual(null, _attemptToFindPerson);
            Assert.IsTrue(_attemptToFindPerson.Images.Count == 1);
        }
    }
}