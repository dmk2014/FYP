using FacialRecognition.Globals;
using FacialRecognition.Library.Models;
using FacialRecognition.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

using System.Threading.Tasks;
using System.Threading;

namespace FacialRecognition.Forms
{
    public partial class frmFacialRecognition : Form
    {
        private Controllers.StartupController StartupController;
        private Controllers.DetectionController DetectionController;
        private Controllers.ImageProcessingController ImageProcessingController;
        private Bitmap RecognitionSourceImage;
        private Bitmap DatabaseSourceImage;
        private DatabaseEditingMode EditingMode = DatabaseEditingMode.AddingNewUser;

        public frmFacialRecognition()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.DisplaySettings();
            this.StartupController = new FacialRecognition.Controllers.StartupController();
            this.DetectionController = new Controllers.DetectionController();
            this.ImageProcessingController = new Controllers.ImageProcessingController();
            this.InitialiseApplication();
            cboSelectCRUDMode.SelectedIndex = 0;
            this.UpdateDatabaseDisplay();
        }

        public void InitialiseApplication()
        {
            try
            {
                this.StartupController.SystemStartup();
            }
            catch (Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        #region CameraStreamsTab
        private void btnCaptureFrames_Click(object sender, EventArgs e)
        {
            try 
            {
                pbxImage.Image = ApplicationGlobals.Kinect.CaptureImage();
                pbxDept.Image = ApplicationGlobals.Kinect.CaptureDepthImage();
            }
            catch (Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        private void btnIncrElevation_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationGlobals.Kinect.AdjustElevation(10);
            }
            catch (Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        private void btnDecrElevation_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationGlobals.Kinect.AdjustElevation(-10);
            }
            catch (Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        private void btnSaveFrameData_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationGlobals.Kinect.SaveFrameData();
                MessageBox.Show("Data saved successfully to your Desktop", "Facial Recognition", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }
        #endregion

        #region FacialRecognitionTab
        private void btnCaptureFrame_Click(object sender, EventArgs e)
        {
            try
            {
                if (ApplicationGlobals.Kinect != null)
                {
                    this.RecognitionSourceImage = ApplicationGlobals.Kinect.CaptureImage(true);
                    pbxCapturedColorImage.Image = this.RecognitionSourceImage;
                    btnFacialDetection.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message + "\n\n" + ex.StackTrace);
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
                var imageToDisplay = new Bitmap((Bitmap)this.RecognitionSourceImage.Clone());
                pbxCapturedColorImage.Image = this.DetectionController.FindAndDrawDetectedFaces(imageToDisplay).Image;
                btnNormalise.Enabled = true;
            }
            catch(Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        private void btnNormalise_Click(object sender, EventArgs e)
        {
            try
            {
                var face = DetectionController.ExtractFacialImage(this.RecognitionSourceImage, ApplicationGlobals.LocationOfDetectedFaces);
                pbxSourceFace.Size = face.Size;
                pbxSourceFace.Image = face;

                var normalisedFace = this.ImageProcessingController.NormaliseFacialImage(pbxSourceFace.Image);
                pbxNormalisedFace.Image = normalisedFace;
                btnPerformFacialRec.Enabled = true;
            }
            catch (Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        private void btnPerformFacialRec_Click(object sender, EventArgs e)
        {
            try
            {
                this.ResetRecognitionResult();
                var face = new Bitmap(pbxNormalisedFace.Image);
                var recognitionResult = ApplicationGlobals.Recogniser.ClassifyFace(face);

                var person = ApplicationGlobals.Database.Retrieve(recognitionResult.Id);
                txtRecognisedID.Text = person.Id;
                txtRecognisedForename.Text = person.Forename;
                txtRecognisedSurname.Text = person.Surname;
            }
            catch (Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        private void ResetRecognitionResult()
        {
            txtRecognisedID.Text = String.Empty;
            txtRecognisedForename.Text = String.Empty;
            txtRecognisedSurname.Text = String.Empty;
        }
        #endregion    

        #region DatabaseTab
        private void UpdateDatabaseDisplay()
        {
            var people = ApplicationGlobals.Database.RetrieveAll();
            grdUsers.DataSource = people;
            this.SetDatabaseCrudMode();
        }

        private void SetDatabaseCrudMode()
        {
            if (cboSelectCRUDMode.SelectedIndex == 0)
            {
                this.EditingMode = DatabaseEditingMode.AddingNewUser;
                txtPersonID.Text = "auto-assigned";
                txtPersonForename.Text = String.Empty;
                txtPersonSurname.Text = String.Empty;
                btnSavePersonToDatabase.Text = "Save Person";
                pbxPersonFacialImages.Image = null;
                DisplayedPerson.Person = new Person();
                DisplayedPerson.ImageIndex = 0;
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

        private void grdUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (this.EditingMode == DatabaseEditingMode.UpdatingExistingUser)
            {
                this.DisplayAPersonsDetailsForEditing();
            }
        }

        private void cboSelectCRUDMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetDatabaseCrudMode();
        }

        private void DisplayAPersonsDetailsForEditing()
        {
            var selectedPerson = grdUsers.SelectedRows;

            if (selectedPerson.Count > 0 && this.EditingMode == DatabaseEditingMode.UpdatingExistingUser)
            {
                var idOfSelectedPerson = selectedPerson[0].Cells["colIdentifier"].Value.ToString();

                var person = ApplicationGlobals.Database.Retrieve(idOfSelectedPerson);
                DisplayedPerson.Person = person;
                pbxPersonFacialImages.Image = null;

                if (DisplayedPerson.Person.Images.Count > 0)
                {
                    pbxPersonFacialImages.Image = DisplayedPerson.Person.Images[0];
                    DisplayedPerson.ImageIndex = 0;
                }

                txtPersonID.Text = person.Id;
                txtPersonForename.Text = person.Forename;
                txtPersonSurname.Text = person.Surname;
            }
        }

        private void btnSavePersonToDatabase_Click(object sender, EventArgs e)
        {
            // TODO - validation
            if (this.EditingMode == DatabaseEditingMode.UpdatingExistingUser)
            {
                var personToUpdate = ApplicationGlobals.Database.Retrieve(txtPersonID.Text);

                personToUpdate.Forename = txtPersonForename.Text;
                personToUpdate.Surname = txtPersonSurname.Text;
                personToUpdate.Images = DisplayedPerson.Person.Images;

                ApplicationGlobals.Database.Update(personToUpdate);
                Messages.DisplayInformationMessage(this, "Document saved successfully");
            }
            else if (this.EditingMode == DatabaseEditingMode.AddingNewUser)
            {
                var person = new Person();
                person.Forename = txtPersonForename.Text;
                person.Surname = txtPersonSurname.Text;
                person.Images = DisplayedPerson.Person.Images;

                ApplicationGlobals.Database.Store(person);
                Messages.DisplayInformationMessage(this, "Document saved successfully");

                txtPersonForename.Text = String.Empty;
                txtPersonSurname.Text = String.Empty;
                pbxPersonFacialImages.Image = null;
            }

            this.UpdateDatabaseDisplay();
        }

        private void btnUserCaptureImage_Click(object sender, EventArgs e)
        {
            try
            {
                this.DatabaseSourceImage = ApplicationGlobals.Kinect.CaptureImage();
                pbxUserImage.Image = this.DatabaseSourceImage;
            }
            catch(Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        private void btnUserDetectFacialImage_Click(object sender, EventArgs e)
        {
            try
            {
                var imageToDisplay = new Bitmap((Bitmap)this.DatabaseSourceImage.Clone());
                pbxUserImage.Image = this.DetectionController.FindAndDrawDetectedFaces(imageToDisplay).Image;
            }
            catch(Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        private void btnUserAddFace_Click(object sender, EventArgs e)
        {
            try
            {
                var face = this.DetectionController.ExtractFacialImage(this.DatabaseSourceImage, ApplicationGlobals.LocationOfDetectedFaces);
                var normalisedImage = this.ImageProcessingController.NormaliseFacialImage(face);
                
                // Add to user images
                DisplayedPerson.Person.Images.Add(normalisedImage);
                pbxPersonFacialImages.Image = DisplayedPerson.Person.Images[DisplayedPerson.Person.Images.Count - 1];
                DisplayedPerson.ImageIndex = DisplayedPerson.Person.Images.Count - 1;
            }
            catch (Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        // TODO
        // Extract these two methods to one method
        private void btnPersonImagesForward_Click(object sender, EventArgs e)
        {
            if (DisplayedPerson.Person.Images.Count > 0)
            {
                var imageCount = DisplayedPerson.Person.Images.Count;
                var displayedIndex = DisplayedPerson.ImageIndex;

                if (displayedIndex + 1 < imageCount)
                {
                    pbxPersonFacialImages.Image = DisplayedPerson.Person.Images[displayedIndex + 1];
                    DisplayedPerson.ImageIndex++;
                }
            }
        }

        private void btnPersonImagesBack_Click(object sender, EventArgs e)
        {
            if (DisplayedPerson.Person.Images.Count > 0)
            {
                var imageCount = DisplayedPerson.Person.Images.Count;
                var displayedIndex = DisplayedPerson.ImageIndex;

                if (displayedIndex - 1 >= 0)
                {
                    pbxPersonFacialImages.Image = DisplayedPerson.Person.Images[displayedIndex - 1];
                    DisplayedPerson.ImageIndex--;
                }
            }
        }
        #endregion

        #region ConfigurationTab
        private void btnRetrainRecogniser_Click(object sender, EventArgs e)
        {
            // Send the retrain request
            Messages.DisplayInformationMessage(this, "Retraining - this will take some time");

            try
            {
                this.RetrainRecogniserAsync();
            }
            catch(Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
        }

        private async void RetrainRecogniserAsync()
        {
            // References for asynchronous method call:
            // http://dotnetcodr.com/2014/01/01/5-ways-to-start-a-task-in-net-c/
            // https://msdn.microsoft.com/en-us/library/hh524395.aspx
            
            // Show wait window
            this.Hide();
            var waitWindow = new frmWaitWindow();
            waitWindow.Show(this);

            await Task.Run(() => ApplicationGlobals.Recogniser.RetrainRecogniser(ApplicationGlobals.Database.RetrieveAll()));

            // Show main window once task has completed
            waitWindow.Close();
            this.Show();
        }

        private void btnPersistRecogniserData_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ApplicationGlobals.Recogniser.SaveSession();
            }
            catch (Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnReloadRecogniserData_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ApplicationGlobals.Recogniser.ReloadSession();
            }
            catch (Exception ex)
            {
                Messages.DisplayErrorMessage(this, ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void DisplaySettings()
        {
            txtCouchDBHost.Text = Properties.Settings.Default.CouchDBHost;
            txtCouchDBPort.Text = Properties.Settings.Default.CouchDBPort.ToString();
            txtCouchDatabaseName.Text = Properties.Settings.Default.CouchDatabaseName;
            txtRedisHost.Text = Properties.Settings.Default.RedisHost;
            txtRedisPort.Text = Properties.Settings.Default.RedisPort.ToString();
            txtMaxDepth.Text = Properties.Settings.Default.MaxImageDepth.ToString();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            // Retrieve and save new settings
            Properties.Settings.Default.CouchDBHost = txtCouchDBHost.Text;
            Properties.Settings.Default.CouchDBPort = int.Parse(txtCouchDBPort.Text);
            Properties.Settings.Default.CouchDatabaseName = txtCouchDatabaseName.Text;
            Properties.Settings.Default.RedisHost = txtRedisHost.Text;
            Properties.Settings.Default.RedisPort = int.Parse(txtRedisPort.Text);
            Properties.Settings.Default.MaxImageDepth = int.Parse(txtMaxDepth.Text);
            Properties.Settings.Default.Save();

            Messages.DisplayInformationMessage(this, "Setting saved successfully. Application will now attempt to re-connect to data stores using specified settings.");

            // Initialise system using new settings
            this.Cursor = Cursors.WaitCursor;
            this.InitialiseApplication();
            this.UpdateDatabaseDisplay();
            this.Cursor = Cursors.Default;
        }
        #endregion
    }
}