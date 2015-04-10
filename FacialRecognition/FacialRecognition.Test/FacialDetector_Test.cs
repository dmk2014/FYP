using FacialRecognition.Library.Core;
using FacialRecognition.Test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System;

namespace FacialRecognition.Test
{
    [TestClass]
    public class FacialDetector_Test
    {
        [TestMethod]
        public void TestFacialDetection()
        {
            // Successful facial detection invokes the detector and receives a non-null result
            // of type System.Drawing.Rectangle[]

            try
            {
                var detector = new FacialDetector();
                var sourceImage = Resources.FacialImage;

                var result = detector.DetectFaces(sourceImage);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Rectangle[]));
            }
            catch(Exception e)
            {
                Assert.Fail("TestFacialDetection failed with an exception:\n\n" + e.Message);
            }
        }
    }
}