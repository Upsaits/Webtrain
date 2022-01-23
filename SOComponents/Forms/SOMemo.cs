using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Drawing.Drawing2D; //für Gradientbrush
using SoftObject.UtilityLibrary;
using DevExpress.XtraBars;
using SoftObject.SOComponents.UtilityLibrary;

namespace SoftObject.SOComponents.Forms
{
	public class SOMemo : System.Windows.Forms.Form
	{
		private int MouseDownX=0;
		private int MouseDownY=0;
		private bool isWindowMoving = false;
		private DevExpress.XtraBars.BarManager barManager1 = new BarManager();
		private DevExpress.XtraBars.BarDockControl barDockControl1 = new BarDockControl();
		private DevExpress.XtraBars.BarDockControl barDockControl2 = new BarDockControl();
        private DevExpress.XtraBars.BarDockControl barDockControl3 = new BarDockControl();
        private DevExpress.XtraBars.BarDockControl barDockControl4 = new BarDockControl();
        private DevExpress.XtraBars.PopupMenu popupMenu1 = new PopupMenu();
        private DevExpress.XtraBars.BarButtonItem PopupItemCut = new BarButtonItem();
        private DevExpress.XtraBars.BarButtonItem PopupItemCopy = new BarButtonItem();
        private DevExpress.XtraBars.BarButtonItem PopupItemPaste = new BarButtonItem();
        private DevExpress.XtraBars.BarButtonItem PopupItemMarkAll = new BarButtonItem();
		private System.Windows.Forms.ImageList imageListMenu;
		private System.ComponentModel.IContainer components;
		private string fileName="";
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private TXTextControl.TextControl textControl1;
		private ResourceHandler rh=null;

		public SOMemo()
		{
			InitializeComponent();
		}

		public SOMemo(string _fileName)
		{
			InitializeComponent();

			string exeFolder=Application.ExecutablePath.Substring(0,Application.ExecutablePath.LastIndexOf('\\'));
			ProfileHandler profHnd = new ProfileHandler(exeFolder+"\\TrainConcept.ini");
			string language=profHnd.GetProfileString("SYSTEM","Language","DE");
			language=language.ToUpper();
			string workingFolder=profHnd.GetProfileString("SYSTEM","WorkingFolder",exeFolder);
			LanguageHandler langHnd= new LanguageHandler(workingFolder,"language",language);
				
			fileName = _fileName;
#if(SKIN_EMCO)
			rh = new ResourceHandler("res.trainconcept.resources_emco",GetType().Assembly);
#elif(SKIN_WEBTRAIN)
			rh = new ResourceHandler("res.webtrain.resources_softobject",GetType().Assembly);
#else
			rh = new ResourceHandler("res.trainconcept.resources_emco",GetType().Assembly);
#endif

			InitPopupMenu();

			this.PopupItemCut.Caption = langHnd.GetText("MENU","Cut","Ausschneiden");
			this.PopupItemCopy.Caption = langHnd.GetText("MENU","Copy","Kopieren");
			this.PopupItemPaste.Caption = langHnd.GetText("MENU","Insert","Einfügen");
			this.PopupItemMarkAll.Caption = langHnd.GetText("MENU","Select_All","Alles Markieren");
			this.button1.Text = langHnd.GetText("FORMS","Ok","OK");
			this.button2.Text = langHnd.GetText("FORMS","Cancel","Abbrechen");
			this.label1.Text = langHnd.GetText("FORMS","Notice","NOTIZ");

			barManager1.SetPopupContextMenu(textControl1, popupMenu1);
			//textControl1.FrameStyle = (short) UtilityLibrary.Win32.BorderFrameFlags.BF_SINGLE;
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		protected override CreateParams CreateParams 
		{
			get 
			{
				CreateParams cp = base.CreateParams;
				cp.Style &= (int)SoftObject.UtilityLibrary.Win32.WindowStyles.WS_CAPTION;
				cp.Style &= (int)SoftObject.UtilityLibrary.Win32.WindowStyles.WS_THICKFRAME;
				cp.Style |= (int)SoftObject.UtilityLibrary.Win32.WindowStyles.WS_BORDER;
				
				return cp;
			}
		}

		private void InitPopupMenu()
		{
			imageListMenu.Images.Add(rh.GetBitmap("CUT"));
			imageListMenu.Images.Add(rh.GetBitmap("COPY"));
			imageListMenu.Images.Add(rh.GetBitmap("PASTE"));
			imageListMenu.Images.Add(rh.GetBitmap("DELETE"));
			imageListMenu.Images.Add(rh.GetBitmap("PRINT"));

			PopupItemCut.ImageIndex = 0;
			PopupItemCopy.ImageIndex = 1;
			PopupItemPaste.ImageIndex = 2;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.imageListMenu = new System.Windows.Forms.ImageList(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textControl1 = new TXTextControl.TextControl();
			this.SuspendLayout();
			// 
			// imageListMenu
			// 
			this.imageListMenu.ImageSize = new System.Drawing.Size(16, 16);
			this.imageListMenu.TransparentColor = System.Drawing.Color.Silver;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(128, 464);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 24);
			this.button1.TabIndex = 6;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(232, 464);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(80, 24);
			this.button2.TabIndex = 7;
			this.button2.Text = "Abbrechen";
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(234)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(432, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "NOTIZ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_MouseUp);
			this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
			this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
			this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
			// 
			// textControl1
			// 
			this.textControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textControl1.Font = new System.Drawing.Font("Arial", 10F);
			this.textControl1.Location = new System.Drawing.Point(0, 16);
			this.textControl1.Name = "textControl1";
			this.textControl1.Size = new System.Drawing.Size(448, 440);
			this.textControl1.TabIndex = 10;
			// 
			// SOMemo
			// 
			this.AllowDrop = true;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(214)), ((System.Byte)(221)), ((System.Byte)(228)));
			this.ClientSize = new System.Drawing.Size(432, 504);
			this.ControlBox = false;
			this.Controls.Add(this.textControl1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.MinimumSize = new System.Drawing.Size(300, 100);
			this.Name = "SOMemo";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		new public void Load()
		{
			textControl1.Load(fileName,TXTextControl.StreamType.RichTextFormat);
		}


		private void PopupItemCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			textControl1.Cut();
		}
		
		private void PopupItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			textControl1.Copy();
		}

		private void PopupItemPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			textControl1.Paste();
		}

		private void PopupItemMarkAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			textControl1.Save(fileName,TXTextControl.StreamType.RichTextFormat);
			Close();
		}

		private void button2_Click_1(object sender, System.EventArgs e)
		{
			Close();
		}

		private void label1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isWindowMoving = true;
				MouseDownX = e.X;
				MouseDownY = e.Y;
			}
		}

		private void label1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( isWindowMoving )
			{
				Point temp = new Point(0,0);

				temp.X = this.Location.X + (e.X - MouseDownX);
				temp.Y = this.Location.Y + (e.Y - MouseDownY);
				this.Location = temp;
			}
		}

		private void label1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				isWindowMoving = false;
		}

		private void label1_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
				this.WindowState = System.Windows.Forms.FormWindowState.Normal;
			else
				this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		}

		private void popupMenu1_Popup(object sender, System.EventArgs e)
		{
			PopupItemCut.Enabled = PopupItemCopy.Enabled = textControl1.CanCopy;
			PopupItemPaste.Enabled = textControl1.CanPaste;
			PopupItemMarkAll.Enabled = textControl1.Text !="";
		}
	}
}