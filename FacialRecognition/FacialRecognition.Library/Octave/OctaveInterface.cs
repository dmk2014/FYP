using StackExchange.Redis;
using System;
using System.Diagnostics;

namespace FacialRecognition.Library.Octave
{
    public class OctaveInterface
    {
        private ConnectionMultiplexer Connection;
        private IDatabase RedisDatabase;

        // Database Related Redis Keys
        private string DatabaseLabelsKey = "facial.database.labels";
        private string DatabaseDataKey = "facial.database.data";

        // Message Related Redis Keys
        private string FacialRequestCodeKey = "facial.request.code";
        private string FacialRequestDataKey = "facial.request.data";
        private string FacialResponseCodeKey = "facial.response.code";
        private string FacialResponseDataKey = "facial.response.data";

        public OctaveInterface(string redisHost, int redisPort)
        {
            this.Connection = ConnectionMultiplexer.Connect(redisHost + ":" + redisPort);
            this.RedisDatabase = this.Connection.GetDatabase();
        }

        public void EnsurePersonDataIsClearedFromCache()
        {
            var transaction = this.RedisDatabase.CreateTransaction();

            transaction.KeyDeleteAsync(this.DatabaseLabelsKey);
            transaction.KeyDeleteAsync(this.DatabaseDataKey);

            transaction.Execute();
        }

        public void SendPersonDataToCache(string personLabel, string imageAsString)
        {
            var transaction = this.RedisDatabase.CreateTransaction();

            transaction.ListRightPushAsync(this.DatabaseLabelsKey, personLabel);
            transaction.ListRightPushAsync(this.DatabaseDataKey, imageAsString);

            transaction.Execute();
        }

        public bool SendRequest(OctaveMessage message)
        {
            var transaction = this.RedisDatabase.CreateTransaction();

            transaction.StringSetAsync(this.FacialRequestCodeKey, message.Code);
            transaction.StringSetAsync(this.FacialRequestDataKey, message.Data);
            transaction.StringSetAsync(this.FacialResponseCodeKey, (int)OctaveMessageType.NoData);
            transaction.StringSetAsync(this.FacialResponseDataKey, (int)OctaveMessageType.NoData);

            return transaction.Execute();
        }

        public OctaveMessage ReceiveResponse(int timeout)
        {
            var response = new OctaveMessage();
            var responseReceived = false;
            var watch = new Stopwatch();

            watch.Start();

            while (watch.ElapsedMilliseconds <= timeout && !responseReceived)
            {
                var responseCodeString = this.RedisDatabase.StringGet(this.FacialResponseCodeKey);
                var responseCode = int.Parse(responseCodeString);

                if (responseCode != (int)OctaveMessageType.NoData)
                {
                    var responseData = this.RedisDatabase.StringGet(this.FacialResponseDataKey);
                    response = new OctaveMessage(responseCode, responseData);
                    responseReceived = true;
                }
            }

            if (response.Code != (int)OctaveMessageType.NoData)
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