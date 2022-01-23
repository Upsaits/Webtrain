namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmInputIPAdress
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
            this.backgroundWorker1.CancelAsync();
            this.backgroundWorker2.CancelAsync();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInputIPAdress));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtOldServer = new System.Windows.Forms.TextBox();
            this.txtNewServer = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbnManual = new System.Windows.Forms.RadioButton();
            this.rbnAutoServer = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAutoServer = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.circularProgress1 = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(39, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Aktuelle Server-Adresse:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(39, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Neue Server-Adresse:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(51, 221);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(159, 221);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtOldServer
            // 
            this.txtOldServer.Location = new System.Drawing.Point(183, 111);
            this.txtOldServer.Name = "txtOldServer";
            this.txtOldServer.ReadOnly = true;
            this.txtOldServer.Size = new System.Drawing.Size(152, 21);
            this.txtOldServer.TabIndex = 6;
            // 
            // txtNewServer
            // 
            this.txtNewServer.Location = new System.Drawing.Point(183, 144);
            this.txtNewServer.Name = "txtNewServer";
            this.txtNewServer.Size = new System.Drawing.Size(152, 21);
            this.txtNewServer.TabIndex = 7;
            this.txtNewServer.TextChanged += new System.EventHandler(this.txtNewServer_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbnManual);
            this.groupBox1.Controls.Add(this.rbnAutoServer);
            this.groupBox1.Controls.Add(this.txtNewServer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtOldServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 185);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // rbnManual
            // 
            this.rbnManual.AutoSize = true;
            this.rbnManual.Location = new System.Drawing.Point(18, 89);
            this.rbnManual.Name = "rbnManual";
            this.rbnManual.Size = new System.Drawing.Size(61, 17);
            this.rbnManual.TabIndex = 10;
            this.rbnManual.TabStop = true;
            this.rbnManual.Text = "Manuell";
            this.rbnManual.UseVisualStyleBackColor = true;
            this.rbnManual.CheckedChanged += new System.EventHandler(this.rbnManual_CheckedChanged);
            // 
            // rbnAutoServer
            // 
            this.rbnAutoServer.AutoSize = true;
            this.rbnAutoServer.Location = new System.Drawing.Point(18, 22);
            this.rbnAutoServer.Name = "rbnAutoServer";
            this.rbnAutoServer.Size = new System.Drawing.Size(84, 17);
            this.rbnAutoServer.TabIndex = 0;
            this.rbnAutoServer.TabStop = true;
            this.rbnAutoServer.Text = "Automatisch";
            this.rbnAutoServer.UseVisualStyleBackColor = true;
            this.rbnAutoServer.CheckedChanged += new System.EventHandler(this.rbnAutoServer_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(39, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Server-Adresse";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAutoServer
            // 
            this.txtAutoServer.Location = new System.Drawing.Point(172, 61);
            this.txtAutoServer.Name = "txtAutoServer";
            this.txtAutoServer.ReadOnly = true;
            this.txtAutoServer.Size = new System.Drawing.Size(152, 21);
            this.txtAutoServer.TabIndex = 10;
            // 
            // btnCheck
            // 
            this.btnCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCheck.ImageKey = "(none)";
            this.btnCheck.ImageList = this.imageList1;
            this.btnCheck.Location = new System.Drawing.Point(266, 221);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 25);
            this.btnCheck.TabIndex = 11;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "false.bmp");
            this.imageList1.Images.SetKeyName(1, "true.bmp");
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // circularProgress1
            // 
            // 
            // 
            // 
            this.circularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.circularProgress1.Location = new System.Drawing.Point(348, 221);
            this.circularProgress1.Name = "circularProgress1";
            this.circularProgress1.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Donut;
            this.circularProgress1.ProgressColor = System.Drawing.Color.DeepSkyBlue;
            this.circularProgress1.Size = new System.Drawing.Size(51, 25);
            this.circularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.circularProgress1.TabIndex = 12;
            this.circularProgress1.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // FrmInputIPAdress
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(411, 253);
            this.Controls.Add(this.circularProgress1);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.txtAutoServer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Dark Side";
            this.Name = "FrmInputIPAdress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server festlegen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmInputIPAdress_FormClosed);
            this.Load += new System.EventHandler(this.FrmInputIPAdress_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtOldServer;
        private System.Windows.Forms.TextBox txtNewServer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAutoServer;
        private System.Windows.Forms.RadioButton rbnManual;
        private System.Windows.Forms.RadioButton rbnAutoServer;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;

    }
}
