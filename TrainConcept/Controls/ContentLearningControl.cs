using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Forms;
using SoftObject.SOComponents.Controls;
using SoftObject.SOComponents.Forms;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Interfaces;
using SoftObject.TrainConcept.Libraries;
using PopupContainerControl = DevComponents.DotNetBar.PopupContainerControl;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Summary description for FrmContentLearning.
	/// </summary>
    public partial class ContentLearningControl : ActivateableUserControl, IContentNavigationCtrl
    {
        private const int msCheckStopwatchesTime = 2000;

        private ContentBrowser contentBrowser;
		private string m_work;
		private FrmContent m_parentContent=null;
		private bool m_isActive=false;
		private string m_gotoMap=null;
		private string m_gotoWork=null;
		private int	   m_gotoPageId=-1;
		private string m_jumpWork=null;
		private int    m_jumpPage=-1;
        private AppHandler AppHandler = Program.AppHandler;

		public string Work
		{
			get
			{
				return m_work;
			}
		}

		public bool IsActive
		{
			get
			{
				return m_isActive;
			}
		}


		public string JumpWork
		{
			get{return m_jumpWork;}
			set
			{
				m_jumpWork=value;
				m_parentContent.CtrlBar.BtnJump.Show();
			}
		}

		public int JumpPage
		{
			get{return m_jumpPage;}
			set{m_jumpPage=value;}
		}

        public void SetPageId(int iPageId)
        {
            m_parentContent.CtrlBar.PageId = iPageId;
        }

        public int GetPageId()
        {
            return m_parentContent.CtrlBar.PageId;
        }

        public void SetMaxPages(int iPages)
        {
            m_parentContent.CtrlBar.MaxPages = iPages;
        }

        public void SetVideo(string strVideoPath)
        {
            m_parentContent.CtrlBar.BtnVideo.Visible = (strVideoPath.Length>0);
        }

        public void SetAnimation(string strAnimPath1, string strAnimPath2)
        {
            m_parentContent.CtrlBar.BtnAnim.Visible = (strAnimPath1.Length>0);
        }

        public void SetGrafic(int iImgId, int iImgCnt)
        {
            if (iImgId>=0)
            {
                if (iImgCnt>1)
                {
                    string sTxt = AppHandler.LanguageHandler.GetText("CONTROLBAR", "Grafic", "Grafik");
                    m_parentContent.CtrlBar.BtnGrafic.Text = String.Format("{0} {1}/{2}", sTxt, iImgId + 1, iImgCnt);
                }
                else
                    m_parentContent.CtrlBar.BtnGrafic.Text = "";
                m_parentContent.CtrlBar.BtnGrafic.Visible = true;
            }
            else
                m_parentContent.CtrlBar.BtnGrafic.Visible = false;
        }

        public void SetSimulation(bool bIsMill, string strSimulPath1, string strSimulPath2)
        {
            m_parentContent.CtrlBar.BtnSimulation.Visible = (strSimulPath1.Length > 0);
        }

        public void AddDocument(string strTitle, int typeId)
        {
            m_parentContent.CtrlBar.AddDocumentButton(strTitle,typeId);
        }

        public void ClearDocuments()
        {
            m_parentContent.CtrlBar.ClearDocuments();
        }

		public ContentLearningControl(FrmContent parentContent,string work)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.timerStopwatches.Enabled = true;
            this.timerStopwatches.Interval = msCheckStopwatchesTime;

			m_work = work;
			m_parentContent = parentContent;
			this.Text = work;

            contentBrowser = new ContentBrowser(m_webBrowser1, this, AppHandler.MainForm.LibOverview, m_work,
                (bIsEnabled) => { m_parentContent.CtrlBar.BtnWorkout.Enabled = bIsEnabled; });
            contentBrowser.OnContentChange += OnContentBrowser_ContentChanged;
		}


        public override void SetActive(bool bIsOn)
        {
            base.SetActive(bIsOn);
            if (m_isActive != bIsOn)
            {
                m_isActive = bIsOn;
                contentBrowser.IsActive = m_isActive;
                if (m_isActive)
                {
                    m_parentContent.CtrlBar.BtnBack.Click += new EventHandler(OnBtnBack);
                    m_parentContent.CtrlBar.BtnForward.Click += new EventHandler(OnBtnForward);
                    m_parentContent.CtrlBar.BtnVideo.Click += new EventHandler(OnBtnVideo);
                    m_parentContent.CtrlBar.BtnAnim.Click += new EventHandler(OnBtnAnim);
                    m_parentContent.CtrlBar.BtnGrafic.Click += new EventHandler(OnBtnGrafic);
                    m_parentContent.CtrlBar.BtnSimulation.Click += new EventHandler(OnBtnSimulation);
                    m_parentContent.CtrlBar.BtnJump.Click += new EventHandler(OnBtnJump);

                    if (m_jumpWork != null)
                        m_parentContent.CtrlBar.BtnJump.Show();
                    else
                        m_parentContent.CtrlBar.BtnJump.Hide();

                    AppHandler.MainForm.ToolBar.Items["bNoticeCenter"].Visible = true;

                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).PopupContainerLoad += new System.EventHandler(this.OnNoticePopupLoad);
                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).Click += new System.EventHandler(this.OnNoticeClicked);

                    AppHandler.MainForm.ToolBar.Items["bGlossary"].Visible = true;
                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bGlossary"]).PopupContainerLoad += new System.EventHandler(this.OnGlossaryPopupLoad);
                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bGlossary"]).PopupContainerUnload += new System.EventHandler(this.OnGlossaryPopupUnload);
                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bGlossary"]).Click += new System.EventHandler(this.OnGlossaryClicked);
                    AppHandler.MainForm.ToolBar.RecalcLayout();

                    AppHandler.MainForm.ToolBar.Items["bPrint"].Enabled = true;
                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bPrint"]).Click += new System.EventHandler(this.OnPrintClicked);

                    m_parentContent.ShowChartControl(false);
                    SetMaxPages(contentBrowser.MaxPages);

                    if (m_parentContent.RecentProgress.Count > 0)
                        foreach (var w in m_parentContent.RecentProgress)
                        {
                            if (w.Value.Item1 == m_work)
                            {
                                m_parentContent.CtrlBar.PageId = w.Value.Item2;
                            }
                        }

                    ShowPage();
                }
                else
                {
                    AppHandler.MainForm.ToolBar.Items["bNoticeCenter"].Visible = false;
                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).PopupContainerLoad -= new System.EventHandler(this.OnNoticePopupLoad);
                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).Click -= new System.EventHandler(this.OnNoticeClicked);
                    AppHandler.MainForm.NoticeBar.Visible = false;
                    //if (AppHandler.MainForm.Notice != null)
                    //    AppHandler.MainForm.Notice.Save();

                    AppHandler.MainForm.ToolBar.Items["bGlossary"].Visible = false;
                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bGlossary"]).PopupContainerLoad -= new System.EventHandler(this.OnGlossaryPopupLoad);
                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bGlossary"]).Click -= new System.EventHandler(this.OnGlossaryClicked);

                    AppHandler.MainForm.ToolBar.Items["bPrint"].Enabled = false;
                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bPrint"]).Click -= new System.EventHandler(this.OnPrintClicked);

                    AppHandler.MainForm.ToolBar.RecalcLayout();

                    m_parentContent.CtrlBar.BtnBack.Click -= new EventHandler(OnBtnBack);
                    m_parentContent.CtrlBar.BtnForward.Click -= new EventHandler(OnBtnForward);
                    m_parentContent.CtrlBar.BtnVideo.Click -= new EventHandler(OnBtnVideo);
                    m_parentContent.CtrlBar.BtnAnim.Click -= new EventHandler(OnBtnAnim);
                    m_parentContent.CtrlBar.BtnGrafic.Click -= new EventHandler(OnBtnGrafic);
                    m_parentContent.CtrlBar.BtnSimulation.Click -= new EventHandler(OnBtnSimulation);
                    m_parentContent.CtrlBar.BtnJump.Click -= new EventHandler(OnBtnJump);
                }
            }
        }


		// ShowPage
		// bestimmte HTML-Seite laden
		public void ShowPage()
		{
            contentBrowser.ShowPage();
		    UpdateNotices();
		}

		public void SetPage(int pageId)
		{
			m_parentContent.CtrlBar.PageId = pageId;
            contentBrowser.SetPage(pageId);
		}

		public void ReloadPage()
		{
            contentBrowser.ReloadPage();
		}

        public void UpdatePage()
        {
            contentBrowser.UpdateWork();
        }

        private void UpdateNotices()
		{
			NoticeItemCollection aNotices = new NoticeItemCollection();
			if (AppHandler.NoticeManager.Find(ref aNotices,AppHandler.MainForm.ActualUserName,m_work,m_parentContent.CtrlBar.PageId)>0)
				((ButtonItem) AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).ImageIndex = 3;
			else
				((ButtonItem) AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).ImageIndex = 4;
		}
		

		// ClearTextFormatters
		// Textformatierungen löschen
		// Syntax: {{*[keyWord]*}}
		// --> Änderungen durchgeführt?
		private void ClearTextFormatters(ref string _text)
		{
			int keyStart;
			// Startstring suchen
			while ((keyStart=_text.IndexOf("{{", StringComparison.Ordinal))>=0)
			{
				// Ende suchen
				int keyEnd = _text.IndexOf("}}", StringComparison.Ordinal);
				if (keyEnd<0)
					break;

				string toReplace=_text.Substring(keyStart,keyEnd-keyStart+2);
				_text=_text.Replace(toReplace,"");
			}
		}

		#region Ereignisbehandlung Browser Buttons
		private void OnBtnBack(object sender, System.EventArgs e)
		{
            contentBrowser.OnBtnBack();
			m_parentContent.CheckPageButtons();
			AppHandler.MainForm.NoticeBar.Visible = false;
		}
		
		private void OnBtnForward(object sender, System.EventArgs e)
		{
            contentBrowser.OnBtnForward();
			m_parentContent.CheckPageButtons();
			AppHandler.MainForm.NoticeBar.Visible = false;
		}

		private void OnBtnVideo(object sender, System.EventArgs e)
		{
            contentBrowser.OnBtnVideo();
		}

		private void OnBtnAnim(object sender, System.EventArgs e)
		{
            contentBrowser.OnBtnAnim();
		}

		private void OnBtnGrafic(object sender, System.EventArgs e)
		{
            contentBrowser.OnBtnGrafic();
		}

        private void OnBtnSimulation(object sender, System.EventArgs e)
        {
            contentBrowser.OnBtnSimulation();
		}

		private void OnBtnJump(object sender, System.EventArgs e)
		{
            if (m_jumpWork != null)
            {
                string toMap = AppHandler.MapManager.FindMapByWorking(m_jumpWork);
                AppHandler.ContentManager.JumpToLearnmap(AppHandler.MainForm, m_work, contentBrowser.PageId, toMap, m_jumpWork, m_jumpPage, false);
                m_jumpWork = null;
            }
		}


		#endregion


		private void label1_Click(object sender, System.EventArgs e)
		{
		}

		private void FrmContentLearning_VisibleChanged(object sender, System.EventArgs e)
		{
		}

		private void OnNoticePopupLoad(object sender, System.EventArgs e)
		{
			ButtonItem item=sender as ButtonItem;

			PopupContainerControl container=item.PopupContainerControl as PopupContainerControl;
            SONoticeCtrl notice = new SONoticeCtrl(GetNoticeTitle, CanDeleteNotice);
			notice.BackColor = System.Drawing.Color.FloralWhite;
			notice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			notice.Name = "NoticeCenter";
			notice.Size = new System.Drawing.Size(420,120);
			notice.TabIndex = 2;
			notice.OnNotice += new SONoticeCtrl.NoticeHandler(OnNotice);

			NoticeItemCollection aNotices=new NoticeItemCollection();
			AppHandler.NoticeManager.Find(ref aNotices,AppHandler.MainForm.ActualUserName,m_work,m_parentContent.CtrlBar.PageId);
			for(int i=0;i<aNotices.Count;++i)
				notice.SetNotice(i,aNotices[i].title,aNotices[i].fileName);

			container.Controls.Add(notice);
			notice.Location=container.ClientRectangle.Location;
			container.ClientSize=notice.Size;
			item.AutoCollapseOnClick = false;
		}

		private void OnPrintClicked(object sender, System.EventArgs e)
		{
			contentBrowser.Print();
		}

		private void OnNoticeClicked(object sender, System.EventArgs e)
		{
			ButtonItem item=sender as ButtonItem;
			item.Expanded = true;
		}

		private void OnGlossaryClicked(object sender, System.EventArgs e)
		{
			ButtonItem item=sender as ButtonItem;
			item.Expanded = true;
		}

		private void OnGlossaryPopupLoad(object sender, System.EventArgs e)
		{
			ButtonItem item=sender as ButtonItem;

			PopupContainerControl container=item.PopupContainerControl as PopupContainerControl;

			SOGlossar glossar=new SOGlossar();
			glossar.BackColor = System.Drawing.Color.FloralWhite;
			glossar.Name = "Glossar";
			glossar.TabIndex = 2;
			glossar.OnGlossar += new GlossarHandler(OnGlossar);

			container.Controls.Add(glossar);
			glossar.Location=container.ClientRectangle.Location;
			container.ClientSize=glossar.Size;
			item.AutoCollapseOnClick = false;
		}

		private void OnGlossaryPopupUnload(object sender, System.EventArgs e)
		{
			ButtonItem item=sender as ButtonItem;
			if (m_gotoMap!=null)
			{
				AppHandler.ContentManager.JumpToLearnmap(AppHandler.MainForm,m_work,contentBrowser.PageId,m_gotoMap,m_gotoWork,m_gotoPageId,true);
				m_gotoMap=null;
				m_gotoWork=null;
				m_gotoPageId=-1;
			}
		}
			
		public void OnNotice(object sender,ref SONoticeCtrl.NoticeEventArgs nea)
		{
			// Neue Notiz erstellen
			if (nea.Action == SONoticeCtrl.NoticeEventAction.New)
			{
                int nid = AppHandler.NoticeManager.CreateNotice(AppHandler.MainForm.ActualUserName, nea.Title,m_work, m_parentContent.CtrlBar.PageId,false);
                if (nid >= 0)
                {
                    NoticeItem noticeItem = AppHandler.NoticeManager.GetNotice(nid);

                    string dirName = String.Format("{0}\\{1}\\notices\\", AppHandler.NoticeManager.GetNoticePath(), AppHandler.MainForm.ActualUserName);
                    string filePath = dirName + noticeItem.fileName;

                    nea.Title = noticeItem.title;
                    nea.FileName = filePath;

                    if (!System.IO.File.Exists(filePath))
                        using (var myFile = System.IO.File.Create(filePath))
                        {

                        }
    
                    AppHandler.NoticeManager.Save();
                    AppHandler.MainForm.Notice.SaveFile();

                    AppHandler.MainForm.ShowNotice(nea.Title, filePath);

                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).Expanded = false;
                }
			}
			else if (nea.Action == SONoticeCtrl.NoticeEventAction.Open)
			{
				int nid = AppHandler.NoticeManager.Find(nea.FileName);
                if (nid >= 0)
                {
                    NoticeItem noticeItem = AppHandler.NoticeManager.GetNotice(nid);

                    string dirName = String.Format("{0}\\{1}\\notices\\", AppHandler.NoticeManager.GetNoticePath(), AppHandler.MainForm.ActualUserName);
                    string filePath = dirName + noticeItem.fileName;

                    if (!System.IO.File.Exists(filePath))
                    {
                        AppHandler.NoticeManager.DeleteNotice(nid);
                        AppHandler.NoticeManager.Save();
                        return;
                    }

                    AppHandler.MainForm.ShowNotice(nea.Title, filePath);

                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).Expanded = false;
                }
			}
			else if (nea.Action == SONoticeCtrl.NoticeEventAction.Delete)
			{
				int nid = AppHandler.NoticeManager.Find(nea.FileName);
				if (nid>=0)
				{
                    NoticeItem noticeItem = AppHandler.NoticeManager.GetNotice(nid);
                    string dirName = String.Format("{0}\\{1}\\notices\\", AppHandler.NoticeManager.GetNoticePath(), AppHandler.MainForm.ActualUserName);
                    string filePath = dirName + noticeItem.fileName;

                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    AppHandler.NoticeManager.DeleteNotice(nid);
                    AppHandler.NoticeManager.Save();

					if (AppHandler.MainForm.NoticeBar.Visible &&
						AppHandler.MainForm.Notice!=null &&
                        AppHandler.MainForm.Notice.Filename == filePath)
					{
						AppHandler.MainForm.NoticeBar.Visible = false;
					}
				}
			}
			else if (nea.Action == SONoticeCtrl.NoticeEventAction.Rename)
			{
				int nid = AppHandler.NoticeManager.Find(nea.FileName);
                if (nid >= 0 && nea.Title.Length>0)
				{
					AppHandler.NoticeManager.SetNoticeTitle(nid,nea.Title);
					AppHandler.NoticeManager.Save();

                    NoticeItem ni = AppHandler.NoticeManager.GetNotice(nid);
                    string dirName = String.Format("{0}\\{1}\\notices\\", AppHandler.NoticeManager.GetNoticePath(), AppHandler.MainForm.ActualUserName);
                    string filePath = dirName + ni.fileName;

                    AppHandler.MainForm.ShowNotice(nea.Title, filePath);

                    ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).Expanded = false;
				}
			}
			else if (nea.Action == SONoticeCtrl.NoticeEventAction.Export)
			{
				int nid = AppHandler.NoticeManager.Find(nea.FileName);
				if (nid>=0)
				{
                    NoticeItem noticeItem = AppHandler.NoticeManager.GetNotice(nid);
                    string dirName = String.Format("{0}\\{1}\\notices\\", AppHandler.NoticeManager.GetNoticePath(), AppHandler.MainForm.ActualUserName);
                    string filePath = dirName + noticeItem.fileName;

					((ButtonItem) AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).Expanded = false;

                    var DP = new FrmDirectoryPicker();
					if (DP.ShowDialog()==DialogResult.OK)
					{
                        File.Copy(filePath, DP.SelectedDirectory + '\\' + nea.Title + ".rtf");
					}
				}
			}
			else if (nea.Action == SONoticeCtrl.NoticeEventAction.Import)
			{
				((ButtonItem) AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).Expanded = false;

                System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();

				dlg.DefaultExt = "rtf";
				dlg.Filter= "RTF-Dateien (*.rtf)|*.rtf";
				dlg.FilterIndex=0;
				dlg.Title = "Öffnen";
		
				if (dlg.ShowDialog()==DialogResult.OK)
				{
                    string title = Path.GetFileNameWithoutExtension(dlg.FileName);
                    
                    int nid = AppHandler.NoticeManager.CreateNotice(AppHandler.MainForm.ActualUserName, title, m_work, m_parentContent.CtrlBar.PageId,false);
					if (nid>=0)
					{
                        SONoticeCtrl noticeCtrl = sender as SONoticeCtrl;
                        NoticeItem noticeItem = AppHandler.NoticeManager.GetNotice(nid);

                        string dirName = String.Format("{0}\\{1}\\notices\\", AppHandler.NoticeManager.GetNoticePath(), AppHandler.MainForm.ActualUserName);
                        string filePath = dirName + noticeItem.fileName;

                        if (!Directory.Exists(dirName))
                            Directory.CreateDirectory(dirName);
                        File.Copy(dlg.FileName, filePath, true);
                        noticeCtrl.SetNotice(nid, title, filePath);

                        noticeItem.title = title;

                        AppHandler.MainForm.ShowNotice(title, filePath);
					}
				}
			}

			UpdateNotices();
		}

		private void OnGlossar(object sender,ref GlossarEventArgs gea)
		{
			KeywordCollection aKeywords = (KeywordCollection) AppHandler.MapManager.GetAllKeywords();

			if (gea.Char!=' ')
			{
				if (aKeywords.Count>0)
				{
					ArrayList aStrings = new ArrayList();

					for(int i=0;i<aKeywords.Count;++i)
					{
						if (aKeywords.Item(i).isGlossar && 
							(aKeywords.Item(i).text[0] == gea.Char ||
							 aKeywords.Item(i).text[0] == Char.ToLower(gea.Char)))
							aStrings.Add(aKeywords.Item(i).text);
					}
					gea.Keywords = aStrings;
				}
			}
			else
			{
				KeywordActionItem keyword = aKeywords.Find(gea.Keyword);

				if (!gea.GotoKeyword)
				{
					if (aKeywords!=null)
					{
						if (keyword!=null)
						{
							string sDescription=keyword.description;
							ClearTextFormatters(ref sDescription);
							gea.Description = sDescription;
						}
					}
				}
				else
				{
					string path=aKeywords.GetPath(keyword);
					if (path!=null)
					{
						m_gotoMap=AppHandler.MapManager.FindMapByWorking(path);
						m_gotoWork=path;
						m_gotoPageId=aKeywords.GetPageId(keyword)+1;

						ButtonItem bGlossary = (ButtonItem) AppHandler.MainForm.ToolBar.Items["bGlossary"];					
						bGlossary.Expanded = false;

						/*
						// Fokus auf aktuellen Inhalt setzen-> schließt Glossary
						SoftObject.UtilityLibrary.Win32.WindowsAPI.PostMessage(AppHandler.MainForm.Handle,(int) Msg.WM_SETFOCUS,0,0);
						*/
					}
					else
						gea.GotoKeyword = false;
				}
			}
		}

		private void FrmContentLearning_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
			{
				AppHandler.LibManager.Reload(m_work);
				ShowPage();		
				e.Handled=true;
			}
			else if (e.KeyCode == Keys.Left)
			{
				if (m_parentContent.CtrlBar.BtnBack.Enabled)
					OnBtnBack(this,System.EventArgs.Empty);
			}
			else if (e.KeyCode == Keys.Right)
			{
				if (m_parentContent.CtrlBar.BtnForward.Enabled)
					OnBtnForward(this,System.EventArgs.Empty);
			}
		}

        private void OnContentBrowser_ContentChanged(object sender, ref ContentBrowserEventArgs ea)
		{
            if (ea.Action == ContentBrowserEventAction.AddDocument)
            {
                DocumentActionItem docAction = ea.Item as DocumentActionItem;
                if (docAction.typeId == 0) // type 0 = rtf -> Notiz erstellen und anzeigen
                {
                    string[] aTitles;
                    Utilities.SplitPath(m_work, out aTitles);
                    string filePathOld = AppHandler.GetDocumentsFolder(aTitles[0]) + "\\" + Path.GetFileName(docAction.fileName);

                    bool bCreateNewNotice=true;
                    int iId = AppHandler.NoticeManager.Find(AppHandler.MainForm.ActualUserName, docAction.id);
                    if (iId>=0)
                    {
                        var ni = AppHandler.NoticeManager.GetNotice(iId);
                        string dirName = String.Format("{0}\\{1}\\notices\\", AppHandler.NoticeManager.GetNoticePath(), AppHandler.MainForm.ActualUserName);
                        string filePath = dirName + ni.fileName;
                        if (File.Exists(filePath))
                            bCreateNewNotice = false;
                    }
                    else
                        iId = AppHandler.NoticeManager.CreateNotice(AppHandler.MainForm.ActualUserName, docAction.id, ea.ContentPath, ea.PageId, true);


                    if (bCreateNewNotice)
                    {
                        if (iId >= 0)
                        {
                            var noticeNew = AppHandler.NoticeManager.GetNotice(iId);
                            string dirName = String.Format("{0}\\{1}\\notices\\", AppHandler.NoticeManager.GetNoticePath(), AppHandler.MainForm.ActualUserName);
                            string filePathNew = dirName + noticeNew.fileName;

                            if (File.Exists(filePathOld))
                                File.Copy(filePathOld, filePathNew, true);

                            AppHandler.NoticeManager.Save();
                            AppHandler.MainForm.ShowNotice(docAction.id, filePathNew, filePathOld);
                        }
                    }
                    else
                    {
                        var ni = AppHandler.NoticeManager.GetNotice(iId);
                        string dirName = String.Format("{0}\\{1}\\notices\\", AppHandler.NoticeManager.GetNoticePath(), AppHandler.MainForm.ActualUserName);
                        string filePath = dirName + ni.fileName;
                        AppHandler.MainForm.ShowNotice(ni.title, filePath,filePathOld);
                    }
                }
                else if (docAction.typeId==1 || docAction.typeId==2 || docAction.typeId == 3) // typeId=1 -> pdf, typeId=2->pptx, typeId=3->xAPI: Anzeigen
                {
                    foreach (BubbleButton bb in m_parentContent.CtrlBar.DocumentButtons)
                    {
                        if (bb.Name == docAction.id)
                            bb.Click += OnShowDocument;
                    }
                }
            }
		}


        private void FrmContentLearning_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.contentBrowser.Dispose();
        }

        private bool GetNoticeTitle(ref string strTitle)
        {
            ((ButtonItem)AppHandler.MainForm.ToolBar.Items["bNoticeCenter"]).Expanded = false;

            FrmNewNotice dlg = new FrmNewNotice();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                strTitle = dlg.Title;
                return true;
            }
            return false;
        }

        private bool CanDeleteNotice(string strTitle)
        {
            var iId=AppHandler.NoticeManager.Find(AppHandler.MainForm.ActualUserName, strTitle);
            var ni = AppHandler.NoticeManager.GetNotice(iId);
            if (ni != null)
                return (ni.workedOutState < 0);
            return true;
        }

        private void OnShowDocument(object sender, ClickEventArgs e)
        {
            var bubbtn = sender as BubbleButton;
            var pageItem=AppHandler.LibManager.GetPage(m_work,contentBrowser.PageId-1);
            if (pageItem!=null && pageItem.PageActions!=null)
            {
                foreach(var a in pageItem.PageActions)
                    if (a is DocumentActionItem && a.id == bubbtn.Name)
                    {
                        var docAction = a as DocumentActionItem;
                        AppHelpers.ShowDocument(m_work, docAction);
                    }
            }
        }

        private void timerStopwatches_Tick(object sender, EventArgs e)
        {
            contentBrowser.WorkoutStopwatches();
        }
	}
}
