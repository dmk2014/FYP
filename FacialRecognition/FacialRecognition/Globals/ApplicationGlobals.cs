using FacialRecognition.Library.Database;
using FacialRecognition.Library.Detection;
using FacialRecognition.Library.Hardware.KinectV1;
using FacialRecognition.Library.Recognition;
using System.Drawing;

namespace FacialRecognition.Globals
{
    public class ApplicationGlobals
    {
        public static IDatabase Database { get; set; }
        public static KinectV1Sensor Kinect { get; set; }
        public static IFacialDetector Detector { get; set; }
        public static IFacialRecogniser Recogniser { get; set; }
        public static Rectangle[] LocationOfDetectedFaces { get; set; }
    }
}