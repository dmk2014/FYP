namespace FacialRecognition.Library.Core
{
    interface IFacialRecogniser
    {
        Models.Person ClassifyFace(System.Drawing.Bitmap FacialImage);
    }
}