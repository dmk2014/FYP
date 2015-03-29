using System;

namespace FacialRecognition.Library.Octave
{
    public class OctaveMessage
    {
        public int Code { set; get; }
        public String Data { set; get; }

        public OctaveMessage()
        {
            this.Code = (int)OctaveMessageType.NoData;
            this.Data = ((int)OctaveMessageType.NoData).ToString();
        }

        public OctaveMessage(int Code)
        {
            this.Code = Code;
            this.Data = ((int)OctaveMessageType.NoData).ToString();
        }

        public OctaveMessage(int Code, String Data)
        {
            this.Code = Code;
            this.Data = Data;
        }
    }
}