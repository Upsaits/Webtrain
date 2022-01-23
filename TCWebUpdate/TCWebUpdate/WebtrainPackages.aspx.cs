using DevExpress.Web;
using System;
using System.Configuration;
using System.Web.UI;
using TCWebUpdate.Repositories;

namespace TCWebUpdate
{
    public partial class WebtrainPackages : System.Web.UI.Page
    {
        private static WebtrainPackageRepository m_wtpRepository = WebtrainPackageRepository.Instance;

        private static bool m_bIsAuthenticated = false;
        private static MainMaster m_masterPage = null;
        private static RootMaster m_rootPage = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            m_bIsAuthenticated = (Session["EMail"] != null);

            bool bIsWebtrainUser = (Session["UserName"] != null && ((string)Session["UserName"]).Length > 0);

            m_masterPage = (MainMaster)Page.Master;
            m_rootPage = (RootMaster)m_masterPage.Master;

            //["Language"],["UseType"],["Version"],["KeyId"] -> sind an die Seite übergebenen Argumente
            //string szLanguage = Request.QueryString["Language"]; //de, en_GB, fr_FR, it_IT, es_ES
            //string szUseType = Request.QueryString["UseType"];  //0=StandAlone  1=Client  2=Server  
            //string szVersion = Request.QueryString["Version"];  //z.B. 1.0.0.0
            //string szKeyId = Request.QueryString["KeyId"];    //KeyId

            if (!IsPostBack)
            {
                m_masterPage = (MainMaster)Page.Master;
                m_rootPage = (RootMaster)m_masterPage.Master;
            }

            m_masterPage.ShowPanels(false);

            bool bIsAuthenticated = (Session["EMail"] != null);
            m_rootPage.ShowMenu(bIsAuthenticated);

            if (bIsAuthenticated)
            {
                bool bShowBtns = true;
                if (Session["LocationId"] != null)
                {
                    int iLocId = (int)Session["LocationId"];
                    if (iLocId == 1)
                    {
                        Response.Redirect("~/WebtrainPackages_01.aspx");
                        bShowBtns = false;
                    }
                    else if (iLocId == 2)
                    {
                        Response.Redirect("~/WebtrainPackages_02.aspx");
                        bShowBtns = false;
                    }
                }

                if (bShowBtns)
                {
                    Button1.Visible = bShowBtns;
                    Button2.Visible = bShowBtns;
                }
            }
            else 
                Response.Redirect("~/WebtrainPackages_01.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebtrainPackages_02.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebtrainPackages_01.aspx");
        }
    }
}