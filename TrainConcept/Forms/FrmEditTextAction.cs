using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Summary description for FrmEditTextAction.
	/// </summary>
	public partial class FrmEditTextAction : XtraForm
	{
		private bool m_isModified=false;
		private string m_changedText="";

		public bool IsModified
		{
			get
			{
				return m_isModified;
			}
		}

		public string ChangedText
		{
			get
			{
				return m_changedText;
			}				

		}

		public FrmEditTextAction(string text)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			edtText.Text = text;

		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if (edtText.CanUndo)
			{
				m_isModified = true;
				m_changedText = edtText.Text;				
			}

			DialogResult = DialogResult.OK;
		}

	}
}
