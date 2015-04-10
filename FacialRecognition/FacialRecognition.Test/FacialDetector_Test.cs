using FacialRecognition.Library.Core;
using FacialRecognition.Test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System;
using System.IO;

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

        [TestMethod]
        public void TestConstructFacialDetectorWithClassifier()
        {
            var pathToClassifier = @"C:\Emgu\emgucv-windows-universal-cuda 2.4.10.1940\bin\haarcascade_frontalface_default.xml";
            var detector = new FacialDetector(pathToClassifier);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestConstructFacialDetectorWithInvalidClassifier()
        {
            var pathToClassifier = @"C:\aninvalidclassifierfile.xml";
            var detector = new FacialDetector(pathToClassifier);
        }
    }
}