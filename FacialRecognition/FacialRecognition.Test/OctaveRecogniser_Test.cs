using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FacialRecognition.Library.Database;
using FacialRecognition.Library.Octave;
using FacialRecognition.Library.Core;

namespace FacialRecognition.Test
{
    [TestClass]
    public class OctaveRecogniser_Test
    {
        private IDatabase Database;
        private const String DatabaseName = "facial1";
        private IFacialRecogniser Recogniser;

        [TestInitialize]
        public void InitializeTest()
        {
            Database = new CouchDatabase("localhost", 5984, DatabaseName);

            var octaveInterface = new OctaveInterface("localhost", "6379");

            Recogniser = new OctaveRecogniser(octaveInterface);
        }

        [TestMethod]
        public void TestSendDataToCache()
        {
            var peopleInDatabase = Database.RetrieveAll();

            var sendDataToCacheMethod = Recogniser.GetType().GetMethod("SendDataToCacheForRetraining", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            sendDataToCacheMethod.Invoke(Recogniser, new Object[] { peopleInDatabase });
        }
    }
}
