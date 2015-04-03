using FacialRecognition.Library.Core;
using FacialRecognition.Library.Database;
using FacialRecognition.Library.Hardware.KinectV1;
using FacialRecognition.Library.Models;
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

            this.Database = new CouchDatabase("localhost", 5984, "facial1");
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
                pbxImage.Image = this.Kinect.CaptureImage();
                pbxDept.Image = this.Kinect.CaptureDepthImage();
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
                this.Kinect.AdjustElevation(10);
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
                this.Kinect.AdjustElevation(-10);
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
                this.Kinect.SaveFrameData();

                MessageBox.Show("Data saved successfully to your Desktop", "Facial Recognition", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region FacialRecognitionTab
        Bitmap RecognitionSourceImage;
        Rectangle[] LocationOfDetectedFaces;

        private void PrepareKinectSensor()
        {
            if (KinectSensor.KinectSensors.Count > 0)
            {
                this.Kinect = new KinectV1Sensor(KinectSensor.KinectSensors[0]);
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
                if (this.Kinect != null)
                {
                    this.RecognitionSourceImage = this.Kinect.CaptureImage();
                    pbxCapturedColorImage.Image = this.RecognitionSourceImage;
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
            var openFileResult = diaOpenFile.ShowDialog();

            if (openFileResult == DialogResult.OK)
            {
                var filePath = diaOpenFile.FileName;
                var image = Image.FromFile(filePath);

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

                RecognitionSourceImage = new Bitmap(image);
                pbxCapturedColorImage.Image = RecognitionSourceImage;
                btnFacialDetection.Enabled = true;
            }
        }

        private void btnFacialDetection_Click(object sender, EventArgs e)
        {
            try 
            {
                this.LocationOfDetectedFaces = Detector.DetectFaces(RecognitionSourceImage);

                if (this.LocationOfDetectedFaces.Length < 1)
                {
                    MessageBox.Show("No faces detected");
                }
                else
                {
                    var graphics = pbxCapturedColorImage.CreateGraphics();
                    var pen = new Pen(Color.Green, 3);

                    foreach(var rectangle in this.LocationOfDetectedFaces)
                    {
                        graphics.DrawRectangle(pen, rectangle);
                    }

                    lblDetectedFaces.Text = "Detected Faces: " + this.LocationOfDetectedFaces.Length;
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
                var normaliser = new FacialRecognition.Library.Octave.OctaveNormaliser();
                var face = RecognitionSourceImage.Clone(LocationOfDetectedFaces[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                pbxSourceFace.Size = face.Size;
                pbxSourceFace.Image = face;

                var normalisedFace = normaliser.NormaliseImage(face, 168, 192);

                pbxNormalisedFace.Image = normalisedFace;
                btnPerformFacialRec.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPerformFacialRec_Click(object sender, EventArgs e)
        {
            try
            {
                var recogniser = new OctaveRecogniser(new OctaveInterface("localhost", 6379));

                var face = new Bitmap(pbxNormalisedFace.Image);

                var recognitionResult = recogniser.ClassifyFace(face);

                MessageBox.Show("Result: " + recognitionResult.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion    

        #region DatabaseTab
        private void UpdateDatabaseDisplay()
        {
            var people = this.Database.RetrieveAll();
            grdUsers.DataSource = people;
        }

        private void grdUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (this.EditingMode == DatabaseEditingMode.UpdatingExistingUser)
            {
                this.DisplayAPersonsDetailsForEditing();
            }
        }

        private void DisplayAPersonsDetailsForEditing()
        {
            var selectedPerson = grdUsers.SelectedRows;

            if (selectedPerson.Count == 1 && this.EditingMode == DatabaseEditingMode.UpdatingExistingUser)
            {
                var idOfSelectedPerson = selectedPerson[0].Cells["colIdentifier"].Value.ToString();

                var person = this.Database.Retrieve(idOfSelectedPerson);
                DatabaseUIGlobals.DisplayedUser = person;
                pbxPersonFacialImages.Image = null;

                if (DatabaseUIGlobals.DisplayedUser.Images.Count > 0)
                {
                    pbxPersonFacialImages.Image = DatabaseUIGlobals.DisplayedUser.Images[0];
                    DatabaseUIGlobals.DisplayedImageIndex = 0;
                }

                txtPersonID.Text = person.Id;
                txtPersonForename.Text = person.Forename;
                txtPersonSurname.Text = person.Surname;
            }
        }

        private DatabaseEditingMode EditingMode = DatabaseEditingMode.AddingNewUser;

        private void cboSelectCRUDMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSelectCRUDMode.SelectedIndex == 0)
            {
                this.EditingMode = DatabaseEditingMode.AddingNewUser;
                txtPersonID.Text = "auto-assigned";
                txtPersonForename.Text = String.Empty;
                txtPersonSurname.Text = String.Empty;
                btnSavePersonToDatabase.Text = "Save Person";
                pbxPersonFacialImages.Image = null;

                //TODO
                //Extract this code
                DatabaseUIGlobals.DisplayedUser = new Person();               
                DatabaseUIGlobals.DisplayedImageIndex = 0;

                txtPersonForename.Focus();
            }
            else if (cboSelectCRUDMode.SelectedIndex == 1)
            {
                this.EditingMode = DatabaseEditingMode.UpdatingExistingUser;
                this.DisplayAPersonsDetailsForEditing();
                btnSavePersonToDatabase.Text = "Update Person";
                grdUsers.Focus();
            }
        }

        private void btnSavePersonToDatabase_Click(object sender, EventArgs e)
        {
            // TODO - validation
            if (this.EditingMode == DatabaseEditingMode.UpdatingExistingUser)
            {
                var personToUpdate = this.Database.Retrieve(txtPersonID.Text);

                personToUpdate.Forename = txtPersonForename.Text;
                personToUpdate.Surname = txtPersonSurname.Text;
                personToUpdate.Images = DatabaseUIGlobals.DisplayedUser.Images;

                this.Database.Update(personToUpdate);

                MessageBox.Show("Person Updated");
            }
            else if (this.EditingMode == DatabaseEditingMode.AddingNewUser)
            {
                var person = new Person();
                person.Forename = txtPersonForename.Text;
                person.Surname = txtPersonSurname.Text;
                person.Images = DatabaseUIGlobals.DisplayedUser.Images;

                this.Database.Store(person);

                MessageBox.Show("Person Stored");

                txtPersonForename.Text = String.Empty;
                txtPersonSurname.Text = String.Empty;
                pbxPersonFacialImages.Image = null;
            }

            this.UpdateDatabaseDisplay();
        }

        private void btnUserCaptureImage_Click(object sender, EventArgs e)
        {
            if (this.Kinect != null)
            {
                pbxUserImage.Image = this.Kinect.CaptureImage();
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
                var graphics = pbxUserImage.CreateGraphics();
                var pen = new Pen(Color.Green, 3);

                foreach(var rectangle in this.LocationOfDetectedFaces)
                {
                    graphics.DrawRectangle(pen, rectangle);
                }
            }
        }

        private void btnUserAddFace_Click(object sender, EventArgs e)
        {
            try
            {
                var normaliser = new OctaveNormaliser();
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
                var imageCount = DatabaseUIGlobals.DisplayedUser.Images.Count;
                var displayedIndex = DatabaseUIGlobals.DisplayedImageIndex;

                if (displayedIndex + 1 < imageCount)
                {
                    pbxPersonFacialImages.Image = DatabaseUIGlobals.DisplayedUser.Images[displayedIndex + 1];
                    DatabaseUIGlobals.DisplayedImageIndex++;
                }
            }
        }

        private void btnPersonImagesBack_Click(object sender, EventArgs e)
        {
            if (DatabaseUIGlobals.DisplayedUser.Images.Count > 0)
            {
                var imageCount = DatabaseUIGlobals.DisplayedUser.Images.Count;
                var displayedIndex = DatabaseUIGlobals.DisplayedImageIndex;

                if (displayedIndex - 1 >= 0)
                {
                    pbxPersonFacialImages.Image = DatabaseUIGlobals.DisplayedUser.Images[displayedIndex - 1];
                    DatabaseUIGlobals.DisplayedImageIndex--;
                }
            }
        }
        #endregion
    }
}