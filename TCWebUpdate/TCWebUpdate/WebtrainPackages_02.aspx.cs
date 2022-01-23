using DevExpress.Web;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using TCWebUpdate.Repositories;

namespace TCWebUpdate
{
    public partial class WebtrainPackages_02 : System.Web.UI.Page
    {
        private static WebtrainPackageRepository wtpRepository = WebtrainPackageRepository.Instance;

        private static bool bIsAuthenticated = false;

        public static WebtrainPackageRepository WtpRepository { get => wtpRepository; set => wtpRepository = value; }
        public static bool IsAuthenticated { get => bIsAuthenticated; set => bIsAuthenticated = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            IsAuthenticated = (Session["EMail"] != null);

            bool bIsWebtrainUser = (Session["UserName"] != null && ((string)Session["UserName"]).Length > 0);

            MainMaster master = (MainMaster)Page.Master;
            RootMaster root = (RootMaster)master.Master;

            //["Language"],["UseType"],["Version"],["KeyId"] -> sind an die Seite übergebenen Argumente
            //string szLanguage = Request.QueryString["Language"]; //de, en_GB, fr_FR, it_IT, es_ES
            //string szUseType = Request.QueryString["UseType"];  //0=StandAlone  1=Client  2=Server  
            //string szVersion = Request.QueryString["Version"];  //z.B. 1.0.0.0
            //string szKeyId = Request.QueryString["KeyId"];    //KeyId

            if (!IsAuthenticated && !bIsWebtrainUser)
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
                PopulateColumns(ref ASPxGridView1, 9991);
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
                PopulateColumns(ref ASPxGridView2,9990);
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
                PopulateColumns(ref ASPxGridView3, 9991);
                ASPxGridView3.DataBind();
            }
            else
                CreateTemplate(ref ASPxGridView3);
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
            col1.DataItemTemplate = new MyButtonTemplate_02(); // Create a template
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
            ((GridViewDataColumn)gv.Columns["Wartung"]).DataItemTemplate = new MyButtonTemplate_02();
        }


    }
    class MyButtonTemplate_02 : ITemplate
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
            if (WebtrainPackages_02.IsAuthenticated)
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
                WebtrainPackages_02.WtpRepository.DeletePackageInfo(Convert.ToInt32(strLicId), value);
                c.Grid.DataBind();
            }
        }
    }
}