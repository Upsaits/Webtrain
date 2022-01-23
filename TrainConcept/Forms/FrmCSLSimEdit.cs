using System.Windows.Forms;
using CSLSim;
using DevExpress.XtraEditors;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmCSLSimEdit.
	/// </summary>
	public partial class FrmCSLSimEdit : XtraForm
	{
		private string m_fileName="";
		private CSLSimControl m_simControl=null;

		public FrmCSLSimEdit(string fileName,CSLSimControl simControl)
		{
			m_fileName = fileName;
			m_simControl = simControl;
			InitializeComponent();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			m_simControl.SelectProgram="";
			textControl1.Save(m_fileName,TXTextControl.StreamType.PlainAnsiText);
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void Form2_Load(object sender, System.EventArgs e)
		{
			textControl1.Load(m_fileName,TXTextControl.StreamType.PlainAnsiText);
		}
	}
}
