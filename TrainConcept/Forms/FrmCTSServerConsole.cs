using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Form1.
	/// </summary>
	public partial class FrmCTSServerConsole : XtraForm
	{
		public FrmCTSServerConsole()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();
		}

        public void SetTitle(string strIPAdr)
        {
            Text = string.Format("Server-Console {0}", strIPAdr);
        }

		public void Add(string sMessage)
		{
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    if (listBoxControl1.ItemCount > 1000)
                        listBoxControl1.Items.RemoveAt(0);

                    int iNew = listBoxControl1.Items.Add(sMessage);
                    listBoxControl1.TopIndex = iNew;
                });
            }
            else
            {
                int iNew = listBoxControl1.Items.Add(sMessage);
                listBoxControl1.TopIndex = iNew;
            }
		}
    }
}
