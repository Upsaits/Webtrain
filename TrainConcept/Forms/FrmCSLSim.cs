using System;
using System.Collections;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Form1.
	/// </summary>
	public partial class FrmCSLSim : XtraForm
	{
        private bool isMill = false;
        private string fileName;
        private string tempFileName;

        public FrmCSLSim()
		{
			isMill=true;
			string exeFolder=Application.ExecutablePath.Substring(0,Application.ExecutablePath.LastIndexOf('\\'));
			if (isMill)
				fileName = exeFolder+"\\test1.nc";
			else
				fileName = exeFolder+"\\test2.nc";

			tempFileName = System.IO.Path.GetTempFileName();
			System.IO.File.Copy(fileName,tempFileName,true);
			
			InitializeComponent();

			if (isMill)
				this.Text = this.Text + " - mill";
			else
			{
				this.Text = this.Text + " - turn";
				this.textBox2.Enabled = false;
				this.textBox2.Text = "-----";
			}
		}

		public FrmCSLSim(bool _isMill,string _fileName)
		{
			isMill = _isMill;
			fileName = _fileName;
			tempFileName = System.IO.Path.GetTempFileName();

			if (fileName.IndexOf("http:")>=0)
			{
				string tempFileName1 = System.IO.Path.GetTempFileName();
				System.Net.WebClient wc = new System.Net.WebClient();
				wc.DownloadFile(fileName,tempFileName1);
				fileName = tempFileName1;
			}
			
			System.IO.File.Copy(fileName,tempFileName,true);
			
			InitializeComponent();

            InitializeSimControl();

			if (isMill)
				this.Text = this.Text + " - mill";
			else
			{
				this.Text = this.Text + " - turn";
				this.textBox2.Enabled = false;
				this.textBox2.Text = "-----";
			}
		}


        private void InitializeSimControl()
        {
            this.cslSimControl1 = new CSLSim.CSLSimControl(isMill);
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cslSimControl1
            // 
            this.cslSimControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cslSimControl1.Location = new System.Drawing.Point(0, 0);
            this.cslSimControl1.Name = "cslSimControl1";
            this.cslSimControl1.Size = new System.Drawing.Size(502, 397);
            this.cslSimControl1.TabIndex = 0;
            this.cslSimControl1.OnVarChanged += new CSLSim.VarChanged(this.cslSimControl1_OnVarChanged);
            this.cslSimControl1.OnViewerStackChanged += new CSLSim.ViewerStackChanged(this.cslSimControl1_OnViewerStackChanged);
            this.cslSimControl1.OnViewerPrgChanged += new CSLSim.ViewerPrgChanged(this.cslSimControl1_OnViewerPrgChanged);
            this.cslSimControl1.OnSetStatusText += new CSLSim.SetStatusText(this.cslSimControl1_OnSetStatusText);
            this.cslSimControl1.OnReady += new CSLSim.Ready(this.cslSimControl1_OnReady);
            this.panel3.Controls.Add(this.cslSimControl1);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
        }

		private void button1_Click(object sender, System.EventArgs e)
		{
			cslSimControl1.StartProgram();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			cslSimControl1.Reset();
		}

		private void cslSimControl1_OnSetStatusText(string text)
		{
			label1.Text = text;
		}

		private void cslSimControl1_OnViewerPrgChanged()
		{
			int actLine=0;
			ArrayList m_aLines = new ArrayList();

			for(int i=0;i<cslSimControl1.GetViewListCount(ref actLine);++i)
			{
				String line="";
				cslSimControl1.GetViewListItem(i,ref line);
				m_aLines.Add(line);
			}
			
			listBox1.Items.Clear();
			if (m_aLines.Count>0)
			{
				listBox1.Items.AddRange(m_aLines.ToArray());
				listBox1.SelectedIndex = actLine;
			}
		}

		private void cslSimControl1_OnViewerStackChanged()
		{
		}

		private void cslSimControl1_OnVarChanged(string varName, int dim1, int dim2, double value)
		{
			if (varName.IndexOf("ContourPos")>0)
			{
				switch(dim2)
				{
					case 0: textBox1.Text = String.Format("{0:#.00}",value*1000);break;
					case 1: if (isMill)
								textBox2.Text = String.Format("{0:#.00}",value*1000);break;
					case 2: textBox3.Text = String.Format("{0:#.00}",value*1000);break;
				}
			}
			else if (varName.IndexOf("IstFeed")>0)
			{
				textBox4.Text = String.Format("{0:#.00}",value*1000*60);
			}
			else if (varName.IndexOf("IstSpeed")>0)
			{
				textBox5.Text = String.Format("{0:#0}",Math.Abs(value*60));
			}
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			cslSimControl1.ToggleSingle();
		}

		private void cslSimControl1_OnReady()
		{
			cslSimControl1.SelectProgram = fileName;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			FrmCSLSimEdit frm = new FrmCSLSimEdit(tempFileName,cslSimControl1);
			if (frm.ShowDialog() == DialogResult.OK)
				cslSimControl1.SelectProgram = tempFileName;
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			Close();
		}

        private void button5_Click(object sender, System.EventArgs e)
        {
        }

	}
}

