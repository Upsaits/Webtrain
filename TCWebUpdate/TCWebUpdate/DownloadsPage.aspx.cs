using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace TCWebUpdate
{
    public partial class DownloadsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool bIsAuthenticated = (Session["EMail"] != null);
            bool bIsWebtrainUser = (Session["UserName"] != null && ((string) Session["UserName"]).Length>0);

            MainMaster master = (MainMaster)Page.Master;
            RootMaster root = (RootMaster)master.Master;

            if (!bIsAuthenticated && !bIsWebtrainUser)
            {
                master = (MainMaster)Page.Master;
                master.ShowPanels(false);

                root = (RootMaster)master.Master;
                root.ShowMenu(false);
            }
            else
            {
                master = (MainMaster)Page.Master;
                master.ShowPanels(false);

                root = (RootMaster)master.Master;
                root.ShowMenu(true);
                root.SetCampusName("Metzentrum Attnang-Puchheim");

                if (bIsWebtrainUser)
                {
                    var strUserName = (string) Session["UserName"];
                    if (strUserName.Length>0)
                        this.Hyperlink2.NavigateUrl = String.Format("OpenVPN/{0}/OpenVPN_Installer_{1}.sfx.exe",strUserName,strUserName);
                }
            }
        }
    }
}