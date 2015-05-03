using FacialRecognition.Library.ImageProcessing;
using System.Drawing;

namespace FacialRecognition.Controllers
{
    public class ImageProcessingController
    {
        /// <summary>
        /// Normalise a facial image for photmetric recognition. (Size: 168x192, Color: Grayscale).
        /// </summary>
        /// <param name="sourceImage">The facial image to be normalised.</param>
        /// <returns>A normalised facial image.</returns>
        public Image NormaliseFacialImage(Image sourceImage)
        {
            var normaliser = new PhotometricFacialImageNormaliser();

            return normaliser.NormaliseImage(sourceImage, 168, 192);
        }
    }
}