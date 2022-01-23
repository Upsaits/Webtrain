namespace SoftObject.TrainConcept.Forms
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.richEditControl1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.richEditControl2 = new DevExpress.XtraRichEdit.RichEditControl();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 630);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Vergleichen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(303, 630);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // richEditControl1
            // 
            this.richEditControl1.Location = new System.Drawing.Point(12, 12);
            this.richEditControl1.Name = "richEditControl1";
            this.richEditControl1.Size = new System.Drawing.Size(399, 318);
            this.richEditControl1.TabIndex = 6;
            this.richEditControl1.Text = "richEditControl1";
            // 
            // richEditControl2
            // 
            this.richEditControl2.Location = new System.Drawing.Point(417, 12);
            this.richEditControl2.Name = "richEditControl2";
            this.richEditControl2.Size = new System.Drawing.Size(354, 318);
            this.richEditControl2.TabIndex = 7;
            this.richEditControl2.Text = "richEditControl2";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 335);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(759, 289);
            this.webBrowser1.TabIndex = 8;
            // 
            // Form2
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 665);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.richEditControl2);
            this.Controls.Add(this.richEditControl1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOk;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl1;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl2;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}