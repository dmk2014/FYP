using System;
using System.Drawing;

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
                _result._id = _response.Data;
                return _result;
            }
            else
            {
                throw new Exception(_response.Data);
            }
        }

        private String MarshalFacialImage(Image FacialImage)
        {
            var _facialBitmap = new Bitmap(FacialImage);
            var _data = String.Empty;

            for (int i = 0; i < FacialImage.Height; i++)
            {
                for (int j = 0; j < FacialImage.Width; j++)
                {
                    //BGR all have same value, we'll take one for now
                    var _pixel = _facialBitmap.GetPixel(j, i);
                    var _value = _pixel.B;
                    _data += _value + ",";
                }
            }

            //Remove trailing ','

            _data = _data.TrimEnd(',');

            return _data;
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

        public Boolean RetrainRecogniser()
        {
            var _message = new OctaveMessage((int)OctaveMessageType.REQUEST_RETRAIN, String.Empty);
            c_Interface.SendRequest(_message);

            var _response = c_Interface.ReceiveResponse(1800000); //30 mins

            if (_response.Code == (int)OctaveMessageType.RESPONSE_OK)
            {
                return true;
            }
            else
            {
                throw new Exception(_response.Data);
            }
        }
    }
}