namespace FacialRecognition.Library.Octave
{
    public enum RecogniserCode
    {
        // Represents a key which contains no data
        NoData = 50,
        
        // The requests that can be sent to Octave
        RequestRecognition = 100,
        RequestReload = 200,
        RequestSave = 300,
        RequestRetrain = 400,

        // A request will either succeed or fail
        ResponseOk = 100,
        ResponseFail = 200
    }
}