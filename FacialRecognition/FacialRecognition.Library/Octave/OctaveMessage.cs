using System;

namespace FacialRecognition.Library.Octave
{
    public class OctaveMessage
    {
        public int Code { set; get; }
        public string Data { set; get; }

        public OctaveMessage()
        {
            this.Code = (int)OctaveMessageType.NoData;
            this.Data = ((int)OctaveMessageType.NoData).ToString();
        }

        public OctaveMessage(int code)
        {
            this.Code = code;
            this.Data = ((int)OctaveMessageType.NoData).ToString();
        }

        public OctaveMessage(int code, string data)
        {
            this.Code = code;
            this.Data = data;
        }
    }
}