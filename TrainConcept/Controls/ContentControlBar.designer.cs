using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept.Controls
{
    public partial class ContentControlBar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnJump = new DevExpress.XtraEditors.SimpleButton();
            this.lBtnImages = new System.Windows.Forms.ImageList(this.components);
            this.btnChoose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSolution = new DevExpress.XtraEditors.SimpleButton();
            this.btnWorkout = new DevExpress.XtraEditors.SimpleButton();
            this.btnForward = new DevExpress.XtraEditors.SimpleButton();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bubbleBar1 = new DevComponents.DotNetBar.BubbleBar();
            this.bubbleBarTab1 = new DevComponents.DotNetBar.BubbleBarTab(this.components);
            this.btnSimulation = new DevExpress.XtraEditors.SimpleButton();
            this.btnAnim = new DevExpress.XtraEditors.SimpleButton();
            this.btnVideo = new DevExpress.XtraEditors.SimpleButton();
            this.btnGrafic = new DevExpress.XtraEditors.SimpleButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bubbleBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnJump);
            this.panel1.Controls.Add(this.btnChoose);
            this.panel1.Controls.Add(this.btnSolution);
            this.panel1.Controls.Add(this.btnWorkout);
            this.panel1.Controls.Add(this.btnForward);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 61);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(310, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 57);
            this.label1.TabIndex = 0;
            this.label1.Text = "10/10";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnJump
            // 
            this.btnJump.ImageOptions.ImageIndex = 5;
            this.btnJump.ImageOptions.ImageList = this.lBtnImages;
            this.btnJump.Location = new System.Drawing.Point(190, 10);
            this.btnJump.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(75, 39);
            this.btnJump.TabIndex = 6;
            this.btnJump.TabStop = false;
            this.btnJump.Text = "Zurück";
            this.btnJump.Visible = false;
            // 
            // lBtnImages
            // 
            this.lBtnImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.lBtnImages.ImageSize = new System.Drawing.Size(32, 32);
            this.lBtnImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnChoose
            // 
            this.btnChoose.ImageOptions.ImageIndex = 4;
            this.btnChoose.ImageOptions.ImageList = this.lBtnImages;
            this.btnChoose.Location = new System.Drawing.Point(26, 10);
            this.btnChoose.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(88, 39);
            this.btnChoose.TabIndex = 5;
            this.btnChoose.TabStop = false;
            this.btnChoose.Text = "Ziehen";
            // 
            // btnSolution
            // 
            this.btnSolution.ImageOptions.ImageIndex = 3;
            this.btnSolution.ImageOptions.ImageList = this.lBtnImages;
            this.btnSolution.Location = new System.Drawing.Point(120, 10);
            this.btnSolution.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSolution.Name = "btnSolution";
            this.btnSolution.Size = new System.Drawing.Size(88, 39);
            this.btnSolution.TabIndex = 4;
            this.btnSolution.TabStop = false;
            this.btnSolution.Text = "Lösung";
            this.btnSolution.Visible = false;
            // 
            // btnWorkout
            // 
            this.btnWorkout.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWorkout.Appearance.Options.UseFont = true;
            this.btnWorkout.ImageOptions.ImageIndex = 2;
            this.btnWorkout.ImageOptions.ImageList = this.lBtnImages;
            this.btnWorkout.Location = new System.Drawing.Point(214, 10);
            this.btnWorkout.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnWorkout.Name = "btnWorkout";
            this.btnWorkout.Size = new System.Drawing.Size(88, 39);
            this.btnWorkout.TabIndex = 3;
            this.btnWorkout.TabStop = false;
            this.btnWorkout.Text = "Ausarbeiten";
            this.btnWorkout.Click += new System.EventHandler(this.btnWorkout_Click);
            // 
            // btnForward
            // 
            this.btnForward.ImageOptions.ImageIndex = 1;
            this.btnForward.ImageOptions.ImageList = this.lBtnImages;
            this.btnForward.Location = new System.Drawing.Point(121, 10);
            this.btnForward.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(88, 39);
            this.btnForward.TabIndex = 2;
            this.btnForward.TabStop = false;
            this.btnForward.Text = "Vor";
            // 
            // btnBack
            // 
            this.btnBack.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnBack.ImageOptions.ImageIndex = 0;
            this.btnBack.ImageOptions.ImageList = this.lBtnImages;
            this.btnBack.Location = new System.Drawing.Point(26, 10);
            this.btnBack.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(88, 39);
            this.btnBack.TabIndex = 1;
            this.btnBack.TabStop = false;
            this.btnBack.Text = "Zurück";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnPrevPage);
            this.panel4.Controls.Add(this.btnNextPage);
            this.panel4.Location = new System.Drawing.Point(269, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(35, 57);
            this.panel4.TabIndex = 12;
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPrevPage.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevPage.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevPage.Location = new System.Drawing.Point(0, 29);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(35, 28);
            this.btnPrevPage.TabIndex = 10;
            this.btnPrevPage.TabStop = false;
            this.btnPrevPage.Text = "<<";
            // 
            // btnNextPage
            // 
            this.btnNextPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNextPage.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextPage.Location = new System.Drawing.Point(0, 0);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(35, 27);
            this.btnNextPage.TabIndex = 9;
            this.btnNextPage.TabStop = false;
            this.btnNextPage.Text = ">>";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.bubbleBar1);
            this.panel2.Controls.Add(this.btnSimulation);
            this.panel2.Controls.Add(this.btnAnim);
            this.panel2.Controls.Add(this.btnVideo);
            this.panel2.Controls.Add(this.btnGrafic);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(444, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(365, 61);
            this.panel2.TabIndex = 1;
            // 
            // bubbleBar1
            // 
            this.bubbleBar1.Alignment = DevComponents.DotNetBar.eBubbleButtonAlignment.Bottom;
            this.bubbleBar1.AntiAlias = true;
            // 
            // 
            // 
            this.bubbleBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.bubbleBar1.ButtonBackAreaStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bubbleBar1.ButtonBackAreaStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.bubbleBar1.ButtonBackAreaStyle.BorderBottomWidth = 1;
            this.bubbleBar1.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.bubbleBar1.ButtonBackAreaStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.bubbleBar1.ButtonBackAreaStyle.BorderLeftWidth = 1;
            this.bubbleBar1.ButtonBackAreaStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.bubbleBar1.ButtonBackAreaStyle.BorderRightWidth = 1;
            this.bubbleBar1.ButtonBackAreaStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.bubbleBar1.ButtonBackAreaStyle.BorderTopWidth = 1;
            this.bubbleBar1.ButtonBackAreaStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.bubbleBar1.ButtonBackAreaStyle.PaddingBottom = 3;
            this.bubbleBar1.ButtonBackAreaStyle.PaddingLeft = 3;
            this.bubbleBar1.ButtonBackAreaStyle.PaddingRight = 3;
            this.bubbleBar1.ButtonBackAreaStyle.PaddingTop = 3;
            this.bubbleBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.bubbleBar1.ImageSizeNormal = new System.Drawing.Size(24, 24);
            this.bubbleBar1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.bubbleBar1.Location = new System.Drawing.Point(281, 0);
            this.bubbleBar1.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight;
            this.bubbleBar1.Name = "bubbleBar1";
            this.bubbleBar1.SelectedTab = this.bubbleBarTab1;
            this.bubbleBar1.SelectedTabColors.BorderColor = System.Drawing.Color.Black;
            this.bubbleBar1.Size = new System.Drawing.Size(80, 57);
            this.bubbleBar1.TabIndex = 11;
            this.bubbleBar1.Tabs.Add(this.bubbleBarTab1);
            this.bubbleBar1.Text = "bubbleBarPdfs";
            this.bubbleBar1.Visible = false;
            // 
            // bubbleBarTab1
            // 
            this.bubbleBarTab1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.bubbleBarTab1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(168)))), ((int)(((byte)(228)))));
            this.bubbleBarTab1.DarkBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bubbleBarTab1.LightBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bubbleBarTab1.Name = "bubbleBarTab1";
            this.bubbleBarTab1.PredefinedColor = DevComponents.DotNetBar.eTabItemColor.Blue;
            this.bubbleBarTab1.Text = "Doc\'s";
            this.bubbleBarTab1.TextColor = System.Drawing.Color.Black;
            // 
            // btnSimulation
            // 
            this.btnSimulation.ImageOptions.ImageIndex = 9;
            this.btnSimulation.ImageOptions.ImageList = this.lBtnImages;
            this.btnSimulation.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnSimulation.Location = new System.Drawing.Point(187, 10);
            this.btnSimulation.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSimulation.Name = "btnSimulation";
            this.btnSimulation.Size = new System.Drawing.Size(88, 39);
            this.btnSimulation.TabIndex = 10;
            this.btnSimulation.TabStop = false;
            this.btnSimulation.Text = "Simulation";
            // 
            // btnAnim
            // 
            this.btnAnim.ImageOptions.ImageIndex = 8;
            this.btnAnim.ImageOptions.ImageList = this.lBtnImages;
            this.btnAnim.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnAnim.Location = new System.Drawing.Point(96, 10);
            this.btnAnim.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnAnim.Name = "btnAnim";
            this.btnAnim.Size = new System.Drawing.Size(88, 39);
            this.btnAnim.TabIndex = 9;
            this.btnAnim.TabStop = false;
            this.btnAnim.Text = "Video";
            // 
            // btnVideo
            // 
            this.btnVideo.ImageOptions.ImageIndex = 7;
            this.btnVideo.ImageOptions.ImageList = this.lBtnImages;
            this.btnVideo.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnVideo.Location = new System.Drawing.Point(96, 10);
            this.btnVideo.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(88, 39);
            this.btnVideo.TabIndex = 8;
            this.btnVideo.TabStop = false;
            this.btnVideo.Text = "Video";
            // 
            // btnGrafic
            // 
            this.btnGrafic.ImageOptions.ImageIndex = 6;
            this.btnGrafic.ImageOptions.ImageList = this.lBtnImages;
            this.btnGrafic.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnGrafic.Location = new System.Drawing.Point(5, 10);
            this.btnGrafic.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnGrafic.Name = "btnGrafic";
            this.btnGrafic.Size = new System.Drawing.Size(88, 39);
            this.btnGrafic.TabIndex = 7;
            this.btnGrafic.TabStop = false;
            this.btnGrafic.Text = "Grafik";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Location = new System.Drawing.Point(373, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(151, 60);
            this.panel3.TabIndex = 2;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(55, 60);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(55, 60);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList3
            // 
            this.imageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList3.ImageSize = new System.Drawing.Size(55, 60);
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ContentControlBar
            // 
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "ContentControlBar";
            this.Size = new System.Drawing.Size(809, 61);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bubbleBar1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList lBtnImages;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.SimpleButton btnForward;
        private DevExpress.XtraEditors.SimpleButton btnWorkout;
        private DevExpress.XtraEditors.SimpleButton btnSolution;
        private DevExpress.XtraEditors.SimpleButton btnChoose;
        private DevExpress.XtraEditors.SimpleButton btnJump;
        private DevExpress.XtraEditors.SimpleButton btnGrafic;
        private DevExpress.XtraEditors.SimpleButton btnVideo;
        private DevExpress.XtraEditors.SimpleButton btnAnim;
        private DevExpress.XtraEditors.SimpleButton btnSimulation;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPrevPage;
        private DevComponents.DotNetBar.BubbleBar bubbleBar1;
        private DevComponents.DotNetBar.BubbleBarTab bubbleBarTab1;
    }
}
