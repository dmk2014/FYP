﻿using ServiceStack.Redis;
using System;
using System.Diagnostics;

namespace FacialRecognition.Library
{
    public class OctaveInterface
    {
        private RedisClient c_Client;

        public OctaveInterface(RedisClient Client)
        {
            this.c_Client = Client;
        }

        public void SetRedisClient(RedisClient Client)
        {
            this.c_Client = Client;
        }

        public Boolean SendRequest(OctaveMessageType Type, String Data)
        {
            var _transaction = new RedisTransaction(c_Client);
            _transaction.QueueCommand(r => r.Set("facial.request.code", Type));
            _transaction.QueueCommand(r => r.Set("facial.request.data", Data));

            return _transaction.Commit();
        }

        public OctaveMessage ReceiveResponse(int Timeout)
        {
            OctaveMessage _response = null;
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (watch.ElapsedMilliseconds <= Timeout)
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

            if (_response == null)
            {
                throw new TimeoutException("A response was not received from Octave in the specified time (" + Timeout + "ms)");
            }
            else
            {
                return _response;
            }
        }
    }
}