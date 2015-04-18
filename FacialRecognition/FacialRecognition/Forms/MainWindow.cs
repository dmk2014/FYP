using FacialRecognition.Library.Database;
using FacialRecognition.Library.Detection;
using FacialRecognition.Library.Hardware.KinectV1;
using FacialRecognition.Library.ImageProcessing;
using FacialRecognition.Library.Models;
using FacialRecognition.Library.Recognition;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FacialRecognition.Forms
{
    public partial class frmFacialRecognition : Form
    {
        private IDatabase Database;
        private KinectV1Sensor Kinect;
        private FacialDetector Detector;
        private PhotometricFacialRecogniser Recogniser;

        private string CouchDBHost;
        private int CouchDBPort;
        private string CouchDatabaseName;
        private string RedisHost;
        private int RedisPort;

        public frmFacialRecognition()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.SystemStartup();
        }

        public void SystemStartup()
        {
            var errorsOccured = false;
            var errorMessages = new List<String>();

            // Read settings
            this.CouchDBHost = Properties.Settings.Default.CouchDBHost;
            this.CouchDBPort = Properties.Settings.Default.CouchDBPort;
            this.CouchDatabaseName = Properties.Settings.Default.CouchDatabaseName;
            this.RedisHost = Properties.Settings.Default.RedisHost;
            this.RedisPort = Properties.Settings.Default.RedisPort;

            // Display settings on configuration tab
            txtCouchDBHost.Text = this.CouchDBHost;
            txtCouchDBPort.Text = this.CouchDBPort.ToString();
            txtCouchDatabaseName.Text = this.CouchDatabaseName;
            txtRedisHost.Text = this.RedisHost;
            txtRedisPort.Text = this.RedisPort.ToString();

            // Construct Facial Detector
            this.Detector = new FacialDetector();

            // Initialise hardware & data stores:
            // Initialise Database
            try
            {
                this.Database = new CouchDatabase(this.CouchDBHost, this.CouchDBPort, this.CouchDatabaseName);
                this.UpdateDatabaseDisplay();
                cboSelectCRUDMode.SelectedIndex = (int)DatabaseEditingMode.AddingNewUser;
            }
            catch(Exception ex)
            {
                errorsOccured = true;
                errorMessages.Add(ex.Message);
            }

            // Initialise Kinect sensor
            try
            {
                this.PrepareKinectSensor(); 
            }
            catch(Exception ex)
            {
                errorsOccured = true;
                errorMessages.Add(ex.Message);
            }

            // Initialise OctaveRecogniser which utilises Redis
            try
            {
                this.Recogniser = new PhotometricFacialRecogniser(this.RedisHost, this.RedisPort);
            }
            catch(Exception ex)
            {
                errorsOccured = true;
                errorMessages.Add(ex.Message);
            }

            if(errorsOccured)
            {
                var errorOutput = String.Empty;

                foreach(var message in errorMessages)
                {
                    errorOutput += message + "\n\n";
                }

                errorOutput.TrimEnd('\n');

                string error = "There were errors launching the application - this may prevent it from functioning correctly.\n\nError(s):\n" + errorOutput;
                MessageBox.Show(this, error, "Initialisation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabMain.SelectTab(tabConfiguration);
            } 
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
                throw new Exception("Could find a Kinect sensor attached to this system");
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
                var normaliser = new PhotometricFacialImageNormaliser();
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
                var face = new Bitmap(pbxNormalisedFace.Image);

                var recognitionResult = this.Recogniser.ClassifyFace(face);

                MessageBox.Show(this, "Result: " + recognitionResult.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
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
                DisplayedPersonConstants.DisplayedPerson = person;
                pbxPersonFacialImages.Image = null;

                if (DisplayedPersonConstants.DisplayedPerson.Images.Count > 0)
                {
                    pbxPersonFacialImages.Image = DisplayedPersonConstants.DisplayedPerson.Images[0];
                    DisplayedPersonConstants.DisplayedImageIndex = 0;
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
                DisplayedPersonConstants.DisplayedPerson = new Person();               
                DisplayedPersonConstants.DisplayedImageIndex = 0;

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
                personToUpdate.Images = DisplayedPersonConstants.DisplayedPerson.Images;

                this.Database.Update(personToUpdate);

                MessageBox.Show("Person Updated");
            }
            else if (this.EditingMode == DatabaseEditingMode.AddingNewUser)
            {
                var person = new Person();
                person.Forename = txtPersonForename.Text;
                person.Surname = txtPersonSurname.Text;
                person.Images = DisplayedPersonConstants.DisplayedPerson.Images;

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
            try
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

                    foreach (var rectangle in this.LocationOfDetectedFaces)
                    {
                        graphics.DrawRectangle(pen, rectangle);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "FacialDetection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUserAddFace_Click(object sender, EventArgs e)
        {
            try
            {
                var normaliser = new PhotometricFacialImageNormaliser();
                var capturedImage = new Bitmap(pbxUserImage.Image);
                var face = capturedImage.Clone(LocationOfDetectedFaces[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                var normalisedImage = normaliser.NormaliseImage(face, 168, 192);
                
                // Add to user images
                DisplayedPersonConstants.DisplayedPerson.Images.Add(normalisedImage);
                pbxPersonFacialImages.Image = DisplayedPersonConstants.DisplayedPerson.Images[DisplayedPersonConstants.DisplayedPerson.Images.Count - 1];
                DisplayedPersonConstants.DisplayedImageIndex = DisplayedPersonConstants.DisplayedPerson.Images.Count - 1;
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
            if (DisplayedPersonConstants.DisplayedPerson.Images.Count > 0)
            {
                var imageCount = DisplayedPersonConstants.DisplayedPerson.Images.Count;
                var displayedIndex = DisplayedPersonConstants.DisplayedImageIndex;

                if (displayedIndex + 1 < imageCount)
                {
                    pbxPersonFacialImages.Image = DisplayedPersonConstants.DisplayedPerson.Images[displayedIndex + 1];
                    DisplayedPersonConstants.DisplayedImageIndex++;
                }
            }
        }

        private void btnPersonImagesBack_Click(object sender, EventArgs e)
        {
            if (DisplayedPersonConstants.DisplayedPerson.Images.Count > 0)
            {
                var imageCount = DisplayedPersonConstants.DisplayedPerson.Images.Count;
                var displayedIndex = DisplayedPersonConstants.DisplayedImageIndex;

                if (displayedIndex - 1 >= 0)
                {
                    pbxPersonFacialImages.Image = DisplayedPersonConstants.DisplayedPerson.Images[displayedIndex - 1];
                    DisplayedPersonConstants.DisplayedImageIndex--;
                }
            }
        }
        #endregion

        #region ConfigurationTab
        private void btnRetrainRecogniser_Click(object sender, EventArgs e)
        {
            // Send the retrain request
            MessageBox.Show(this, "Retraining - this will take some time", "Facial Recognition", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                this.Recogniser.RetrainRecogniser(this.Database.RetrieveAll());
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPersistRecogniserData_Click(object sender, EventArgs e)
        {
            try
            {
                this.Recogniser.SaveSession();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReloadRecogniserData_Click(object sender, EventArgs e)
        {
            try
            {
                this.Recogniser.ReloadSession();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            // Retrieve and save new settings
            Properties.Settings.Default.CouchDBHost = txtCouchDBHost.Text;
            Properties.Settings.Default.CouchDBPort = int.Parse(txtCouchDBPort.Text);
            Properties.Settings.Default.CouchDatabaseName = txtCouchDatabaseName.Text;
            Properties.Settings.Default.RedisHost = txtRedisHost.Text;
            Properties.Settings.Default.RedisPort = int.Parse(txtRedisPort.Text);

            Properties.Settings.Default.Save();

            MessageBox.Show(this, "Setting saved successfully. Application will now attempt to re-connect to data stores using specified settings.", "Facial Recognition", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Initialise system using new settings
            this.SystemStartup();
        }
        #endregion
    }
}