using FacialRecognition.Library.Core;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace FacialRecognition.Library.Octave
{
    public class OctaveNormaliser : FacialImageNormaliser
    {
        public override Image Resize(Image source, int width, int height)
        {
            var resizedImage = new Bitmap(width, height);
            var graphics = Graphics.FromImage(resizedImage);

            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(source, 0, 0, width, height);
            graphics.Dispose();

            return resizedImage;
        }

        public override Image SetColormap(Image source)
        {
            //Reference: http://tech.pro/tutorial/660/csharp-tutorial-convert-a-color-image-to-grayscale
            //Conversion formula: http://www.johndcook.com/blog/2009/08/24/algorithms-convert-color-grayscale/

            var originalImage = new Bitmap(source);
            var result = new Bitmap(source.Width, source.Height);

            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    var pixel = originalImage.GetPixel(i, j);
                    var grayscaleValueOfPixel = Convert.ToInt32((0.21 * (int)pixel.R) + (0.72 * (int)pixel.G) + (0.07 * (int)pixel.B));
                    var pixelGrayscale = Color.FromArgb(grayscaleValueOfPixel, grayscaleValueOfPixel, grayscaleValueOfPixel);

                    result.SetPixel(i, j, pixelGrayscale);
                }
            }

            return result;
        }
    }
}