using System.Drawing;

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;

namespace FacialRecognition.Library
{
    public class FacialDetector
    {
        public Rectangle[] DetectFaces(Bitmap image)
        {
            var _emguImage = new Image<Gray, byte>(image);

            var _classifier = new CascadeClassifier(@"C:\Emgu\emgucv-windows-universal-cuda 2.4.10.1940\bin\haarcascade_frontalface_default.xml");

            var _faces = _classifier.DetectMultiScale(_emguImage, 1.1, 3, Size.Empty, Size.Empty);

            return _faces;
        }
    }
}