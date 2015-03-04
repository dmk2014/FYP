﻿namespace FacialRecognition
{
    partial class frmFacialRecPrototype
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
            this.btnSaveFrameData = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStartSensor = new System.Windows.Forms.Button();
            this.grpColour = new System.Windows.Forms.GroupBox();
            this.grpDepth = new System.Windows.Forms.GroupBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabStreams = new System.Windows.Forms.TabPage();
            this.tabFacialRec = new System.Windows.Forms.TabPage();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.pbxSourceFace = new System.Windows.Forms.PictureBox();
            this.lblDetectedFaces = new System.Windows.Forms.Label();
            this.btnNormalise = new System.Windows.Forms.Button();
            this.pbxNormalisedFace = new System.Windows.Forms.PictureBox();
            this.pbxCapturedColorImage = new System.Windows.Forms.PictureBox();
            this.btnPerformFacialRec = new System.Windows.Forms.Button();
            this.btnFacialDetection = new System.Windows.Forms.Button();
            this.btnCaptureFrame = new System.Windows.Forms.Button();
            this.tabPayment = new System.Windows.Forms.TabPage();
            this.btnProcessPayment = new System.Windows.Forms.Button();
            this.diaOpenFile = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDept)).BeginInit();
            this.grpControls.SuspendLayout();
            this.grpColour.SuspendLayout();
            this.grpDepth.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabStreams.SuspendLayout();
            this.tabFacialRec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSourceFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNormalisedFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCapturedColorImage)).BeginInit();
            this.tabPayment.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxImage
            // 
            this.pbxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxImage.Location = new System.Drawing.Point(6, 19);
            this.pbxImage.Name = "pbxImage";
            this.pbxImage.Size = new System.Drawing.Size(527, 294);
            this.pbxImage.TabIndex = 0;
            this.pbxImage.TabStop = false;
            // 
            // btnIncrElevation
            // 
            this.btnIncrElevation.Enabled = false;
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
            this.btnDecrElevation.Enabled = false;
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
            this.pbxDept.Location = new System.Drawing.Point(6, 19);
            this.pbxDept.Name = "pbxDept";
            this.pbxDept.Size = new System.Drawing.Size(527, 294);
            this.pbxDept.TabIndex = 3;
            this.pbxDept.TabStop = false;
            // 
            // grpControls
            // 
            this.grpControls.Controls.Add(this.btnSaveFrameData);
            this.grpControls.Controls.Add(this.btnStop);
            this.grpControls.Controls.Add(this.btnStartSensor);
            this.grpControls.Controls.Add(this.btnIncrElevation);
            this.grpControls.Controls.Add(this.btnDecrElevation);
            this.grpControls.Location = new System.Drawing.Point(260, 342);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new System.Drawing.Size(593, 73);
            this.grpControls.TabIndex = 4;
            this.grpControls.TabStop = false;
            this.grpControls.Text = "Camera Controls";
            // 
            // btnSaveFrameData
            // 
            this.btnSaveFrameData.Enabled = false;
            this.btnSaveFrameData.Location = new System.Drawing.Point(122, 19);
            this.btnSaveFrameData.Name = "btnSaveFrameData";
            this.btnSaveFrameData.Size = new System.Drawing.Size(110, 36);
            this.btnSaveFrameData.TabIndex = 7;
            this.btnSaveFrameData.Text = "Save Raw Frame Data";
            this.btnSaveFrameData.UseVisualStyleBackColor = true;
            this.btnSaveFrameData.Click += new System.EventHandler(this.btnSaveFrameData_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(470, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(110, 36);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop Sensor";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStartSensor
            // 
            this.btnStartSensor.Location = new System.Drawing.Point(6, 19);
            this.btnStartSensor.Name = "btnStartSensor";
            this.btnStartSensor.Size = new System.Drawing.Size(110, 36);
            this.btnStartSensor.TabIndex = 3;
            this.btnStartSensor.Text = "Start Sensor";
            this.btnStartSensor.UseVisualStyleBackColor = true;
            this.btnStartSensor.Click += new System.EventHandler(this.btnStartSensor_Click);
            // 
            // grpColour
            // 
            this.grpColour.Controls.Add(this.pbxImage);
            this.grpColour.Location = new System.Drawing.Point(6, 6);
            this.grpColour.Name = "grpColour";
            this.grpColour.Size = new System.Drawing.Size(543, 318);
            this.grpColour.TabIndex = 5;
            this.grpColour.TabStop = false;
            this.grpColour.Text = "Colour Stream";
            // 
            // grpDepth
            // 
            this.grpDepth.Controls.Add(this.pbxDept);
            this.grpDepth.Location = new System.Drawing.Point(563, 6);
            this.grpDepth.Name = "grpDepth";
            this.grpDepth.Size = new System.Drawing.Size(543, 318);
            this.grpDepth.TabIndex = 6;
            this.grpDepth.TabStop = false;
            this.grpDepth.Text = "Depth Stream";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabFacialRec);
            this.tabMain.Controls.Add(this.tabStreams);
            this.tabMain.Controls.Add(this.tabPayment);
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1120, 472);
            this.tabMain.TabIndex = 7;
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
            this.tabStreams.Size = new System.Drawing.Size(1112, 446);
            this.tabStreams.TabIndex = 0;
            this.tabStreams.Text = "Camera Streams";
            // 
            // tabFacialRec
            // 
            this.tabFacialRec.BackColor = System.Drawing.SystemColors.Control;
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
            this.tabFacialRec.Size = new System.Drawing.Size(1112, 446);
            this.tabFacialRec.TabIndex = 1;
            this.tabFacialRec.Text = "Facial Recognition";
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(268, 16);
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
            this.pbxSourceFace.Location = new System.Drawing.Point(614, 146);
            this.pbxSourceFace.MaximumSize = new System.Drawing.Size(168, 192);
            this.pbxSourceFace.Name = "pbxSourceFace";
            this.pbxSourceFace.Size = new System.Drawing.Size(168, 192);
            this.pbxSourceFace.TabIndex = 7;
            this.pbxSourceFace.TabStop = false;
            // 
            // lblDetectedFaces
            // 
            this.lblDetectedFaces.AutoSize = true;
            this.lblDetectedFaces.Location = new System.Drawing.Point(611, 89);
            this.lblDetectedFaces.Name = "lblDetectedFaces";
            this.lblDetectedFaces.Size = new System.Drawing.Size(95, 13);
            this.lblDetectedFaces.TabIndex = 6;
            this.lblDetectedFaces.Text = "Detected Faces: 0";
            // 
            // btnNormalise
            // 
            this.btnNormalise.Enabled = false;
            this.btnNormalise.Location = new System.Drawing.Point(767, 16);
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
            this.pbxNormalisedFace.Location = new System.Drawing.Point(869, 146);
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
            this.pbxCapturedColorImage.Size = new System.Drawing.Size(527, 294);
            this.pbxCapturedColorImage.TabIndex = 3;
            this.pbxCapturedColorImage.TabStop = false;
            // 
            // btnPerformFacialRec
            // 
            this.btnPerformFacialRec.Enabled = false;
            this.btnPerformFacialRec.Location = new System.Drawing.Point(896, 16);
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
            this.btnFacialDetection.Location = new System.Drawing.Point(638, 16);
            this.btnFacialDetection.Name = "btnFacialDetection";
            this.btnFacialDetection.Size = new System.Drawing.Size(123, 40);
            this.btnFacialDetection.TabIndex = 1;
            this.btnFacialDetection.Text = "Perform Facial Detection";
            this.btnFacialDetection.UseVisualStyleBackColor = true;
            this.btnFacialDetection.Click += new System.EventHandler(this.btnFacialDetection_Click);
            // 
            // btnCaptureFrame
            // 
            this.btnCaptureFrame.Location = new System.Drawing.Point(139, 16);
            this.btnCaptureFrame.Name = "btnCaptureFrame";
            this.btnCaptureFrame.Size = new System.Drawing.Size(123, 40);
            this.btnCaptureFrame.TabIndex = 0;
            this.btnCaptureFrame.Text = "Capture Frame";
            this.btnCaptureFrame.UseVisualStyleBackColor = true;
            this.btnCaptureFrame.Click += new System.EventHandler(this.btnCaptureFrame_Click);
            // 
            // tabPayment
            // 
            this.tabPayment.BackColor = System.Drawing.SystemColors.Control;
            this.tabPayment.Controls.Add(this.btnProcessPayment);
            this.tabPayment.Location = new System.Drawing.Point(4, 22);
            this.tabPayment.Name = "tabPayment";
            this.tabPayment.Padding = new System.Windows.Forms.Padding(3);
            this.tabPayment.Size = new System.Drawing.Size(1112, 446);
            this.tabPayment.TabIndex = 2;
            this.tabPayment.Text = "Process Payment";
            // 
            // btnProcessPayment
            // 
            this.btnProcessPayment.Enabled = false;
            this.btnProcessPayment.Location = new System.Drawing.Point(492, 98);
            this.btnProcessPayment.Name = "btnProcessPayment";
            this.btnProcessPayment.Size = new System.Drawing.Size(129, 41);
            this.btnProcessPayment.TabIndex = 0;
            this.btnProcessPayment.Text = "Process Payment";
            this.btnProcessPayment.UseVisualStyleBackColor = true;
            // 
            // frmFacialRecPrototype
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 496);
            this.Controls.Add(this.tabMain);
            this.Name = "frmFacialRecPrototype";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facial Recognition System";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDept)).EndInit();
            this.grpControls.ResumeLayout(false);
            this.grpColour.ResumeLayout(false);
            this.grpDepth.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabStreams.ResumeLayout(false);
            this.tabFacialRec.ResumeLayout(false);
            this.tabFacialRec.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSourceFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxNormalisedFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCapturedColorImage)).EndInit();
            this.tabPayment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxImage;
        private System.Windows.Forms.Button btnIncrElevation;
        private System.Windows.Forms.Button btnDecrElevation;
        private System.Windows.Forms.PictureBox pbxDept;
        private System.Windows.Forms.GroupBox grpControls;
        private System.Windows.Forms.Button btnStartSensor;
        private System.Windows.Forms.GroupBox grpColour;
        private System.Windows.Forms.GroupBox grpDepth;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabStreams;
        private System.Windows.Forms.TabPage tabFacialRec;
        private System.Windows.Forms.Button btnPerformFacialRec;
        private System.Windows.Forms.Button btnFacialDetection;
        private System.Windows.Forms.Button btnCaptureFrame;
        private System.Windows.Forms.TabPage tabPayment;
        private System.Windows.Forms.Button btnProcessPayment;
        private System.Windows.Forms.Button btnSaveFrameData;
        private System.Windows.Forms.PictureBox pbxCapturedColorImage;
        private System.Windows.Forms.PictureBox pbxNormalisedFace;
        private System.Windows.Forms.Button btnNormalise;
        private System.Windows.Forms.Label lblDetectedFaces;
        private System.Windows.Forms.PictureBox pbxSourceFace;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.OpenFileDialog diaOpenFile;
    }
}

