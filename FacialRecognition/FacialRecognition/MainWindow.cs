using FacialRecognition.Library.Hardware.KinectV1;
using FacialRecognition.Library.Octave;
using Microsoft.Kinect;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FacialRecognition
{
    public partial class frmFacialRecPrototype : Form
    {
        KinectSensor c_Sensor;
        SensorDataProcessor c_DataProcessor;

        public frmFacialRecPrototype()
        {
            InitializeComponent();
            this.CenterToScreen();

            c_DataProcessor = new SensorDataProcessor();
        }

        private void frmTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (c_Sensor.IsRunning)
                c_Sensor.Stop();
        }

        #region CameraStreamsTab
        private void btnStartSensor_Click(object sender, EventArgs e)
        {
            PrototypeStartup();         
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (c_Sensor.IsRunning)
            {
                c_Sensor.Stop();
                btnStartSensor.Enabled = true;
                btnStop.Enabled = false;
            }
        }

        private void PrototypeStartup()
        {
            try
            {
                if (KinectSensor.KinectSensors.Count == 0)
                {
                    MessageBox.Show("Unable to initialise a Kinect sensor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    c_Sensor = KinectSensor.KinectSensors[0];

                    c_Sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                    c_Sensor.ColorFrameReady += runtime_VideoFrameReady;

                    c_Sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    c_Sensor.DepthFrameReady += runtime_DeptFrameReady;

                    c_Sensor.Start();

                    btnStartSensor.Enabled = false;
                    btnIncrElevation.Enabled = true;
                    btnDecrElevation.Enabled = true;
                    btnSaveFrameData.Enabled = true;
                    btnStop.Enabled = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void runtime_DeptFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            var _frame = e.OpenDepthImageFrame();

            if (_frame != null)
            {
                pbxDept.Image = c_DataProcessor.DepthToBitmap(_frame);
            }
        }

        void runtime_VideoFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            var _frame = e.OpenColorImageFrame();

            if (_frame != null)
            {
                pbxImage.Image = c_DataProcessor.ColorToBitmap(_frame);   
            }
        }

        private void btnIncrElevation_Click(object sender, EventArgs e)
        {
            try
            {
                var _max = c_Sensor.MaxElevationAngle;

                if (c_Sensor.ElevationAngle + 15 <= _max)
                    c_Sensor.ElevationAngle = c_Sensor.ElevationAngle + 5; ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnDecrElevation_Click(object sender, EventArgs e)
        {
            try
            {
                var _min = c_Sensor.MinElevationAngle;

                if (c_Sensor.ElevationAngle - 15 >= _min)
                    c_Sensor.ElevationAngle = c_Sensor.ElevationAngle - 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnSaveFrameData_Click(object sender, EventArgs e)
        {
            var _io = new SensorDataIO();

            //Dispose the running sensor instance
            //Create new instance and then poll for frames
            c_Sensor.Dispose();

            c_Sensor = KinectSensor.KinectSensors[0];
            c_Sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            c_Sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            c_Sensor.Start();

            var _colourFrame = c_Sensor.ColorStream.OpenNextFrame(1000);
            var _depthFrame = c_Sensor.DepthStream.OpenNextFrame(1000);

            _io.SaveRawPixelDataColour(_colourFrame);
            _io.SaveRawPixelDataDepth(_depthFrame);

            //Dispose the running sensor used only by this method
            //Restart the prototype
            c_Sensor.Dispose();
            PrototypeStartup();
        }
        #endregion

        #region FacialRecognitionTab
        KinectV1Sensor c_Kinect;
        Bitmap c_capturedFrame;
        Rectangle[] c_Faces;

        private void PrepareKinectSensor()
        {

            if (KinectSensor.KinectSensors.Count > 0)
            {
                c_Kinect = new KinectV1Sensor(KinectSensor.KinectSensors[0]);
            }
            else
            {
                MessageBox.Show("Could find a Kinect sensor attached to this system","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnCaptureFrame_Click(object sender, EventArgs e)
        {
            try
            {
                this.PrepareKinectSensor();

                if (c_Kinect != null)
                {
                    c_capturedFrame = this.c_Kinect.CaptureImage();
                    pbxCapturedColorImage.Image = c_capturedFrame;
                    btnFacialDetection.Enabled = true;
                    btnPerformFacialRec.Enabled = true;
                }
            }
            catch(Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }

        private void btnFacialDetection_Click(object sender, EventArgs e)
        {
            var _detector = new FacialRecognition.Library.Core.FacialDetector();

            c_Faces = _detector.DetectFaces(c_capturedFrame);

            if (c_Faces.Length < 1)
            {
                MessageBox.Show("No faces detected");
            }
            else
            {
                var g = pbxCapturedColorImage.CreateGraphics();
                var _pen = new Pen(Color.Green, 3);
                var _i = 0;

                while (_i < c_Faces.Length)
                {
                    g.DrawRectangle(_pen,c_Faces[_i]);
                    _i++;
                }
            }
        }

        private void btnPerformFacialRec_Click(object sender, EventArgs e)
        {
            try
            {
                var _recogniser = new OctaveRecogniser(new OctaveInterface(new ServiceStack.Redis.RedisClient()));

                var _face = c_capturedFrame.Clone(c_Faces[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb);//normalise this face

                var _result = _recogniser.ClassifyFace(_face);

                MessageBox.Show("Result: " + _result._id);
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }
        #endregion
    }
}