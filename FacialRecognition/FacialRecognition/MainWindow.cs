﻿using FacialRecognition.Library.Database;
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
        private KinectSensor c_Sensor;
        private SensorDataProcessor c_DataProcessor;
        private IDatabase c_Database;

        public frmFacialRecPrototype()
        {
            InitializeComponent();
            this.CenterToScreen();

            c_Database = new CouchDatabase("localhost", 5984, "facial1");
            this.UpdateDatabaseDisplay();
            cboSelectCRUDMode.SelectedIndex = 0;
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
                    c_DataProcessor = new SensorDataProcessor();
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
        Bitmap c_SourceImage;
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
                    c_SourceImage = this.c_Kinect.CaptureImage();
                    pbxCapturedColorImage.Image = c_SourceImage;
                    btnFacialDetection.Enabled = true;
                }
            }
            catch(Exception _ex)
            {
                MessageBox.Show(_ex.Message + "\n\n" + _ex.StackTrace);
            }
        }

        private void btnFacialDetection_Click(object sender, EventArgs e)
        {
            var _detector = new FacialRecognition.Library.Core.FacialDetector();

            c_Faces = _detector.DetectFaces(c_SourceImage);

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

                lblDetectedFaces.Text = "Detected Faces: " + c_Faces.Length;
                btnNormalise.Enabled = true;
            }
        }

        private void btnNormalise_Click(object sender, EventArgs e)
        {
            try
            {
                var _normaliser = new FacialRecognition.Library.Octave.OctaveNormaliser();
                var _face = c_SourceImage.Clone(c_Faces[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                pbxSourceFace.Size = _face.Size;
                pbxSourceFace.Image = _face;

                var _result = _normaliser.NormaliseImage(_face, 168, 192);

                pbxNormalisedFace.Image = _result;
                btnPerformFacialRec.Enabled = true;
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }

        private void btnPerformFacialRec_Click(object sender, EventArgs e)
        {
            try
            {
                var _recogniser = new OctaveRecogniser(new OctaveInterface("localhost","6379"));

                var _face = new Bitmap(pbxNormalisedFace.Image);

                var _result = _recogniser.ClassifyFace(_face);

                MessageBox.Show("Result: " + _result.Id);
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }
        #endregion    

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            var _openFileResult = diaOpenFile.ShowDialog();

            if(_openFileResult == DialogResult.OK)
            {
                var _filePath = diaOpenFile.FileName;
                Image _image = Image.FromFile(_filePath);
                
                //Move this section to a library class
                //Method such as resize while constraining proportions
                if (_image.Height > pbxCapturedColorImage.Height || _image.Width > pbxCapturedColorImage.Width)
                {
                    var _normaliser = new FacialRecognition.Library.Octave.OctaveNormaliser();
                    var _contrastRatio = pbxCapturedColorImage.Width / pbxCapturedColorImage.Height;
                    var _sourceImageRatio = _image.Width / _image.Height;

                    if(_sourceImageRatio <= _contrastRatio)
                    {
                        _image = _normaliser.Resize(_image, pbxCapturedColorImage.Height * _contrastRatio, pbxCapturedColorImage.Height);
                    }
                    else
                    {
                        _image = _normaliser.Resize(_image, pbxCapturedColorImage.Width, pbxCapturedColorImage.Width / _contrastRatio);
                    }
                }

                pbxCapturedColorImage.Width = _image.Width;
                pbxCapturedColorImage.Height = _image.Height;
                c_SourceImage = (Bitmap)_image;
                pbxCapturedColorImage.Image = c_SourceImage;
                btnFacialDetection.Enabled = true;
            }
        }

        private void UpdateDatabaseDisplay()
        {
            var _people = c_Database.RetrieveAll();
            grdUsers.DataSource = _people;
        }

        private void grdUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (c_Editing)
            {
                this.DisplayPersonDetailsToEdit();
            }
        }

        private void DisplayPersonDetailsToEdit()
        {
            var _selection = grdUsers.SelectedRows;

            if (_selection.Count == 1 && c_Editing == true)
            {

                var _selectedID = _selection[0].Cells["colIdentifier"].Value.ToString();

                var _person = c_Database.Retrieve(_selectedID);

                txtPersonID.Text = _person.Id;
                txtPersonForename.Text = _person.Forename;
                txtPersonSurname.Text = _person.Surname;
            }
        }

        private Boolean c_Editing = false;

        private void cboSelectCRUDMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSelectCRUDMode.SelectedIndex == 0)
            {
                c_Editing = false;
                txtPersonID.Text = "auto-assigned";
                txtPersonForename.Text = String.Empty;
                txtPersonSurname.Text = String.Empty;
                btnSavePersonToDatabase.Text = "Save Person";
            }
            else if (cboSelectCRUDMode.SelectedIndex == 1)
            {
                c_Editing = true;
                this.DisplayPersonDetailsToEdit();
                btnSavePersonToDatabase.Text = "Update Person";
            }
        }

        private void btnSavePersonToDatabase_Click(object sender, EventArgs e)
        {
            //TODO - validation
            if (c_Editing)
            {
                var _personToUpdate = c_Database.Retrieve(txtPersonID.Text);

                _personToUpdate.Forename = txtPersonForename.Text;
                _personToUpdate.Surname = txtPersonSurname.Text;

                c_Database.Update(_personToUpdate);

                MessageBox.Show("Person Updated");
            }
            else if (!c_Editing)
            {

                var _person = new FacialRecognition.Library.Models.Person();
                _person.Forename = txtPersonForename.Text;
                _person.Surname = txtPersonSurname.Text;

                c_Database.Store(_person);

                MessageBox.Show("Person Stored");

                txtPersonForename.Text = String.Empty;
                txtPersonSurname.Text = String.Empty;
            }
            this.UpdateDatabaseDisplay();
        }
    }
}