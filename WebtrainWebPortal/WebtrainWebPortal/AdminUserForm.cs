using MySqlConnector;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Wisej.Web;

namespace WebtrainWebPortal
{
    public partial class AdminUserForm : Form
    {
        private static AdminUserRepository m_adminUserRepo = AdminUserRepository.Instance;
        public AdminUserForm()
        {
            InitializeComponent();
        }

        private void Window1_Load(object sender, EventArgs e)
        {
        }

        private void ckEditor1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
