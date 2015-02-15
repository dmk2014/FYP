using ServiceStack.Redis;
using System;

namespace FacialRecognition.Library
{
    public class OctaveInterface
    {
        RedisClient c_Client;

        public OctaveInterface(RedisClient Client)
        {
            //TODO
            this.c_Client = Client;
        }

        public void SendRequest(OctaveMessage Type, String Data)
        {
            //TODO
            var _transaction = new RedisTransaction(c_Client);
            _transaction.QueueCommand(r => r.Set("facial.request.code", Type));
            _transaction.QueueCommand(r => r.Set("facial.request.data", Data));

            _transaction.Commit();
        }

        public void ReceiveResponse()
        {
            //TODO
            //We need to allow Redis time to process request
            //Maybe set a timer, pass timeout as argument?

            do
            {
                var _responseCodeString = c_Client.Get("facial.response.code").ToString();
                var _reponseCode = int.Parse(_responseCodeString);

                if (_reponseCode != (int)OctaveMessage.NO_DATA)
                {
                    //TODO - parse response
                    var _reponseData = c_Client.Get("facial.response.data").ToString();
                }
            } while (true);
        }
    }
}