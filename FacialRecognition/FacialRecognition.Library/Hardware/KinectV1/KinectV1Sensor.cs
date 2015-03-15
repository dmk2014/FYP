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

            this.ConfigureSensor();
        }

        private void ConfigureSensor()
        {
            c_Sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            c_Sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            c_Sensor.Start();
        }

        public void AdjustElevation(int Angle)
        {
            if (this.c_Sensor.ElevationAngle + Angle >= this.c_Sensor.MinElevationAngle &&
                this.c_Sensor.ElevationAngle + Angle <= this.c_Sensor.MaxElevationAngle)
            {
                this.c_Sensor.ElevationAngle = this.c_Sensor.ElevationAngle + Angle;
            }
        }

        public Bitmap CaptureImage()
        {
            if(!c_Sensor.IsRunning)
            {
                this.ConfigureSensor();
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

        public Bitmap CaptureDepthImage()
        {
            if (!c_Sensor.IsRunning)
            {
                this.ConfigureSensor();
            }

            var _depthFrame = c_Sensor.DepthStream.OpenNextFrame(500);

            var _dataProcessor = new SensorDataProcessor();
            var _depthBitmap = _dataProcessor.DepthToBitmap(_depthFrame);

            return _depthBitmap;
        }

        private Bitmap RemoveBackground(ColorImageFrame Color, DepthImageFrame Depth)
        {
            throw new NotImplementedException();
        }

        public void SaveFrameData()
        {
            if (!c_Sensor.IsRunning)
            {
                this.ConfigureSensor();
            }

            var _io = new SensorDataIO();

            var _colourFrame = c_Sensor.ColorStream.OpenNextFrame(1000);
            var _depthFrame = c_Sensor.DepthStream.OpenNextFrame(1000);

            _io.SaveRawPixelDataColour(_colourFrame);
            _io.SaveRawPixelDataDepth(_depthFrame);
        }
    }
}