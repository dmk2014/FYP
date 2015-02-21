using Microsoft.Kinect;
using System;
using System.Drawing;

namespace FacialRecognition.Library.Hardware.KinectV1
{
    public class KinectV1Sensor : IImagingHardware
    {
        private KinectSensor c_Sensor;

        public KinectV1Sensor(KinectSensor Sensor)
        {
            //May reconsider this
            this.c_Sensor = Sensor;
        }

        public Bitmap CaptureImage()
        {
            if(!c_Sensor.IsRunning)
            {
                c_Sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                c_Sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                c_Sensor.Start();
            }

            var _colorFrame = c_Sensor.ColorStream.OpenNextFrame(10);
            var _depthFrame = c_Sensor.DepthStream.OpenNextFrame(500);

            var _dataProcessor = new SensorDataProcessor();
            var _colorBitmap = _dataProcessor.ColorToBitmap(_colorFrame);
            var _depthBitmap = _dataProcessor.DepthToBitmap(_depthFrame);

            //TODO
            //Depth reduction

            return _colorBitmap;
        }

        private Bitmap RemoveBackground(ColorImageFrame Color, DepthImageFrame Depth)
        {
            throw new NotImplementedException();
        }
    }
}