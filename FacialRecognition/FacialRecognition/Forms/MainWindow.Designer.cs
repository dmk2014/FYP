namespace FacialRecognition.Forms
{
    partial class frmFacialRecognition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbxImage = new System.Windows.Forms.PictureBox();
            this.btnIncrElevation = new System.Windows.Forms.Button();
            this.btnDecrElevation = new System.Windows.Forms.Button();
            this.pbxDept = new System.Windows.Forms.PictureBox();
            this.grpControls = new System.Windows.Forms.GroupBox();
            this.btnCaptureFrames = new System.Windows.Forms.Button();
            this.btnSaveFrameData = new System.Windows.Forms.Button();
            this.grpColour = new System.Windows.Forms.GroupBox();
            this.grpDepth = new System.Windows.Forms.GroupBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabFacialRec = new System.Windows.Forms.TabPage();
            this.grpRecognitionResult = new System.Windows.Forms.GroupBox();
            this.btnViewRecognitionResultInDatabase = new System.Windows.Forms.Button();
            this.txtRecognisedForename = new System.Windows.Forms.TextBox();
            this.txtRecognisedSurname = new System.Windows.Forms.TextBox();
            this.txtRecognisedID = new System.Windows.Forms.TextBox();
            this.lblRecognisedForename = new System.Windows.Forms.Label();
            this.lblRecognisedSurname = new System.Windows.Forms.Label();
            this.lblRecognisedPersonID = new System.Windows.Forms.Label();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.pbxSourceFace = new System.Windows.Forms.PictureBox();
            this.lblDetectedFaces = new System.Windows.Forms.Label();
            this.btnNormalise = new System.Windows.Forms.Button();
            this.pbxNormalisedFace = new System.Windows.Forms.PictureBox();
            this.pbxCapturedColorImage = new System.Windows.Forms.PictureBox();
            this.btnPerformFacialRec = new System.Windows.Forms.Button();
            this.btnFacialDetection = new System.Windows.Forms.Button();
            this.btnCaptureFrame = new System.Windows.Forms.Button();
            this.tabDatabase = new System.Windows.Forms.TabPage();
            this.btnUserAddFace = new System.Windows.Forms.Button();
            this.btnUserDetectFacialImage = new System.Windows.Forms.Button();
            this.btnUserCaptureImage = new System.Windows.Forms.Button();
            this.pbxUserImage = new System.Windows.Forms.PictureBox();
            this.btnPersonImagesDelete = new System.Windows.Forms.Button();
            this.btnPersonImagesForward = new System.Windows.Forms.Button();
            this.btnPersonImagesBack = new System.Windows.Forms.Button();
            this.lblChooseMode = new System.Windows.Forms.Label();
            this.cboSelectCRUDMode = new System.Windows.Forms.ComboBox();
            this.pbxPersonFacialImages = new System.Windows.Forms.PictureBox();
            this.btnSavePersonToDatabase = new System.Windows.Forms.Button();
            this.txtPersonID = new System.Windows.Forms.TextBox();
            this.lblNewPersonID = new System.Windows.Forms.Label();
            this.txtPersonSurname = new System.Windows.Forms.TextBox();
            this.txtPersonForename = new System.Windows.Forms.TextBox();
            this.lblNewPersonSurname = new System.Windows.Forms.Label();
            this.lblNewPersonForename = new System.Windows.Forms.Label();
            this.grdUsers = new System.Windows.Forms.DataGridView();
            this.colIdentifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colForename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSurname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colImageCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabStreams = new System.Windows.Forms.TabPage();
            this.tabConfiguration = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCouchDatabaseName = new System.Windows.Forms.TextBox();
            this.lblCouchDatabaseName = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.txtCouchDBPort = new System.Windows.Forms.TextBox();
            this.lblCouchDBPort = new System.Windows.Forms.Label();
            this.txtCouchDBHost = new System.Windows.Forms.TextBox();
            this.lblCouchDBHost = new System.Windows.Forms.Label();
            this.txtRedisPort = new System.Windows.Forms.TextBox();
            this.lblRedisPort = new System.Windows.Forms.Label();
            this.txtRedisHost = new System.Windows.Forms.TextBox();
            this.lblRedisHost = new System.Windows.Forms.Label();
            this.grpRecogniserControls = new System.Windows.Forms.GroupBox();
            this.btnRetrainRecogniser = new System.Windows.Forms.Button();
            this.btnReloadRecogniserData = new System.Windows.Forms.Button();
            this.btnPersistRecogniserData = new System.Windows.Forms.Button();
            this.diaOpenFile = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDept)).BeginInit();
            this.grpControls.SuspendLayout();
            this.grpColour.SuspendLayout();
            this.grpDepth.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabFacialRec.SuspendLayout();
            this.grpRecognitionResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSourceFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNormalisedFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCapturedColorImage)).BeginInit();
            this.tabDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUserImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPersonFacialImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).BeginInit();
            this.tabStreams.SuspendLayout();
            this.tabConfiguration.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpRecogniserControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxImage
            // 
            this.pbxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxImage.Location = new System.Drawing.Point(6, 19);
            this.pbxImage.Name = "pbxImage";
            this.pbxImage.Size = new System.Drawing.Size(640, 480);
            this.pbxImage.TabIndex = 0;
            this.pbxImage.TabStop = false;
            // 
            // btnIncrElevation
            // 
            this.btnIncrElevation.Location = new System.Drawing.Point(238, 19);
            this.btnIncrElevation.Name = "btnIncrElevation";
            this.btnIncrElevation.Size = new System.Drawing.Size(110, 36);
            this.btnIncrElevation.TabIndex = 1;
            this.btnIncrElevation.Text = "Increase Elevation";
            this.btnIncrElevation.UseVisualStyleBackColor = true;
            this.btnIncrElevation.Click += new System.EventHandler(this.btnIncrElevation_Click);
            // 
            // btnDecrElevation
            // 
            this.btnDecrElevation.Location = new System.Drawing.Point(354, 19);
            this.btnDecrElevation.Name = "btnDecrElevation";
            this.btnDecrElevation.Size = new System.Drawing.Size(110, 36);
            this.btnDecrElevation.TabIndex = 2;
            this.btnDecrElevation.Text = "Decrease Elevation";
            this.btnDecrElevation.UseVisualStyleBackColor = true;
            this.btnDecrElevation.Click += new System.EventHandler(this.btnDecrElevation_Click);
            // 
            // pbxDept
            // 
            this.pbxDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxDept.Location = new System.Drawing.Point(10, 19);
            this.pbxDept.Name = "pbxDept";
            this.pbxDept.Size = new System.Drawing.Size(640, 480);
            this.pbxDept.TabIndex = 3;
            this.pbxDept.TabStop = false;
            // 
            // grpControls
            // 
            this.grpControls.Controls.Add(this.btnCaptureFrames);
            this.grpControls.Controls.Add(this.btnSaveFrameData);
            this.grpControls.Controls.Add(this.btnIncrElevation);
            this.grpControls.Controls.Add(this.btnDecrElevation);
            this.grpControls.Location = new System.Drawing.Point(445, 526);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new System.Drawing.Size(473, 73);
            this.grpControls.TabIndex = 4;
            this.grpControls.TabStop = false;
            this.grpControls.Text = "Camera Controls";
            // 
            // btnCaptureFrames
            // 
            this.btnCaptureFrames.Location = new System.Drawing.Point(6, 19);
            this.btnCaptureFrames.Name = "btnCaptureFrames";
            this.btnCaptureFrames.Size = new System.Drawing.Size(110, 36);
            this.btnCaptureFrames.TabIndex = 8;
            this.btnCaptureFrames.Text = "Capture Frames";
            this.btnCaptureFrames.UseVisualStyleBackColor = true;
            this.btnCaptureFrames.Click += new System.EventHandler(this.btnCaptureFrames_Click);
            // 
            // btnSaveFrameData
            // 
            this.btnSaveFrameData.Location = new System.Drawing.Point(122, 19);
            this.btnSaveFrameData.Name = "btnSaveFrameData";
            this.btnSaveFrameData.Size = new System.Drawing.Size(110, 36);
            this.btnSaveFrameData.TabIndex = 7;
            this.btnSaveFrameData.Text = "Save Raw Frame Data";
            this.btnSaveFrameData.UseVisualStyleBackColor = true;
            this.btnSaveFrameData.Click += new System.EventHandler(this.btnSaveFrameData_Click);
            // 
            // grpColour
            // 
            this.grpColour.Controls.Add(this.pbxImage);
            this.grpColour.Location = new System.Drawing.Point(6, 6);
            this.grpColour.Name = "grpColour";
            this.grpColour.Size = new System.Drawing.Size(662, 508);
            this.grpColour.TabIndex = 5;
            this.grpColour.TabStop = false;
            this.grpColour.Text = "Colour Stream";
            // 
            // grpDepth
            // 
            this.grpDepth.Controls.Add(this.pbxDept);
            this.grpDepth.Location = new System.Drawing.Point(692, 6);
            this.grpDepth.Name = "grpDepth";
            this.grpDepth.Size = new System.Drawing.Size(662, 508);
            this.grpDepth.TabIndex = 6;
            this.grpDepth.TabStop = false;
            this.grpDepth.Text = "Depth Stream";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabFacialRec);
            this.tabMain.Controls.Add(this.tabDatabase);
            this.tabMain.Controls.Add(this.tabStreams);
            this.tabMain.Controls.Add(this.tabConfiguration);
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1371, 639);
            this.tabMain.TabIndex = 7;
            // 
            // tabFacialRec
            // 
            this.tabFacialRec.BackColor = System.Drawing.SystemColors.Control;
            this.tabFacialRec.Controls.Add(this.grpRecognitionResult);
            this.tabFacialRec.Controls.Add(this.btnLoadImage);
            this.tabFacialRec.Controls.Add(this.pbxSourceFace);
            this.tabFacialRec.Controls.Add(this.lblDetectedFaces);
            this.tabFacialRec.Controls.Add(this.btnNormalise);
            this.tabFacialRec.Controls.Add(this.pbxNormalisedFace);
            this.tabFacialRec.Controls.Add(this.pbxCapturedColorImage);
            this.tabFacialRec.Controls.Add(this.btnPerformFacialRec);
            this.tabFacialRec.Controls.Add(this.btnFacialDetection);
            this.tabFacialRec.Controls.Add(this.btnCaptureFrame);
            this.tabFacialRec.Location = new System.Drawing.Point(4, 22);
            this.tabFacialRec.Name = "tabFacialRec";
            this.tabFacialRec.Padding = new System.Windows.Forms.Padding(3);
            this.tabFacialRec.Size = new System.Drawing.Size(1363, 613);
            this.tabFacialRec.TabIndex = 1;
            this.tabFacialRec.Text = "Facial Recognition";
            // 
            // grpRecognitionResult
            // 
            this.grpRecognitionResult.Controls.Add(this.btnViewRecognitionResultInDatabase);
            this.grpRecognitionResult.Controls.Add(this.txtRecognisedForename);
            this.grpRecognitionResult.Controls.Add(this.txtRecognisedSurname);
            this.grpRecognitionResult.Controls.Add(this.txtRecognisedID);
            this.grpRecognitionResult.Controls.Add(this.lblRecognisedForename);
            this.grpRecognitionResult.Controls.Add(this.lblRecognisedSurname);
            this.grpRecognitionResult.Controls.Add(this.lblRecognisedPersonID);
            this.grpRecognitionResult.Location = new System.Drawing.Point(785, 337);
            this.grpRecognitionResult.Name = "grpRecognitionResult";
            this.grpRecognitionResult.Size = new System.Drawing.Size(426, 232);
            this.grpRecognitionResult.TabIndex = 9;
            this.grpRecognitionResult.TabStop = false;
            this.grpRecognitionResult.Text = "Recognition Result";
            // 
            // btnViewRecognitionResultInDatabase
            // 
            this.btnViewRecognitionResultInDatabase.Enabled = false;
            this.btnViewRecognitionResultInDatabase.Location = new System.Drawing.Point(218, 102);
            this.btnViewRecognitionResultInDatabase.Name = "btnViewRecognitionResultInDatabase";
            this.btnViewRecognitionResultInDatabase.Size = new System.Drawing.Size(123, 40);
            this.btnViewRecognitionResultInDatabase.TabIndex = 10;
            this.btnViewRecognitionResultInDatabase.Text = "View in Database";
            this.btnViewRecognitionResultInDatabase.UseVisualStyleBackColor = true;
            // 
            // txtRecognisedForename
            // 
            this.txtRecognisedForename.Location = new System.Drawing.Point(143, 50);
            this.txtRecognisedForename.Name = "txtRecognisedForename";
            this.txtRecognisedForename.ReadOnly = true;
            this.txtRecognisedForename.Size = new System.Drawing.Size(198, 20);
            this.txtRecognisedForename.TabIndex = 15;
            // 
            // txtRecognisedSurname
            // 
            this.txtRecognisedSurname.Location = new System.Drawing.Point(143, 76);
            this.txtRecognisedSurname.Name = "txtRecognisedSurname";
            this.txtRecognisedSurname.ReadOnly = true;
            this.txtRecognisedSurname.Size = new System.Drawing.Size(198, 20);
            this.txtRecognisedSurname.TabIndex = 14;
            // 
            // txtRecognisedID
            // 
            this.txtRecognisedID.Location = new System.Drawing.Point(143, 24);
            this.txtRecognisedID.Name = "txtRecognisedID";
            this.txtRecognisedID.ReadOnly = true;
            this.txtRecognisedID.Size = new System.Drawing.Size(198, 20);
            this.txtRecognisedID.TabIndex = 13;
            // 
            // lblRecognisedForename
            // 
            this.lblRecognisedForename.AutoSize = true;
            this.lblRecognisedForename.Location = new System.Drawing.Point(75, 53);
            this.lblRecognisedForename.Name = "lblRecognisedForename";
            this.lblRecognisedForename.Size = new System.Drawing.Size(54, 13);
            this.lblRecognisedForename.TabIndex = 12;
            this.lblRecognisedForename.Text = "Forename";
            // 
            // lblRecognisedSurname
            // 
            this.lblRecognisedSurname.AutoSize = true;
            this.lblRecognisedSurname.Location = new System.Drawing.Point(76, 79);
            this.lblRecognisedSurname.Name = "lblRecognisedSurname";
            this.lblRecognisedSurname.Size = new System.Drawing.Size(49, 13);
            this.lblRecognisedSurname.TabIndex = 11;
            this.lblRecognisedSurname.Text = "Surname";
            // 
            // lblRecognisedPersonID
            // 
            this.lblRecognisedPersonID.AutoSize = true;
            this.lblRecognisedPersonID.Location = new System.Drawing.Point(48, 27);
            this.lblRecognisedPersonID.Name = "lblRecognisedPersonID";
            this.lblRecognisedPersonID.Size = new System.Drawing.Size(81, 13);
            this.lblRecognisedPersonID.TabIndex = 10;
            this.lblRecognisedPersonID.Text = "Couch Identifier";
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Enabled = false;
            this.btnLoadImage.Location = new System.Drawing.Point(311, 16);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(123, 40);
            this.btnLoadImage.TabIndex = 8;
            this.btnLoadImage.Text = "Load an Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // pbxSourceFace
            // 
            this.pbxSourceFace.BackColor = System.Drawing.SystemColors.Control;
            this.pbxSourceFace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxSourceFace.Location = new System.Drawing.Point(785, 126);
            this.pbxSourceFace.MaximumSize = new System.Drawing.Size(168, 192);
            this.pbxSourceFace.Name = "pbxSourceFace";
            this.pbxSourceFace.Size = new System.Drawing.Size(168, 192);
            this.pbxSourceFace.TabIndex = 7;
            this.pbxSourceFace.TabStop = false;
            // 
            // lblDetectedFaces
            // 
            this.lblDetectedFaces.AutoSize = true;
            this.lblDetectedFaces.Location = new System.Drawing.Point(785, 97);
            this.lblDetectedFaces.Name = "lblDetectedFaces";
            this.lblDetectedFaces.Size = new System.Drawing.Size(95, 13);
            this.lblDetectedFaces.TabIndex = 6;
            this.lblDetectedFaces.Text = "Detected Faces: 0";
            // 
            // btnNormalise
            // 
            this.btnNormalise.Enabled = false;
            this.btnNormalise.Location = new System.Drawing.Point(959, 16);
            this.btnNormalise.Name = "btnNormalise";
            this.btnNormalise.Size = new System.Drawing.Size(123, 40);
            this.btnNormalise.TabIndex = 5;
            this.btnNormalise.Text = "Perform Facial Normalisation";
            this.btnNormalise.UseVisualStyleBackColor = true;
            this.btnNormalise.Click += new System.EventHandler(this.btnNormalise_Click);
            // 
            // pbxNormalisedFace
            // 
            this.pbxNormalisedFace.BackColor = System.Drawing.SystemColors.Control;
            this.pbxNormalisedFace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxNormalisedFace.Location = new System.Drawing.Point(1040, 126);
            this.pbxNormalisedFace.Name = "pbxNormalisedFace";
            this.pbxNormalisedFace.Size = new System.Drawing.Size(168, 192);
            this.pbxNormalisedFace.TabIndex = 4;
            this.pbxNormalisedFace.TabStop = false;
            // 
            // pbxCapturedColorImage
            // 
            this.pbxCapturedColorImage.BackColor = System.Drawing.SystemColors.Control;
            this.pbxCapturedColorImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxCapturedColorImage.Location = new System.Drawing.Point(6, 89);
            this.pbxCapturedColorImage.Name = "pbxCapturedColorImage";
            this.pbxCapturedColorImage.Size = new System.Drawing.Size(640, 480);
            this.pbxCapturedColorImage.TabIndex = 3;
            this.pbxCapturedColorImage.TabStop = false;
            // 
            // btnPerformFacialRec
            // 
            this.btnPerformFacialRec.Enabled = false;
            this.btnPerformFacialRec.Location = new System.Drawing.Point(1088, 16);
            this.btnPerformFacialRec.Name = "btnPerformFacialRec";
            this.btnPerformFacialRec.Size = new System.Drawing.Size(123, 40);
            this.btnPerformFacialRec.TabIndex = 2;
            this.btnPerformFacialRec.Text = "Perform Facial Recognition";
            this.btnPerformFacialRec.UseVisualStyleBackColor = true;
            this.btnPerformFacialRec.Click += new System.EventHandler(this.btnPerformFacialRec_Click);
            // 
            // btnFacialDetection
            // 
            this.btnFacialDetection.Enabled = false;
            this.btnFacialDetection.Location = new System.Drawing.Point(830, 16);
            this.btnFacialDetection.Name = "btnFacialDetection";
            this.btnFacialDetection.Size = new System.Drawing.Size(123, 40);
            this.btnFacialDetection.TabIndex = 1;
            this.btnFacialDetection.Text = "Perform Facial Detection";
            this.btnFacialDetection.UseVisualStyleBackColor = true;
            this.btnFacialDetection.Click += new System.EventHandler(this.btnFacialDetection_Click);
            // 
            // btnCaptureFrame
            // 
            this.btnCaptureFrame.Location = new System.Drawing.Point(182, 16);
            this.btnCaptureFrame.Name = "btnCaptureFrame";
            this.btnCaptureFrame.Size = new System.Drawing.Size(123, 40);
            this.btnCaptureFrame.TabIndex = 0;
            this.btnCaptureFrame.Text = "Capture Frame";
            this.btnCaptureFrame.UseVisualStyleBackColor = true;
            this.btnCaptureFrame.Click += new System.EventHandler(this.btnCaptureFrame_Click);
            // 
            // tabDatabase
            // 
            this.tabDatabase.BackColor = System.Drawing.SystemColors.Control;
            this.tabDatabase.Controls.Add(this.btnUserAddFace);
            this.tabDatabase.Controls.Add(this.btnUserDetectFacialImage);
            this.tabDatabase.Controls.Add(this.btnUserCaptureImage);
            this.tabDatabase.Controls.Add(this.pbxUserImage);
            this.tabDatabase.Controls.Add(this.btnPersonImagesDelete);
            this.tabDatabase.Controls.Add(this.btnPersonImagesForward);
            this.tabDatabase.Controls.Add(this.btnPersonImagesBack);
            this.tabDatabase.Controls.Add(this.lblChooseMode);
            this.tabDatabase.Controls.Add(this.cboSelectCRUDMode);
            this.tabDatabase.Controls.Add(this.pbxPersonFacialImages);
            this.tabDatabase.Controls.Add(this.btnSavePersonToDatabase);
            this.tabDatabase.Controls.Add(this.txtPersonID);
            this.tabDatabase.Controls.Add(this.lblNewPersonID);
            this.tabDatabase.Controls.Add(this.txtPersonSurname);
            this.tabDatabase.Controls.Add(this.txtPersonForename);
            this.tabDatabase.Controls.Add(this.lblNewPersonSurname);
            this.tabDatabase.Controls.Add(this.lblNewPersonForename);
            this.tabDatabase.Controls.Add(this.grdUsers);
            this.tabDatabase.Location = new System.Drawing.Point(4, 22);
            this.tabDatabase.Name = "tabDatabase";
            this.tabDatabase.Size = new System.Drawing.Size(1363, 613);
            this.tabDatabase.TabIndex = 3;
            this.tabDatabase.Text = "Database";
            // 
            // btnUserAddFace
            // 
            this.btnUserAddFace.Location = new System.Drawing.Point(392, 521);
            this.btnUserAddFace.Name = "btnUserAddFace";
            this.btnUserAddFace.Size = new System.Drawing.Size(123, 40);
            this.btnUserAddFace.TabIndex = 27;
            this.btnUserAddFace.Text = "Add Face To User Data";
            this.btnUserAddFace.UseVisualStyleBackColor = true;
            this.btnUserAddFace.Click += new System.EventHandler(this.btnUserAddFace_Click);
            // 
            // btnUserDetectFacialImage
            // 
            this.btnUserDetectFacialImage.Location = new System.Drawing.Point(263, 521);
            this.btnUserDetectFacialImage.Name = "btnUserDetectFacialImage";
            this.btnUserDetectFacialImage.Size = new System.Drawing.Size(123, 40);
            this.btnUserDetectFacialImage.TabIndex = 26;
            this.btnUserDetectFacialImage.Text = "Detect Face";
            this.btnUserDetectFacialImage.UseVisualStyleBackColor = true;
            this.btnUserDetectFacialImage.Click += new System.EventHandler(this.btnUserDetectFacialImage_Click);
            // 
            // btnUserCaptureImage
            // 
            this.btnUserCaptureImage.Location = new System.Drawing.Point(134, 521);
            this.btnUserCaptureImage.Name = "btnUserCaptureImage";
            this.btnUserCaptureImage.Size = new System.Drawing.Size(123, 40);
            this.btnUserCaptureImage.TabIndex = 25;
            this.btnUserCaptureImage.Text = "Capture Image";
            this.btnUserCaptureImage.UseVisualStyleBackColor = true;
            this.btnUserCaptureImage.Click += new System.EventHandler(this.btnUserCaptureImage_Click);
            // 
            // pbxUserImage
            // 
            this.pbxUserImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxUserImage.Location = new System.Drawing.Point(19, 21);
            this.pbxUserImage.Name = "pbxUserImage";
            this.pbxUserImage.Size = new System.Drawing.Size(640, 480);
            this.pbxUserImage.TabIndex = 24;
            this.pbxUserImage.TabStop = false;
            // 
            // btnPersonImagesDelete
            // 
            this.btnPersonImagesDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPersonImagesDelete.ForeColor = System.Drawing.Color.Red;
            this.btnPersonImagesDelete.Location = new System.Drawing.Point(889, 534);
            this.btnPersonImagesDelete.Name = "btnPersonImagesDelete";
            this.btnPersonImagesDelete.Size = new System.Drawing.Size(50, 26);
            this.btnPersonImagesDelete.TabIndex = 23;
            this.btnPersonImagesDelete.Text = "X";
            this.btnPersonImagesDelete.UseVisualStyleBackColor = true;
            // 
            // btnPersonImagesForward
            // 
            this.btnPersonImagesForward.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPersonImagesForward.Location = new System.Drawing.Point(833, 534);
            this.btnPersonImagesForward.Name = "btnPersonImagesForward";
            this.btnPersonImagesForward.Size = new System.Drawing.Size(50, 26);
            this.btnPersonImagesForward.TabIndex = 22;
            this.btnPersonImagesForward.Text = ">";
            this.btnPersonImagesForward.UseVisualStyleBackColor = true;
            this.btnPersonImagesForward.Click += new System.EventHandler(this.btnPersonImagesForward_Click);
            // 
            // btnPersonImagesBack
            // 
            this.btnPersonImagesBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPersonImagesBack.Location = new System.Drawing.Point(777, 534);
            this.btnPersonImagesBack.Name = "btnPersonImagesBack";
            this.btnPersonImagesBack.Size = new System.Drawing.Size(50, 26);
            this.btnPersonImagesBack.TabIndex = 21;
            this.btnPersonImagesBack.Text = "<";
            this.btnPersonImagesBack.UseVisualStyleBackColor = true;
            this.btnPersonImagesBack.Click += new System.EventHandler(this.btnPersonImagesBack_Click);
            // 
            // lblChooseMode
            // 
            this.lblChooseMode.AutoSize = true;
            this.lblChooseMode.Location = new System.Drawing.Point(856, 306);
            this.lblChooseMode.Name = "lblChooseMode";
            this.lblChooseMode.Size = new System.Drawing.Size(104, 13);
            this.lblChooseMode.TabIndex = 20;
            this.lblChooseMode.Text = "Select CRUD Mode:";
            // 
            // cboSelectCRUDMode
            // 
            this.cboSelectCRUDMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectCRUDMode.FormattingEnabled = true;
            this.cboSelectCRUDMode.Items.AddRange(new object[] {
            "Create New User",
            "Edit Existing User"});
            this.cboSelectCRUDMode.Location = new System.Drawing.Point(966, 302);
            this.cboSelectCRUDMode.Name = "cboSelectCRUDMode";
            this.cboSelectCRUDMode.Size = new System.Drawing.Size(121, 21);
            this.cboSelectCRUDMode.TabIndex = 19;
            this.cboSelectCRUDMode.SelectedIndexChanged += new System.EventHandler(this.cboSelectCRUDMode_SelectedIndexChanged);
            // 
            // pbxPersonFacialImages
            // 
            this.pbxPersonFacialImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxPersonFacialImages.Location = new System.Drawing.Point(774, 336);
            this.pbxPersonFacialImages.Name = "pbxPersonFacialImages";
            this.pbxPersonFacialImages.Size = new System.Drawing.Size(168, 192);
            this.pbxPersonFacialImages.TabIndex = 17;
            this.pbxPersonFacialImages.TabStop = false;
            // 
            // btnSavePersonToDatabase
            // 
            this.btnSavePersonToDatabase.Location = new System.Drawing.Point(1125, 422);
            this.btnSavePersonToDatabase.Name = "btnSavePersonToDatabase";
            this.btnSavePersonToDatabase.Size = new System.Drawing.Size(123, 40);
            this.btnSavePersonToDatabase.TabIndex = 9;
            this.btnSavePersonToDatabase.Text = "Save Person";
            this.btnSavePersonToDatabase.UseVisualStyleBackColor = true;
            this.btnSavePersonToDatabase.Click += new System.EventHandler(this.btnSavePersonToDatabase_Click);
            // 
            // txtPersonID
            // 
            this.txtPersonID.Enabled = false;
            this.txtPersonID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPersonID.Location = new System.Drawing.Point(1050, 345);
            this.txtPersonID.Name = "txtPersonID";
            this.txtPersonID.Size = new System.Drawing.Size(198, 20);
            this.txtPersonID.TabIndex = 8;
            this.txtPersonID.Text = "auto-assigned";
            this.txtPersonID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNewPersonID
            // 
            this.lblNewPersonID.AutoSize = true;
            this.lblNewPersonID.Location = new System.Drawing.Point(985, 348);
            this.lblNewPersonID.Name = "lblNewPersonID";
            this.lblNewPersonID.Size = new System.Drawing.Size(47, 13);
            this.lblNewPersonID.TabIndex = 7;
            this.lblNewPersonID.Text = "Identifier";
            // 
            // txtPersonSurname
            // 
            this.txtPersonSurname.Location = new System.Drawing.Point(1050, 396);
            this.txtPersonSurname.Name = "txtPersonSurname";
            this.txtPersonSurname.Size = new System.Drawing.Size(198, 20);
            this.txtPersonSurname.TabIndex = 6;
            // 
            // txtPersonForename
            // 
            this.txtPersonForename.Location = new System.Drawing.Point(1050, 370);
            this.txtPersonForename.Name = "txtPersonForename";
            this.txtPersonForename.Size = new System.Drawing.Size(198, 20);
            this.txtPersonForename.TabIndex = 5;
            // 
            // lblNewPersonSurname
            // 
            this.lblNewPersonSurname.AutoSize = true;
            this.lblNewPersonSurname.Location = new System.Drawing.Point(985, 399);
            this.lblNewPersonSurname.Name = "lblNewPersonSurname";
            this.lblNewPersonSurname.Size = new System.Drawing.Size(49, 13);
            this.lblNewPersonSurname.TabIndex = 4;
            this.lblNewPersonSurname.Text = "Surname";
            // 
            // lblNewPersonForename
            // 
            this.lblNewPersonForename.AutoSize = true;
            this.lblNewPersonForename.Location = new System.Drawing.Point(985, 373);
            this.lblNewPersonForename.Name = "lblNewPersonForename";
            this.lblNewPersonForename.Size = new System.Drawing.Size(54, 13);
            this.lblNewPersonForename.TabIndex = 3;
            this.lblNewPersonForename.Text = "Forename";
            // 
            // grdUsers
            // 
            this.grdUsers.AllowUserToAddRows = false;
            this.grdUsers.AllowUserToDeleteRows = false;
            this.grdUsers.AllowUserToOrderColumns = true;
            this.grdUsers.AllowUserToResizeRows = false;
            this.grdUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdentifier,
            this.colForename,
            this.colSurname,
            this.colImageCount});
            this.grdUsers.Location = new System.Drawing.Point(723, 21);
            this.grdUsers.MultiSelect = false;
            this.grdUsers.Name = "grdUsers";
            this.grdUsers.ReadOnly = true;
            this.grdUsers.RowHeadersVisible = false;
            this.grdUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdUsers.ShowEditingIcon = false;
            this.grdUsers.Size = new System.Drawing.Size(603, 249);
            this.grdUsers.TabIndex = 2;
            this.grdUsers.SelectionChanged += new System.EventHandler(this.grdUsers_SelectionChanged);
            // 
            // colIdentifier
            // 
            this.colIdentifier.DataPropertyName = "Id";
            this.colIdentifier.HeaderText = "Identifier";
            this.colIdentifier.Name = "colIdentifier";
            this.colIdentifier.ReadOnly = true;
            // 
            // colForename
            // 
            this.colForename.DataPropertyName = "Forename";
            this.colForename.HeaderText = "Forename";
            this.colForename.Name = "colForename";
            this.colForename.ReadOnly = true;
            // 
            // colSurname
            // 
            this.colSurname.DataPropertyName = "Surname";
            this.colSurname.HeaderText = "Surname";
            this.colSurname.Name = "colSurname";
            this.colSurname.ReadOnly = true;
            // 
            // colImageCount
            // 
            this.colImageCount.DataPropertyName = "Images";
            this.colImageCount.HeaderText = "Image Count";
            this.colImageCount.Name = "colImageCount";
            this.colImageCount.ReadOnly = true;
            // 
            // tabStreams
            // 
            this.tabStreams.BackColor = System.Drawing.SystemColors.Control;
            this.tabStreams.Controls.Add(this.grpColour);
            this.tabStreams.Controls.Add(this.grpControls);
            this.tabStreams.Controls.Add(this.grpDepth);
            this.tabStreams.Location = new System.Drawing.Point(4, 22);
            this.tabStreams.Name = "tabStreams";
            this.tabStreams.Padding = new System.Windows.Forms.Padding(3);
            this.tabStreams.Size = new System.Drawing.Size(1363, 613);
            this.tabStreams.TabIndex = 0;
            this.tabStreams.Text = "Camera Streams";
            // 
            // tabConfiguration
            // 
            this.tabConfiguration.BackColor = System.Drawing.SystemColors.Control;
            this.tabConfiguration.Controls.Add(this.groupBox1);
            this.tabConfiguration.Controls.Add(this.grpRecogniserControls);
            this.tabConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tabConfiguration.Name = "tabConfiguration";
            this.tabConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfiguration.Size = new System.Drawing.Size(1363, 613);
            this.tabConfiguration.TabIndex = 2;
            this.tabConfiguration.Text = "Configuration";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCouchDatabaseName);
            this.groupBox1.Controls.Add(this.lblCouchDatabaseName);
            this.groupBox1.Controls.Add(this.btnSaveSettings);
            this.groupBox1.Controls.Add(this.txtCouchDBPort);
            this.groupBox1.Controls.Add(this.lblCouchDBPort);
            this.groupBox1.Controls.Add(this.txtCouchDBHost);
            this.groupBox1.Controls.Add(this.lblCouchDBHost);
            this.groupBox1.Controls.Add(this.txtRedisPort);
            this.groupBox1.Controls.Add(this.lblRedisPort);
            this.groupBox1.Controls.Add(this.txtRedisHost);
            this.groupBox1.Controls.Add(this.lblRedisHost);
            this.groupBox1.Location = new System.Drawing.Point(394, 232);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 268);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // txtCouchDatabaseName
            // 
            this.txtCouchDatabaseName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtCouchDatabaseName.Location = new System.Drawing.Point(136, 98);
            this.txtCouchDatabaseName.Name = "txtCouchDatabaseName";
            this.txtCouchDatabaseName.Size = new System.Drawing.Size(235, 20);
            this.txtCouchDatabaseName.TabIndex = 9;
            // 
            // lblCouchDatabaseName
            // 
            this.lblCouchDatabaseName.AutoSize = true;
            this.lblCouchDatabaseName.Location = new System.Drawing.Point(9, 101);
            this.lblCouchDatabaseName.Name = "lblCouchDatabaseName";
            this.lblCouchDatabaseName.Size = new System.Drawing.Size(121, 13);
            this.lblCouchDatabaseName.TabIndex = 8;
            this.lblCouchDatabaseName.Text = "Couch Database Name:";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(242, 211);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(129, 41);
            this.btnSaveSettings.TabIndex = 3;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // txtCouchDBPort
            // 
            this.txtCouchDBPort.Location = new System.Drawing.Point(136, 65);
            this.txtCouchDBPort.Name = "txtCouchDBPort";
            this.txtCouchDBPort.Size = new System.Drawing.Size(235, 20);
            this.txtCouchDBPort.TabIndex = 7;
            // 
            // lblCouchDBPort
            // 
            this.lblCouchDBPort.AutoSize = true;
            this.lblCouchDBPort.Location = new System.Drawing.Point(52, 68);
            this.lblCouchDBPort.Name = "lblCouchDBPort";
            this.lblCouchDBPort.Size = new System.Drawing.Size(78, 13);
            this.lblCouchDBPort.TabIndex = 6;
            this.lblCouchDBPort.Text = "CouchDB Port:";
            // 
            // txtCouchDBHost
            // 
            this.txtCouchDBHost.Location = new System.Drawing.Point(136, 32);
            this.txtCouchDBHost.Name = "txtCouchDBHost";
            this.txtCouchDBHost.Size = new System.Drawing.Size(235, 20);
            this.txtCouchDBHost.TabIndex = 5;
            // 
            // lblCouchDBHost
            // 
            this.lblCouchDBHost.AutoSize = true;
            this.lblCouchDBHost.Location = new System.Drawing.Point(49, 35);
            this.lblCouchDBHost.Name = "lblCouchDBHost";
            this.lblCouchDBHost.Size = new System.Drawing.Size(81, 13);
            this.lblCouchDBHost.TabIndex = 4;
            this.lblCouchDBHost.Text = "CouchDB Host:";
            // 
            // txtRedisPort
            // 
            this.txtRedisPort.Location = new System.Drawing.Point(136, 167);
            this.txtRedisPort.Name = "txtRedisPort";
            this.txtRedisPort.Size = new System.Drawing.Size(235, 20);
            this.txtRedisPort.TabIndex = 3;
            // 
            // lblRedisPort
            // 
            this.lblRedisPort.AutoSize = true;
            this.lblRedisPort.Location = new System.Drawing.Point(71, 170);
            this.lblRedisPort.Name = "lblRedisPort";
            this.lblRedisPort.Size = new System.Drawing.Size(59, 13);
            this.lblRedisPort.TabIndex = 2;
            this.lblRedisPort.Text = "Redis Port:";
            // 
            // txtRedisHost
            // 
            this.txtRedisHost.Location = new System.Drawing.Point(136, 134);
            this.txtRedisHost.Name = "txtRedisHost";
            this.txtRedisHost.Size = new System.Drawing.Size(235, 20);
            this.txtRedisHost.TabIndex = 1;
            // 
            // lblRedisHost
            // 
            this.lblRedisHost.AutoSize = true;
            this.lblRedisHost.Location = new System.Drawing.Point(71, 137);
            this.lblRedisHost.Name = "lblRedisHost";
            this.lblRedisHost.Size = new System.Drawing.Size(62, 13);
            this.lblRedisHost.TabIndex = 0;
            this.lblRedisHost.Text = "Redis Host:";
            // 
            // grpRecogniserControls
            // 
            this.grpRecogniserControls.Controls.Add(this.btnRetrainRecogniser);
            this.grpRecogniserControls.Controls.Add(this.btnReloadRecogniserData);
            this.grpRecogniserControls.Controls.Add(this.btnPersistRecogniserData);
            this.grpRecogniserControls.Location = new System.Drawing.Point(394, 6);
            this.grpRecogniserControls.Name = "grpRecogniserControls";
            this.grpRecogniserControls.Size = new System.Drawing.Size(474, 199);
            this.grpRecogniserControls.TabIndex = 3;
            this.grpRecogniserControls.TabStop = false;
            this.grpRecogniserControls.Text = "Recogniser Controls";
            // 
            // btnRetrainRecogniser
            // 
            this.btnRetrainRecogniser.Location = new System.Drawing.Point(36, 33);
            this.btnRetrainRecogniser.Name = "btnRetrainRecogniser";
            this.btnRetrainRecogniser.Size = new System.Drawing.Size(129, 41);
            this.btnRetrainRecogniser.TabIndex = 0;
            this.btnRetrainRecogniser.Text = "Retrain Recogniser";
            this.btnRetrainRecogniser.UseVisualStyleBackColor = true;
            this.btnRetrainRecogniser.Click += new System.EventHandler(this.btnRetrainRecogniser_Click);
            // 
            // btnReloadRecogniserData
            // 
            this.btnReloadRecogniserData.Location = new System.Drawing.Point(306, 33);
            this.btnReloadRecogniserData.Name = "btnReloadRecogniserData";
            this.btnReloadRecogniserData.Size = new System.Drawing.Size(129, 41);
            this.btnReloadRecogniserData.TabIndex = 2;
            this.btnReloadRecogniserData.Text = "Reload Recogniser Data";
            this.btnReloadRecogniserData.UseVisualStyleBackColor = true;
            this.btnReloadRecogniserData.Click += new System.EventHandler(this.btnReloadRecogniserData_Click);
            // 
            // btnPersistRecogniserData
            // 
            this.btnPersistRecogniserData.Location = new System.Drawing.Point(171, 33);
            this.btnPersistRecogniserData.Name = "btnPersistRecogniserData";
            this.btnPersistRecogniserData.Size = new System.Drawing.Size(129, 41);
            this.btnPersistRecogniserData.TabIndex = 1;
            this.btnPersistRecogniserData.Text = "Persist Recogniser";
            this.btnPersistRecogniserData.UseVisualStyleBackColor = true;
            this.btnPersistRecogniserData.Click += new System.EventHandler(this.btnPersistRecogniserData_Click);
            // 
            // frmFacialRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1394, 663);
            this.Controls.Add(this.tabMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1410, 701);
            this.MinimumSize = new System.Drawing.Size(1410, 701);
            this.Name = "frmFacialRecognition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facial Recognition System";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDept)).EndInit();
            this.grpControls.ResumeLayout(false);
            this.grpColour.ResumeLayout(false);
            this.grpDepth.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabFacialRec.ResumeLayout(false);
            this.tabFacialRec.PerformLayout();
            this.grpRecognitionResult.ResumeLayout(false);
            this.grpRecognitionResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSourceFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNormalisedFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCapturedColorImage)).EndInit();
            this.tabDatabase.ResumeLayout(false);
            this.tabDatabase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUserImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPersonFacialImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).EndInit();
            this.tabStreams.ResumeLayout(false);
            this.tabConfiguration.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpRecogniserControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxImage;
        private System.Windows.Forms.Button btnIncrElevation;
        private System.Windows.Forms.Button btnDecrElevation;
        private System.Windows.Forms.PictureBox pbxDept;
        private System.Windows.Forms.GroupBox grpControls;
        private System.Windows.Forms.GroupBox grpColour;
        private System.Windows.Forms.GroupBox grpDepth;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabStreams;
        private System.Windows.Forms.TabPage tabFacialRec;
        private System.Windows.Forms.Button btnPerformFacialRec;
        private System.Windows.Forms.Button btnFacialDetection;
        private System.Windows.Forms.Button btnCaptureFrame;
        private System.Windows.Forms.TabPage tabConfiguration;
        private System.Windows.Forms.Button btnRetrainRecogniser;
        private System.Windows.Forms.Button btnSaveFrameData;
        private System.Windows.Forms.PictureBox pbxCapturedColorImage;
        private System.Windows.Forms.PictureBox pbxNormalisedFace;
        private System.Windows.Forms.Button btnNormalise;
        private System.Windows.Forms.Label lblDetectedFaces;
        private System.Windows.Forms.PictureBox pbxSourceFace;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.OpenFileDialog diaOpenFile;
        private System.Windows.Forms.TabPage tabDatabase;
        private System.Windows.Forms.DataGridView grdUsers;
        private System.Windows.Forms.TextBox txtPersonSurname;
        private System.Windows.Forms.TextBox txtPersonForename;
        private System.Windows.Forms.Label lblNewPersonSurname;
        private System.Windows.Forms.Label lblNewPersonForename;
        private System.Windows.Forms.TextBox txtPersonID;
        private System.Windows.Forms.Label lblNewPersonID;
        private System.Windows.Forms.Button btnSavePersonToDatabase;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdentifier;
        private System.Windows.Forms.DataGridViewTextBoxColumn colForename;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSurname;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImageCount;
        private System.Windows.Forms.Label lblChooseMode;
        private System.Windows.Forms.ComboBox cboSelectCRUDMode;
        private System.Windows.Forms.PictureBox pbxPersonFacialImages;
        private System.Windows.Forms.Button btnPersonImagesDelete;
        private System.Windows.Forms.Button btnPersonImagesForward;
        private System.Windows.Forms.Button btnPersonImagesBack;
        private System.Windows.Forms.Button btnUserDetectFacialImage;
        private System.Windows.Forms.Button btnUserCaptureImage;
        private System.Windows.Forms.PictureBox pbxUserImage;
        private System.Windows.Forms.Button btnUserAddFace;
        private System.Windows.Forms.Button btnCaptureFrames;
        private System.Windows.Forms.GroupBox grpRecogniserControls;
        private System.Windows.Forms.Button btnReloadRecogniserData;
        private System.Windows.Forms.Button btnPersistRecogniserData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.TextBox txtCouchDBPort;
        private System.Windows.Forms.Label lblCouchDBPort;
        private System.Windows.Forms.TextBox txtCouchDBHost;
        private System.Windows.Forms.Label lblCouchDBHost;
        private System.Windows.Forms.TextBox txtRedisPort;
        private System.Windows.Forms.Label lblRedisPort;
        private System.Windows.Forms.TextBox txtRedisHost;
        private System.Windows.Forms.Label lblRedisHost;
        private System.Windows.Forms.TextBox txtCouchDatabaseName;
        private System.Windows.Forms.Label lblCouchDatabaseName;
        private System.Windows.Forms.GroupBox grpRecognitionResult;
        private System.Windows.Forms.TextBox txtRecognisedForename;
        private System.Windows.Forms.TextBox txtRecognisedSurname;
        private System.Windows.Forms.TextBox txtRecognisedID;
        private System.Windows.Forms.Label lblRecognisedForename;
        private System.Windows.Forms.Label lblRecognisedSurname;
        private System.Windows.Forms.Label lblRecognisedPersonID;
        private System.Windows.Forms.Button btnViewRecognitionResultInDatabase;
    }
}

