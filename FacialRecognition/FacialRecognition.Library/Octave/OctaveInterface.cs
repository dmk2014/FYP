using System;
using System.Diagnostics;

using StackExchange.Redis;

namespace FacialRecognition.Library.Octave
{
    public class OctaveInterface
    {
        //This should be a global constant, may move
        private ConnectionMultiplexer c_Connection;
        private IDatabase c_redisDatabase;

        public OctaveInterface(String Host, String Port)
        {
            this.c_Connection = ConnectionMultiplexer.Connect(Host + ":" + Port);
            this.c_redisDatabase = c_Connection.GetDatabase();
        }

        public Boolean SendRequest(OctaveMessage Message)
        {
            var _transaction = c_redisDatabase.CreateTransaction();

            _transaction.StringSetAsync("facial.request.code", Message.Code);
            _transaction.StringSetAsync("facial.request.data", Message.Data);
            _transaction.StringSetAsync("facial.response.code", (int)OctaveMessageType.NO_DATA);

            return _transaction.Execute();
        }

        public OctaveMessage ReceiveResponse(int Timeout)
        {
            OctaveMessage _response = null;
            var _watch = new Stopwatch();
            _watch.Start();

            while (_watch.ElapsedMilliseconds <= Timeout)
            {
                var _responseCodeString = c_redisDatabase.StringGet("facial.response.code");
                var _responseCode = int.Parse(_responseCodeString);

                if (_responseCode != (int)OctaveMessageType.NO_DATA)
                {
                    var _reponseCode = int.Parse(_responseCodeString);
                    var _reponseData = c_redisDatabase.StringGet("facial.response.data");
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