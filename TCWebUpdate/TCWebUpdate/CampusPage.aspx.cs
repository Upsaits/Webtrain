using System;
using System.Net;
using System.Windows.Forms;
using DevExpress.Web.Office;
using DevExpress.XtraPrinting.Native;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.WebPages;
using DevExpress.Charts.Model;
using DevExpress.Utils.StructuredStorage.Internal;
using TCWebUpdate.Repositories;

namespace TCWebUpdate
{
    public partial class CampusPage : System.Web.UI.Page
    {
        private static UserRepository m_userRepo = UserRepository.Instance;
        private static string strSyncURL = "";
        private static bool bCalendarInit = false;
        //private static string strSyncURL = "http://194.112.244.74/Franky/notenliste.xlsx";

        protected void Page_Load(object sender, EventArgs e)
        {
#if USE_LOCALHOST
            this.SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
            this.SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            this.SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
            this.SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#else
                this.SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["world4YouConnection"].ConnectionString;
                this.SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings["world4YouConnection"].ConnectionString;
#endif
#endif
            bool bIsAuthenticated = (Session["EMail"] != null);
           
            MainMaster master = (MainMaster)Page.Master;
            RootMaster root = (RootMaster)master.Master;

            if (!bIsAuthenticated)
            {
                master = (MainMaster)Page.Master;
                master.ShowPanels(false);

                root = (RootMaster)master.Master;
                root.ShowMenu(false);
                formLayout1.Visible = false;
            }
            else
            {
                if (!bCalendarInit)
                {
                    calendar.SelectedDate = DateTime.Now;
                    bCalendarInit = true;
                }

                master = (MainMaster)Page.Master;
                master.ShowPanels(false);

                root = (RootMaster)master.Master;
                root.ShowMenu(true);
                root.SetCampusName("Metzentrum Attnang-Puchheim");
            }
        }

        /*
        protected void uplExcelUserSheet_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            string fileName = e.UploadedFile.FileName;
            string filePath = string.Format(MapPath("~/App_Data/{0}"), fileName);
            e.UploadedFile.SaveAs(filePath);
            strFilePath = filePath;
            LoadDataFromFile();
        }*/


        private void LoadDataFromFile(string strFilePath)
        {
            if (!strFilePath.IsEmpty())
            {
                int FirstRowId = 0;
                int nameColId = 0;
                int classColId = 1;
                int emailColId = -1;

                DocumentManager.CloseDocument(strFilePath);
                ASPxSpreadsheet1.Open(strFilePath);

                edtSyncURLFile.Text = "";

                DateTime dt = calendar.SelectedDate;
                string monthname = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dt.Month);
                string year = (dt.Year - 2000).ToString();

                string actualSheet = monthname;
                string actualSheet1 = ' ' + monthname;
                string actualSheet2 = ' ' + monthname + ' ';

                if (ASPxSpreadsheet1.Document.Worksheets.Contains(actualSheet1))
                    actualSheet = actualSheet1;
                else if (ASPxSpreadsheet1.Document.Worksheets.Contains(actualSheet2))
                    actualSheet = actualSheet2;

                if (!(ASPxSpreadsheet1.Document.Worksheets.Contains(actualSheet)))
                {
                    actualSheet = monthname + ' ' + year;
                    if (!ASPxSpreadsheet1.Document.Worksheets.Contains(actualSheet))
                    {
                        edtSyncURLFile.Text = String.Format("{0} im Excel-Sheet nicht gefunden!",actualSheet);
                        return;
                    }
                }
                
                // all ids 0 based
                if (strFilePath.Contains("Anwesenheitsliste_Auswahl"))
                {
                    classColId = 1;
                    FirstRowId = 7;
                    nameColId = 2;
                }
                else if (strFilePath.Contains("Anwesenheit_TeilnehmerInnen"))
                {
                    classColId = 1;
                    FirstRowId = 8;
                    nameColId = 3;
                    emailColId = 6;
                }
                else if (strFilePath.Contains("Anwesenheit_Trainer"))
                {
                    classColId = 0;
                    FirstRowId = 2;
                    nameColId = 1;
                    emailColId = 2;
                }
                else
                    return;

                /*
                string strTest = ASPxSpreadsheet1.Document.Worksheets["Februar 19"].Columns[2][7].DisplayText;
                var f = ASPxSpreadsheet1.Document.Worksheets["Februar 19"].Columns[2][3].Font;
                Console.WriteLine(strTest);
                Console.WriteLine(f.ToString());*/

                bool bExit = false;
                do
                {

                    string name = ASPxSpreadsheet1.Document.Worksheets[actualSheet].Columns[nameColId][FirstRowId].DisplayText;
                    string className = ASPxSpreadsheet1.Document.Worksheets[actualSheet].Columns[classColId][FirstRowId].DisplayText;
                    string email = (emailColId>=0) ? ASPxSpreadsheet1.Document.Worksheets[actualSheet].Columns[emailColId][FirstRowId].DisplayText : "";
                    bool bActive = ASPxSpreadsheet1.Document.Worksheets[actualSheet].Columns[nameColId][FirstRowId].Font.Color.ToArgb().Equals(Color.FromArgb(0, 0, 0, 0).ToArgb());

                    string strTemp = string.Format("Analse: {0},{1},{2}", name, className,
                        (bActive) ? "active" : "inactive");

                    this.lblSyncResult.Text = strTemp;

                    if (!name.IsEmpty()/* && className.All(Char.IsDigit)*/)
                        CheckUserEntry(name.Trim(' '), String.Format("LAP{0}", className), email, bActive);
                    else if (name.IsEmpty())
                        bExit = true;

                    ++FirstRowId;

                } while (!bExit);

                this.ASPxGridView2.DataBind();
                this.ASPxGridView2.EndUpdate();
                /*
                string strTest = ASPxSpreadsheet1.Document.Worksheets["ZeugnisNoten"].Columns[2][3].DisplayText;
                var f = ASPxSpreadsheet1.Document.Worksheets["ZeugnisNoten"].Columns[2][3].Font;
                Console.WriteLine(strTest);
                Console.WriteLine(f.ToString());

                bool bExit = false;
                int rowId = 3;
                do
                {
                    string name = ASPxSpreadsheet1.Document.Worksheets["ZeugnisNoten"].Columns[2][rowId].DisplayText;
                    string className = ASPxSpreadsheet1.Document.Worksheets["ZeugnisNoten"].Columns[0][rowId].DisplayText;
                    bool bActive = !ASPxSpreadsheet1.Document.Worksheets["ZeugnisNoten"].Columns[2][rowId].Font.Strikethrough;
                    if (!name.IsEmpty())
                        CheckUserEntry(name.Trim(' '), String.Format("LAP{0}", className), bActive);
                    else
                        bExit = true;

                    ++rowId;

                } while (!bExit);*/
            }

        }

