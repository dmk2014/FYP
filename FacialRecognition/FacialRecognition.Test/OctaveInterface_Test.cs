using FacialRecognition.Library.Octave;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using System;

namespace FacialRecognition.Test
{
    [TestClass]
    public class OctaveInterface_Test
    {
        private ConnectionMultiplexer Connection;
        private IDatabase RedisDatabase;

        private OctaveInterface Interface;
        private const string RedisHost = "localhost";
        private const int RedisPort = 6379;

        private const string RequestCodeKey = "facial.request.code";
        private const string RequestDataKey = "facial.request.data";
        private const string ResponseCodeKey = "facial.response.code";
        private const string ResponseDataKey = "facial.response.data";

        [TestInitialize]
        public void InitializeTest()
        {
            this.Connection = ConnectionMultiplexer.Connect(RedisHost + ":" + RedisPort);
            this.RedisDatabase = this.Connection.GetDatabase();
            this.Interface = new OctaveInterface(RedisHost, RedisPort);

            // Ensure a clear cache
            this.Interface.EnsurePersonDataIsClearedFromCache();
            this.RedisDatabase.KeyDelete(RequestCodeKey);
            this.RedisDatabase.KeyDelete(RequestDataKey);
            this.RedisDatabase.KeyDelete(ResponseCodeKey);
            this.RedisDatabase.KeyDelete(ResponseDataKey);
        }
        
        [TestMethod]
        public void TestSendPersonDataToCache()
        {
            // Create fake data
            var personLabel = "TestLabel";
            var personImage = "1,2,3,4,5";
            var numItemsInList = 10;

            for (var i = 0; i < numItemsInList; i++)
            {
                this.Interface.SendPersonDataToCache(personLabel, personImage);
            }

            // Assert that the correct amount of data was inserted
            var labelKey = "facial.database.labels";
            var dataKey = "facial.database.data";

            var labelCount = this.RedisDatabase.ListLength(labelKey);
            var dataCount = this.RedisDatabase.ListLength(dataKey);

            Assert.AreEqual(numItemsInList, labelCount);
            Assert.AreEqual(numItemsInList, dataCount);
        }

        [TestMethod]
        public void TestEnsurePersonDataIsClearedFromCache()
        {
            // Create fake data
            var personLabel = "TestLabel";
            var personImage = "1,2,3,4,5";
            var numItemsInList = 10;

            for (var i = 0; i < numItemsInList; i++)
            {
                this.Interface.SendPersonDataToCache(personLabel, personImage);
            }

            // Assert that the correct amount of data was inserted
            var labelKey = "facial.database.labels";
            var dataKey = "facial.database.data";

            var labelCount = this.RedisDatabase.ListLength(labelKey);
            var dataCount = this.RedisDatabase.ListLength(dataKey);

            Assert.AreEqual(numItemsInList, labelCount);
            Assert.AreEqual(numItemsInList, dataCount);

            // Clear all person data from the cache
            this.Interface.EnsurePersonDataIsClearedFromCache();

            // Assert that list size is 0, indicating data has been cleared
            labelCount = this.RedisDatabase.ListLength(labelKey);
            dataCount = this.RedisDatabase.ListLength(dataKey);

            Assert.AreEqual(0, labelCount);
            Assert.AreEqual(0, dataCount);
        }

        [TestMethod]
        public void TestSendRequest()
        {
            // Create a test message
            var testCode = 21;
            var testData = "testdata";
            var message = new OctaveMessage(testCode, testData);

            // Send the message to Redis
            this.Interface.SendRequest(message);
            
            // Manually retrieve the values of the requests keys
            var requestCode = int.Parse(this.RedisDatabase.StringGet(RequestCodeKey));
            var requestData = this.RedisDatabase.StringGet(RequestDataKey).ToString();

            // Assert that request keys received the correct values
            Assert.AreEqual(testCode, requestCode);
            Assert.AreEqual(testData, requestData);
        }

        [TestMethod]
        public void TestReceiveResponse()
        {
            // Send a fake response to Redis
            var responseCode = 22;
            var responseData = "responsetestdata";

            this.RedisDatabase.StringSet(ResponseCodeKey, responseCode);
            this.RedisDatabase.StringSet(ResponseDataKey, responseData);

            // Retrieve the response
            var timeout = 100;
            var result = this.Interface.ReceiveResponse(timeout);

            // Assert that the response specified was received
            Assert.AreEqual(responseCode, result.Code);
            Assert.AreEqual(responseData, result.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void TestTimeout()
        {
            // Send a request for which there will be no response
            // A TimeoutException should be thrown
            var timeoutTenSeconds = 10000;
            this.Interface.ReceiveResponse(timeoutTenSeconds);
        }
    }
}