using FacialRecognition.Library.Core;
using FacialRecognition.Library.Database;
using FacialRecognition.Library.Octave;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace FacialRecognition.Test
{
    [TestClass]
    public class OctaveRecogniser_Test
    {
        private IDatabase Database;
        private const string DatabaseName = "facial1";
        private OctaveRecogniser Recogniser;
        private readonly Image TestImage = FacialRecognition.Test.Properties.Resources.FacialImage;

        private const string CouchHost = "localhost";
        private const int CouchPort = 5984;
        private const string RedisHost = "localhost";
        private const int RedisPort = 6379;

        [TestInitialize]
        public void InitializeTest()
        {
            Database = new CouchDatabase(CouchHost, CouchPort, DatabaseName);
            Recogniser = new OctaveRecogniser(RedisHost, RedisPort);
        }

        [TestMethod]
        public void TestSetRecogniserInterface()
        {
            this.Recogniser.SetInterface(RedisHost, RedisPort);
        }

        [TestMethod]
        public void TestSendDataToCache()
        {
            var peopleInDatabase = Database.RetrieveAll();

            var sendDataToCacheMethod = Recogniser.GetType().GetMethod("SendDataToCacheForRetraining",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            sendDataToCacheMethod.Invoke(Recogniser, new Object[] { peopleInDatabase });
        }

        [TestMethod]
        public void TestMarshalFacialImage()
        {
            var image = this.TestImage;

            var marshalImageMethod = Recogniser.GetType().GetMethod("MarshalFacialImage",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var result = (string)marshalImageMethod.Invoke(Recogniser, new Object[] { image });

            var numPixelsInTestImage = this.TestImage.Width * this.TestImage.Height;
            var numPixelsInMarshalledImage = result.Split(',').Length;

            Assert.AreEqual(numPixelsInTestImage, numPixelsInMarshalledImage);
        }
    }
}