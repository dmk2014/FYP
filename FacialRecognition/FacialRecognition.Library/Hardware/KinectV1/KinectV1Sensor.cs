using Microsoft.Kinect;
using System;
using System.Drawing;

namespace FacialRecognition.Library.Hardware.KinectV1
{
    public class KinectV1Sensor : IImagingHardware
    {
        private KinectSensor Sensor;
        private const int FrameWaitTimeout = 700;

        public KinectV1Sensor(KinectSensor sensor)
        {
            this.Sensor = sensor;
            this.ConfigureSensor();
        }

        private void ConfigureSensor()
        {
            this.Sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            this.Sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            this.Sensor.Start();
        }

        public void AdjustElevation(int angleChange)
        {
            if (this.Sensor.ElevationAngle + angleChange >= this.Sensor.MinElevationAngle &&
                this.Sensor.ElevationAngle + angleChange <= this.Sensor.MaxElevationAngle)
            {
                this.Sensor.ElevationAngle = this.Sensor.ElevationAngle + angleChange;
            }
        }

        public Bitmap CaptureImage()
        {
            if(!this.Sensor.IsRunning)
            {
                this.ConfigureSensor();
            }

            var colorImage = this.CaptureColorImage();
            var depthImage = this.CaptureDepthImage();

            // TODO
            // Depth reduction

            return colorImage;
        }

        public Bitmap CaptureColorImage()
        {
            if (!this.Sensor.IsRunning)
            {
                this.ConfigureSensor();
            }

            var colorFrame = Sensor.ColorStream.OpenNextFrame(FrameWaitTimeout);
            var dataProcessor = new SensorDataProcessor();
            var colorBitmap = dataProcessor.ColorToBitmap(colorFrame);

            return colorBitmap;
        }

        public Bitmap CaptureDepthImage()
        {
            if (!Sensor.IsRunning)
            {
                this.ConfigureSensor();
            }

            var depthFrame = Sensor.DepthStream.OpenNextFrame(FrameWaitTimeout);
            var dataProcessor = new SensorDataProcessor();
            var depthBitmap = dataProcessor.DepthToBitmap(depthFrame);

            return depthBitmap;
        }

        private Bitmap RemoveBackground(ColorImageFrame colorImage, DepthImageFrame depthImage)
        {
            throw new NotImplementedException();
        }

        public void SaveFrameData()
        {
            if (!this.Sensor.IsRunning)
            {
                this.ConfigureSensor();
            }

            var sensorDataIO = new SensorDataIO();
            var colourFrame = this.Sensor.ColorStream.OpenNextFrame(FrameWaitTimeout);
            var depthFrame = this.Sensor.DepthStream.OpenNextFrame(FrameWaitTimeout);

            sensorDataIO.SaveRawPixelDataColour(colourFrame);
            sensorDataIO.SaveRawPixelDataDepth(depthFrame);
        }
    }
}