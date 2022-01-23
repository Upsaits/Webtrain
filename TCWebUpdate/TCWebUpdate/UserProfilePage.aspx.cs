using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace TCWebUpdate
{
    public partial class UserProfilePage : System.Web.UI.Page
    {
        private static MainMaster m_masterPage = null;
        private static RootMaster m_rootPage = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                RichEdit.CreateDefaultRibbonTabs(true);
                RichEdit.RibbonTabs[0].Groups[0].Items.Clear();

                for (int i = 1; i < RichEdit.RibbonTabs.Count; i++)
                    RichEdit.RibbonTabs[i].Visible = false;

                var saveItem = new RibbonButtonItem("Save");
                //saveItem.LargeImage.IconID = IconID.SaveSave32x32;
                saveItem.Size = RibbonItemSize.Large;
                RichEdit.RibbonTabs[0].Groups[0].Items.Add(saveItem);

                string filename = MapPath("~/docs") + "/Profildaten.docx";
                RichEdit.Open(filename);
            }

            bool bIsAuthenticated = (Session["EMail"] != null);
            bool bIsWebtrainUser = (Session["UserName"] != null && ((string)Session["UserName"]).Length > 0);

            m_masterPage = (MainMaster)Page.Master;
            m_rootPage = (RootMaster)m_masterPage.Master;

            if (!bIsAuthenticated || !bIsWebtrainUser)
            {
                m_masterPage = (MainMaster)Page.Master;
                m_masterPage.ShowPanels(false);

                m_rootPage = (RootMaster)m_masterPage.Master;
                m_rootPage.ShowMenu(false);
            }
            else
            {
                m_masterPage = (MainMaster)Page.Master;
                m_masterPage.ShowPanels(true,MainMaster.PanelType.UserProfile);
                m_masterPage.NavigationBar.ItemClick += NavigationBar_ItemClick; ;

                m_rootPage = (RootMaster)m_masterPage.Master;
                m_rootPage.ShowMenu(true);

            }
        }

        private void NavigationBar_ItemClick(object source, NavBarItemEventArgs e)
        {
            
        }

    }
}