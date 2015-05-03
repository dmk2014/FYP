namespace FacialRecognition.Forms
{
    partial class frmWaitWindow
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
            this.lblFacialRecognition = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFacialRecognition
            // 
            this.lblFacialRecognition.AutoSize = true;
            this.lblFacialRecognition.Enabled = false;
            this.lblFacialRecognition.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacialRecognition.Location = new System.Drawing.Point(64, 101);
            this.lblFacialRecognition.Name = "lblFacialRecognition";
            this.lblFacialRecognition.Size = new System.Drawing.Size(429, 65);
            this.lblFacialRecognition.TabIndex = 0;
            this.lblFacialRecognition.Text = "Facial Recognition";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Enabled = false;
            this.lblMessage.Font = new System.Drawing.Font("Segoe WP", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(15, 189);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(527, 42);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "A long running operation is in progress - please wait.\r\nThe main window will beco" +
    "me available once the operation has completed.";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmWaitWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(556, 332);
            this.ControlBox = false;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblFacialRecognition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWaitWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facial Recognition System";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmWaitWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmWaitWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmWaitWindow_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFacialRecognition;
        private System.Windows.Forms.Label lblMessage;
    }
}