using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TCWebUpdate {
    public partial class _Default : System.Web.UI.Page 
    {
        private static MainMaster m_masterPage = null;
        private static RootMaster m_rootPage = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                m_masterPage = (MainMaster)Page.Master;
                m_rootPage = (RootMaster)m_masterPage.Master;
            }

            bool bIsAuthenticated = (Session["EMail"] != null);
            if (!bIsAuthenticated)
            {
                m_masterPage.ShowPanels(false);
                m_rootPage.ShowMenu(false);
            }

        }
        
    }
}