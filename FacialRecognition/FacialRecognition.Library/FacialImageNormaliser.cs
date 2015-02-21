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

            _normalisedImage = this.Resize(SourceImage, Width, Height);
            _normalisedImage = this.SetColormap(_normalisedImage);

            return _normalisedImage;
        }

        public abstract Image Resize(Image Source, int Width, int Height);
        public abstract Image SetColormap(Image Source);
    }
}