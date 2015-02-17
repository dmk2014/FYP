using ServiceStack.Redis;
using System;
using System.Diagnostics;

namespace FacialRecognition.Library
{
    public class OctaveInterface
    {
        private RedisClient c_Client;

        public OctaveInterface(RedisClient Client)
        {
            this.SetRedisClient(Client);
        }

        public void SetRedisClient(RedisClient Client)
        {
            this.c_Client = Client;
        }

        public Boolean SendRequest(OctaveMessage Message)
        {
            var _transaction = new RedisTransaction(c_Client);
            _transaction.QueueCommand(r => r.Set("facial.request.code", Message.Code));
            _transaction.QueueCommand(r => r.Set("facial.request.data", Message.Data));

            return _transaction.Commit();
        }

        public OctaveMessage ReceiveResponse(int Timeout)
        {
            OctaveMessage _response = null;
            var _watch = new Stopwatch();
            _watch.Start();

            while (_watch.ElapsedMilliseconds <= Timeout)
            {
                var _responseCodeString = c_Client.Get("facial.response.code").ToString();
                var _reponseCode = int.Parse(_responseCodeString);

                if (_reponseCode != (int)OctaveMessageType.NO_DATA)
                {
                    var _reponseData = c_Client.Get("facial.response.data").ToString();
                    _response = new OctaveMessage((OctaveMessageType) _reponseCode, _reponseData);
                    break;
                }
            }

            if (_response != null)
            {
                c_Client.Set("facial.response.code", (int)OctaveMessageType.NO_DATA);
                return _response;
            }
            else
            {
                throw new TimeoutException("A response was not received from Octave in the specified time (" + Timeout + "ms)");
            }
        }
    }
}