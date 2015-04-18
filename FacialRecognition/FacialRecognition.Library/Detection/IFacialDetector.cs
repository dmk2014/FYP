using System.Drawing;

namespace FacialRecognition.Library.Detection
{
    public interface IFacialDetector
    {
        Rectangle[] DetectFaces(Bitmap image);
    }
}