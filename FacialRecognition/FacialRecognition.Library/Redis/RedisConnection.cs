using FacialRecognition.Library.Recognition;
using System;
using System.Diagnostics;
using System.Threading;

namespace FacialRecognition.Library.Redis
{
    public class RedisConnection
    {
        private StackExchange.Redis.ConnectionMultiplexer Connection;
        private StackExchange.Redis.IDatabase RedisDatabase;

        // Database Related Redis Keys
        private string DatabaseLabelsKey = "facial.database.labels";
        private string DatabaseDataKey = "facial.database.data";

        // Message Related Redis Keys
        private string FacialRequestCodeKey = "facial.request.code";
        private string FacialRequestDataKey = "facial.request.data";
        private string FacialResponseCodeKey = "facial.response.code";
        private string FacialResponseDataKey = "facial.response.data";

        // Recogniser Status Key
        private string FacialRecogniserStatusKey = "facial.recogniser.status";

        /// <summary>
        /// Connects a Redis connection, for sending messages to Octave, to the server at the specified host and port.
        /// </summary>
        /// <param name="redisHost">Host where Redis server is running.</param>
        /// <param name="redisPort">Port where Redis server is running.</param>
        public RedisConnection(string redisHost, int redisPort)
        {
            this.Connection = StackExchange.Redis.ConnectionMultiplexer.Connect(redisHost + ":" + redisPort);
            this.RedisDatabase = this.Connection.GetDatabase();
        }

        /// <summary>
        /// Ensures that the facial.database.* keys are cleared from Redis.
        /// </summary>
        public void EnsurePersonDataIsClearedFromCache()
        {
            var transaction = this.RedisDatabase.CreateTransaction();

            transaction.KeyDeleteAsync(this.DatabaseLabelsKey);
            transaction.KeyDeleteAsync(this.DatabaseDataKey);

            transaction.Execute();
        }

        /// <summary>
        /// Appends the person data provided to the facial.data.* lists in Redis
        /// </summary>
        /// <param name="personLabel">The label of the person being added.</param>
        /// <param name="imageAsString">An image of the person in string delimited format.</param>
        public void SendPersonDataToCache(string personLabel, string imageAsString)
        {
            var transaction = this.RedisDatabase.CreateTransaction();

            transaction.ListRightPushAsync(this.DatabaseLabelsKey, personLabel);
            transaction.ListRightPushAsync(this.DatabaseDataKey, imageAsString);

            transaction.Execute();
        }

        private bool IsRecogniserAvailable()
        {
            var status = this.RedisDatabase.StringGet(this.FacialRecogniserStatusKey).ToString();

            if (status != null)
            {
                var statusCode = int.Parse(status);

                if (statusCode == (int)RecogniserStatus.Available)
                    return true;
                else
                    throw new Exception("Octave reports that it is busy and cannot currently handle a request. Please wait and try again.");
            }
            else
            {
                throw new Exception("The status of the recogniser could not be determined. " +
                    "Please ensure that it is running and correct Redis address has been supplied.");
            }
        }

        /// <summary>
        /// Send a request to the facial recogniser using Redis.
        /// </summary>
        /// <param name="message">An OctaveMessage that specifies the requirements of the request.</param>
        /// <returns>Boolean value indicating if sending the request was successful.</returns>
        public bool SendRequest(RedisMessage message)
        {
            if (this.IsRecogniserAvailable())
            {
                var transaction = this.RedisDatabase.CreateTransaction();

                transaction.StringSetAsync(this.FacialRequestCodeKey, message.Code);
                transaction.StringSetAsync(this.FacialRequestDataKey, message.Data);
                transaction.StringSetAsync(this.FacialResponseCodeKey, (int)RecogniserCode.NoData);
                transaction.StringSetAsync(this.FacialResponseDataKey, (int)RecogniserCode.NoData);

                return transaction.Execute();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Wait for a response via Redis from the facial recogniser. 
        /// </summary>
        /// <param name="timeout">The maximum time (in milliseconds) to wait for a response.</param>
        /// <returns>An OctaveMessage containing the response code & data.</returns>
        public RedisMessage ReceiveResponse(int timeout)
        {
            var response = new RedisMessage();
            var responseReceived = false;
            var watch = new Stopwatch();

            watch.Start();

            while (watch.ElapsedMilliseconds <= timeout && !responseReceived)
            {
                var responseCodeString = this.RedisDatabase.StringGet(this.FacialResponseCodeKey).ToString();

                if (responseCodeString != null)
                {
                    var responseCode = int.Parse(responseCodeString);

                    if (responseCode != (int)RecogniserCode.NoData)
                    {
                        var responseData = this.RedisDatabase.StringGet(this.FacialResponseDataKey);
                        response = new RedisMessage(responseCode, responseData);
                        responseReceived = true;
                    }
                }

                // Wait 100 milliseconds between each check for a response
                // This improves performance - CPU usage of application reduced from 25% to between 1 and 3% on test machine
                Thread.Sleep(100);
            }

            if (response.Code != (int)RecogniserCode.NoData)
            {
                return response;
            }
            else
            {
                throw new TimeoutException("A response was not received from Octave in the specified time (" + timeout + "ms)");
            }
        }
    }
}