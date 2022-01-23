using System.Windows.Forms;
using SoftObject.SOComponents.Controls;

namespace SoftObject.TrainConcept.Controls
{
    public partial class ContentLearningControl
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
            this.components = new System.ComponentModel.Container();
            this.timerStopwatches = new System.Windows.Forms.Timer(this.components);
            this.m_webBrowser1 = new WebBrowserEx();
            this.SuspendLayout();
            // 
            // timerStopwatches
            // 
            this.timerStopwatches.Interval = 10000;
            this.timerStopwatches.Tick += new System.EventHandler(this.timerStopwatches_Tick);
            // 
            // m_webBrowser1
            // 
            this.m_webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.m_webBrowser1.Name = "m_webBrowser1";
            this.m_webBrowser1.Size = new System.Drawing.Size(790, 590);
            this.m_webBrowser1.TabIndex = 0;
            // 
            // FrmContentLearning
            // 
            //this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(790, 590);
            //this.ControlBox = false;
            this.Controls.Add(this.m_webBrowser1);
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            //this.KeyPreview = true;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(667, 520);
            this.Name = "FrmContentLearning";
            //this.ShowInTaskbar = false;
            this.Text = "FrmContentLearning";
            //this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmContentLearning_FormClosed);
            this.ResumeLayout(false);

        }
        #endregion

        private WebBrowserEx m_webBrowser1;
        private Timer timerStopwatches;

    }
}
