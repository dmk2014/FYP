using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FacialRecognition.Library
{
    public class OctaveNormaliser : FacialImageNormaliser
    {
        public override Image Resize(Image Source, int Width, int Height)
        {
            var _resizedImage = new Bitmap(Width, Height);
            var _graphics = Graphics.FromImage(_resizedImage);

            _graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            _graphics.DrawImage(Source, 0, 0, Width, Height);
            _graphics.Dispose();

            return _resizedImage;
        }

        public override Image SetColormap(Image Source)
        {
            //Reference: http://tech.pro/tutorial/660/csharp-tutorial-convert-a-color-image-to-grayscale

            var _originalImage = new Bitmap(Source);
            var _result = new Bitmap(Source.Width, Source.Height);

            for (int i = 0; i < Source.Width; i++)
            {
                for (int j = 0; j < Source.Height; j++)
                {
                    var _pixel = _originalImage.GetPixel(i, j);
                    var _pixelGrayscale = (_pixel.R + _pixel.G + _pixel.B) / 3;
                    var _pixelAsColor = Color.FromArgb(_pixelGrayscale, _pixelGrayscale, _pixelGrayscale);

                    _result.SetPixel(i, j, _pixelAsColor);
                }
            }


            return _result;
        }
    }
}