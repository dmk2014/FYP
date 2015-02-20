namespace FacialRecognition.Library.Octave
{
    public enum OctaveMessageType
    {
        NO_DATA = 50,

        REQUEST_REC = 100,
        REQUEST_RELOAD = 200,
        REQUEST_SAVE = 300,
        REQUEST_RETRAIN = 400,

        RESPONSE_OK = 100,
        RESPONSE_FAIL = 200
    }
}