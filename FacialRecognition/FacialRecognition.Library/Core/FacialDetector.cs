using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace FacialRecognition.Library.Core
{
    public class FacialDetector : FacialRecognition.Library.Core.IFacialDetector
    {
        // These variables are used to tune the cascade classifier
        // The following links are sources that were consulted when deciding these values:
        // http://stackoverflow.com/questions/20801015/recommended-values-for-opencv-detectmultiscale-parameters

        // ScaleFactor is how much the image size is reduced at each image scale
        // 5% here. Low value, compationally tougher -> greater than 2x slower than using 10%
        // It should produce better recognition results
        double ScaleFactor = 1.05;

        // How many neighbours a candidate rectangle requires to be classed as a face
        // The following article was consulted when researching this value:
        // http://fewtutorials.bravesites.com/entries/emgu-cv-c/level-3c---how-to-improve-face-detection
        int MinimumNeighbours = 6;

        // MinimumSize is the minimum possible object size, i.e. smaller objects are ignored
        Size MinimumSize = new Size(50, 50);

        // MaximumSize is the maximum possible object size, i.e. larger objects are ignored
        // This is not specified - detect faces of any size, once they are larger than that specified
        // in MinimumSize
        Size MaximumSize = Size.Empty;

        // Location of cascade classifier
        String ClassifierPath = @"C:\Emgu\emgucv-windows-universal-cuda 2.4.10.1940\bin\haarcascade_frontalface_default.xml";

        public Rectangle[] DetectFaces(Bitmap image)
        {
            var emguImage = new Image<Gray, byte>(image);

            var classifier = new CascadeClassifier(this.ClassifierPath);

            var faces = classifier.DetectMultiScale(emguImage, this.ScaleFactor, this.MinimumNeighbours, this.MinimumSize, this.MaximumSize);

            return faces;
        }
    }
}