using System.Drawing;

namespace FacialRecognition.Library.Detection
{
    interface IFacialDetector
    {
        Rectangle[] DetectFaces(Bitmap image);
    }
}