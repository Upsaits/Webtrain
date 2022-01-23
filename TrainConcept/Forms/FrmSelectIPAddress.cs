using System;
using System.Net;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmSelectIPAddress : XtraForm
    {
        private IPAddress m_oSelIPAddress = null;
        public System.Net.IPAddress SelIPAddress {get { return m_oSelIPAddress; }}

        public FrmSelectIPAddress()
        {
            InitializeComponent();

            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList) 
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    cboIPAdresses.Items.Add(ip);
            cboIPAdresses.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cboIPAdresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_oSelIPAddress = (IPAddress) cboIPAdresses.SelectedItem;
        }
    }
}
