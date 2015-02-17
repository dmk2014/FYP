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

        public Models.Person ClassifyFace(System.Drawing.Bitmap FacialImage)
        {
            //Normalize request image -> maybe should be done earlier than here
            var _message = new OctaveMessage((int)OctaveMessageType.REQUEST_REC, FacialImage.ToString());
            c_Interface.SendRequest(_message);

            var _response = c_Interface.ReceiveResponse(15000);
            var _result = new Models.Person();
            _result._id = _response.Data;

            return _result;
        }
    }
}