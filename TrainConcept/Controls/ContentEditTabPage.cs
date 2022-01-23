using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data.Extensions;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Libraries;
using SoftObject.UtilityLibrary;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Summary description for ContentEditTabPage.
	/// </summary>
	// Show Page 
	public delegate void ShowPageCallback(object parentPage,bool bUpdateTabPage);

	public partial class ContentEditTabPage : System.Windows.Forms.UserControl
	{
		private readonly string m_sPath="";
		private readonly string m_contentFolder="";
		private int    m_pageId=0;
		private ArrayList m_aRecords= new ArrayList();
		private PageItem m_page=null;
        private ContentEditRecord m_selectedRecord = null;
		private readonly ShowPageCallback  m_fnShowPage=null;
        private int m_iSelectedTemplate = -1;
        private AppHandler AppHandler = Program.AppHandler;
        private Action<bool> ActivateDeleteButtonAction { get; set; }
        private Func<string,string> GetFullFilenameFunc { get; set; }

        public int SelectedTemplateId
        {
            get
            {
                return m_iSelectedTemplate;
            }
            set
            {
                if (m_iSelectedTemplate!=value)
                    this.comboBox1.SelectedIndex = value;
            }
        }

        public int PageId
        {
            get { return m_pageId; }
            set { m_pageId = value; }
        }

	
		public string TemplatePath
		{
			get
			{
				if (m_page!=null)
					return 	m_contentFolder+"\\Templates\\"+m_page.templateName;
				else
					return "";
			}
		}
		
		public PageItem Page
		{
			get
			{
				return m_page;
			}
		}
		


		public ContentEditTabPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		public ContentEditTabPage(string sPath,int pageId,ShowPageCallback fnShowPage,Action<bool> activateDeleteButtonaAction,
                                  Func<string, string> getFullFilenameFunc)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		    ActivateDeleteButtonAction = activateDeleteButtonaAction;
            GetFullFilenameFunc = getFullFilenameFunc;

            this.comboBox1.Items.AddRange(AppHandler.Templates.Values.ToArray());

			m_sPath = sPath;
			m_pageId = pageId;
			m_fnShowPage = fnShowPage;

			// Folder für Inhalte bestimmen
			m_contentFolder = AppHandler.ContentFolder + '\\' + AppHandler.LibManager.GetFileName(m_sPath);

			m_page=AppHandler.LibManager.GetPage(m_sPath,m_pageId);
			if (m_page!=null)
			{
                string strTempl = Utilities.GetTemplateName(m_page.templateName);
                if (AppHandler.Templates.ContainsValue(strTempl))
                {
                    int iKey = AppHandler.Templates.FirstOrDefault(x => x.Value == strTempl).Key;
                    comboBox1.SelectedIndex = iKey;
                }

                if (m_page.PageActions != null)
                {
                    for (int i = 0; i < m_page.PageActions.Length; ++i)
                        if (m_page.PageActions[i] is VideoActionItem)
                        {
                            VideoActionItem vi = (VideoActionItem)m_page.PageActions[i];
                            if (!FileAndDirectoryHelpers.IsValidFileName(Path.GetFileName(vi.fileName), true))
                            {
                                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Filename_XXX_contains_invalid_chars", "Der Dateiname \"{0}\" enthält ungültige Zeichen!");
                                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                txt = String.Format(txt, vi.fileName);
                                MessageBox.Show(txt, cap, MessageBoxButtons.OK);
                            }
                        }
                }
			}
		}

        private void UpdatePageActionTreelist()
        {
            m_aRecords = new ArrayList();
            if (m_page.PageActions != null)
            {
                for (int i = 0; i < m_page.PageActions.Length; ++i)
                    if (!(m_page.PageActions[i] is DocumentActionItem))
                        m_aRecords.Add(new ContentEditRecord(m_page, m_page.PageActions[i], i));
            }

            try
            {
                // da stürzte es manchmal ab. Grund: ClearNodes() ruft indirekt treeList1_FocusedNodeChanged() (->in dieser Klasse hier) auf und da war eine NullReference-Exception.
                // Ich laß den code deswegen so drin, Problem gibt's aber jetzt nicht mehr.
                treeList1.ClearNodes();
            }
            catch (System.Exception ex)
            {
                string strText = ex.ToString();
            }
            finally
            {
            }

            if (m_aRecords.Count > 0)
            {
                treeList1.DataSource = m_aRecords;
                //treeList1.BestFitColumns();
                treeList1.ExpandAll();
            }
            else
                treeList1.DataSource = null;
        }

        public void AddPageAction(ActionItem item)
        {
            PageItem pg = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
            if (pg != null && pg.PageActions!=null)
            {
                AppHandler.LibManager.AddPageAction(pg, item);
                AppHandler.LibManager.SetModified(m_sPath);
                m_page = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
                UpdatePageActionTreelist();
                m_fnShowPage(this,false);
            }
        }

        public bool DelDocumentPageAction(string docActionTitle)
        {
            PageItem pg = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
            if (pg != null && pg.PageActions != null)
            {
                for (int i = 0; i <= pg.PageActions.Length; ++i)
                    if (pg.PageActions[i] is DocumentActionItem && pg.PageActions[i].id == docActionTitle)
                    {
                        AppHandler.LibManager.DeletePageAction(pg, i);
                        AppHandler.LibManager.SetModified(m_sPath);
                        m_page = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
                        return true;
                    }
            }
            return false;
        }

        public bool HasDocumentPageAction(int typeId)
        {
            PageItem pg = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
            if (pg != null && pg.PageActions != null)
                return (pg.PageActions.Any(x => x is DocumentActionItem && (x as DocumentActionItem).typeId == typeId));
            return false;
        }

        public bool HasDocumentPageAction(string docActionTitle)
        {
            PageItem pg = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
            if (pg != null && pg.PageActions != null)
                return (pg.PageActions.Any(x => x is DocumentActionItem && (x as DocumentActionItem).id == docActionTitle));
            return false;
        }

        public void DeleteSelectedAction()
        {
            if (m_selectedRecord != null && m_selectedRecord.Type != ContentEditRecord.ActionType.Text)
            {
                PageItem pg = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
                if (pg != null)
                {
                    string txt1 = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_really_want_to_delete_PageAction", "Wollen sie den/das {0} wirklich löschen?");
                    txt1 = String.Format(txt1, m_selectedRecord.Type);
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    if (MessageBox.Show(txt1, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var actItem = AppHandler.LibManager.GetPageAction(pg, m_selectedRecord.Id);
                        if (actItem != null)
                        {
                            string strTypeString = "";
                            string strFilename = "";
                            switch (m_selectedRecord.Type)
                            {
                                case ContentEditRecord.ActionType.Video: strTypeString = "Videodatei";
                                    strFilename = (actItem as VideoActionItem).fileName;
                                    break;
                                case ContentEditRecord.ActionType.Animation: strTypeString = "Animationsdatei";
                                    strFilename = (actItem as AnimationActionItem).fileName1;
                                    break;
                                case ContentEditRecord.ActionType.Simulation: strTypeString = "Simulationsdatei";
                                    strFilename = (actItem as SimulationActionItem).fileName1;
                                    break;
                                case ContentEditRecord.ActionType.Image: strTypeString = "Imagedatei";
                                    strFilename = (actItem as ImageActionItem).fileName;
                                    break;
                            }

                            if (strFilename.Length > 0)
                            {
                                strFilename = GetFullFilenameFunc(strFilename);

                                txt1 = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_delete_related_typefile_too", "Wollen sie die zugehörige {0} auch löschen?");
                                txt1 = String.Format(txt1, strTypeString);
                                cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                if (MessageBox.Show(txt1, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    try
                                    {
                                        File.Delete(strFilename);
                                    }
                                    catch (System.Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                            }
                        }

                        AppHandler.LibManager.DeletePageAction(pg, actItem.id);
                        AppHandler.LibManager.SetModified(m_sPath);

                        m_page = AppHandler.LibManager.GetPage(m_sPath, m_pageId);

                        UpdatePageActionTreelist();
                        m_fnShowPage(this, true);
                    }
                }
            }
        }

        public static bool CreatePageItem(int iTemplId, ref PageItem pageItem)
        {
            if (iTemplId < 0)
                iTemplId = 0;
            if (iTemplId >= 0 && iTemplId < Program.AppHandler.Templates.Count)
            {
                pageItem = new PageItem(Program.AppHandler.Templates.ElementAt(iTemplId).Value + "_f.htm", true);
                String strTextItems = Program.AppHandler.PageActionTextItems.ElementAt(iTemplId).Value;
                string[] aTextItems = strTextItems.Split(new char[] { ',' });
                foreach (var t in aTextItems)
                    Program.AppHandler.LibManager.AddPageAction(pageItem, new TextActionItem(t, t));
                return true;
            }
            return false;
        }

        private static void CopyPageItem(PageItem pageItemFrom, ref PageItem pageItemTo, int iTemplId)
        {
            String strTextItems = Program.AppHandler.PageActionTextItems.ElementAt(iTemplId).Value;
            string[] aTextItems = strTextItems.Split(new char[] { ',' });
            foreach (var t in aTextItems)
            {
                int iFoundFrom = pageItemFrom.PageActions.FindIndex(x => x.id == t);
                int iFoundTo = pageItemTo.PageActions.FindIndex(x => x.id == t);
                if (iFoundFrom >= 0 && iFoundTo>=0)
                {
                    var actFrom = pageItemFrom.PageActions[iFoundFrom];
                    var actTo = pageItemTo.PageActions[iFoundTo];
                    if (actFrom.GetType().Equals(actTo.GetType()))
                        actTo.CopyFrom(actFrom);
                }
            }
        }


        private void repositoryItemTextEdit1_Leave(object sender, System.EventArgs e)
		{
			DevExpress.XtraEditors.TextEdit txtEdit = sender as DevExpress.XtraEditors.TextEdit;
			PageItem pg = AppHandler.LibManager.GetPage(m_sPath,m_pageId);
            if (pg != null && txtEdit != null && treeList1.FocusedNode!=null)
			{
				ActionItem act=AppHandler.LibManager.GetPageAction(pg,treeList1.FocusedNode.Id);
				ActionItem newAct = (ActionItem) act.Clone();
				if (newAct!=null)
				{
					if (newAct is TextActionItem)
						((TextActionItem)newAct).text = txtEdit.Text;
					else if (newAct is KeywordActionItem)
						((KeywordActionItem)newAct).text = txtEdit.Text;
					else if (newAct is ImageActionItem)
						((ImageActionItem)newAct).fileName = txtEdit.Text;
					else if (newAct is AnimationActionItem)
						((AnimationActionItem)newAct).fileName1 = txtEdit.Text;
					else if (newAct is VideoActionItem)
						((VideoActionItem)newAct).fileName = txtEdit.Text;
					else if (newAct is SimulationActionItem)
						((SimulationActionItem)newAct).fileName1 = txtEdit.Text;
				}

				AppHandler.LibManager.SetPageAction(pg,newAct,treeList1.FocusedNode.Id);
				AppHandler.LibManager.SetModified(m_sPath);
				//AppHandler.LibManager.Reload(m_sPath);

				m_page = AppHandler.LibManager.GetPage(m_sPath,m_pageId);
                UpdatePageActionTreelist();

				m_fnShowPage(this,false);
			}
		}

        private void repositoryItemTextEdit1_EditValueChanged(object sender, System.EventArgs e)
        {
        }


        private void repositoryItemTextEdit2_Leave(object sender, System.EventArgs e)
        {
            DevExpress.XtraEditors.TextEdit txtEdit = sender as DevExpress.XtraEditors.TextEdit;
            PageItem pg = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
            if (pg != null && txtEdit != null)
            {
                ActionItem act = AppHandler.LibManager.GetPageAction(pg, treeList1.FocusedNode.Id);
                ActionItem newAct = (ActionItem)act.Clone();
                if (newAct != null)
                {
                    if (newAct is KeywordActionItem)
                        ((KeywordActionItem)newAct).description = txtEdit.Text;
                    else if (newAct is ImageActionItem)
                    {
                        string[] aVals = txtEdit.Text.Split(new char[] { ';' }, 2);
                        if (aVals.Length == 2)
                        {
                            ((ImageActionItem)newAct).left = Convert.ToInt32(aVals[0]);
                            ((ImageActionItem)newAct).top = Convert.ToInt32(aVals[1]);
                        }
                    }
                    else if (newAct is AnimationActionItem)
                        ((AnimationActionItem)newAct).fileName2 = txtEdit.Text;
                    else if (newAct is SimulationActionItem)
                        ((SimulationActionItem)newAct).fileName2 = txtEdit.Text;
                    else
                        return;
                }

                AppHandler.LibManager.SetPageAction(pg, newAct, treeList1.FocusedNode.Id);
                AppHandler.LibManager.SetModified(m_sPath);
                //AppHandler.LibManager.Reload(m_sPath);
                m_page = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
                UpdatePageActionTreelist();

                m_fnShowPage(this,false);
            }
        }

        private void repositoryItemTextEdit2_EditValueChanged(object sender, System.EventArgs e)
        {
        }

        private void repositoryItemTextEdit3_Leave(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.TextEdit txtEdit = sender as DevExpress.XtraEditors.TextEdit;
            PageItem pg = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
            if (pg != null && txtEdit != null)
            {
                ActionItem act = AppHandler.LibManager.GetPageAction(pg, treeList1.FocusedNode.Id);
                ActionItem newAct = (ActionItem)act.Clone();
                newAct.id = txtEdit.Text;

                AppHandler.LibManager.SetPageAction(pg, newAct, treeList1.FocusedNode.Id);
                AppHandler.LibManager.SetModified(m_sPath);
                //AppHandler.LibManager.Reload(m_sPath);
                m_page = AppHandler.LibManager.GetPage(m_sPath, m_pageId);
                UpdatePageActionTreelist();

                m_fnShowPage(this,false);
            }
        }


        private void repositoryItemTextEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
		{
            if (e.Node != null && m_aRecords != null)
			{
                if (e.Node.Id >= 0 && e.Node.Id < m_aRecords.Count)
                {
                    ContentEditRecord rec = (ContentEditRecord)m_aRecords[e.Node.Id];
                    if (rec != null)
                    {
                        m_selectedRecord = rec;
                        ActivateDeleteButtonAction.Invoke(m_selectedRecord != null && m_selectedRecord.Type != ContentEditRecord.ActionType.Text);
                    }
                }
			}
		}


		private void treeList1_DoubleClick(object sender, System.EventArgs e)
		{
            ContentEditRecord rec = (ContentEditRecord)m_aRecords[treeList1.FocusedNode.Id];
			if (rec!=null)
			{
				ActionItem act=AppHandler.LibManager.GetPageAction(m_page,treeList1.FocusedNode.Id);
				if (act!=null)
				{
					ActionItem newAct = (ActionItem) act.Clone();
					if (newAct!=null)
					{
						if (act is TextActionItem)
						{
							TextActionItem textItem = newAct as TextActionItem;
							FrmEditTextAction frm = new FrmEditTextAction(textItem.text);
							if (frm.ShowDialog() != DialogResult.OK || !frm.IsModified)
								return;

							textItem.text = frm.ChangedText;
							rec.Value1 = frm.ChangedText;
						}
						else if (newAct is KeywordActionItem)
						{
							KeywordActionItem keyItem = newAct as KeywordActionItem;
							XFrmEditKeywordAction frm = new XFrmEditKeywordAction(keyItem.text,keyItem.description,keyItem.isTooltip,keyItem.isGlossar);
							if (frm.ShowDialog() != DialogResult.OK || !frm.IsModified)
								return;

							keyItem.text = frm.KeyText;
							keyItem.description = frm.KeyDescription;
							keyItem.isGlossar = frm.IsGlossary;
							keyItem.isTooltip = frm.IsTooltip;
							rec.Value1 = frm.KeyText;
							rec.Value2 = frm.KeyDescription;
						}
						/*
						else if (newAct is ImageActionItem)
							;
						else if (newAct is AnimationActionItem)
							;
						else if (newAct is VideoActionItem)
							;
						else if (newAct is SimulationActionItem)
							;
						*/
					}

					AppHandler.LibManager.SetPageAction(m_page,newAct,treeList1.FocusedNode.Id);
					AppHandler.LibManager.SetModified(m_sPath);
					//AppHandler.LibManager.Reload(m_sPath);

					m_page = AppHandler.LibManager.GetPage(m_sPath,m_pageId);
                    UpdatePageActionTreelist();

					m_fnShowPage(this,false);

				}
			}
		}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex != m_iSelectedTemplate)
            {
                if (m_iSelectedTemplate != -1)
                {
                    PageItem pgNew = new PageItem();
                    CreatePageItem(this.comboBox1.SelectedIndex, ref pgNew);
                    CopyPageItem(m_page, ref pgNew, comboBox1.SelectedIndex);
                    m_page = pgNew;
                    AppHandler.LibManager.SetPage(m_sPath, m_page, m_pageId);
                }

                UpdatePageActionTreelist();

                m_iSelectedTemplate = this.comboBox1.SelectedIndex;
                m_fnShowPage(this,false);
            }
        }

        private void treeList1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedAction();
            }
        }

    };

    public class ContentEditRecord
    {
        public enum ActionType
        {
            Unknown,
            Text,
            Keyword,
            Image,
            Animation,
            Video,
            Simulation
        };
        private ActionItem m_action;
        private PageItem m_page;
        private int m_actionId;

        public string Id
        {
            get
            {
                if (m_action != null)
                {
                    if (m_action.id.Length > 0)
                       return m_action.id;
                }
                return "";
            }

            set
            {
                if (m_action != null)
                    m_action.id = value;
            }
        }

        public ActionType Type
        {
            get
            {
                if (m_action != null)
                {
                    if (m_action is TextActionItem)
                        return ActionType.Text;
                    else if (m_action is KeywordActionItem)
                        return ActionType.Keyword;
                    else if (m_action is ImageActionItem)
                        return ActionType.Image;
                    else if (m_action is AnimationActionItem)
                        return ActionType.Animation;
                    else if (m_action is VideoActionItem)
                        return ActionType.Video;
                    else if (m_action is SimulationActionItem)
                        return ActionType.Simulation;
                }
                return ActionType.Unknown;
            }
        }

        public string Value1
        {
            get
            {
                if (m_action != null)
                {
                    if (m_action is TextActionItem)
                        return ((TextActionItem)m_action).text;
                    else if (m_action is KeywordActionItem)
                        return ((KeywordActionItem)m_action).text;
                    else if (m_action is ImageActionItem)
                        return ((ImageActionItem)m_action).fileName;
                    else if (m_action is AnimationActionItem)
                        return ((AnimationActionItem)m_action).fileName1;
                    else if (m_action is VideoActionItem)
                        return ((VideoActionItem)m_action).fileName;
                    else if (m_action is SimulationActionItem)
                        return ((SimulationActionItem)m_action).fileName1;
                }
                return "unknown";
            }

            set
            {
                if (m_action != null)
                {
                    if (m_action is TextActionItem)
                        ((TextActionItem)m_action).text = value;
                    else if (m_action is KeywordActionItem)
                        ((KeywordActionItem)m_action).text = value;
                    else if (m_action is ImageActionItem)
                        ((ImageActionItem)m_action).fileName = value;
                    else if (m_action is AnimationActionItem)
                        ((AnimationActionItem)m_action).fileName1 = value;
                    else if (m_action is VideoActionItem)
                        ((VideoActionItem)m_action).fileName = value;
                    else if (m_action is SimulationActionItem)
                        ((SimulationActionItem)m_action).fileName1 = value;
                }
            }


        }

        public string Value2
        {
            get
            {
                if (m_action != null)
                {
                    if (m_action is TextActionItem)
                        return "";
                    else if (m_action is KeywordActionItem)
                        return ((KeywordActionItem)m_action).description;
                    else if (m_action is ImageActionItem)
                        return String.Format("{0};{1}", ((ImageActionItem)m_action).left, ((ImageActionItem)m_action).top);
                    else if (m_action is SimulationActionItem)
                        return ((SimulationActionItem)m_action).fileName2;
                    else 
                        return "";
                }
                return "unknown";
            }
            set
            {
                if (m_action != null)
                {
                    if (m_action is KeywordActionItem)
                        ((KeywordActionItem)m_action).description = value;
                    else if (m_action is AnimationActionItem)
                        ((AnimationActionItem)m_action).fileName2 = value;
                    else if (m_action is SimulationActionItem)
                        ((SimulationActionItem)m_action).fileName2 = value;
                }
            }

        }

        public ContentEditRecord(PageItem page, ActionItem action, int actionId)
        {
            m_action = action;
            m_page = page;
            m_actionId = actionId;
        }
    };

}


/*
*/