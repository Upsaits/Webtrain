using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SoftObject.TrainConcept.Libraries;
using SoftObject.SOComponents.Forms;
using SoftObject.SOComponents.Controls;
using System.Windows.Forms;
using System.Collections;
using Vereyon.Windows;
using System.Linq;
using mshtml;
using System.Threading;
using System.Diagnostics;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Controls;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Interfaces;
using SoftObject.UtilityLibrary;


namespace SoftObject.TrainConcept
{
    public delegate void ContentBrowserHandler(object sender, ref ContentBrowserEventArgs ea);
    public enum ContentBrowserEventAction { BeforeSetPage, AfterSetPage, AddDocument };
    public class ContentBrowserEventArgs : EventArgs
    {
        private ContentBrowserEventAction m_action;
        private int m_pageId;
        private string m_contentPath;
        private ActionItem m_item;


        public ContentBrowserEventAction Action
        {
            get { return m_action; }
        }

        public int PageId
        {
            get { return m_pageId; }
        }

        public string ContentPath
        {
            get { return m_contentPath; }
        }

        public ActionItem Item
        {
            get { return m_item; }
        }

        public ContentBrowserEventArgs(ContentBrowserEventAction action, int pageId, string contentPath, ActionItem item=null)
        {
            m_action = action;
            m_pageId = pageId;
            m_contentPath = contentPath;
            m_item = item;
        }
    }

    public class ContentBrowser : IDisposable
    {
        public enum ContentUseType {LearningPage, Question};
        private ContentUseType m_tUseType = ContentUseType.LearningPage;
		private WebBrowserEx m_axWebBrowser=null;
        private IContentNavigationCtrl m_contentNavCtrl=null;
        private LibraryOverview m_libOverview=null;
        private DHTMLParser m_dhtmlParser = null;
        private PageItem m_actPageItem = null;
        private QuestionItem m_actQuestionItem = null;
        private List<ImageActionItem> m_aImages = null;
        private static SOVideoPlayer_VisioForge m_videoPlayer = null;
        private static SOFlashPlayer m_flashPlayer = null;
        private string m_contentFolder;
        private string m_work = "";
        private int m_maxPages = 0;
        private int m_pageId = 0;
        private int m_actImageId=0;
        private string m_videoFilename = "";
        private string m_animFilename1 = "";
        private string m_animFilename2 = "";
        private string m_simFilename1  = "";
        private string m_simFilename2  = "";
        private bool m_bIsSimMill = true;
        private bool m_isActive=true;
        private bool m_bEditMode = false;
        private static int m_zoomValue = 100;
        private Tuple<bool, Stopwatch> [] m_aStopWatches = null;
        private Action<bool> m_setWorkoutStateButtonAction = null;
        private bool m_bLastTestResult = false;
        private object m_oLastTestInputs = null;
        private AppHandler AppHandler = Program.AppHandler;
        public bool IsActive
        {
            get
            {
                return m_isActive;
            }

            set
            {
                m_isActive = value;
                //m_axWebBrowser.Visible = m_isActive;
                if (!m_isActive)
                {
                    m_axWebBrowser.Stop();
                    if (!m_bEditMode)
                        foreach (var w in m_aStopWatches)
                            w.Item2.Stop();
                }
            }
        }

        public ContentUseType UseType
        {
            get { return m_tUseType; }
            set 
            { 
                m_tUseType = value;
            }
        }

        public int PageId
        {
            get { return m_pageId; }
        }

        public int MaxPages
        {
            get { return m_maxPages; }
        }

        public string ContentFolder
        {
            get { return m_contentFolder; }
        }

        public bool HasImageGallery
        {
            get { return (m_aImages!=null && m_aImages.Where(i => i.id == "img1").Count() > 1); }
        }

        public object LastTestInputs
        {
            get { return m_oLastTestInputs; }
        }

        public DHTMLParser DHTMLParser
        {
            get { return m_dhtmlParser; }
        }

        private event ContentBrowserHandler ContentBrowserEventHandler;
        public event ContentBrowserHandler OnContentChange
        {
            add { ContentBrowserEventHandler += value; }
            remove { ContentBrowserEventHandler -= value; }
        }

        public ContentBrowser(WebBrowserEx axWebBrowser,IContentNavigationCtrl contentNavCtrl,
                              LibraryOverview libOverview,string work, Action<bool> setWorkoutStateButtonAction,
                              bool bEditMode=false)
        {
		    m_axWebBrowser=axWebBrowser;
            m_contentNavCtrl=contentNavCtrl;
            m_libOverview = libOverview;
            m_bEditMode = bEditMode;
            m_setWorkoutStateButtonAction = setWorkoutStateButtonAction;

            m_axWebBrowser.Stop();
            this.m_axWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.axWebBrowser1_DocumentComplete);
            this.m_axWebBrowser.Navigated += new WebBrowserNavigatedEventHandler(this.axWebBrowser1_Navigated);
            this.m_axWebBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.axWebBrowser1_Navigating);
            this.m_axWebBrowser.NavigateError += new WebBrowserNavigateErrorEventHandler(this.axWebBrowser1_NavigateError);

