using Microsoft.Kinect;
using System.Drawing;

namespace FacialRecognition.Library.Hardware.KinectV1
{
    public class KinectV1Sensor : IImagingHardware
    {
        private KinectSensor Sensor;
        private SensorDataProcessor DataProcessor;
        private const int FrameWaitTimeout = 700;

        /// <summary>
        /// Constructs a KinectV1Sensor that uses the specified KinectSensor for data capture.
        /// </summary>
        /// <param name="sensor">The KinectSensor to be used.</param>
        public KinectV1Sensor(KinectSensor sensor)
        {
            this.Sensor = sensor;
            this.ConfigureSensor();
            this.DataProcessor = new SensorDataProcessor();
        }

        private void ConfigureSensor()
        {
            this.Sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            this.Sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            this.Sensor.DepthStream.Range = DepthRange.Default;
            this.Sensor.Start();
        }

        /// <summary>
        /// Increase or decrease the elevation angle of the sensor. Boundary is +-27 degrees.
        /// </summary>
        /// <param name="angleChange">The angle by which to adjust elevation.</param>
        public void AdjustElevation(int angleChange)
        {
            if (this.Sensor.ElevationAngle + angleChange >= this.Sensor.MinElevationAngle &&
                this.Sensor.ElevationAngle + angleChange <= this.Sensor.MaxElevationAngle)
            {
                this.Sensor.ElevationAngle = this.Sensor.ElevationAngle + angleChange;
            }
        }

        /// <summary>
        /// Capture an image using the Kinect sensor.
        /// </summary>
        /// <returns>A Bitmap image.</returns>
        public Bitmap CaptureImage()
        {
            return this.CaptureImage(false);
        }

        /// <summary>
        /// Capture an image using the Kinect sensor, specifying whether depth reduction should be performed.
        /// </summary>
        /// <param name="useDepthReduction">Indicates if depth reduction should be used.</param>
        /// <returns>A Bitmap image.</returns>
        public Bitmap CaptureImage(bool useDepthReduction)
        {
            if(!this.Sensor.IsRunning)
            {
                this.ConfigureSensor();
            }

            if(useDepthReduction)
            {
                return this.CaptureImageUsingDepthReduction();
            }
            else
            {
                return this.CaptureColorImage();
            }
        }

        /// <summary>
        /// Capture a color image using the Kinect sensor.
        /// </summary>
        /// <returns>A Bitmap image.</returns>
        public Bitmap CaptureColorImage()
        {
            if (!this.Sensor.IsRunning)
            {
                this.ConfigureSensor();
            }

            var colorFrame = this.Sensor.ColorStream.OpenNextFrame(FrameWaitTimeout);
            var colorBitmap = this.DataProcessor.ColorToBitmap(colorFrame);

            return colorBitmap;
        }

        /// <summary>
        /// Capture a depth image using the Kinect sensor.
        /// </summary>
        /// <returns>A Bitmap image.</returns>
        public Bitmap CaptureDepthImage()
        {
            if (!this.Sensor.IsRunning)
            {
                this.ConfigureSensor();
            }

            var depthFrame = this.Sensor.DepthStream.OpenNextFrame(FrameWaitTimeout);
            var depthBitmap = this.DataProcessor.DepthToBitmap(depthFrame);

            return depthBitmap;
        }

        private Bitmap CaptureImageUsingDepthReduction()
        {
            // Capture raw frames
            var colorFrame = this.Sensor.ColorStream.OpenNextFrame(FrameWaitTimeout);
            var depthFrame = this.Sensor.DepthStream.OpenNextFrame(FrameWaitTimeout);
            
            // Invoke depth reduction method
            var maxDepth = this.Sensor.DepthStream.MaxDepth;
            var reducedImage = this.DataProcessor.ReduceColorImageUsingDepthData(colorFrame, depthFrame, maxDepth);

            return reducedImage;
        }

        /// <summary>
        /// Capture and save the raw pixel data of both a color and depth image. Data saved to the Desktop as .CSVs.
        /// </summary>
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