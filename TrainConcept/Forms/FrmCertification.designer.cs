namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmCertification
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCertification));
            this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            this.link1 = new DevExpress.XtraPrinting.Link(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPerson = new System.Windows.Forms.Label();
            this.lblVisitedText = new System.Windows.Forms.Label();
            this.lblContentTitle = new System.Windows.Forms.Label();
            this.lblSuccess = new System.Windows.Forms.Label();
            this.lblTeacher = new System.Windows.Forms.Label();
            this.lblPlace = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblPlace1 = new System.Windows.Forms.Label();
            this.lblDate1 = new System.Windows.Forms.Label();
            this.lblTeacher1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).BeginInit();
            this.SuspendLayout();
            // 
            // printingSystem1
            // 
            this.printingSystem1.Links.AddRange(new object[] {
            this.link1});
            // 
            // link1
            // 
            this.link1.PrintingSystemBase = this.printingSystem1;
            this.link1.CreateMarginalHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.link1_CreatePageHeaderArea);
            this.link1.CreateMarginalFooterArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.link1_CreatePageFooterArea);
            this.link1.CreateDetailHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.link1_CreateDetailHeaderArea);
            this.link1.CreateDetailArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.link1_CreateDetailArea);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(10)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(56, 60);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(464, 52);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "ZERTIFIKAT";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPerson
            // 
            this.lblPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerson.Location = new System.Drawing.Point(104, 146);
            this.lblPerson.Name = "lblPerson";
            this.lblPerson.Size = new System.Drawing.Size(368, 44);
            this.lblPerson.TabIndex = 2;
            this.lblPerson.Text = "Hr. Franz Mair";
            this.lblPerson.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVisitedText
            // 
            this.lblVisitedText.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVisitedText.Location = new System.Drawing.Point(40, 224);
            this.lblVisitedText.Name = "lblVisitedText";
            this.lblVisitedText.Size = new System.Drawing.Size(504, 43);
            this.lblVisitedText.TabIndex = 3;
            this.lblVisitedText.Text = "besuchte das Ausbildungsseminar";
            this.lblVisitedText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblContentTitle
            // 
            this.lblContentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContentTitle.Location = new System.Drawing.Point(24, 293);
            this.lblContentTitle.Name = "lblContentTitle";
            this.lblContentTitle.Size = new System.Drawing.Size(536, 77);
            this.lblContentTitle.TabIndex = 4;
            this.lblContentTitle.Text = "Grundlagen der Programmierung";
            this.lblContentTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSuccess
            // 
            this.lblSuccess.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuccess.Location = new System.Drawing.Point(96, 388);
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(392, 43);
            this.lblSuccess.TabIndex = 5;
            this.lblSuccess.Text = "mit ausgezeichnetem Erfolg";
            this.lblSuccess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTeacher
            // 
            this.lblTeacher.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeacher.Location = new System.Drawing.Point(224, 448);
            this.lblTeacher.Name = "lblTeacher";
            this.lblTeacher.Size = new System.Drawing.Size(144, 25);
            this.lblTeacher.TabIndex = 6;
            this.lblTeacher.Text = "Hr. Ing. Zerz Leopold";
            // 
            // lblPlace
            // 
            this.lblPlace.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlace.Location = new System.Drawing.Point(224, 474);
            this.lblPlace.Name = "lblPlace";
            this.lblPlace.Size = new System.Drawing.Size(144, 25);
            this.lblPlace.TabIndex = 7;
            this.lblPlace.Text = "Hallein";
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(224, 500);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(312, 24);
            this.lblDate.TabIndex = 8;
            this.lblDate.Text = "12.September 2003";
            // 
            // lblPlace1
            // 
            this.lblPlace1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlace1.Location = new System.Drawing.Point(16, 474);
            this.lblPlace1.Name = "lblPlace1";
            this.lblPlace1.Size = new System.Drawing.Size(144, 25);
            this.lblPlace1.TabIndex = 10;
            this.lblPlace1.Text = "Ausbildunsort:";
            // 
            // lblDate1
            // 
            this.lblDate1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate1.Location = new System.Drawing.Point(16, 500);
            this.lblDate1.Name = "lblDate1";
            this.lblDate1.Size = new System.Drawing.Size(144, 24);
            this.lblDate1.TabIndex = 11;
            this.lblDate1.Text = "Datum:";
            // 
            // lblTeacher1
            // 
            this.lblTeacher1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeacher1.Location = new System.Drawing.Point(16, 448);
            this.lblTeacher1.Name = "lblTeacher1";
            this.lblTeacher1.Size = new System.Drawing.Size(144, 25);
            this.lblTeacher1.TabIndex = 9;
            this.lblTeacher1.Text = "Ausbildungsleiter:";
            // 
            // FrmCertification
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(584, 506);
            this.Controls.Add(this.lblDate1);
            this.Controls.Add(this.lblPlace1);
            this.Controls.Add(this.lblTeacher1);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblPlace);
            this.Controls.Add(this.lblTeacher);
            this.Controls.Add(this.lblSuccess);
            this.Controls.Add(this.lblContentTitle);
            this.Controls.Add(this.lblVisitedText);
            this.Controls.Add(this.lblPerson);
            this.Controls.Add(this.lblTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Dark Side";
            this.Name = "FrmCertification";
            this.ShowInTaskbar = false;
            this.Text = "FrmCertification";
            this.Activated += new System.EventHandler(this.FrmCertification_Activated);
            this.Load += new System.EventHandler(this.FrmCertification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.XtraPrinting.PrintingSystem printingSystem1;
        private DevExpress.XtraPrinting.Link link1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.Label lblVisitedText;
        private System.Windows.Forms.Label lblContentTitle;
        private System.Windows.Forms.Label lblSuccess;
        private System.Windows.Forms.Label lblTeacher;
        private System.Windows.Forms.Label lblPlace;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblPlace1;
        private System.Windows.Forms.Label lblDate1;
        private System.Windows.Forms.Label lblTeacher1;
    }
}
