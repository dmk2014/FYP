using Microsoft.Kinect;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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
            this.Sensor.DepthStream.Range = DepthRange.Default;
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
            return this.RemoveBackground(Sensor.ColorStream.OpenNextFrame(FrameWaitTimeout), Sensor.DepthStream.OpenNextFrame(FrameWaitTimeout));

            //return colorImage;
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
            // Reference used for Kinect API functions: https://msdn.microsoft.com/en-us/library/jj131029.aspx

            // Get the depth data
            DepthImagePixel[] depthData = new DepthImagePixel[depthImage.PixelDataLength];
            depthImage.CopyDepthImagePixelDataTo(depthData);
            
            // Get the color data
            byte[] colorImageData = new byte[colorImage.PixelDataLength];
            colorImage.CopyPixelDataTo(colorImageData);

            // The color data is four times the size of the depth data
            // Color data has four values - R, G, B, A

            int maxDepth = this.Sensor.DepthStream.MaxDepth;
            
            // Loop through the depth values
            for (var i = 0; i < depthData.Length;i++ )
            {
                // Check if the depth value is outside the specified range
                if (depthData[i].Depth > maxDepth)
                {
                    // Modify the color frame - set the color to black
                    // Multiply the index by 4 to get the correct start point in the color data array
                    // Remember that color data has 4 times as many values as depth data

                    int startIndexRGBA = i * 4;
                    int rIndex = startIndexRGBA;
                    int gIndex = startIndexRGBA + 1;
                    int bIndex = startIndexRGBA + 2;
                    int aIndex = startIndexRGBA + 3;

                    colorImageData[rIndex] = 0;
                    colorImageData[gIndex] = 0;
                    colorImageData[bIndex] = 0;
                    colorImageData[aIndex] = 0;
                }
            }

            // Convert the processed color byte array to a Bitmap and return it
            var image = new Bitmap(colorImage.Width, colorImage.Height, PixelFormat.Format32bppRgb);
            var imageRectangle = new Rectangle(0, 0, colorImage.Width, colorImage.Height);
            var bitmapData = image.LockBits(imageRectangle, ImageLockMode.WriteOnly, image.PixelFormat);
            var addressFirstPixel = bitmapData.Scan0;

            Marshal.Copy(colorImageData, 0, addressFirstPixel, colorImage.PixelDataLength);
            image.UnlockBits(bitmapData);

            return image;
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