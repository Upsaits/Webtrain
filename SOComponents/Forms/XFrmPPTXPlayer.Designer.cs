
namespace Forms
{
    partial class XFrmPPTXPlayer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.transparentFrameControl1 = new SoftObject.SOComponents.Controls.TransparentFrameControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 354);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 53);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // transparentFrameControl1
            // 
            this.transparentFrameControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.transparentFrameControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transparentFrameControl1.Location = new System.Drawing.Point(0, 0);
            this.transparentFrameControl1.Name = "transparentFrameControl1";
            this.transparentFrameControl1.Opacity = 100;
            this.transparentFrameControl1.Size = new System.Drawing.Size(598, 354);
            this.transparentFrameControl1.TabIndex = 1;
            this.transparentFrameControl1.FrameChanged += new SoftObject.SOComponents.TransparentFrameEventDelegate(this.transparentFrameControl1_FrameChanged);
            // 
            // XFrmPPTXPlayer
            // 
            this.ClientSize = new System.Drawing.Size(598, 407);
            this.ControlBox = false;
            this.Controls.Add(this.transparentFrameControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XFrmPPTXPlayer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.XFrmPPTXPlayer_FormClosed);
            this.Load += new System.EventHandler(this.XFrmPPTXPlayer_Load);
            this.Move += new System.EventHandler(this.XFrmPPTXPlayer_Move);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private SoftObject.SOComponents.Controls.TransparentFrameControl transparentFrameControl1;
    }
}