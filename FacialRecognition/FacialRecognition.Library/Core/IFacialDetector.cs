using System.Drawing;

namespace FacialRecognition.Library.Core
{
    interface IFacialDetector
    {
        Rectangle[] DetectFaces(Bitmap image);
    }
}