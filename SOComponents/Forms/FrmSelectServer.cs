using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SoftObject.SOComponents.Forms
{
    public partial class FrmSelectServer : XtraForm
    {
        private IPAddress m_oSelIPAddress = null;
        public System.Net.IPAddress SelIPAddress {get { return m_oSelIPAddress; }}

        public FrmSelectServer()
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
