using FacialRecognition.Library.Octave;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FacialRecognition.Test
{
    [TestClass]
    public class OctaveNormaliser_Test
    {
        [TestMethod]
        public void TestNormaliseAnImage()
        {
            var normaliser = new OctaveNormaliser();
            var sourceImage = Properties.Resources.FacialImage; 
            var normalisationWidth = 168;
            var normalisationHeight = 192;

            var result = normaliser.NormaliseImage(sourceImage, normalisationWidth, normalisationHeight);

            Assert.AreEqual(normalisationWidth, result.Width);
            Assert.AreEqual(normalisationHeight, result.Height);
        }
    }
}