using SoftObject.SOComponents.Controls;

namespace SOComponentsTest
{
    partial class FrmMain : DevExpress.XtraEditors.XtraForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnMemo = new System.Windows.Forms.Button();
            this.btnGlossar = new System.Windows.Forms.Button();
            this.btnVideo = new System.Windows.Forms.Button();
            this.btnAnim = new System.Windows.Forms.Button();
            this.btnFlash = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.soNotice1 = new SoftObject.SOComponents.Controls.SONotice();
            this.soGlossar1 = new SoftObject.SOComponents.Controls.SOGlossar();
            this.transparentFrameControl1 = new SoftObject.SOComponents.Controls.TransparentFrameControl();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnMemo
            // 
            this.btnMemo.Location = new System.Drawing.Point(32, 34);
            this.btnMemo.Name = "btnMemo";
            this.btnMemo.Size = new System.Drawing.Size(96, 35);
            this.btnMemo.TabIndex = 1;
            this.btnMemo.Text = "Memo";
            this.btnMemo.Click += new System.EventHandler(this.btnMemo_Click);
            // 
            // btnGlossar
            // 
            this.btnGlossar.Location = new System.Drawing.Point(152, 34);
            this.btnGlossar.Name = "btnGlossar";
            this.btnGlossar.Size = new System.Drawing.Size(88, 35);
            this.btnGlossar.TabIndex = 3;
            this.btnGlossar.Text = "Glossar";
            this.btnGlossar.Click += new System.EventHandler(this.btnGlossar_Click);
            // 
            // btnVideo
            // 
            this.btnVideo.Location = new System.Drawing.Point(264, 34);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(88, 35);
            this.btnVideo.TabIndex = 5;
            this.btnVideo.Text = "Video";
            this.btnVideo.Click += new System.EventHandler(this.btnVideo_Click);
            // 
            // btnAnim
            // 
            this.btnAnim.Location = new System.Drawing.Point(456, 34);
            this.btnAnim.Name = "btnAnim";
            this.btnAnim.Size = new System.Drawing.Size(88, 35);
            this.btnAnim.TabIndex = 6;
            this.btnAnim.Text = "Powerpoint";
            this.btnAnim.Click += new System.EventHandler(this.btnFlash_Click);
            // 
            // btnFlash
            // 
            this.btnFlash.Location = new System.Drawing.Point(368, 34);
            this.btnFlash.Name = "btnFlash";
            this.btnFlash.Size = new System.Drawing.Size(80, 35);
            this.btnFlash.TabIndex = 7;
            this.btnFlash.Text = "Anim";
            this.btnFlash.Click += new System.EventHandler(this.btnAnim_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "rtf";
            this.openFileDialog1.Filter = "Textdateien (*.txt)|*.txt|RTF-Dateien (*.rtf)|*.rtf|Alle Dateien (*.*)|*.*";
            this.openFileDialog1.Title = "Öffnen";
            // 
            // soNotice1
            // 
            this.soNotice1.Location = new System.Drawing.Point(8, 78);
            this.soNotice1.Name = "soNotice1";
            this.soNotice1.Size = new System.Drawing.Size(150, 161);
            this.soNotice1.TabIndex = 8;
            this.soNotice1.Visible = false;
            // 
            // soGlossar1
            // 
            this.soGlossar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(221)))), ((int)(((byte)(228)))));
            this.soGlossar1.Location = new System.Drawing.Point(24, 146);
            this.soGlossar1.Name = "soGlossar1";
            this.soGlossar1.Size = new System.Drawing.Size(506, 435);
            this.soGlossar1.TabIndex = 4;
            this.soGlossar1.Visible = false;
            this.soGlossar1.OnGlossar += new SoftObject.SOComponents.Controls.GlossarHandler(this.soGlossar1_OnGlossar);
            // 
            // transparentFrameControl1
            // 
            this.transparentFrameControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.transparentFrameControl1.Location = new System.Drawing.Point(32, 75);
            this.transparentFrameControl1.Name = "transparentFrameControl1";
            this.transparentFrameControl1.Opacity = 100;
            this.transparentFrameControl1.Size = new System.Drawing.Size(648, 506);
            this.transparentFrameControl1.TabIndex = 9;
            this.transparentFrameControl1.Visible = false;
            this.transparentFrameControl1.FrameChanged += new SoftObject.SOComponents.TransparentFrameEventDelegate(this.transparentFrameControl1_FrameChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(875, 705);
            this.Controls.Add(this.transparentFrameControl1);
            this.Controls.Add(this.soNotice1);
            this.Controls.Add(this.btnFlash);
            this.Controls.Add(this.btnAnim);
            this.Controls.Add(this.btnVideo);
            this.Controls.Add(this.soGlossar1);
            this.Controls.Add(this.btnGlossar);
            this.Controls.Add(this.btnMemo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "SOComponentsTest";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnGlossar;
        private System.Windows.Forms.Button btnMemo;
        private System.Windows.Forms.Button btnVideo;
        private System.Windows.Forms.Button btnAnim;
        private SoftObject.SOComponents.Controls.SOGlossar soGlossar1;
        private System.Windows.Forms.Button btnFlash;
        private SoftObject.SOComponents.Controls.SONotice soNotice1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private TransparentFrameControl transparentFrameControl1;
    }
}