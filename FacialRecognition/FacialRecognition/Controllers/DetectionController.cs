using FacialRecognition.Globals;
using FacialRecognition.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FacialRecognition.Controllers
{
    public class DetectionController
    {
        /// <summary>
        /// Find detected faces in the provided image and draws them onto a PictureBox component.
        /// </summary>
        /// <param name="sourceImage">The image from which to detect and draw faces.</param>
        /// <returns>A PictureBox containing the source image with drawn faces.</returns>
        public PictureBox FindAndDrawDetectedFaces(Bitmap sourceImage)
        {
            var pictureBox = new PictureBox();
            pictureBox.Image = sourceImage;

            ApplicationGlobals.LocationOfDetectedFaces = ApplicationGlobals.Detector.DetectFaces(sourceImage);

            if (ApplicationGlobals.LocationOfDetectedFaces.Length > 0)
            {
                var graphics = Graphics.FromImage(sourceImage);
                var pen = new Pen(Color.Green, 3);

                foreach (var rectangle in ApplicationGlobals.LocationOfDetectedFaces)
                {
                    graphics.DrawRectangle(pen, rectangle);
                }

                pictureBox.Image = sourceImage;
            }
            else
            {
                Messages.DisplayInformationMessage(null, "No faces were detected within the specified image");
            }

            return pictureBox;
        }

        /// <summary>
        /// Extracts the largest face from the provided source image.
        /// </summary>
        /// <param name="sourceImage">The image from which to extract faces.</param>
        /// <param name="faceLocations">An array of rectangles containing the locations of faces in the source image.</param>
        /// <returns>A facial image.</returns>
        public Image ExtractFacialImage(Image sourceImage, Rectangle[] faceLocations)
        {
            if (faceLocations.Length > 0)
            {
                var faceIndexToExtract = this.GetIndexOfLargestFace(faceLocations);
                var image = new Bitmap(sourceImage);

                var face = image.Clone(faceLocations[faceIndexToExtract], System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                return face;
            }
            else
            {
                throw new Exception("No facial images could be extracted - ensure that a face has been captured");
            }
        }

        private int GetIndexOfLargestFace(Rectangle[] faceLocations)
        {
            int indexLargestFace = 0;
            int areaLargestFace = 0;

            for (int i = 0; i < faceLocations.Length; i++ )
            {
                int width = faceLocations[i].Width;
                int height = faceLocations[i].Height;
                int areaFace = width * height;

                if (areaFace > areaLargestFace)
                {
                    indexLargestFace = i;
                    areaLargestFace = areaFace;
                }
            }

            return indexLargestFace;
        }
    }
}