using DevExpress.Web;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using TCWebUpdate.Repositories;

namespace TCWebUpdate
{
    public partial class WebtrainPackages_01 : System.Web.UI.Page
    {
        private static WebtrainPackageRepository wtpRepository = WebtrainPackageRepository.Instance;

        private static bool bIsAuthenticated = false;

        public static WebtrainPackageRepository WtpRepository { get => wtpRepository; set => wtpRepository = value; }
        public static bool BIsAuthenticated { get => bIsAuthenticated; set => bIsAuthenticated = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            BIsAuthenticated = (Session["EMail"] != null);

            bool bIsWebtrainUser = (Session["UserName"] != null && ((string)Session["UserName"]).Length > 0);

            MainMaster master = (MainMaster)Page.Master;
            RootMaster root = (RootMaster)master.Master;

            //["Language"],["UseType"],["Version"],["KeyId"] -> sind an die Seite übergebenen Argumente
            //string szLanguage = Request.QueryString["Language"]; //de, en_GB, fr_FR, it_IT, es_ES
            //string szUseType = Request.QueryString["UseType"];  //0=StandAlone  1=Client  2=Server  
            //string szVersion = Request.QueryString["Version"];  //z.B. 1.0.0.0
            //string szKeyId = Request.QueryString["KeyId"];    //KeyId

            if (!BIsAuthenticated && !bIsWebtrainUser)
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
            }

        }

        protected void ASPxButton1_Init(object sender, EventArgs e)
        {
            //if (m_bIsAuthenticated)
            {
                ((ASPxButton)sender).Enabled = true;

                GridViewDataItemTemplateContainer c = ((ASPxButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
                if (c.Grid.Columns["Dateiname"] is GridViewDataHyperLinkColumn)
                {
                    string value = c.Grid.GetRowValues(c.VisibleIndex, "filename").ToString();
                    ((ASPxButton)sender).ClientSideEvents.Click = "function(s,e){alert('" + value + "');}";
                }
            }
            //else
            //    ((ASPxButton)sender).Enabled = false;
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((ASPxButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            if (c.Grid.Columns["Dateiname"] is GridViewDataHyperLinkColumn)
            {
                /*
                string value = c.Grid.GetRowValues(c.VisibleIndex, "filename").ToString();
                GridViewDataHyperLinkColumn temp = c.Grid.Columns["Dateiname"] as GridViewDataHyperLinkColumn;
                string strLicId = temp.PropertiesHyperLinkEdit.NavigateUrlFormatString.Substring(10, 4);
                string strPackagesDir = HttpContext.Current.Server.MapPath("~/packages/");
                string strDestFilename = $"{strPackagesDir}{strLicId}\\{value}";
                if (File.Exists(strDestFilename))
                    File.Delete(strDestFilename);
                m_wtpRepository.DeletePackageInfo(Convert.ToInt32(strLicId), value);*/
                c.Grid.DataBind();
            }
        }

        protected void ASPxGridView1_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView1, 1002);
                ASPxGridView1.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView1);
        }

        protected void ASPxGridView2_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView2,1005);
                ASPxGridView2.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView2);
        }

        protected void ASPxGridView3_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource3.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource3.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView3, 1006);
                ASPxGridView3.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView3);
        }

        protected void ASPxGridView4_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource4.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource4.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView4, 1019);
                ASPxGridView4.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView4);
        }


        protected void ASPxGridView5_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource5.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource5.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView5, 1009);
                ASPxGridView4.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView5);
        }

        protected void ASPxGridView6_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource6.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource6.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView6, 1012);
                ASPxGridView6.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView6);
        }

        protected void ASPxGridView7_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource7.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource7.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView7, 1011);
                ASPxGridView7.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView7);
        }


        protected void ASPxGridView8_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource8.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource8.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView8, 1010);
                ASPxGridView7.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView8);
        }

        protected void ASPxGridView9_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource9.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource9.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView9, 1013);
                ASPxGridView9.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView9);
        }

        protected void ASPxGridView10_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource10.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource10.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView10, 1014);
                ASPxGridView10.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView10);
        }

        protected void ASPxGridView11_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource11.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource11.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView11, 1017);
                ASPxGridView11.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView11);
        }

        protected void ASPxGridView12_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource12.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource12.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView12, 1016);
                ASPxGridView12.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView12);
        }

        protected void ASPxGridView13_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource13.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource13.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#endif
