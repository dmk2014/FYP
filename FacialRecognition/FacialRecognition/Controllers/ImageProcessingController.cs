using FacialRecognition.Library.ImageProcessing;
using System.Drawing;

namespace FacialRecognition.Controllers
{
    public class ImageProcessingController
    {
        public Image NormaliseFacialImage(Image sourceImage)
        {
            var normaliser = new PhotometricFacialImageNormaliser();

            return normaliser.NormaliseImage(sourceImage, 168, 192);
        }
    }
}