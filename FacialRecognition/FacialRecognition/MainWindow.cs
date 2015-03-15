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
        private IDatabase c_Database;
        private KinectV1Sensor c_Kinect;

        public frmFacialRecPrototype()
        {
            InitializeComponent();
            this.CenterToScreen();

            c_Database = new CouchDatabase("localhost", 5984, "facial1");
            this.UpdateDatabaseDisplay();
            cboSelectCRUDMode.SelectedIndex = 0;

            this.PrepareKinectSensor();
        }

        #region CameraStreamsTab
        private void btnCaptureFrames_Click(object sender, EventArgs e)
        {
            try 
            {
                pbxImage.Image = c_Kinect.CaptureImage();
                pbxDept.Image = c_Kinect.CaptureDepthImage();
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
                c_Kinect.AdjustElevation(10);
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
                c_Kinect.AdjustElevation(-10);
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
                c_Kinect.SaveFrameData();

                MessageBox.Show("Data saved successfully to your Desktop", "Facial Recognition", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region FacialRecognitionTab
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

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            var _openFileResult = diaOpenFile.ShowDialog();

            if (_openFileResult == DialogResult.OK)
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

                    if (_sourceImageRatio <= _contrastRatio)
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

        private void UpdateDatabaseDisplay()
        {
            var _people = c_Database.RetrieveAll();
            grdUsers.DataSource = _people;
        }

        private void grdUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (c_Editing == DatabaseEditingMode.UPDATING_EXISTING_USER)
            {
                this.DisplayPersonDetailsToEdit();
            }
        }

        private void DisplayPersonDetailsToEdit()
        {
            var _selection = grdUsers.SelectedRows;

            if (_selection.Count == 1 && c_Editing == DatabaseEditingMode.UPDATING_EXISTING_USER)
            {
                var _selectedID = _selection[0].Cells["colIdentifier"].Value.ToString();

                var _person = c_Database.Retrieve(_selectedID);
                DatabaseUIGlobals.DISPLAYED_USER = _person;
                pbxPersonFacialImages.Image = null;

                if (DatabaseUIGlobals.DISPLAYED_USER.Images.Count > 0)
                {
                    pbxPersonFacialImages.Image = DatabaseUIGlobals.DISPLAYED_USER.Images[0];
                    DatabaseUIGlobals.DISPLAYED_IMAGE_INDEX = 0;
                }

                txtPersonID.Text = _person.Id;
                txtPersonForename.Text = _person.Forename;
                txtPersonSurname.Text = _person.Surname;
            }
        }

        private DatabaseEditingMode c_Editing = DatabaseEditingMode.ADDING_NEW_USER;

        private void cboSelectCRUDMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSelectCRUDMode.SelectedIndex == 0)
            {
                c_Editing = DatabaseEditingMode.ADDING_NEW_USER;
                txtPersonID.Text = "auto-assigned";
                txtPersonForename.Text = String.Empty;
                txtPersonSurname.Text = String.Empty;
                btnSavePersonToDatabase.Text = "Save Person";
                pbxPersonFacialImages.Image = null;

                //TODO
                //Extract this code
                DatabaseUIGlobals.DISPLAYED_USER = new FacialRecognition.Library.Models.Person();               
                DatabaseUIGlobals.DISPLAYED_IMAGE_INDEX = 0;

                txtPersonForename.Focus();
            }
            else if (cboSelectCRUDMode.SelectedIndex == 1)
            {
                c_Editing = DatabaseEditingMode.UPDATING_EXISTING_USER;
                this.DisplayPersonDetailsToEdit();
                btnSavePersonToDatabase.Text = "Update Person";
                grdUsers.Focus();
            }
        }

        private void btnSavePersonToDatabase_Click(object sender, EventArgs e)
        {
            //TODO - validation
            if (c_Editing == DatabaseEditingMode.UPDATING_EXISTING_USER)
            {
                var _personToUpdate = c_Database.Retrieve(txtPersonID.Text);

                _personToUpdate.Forename = txtPersonForename.Text;
                _personToUpdate.Surname = txtPersonSurname.Text;
                _personToUpdate.Images = DatabaseUIGlobals.DISPLAYED_USER.Images;

                c_Database.Update(_personToUpdate);

                MessageBox.Show("Person Updated");
            }
            else if (c_Editing == DatabaseEditingMode.ADDING_NEW_USER)
            {

                var _person = new FacialRecognition.Library.Models.Person();
                _person.Forename = txtPersonForename.Text;
                _person.Surname = txtPersonSurname.Text;
                _person.Images = DatabaseUIGlobals.DISPLAYED_USER.Images;

                c_Database.Store(_person);

                MessageBox.Show("Person Stored");

                txtPersonForename.Text = String.Empty;
                txtPersonSurname.Text = String.Empty;
                pbxPersonFacialImages.Image = null;
            }
            this.UpdateDatabaseDisplay();
        }

        private void btnUserCaptureImage_Click(object sender, EventArgs e)
        {
            if (c_Kinect != null)
            {
                pbxUserImage.Image = c_Kinect.CaptureImage();
            }
        }

        private void btnUserDetectFacialImage_Click(object sender, EventArgs e)
        {
            var _detector = new FacialRecognition.Library.Core.FacialDetector();

            c_Faces = _detector.DetectFaces((Bitmap)pbxUserImage.Image);

            if (c_Faces.Length < 1)
            {
                MessageBox.Show("No faces detected");
            }
            else
            {
                var g = pbxUserImage.CreateGraphics();
                var _pen = new Pen(Color.Green, 3);
                var _i = 0;

                while (_i < c_Faces.Length)
                {
                    g.DrawRectangle(_pen, c_Faces[_i]);
                    _i++;
                }
            }
        }

        private void btnUserAddFace_Click(object sender, EventArgs e)
        {
            try
            {
                var _normaliser = new FacialRecognition.Library.Octave.OctaveNormaliser();
                var _image = (Bitmap)pbxUserImage.Image;
                var _face = _image.Clone(c_Faces[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                var _result = _normaliser.NormaliseImage(_face, 168, 192);
                
                //Add to user images
                DatabaseUIGlobals.DISPLAYED_USER.Images.Add(_result);
                pbxPersonFacialImages.Image = DatabaseUIGlobals.DISPLAYED_USER.Images[DatabaseUIGlobals.DISPLAYED_USER.Images.Count - 1];
                DatabaseUIGlobals.DISPLAYED_IMAGE_INDEX = DatabaseUIGlobals.DISPLAYED_USER.Images.Count - 1;
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_ex.Message);
            }
        }

        //TODO
        //Extract these two methods to one method
        private void btnPersonImagesForward_Click(object sender, EventArgs e)
        {
            if (DatabaseUIGlobals.DISPLAYED_USER.Images.Count > 0)
            {
                var _imageCount = DatabaseUIGlobals.DISPLAYED_USER.Images.Count;
                var _displayedIndex = DatabaseUIGlobals.DISPLAYED_IMAGE_INDEX;

                if (_displayedIndex + 1 < _imageCount)
                {
                    pbxPersonFacialImages.Image = DatabaseUIGlobals.DISPLAYED_USER.Images[_displayedIndex + 1];
                    DatabaseUIGlobals.DISPLAYED_IMAGE_INDEX++;
                }
            }
        }

        private void btnPersonImagesBack_Click(object sender, EventArgs e)
        {
            if (DatabaseUIGlobals.DISPLAYED_USER.Images.Count > 0)
            {
                var _imageCount = DatabaseUIGlobals.DISPLAYED_USER.Images.Count;
                var _displayedIndex = DatabaseUIGlobals.DISPLAYED_IMAGE_INDEX;

                if (_displayedIndex - 1 >= 0)
                {
                    pbxPersonFacialImages.Image = DatabaseUIGlobals.DISPLAYED_USER.Images[_displayedIndex - 1];
                    DatabaseUIGlobals.DISPLAYED_IMAGE_INDEX--;
                }
            }
        }
    }
}