            if (work.Length == 0 && libOverview.ContentTree.FocusedNode!=null)
                work=libOverview.ContentTree.ContentList.GetPath(libOverview.ContentTree.FocusedNode.Id);

            if (work.Length>0)
            {
                string[] aTitles = null;
                Utilities.SplitPath(work, out aTitles);
                if (aTitles.Length==4)
                    SetWork(work);
            }

            axWebBrowser.Visible = m_work.Length > 0;
        }


        public void SetWork(string strWorkPath)
        {
            m_work = strWorkPath;
            // Folder für Inhalte bestimmen
            m_contentFolder = AppHandler.ContentFolder + '\\' + AppHandler.LibManager.GetFileName(m_work).ToLower();
            UpdateWork();
        }

        public void SetPage(int pageId)
        {
            if (!m_bEditMode && m_aStopWatches!=null)
            {
                if ((m_pageId - 1) >= 0 && (m_pageId - 1) < m_aStopWatches.Length)
                    m_aStopWatches[m_pageId - 1].Item2.Stop();
                Debug.WriteLine(String.Format("Watch stopped for {0},Page={1},Elapsed={2}", m_work, m_pageId, m_aStopWatches[m_pageId - 1].Item2.Elapsed.ToString()));
            }
            m_pageId = pageId;
            m_contentNavCtrl.SetPageId(pageId);
            ShowPage();
        }

        public void ReloadPage()
        {
            AppHandler.LibManager.Reload(m_work);
            ShowPage();
        }

        public void UpdateWork()
        {
            AppHandler.LibManager.GetPageCnt(m_work, out m_maxPages);
            m_contentNavCtrl.SetMaxPages(m_maxPages);
            m_pageId = m_contentNavCtrl.GetPageId();

            if (!m_bEditMode)
            {
                m_aStopWatches = new Tuple<bool, Stopwatch>[m_maxPages];
                for (int i = 0; i < m_maxPages; ++i)
                    m_aStopWatches[i] = new Tuple<bool, Stopwatch>(false, new Stopwatch());
            }
        }


        // ShowPage
        // bestimmte HTML-Seite laden
        public void ShowPage()
        {
            if (m_axWebBrowser == null)
                return;

            CloseAllPlayer();

            if (m_tUseType == ContentUseType.LearningPage)
            {
                if (m_contentNavCtrl.GetPageId() > 0 && m_work.Length > 0 && m_isActive)
                {
                    // Template holen
                    m_actPageItem = AppHandler.LibManager.GetPage(m_work, m_contentNavCtrl.GetPageId() - 1);

                    ContentBrowserEventArgs ea = new ContentBrowserEventArgs(ContentBrowserEventAction.BeforeSetPage, m_pageId, m_work);
                    if (ContentBrowserEventHandler != null)
                        ContentBrowserEventHandler(this, ref ea);

                    // dynamisch Variablennamen löschen
                    m_videoFilename = "";
                    m_animFilename1 = "";
                    m_animFilename2 = "";
                    m_simFilename1 = "";
                    m_simFilename2 = "";

                    m_contentNavCtrl.SetVideo("");
                    m_contentNavCtrl.SetAnimation("", "");
                    m_contentNavCtrl.SetGrafic(-1, 0);
                    m_contentNavCtrl.SetSimulation(false, "", "");
                    m_contentNavCtrl.ClearDocuments();

                    if (m_libOverview != null)
                        m_libOverview.SetActualPoint(m_work);

                    // Template-Seite ansteuern
                    if (m_actPageItem != null)
                    {
                        try
                        {
                            m_dhtmlParser = null;
                            string url = m_contentFolder + "/Templates/" + m_actPageItem.templateName;
                            m_axWebBrowser.Stop();
                            m_axWebBrowser.Refresh(WebBrowserRefreshOption.Completely);
                            m_axWebBrowser.Navigate(url);
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    ea = new ContentBrowserEventArgs(ContentBrowserEventAction.AfterSetPage, m_pageId, m_work);
                    if (ContentBrowserEventHandler != null)
                        ContentBrowserEventHandler(this, ref ea);

                    if (!m_bEditMode && m_aStopWatches!=null)
                    {
                        if ((m_pageId-1)>=0 && (m_pageId-1)<m_aStopWatches.Length)
                            m_aStopWatches[m_pageId - 1].Item2.Start();
                        Debug.WriteLine(String.Format("Watch started for {0}, Page={1}", m_work, m_pageId));
                    }

                    /*
                    m_timerUserProgress = new System.Threading.Timer((obj) =>
                    {
                        if (Monitor.TryEnter(lockObject))
                        AppHandler.AddUserProgressInfo(AppHandler.MainForm.ActualUserName, m_work, (int)UserProgressInfoManager.RegionType.Learning, HelperMacros.MAKEWORD(2, (byte)m_pageId));

                        System.Threading.Thread.Sleep(5000);
                        
                        m_timerUserProgress.Change(1000, System.Threading.Timeout.Infinite);
                    }, m_timerCanceller, 5000, System.Threading.Timeout.Infinite);*/

                }
            }
            else
            {
                try
                {
                    m_dhtmlParser = null;
                    string url = m_contentFolder + "/Templates/" + m_actQuestionItem.templateName;
                    m_axWebBrowser.Stop();
                    m_axWebBrowser.Refresh(WebBrowserRefreshOption.Completely);
                    m_axWebBrowser.Navigate(url);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CloseAllPlayer()
        {

            if (m_videoPlayer != null)
            {
                m_videoPlayer.Close();
                m_videoPlayer = null;
                GC.Collect();
            }

            if (m_flashPlayer != null)
            {
                m_flashPlayer.Close();
                m_flashPlayer = null;
                GC.Collect();
            }
        }

        public void SetQuestion(QuestionItem questionItem)
        {
            m_actQuestionItem = questionItem;
            m_oLastTestInputs = null;
        }

        private void ShowImageGallery()
        {
            if (m_dhtmlParser != null && m_aImages.Count>1)
            {
                foreach (var img in m_aImages)
                    if (img.id=="img1")
                        m_dhtmlParser.Call_addImage2Gallery("../" + img.fileName, "../" + img.fileName);
                m_dhtmlParser.Call_showGallery();

                var imgItem0 = m_aImages[0];
                m_dhtmlParser.SetDivAt("image-gallery", ImageActionItem.DefaultImgPosX + imgItem0.left, ImageActionItem.DefaultImgPosY + imgItem0.top);
                m_dhtmlParser.SetElementAttributes("image-gallery", "", imgItem0.width, imgItem0.height);
            }
        }

        private void ShowImage(int imgId)
        {
            if (m_dhtmlParser != null && imgId >= 0 && imgId < m_aImages.Count)
            {
                var imgItem = m_aImages[imgId];

                m_dhtmlParser.Call_setShowElement(String.Format("image{0}", imgId+1), true);
                m_dhtmlParser.SetDivAt(String.Format("image{0}",imgId+1), ImageActionItem.DefaultImgPosX + imgItem.left, ImageActionItem.DefaultImgPosY + imgItem.top);
                m_dhtmlParser.SetElementAttributes(imgItem.id, "../" + imgItem.fileName, imgItem.width, imgItem.height);
                //if (m_aImages.Count > 1)
                //    m_contentNavCtrl.SetGrafic(imgId, m_aImages.Count);
                m_actImageId = imgId;
            }
        }

        public void Print()
        {
            m_axWebBrowser.Print();
        }

        public bool CheckActualQuestion()
        {
            if (m_tUseType == ContentUseType.Question && m_actQuestionItem != null && m_actQuestionItem.Answers !=null)
            {
                if (m_actQuestionItem.type == "Completion")
                {
                    EndQuiz();
                    return m_bLastTestResult;
                }
                else if (m_actQuestionItem.type == "MultipleChoice")
                {
                    BitArray actBits = new BitArray(new int[] { 0 });
                    for (int i = 0; i < m_actQuestionItem.Answers.Length; ++i)
                    {
                        string strChkId = String.Format("chkAnswer{0}", i + 1);
                        bool isChecked = m_dhtmlParser.Call_getChecked(strChkId);
                        actBits.Set(i, isChecked);
                    }

                    bool isRight = true;
                    BitArray corrMask = new BitArray(new int[] { m_actQuestionItem.correctAnswerMask });
                    for (int i = 0; i < m_actQuestionItem.Answers.Length; ++i)
                    {
                        if (corrMask[i] != actBits[i])
                            isRight = false;
                    }

                    return isRight;
                }
            }
            return false;
        }


        public bool GetActualSelection(WorkoutInfoItem workoutInfo,out bool bWasEdited)
        {
            bWasEdited = false;
            if (m_tUseType == ContentUseType.Question && workoutInfo.Question != null && workoutInfo.Question.Answers !=null)
            {
                if (workoutInfo.Question.type == "Completion")
                {
                    if (!workoutInfo.IsWorkedOut)
                    {
                        if (m_oLastTestInputs != null)
                            EndQuiz();
                        if (m_oLastTestInputs != null)
                        {
                            workoutInfo.QuizAnswers = (string)m_oLastTestInputs;
                            workoutInfo.QuizResult = m_bLastTestResult;
                            bWasEdited = true;
                            return m_bLastTestResult;
                        }
                    }

                    return false;
                }
                else if (workoutInfo.Question.type == "MultipleChoice")
                {
                    BitArray actBits = new BitArray(new int[] { 0 });
                    for (int i = 0; i < workoutInfo.Question.Answers.Length; ++i)
                    {
                        string strChkId = String.Format("chkAnswer{0}", i + 1);
                        bool isChecked = m_dhtmlParser.Call_getChecked(strChkId);
                        actBits.Set(i, isChecked);
                    }

                    bool isRight = true;
                    BitArray corrMask = new BitArray(new int[] { workoutInfo.Question.correctAnswerMask });
                    for (int i = 0; i < workoutInfo.Question.Answers.Length; ++i)
                    {
                        if (corrMask[i] != actBits[i])
                            isRight = false;
                    }

                    bWasEdited = actBits.Cast<bool>().Any(x => x);

                    workoutInfo.ActCorrMask = actBits;
                    return isRight;
                }
            }
            return false;
        }

        public void ShowAnswer(WorkoutInfoItem workoutInfo, bool bShowCorrect)
        {
            if (m_dhtmlParser != null)
            {
                if (workoutInfo.Question.type == "MultipleChoice")
                {
                    for (int i = 0; i < workoutInfo.Question.Answers.Length; ++i)
                    {
                        string strChkId = String.Format("chkAnswer{0}", i + 1);
                        BitArray corrMask = null;
                        if (bShowCorrect)
                            corrMask = new BitArray(new int[] { workoutInfo.Question.correctAnswerMask });
                        else
                            corrMask = workoutInfo.ActCorrMask;
                        m_dhtmlParser.SetInputCheck(strChkId, corrMask.Get(i));
                    }
                }
                else if (workoutInfo.Question.type == "Completion" && bShowCorrect)
                {
                    string strResult = string.Join(",",workoutInfo.Question.Answers);
                    MessageBox.Show(strResult,"Webtrain",MessageBoxButtons.OK);
                }
            }
        }

        private void axWebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
        }

        private void axWebBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
           
        }

        private void axWebBrowser1_NavigateError(object sender, WebBrowserNavigateErrorEventArgs e)
        {
            MessageBox.Show(String.Format("Template ist am Server nicht vorhanden: {0}",e.Url));
            m_axWebBrowser.Stop();
        }

        // OnDocumentComplete
        // Event ausgelöst nach dem jedem Laden eines Frames
        private void axWebBrowser1_DocumentComplete(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                var webBrowser = sender as WebBrowserEx;
                // DHTML Zugriff auf HTML-Dokument
                IHTMLDocument2 doc = (IHTMLDocument2)webBrowser.Document.DomDocument;
                string htmName = Path.GetFileNameWithoutExtension(e.Url.ToString());
                if (doc.url.IndexOf(htmName) >= 0)
                {
                    var frames = doc.frames;
                    if (frames.length > 0)
                    {
                        HTMLWindow2 wnd = null;
                        try
                        {
                            //object frmid = "Mainframe";
                            // Zuständiges Mainframe-Fenster suchen; normalerweise am Index 2
                            object frmid = 2;
                            if (frames.item(ref frmid) is HTMLWindow2)
                                wnd = frames.item(ref frmid) as HTMLWindow2;
                        }
                        catch (System.Exception/* ex*/)
                        {
                        }

                        if (wnd != null) // gefunden?
                        {
                            webBrowser.Document.Window.AttachEventHandler("onscroll", OnScrollEventHandler);

                            wnd.focus();
                            htmName = htmName.ToLower();
                            if (m_dhtmlParser == null && (htmName.EndsWith("_f") || htmName.EndsWith("_frm")))
                            {
                                //doc.body.setAttribute("style", "font-size:medium;");

                                m_dhtmlParser = new DHTMLParser(webBrowser, (IHTMLDocument2) wnd.document,
                                    (m_tUseType == ContentUseType.LearningPage) ? new fnFindKeyword(FindKeyword) : null,
                                    (strType, bResult, strInputs) =>
                                    {
                                        //MessageBox.Show(String.Format("{0},{1},{2}",strType,bResult.ToString(),strInputs));
                                        m_oLastTestInputs = strInputs;
                                        m_bLastTestResult = bResult;
                                    },
                                    (bIsEnabled) =>
                                    {
                                        m_oLastTestInputs = new string(' ',1);
                                        m_setWorkoutStateButtonAction.Invoke(bIsEnabled);
                                    }
                                    );

                                string strZoom = String.Format("zoom:{0}%", m_zoomValue);

                                if (webBrowser.Document.Body != null) 
                                    webBrowser.Document.Body.Style = strZoom;

                                #region DHTML LearningPages
                                if (m_tUseType == ContentUseType.LearningPage)
                                {
                                    if (m_actPageItem != null)
                                    {
                                        var aImages = m_actPageItem.GetImageActions();
                                        if (aImages != null)
                                        {
                                            m_aImages = new List<ImageActionItem>((ImageActionItem[])aImages.ToArray(typeof(ImageActionItem)));
                                            if (m_aImages.Count > 0)
                                            {
                                                if (HasImageGallery)
                                                {
                                                    m_dhtmlParser.Call_setShowElement("image1", false);
                                                    m_dhtmlParser.Call_setShowElement("image-gallery", true);
                                                    ShowImageGallery();
                                                }
                                                else
                                                {
                                                    m_dhtmlParser.Call_setShowElement("image-gallery", false);
                                                    for (int i = 0; i < m_aImages.Count; ++i)
                                                        ShowImage(i);
                                                }
                                            }
                                            else
                                            {
                                                m_dhtmlParser.Call_setShowElement("image-gallery", false);
                                                m_dhtmlParser.Call_setShowElement("image1", false);
                                            }
                                        }

                                        // einzelne Aktionen holen und ausarbeiten
                                        int actCnt = m_actPageItem.PageActions.Length;
                                        for (int i = 0; i < actCnt; ++i)
                                        {
                                            ActionItem item = m_actPageItem.PageActions[i];
                                            if (item != null)
                                            {
                                                // Text
                                                if (item is TextActionItem)
                                                {
                                                    TextActionItem txtItem = (TextActionItem)item;
                                                    if (txtItem.text.Length == 0)
                                                    {
                                                        // hide empty "enumtext[x]" and "enum[x]" entries
                                                        if (txtItem.id.IndexOf("enumtext") == 0 && txtItem.id.Length > 8)
                                                        {
                                                            //int id = Int32.Parse(txtItem.id[8].ToString());
                                                            m_dhtmlParser.Call_setShowElement(txtItem.id, false);
                                                        }
                                                        else if (txtItem.id.IndexOf("enum") == 0 && txtItem.id.Length > 4)
                                                        {
                                                            int id = Int32.Parse(txtItem.id[4].ToString());
                                                            m_dhtmlParser.Call_setShowElement(String.Format("dive{0}", id), false);
                                                            m_dhtmlParser.Call_setShowElement(String.Format("divenum{0}", id), false);
                                                        }
                                                        else
                                                        {
                                                            m_dhtmlParser.Call_setShowElement(txtItem.id, false);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        m_dhtmlParser.ReplaceText(item.id, txtItem.text);
                                                        if (txtItem.customWidth > 0 || txtItem.customHeight > 0)
                                                            m_dhtmlParser.SetDivSize("div" + txtItem.id,txtItem.customWidth, txtItem.customHeight);
                                                    }
                                                }
                                                // Video
                                                else if (item is VideoActionItem)
                                                {
                                                    VideoActionItem vidItem = (VideoActionItem)item;
                                                    m_videoFilename = m_contentFolder + '/' + vidItem.fileName;
                                                    m_videoFilename = m_videoFilename.Replace('/', '\\');
                                                    m_videoFilename = m_videoFilename.Replace("http:\\\\", "http://");
                                                    m_contentNavCtrl.SetVideo(m_videoFilename);
                                                }
                                                // Animation
                                                else if (item is AnimationActionItem)
                                                {
                                                    AnimationActionItem aniItem = (AnimationActionItem)item;
                                                    if (aniItem.fileName1.Length > 0)
                                                    {
                                                        m_animFilename1 = m_contentFolder + '/' + aniItem.fileName1;
                                                        if (m_animFilename1.IndexOf("http") < 0)
                                                            m_animFilename1 = m_animFilename1.Replace('/', '\\');
                                                        m_animFilename1 = m_animFilename1.Replace("http:\\\\", "http://");
                                                    }
                                                    if (aniItem.fileName2.Length > 0)
                                                    {
                                                        m_animFilename2 = m_contentFolder + '/' + aniItem.fileName2;
                                                        if (m_animFilename2.IndexOf("http") < 0)
                                                            m_animFilename2 = m_animFilename2.Replace('/', '\\');
                                                        m_animFilename2 = m_animFilename2.Replace("http:\\\\", "http://");
                                                    }

                                                    m_contentNavCtrl.SetAnimation(m_animFilename1, m_animFilename2);
                                                }
                                                // Simulation
                                                else if (item is SimulationActionItem)
                                                {
                                                    SimulationActionItem simItem = (SimulationActionItem)item;
                                                    m_bIsSimMill = simItem.isMill;
                                                    if (simItem.fileName1.Length > 0)
                                                        m_simFilename1 = m_contentFolder + '/' + simItem.fileName1;
                                                    if (simItem.fileName2.Length > 0)
                                                        m_simFilename2 = m_contentFolder + '/' + simItem.fileName2;

                                                    m_contentNavCtrl.SetSimulation(m_bIsSimMill, m_simFilename1, m_simFilename2);
                                                }
                                                else if (item is DocumentActionItem)
                                                {
                                                    var docItem = (DocumentActionItem)item;

                                                    if (docItem.typeId == 1 || docItem.typeId == 2 || docItem.typeId == 3)
                                                        m_contentNavCtrl.AddDocument(docItem.id,docItem.typeId);

                                                    ContentBrowserEventArgs ea = new ContentBrowserEventArgs(ContentBrowserEventAction.AddDocument, m_pageId, m_work, item);
                                                    if (ContentBrowserEventHandler != null)
                                                        ContentBrowserEventHandler(this, ref ea);
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                                else
                                #region DHTML QuestionPages
                                {
                                    if (m_actQuestionItem != null)
                                    {
                                        var aImages = m_actQuestionItem.GetImageActions();
                                        if (aImages != null)
                                            m_aImages = new List<ImageActionItem>((ImageActionItem[])aImages.ToArray(typeof(ImageActionItem)));
                                        else
                                            m_dhtmlParser.Call_setShowElement("image1", false);

                                        // einzelne Aktionen holen und ausarbeiten
                                        if (m_actQuestionItem.QuestionActions != null)
                                        {
                                            int actCnt = m_actQuestionItem.QuestionActions.Length;
                                            for (int i = 0; i < actCnt; ++i)
                                            {
                                                ActionItem item = m_actQuestionItem.QuestionActions[i];
                                                if (item != null)
                                                {
                                                    // Grafik?
                                                    if (item is ImageActionItem)
                                                    {
                                                        ShowImage(0);
                                                    }
                                                    // Video
                                                    else if (item is VideoActionItem)
                                                    {
                                                    }
                                                    // Animation
                                                    else if (item is AnimationActionItem)
                                                    {
                                                    }
                                                }
                                            }
                                        }


                                        if (m_actQuestionItem.type == "MultipleChoice")
                                        {
                                            m_dhtmlParser.ReplaceText("question", m_actQuestionItem.question);

                                            if (m_actQuestionItem.Answers != null)
                                            {
                                                int iQuId;
                                                for (iQuId = 0; iQuId < m_actQuestionItem.Answers.Length; ++iQuId)
                                                {
                                                    string strTxtId = String.Format("answer{0}", iQuId + 1);
                                                    m_dhtmlParser.ReplaceText(strTxtId, m_actQuestionItem.Answers[iQuId]);
                                                }

                                                //int i = 0;
                                                while (iQuId < 8)
                                                {
                                                    string strChkId = String.Format("chkAnswer{0}", iQuId + 1);
                                                    string strTxtId = String.Format("answer{0}", iQuId + 1);
                                                    string strImgId = String.Format("chkAnswer{0}_Image", iQuId + 1);

                                                    m_dhtmlParser.Call_setShowElement(strChkId, false);
                                                    m_dhtmlParser.Call_setShowElement(strTxtId, false);
                                                    m_dhtmlParser.Call_setShowElement(strImgId, false);
                                                    ++iQuId;
                                                }
                                            }
                                            else
                                            {
                                                for (int i = 0; i < 8; ++i)
                                                {
                                                    string strChkId = String.Format("chkAnswer{0}", i + 1);
                                                    string strTxtId = String.Format("answer{0}", i + 1);
                                                    string strImgId = String.Format("chkAnswer{0}_Image", i + 1);

                                                    m_dhtmlParser.Call_setShowElement(strChkId, false);
                                                    m_dhtmlParser.Call_setShowElement(strTxtId, false);
                                                    m_dhtmlParser.Call_setShowElement(strImgId, false);

                                                }
                                            }

                                            if (m_actQuestionItem.Answers!=null)
                                                for (int i = 0; i < m_actQuestionItem.Answers.Length; ++i)
                                                {
                                                    string strChkId = String.Format("chkAnswer{0}", i + 1);
                                                    m_dhtmlParser.Call_setChecked(strChkId, false);
                                                    m_dhtmlParser.Call_setEnabled(strChkId, true);
                                                }
                                        }
                                        else if (m_actQuestionItem.type == "Completion")
                                        {
                                            //string strText = @"<p id=""textdescription"">Beschreibung..</p>
                                            //                   <p id=""question"">Frage: <strong id=""answer1"">richtig()</strong>&emsp;<strong id=""answer2"">richtig()</strong> </p></div>";
                                            //string strText=@"<div class=""lueckentext-quiz"" lang=""de""><h2 id=""lueckentext-quiz\"">Lückentext-Quiz</h2>
                                            //              <p id=""textdescription"">Beschreibung..</p>
                                            //              <p id=""question"">Frage: <strong id=""answer1"">richtig()</strong>&emsp;<strong id=""answer2"">richtig()</strong> </p></div>";
                                            //m_dhtmlParser.InsertAfter("lueckentext-quiz", strText);
                                            m_dhtmlParser.ReplaceText("question", m_actQuestionItem.question);
                                            foreach (var i in m_actQuestionItem.QuestionActions)
                                            {
                                                // Text
                                                var item = i as TextActionItem;
                                                if (item != null)
                                                {
                                                    m_dhtmlParser.ReplaceText(item.id, item.text);
                                                }
                                            }

                                            if (m_actQuestionItem.Answers != null)
                                                for (int i = 0; i < m_actQuestionItem.Answers.Length; ++i)
                                                {
                                                    string strAnswer=String.Format("answer{0}",i+1);
                                                    m_dhtmlParser.ReplaceText(strAnswer, m_actQuestionItem.Answers[i]);
                                                }

                                            m_dhtmlParser.Call_initQuiz();
                                            //int iRes = m_dhtmlParser.Call_getQuizResult();
                                            //MessageBox.Show(iRes.ToString());
                                        }
                                    }
                                }
                                #endregion

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void OnScrollEventHandler(object sender, EventArgs e)
        {
            int iZoomVal;
            if (m_dhtmlParser.Call_getZoom(out iZoomVal))
                m_zoomValue = iZoomVal;
         }

        private bool FindKeyword(string _key, ref string _keyText, ref string _keyDescription)
        {
            ActionItem keyItem = AppHandler.LibManager.GetPageAction(m_actPageItem, _key);
            // Zugehörige Aktion gefunden?
            if (keyItem != null && (keyItem is KeywordActionItem))
            {
                KeywordActionItem key = ((KeywordActionItem)keyItem);
                if (key.isTooltip)
                    _keyDescription = key.description;
                _keyText = key.text;
                return true;
            }
            return false;
        }
        
        public void OnBtnBack()
		{
			SetPage(m_contentNavCtrl.GetPageId()-1);
		}
		
		public void OnBtnForward()
		{
			SetPage(m_contentNavCtrl.GetPageId()+1);
		}

		public void OnBtnVideo()
		{
            CloseAllPlayer();

            m_videoPlayer = new SOVideoPlayer_VisioForge();
			m_videoPlayer.Closed += new System.EventHandler(VideoPlayer_Closed);
		    m_videoPlayer.Show();
			m_videoPlayer.Play(m_videoFilename);
		}

        public void OnBtnAnim()
        {
            // 2. Filename angegeben? --> Shockwave
            if (m_animFilename2.Length > 0)
            {
                MessageBox.Show("We are Sorry but TrainConcept can't show Shockwave-animations!",
                                "WebTrain", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                CloseAllPlayer();

                // Video-File?
                if (m_animFilename1.IndexOf("avi") >= 0)
                {
                    if (m_videoPlayer != null)
                    {
                        m_videoPlayer.Close();
                        m_videoPlayer = null;
                        GC.Collect();
                    }

                    m_videoPlayer = new SOVideoPlayer_VisioForge();
                    m_videoPlayer.Closed += new System.EventHandler(VideoPlayer_Closed);
                    m_videoPlayer.Show();
                    m_videoPlayer.Play(m_animFilename1);
                }
                // oder Shockwave-File
                else
                {
                    if (m_flashPlayer != null)
                    {
                        m_flashPlayer.Close();
                        m_flashPlayer = null;
                        GC.Collect();
                    }

                    m_flashPlayer = new SOFlashPlayer(false);
                    m_flashPlayer.Closed += new System.EventHandler(FlashPlayer_Closed);

                    if (!m_flashPlayer.IsValid)
                        MessageBox.Show("We are Sorry but TrainConcept can't show Flash-animations."+
                                        "\nMaybe something went wrong during the installation process?"+
                                        "\nplease contact your administrator","WebTrain",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    else
                    {
                        //MessageBox.Show(String.Format("play animation {0}", m_animFilename1));
                        m_flashPlayer.Play(m_animFilename1);
                    }

                }
            }
        }

        public void OnBtnGrafic()
        {
            if (++m_actImageId < m_aImages.Count)
                ShowImage(m_actImageId);
            else
            {
                ShowImage(0);
                m_actImageId = 0;
            }
        }

        public void OnBtnSimulation()
        {
            // SchockwavePlayer schon geladen?
            FrmCSLSim frm = new FrmCSLSim(m_bIsSimMill, m_simFilename1);
            frm.ShowDialog();
            frm.Dispose();
        }


        private void VideoPlayer_Closed(object sender, System.EventArgs e)
        {
            m_videoPlayer = null;
        }

        private void FlashPlayer_Closed(object sender, System.EventArgs e)
        {
            m_flashPlayer = null;
        }

        public void ReplaceImageRegion(ImageActionItem imgItem)
        {
            if (HasImageGallery)
            {
                m_dhtmlParser.SetDivAt("image-gallery", ImageActionItem.DefaultImgPosX + imgItem.left, ImageActionItem.DefaultImgPosY + imgItem.top);
            }
            else
            {
                string strImgId = "image1";
                switch (imgItem.id)
                {
                    case "img2": strImgId = "image2"; break;
                    case "img3": strImgId = "image3"; break;
                    case "img4": strImgId = "image4"; break;
                }
                m_dhtmlParser.SetDivAt(strImgId, ImageActionItem.DefaultImgPosX + imgItem.left, ImageActionItem.DefaultImgPosY + imgItem.top);
            }
        }

        public void ChangeImageRegion(ImageActionItem imgItem)
        {
            if (HasImageGallery)
            {
                this.m_dhtmlParser.SetDivAt("image-gallery", ImageActionItem.DefaultImgPosX + imgItem.left, ImageActionItem.DefaultImgPosY + imgItem.top);
                this.m_dhtmlParser.SetElementAttributes("image-gallery", "", imgItem.width, imgItem.height);
            }
            else
            {
                string strImgId = "image1";
                switch (imgItem.id)
                {
                    case "img2": strImgId = "image2"; break;
                    case "img3": strImgId = "image3"; break;
                    case "img4": strImgId = "image4"; break;
                }
                m_dhtmlParser.SetDivAt(strImgId, ImageActionItem.DefaultImgPosX + imgItem.left, ImageActionItem.DefaultImgPosY + imgItem.top);
                m_dhtmlParser.SetElementAttributes(imgItem.id, "../" + imgItem.fileName, imgItem.width, imgItem.height);
            }
        }

        public ImageActionItem GetImageItem(int iId = 0)
        {
            if (m_aImages != null)
                return m_aImages[iId];
            return null;
        }

        public int GetImageCount()
        {
            if (m_aImages != null)
                return m_aImages.Count;
            return 0;
        }

        public void Dispose()
        {
            if (m_axWebBrowser != null)
            {
                this.m_axWebBrowser.Stop();
                this.m_axWebBrowser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(this.axWebBrowser1_DocumentComplete);
                this.m_axWebBrowser.Navigated -= new WebBrowserNavigatedEventHandler(this.axWebBrowser1_Navigated);
                this.m_axWebBrowser.Navigating -= new WebBrowserNavigatingEventHandler(this.axWebBrowser1_Navigating);
                this.m_axWebBrowser.NavigateError -= new WebBrowserNavigateErrorEventHandler(this.axWebBrowser1_NavigateError);
            }
            if (m_dhtmlParser != null)
                m_dhtmlParser.Dispose();
            if (m_videoPlayer != null)
                m_videoPlayer.Dispose();
            if (m_flashPlayer != null)
                m_flashPlayer.Dispose();
            m_axWebBrowser = null;
            m_dhtmlParser = null;
            m_videoPlayer = null;
            m_flashPlayer = null;
        }

        public void EndQuiz()
        {
            m_dhtmlParser.Call_EndQuiz();
        }

        public void DisableAnswers()
        {
            if (m_tUseType == ContentUseType.Question && m_actQuestionItem != null && 
                m_actQuestionItem.Answers != null && m_dhtmlParser!=null)
            {
                if (m_actQuestionItem.type == "MultipleChoice")
                    for (int i = 0; i < m_actQuestionItem.Answers.Length; ++i)
                    {
                        string strChkId = String.Format("chkAnswer{0}", i + 1);
                        m_dhtmlParser.Call_setEnabled(strChkId, false);
                    }
            }
        }

        internal void WorkoutStopwatches()
        {
            if (!m_bEditMode && m_aStopWatches!=null)
                for(int i=0;i<m_aStopWatches.Count();++i)
                {
                    var t=m_aStopWatches[i];
                    if (!t.Item1)
                    {
                        if (t.Item2.Elapsed.TotalMilliseconds > 5000)
                        {
                            AppHelpers.AddUserProgressInfo(AppHandler.MainForm.ActualUserName, m_work, (int)UserProgressInfoManager.RegionType.Learning, HelperMacros.MAKEWORD(2, (byte)i));
                            Debug.WriteLine(String.Format("UserProgressInfo sent for {0},Region=Learning, Work={1}, Page={2}, Time Elapsed={3}",AppHandler.MainForm.ActualUserName, m_work, i, m_aStopWatches[i].Item2.Elapsed.ToString()));
                            m_aStopWatches[i] = new Tuple<bool, Stopwatch>(true, t.Item2);
                        }
                    }
                }
        }
    }
}
