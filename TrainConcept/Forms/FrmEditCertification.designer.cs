namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmEditCertification
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
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditCertification));
            this.txtPerson = new System.Windows.Forms.TextBox();
            this.lblPerson = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblContentTitle = new System.Windows.Forms.Label();
            this.grpSuccessType = new System.Windows.Forms.GroupBox();
            this.rbnWonderfulSuccess = new System.Windows.Forms.RadioButton();
            this.rbnVeryGoodSuccess = new System.Windows.Forms.RadioButton();
            this.rbnGoodSuccess = new System.Windows.Forms.RadioButton();
            this.rbnWithSuccess = new System.Windows.Forms.RadioButton();
            this.rbnNoSuccess = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtTeacher = new System.Windows.Forms.TextBox();
            this.lblTeacher = new System.Windows.Forms.Label();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.lblPlace = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblLogo = new System.Windows.Forms.Label();
            this.btnChooseLogo = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.iedLogo = new DevExpress.XtraEditors.ImageEdit();
            this.grpSuccessType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iedLogo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPerson
            // 
            this.txtPerson.Location = new System.Drawing.Point(179, 43);
            this.txtPerson.Name = "txtPerson";
            this.txtPerson.Size = new System.Drawing.Size(240, 20);
            this.txtPerson.TabIndex = 3;
            // 
            // lblPerson
            // 
            this.lblPerson.Location = new System.Drawing.Point(19, 43);
            this.lblPerson.Name = "lblPerson";
            this.lblPerson.Size = new System.Drawing.Size(144, 16);
            this.lblPerson.TabIndex = 2;
            this.lblPerson.Text = "Auszubildender:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(179, 75);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(240, 20);
            this.txtTitle.TabIndex = 5;
            // 
            // lblContentTitle
            // 
            this.lblContentTitle.Location = new System.Drawing.Point(19, 75);
            this.lblContentTitle.Name = "lblContentTitle";
            this.lblContentTitle.Size = new System.Drawing.Size(144, 16);
            this.lblContentTitle.TabIndex = 4;
            this.lblContentTitle.Text = "Ausbildungstitel:";
            // 
            // grpSuccessType
            // 
            this.grpSuccessType.Controls.Add(this.rbnWonderfulSuccess);
            this.grpSuccessType.Controls.Add(this.rbnVeryGoodSuccess);
            this.grpSuccessType.Controls.Add(this.rbnGoodSuccess);
            this.grpSuccessType.Controls.Add(this.rbnWithSuccess);
            this.grpSuccessType.Controls.Add(this.rbnNoSuccess);
            this.grpSuccessType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpSuccessType.Location = new System.Drawing.Point(19, 203);
            this.grpSuccessType.Name = "grpSuccessType";
            this.grpSuccessType.Size = new System.Drawing.Size(384, 152);
            this.grpSuccessType.TabIndex = 6;
            this.grpSuccessType.TabStop = false;
            this.grpSuccessType.Text = "Beurteilung";
            // 
            // rbnWonderfulSuccess
            // 
            this.rbnWonderfulSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnWonderfulSuccess.Location = new System.Drawing.Point(56, 120);
            this.rbnWonderfulSuccess.Name = "rbnWonderfulSuccess";
            this.rbnWonderfulSuccess.Size = new System.Drawing.Size(304, 24);
            this.rbnWonderfulSuccess.TabIndex = 4;
            this.rbnWonderfulSuccess.Text = "mit Ausgezeichnetem Erfolg";
            // 
            // rbnVeryGoodSuccess
            // 
            this.rbnVeryGoodSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnVeryGoodSuccess.Location = new System.Drawing.Point(56, 96);
            this.rbnVeryGoodSuccess.Name = "rbnVeryGoodSuccess";
            this.rbnVeryGoodSuccess.Size = new System.Drawing.Size(304, 24);
            this.rbnVeryGoodSuccess.TabIndex = 3;
            this.rbnVeryGoodSuccess.Text = "mit Sehr Gutem Erfolg";
            // 
            // rbnGoodSuccess
            // 
            this.rbnGoodSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnGoodSuccess.Location = new System.Drawing.Point(56, 72);
            this.rbnGoodSuccess.Name = "rbnGoodSuccess";
            this.rbnGoodSuccess.Size = new System.Drawing.Size(304, 24);
            this.rbnGoodSuccess.TabIndex = 2;
            this.rbnGoodSuccess.Text = "mit Gutem Erfolg";
            // 
            // rbnWithSuccess
            // 
            this.rbnWithSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnWithSuccess.Location = new System.Drawing.Point(56, 48);
            this.rbnWithSuccess.Name = "rbnWithSuccess";
            this.rbnWithSuccess.Size = new System.Drawing.Size(304, 24);
            this.rbnWithSuccess.TabIndex = 1;
            this.rbnWithSuccess.Text = "mit Erfolg";
            // 
            // rbnNoSuccess
            // 
            this.rbnNoSuccess.Checked = true;
            this.rbnNoSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbnNoSuccess.Location = new System.Drawing.Point(56, 24);
            this.rbnNoSuccess.Name = "rbnNoSuccess";
            this.rbnNoSuccess.Size = new System.Drawing.Size(304, 24);
            this.rbnNoSuccess.TabIndex = 0;
            this.rbnNoSuccess.TabStop = true;
            this.rbnNoSuccess.Text = "ohne Erfolg";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(237, 378);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(118, 378);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtTeacher
            // 
            this.txtTeacher.Location = new System.Drawing.Point(179, 107);
            this.txtTeacher.Name = "txtTeacher";
            this.txtTeacher.Size = new System.Drawing.Size(240, 20);
            this.txtTeacher.TabIndex = 12;
            // 
            // lblTeacher
            // 
            this.lblTeacher.Location = new System.Drawing.Point(19, 107);
            this.lblTeacher.Name = "lblTeacher";
            this.lblTeacher.Size = new System.Drawing.Size(144, 16);
            this.lblTeacher.TabIndex = 11;
            this.lblTeacher.Text = "Ausbilder:";
            // 
            // txtPlace
            // 
            this.txtPlace.Location = new System.Drawing.Point(179, 139);
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Size = new System.Drawing.Size(240, 20);
            this.txtPlace.TabIndex = 14;
            // 
            // lblPlace
            // 
            this.lblPlace.Location = new System.Drawing.Point(19, 139);
            this.lblPlace.Name = "lblPlace";
            this.lblPlace.Size = new System.Drawing.Size(152, 16);
            this.lblPlace.TabIndex = 13;
            this.lblPlace.Text = "Ausbildungsort:";
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(19, 171);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(144, 16);
            this.lblDate.TabIndex = 16;
            this.lblDate.Text = "Datum:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(179, 171);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 17;
            // 
            // lblLogo
            // 
            this.lblLogo.Location = new System.Drawing.Point(20, 14);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(144, 16);
            this.lblLogo.TabIndex = 20;
            this.lblLogo.Text = "FirmenLogo:";
            // 
            // btnChooseLogo
            // 
            this.btnChooseLogo.Location = new System.Drawing.Point(367, 11);
            this.btnChooseLogo.Name = "btnChooseLogo";
            this.btnChooseLogo.Size = new System.Drawing.Size(52, 19);
            this.btnChooseLogo.TabIndex = 21;
            this.btnChooseLogo.Text = "Suchen";
            this.btnChooseLogo.UseVisualStyleBackColor = true;
            this.btnChooseLogo.Click += new System.EventHandler(this.btnChooseLogo_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Fi" +
    "les (*.gif)|*.gif";
            this.openFileDialog1.Title = "Bitte wählen sie ein Firmenlogo";
            // 
            // iedLogo
            // 
            this.iedLogo.EditValue = ((object)(resources.GetObject("iedLogo.EditValue")));
            this.iedLogo.Location = new System.Drawing.Point(180, 11);
            this.iedLogo.Name = "iedLogo";
            this.iedLogo.Properties.ShowMenu = false;
            this.iedLogo.Size = new System.Drawing.Size(180, 20);
            this.iedLogo.TabIndex = 19;
            this.iedLogo.ImageChanged += new System.EventHandler(this.iedLogo_ImageChanged);
            // 
            // FrmEditCertification
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(437, 412);
            this.Controls.Add(this.btnChooseLogo);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.iedLogo);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.txtPlace);
            this.Controls.Add(this.txtTeacher);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtPerson);
            this.Controls.Add(this.lblPlace);
            this.Controls.Add(this.lblTeacher);
            this.Controls.Add(this.grpSuccessType);
            this.Controls.Add(this.lblContentTitle);
            this.Controls.Add(this.lblPerson);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmEditCertification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zertifikat erstellen";
            this.Load += new System.EventHandler(this.FrmEditCertification_Load);
            this.grpSuccessType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iedLogo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.Label lblContentTitle;
        private System.Windows.Forms.GroupBox grpSuccessType;
        private System.Windows.Forms.RadioButton rbnNoSuccess;
        private System.Windows.Forms.RadioButton rbnWithSuccess;
        private System.Windows.Forms.RadioButton rbnVeryGoodSuccess;
        private System.Windows.Forms.RadioButton rbnWonderfulSuccess;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtPerson;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.RadioButton rbnGoodSuccess;
        private System.Windows.Forms.TextBox txtTeacher;
        private System.Windows.Forms.Label lblTeacher;
        private System.Windows.Forms.TextBox txtPlace;
        private System.Windows.Forms.Label lblPlace;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DevExpress.XtraEditors.ImageEdit iedLogo;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnChooseLogo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

    }
}
