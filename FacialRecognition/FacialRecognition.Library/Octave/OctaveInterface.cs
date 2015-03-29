using System;
using System.Diagnostics;

using StackExchange.Redis;

namespace FacialRecognition.Library.Octave
{
    public class OctaveInterface
    {
        private ConnectionMultiplexer Connection;
        private IDatabase RedisDatabase;

        // Database Related Redis Keys
        private String DatabaseLabelsKey = "facial.database.labels";
        private String DatabaseDataKey = "facial.database.data";

        // Message Related Redis Keys
        private String FacialRequestCodeKey = "facial.request.code";
        private String FacialRequestDataKey = "facial.request.data";
        private String FacialResponseCodeKey = "facial.response.code";
        private String FacialResponseDataKey = "facial.response.data";

        public OctaveInterface(String RedisHost, int RedisPort)
        {
            this.Connection = ConnectionMultiplexer.Connect(RedisHost + ":" + RedisPort);
            this.RedisDatabase = Connection.GetDatabase();
        }

        public void EnsurePersonDataIsClearedFromCache()
        {
            var transaction = RedisDatabase.CreateTransaction();

            transaction.KeyDeleteAsync(this.DatabaseLabelsKey);
            transaction.KeyDeleteAsync(this.DatabaseDataKey);

            transaction.Execute();
        }

        public void SendPersonDataToCache(String PersonLabel, String ImageAsString)
        {
            var transaction = RedisDatabase.CreateTransaction();

            transaction.ListRightPushAsync(this.DatabaseLabelsKey, PersonLabel);
            transaction.ListRightPushAsync(this.DatabaseDataKey, ImageAsString);

            transaction.Execute();
        }

        public Boolean SendRequest(OctaveMessage Message)
        {
            var transaction = RedisDatabase.CreateTransaction();

            transaction.StringSetAsync(this.FacialRequestCodeKey, Message.Code);
            transaction.StringSetAsync(this.FacialRequestDataKey, Message.Data);
            transaction.StringSetAsync(this.FacialResponseCodeKey, (int)OctaveMessageType.NoData);
            transaction.StringSetAsync(this.FacialResponseDataKey, (int)OctaveMessageType.NoData);

            return transaction.Execute();
        }

        public OctaveMessage ReceiveResponse(int Timeout)
        {
            var response = new OctaveMessage();
            var responseReceived = false;
            var watch = new Stopwatch();

            watch.Start();

            while (watch.ElapsedMilliseconds <= Timeout && !responseReceived)
            {
                var responseCodeString = RedisDatabase.StringGet(this.FacialResponseCodeKey);
                var responseCode = int.Parse(responseCodeString);

                if (responseCode != (int)OctaveMessageType.NoData)
                {
                    var responseData = RedisDatabase.StringGet(this.FacialResponseDataKey);
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
                throw new TimeoutException("A response was not received from Octave in the specified time (" + Timeout + "ms)");
            }
        }
    }
}