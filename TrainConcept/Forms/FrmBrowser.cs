using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using mshtml;
using SoftObject.SOComponents.UtilityLibrary;


namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmBrowser.
	/// </summary>
	public partial class FrmBrowser : XtraForm
	{
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        ///
        private AppHandler AppHandler = Program.AppHandler;
        private string m_url="http://www.webtrain.at";
        private string m_title = "";
		private ResourceHandler rh=null;
        private const string strJavaScript =
@"javascript: function updateGridview(id) 
		{
			var myReference = ASPxClientControl.GetControlCollection().GetByName(id);
            if( !myReference ) 
				window.alert('Nothing works in this browser'); 
			else
			{
                myReference.Refresh();
			}

		}";

		public string Title
		{
			get {return m_title;}
			set {m_title=value;}
		}

		public string URL
		{
			set {
					m_url=value;
					webBrowser1.Navigate(m_url);
				}
            get {
                    return m_url;
                }
		}

		public FrmBrowser()
		{
			InitializeComponent();

            rh = new ResourceHandler(AppHandler.ResourceName, GetType().Assembly);
            this.Icon = rh.GetIcon("main");

            webBrowser1.Navigate(m_url);
		}

        public new void Activate()
        {
            base.Activate();
            AfterActivation(true);
        }

        public void AfterActivation(bool bIsOn)
        {
            if (!bIsOn)
            {
                AppHandler.MainForm.CloseBar.Visible = false;
            }
            else
            {
                AppHandler.MainForm.CloseBar.Visible = true;
            }
        }
		private void FrmBrowser_Load(object sender, System.EventArgs e)
		{
		}

		private void FrmBrowser_Closed(object sender, System.EventArgs e)
		{
            AfterActivation(false);
			AppHandler.ContentManager.BrowserClosed(m_title);
		}

        public void UpdateGridview()
        {
            string strGridviewId = String.Format("gridview_cid{0}", AppHandler.DongleId);
            this.webBrowser1.Document.InvokeScript("updateGridView", new object[] { strGridviewId });
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // DHTML Zugriff auf HTML-Dokument
            IHTMLDocument2 doc = (IHTMLDocument2)((WebBrowser)sender).Document.DomDocument;
            // Sind wir im MainFrame?
            //FileInfo fi = new FileInfo((string)e.uRL);
            //if(doc.url.IndexOf(fi.Name)>=0)
            string htmName = Path.GetFileNameWithoutExtension(e.Url.ToString());
            if (doc.url.IndexOf(htmName) >= 0)
            {
                DHTMLParser dhtmlParser = new DHTMLParser(doc);
                ((WebBrowser)sender).Document.InvokeScript("execScript", new object[] { strJavaScript, "JavaScript" });
                string text = "";
                dhtmlParser.FindText("LabelAuthentication", ref text);
                if (text.IndexOf("Authentification") >= 0)
                {
                    if (text.IndexOf("OK") > 0)
                    {
                        AppHandler.SetTimeLimit(DateTime.Now);
                        webBrowser1.Navigate(m_url + "&OkFailed=OK");
                    }
                    else
                    {
                        webBrowser1.Navigate(m_url + "&OkFailed=FAILED");
                    }
                }
            }
        }
	}


}
