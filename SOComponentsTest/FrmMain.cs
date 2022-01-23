using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using Forms;
using Microsoft.Win32;
using SOComponentsTest;
using SoftObject.SOComponents.Forms;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.SOComponents.Controls;

namespace SOComponentsTest
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Form1.
	/// </summary>
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
	{
		private ProfileHandler m_profHnd = new ProfileHandler();
        PowerPointViewer.PowerPointViewerControl objPPTViewer = null;

		public FrmMain()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();
		    UserLookAndFeel.Default.SkinName = "SOSkinDark";
		}

		private void OpenAnimation(string _fileName1,string _fileName2)
		{
            MessageBox.Show("We are Sorry but TrainConcept can't show Shockwave-animations", "WebTrain", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void OpenAnimation(string _fileName)
		{
			SOFlashPlayer player= new SOFlashPlayer(false);
			player.Play(_fileName);
			//player.Play("http://195.58.183.88\\TrainConceptServer");
			//player.Play("http://195.58.183.88/TrainConceptServer/Content/de/cnc_programmierung/animations/ncgru_4_3_anim1.swf");
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			m_profHnd.SetFileName("TrainConcept.ini");
			string test=m_profHnd.GetProfileString("SYSTEM","Language","test");
            objPPTViewer = new PowerPointViewer.PowerPointViewerControl();
            objPPTViewer.SerialNumber = "FranzMair3";
            objPPTViewer.LicenseKey = "9955";
		}

		private void btnGlossar_Click(object sender, System.EventArgs e)
		{
			this.soGlossar1.Visible=true;
		}

		private void soGlossar1_OnGlossar(object sender, ref GlossarEventArgs gea)
		{
			if (gea.Char!=' ')
			{
				ArrayList aStrings = new ArrayList();
				aStrings.Add(gea.Char+"-Buchstabe");
				aStrings.Add(gea.Char+"-Buchstabe2");
				gea.Keywords = aStrings;
			}
			else
			{
				if (!gea.GotoKeyword)
				{
					if (gea.Keyword == "A-Buchstabe")
					{
						gea.Description = "Das ist der Buchstabe A";
					}
					if (gea.Keyword == "B-Buchstabe")
					{
						gea.Description = "Das ist der Buchstabe B";
					}
					if (gea.Keyword == "C-Buchstabe")
					{
						gea.Description = "Das ist der Buchstabe C";
					}
					if (gea.Keyword == "D-Buchstabe")
					{
						gea.Description = "Das ist der Buchstabe D";
					}
				}
			}
		}

		private void btnMemo_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
			{
				/*
				SOMemo memo = new SOMemo(openFileDialog1.FileName);
				memo.Show();
				memo.Load();
				*/
                this.soNotice1.Show();
                this.soNotice1.Load(openFileDialog1.FileName, delegate() { return "test"; }, null, null);
				this.soNotice1.Visible=true;
			}
		}

        private int GetWorkoutState()
        {
            return -1;
        }

        private void SaveNotice(string fileName)
        {
        }

        private string GetNoticeTitle()
        {
            return "Test";
        }

		private void btnAnim_Click(object sender, System.EventArgs e)
		{
            var frm = new FrmSelectVideo();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var player = new XFrmPPTXPlayer();
                player.Show();
                player.SetPPTXFile(frm.SelectedFile);
            }
		}

	    private void btnVideo_Click(object sender, System.EventArgs e)
	    {
            String strRoot = "";
            RegistryKey key = GetRegistrySoftwareKey(@"SoftObject\TrainConcept");
            if (key != null)
                strRoot = (string)key.GetValue("InstallationPath");
            
	        var frm = new FrmSelectVideo();
	        if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
	        {
                var player = new SOVideoPlayer_VisioForge();
                player.Show();
                player.Play(frm.SelectedFile);
	        }
	    }

	    private void btnFlash_Click(object sender, System.EventArgs e)
		{
            openFileDialog1.Filter = "PowerPoint Files (*.ppt;*.pptx*.pps)|*.ppt;*.pptx;*.pps||";

            if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var player = new XFrmPPTXPlayer();
                player.Show();
                player.SetPPTXFile(openFileDialog1.FileName);

				/*
                transparentFrameControl1.Initialize(-1,-1,-1,-1, objPPTViewer);
                objPPTViewer.IsHide = false;
                objPPTViewer.Fastmode = true;
                objPPTViewer.IsTopMost = true;

                Point absLoc = this.PointToScreen(transparentFrameControl1.Location);
                Point absSize = new Point(transparentFrameControl1.Width, transparentFrameControl1.Height);
                objPPTViewer.Open(openFileDialog1.FileName, absLoc.X+5, absLoc.Y+5, absSize.X-10, absSize.Y-10);*/
			}
        }

        public static RegistryKey GetRegistrySoftwareKey(string strKey)
        {
            RegistryKey key = null;
            if (Environment.Is64BitOperatingSystem)
            {
                RegistryKey localMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                key = localMachine64.OpenSubKey(@"Software\Wow6432\" + strKey, false);
            }
            else
                key = Registry.LocalMachine.OpenSubKey(@"Software\" + strKey, true);
            return key;
        }

        private void transparentFrameControl1_FrameChanged(object sender, ref SoftObject.SOComponents.TransparentFrameEventArgs ea)
        {
            Point absLoc = this.PointToScreen((sender as TransparentFrameControl).Location);
			(ea.Context as PowerPointViewer.PowerPointViewerControl).Move(absLoc.X,absLoc.Y);
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            objPPTViewer.Close();
        }
    }
}
