using FacialRecognition.Library.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FacialRecognition.Test
{
    [TestClass]
    public class CouchDB_Connection_Test
    {
        /// <summary>
        /// Test connectivity with CouchDB. Test should be passed
        /// if no exception are thrown while connecting
        /// </summary>
        [TestMethod]
        public void TestCouchConnection()
        {
            var _db = new CouchDatabase("localhost", 5984, "People");
        }
    }
}