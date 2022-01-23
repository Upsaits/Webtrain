namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmTestReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTestReport));
            this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            this.link1 = new DevExpress.XtraPrinting.Link(this.components);
            this.lblResult = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblLearnmap = new System.Windows.Forms.Label();
            this.lblQuestionTitle = new System.Windows.Forms.Label();
            this.lblResultTitle = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblPercWrong = new System.Windows.Forms.Label();
            this.lblPercRight = new System.Windows.Forms.Label();
            this.lblHeaderLine = new System.Windows.Forms.Label();
            this.lblHeaderLinePage = new System.Windows.Forms.Label();
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
            this.link1.CreateDetailHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.link1_CreateDetailHeaderArea);
            this.link1.CreateDetailFooterArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.link1_CreateDetailFooterArea);
            this.link1.CreateDetailArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(this.link1_CreateDetailArea);
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(10)))), ((int)(((byte)(0)))));
            this.lblResult.Location = new System.Drawing.Point(48, 43);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(472, 43);
            this.lblResult.TabIndex = 0;
            this.lblResult.Text = "TESTERGEBNIS";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(24, 164);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(144, 24);
            this.lblTime.TabIndex = 14;
            this.lblTime.Text = "Uhrzeit:";
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(24, 138);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(144, 25);
            this.lblDate.TabIndex = 13;
            this.lblDate.Text = "Datum:";
            // 
            // lblUser
            // 
            this.lblUser.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(24, 112);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(144, 25);
            this.lblUser.TabIndex = 12;
            this.lblUser.Text = "Benutzer:";
            // 
            // lblLearnmap
            // 
            this.lblLearnmap.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLearnmap.Location = new System.Drawing.Point(24, 190);
            this.lblLearnmap.Name = "lblLearnmap";
            this.lblLearnmap.Size = new System.Drawing.Size(144, 24);
            this.lblLearnmap.TabIndex = 15;
            this.lblLearnmap.Text = "Lernmappe:";
            // 
            // lblQuestionTitle
            // 
            this.lblQuestionTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionTitle.Location = new System.Drawing.Point(32, 233);
            this.lblQuestionTitle.Name = "lblQuestionTitle";
            this.lblQuestionTitle.Size = new System.Drawing.Size(312, 24);
            this.lblQuestionTitle.TabIndex = 16;
            this.lblQuestionTitle.Text = "Frage";
            // 
            // lblResultTitle
            // 
            this.lblResultTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultTitle.Location = new System.Drawing.Point(352, 233);
            this.lblResultTitle.Name = "lblResultTitle";
            this.lblResultTitle.Size = new System.Drawing.Size(104, 24);
            this.lblResultTitle.TabIndex = 17;
            this.lblResultTitle.Text = "Ergebnis";
            // 
            // lblQuestion
            // 
            this.lblQuestion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(32, 258);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(312, 25);
            this.lblQuestion.TabIndex = 18;
            this.lblQuestion.Text = "Was ist eine Frage..";
            // 
            // lblPercWrong
            // 
            this.lblPercWrong.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercWrong.Location = new System.Drawing.Point(32, 388);
            this.lblPercWrong.Name = "lblPercWrong";
            this.lblPercWrong.Size = new System.Drawing.Size(144, 24);
            this.lblPercWrong.TabIndex = 20;
            this.lblPercWrong.Text = "%-Falsch:";
            // 
            // lblPercRight
            // 
            this.lblPercRight.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercRight.Location = new System.Drawing.Point(32, 362);
            this.lblPercRight.Name = "lblPercRight";
            this.lblPercRight.Size = new System.Drawing.Size(144, 25);
            this.lblPercRight.TabIndex = 19;
            this.lblPercRight.Text = "%-Richtig:";
            // 
            // lblHeaderLine
            // 
            this.lblHeaderLine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderLine.Location = new System.Drawing.Point(16, 0);
            this.lblHeaderLine.Name = "lblHeaderLine";
            this.lblHeaderLine.Size = new System.Drawing.Size(176, 17);
            this.lblHeaderLine.TabIndex = 21;
            this.lblHeaderLine.Text = "WebTrain";
            // 
            // lblHeaderLinePage
            // 
            this.lblHeaderLinePage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderLinePage.Location = new System.Drawing.Point(352, 0);
            this.lblHeaderLinePage.Name = "lblHeaderLinePage";
            this.lblHeaderLinePage.Size = new System.Drawing.Size(176, 17);
            this.lblHeaderLinePage.TabIndex = 22;
            this.lblHeaderLinePage.Text = "Seite {0}/{1}";
            // 
            // FrmTestReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(568, 482);
            this.Controls.Add(this.lblHeaderLinePage);
            this.Controls.Add(this.lblHeaderLine);
            this.Controls.Add(this.lblPercWrong);
            this.Controls.Add(this.lblPercRight);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.lblResultTitle);
            this.Controls.Add(this.lblQuestionTitle);
            this.Controls.Add(this.lblLearnmap);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Dark Side";
            this.Name = "FrmTestReport";
            this.Text = "FrmTestReport";
            this.Activated += new System.EventHandler(this.FrmTestReport_Activated);
            this.Load += new System.EventHandler(this.FrmTestReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.XtraPrinting.PrintingSystem printingSystem1;
        private DevExpress.XtraPrinting.Link link1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblLearnmap;
        private System.Windows.Forms.Label lblQuestionTitle;
        private System.Windows.Forms.Label lblResultTitle;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label lblPercWrong;
        private System.Windows.Forms.Label lblPercRight;
        private System.Windows.Forms.Label lblHeaderLine;
        private System.Windows.Forms.Label lblHeaderLinePage;

    }
}
