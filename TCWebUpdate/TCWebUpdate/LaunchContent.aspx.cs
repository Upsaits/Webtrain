using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using DevExpress.XtraPrinting.Native;
using TCWebUpdate.Repositories;

namespace TCWebUpdate
{
    public partial class LaunchContent : System.Web.UI.Page
    {
        private static int m_licenseId = -1;
        private static string m_userName = "";
        private static string m_packageName = ";";
        private static LaunchPackageRepository launchPackageRepo=LaunchPackageRepository.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            bool bIsValid = true;

            string strLocationId = Request.QueryString["LicenseId"];
            if (String.IsNullOrEmpty(strLocationId))
            {
                strLocationId = "";
                bIsValid = false;
            }

            m_userName = Request.QueryString["Username"];
            if (String.IsNullOrEmpty(strLocationId))
            {
                m_userName = "";
                bIsValid = false;
            }

            m_packageName = Request.QueryString["Title"];
            if (String.IsNullOrEmpty(m_packageName))
            {
                m_packageName = "Fehler";
                bIsValid = false;
            }

            string strDescription = Request.QueryString["Description"];
            if (String.IsNullOrEmpty(strDescription))
            {
                strDescription = "Dieser Inhalt ist ungültig";
                bIsValid = false;
            }

            lblDescription.Text = strDescription;
            formLayout.Items[0].Caption = m_packageName;
            if (!strLocationId.IsEmpty())
                m_licenseId = Convert.ToInt32(strLocationId);
            if (!bIsValid)
                btnLaunch.Enabled = false;
        }

        protected void btnLaunch_Click(object sender, EventArgs e)
        {
            launchPackageRepo.AddPackage(m_licenseId,m_userName, m_packageName);
        }
    }
}