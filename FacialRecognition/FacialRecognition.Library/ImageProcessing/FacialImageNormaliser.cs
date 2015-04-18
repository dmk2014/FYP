using System.Drawing;

namespace FacialRecognition.Library.ImageProcessing
{
    public abstract class FacialImageNormaliser
    {
        /// <summary>
        /// Template method to normalise a facial image to set dimensions and colormap
        /// </summary>
        /// <param name="sourceImage">The image to be normalised</param>
        /// <param name="width">The width to normalise the image to</param>
        /// <param name="height">The height to normalise the image to</param>
        /// <returns>The normalised image</returns>
        public Image NormaliseImage(Image sourceImage, int width, int height)
        {
            Image normalisedImage;

            normalisedImage = this.Resize(sourceImage, width, height);
            normalisedImage = this.SetColormap(normalisedImage);

            return normalisedImage;
        }

        public abstract Image Resize(Image source, int width, int height);
        public abstract Image SetColormap(Image source);
    }
}