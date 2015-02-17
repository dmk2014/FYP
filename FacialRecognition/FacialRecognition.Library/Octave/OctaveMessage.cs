using System;

namespace FacialRecognition.Library.Octave
{
    public class OctaveMessage
    {
        public int Code { set; get; }
        public String Data { set; get; }

        public OctaveMessage(int Code, String Data)
        {
            this.Code = Code;
            this.Data = Data;
        }
    }
}