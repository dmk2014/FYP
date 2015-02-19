using ServiceStack.Redis;
using System;
using System.Diagnostics;

namespace FacialRecognition.Library.Octave
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
            _transaction.QueueCommand(r => r.Set("facial.response.code", (int)OctaveMessageType.NO_DATA));

            return _transaction.Commit();
        }

        public OctaveMessage ReceiveResponse(int Timeout)
        {
            OctaveMessage _response = null;
            var _watch = new Stopwatch();
            _watch.Start();

            while (_watch.ElapsedMilliseconds <= Timeout)
            {
                var _responseCodeString = c_Client.GetValue("facial.response.code");
                var _responseCode = int.Parse(_responseCodeString);

                if (_responseCode != (int)OctaveMessageType.NO_DATA)
                {
                    var _reponseCode = int.Parse(_responseCodeString);
                    var _reponseData = c_Client.GetValue("facial.response.data");
                    _response = new OctaveMessage(_reponseCode, _reponseData);
                    break;
                }
            }

            if (_response != null)
            {
                return _response;
            }
            else
            {
                throw new TimeoutException("A response was not received from Octave in the specified time (" + Timeout + "ms)");
            }
        }
    }
}