//Code by Robert Pohl Nov. 2003
//robert@2cool2care.com
//Cred to: called odah and Parvez Ahmad Hakim.
//They have written the code about getting icons and drive information with the Interop service. 

using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using System.Runtime.InteropServices;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.UtilityLibrary.Win32;

namespace SoftObject.SOComponents.Forms
{
	public class FrmDirectoryPicker : XtraForm
	{
		private bool isLocked = false;
		public string SelectedDirectory = "";

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		public FrmDirectoryPicker()
		{
			InitializeComponent();

			string exeFolder=Application.ExecutablePath.Substring(0,Application.ExecutablePath.LastIndexOf('\\'));
			ProfileHandler profHnd = new ProfileHandler(exeFolder+"\\TrainConcept.ini");
			string language=profHnd.GetProfileString("SYSTEM","Language","DE");
			language=language.ToUpper();
			string workingFolder=profHnd.GetProfileString("SYSTEM","WorkingFolder",exeFolder);
			LanguageHandler langHnd= new LanguageHandler(workingFolder,"language",language);

			this.btnOk.Text = langHnd.GetText("FORMS","Ok","OK");
			this.btnCancel.Text = langHnd.GetText("FORMS","Cancel","Abbrechen");
			this.Text = langHnd.GetText("FORMS","Choose_Directory","Verzeichnis wählen");

			CreateFolderIcon();
			CreateDrives();
		}


		private void CreateDrives()
		{
			//Generate list of system drives
			string[] drives = Directory.GetLogicalDrives();
			string _name ="";
			TreeNode tmpNode;

			//Loop through system drives and get name and path.
			//Name is the TreeNode's Text and the path is in the Tag property.
			foreach (string s in drives)
			{
				_name = GetDriveName(s);
				if(getDriveType(s)==4) _name="Network Drive";
				if(getDriveType(s)==5) _name="CD-ROM/DVD";

				imageList1.Images.Add(GetIcon(s));
				tmpNode = new TreeNode(_name+" ("+s+")",imageList1.Images.Count-1,imageList1.Images.Count-1);
				tmpNode.Tag = s;
				//Since A: and CD-ROM is not always readable we have to try n catch it.
				if(this.getDriveType(s)!=5&&this.getDriveType(s)!=2)
				{
					try
					{
						HasSubDirs(tmpNode);
					}
					catch{}
				}
				this.treeView1.Nodes.Add(tmpNode);
			}
		}

		//Looks for the first dir in the first HD, and takes that icon
		private void CreateFolderIcon()
		{
			string[] drives = Directory.GetLogicalDrives();
			int i=0;
			for(i=0;i<drives.Length;i++)
			{
				//look for the first harddrive
				if(getDriveType(drives[i])==3)
				{
					break;
				}
			}
			string[] dir = Directory.GetDirectories(drives[i]);
			Icon ico=GetIcon(dir[0]);
			this.imageList1.Images.Add(ico);
		}

		//This method returns a ICON from a path
		private Icon GetIcon(string _file)
		{
			SHFILEINFO shinfo = new SHFILEINFO();
			IntPtr hImgSmall;    //the handle to the system image list
			hImgSmall = WindowsAPI.SHGetFileInfo(_file, 0, ref shinfo,
				(uint) Marshal.SizeOf(shinfo),
				(uint) SHGetFileInfoStyles.SHGFI_ICON |
				(uint) SHGetFileInfoStyles.SHGFI_SMALLICON);
			return System.Drawing.Icon.FromHandle(shinfo.hIcon);
		}
		
		//Generates childnodes, if possibe
		private void ClickNode(TreeNode _node)
		{
			this.treeView1.SelectedNode = _node;
			try
			{
				ViewFolder(_node);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
			this.treeView1.SelectedNode.Expand();
		}
		private void ViewFolder(TreeNode _node)
		{
			_node.Nodes.Clear();

			TreeNode tmpNode;
			string[] dir = Directory.GetDirectories(_node.Tag.ToString());

			foreach(string d in dir)
			{
				tmpNode = new TreeNode(d.Substring(d.LastIndexOf(@"\")+1));
				tmpNode.Tag = d;
				//This (HasSubDirs) makes about the same as the method we are already in,
				//but we don't want to loop through ALL folders.. that will take to long :)
				try
				{
					HasSubDirs(tmpNode);
				}
				catch{}

				_node.Nodes.Add(tmpNode);
			}
		}

		private void HasSubDirs(TreeNode _node)
		{
			_node.Nodes.Clear();

			TreeNode tmpNode;
			string[] dir = Directory.GetDirectories(_node.Tag.ToString());

			for(int i=0;i<dir.Length;i++)
			{
				tmpNode = new TreeNode(dir[i].ToString());
				tmpNode.Tag = dir[i].ToString();
				_node.Nodes.Add(tmpNode);
			}
		}

		public bool IsNode(TreeNode mynode)
		{
			//Are we clickin on a node?
			try
			{
				string foo = mynode.Text;
				return true;
			}
			catch
			{
				return false;
			}
		}

		public string GetDriveName(string drive)
		{
			//receives volume name of drive
			StringBuilder volname = new StringBuilder(256);
			//receives serial number of drive,not in case of network drive(win95/98)
			long sn= new long();
			long maxcomplen = new long();//receives maximum component length
			long sysflags = new long();//receives file system flags
			StringBuilder sysname = new StringBuilder(256);//receives the file system name
			long retval= new long();//return value

			retval = WindowsAPI.GetVolumeInformation(drive, volname, 256, sn, maxcomplen, 
				sysflags, sysname,256);
            
			if(retval!=0)return volname.ToString();
			else return "";
		}
		public int getDriveType(string drive)
		{
			if((WindowsAPI.GetDriveType(drive) & 5)==5)return 5;//cd
			if((WindowsAPI.GetDriveType(drive) & 3)==3)return 3;//fixed
			if((WindowsAPI.GetDriveType(drive) & 2)==2)return 2;//removable
			if((WindowsAPI.GetDriveType(drive) & 4)==4)return 4;//remote disk
			if((WindowsAPI.GetDriveType(drive) & 6)==6)return 6;//ram disk
			return 0;
		}


		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(320, 231);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCollapse);
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(64, 237);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(67, 25);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(168, 237);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmDirectoryPicker
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(320, 269);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDirectoryPicker";
            this.ShowInTaskbar = false;
            this.Text = "Verzeichnis wählen";
            this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.SelectedDirectory = this.treeView1.SelectedNode.Tag.ToString();
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void treeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			this.isLocked = true;
			ViewFolder(e.Node);
		}
		private void treeView1_BeforeCollapse(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			this.isLocked = true;
		}

		public void treeView1_Click(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//In case that we have collapsed/expanded a node just before, we don't want to trigger this method.
			if(this.isLocked==false)
			{
				/* The clicked node */
				if(IsNode(this.treeView1.GetNodeAt(this.treeView1.PointToClient(Cursor.Position)))==true)
				{
					ClickNode(this.treeView1.GetNodeAt(this.treeView1.PointToClient(Cursor.Position)));
				}
			}
			else //First unlock the method.
			{
				this.isLocked = false;
			}
		}
	}
}
