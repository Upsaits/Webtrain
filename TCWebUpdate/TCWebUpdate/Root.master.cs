using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DevExpress.Utils.StructuredStorage.Internal.Reader;
using DevExpress.Web;

namespace TCWebUpdate
{
    public partial class RootMaster : System.Web.UI.MasterPage 
    {
        bool bIsAuthenticated=false;
        bool bIsWebtrainUser=false;

        protected void Page_Load(object sender, EventArgs e) 
        {
            bIsAuthenticated = (Session["EMail"] != null);
            bIsWebtrainUser = (Session["UserName"] != null && ((string)Session["UserName"]).Length > 0);

            ASPxLabel2.Text = DateTime.Now.Year + Server.HtmlDecode(" &copy; Copyright by [SoftObject e.U.]");

            if (bIsAuthenticated)
            {
                LoginLink.Text = String.Format("Abmelden [{0}]", Session["EMail"].ToString());
                LoginLink.NavigateUrl = "~/Account/Logout.aspx"; 
                RegisterLink.Text= "";
                RegisterLink.NavigateUrl = "";
            }
            else
            {
                LoginLink.Text = "Anmelden | ";
                LoginLink.NavigateUrl = "~/Account/Login.aspx";
                RegisterLink.Text = "Registrieren";
                RegisterLink.NavigateUrl = "~/Account/Register.aspx";
            }

            if (bIsWebtrainUser)
            {
                HeaderMenu.Items[0].Visible = false;
                HeaderMenu.Items[1].Visible = true;
            }
            else
            {
                HeaderMenu.Items[0].Visible = true;
                HeaderMenu.Items[1].Visible = false;
            }
        }

        public void ShowMenu(bool bShow)
        {
            this.HeaderMenu.Visible = bShow;
            HeaderMenu.SelectedItem = HeaderMenu.Items[0];
        }

        public void SetCampusName(string strCampusName)
        {
            int iFound = TitleLink.Text.IndexOf(":", StringComparison.Ordinal);
            if (iFound > 0)
                TitleLink.Text = TitleLink.Text.Substring(0, iFound);
            TitleLink.Text += ": ";
            TitleLink.Text += strCampusName;
        }

        protected void LoginLinkSmall_OnLoad(object sender, EventArgs e)
        {
            bIsAuthenticated = (Session["EMail"] != null);
            bIsWebtrainUser = (Session["UserName"] != null && ((string)Session["UserName"]).Length > 0);

            var link = sender as ASPxHyperLink;
            if (link != null)
            {
                if (bIsAuthenticated)
                {
                    link.Text = "";
                    link.NavigateUrl = "";
                }
                else
                {
                    link.Text = "Anmelden";
                    link.NavigateUrl = "~/Account/Login.aspx";
                }
            }
        }

        protected void RegisterLinkSmall_OnLoad(object sender, EventArgs e)
        {
            bIsAuthenticated = (Session["EMail"] != null);
            bIsWebtrainUser = (Session["UserName"] != null && ((string)Session["UserName"]).Length > 0);

            var link = sender as ASPxHyperLink;
            if (link != null)
            {
                if (bIsAuthenticated)
                {
                    link.Text = String.Format("Abmelden [{0}]", Session["EMail"].ToString());
                    link.NavigateUrl = "~/Account/Logout.aspx";
                }
                else
                {
                    link.Text = "Registrieren";
                    link.NavigateUrl = "~/Account/Register.aspx";
                }
            }
        }
    }
}