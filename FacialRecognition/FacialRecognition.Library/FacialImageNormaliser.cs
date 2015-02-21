using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FacialRecognition.Library
{
    public abstract class FacialImageNormaliser
    {
        public Image NormaliseImage(Image SourceImage, int Width, int Height)
        {
            Image _normalisedImage;
            //Resize
            _normalisedImage = this.Resize(SourceImage, Width, Height);

            //Convert to Grayscale
            _normalisedImage = this.SetColormap(SourceImage);

            throw new NotImplementedException();
        }

        public abstract Image Resize(Image Source, int Width, int Height);
        public abstract Image SetColormap(Image Source);
    }
}