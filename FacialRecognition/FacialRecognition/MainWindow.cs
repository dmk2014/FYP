using FacialRecognition.Library.Core;
using FacialRecognition.Library.Database;
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
        private IDatabase Database;
        private KinectV1Sensor Kinect;
        private FacialDetector Detector;

        public frmFacialRecPrototype()
        {
            InitializeComponent();
            this.CenterToScreen();

            Database = new CouchDatabase("localhost", 5984, "facial1");
            this.UpdateDatabaseDisplay();
            cboSelectCRUDMode.SelectedIndex = 0;

            this.PrepareKinectSensor();

            this.Detector = new FacialRecognition.Library.Core.FacialDetector();
        }

        #region CameraStreamsTab
        private void btnCaptureFrames_Click(object sender, EventArgs e)
        {
            try 
            {
                pbxImage.Image = Kinect.CaptureImage();
                pbxDept.Image = Kinect.CaptureDepthImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnIncrElevation_Click(object sender, EventArgs e)
        {
            try
            {
                Kinect.AdjustElevation(10);
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
                Kinect.AdjustElevation(-10);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnSaveFrameData_Click(object sender, EventArgs e)
        {
            try
            {
                Kinect.SaveFrameData();

                MessageBox.Show("Data saved successfully to your Desktop", "Facial Recognition", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region FacialRecognitionTab
        Bitmap SourceRecognitionImage;
        Rectangle[] LocationOfDetectedFaces;

        private void PrepareKinectSensor()
        {
            if (KinectSensor.KinectSensors.Count > 0)
            {
                Kinect = new KinectV1Sensor(KinectSensor.KinectSensors[0]);
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
                if (Kinect != null)
                {
                    SourceRecognitionImage = this.Kinect.CaptureImage();
                    pbxCapturedColorImage.Image = SourceRecognitionImage;
                    btnFacialDetection.Enabled = true;
                }
            }
            catch(Exception _ex)
            {
                MessageBox.Show(_ex.Message + "\n\n" + _ex.StackTrace);
            }
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            var _openFileResult = diaOpenFile.ShowDialog();

            if (_openFileResult == DialogResult.OK)
            {
                var _filePath = diaOpenFile.FileName;
                Image _image = Image.FromFile(_filePath);

                //Move this section to a library class
                //Method such as resize while constraining proportions
                //if (_image.Height > pbxCapturedColorImage.Height || _image.Width > pbxCapturedColorImage.Width)
                //{
                //    var _normaliser = new FacialRecognition.Library.Octave.OctaveNormaliser();
                //    var _contrastRatio = pbxCapturedColorImage.Width / pbxCapturedColorImage.Height;
                //    var _sourceImageRatio = _image.Width / _image.Height;

                //    if (_sourceImageRatio <= _contrastRatio)
                //    {
                //        _image = _normaliser.Resize(_image, pbxCapturedColorImage.Height * _contrastRatio, pbxCapturedColorImage.Height);
                //    }
                //    else
                //    {
                //        _image = _normaliser.Resize(_image, pbxCapturedColorImage.Width, pbxCapturedColorImage.Width / _contrastRatio);
                //    }
                //}

                //pbxCapturedColorImage.Width = _image.Width;
                //pbxCapturedColorImage.Height = _image.Height;

                SourceRecognitionImage = new Bitmap(_image);
                pbxCapturedColorImage.Image = SourceRecognitionImage;
                btnFacialDetection.Enabled = true;
            }
        }

        private void btnFacialDetection_Click(object sender, EventArgs e)
        {
            try 
            {
                LocationOfDetectedFaces = Detector.DetectFaces(SourceRecognitionImage);

                if (LocationOfDetectedFaces.Length < 1)
                {
                    MessageBox.Show("No faces detected");
                }
                else
                {
                    var g = pbxCapturedColorImage.CreateGraphics();
                    var _pen = new Pen(Color.Green, 3);
                    var _i = 0;

                    while (_i < LocationOfDetectedFaces.Length)
                    {
                        g.DrawRectangle(_pen, LocationOfDetectedFaces[_i]);
                        _i++;
                    }

                    lblDetectedFaces.Text = "Detected Faces: " + LocationOfDetectedFaces.Length;
                    btnNormalise.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "FacialDetection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnNormalise_Click(object sender, EventArgs e)
        {
            try
            {
                var _normaliser = new FacialRecognition.Library.Octave.OctaveNormaliser();
                var _face = SourceRecognitionImage.Clone(LocationOfDetectedFaces[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb);

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
                var _recogniser = new OctaveRecogniser(new OctaveInterface("localhost", 6379));

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

        private void UpdateDatabaseDisplay()
        {
            var _people = Database.RetrieveAll();
            grdUsers.DataSource = _people;
        }

        private void grdUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (this.EditingMode == DatabaseEditingMode.UPDATING_EXISTING_USER)
            {
                this.DisplayAPersonsDetailsForEditing();
            }
        }

        private void DisplayAPersonsDetailsForEditing()
        {
            var _selection = grdUsers.SelectedRows;

            if (_selection.Count == 1 && this.EditingMode == DatabaseEditingMode.UPDATING_EXISTING_USER)
            {
                var _selectedID = _selection[0].Cells["colIdentifier"].Value.ToString();

                var _person = Database.Retrieve(_selectedID);
                DatabaseUIGlobals.DisplayedUser = _person;
                pbxPersonFacialImages.Image = null;

                if (DatabaseUIGlobals.DisplayedUser.Images.Count > 0)
                {
                    pbxPersonFacialImages.Image = DatabaseUIGlobals.DisplayedUser.Images[0];
                    DatabaseUIGlobals.DisplayedImageIndex = 0;
                }

                txtPersonID.Text = _person.Id;
                txtPersonForename.Text = _person.Forename;
                txtPersonSurname.Text = _person.Surname;
            }
        }

        private DatabaseEditingMode EditingMode = DatabaseEditingMode.ADDING_NEW_USER;

        private void cboSelectCRUDMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSelectCRUDMode.SelectedIndex == 0)
            {
                this.EditingMode = DatabaseEditingMode.ADDING_NEW_USER;
                txtPersonID.Text = "auto-assigned";
                txtPersonForename.Text = String.Empty;
                txtPersonSurname.Text = String.Empty;
                btnSavePersonToDatabase.Text = "Save Person";
                pbxPersonFacialImages.Image = null;

                //TODO
                //Extract this code
                DatabaseUIGlobals.DisplayedUser = new FacialRecognition.Library.Models.Person();               
                DatabaseUIGlobals.DisplayedImageIndex = 0;

                txtPersonForename.Focus();
            }
            else if (cboSelectCRUDMode.SelectedIndex == 1)
            {
                this.EditingMode = DatabaseEditingMode.UPDATING_EXISTING_USER;
                this.DisplayAPersonsDetailsForEditing();
                btnSavePersonToDatabase.Text = "Update Person";
                grdUsers.Focus();
            }
        }

        private void btnSavePersonToDatabase_Click(object sender, EventArgs e)
        {
            // TODO - validation
            if (this.EditingMode == DatabaseEditingMode.UPDATING_EXISTING_USER)
            {
                var _personToUpdate = Database.Retrieve(txtPersonID.Text);

                _personToUpdate.Forename = txtPersonForename.Text;
                _personToUpdate.Surname = txtPersonSurname.Text;
                _personToUpdate.Images = DatabaseUIGlobals.DisplayedUser.Images;

                Database.Update(_personToUpdate);

                MessageBox.Show("Person Updated");
            }
            else if (this.EditingMode == DatabaseEditingMode.ADDING_NEW_USER)
            {
                var _person = new FacialRecognition.Library.Models.Person();
                _person.Forename = txtPersonForename.Text;
                _person.Surname = txtPersonSurname.Text;
                _person.Images = DatabaseUIGlobals.DisplayedUser.Images;

                Database.Store(_person);

                MessageBox.Show("Person Stored");

                txtPersonForename.Text = String.Empty;
                txtPersonSurname.Text = String.Empty;
                pbxPersonFacialImages.Image = null;
            }
            this.UpdateDatabaseDisplay();
        }

        private void btnUserCaptureImage_Click(object sender, EventArgs e)
        {
            if (Kinect != null)
            {
                pbxUserImage.Image = Kinect.CaptureImage();
            }
        }

        private void btnUserDetectFacialImage_Click(object sender, EventArgs e)
        {
            LocationOfDetectedFaces = Detector.DetectFaces((Bitmap)pbxUserImage.Image);

            if (LocationOfDetectedFaces.Length < 1)
            {
                MessageBox.Show("No faces detected");
            }
            else
            {
                var g = pbxUserImage.CreateGraphics();
                var _pen = new Pen(Color.Green, 3);
                var _i = 0;

                while (_i < LocationOfDetectedFaces.Length)
                {
                    g.DrawRectangle(_pen, LocationOfDetectedFaces[_i]);
                    _i++;
                }
            }
        }

        private void btnUserAddFace_Click(object sender, EventArgs e)
        {
            try
            {
                var normaliser = new FacialRecognition.Library.Octave.OctaveNormaliser();
                var capturedImage = new Bitmap(pbxUserImage.Image);
                var face = capturedImage.Clone(LocationOfDetectedFaces[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                var normalisedImage = normaliser.NormaliseImage(face, 168, 192);
                
                // Add to user images
                DatabaseUIGlobals.DisplayedUser.Images.Add(normalisedImage);
                pbxPersonFacialImages.Image = DatabaseUIGlobals.DisplayedUser.Images[DatabaseUIGlobals.DisplayedUser.Images.Count - 1];
                DatabaseUIGlobals.DisplayedImageIndex = DatabaseUIGlobals.DisplayedUser.Images.Count - 1;
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }

        // TODO
        // Extract these two methods to one method
        private void btnPersonImagesForward_Click(object sender, EventArgs e)
        {
            if (DatabaseUIGlobals.DisplayedUser.Images.Count > 0)
            {
                var _imageCount = DatabaseUIGlobals.DisplayedUser.Images.Count;
                var _displayedIndex = DatabaseUIGlobals.DisplayedImageIndex;

                if (_displayedIndex + 1 < _imageCount)
                {
                    pbxPersonFacialImages.Image = DatabaseUIGlobals.DisplayedUser.Images[_displayedIndex + 1];
                    DatabaseUIGlobals.DisplayedImageIndex++;
                }
            }
        }

        private void btnPersonImagesBack_Click(object sender, EventArgs e)
        {
            if (DatabaseUIGlobals.DisplayedUser.Images.Count > 0)
            {
                var _imageCount = DatabaseUIGlobals.DisplayedUser.Images.Count;
                var _displayedIndex = DatabaseUIGlobals.DisplayedImageIndex;

                if (_displayedIndex - 1 >= 0)
                {
                    pbxPersonFacialImages.Image = DatabaseUIGlobals.DisplayedUser.Images[_displayedIndex - 1];
                    DatabaseUIGlobals.DisplayedImageIndex--;
                }
            }
        }
    }
}