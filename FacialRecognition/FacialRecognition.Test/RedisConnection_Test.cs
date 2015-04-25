using FacialRecognition.Library.Recognition;
using FacialRecognition.Library.Redis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FacialRecognition.Test
{
    [TestClass]
    public class RedisConnection_Test
    {
        private StackExchange.Redis.ConnectionMultiplexer ConnectionMultiplexer;
        private StackExchange.Redis.IDatabase RedisDatabase;

        private RedisConnection RedisConnection;
        private const string RedisHost = "localhost";
        private const int RedisPort = 6379;

        private const string RequestCodeKey = "facial.request.code";
        private const string RequestDataKey = "facial.request.data";
        private const string ResponseCodeKey = "facial.response.code";
        private const string ResponseDataKey = "facial.response.data";
        private const string RecogniserStatusKey = "facial.recogniser.status";

        [TestInitialize]
        public void InitializeTest()
        {
            this.ConnectionMultiplexer = StackExchange.Redis.ConnectionMultiplexer.Connect(RedisHost + ":" + RedisPort);
            this.RedisDatabase = this.ConnectionMultiplexer.GetDatabase();
            this.RedisConnection = new RedisConnection(RedisHost, RedisPort);

            // Ensure a clear cache
            this.RedisConnection.EnsurePersonDataIsClearedFromCache();
            this.RedisDatabase.KeyDelete(RequestCodeKey);
            this.RedisDatabase.KeyDelete(RequestDataKey);
            this.RedisDatabase.KeyDelete(ResponseCodeKey);
            this.RedisDatabase.KeyDelete(ResponseDataKey);
        }

        [TestMethod]
        public void TestCreateRedisConnection()
        {
            // No exception indicates success
            var octaveInterface = new RedisConnection(RedisHost, RedisPort);
        }

        [TestMethod]
        [ExpectedException(typeof(StackExchange.Redis.RedisConnectionException))]
        public void TestCreateRedisConnectionUsingNoExistantServer()
        {
            var host = "localhost";
            var port = 123;

            var octaveInterface = new RedisConnection(host, port);
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
                this.RedisConnection.SendPersonDataToCache(personLabel, personImage);
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
                this.RedisConnection.SendPersonDataToCache(personLabel, personImage);
            }

            // Assert that the correct amount of data was inserted
            var labelKey = "facial.database.labels";
            var dataKey = "facial.database.data";

            var labelCount = this.RedisDatabase.ListLength(labelKey);
            var dataCount = this.RedisDatabase.ListLength(dataKey);

            Assert.AreEqual(numItemsInList, labelCount);
            Assert.AreEqual(numItemsInList, dataCount);

            // Clear all person data from the cache
            this.RedisConnection.EnsurePersonDataIsClearedFromCache();

            // Assert that list size is 0, indicating data has been cleared
            labelCount = this.RedisDatabase.ListLength(labelKey);
            dataCount = this.RedisDatabase.ListLength(dataKey);

            Assert.AreEqual(0, labelCount);
            Assert.AreEqual(0, dataCount);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        public void TestIsRecogniserAvailableThrowsException()
        {
            this.RedisDatabase.KeyDelete(RecogniserStatusKey);

            var isRecogniserAvailableMethod = this.RedisConnection.GetType().GetMethod("IsRecogniserAvailable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            isRecogniserAvailableMethod.Invoke(this.RedisConnection, new Object[] { });
        }

        [TestMethod]
        public void TestIsRecogniserAvailableSucceeds()
        {
            this.RedisDatabase.StringSet(RecogniserStatusKey, (int)RecogniserStatus.Available);

            var isRecogniserAvailableMethod = this.RedisConnection.GetType().GetMethod("IsRecogniserAvailable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var result = (bool)isRecogniserAvailableMethod.Invoke(this.RedisConnection, new Object[] { });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSendRequestUsingRedisConnection()
        {
            // Create a test message
            var testCode = 21;
            var testData = "testdata";
            var message = new RedisMessage(testCode, testData);

            // Send the message to Redis
            this.RedisConnection.SendRequest(message);
            
            // Manually retrieve the values of the requests keys
            var requestCode = int.Parse(this.RedisDatabase.StringGet(RequestCodeKey));
            var requestData = this.RedisDatabase.StringGet(RequestDataKey).ToString();

            // Assert that request keys received the correct values
            Assert.AreEqual(testCode, requestCode);
            Assert.AreEqual(testData, requestData);
        }

        [TestMethod]
        public void TestReceiveResponseUsingRedisConnection()
        {
            // Send a fake response to Redis
            var responseCode = 22;
            var responseData = "responsetestdata";

            this.RedisDatabase.StringSet(ResponseCodeKey, responseCode);
            this.RedisDatabase.StringSet(ResponseDataKey, responseData);

            // Retrieve the response
            var timeout = 100;
            var result = this.RedisConnection.ReceiveResponse(timeout);

            // Assert that the response specified was received
            Assert.AreEqual(responseCode, result.Code);
            Assert.AreEqual(responseData, result.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void TestRedisConnectionReceiveResponseTimeout()
        {
            // Send a request for which there will be no response
            // A TimeoutException should be thrown
            var timeoutTenSeconds = 10000;
            this.RedisConnection.ReceiveResponse(timeoutTenSeconds);
        }
    }
}