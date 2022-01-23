namespace SoftObject.SOComponents.Forms
{
    partial class XFrmLongProcessToastNotificationSmall
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
            this.circProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.lblStatus = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // circProgress
            // 
            // 
            // 
            // 
            this.circProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.circProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.circProgress.FocusCuesEnabled = false;
            this.circProgress.Location = new System.Drawing.Point(113, 0);
            this.circProgress.Name = "circProgress";
            this.circProgress.PieBorderDark = System.Drawing.Color.Blue;
            this.circProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Spoke;
            this.circProgress.ProgressColor = System.Drawing.Color.Blue;
            this.circProgress.Size = new System.Drawing.Size(47, 50);
            this.circProgress.SpokeBorderDark = System.Drawing.Color.Black;
            this.circProgress.SpokeBorderLight = System.Drawing.Color.White;
            this.circProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.circProgress.TabIndex = 5;
            // 
            // lblStatus
            // 
            // 
            // 
            // 
            this.lblStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(113, 50);
            this.lblStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Text";
            this.lblStatus.WordWrap = true;
            // 
            // XFrmLongProcessToastNotificationSmall
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseBorderColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(160, 50);
            this.ControlBox = false;
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.circProgress);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(800, 50);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XFrmLongProcessToastNotificationSmall";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.CircularProgress circProgress;
        private DevComponents.DotNetBar.LabelX lblStatus;
    }
}