        private bool CheckUserEntry(string name, string className, string email, bool bActive)
        {
            if (name.IsEmpty())
                return false;

            // search for academic title
            string[] names = name.Split(',');
            string strDegree = "";
            if (names.Length > 1)
            {
                strDegree = names[1];
                name = names[0];
            }

            names = name.Split(' ');
            if (names.Length < 2 || names.Length>3)
                return false;

            int userId=-1;
            if (names.Length == 2)
                userId = m_userRepo.FindUser(1,names[1], "", names[0], strDegree);
            else
                userId = m_userRepo.FindUser(1,names[2], names[1], names[0], strDegree);

            bool bRes = false;
            if (userId<0)
            {
                if (names.Length == 2)
                    bRes = m_userRepo.CreateUser(1, names[1], "", names[0], strDegree, bActive, className, email, className == "LAPTrainer");
                else
                    bRes = m_userRepo.CreateUser(1, names[2], names[1], names[0], strDegree, bActive, className, email, className == "LAPTrainer");
            }
            else
            {
                if (names.Length == 2)
                    bRes = m_userRepo.UpdateUser(1, userId, names[1], "", names[0], strDegree, bActive, className, email);
                else
                    bRes = m_userRepo.UpdateUser(1, userId, names[2], names[1], names[0], strDegree, bActive, className, email);
            }
            return bRes;
        }

        protected void ASPxSpreadsheet1_Init(object sender, EventArgs e)
        {

        }

        protected void btnToggleTimer_Click(object sender, EventArgs e)
        {
            if (timerSync.Enabled)
            {
                this.timerSync.Enabled = false;
                this.edtSyncURLFile.Enabled = true;
                this.btnToggleTimer.Text = "Start";
                //this.uplExcelUserSheet.Enabled = true;
            }
            else
            {
                this.timerSync.Enabled = true;
                this.edtSyncURLFile.Enabled = false;
                this.btnToggleTimer.Text = "Stop";
                //this.uplExcelUserSheet.Enabled = false;
                this.lblSyncResult.Text = "";
            }
        }

        protected void timerSync_Tick(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                strSyncURL = edtSyncURLFile.Text;
                try
                {
                    string filePath = string.Format(MapPath("~/App_Data/notenliste.xlsx"));
                    client.Credentials = new NetworkCredential("Franky", "W3btr@1n");
                    client.DownloadFile(strSyncURL, filePath);
                    LoadDataFromFile(filePath);
                    
                    lblSyncResult.Text = String.Format("Ok., " + DateTime.Now.ToString("G"));
                }
                catch (System.Exception ex)
                {
                    btnToggleTimer_Click(this, new EventArgs());
                    lblSyncResult.Text = ex.Message;
                }
            }
        }

        protected void ASPxFileManager1_FilesUploaded(object source, DevExpress.Web.FileManagerFilesUploadedEventArgs e)
        {
            foreach (var f in e.Files)
            {
                string strPath = MapPath(f.FullName);
                LoadDataFromFile(strPath);
            }
        }

        protected void calendar_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void btnSynchronize_Click(object sender, EventArgs e)
        {
            try
            {
	            m_userRepo.DeleteAllUsers(1);
                this.lblSyncResult.Text = "Alle User gelöscht!";

	            string[] files = Directory.GetFiles(MapPath("~/Excel"));
	            foreach (var f in files)
	            {
	                LoadDataFromFile(f);
	            }
            }
            catch (System.Exception ex)
            {
#if USE_LOCALHOST
                MessageBox.Show(ex.Message);
#endif
            }
        }


    }
}