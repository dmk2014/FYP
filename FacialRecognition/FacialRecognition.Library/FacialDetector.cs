using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using System.Drawing;

namespace FacialRecognition.Library
{
    public class FacialDetector
    {
        public void DetectFaces(Bitmap image)
        {
            CascadeClassifier classifier = new CascadeClassifier("haarcascade_frontalface_alt2.xml");

            // Load target image
            var gray = new Mat("faces.png", LoadMode.GrayScale);

            // Detect faces
            Rect[] faces = classifier.DetectMultiScale(gray,
                1.08,
                2,
                HaarDetectionType.ScaleImage,
                new OpenCvSharp.CPlusPlus.Size(30, 30));
        }
    }
}