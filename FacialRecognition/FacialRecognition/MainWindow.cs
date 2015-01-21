﻿using Microsoft.Kinect;
using System;
using System.Windows.Forms;

namespace FacialRecognition
{
    public partial class frmFacialRecPrototype : Form
    {
        KinectSensor Sensor;
        bool SensorActive;
        SensorDataProcessor DataProcessor;

        public frmFacialRecPrototype()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void btnStartSensor_Click(object sender, EventArgs e)
        {
            PrototypeStartup();         
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (Sensor.IsRunning)
            {
                Sensor.Stop();
                SensorActive = false;
                btnStartSensor.Enabled = true;
                btnStop.Enabled = false;
            }
        }

        public void PrototypeStartup()
        {
            try
            {
                if (KinectSensor.KinectSensors.Count == 0)
                {
                    MessageBox.Show("Unable to initialise a Kinect sensor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    DataProcessor = new SensorDataProcessor();
                    Sensor = KinectSensor.KinectSensors[0];

                    Sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                    Sensor.ColorFrameReady += runtime_VideoFrameReady;

                    Sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    Sensor.DepthFrameReady += runtime_DeptFrameReady;

                    Sensor.Start();

                    SensorActive = true;
                    btnStartSensor.Enabled = false;
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
                pbxDept.Image = DataProcessor.DepthToBitmap(_frame);
            }
        }

        void runtime_VideoFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            var _frame = e.OpenColorImageFrame();

            if (_frame != null)
            {
                pbxImage.Image = DataProcessor.ColorToBitmap(_frame);   
            }
        }

        private void frmTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Sensor.IsRunning)
                Sensor.Stop();
        }

        private void btnIncrElevation_Click(object sender, EventArgs e)
        {
            try
            {
                var _max = Sensor.MaxElevationAngle;

                if (Sensor.ElevationAngle + 15 <= _max)
                    Sensor.ElevationAngle = Sensor.ElevationAngle + 5; ;
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
                var _min = Sensor.MinElevationAngle;

                if (Sensor.ElevationAngle - 15 >= _min)
                    Sensor.ElevationAngle = Sensor.ElevationAngle - 5;
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
            Sensor.Dispose();

            Sensor = KinectSensor.KinectSensors[0];
            Sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            Sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            Sensor.Start();

            var _colourFrame = Sensor.ColorStream.OpenNextFrame(1000);
            var _depthFrame = Sensor.DepthStream.OpenNextFrame(1000);

            _io.SaveRawPixelDataColour(_colourFrame);
            _io.SaveRawPixelDataDepth(_depthFrame);

            //Dispose the running sensor used only by this method
            //Restart the prototype
            Sensor.Dispose();
            PrototypeStartup();
        }
    }
}