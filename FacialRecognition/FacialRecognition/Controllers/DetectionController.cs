using FacialRecognition.Globals;
using FacialRecognition.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FacialRecognition.Controllers
{
    public class DetectionController
    {
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