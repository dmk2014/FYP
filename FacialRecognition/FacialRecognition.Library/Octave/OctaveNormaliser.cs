using FacialRecognition.Library.Core;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace FacialRecognition.Library.Octave
{
    public class OctaveNormaliser : FacialImageNormaliser
    {
        /// <summary>
        /// Resizes an image using the specified parameters
        /// </summary>
        /// <param name="sourceImage">The image to be resized</param>
        /// <param name="width">The width to resize the image to</param>
        /// <param name="height">The height to resize the image to</param>
        /// <returns>The resized image</returns>
        public override Image Resize(Image sourceImage, int width, int height)
        {
            var resizedImage = new Bitmap(width, height);
            var graphics = Graphics.FromImage(resizedImage);

            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(sourceImage, 0, 0, width, height);
            graphics.Dispose();

            return resizedImage;
        }

        /// <summary>
        /// Sets the colormap of the specified image to grayscale
        /// </summary>
        /// <param name="source">The image to be processed</param>
        /// <returns>The processed image with a grayscale colormap</returns>
        public override Image SetColormap(Image source)
        {
            // Reference: http://tech.pro/tutorial/660/csharp-tutorial-convert-a-color-image-to-grayscale
            // Conversion formula: http://www.johndcook.com/blog/2009/08/24/algorithms-convert-color-grayscale/

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