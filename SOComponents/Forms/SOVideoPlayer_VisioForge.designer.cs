using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.IO;
using System.Resources;
using SoftObject.UtilityLibrary;
using SoftObject.SOComponents.Controls;
using VisioForge.Controls.UI.WinForms;
using VisioForge.Types;

namespace SoftObject.SOComponents.Forms
{
    public partial class SOVideoPlayer_VisioForge
    {
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Die verwendeten Ressourcen bereinigen.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SOVideoPlayer_VisioForge));
            VisioForge.Types.VideoRendererSettingsWinForms videoRendererSettingsWinForms1 = new VisioForge.Types.VideoRendererSettingsWinForms();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.mediaPlayer1 = new VisioForge.Controls.UI.WinForms.MediaPlayer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.slider1 = new DevComponents.DotNetBar.Controls.Slider();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPause = new DevComponents.DotNetBar.ButtonX();
            this.btnPlay = new DevComponents.DotNetBar.ButtonX();
            this.btnStop = new DevComponents.DotNetBar.ButtonX();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mediaPlayer1);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(504, 294);
            this.panel1.TabIndex = 12;
            // 
            // mediaPlayer1
            // 
            this.mediaPlayer1.Audio_Channel_Mapper = null;
            this.mediaPlayer1.Audio_Effects_Enabled = false;
            this.mediaPlayer1.Audio_Effects_UseLegacyEffects = false;
            this.mediaPlayer1.Audio_Enhancer_Enabled = false;
            this.mediaPlayer1.Audio_OutputDevice = "";
            this.mediaPlayer1.Audio_PlayAudio = false;
            this.mediaPlayer1.Audio_Sample_Grabber_Enabled = false;
            this.mediaPlayer1.Audio_VUMeter_Enabled = false;
            this.mediaPlayer1.Audio_VUMeter_Pro_Enabled = false;
            this.mediaPlayer1.Audio_VUMeter_Pro_Volume = 0;
            this.mediaPlayer1.AutoSize = true;
            this.mediaPlayer1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mediaPlayer1.BackColor = System.Drawing.Color.Black;
            this.mediaPlayer1.Barcode_Reader_Enabled = false;
            this.mediaPlayer1.Barcode_Reader_Type = VisioForge.Types.VFBarcodeType.Auto;
            this.mediaPlayer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mediaPlayer1.ChromaKey = null;
            this.mediaPlayer1.Custom_Audio_Decoder = null;
            this.mediaPlayer1.Custom_Splitter = null;
            this.mediaPlayer1.Custom_Video_Decoder = null;
            this.mediaPlayer1.CustomRedist_Enabled = false;
            this.mediaPlayer1.CustomRedist_Path = null;
            this.mediaPlayer1.Debug_DeepCleanUp = false;
            this.mediaPlayer1.Debug_Dir = null;
            this.mediaPlayer1.Debug_Mode = false;
            this.mediaPlayer1.Debug_Telemetry = true;
            this.mediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaPlayer1.Encryption_Key = "";
            this.mediaPlayer1.Encryption_KeyType = VisioForge.Types.VFEncryptionKeyType.String;
            this.mediaPlayer1.Face_Tracking = null;
            this.mediaPlayer1.FilenamesOrURL = ((System.Collections.Generic.List<string>)(resources.GetObject("mediaPlayer1.FilenamesOrURL")));
            this.mediaPlayer1.Info_UseLibMediaInfo = false;
            this.mediaPlayer1.Location = new System.Drawing.Point(0, 0);
            this.mediaPlayer1.Loop = false;
            this.mediaPlayer1.Loop_DoNotSeekToBeginning = false;
            this.mediaPlayer1.MaximalSpeedPlayback = false;
            this.mediaPlayer1.Motion_Detection = null;
            this.mediaPlayer1.Motion_DetectionEx = null;
            this.mediaPlayer1.MultiScreen_Enabled = false;
            this.mediaPlayer1.Name = "mediaPlayer1";
            this.mediaPlayer1.Play_DelayEnabled = false;
            this.mediaPlayer1.Play_PauseAtFirstFrame = false;
            this.mediaPlayer1.ReversePlayback_CacheSize = 0;
            this.mediaPlayer1.ReversePlayback_Enabled = false;
            this.mediaPlayer1.Selection_Active = false;
            this.mediaPlayer1.Selection_Start = 0;
            this.mediaPlayer1.Selection_Stop = 0;
            this.mediaPlayer1.Size = new System.Drawing.Size(504, 260);
            this.mediaPlayer1.Source_Custom_CLSID = null;
            this.mediaPlayer1.Source_Mode = VisioForge.Types.VFMediaPlayerSource.File_DS;
            this.mediaPlayer1.Source_Stream = null;
            this.mediaPlayer1.Source_Stream_AudioPresent = true;
            this.mediaPlayer1.Source_Stream_Size = ((long)(0));
            this.mediaPlayer1.Source_Stream_VideoPresent = true;
            this.mediaPlayer1.TabIndex = 0;
            this.mediaPlayer1.Video_Effects_Enabled = false;
            videoRendererSettingsWinForms1.Aspect_Ratio_Override = false;
            videoRendererSettingsWinForms1.Aspect_Ratio_X = 0;
            videoRendererSettingsWinForms1.Aspect_Ratio_Y = 0;
            videoRendererSettingsWinForms1.BackgroundColor = System.Drawing.Color.Black;
            videoRendererSettingsWinForms1.Deinterlace_EVR_Mode = VisioForge.Types.EVRDeinterlaceMode.Auto;
            videoRendererSettingsWinForms1.Deinterlace_VMR9_Mode = null;
            videoRendererSettingsWinForms1.Deinterlace_VMR9_UseDefault = true;
            videoRendererSettingsWinForms1.Flip_Horizontal = false;
            videoRendererSettingsWinForms1.Flip_Vertical = false;
            videoRendererSettingsWinForms1.RotationAngle = 0;
            videoRendererSettingsWinForms1.StretchMode = VisioForge.Types.VFVideoRendererStretchMode.Letterbox;
            videoRendererSettingsWinForms1.Video_Renderer = VisioForge.Types.VFVideoRenderer.EVR;
            videoRendererSettingsWinForms1.VideoRendererInternal = VisioForge.Types.VFVideoRendererInternal.EVR;
            videoRendererSettingsWinForms1.Zoom_Ratio = 0;
            videoRendererSettingsWinForms1.Zoom_ShiftX = 0;
            videoRendererSettingsWinForms1.Zoom_ShiftY = 0;
            this.mediaPlayer1.Video_Renderer = videoRendererSettingsWinForms1;
            this.mediaPlayer1.Video_Sample_Grabber_UseForVideoEffects = true;
            this.mediaPlayer1.Video_Stream_Index = 0;
            this.mediaPlayer1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mediaPlayer1_MouseDoubleClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 260);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(504, 34);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.slider1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(96, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(408, 34);
            this.panel3.TabIndex = 14;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(96, 34);
            this.textBox1.TabIndex = 13;
            // 
            // slider1
            // 
            // 
            // 
            // 
            this.slider1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider1.Dock = System.Windows.Forms.DockStyle.Right;
            this.slider1.Location = new System.Drawing.Point(96, 0);
            this.slider1.Name = "slider1";
            this.slider1.Size = new System.Drawing.Size(312, 34);
            this.slider1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider1.TabIndex = 12;
            this.slider1.Value = 0;
            this.slider1.ValueChanged += new System.EventHandler(this.slider1_ValueChanged);
            this.slider1.IncreaseButtonClick += new System.EventHandler(this.slider1_IncreaseButtonClick);
            this.slider1.DecreaseButtonClick += new System.EventHandler(this.slider1_DecreaseButtonClick);
            // 
            // panel2
            // 
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnPause);
            this.panel2.Controls.Add(this.btnPlay);
            this.panel2.Controls.Add(this.btnStop);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(96, 34);
            this.panel2.TabIndex = 13;
            // 
            // btnPause
            // 
            this.btnPause.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPause.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPause.Location = new System.Drawing.Point(65, 4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(24, 24);
            this.btnPause.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPause.TabIndex = 2;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click_1);
            // 
            // btnPlay
            // 
            this.btnPlay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPlay.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPlay.Location = new System.Drawing.Point(35, 4);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(24, 24);
            this.btnPlay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click_1);
            // 
            // btnStop
            // 
            this.btnStop.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStop.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStop.Location = new System.Drawing.Point(4, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(24, 24);
            this.btnStop.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStop.TabIndex = 0;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click_1);
            // 
            // SOVideoPlayer_VisioForge
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(504, 294);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Office 2010 Blue";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MinimumSize = new System.Drawing.Size(349, 329);
            this.Name = "SOVideoPlayer_VisioForge";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebTrain - Video Player";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SOVideoPlayer_VisioForge_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.Controls.Slider slider1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private MediaPlayer mediaPlayer1;
        private Panel panel4;
        private DevComponents.DotNetBar.ButtonX btnPause;
        private DevComponents.DotNetBar.ButtonX btnPlay;
        private DevComponents.DotNetBar.ButtonX btnStop;
        private TextBox textBox1;

    }
}