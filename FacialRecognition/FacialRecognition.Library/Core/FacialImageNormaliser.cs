using System.Drawing;

namespace FacialRecognition.Library.Core
{
    public abstract class FacialImageNormaliser
    {
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