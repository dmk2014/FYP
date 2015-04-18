using FacialRecognition.Library.Octave;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace FacialRecognition.Test
{
    [TestClass]
    public class OctaveNormaliser_Test
    {
        PhotometricFacialImageNormaliser Normaliser;
        Bitmap TestImage;

        [TestInitialize]
        public void InitializeTest()
        {
            this.Normaliser = new PhotometricFacialImageNormaliser();
            this.TestImage = Properties.Resources.FacialImage; 
        }

        [TestMethod]
        public void TestNormaliseAnImageUsingTemplateMethod()
        {
            var normalisationWidth = 168;
            var normalisationHeight = 192;

            var result = this.Normaliser.NormaliseImage(this.TestImage, normalisationWidth, normalisationHeight);
            
            Assert.AreEqual(normalisationWidth, result.Width);
            Assert.AreEqual(normalisationHeight, result.Height);
        }

        [TestMethod]
        public void TestResizeAnImage()
        {
            var resizeWidth = 50;
            var resizeHeight = 50;

            var result = this.Normaliser.Resize(this.TestImage, resizeWidth, resizeHeight);

            Assert.AreEqual(resizeWidth, result.Width);
            Assert.AreEqual(resizeHeight, result.Height);
        }

        [TestMethod]
        public void TestSetColormapOfImage()
        {
            // No exceptions indicate success
            var result = this.Normaliser.SetColormap(this.TestImage);

            // Check a random pixel to ensure all components contain the same value
            var resultAsBitmap = new Bitmap(result);
            var colorOfPixel = resultAsBitmap.GetPixel(1, 1);

            Assert.AreEqual(colorOfPixel.G, colorOfPixel.R);
            Assert.AreEqual(colorOfPixel.R, colorOfPixel.B);
        }
    }
}