using System;

namespace FacialRecognition.Library.Octave
{
    public class OctaveMessage
    {
        public OctaveMessageType Code { set; get; }
        public String Data { set; get; }

        public OctaveMessage(OctaveMessageType Code, String Data)
        {
            this.Code = Code;
            this.Data = Data;
        }
    }
}