#endif
            if (!IsPostBack && !IsCallback)
            {
                PopulateColumns(ref ASPxGridView13, 1015);
                ASPxGridView13.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView13);
        }


        public void PopulateColumns(ref ASPxGridView gv,int iLicId)
        {
            gv.Columns.Add(CreateColumn("autor","Autor",1,100));
            gv.Columns.Add(CreateColumn("description", "Beschreibung", 2, 100));
            gv.Columns.Add(CreateColumn("languageId", "Sprache", 3, 100));
            gv.Columns.Add(CreateColumn("createdOn", "Erstellt am", 4, 80));
            gv.Columns.Add(CreateColumn("title", "Titel", 5, 100));

            var col = new GridViewDataHyperLinkColumn();
            col.FieldName = "filename";
            col.Caption = "Dateiname";
            col.VisibleIndex = 6;
            col.PropertiesHyperLinkEdit.NavigateUrlFormatString = $"/packages/{iLicId}/{0}";
            gv.Columns.Add(col);

            var col1 = new DevExpress.Web.GridViewDataButtonEditColumn();
            col1.DataItemTemplate = new MyButtonTemplate_01(); // Create a template
            col1.Name = "Wartung";
            col1.Caption = "Wartung";
            col1.VisibleIndex = 7;
            col1.ShowInCustomizationForm = true;
            gv.Columns.Add(col1);

        }

        GridViewDataTextColumn CreateColumn(string strFieldname, string strCaption, int iVisibleId, int iWidth)
        {
            GridViewDataTextColumn colID = new GridViewDataTextColumn();
            colID.FieldName = strFieldname;
            colID.Caption = strCaption;
            colID.ShowInCustomizationForm = true;
            colID.VisibleIndex = iVisibleId;
            colID.Width = iWidth;
            return colID;
        }
        private void CreateTemplate(ref ASPxGridView gv)
        {
            ((GridViewDataColumn)gv.Columns["Wartung"]).DataItemTemplate = new MyButtonTemplate_01();
        }


    }
    class MyButtonTemplate_01 : ITemplate
    {
        public void InstantiateIn(System.Web.UI.Control container)
        {
            var btn = new ASPxButton();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
            btn.ID = "DeleteButton";
            btn.Text = "Löschen";
            btn.Click += Btn_Click;
            btn.Init += Btn_Init;
            container.Controls.Add(btn);
        }

        private void Btn_Init(object sender, EventArgs e)
        {
            if (WebtrainPackages_01.BIsAuthenticated)
            {
                ((ASPxButton)sender).Enabled = true;

                GridViewDataItemTemplateContainer c = ((ASPxButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
                if (c.Grid.Columns["Dateiname"] is GridViewDataHyperLinkColumn)
                {
                    string value = c.Grid.GetRowValues(c.VisibleIndex, "filename").ToString();
                    ((ASPxButton)sender).ClientSideEvents.Click = "function(s,e){alert('" + value + "');}";
                }
            }
            else
                ((ASPxButton)sender).Enabled = false;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((ASPxButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            if (c.Grid.Columns["Dateiname"] is GridViewDataHyperLinkColumn)
            {
                string value = c.Grid.GetRowValues(c.VisibleIndex, "filename").ToString();
                GridViewDataHyperLinkColumn temp = c.Grid.Columns["Dateiname"] as GridViewDataHyperLinkColumn;
                string strLicId = temp.PropertiesHyperLinkEdit.NavigateUrlFormatString.Substring(10, 4);
                string strPackagesDir = HttpContext.Current.Server.MapPath("~/packages/");
                string strDestFilename = $"{strPackagesDir}{strLicId}\\{value}";
                if (File.Exists(strDestFilename))
                    File.Delete(strDestFilename);
                WebtrainPackages_01.WtpRepository.DeletePackageInfo(Convert.ToInt32(strLicId), value);
                c.Grid.DataBind();
            }
        }
    }
}