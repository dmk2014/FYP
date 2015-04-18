using System.Drawing;
using System.IO;

namespace FacialRecognition.Library.Detection
{
    public class FacialDetector : IFacialDetector
    {
        // These variables are used to tune the cascade classifier
        // The following links are sources that were consulted when deciding these values:
        // http://stackoverflow.com/questions/20801015/recommended-values-for-opencv-detectmultiscale-parameters

        // ScaleFactor is how much the image size is reduced at each image scale
        // 5% here. Low value, compationally tougher -> greater than 2x slower than using 10%
        // It should produce better recognition results
        private double ScaleFactor = 1.05;

        // How many neighbours a candidate rectangle requires to be classed as a face
        // The following article was consulted when researching this value:
        // http://fewtutorials.bravesites.com/entries/emgu-cv-c/level-3c---how-to-improve-face-detection
        private int MinimumNeighbours = 6;

        // MinimumSize is the minimum possible object size, i.e. smaller objects are ignored
        private Size MinimumSize = new Size(50, 50);

        // MaximumSize is the maximum possible object size, i.e. larger objects are ignored
        // This is not specified - detect faces of any size, once they are larger than that specified
        // in MinimumSize
        private Size MaximumSize = Size.Empty;

        // Location of cascade classifier
        private string ClassifierPath;

        /// <summary>
        /// Creates a FacialDetector that utilises the default EmguCV cascade classifier
        /// </summary>
        public FacialDetector()
        {
            this.ClassifierPath = @"C:\Emgu\emgucv-windows-universal-cuda 2.4.10.1940\bin\haarcascade_frontalface_default.xml";
        }

        /// <summary>
        /// Creates a FacialDector that utilises a specified classifier
        /// </summary>
        /// <param name="pathToClassifier">The location of the classifier that is to be used</param>
        public FacialDetector(string pathToClassifier)
        {
            if(File.Exists(pathToClassifier))
            {
                this.ClassifierPath = pathToClassifier;
            }
            else
            {
                throw new FileNotFoundException("The specified classifier (" + pathToClassifier + ") could not be found");
            }
        }
        
        /// <summary>
        /// Detects faces present in a given Bitmap image
        /// </summary>
        /// <param name="image">The source Bitmap image on which to perform detection</param>
        /// <returns>A array of System.Drawing.Rectangle whose contents defines the locations of all detected faces</returns>
        public Rectangle[] DetectFaces(Bitmap image)
        {
            var emguImage = new Emgu.CV.Image<Emgu.CV.Structure.Gray, byte>(image);

            var classifier = new Emgu.CV.CascadeClassifier(this.ClassifierPath);

            var faces = classifier.DetectMultiScale(emguImage, this.ScaleFactor, this.MinimumNeighbours, this.MinimumSize, this.MaximumSize);

            return faces;
        }
    }
}