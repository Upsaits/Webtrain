
using Wisej.Web;

namespace WebtrainWebPortal.Forms
{
    partial class FrmStartSickness
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

        #region Wisej Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStartSickness));
            this.dateSicknessStart = new Wisej.Web.DateTimePicker();
            this.dateSicknessEnd = new Wisej.Web.DateTimePicker();
            this.radSickStartToday = new Wisej.Web.RadioButton();
            this.radSickTimeSpan = new Wisej.Web.RadioButton();
            this.btnOk = new Wisej.Web.Button();
            this.btnCancel = new Wisej.Web.Button();
            this.SuspendLayout();
            // 
            // dateSicknessStart
            // 
            this.dateSicknessStart.Checked = false;
            this.dateSicknessStart.FirstDayOfWeek = Wisej.Web.Day.Monday;
            this.dateSicknessStart.Format = Wisej.Web.DateTimePickerFormat.Custom;
            this.dateSicknessStart.InvalidMessage = "Falsches Datum!";
            this.dateSicknessStart.LabelText = "von";
            this.dateSicknessStart.Location = new System.Drawing.Point(38, 48);
            this.dateSicknessStart.Mask = "99.99.9999";
            this.dateSicknessStart.MaxDate = new System.DateTime(2031, 1, 27, 0, 0, 0, 0);
            this.dateSicknessStart.MinDate = new System.DateTime(2021, 1, 27, 0, 0, 0, 0);
            this.dateSicknessStart.Name = "dateSicknessStart";
            this.dateSicknessStart.SelectOnEnter = true;
            this.dateSicknessStart.Size = new System.Drawing.Size(214, 42);
            this.dateSicknessStart.TabIndex = 5;
            this.dateSicknessStart.Value = new System.DateTime(2021, 1, 27, 0, 0, 0, 0);
            this.dateSicknessStart.ValueChanged += new System.EventHandler(this.dateSicknessStart_ValueChanged);
            // 
            // dateSicknessEnd
            // 
            this.dateSicknessEnd.Checked = false;
            this.dateSicknessEnd.Format = Wisej.Web.DateTimePickerFormat.Custom;
            this.dateSicknessEnd.InvalidMessage = "Falsches Datum!";
            this.dateSicknessEnd.LabelText = "bis";
            this.dateSicknessEnd.Location = new System.Drawing.Point(38, 104);
            this.dateSicknessEnd.Mask = "99.99.9999";
            this.dateSicknessEnd.MaxDate = new System.DateTime(2031, 1, 27, 0, 0, 0, 0);
            this.dateSicknessEnd.MinDate = new System.DateTime(2021, 1, 27, 0, 0, 0, 0);
            this.dateSicknessEnd.Name = "dateSicknessEnd";
            this.dateSicknessEnd.SelectOnEnter = true;
            this.dateSicknessEnd.Size = new System.Drawing.Size(214, 42);
            this.dateSicknessEnd.TabIndex = 7;
            this.dateSicknessEnd.Value = new System.DateTime(2021, 1, 27, 0, 0, 0, 0);
            this.dateSicknessEnd.ValueChanged += new System.EventHandler(this.dateSicknessEnd_ValueChanged);
            // 
            // radSickStartToday
            // 
            this.radSickStartToday.Location = new System.Drawing.Point(18, 162);
            this.radSickStartToday.Name = "radSickStartToday";
            this.radSickStartToday.Size = new System.Drawing.Size(86, 22);
            this.radSickStartToday.TabIndex = 6;
            this.radSickStartToday.TabStop = true;
            this.radSickStartToday.Text = "Ab Heute";
            this.radSickStartToday.CheckedChanged += new System.EventHandler(this.radSickStartToday_CheckedChanged);
            // 
            // radSickTimeSpan
            // 
            this.radSickTimeSpan.Location = new System.Drawing.Point(15, 20);
            this.radSickTimeSpan.Name = "radSickTimeSpan";
            this.radSickTimeSpan.Size = new System.Drawing.Size(137, 22);
            this.radSickTimeSpan.TabIndex = 4;
            this.radSickTimeSpan.TabStop = true;
            this.radSickTimeSpan.Text = "Zeitraum bekannt:";
            this.radSickTimeSpan.CheckedChanged += new System.EventHandler(this.radSickTimeSpan_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = Wisej.Web.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(18, 203);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 27);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = Wisej.Web.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(152, 203);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 27);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmStartSickness
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(270, 247);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dateSicknessStart);
            this.Controls.Add(this.dateSicknessEnd);
            this.Controls.Add(this.radSickStartToday);
            this.Controls.Add(this.radSickTimeSpan);
            this.Icon = ((System.Drawing.Image)(resources.GetObject("$this.Icon")));
            this.KeepCentered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStartSickness";
            this.ShowInTaskbar = false;
            this.StartPosition = Wisej.Web.FormStartPosition.CenterScreen;
            this.Text = "Krankmeldung";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Wisej.Web.DateTimePicker dateSicknessStart;
        private Wisej.Web.DateTimePicker dateSicknessEnd;
        private Wisej.Web.RadioButton radSickStartToday;
        private Wisej.Web.RadioButton radSickTimeSpan;
        private Wisej.Web.Button btnOk;
        private Wisej.Web.Button btnCancel;
    }
}