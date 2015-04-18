using FacialRecognition.Globals;
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
                var pen = new Pen(Color.Green, 2);

                foreach (var rectangle in ApplicationGlobals.LocationOfDetectedFaces)
                {
                    graphics.DrawRectangle(pen, rectangle);
                }

                pictureBox.Image = sourceImage;
            }

            return pictureBox;
        }

        public Image ExtractFacialImage(Image sourceImage, Rectangle[] faceLocations)
        {
            var image = new Bitmap(sourceImage);

            var face = image.Clone(faceLocations[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            return face;
        }
    }
}