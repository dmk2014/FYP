﻿using System;
using System.Drawing;
using System.Collections.Generic;

namespace FacialRecognition.Library.Octave
{
    public class OctaveRecogniser : Core.IFacialRecogniser
    {
        private OctaveInterface c_Interface;

        public OctaveRecogniser(OctaveInterface Interface)
        {
            this.SetInterface(Interface);
        }

        public void SetInterface(OctaveInterface Interface)
        {
            this.c_Interface = Interface;
        }

        public Models.Person ClassifyFace(Image FacialImage)
        {
            //FacialImage is normales, need to marshal it to a string
            var _imageAsString = this.MarshalFacialImage(FacialImage);

            var _message = new OctaveMessage((int)OctaveMessageType.REQUEST_REC, _imageAsString);
            c_Interface.SendRequest(_message);

            var _response = c_Interface.ReceiveResponse(15000);

            if (_response.Code == (int)OctaveMessageType.RESPONSE_OK)
            {
                var _result = new Models.Person();
                _result.Id = _response.Data;
                return _result;
            }
            else
            {
                throw new Exception(_response.Data);
            }
        }

        private String MarshalFacialImage(Image FacialImage)
        {
            var facialBitmap = new Bitmap(FacialImage);
            var faceAsString = String.Empty;

            for (int column = 0; column < FacialImage.Width; column++)
            {
                for (int row = 0; row < FacialImage.Height; row++)
                {
                    // BGR all have same values - can use any one of these to produce 8 bit grayscale image
                    // Octave requires that data is passed in column major order
                    // column1
                    // column2
                    // columnN
                    var pixel = facialBitmap.GetPixel(column, row);
                    var value = pixel.B;
                    faceAsString += value + ",";
                }
            }

            //Remove trailing ','

            faceAsString = faceAsString.TrimEnd(',');

            return faceAsString;
        }

        public Boolean SaveSession()
        {
            var _message = new OctaveMessage((int)OctaveMessageType.REQUEST_SAVE, String.Empty);
            c_Interface.SendRequest(_message);

            var _response = c_Interface.ReceiveResponse(30000);

            if (_response.Code == (int)OctaveMessageType.RESPONSE_OK)
            {
                return true;
            }
            else
            {
                throw new Exception(_response.Data);
            }
        }

        public Boolean ReloadSession()
        {
            var _message = new OctaveMessage((int)OctaveMessageType.REQUEST_RELOAD, String.Empty);
            c_Interface.SendRequest(_message);

            var _response = c_Interface.ReceiveResponse(30000);

            if (_response.Code == (int)OctaveMessageType.RESPONSE_OK)
            {
                return true;
            }
            else
            {
                throw new Exception(_response.Data);
            }
        }

        public Boolean RetrainRecogniser(List<Models.Person> PeopleInDatabase)
        {
            // Prepare the data for retraining
            this.SendDataToCacheForRetraining(PeopleInDatabase);

            // Send a request to retrain the recogniser
            var _message = new OctaveMessage((int)OctaveMessageType.REQUEST_RETRAIN, String.Empty);
            c_Interface.SendRequest(_message);

            // Wait for a response
            int timeoutThirtyMinutes = 1800000;
            var _response = c_Interface.ReceiveResponse(timeoutThirtyMinutes);

            if (_response.Code == (int)OctaveMessageType.RESPONSE_OK)
            {
                return true;
            }
            else
            {
                throw new Exception(_response.Data);
            }
        }

        private void SendDataToCacheForRetraining(List<Models.Person> PeopleInDatabase)
        {
            // Send all data to the cache
            foreach(var person in PeopleInDatabase)
            {
                foreach(var image in person.Images)
                {
                    c_Interface.SendPersonDataToCache(person.Id, this.MarshalFacialImage(image));
                }
            }

            // Mark end of data in the cache - required by Octave
            c_Interface.SendPersonDataToCache(((int)OctaveMessageType.NO_DATA).ToString(), ((int)OctaveMessageType.NO_DATA).ToString());
        }
    }
}