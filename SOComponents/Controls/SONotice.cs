using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SoftObject.SOComponents.UtilityLibrary;

namespace SoftObject.SOComponents.Controls
{
	/// <summary>
	/// Zusammendfassende Beschreibung für SONotice.
	/// </summary>
	public partial class SONotice : System.Windows.Forms.UserControl
	{
        public delegate int GetWorkedOutStateCallBack(int iNoticeId);
        public delegate void SetWorkedOutStateCallBack(int iNoticeId,int iState);
        public delegate bool DoSubmitCallBack(int iNoticeId,string strFilename,string strSolFilename,ref string strDiffResult);
        public delegate string GetTitleCallBack(int iNoticeId);
        public delegate void DeleteNoticeCallBack(int iNoticeId);
        public delegate void SaveNoticeCallBack(string fileName);
        public delegate bool GetSolutionFileCallBack(string strFileName,ref string strSolutionFile);
        private delegate void fnUpdateWorkoutState(string strTitle);


        private ResourceHandler rh=null;
		private string m_strFilename="";
        private string m_strSolutionFileName = "";
        private GetTitleCallBack m_fnGetTitle=null;
        private GetWorkedOutStateCallBack m_fnGetWorkeOutState = null;
        private SetWorkedOutStateCallBack m_fnSetWorkedOutState = null;
        private DeleteNoticeCallBack m_fnOnDeleteNotice = null;
        private DoSubmitCallBack m_fnDoSubmit = null;
        private SaveNoticeCallBack m_fnSaveNotice = null;
        private GetSolutionFileCallBack m_fnGetSolutionFile = null;
        private bool m_bCanWorkOut = false;
        private bool m_bCanDelete = false;
        private bool m_bIsSolutionFile = false;
        private int m_iNoticeId = -1;
        private string m_strNoticeDocFilename="";
	
		public string Filename
		{
			get
			{
				return m_strFilename;
			}
		}

        public bool ShowPanelTopButtons
        {
            set
            {
                if (value == true)
                {
                    panelTopTitle.Dock = DockStyle.None;
                    panelTopTitle.Visible = false;
                    panelTopButtons.Visible = true;
                    panelTopButtons.Dock = DockStyle.Top;
                }
                else 
                {
                    panelTopButtons.Visible = false;
                    panelTopButtons.Dock = DockStyle.None;
                    panelTopTitle.Dock = DockStyle.Top;
                    panelTopTitle.Visible = true;
                }
            }
        }

		public SONotice()
		{
			// Dieser Aufruf ist für den Windows Form-Designer erforderlich.
			InitializeComponent();

			string exeFolder=Application.ExecutablePath.Substring(0,Application.ExecutablePath.LastIndexOf('\\'));
			ProfileHandler profHnd = new ProfileHandler(exeFolder+"\\TrainConcept.ini");
			string language=profHnd.GetProfileString("SYSTEM","Language","DE");
			language=language.ToUpper();
			string workingFolder=profHnd.GetProfileString("SYSTEM","WorkingFolder",exeFolder);
			LanguageHandler langHnd= new LanguageHandler(workingFolder,"language",language);
				
#if(SKIN_EMCO)
			rh = new ResourceHandler("res.trainconcept.resources_emco",GetType().Assembly);
#elif(SKIN_WEBTRAIN)
			rh = new ResourceHandler("res.webtrain.resources_softobject",GetType().Assembly);
#else
			rh = new ResourceHandler("res.trainconcept.resources_emco",GetType().Assembly);
#endif

			imageListMenu.Images.Add(rh.GetBitmap("CUT"));
			imageListMenu.Images.Add(rh.GetBitmap("COPY"));
			imageListMenu.Images.Add(rh.GetBitmap("PASTE"));

			this.PopupItemCut.Caption = langHnd.GetText("MENU","Cut","Ausschneiden");
			this.PopupItemCopy.Caption = langHnd.GetText("MENU","Copy","Kopieren");
			this.PopupItemPaste.Caption = langHnd.GetText("MENU","Insert","Einfügen");
			this.PopupItemMarkAll.Caption = langHnd.GetText("MENU","Select_All","Alles Markieren");

            m_strSolutionFileName = "";
            m_strFilename = "";
            m_bIsSolutionFile = false;

            base.Load += SONotice_Load;
        }

		public void New(string _fileName)
		{
            ShowPanelTopButtons = false;
            m_strFilename = _fileName;
            m_strSolutionFileName = "";
            m_bIsSolutionFile = false;
			textControl1.Text="...";
			SaveFile();
		}

        public void UpdateWorkoutState(string strTitle)
        {
            if (m_fnGetTitle != null && m_fnGetTitle(m_iNoticeId) == strTitle)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new fnUpdateWorkoutState(UpdateWorkoutState),strTitle);
                    return;
                }

                ratingStarCorrect.Visible = false;
                textControl1.Enabled = true;
                panelBottom.BackColor = System.Drawing.SystemColors.Control;
                lblTextWrong.Visible = false;


                int iState = 0;
                if (m_fnGetWorkeOutState != null)
                    iState = m_fnGetWorkeOutState(m_iNoticeId);

                bool bCanWorkout = m_bCanWorkOut;
                if (bCanWorkout)
                {
                    bCanWorkout = iState >= 0;
                    if (bCanWorkout)
                    {
                        switch (iState)
                        {
                            case 0: btnNotWorkedOut_Click(this, new EventArgs());
                                panelBottom.BackColor = System.Drawing.SystemColors.Control;
                                break;
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                btnCorrectWorkedOut_Click(this, new EventArgs());
                                ratingStarCorrect.Visible = true;
                                ratingStarCorrect.Rating = iState;
                                panelBottom.BackColor = Color.LightGreen;
                                break;
                            case 6: 
                                btnIncorrectWorkedOut_Click(this, new EventArgs());
                                lblTextWrong.Visible = true;
                                break;
                        }
                    }
                }

                if (!bCanWorkout)
                {
                    switch (iState)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5: ratingStarCorrect.Rating = iState;
                                ratingStarCorrect.Visible = true;
                                panelBottom.BackColor = Color.LightGreen;
                                ratingStarCorrect.Enabled = false;
                                textControl1.Enabled = false;
                                lblTextWrong.Visible = false;
                                break;
                        case 6: panelBottom.BackColor = Color.OrangeRed;
                                textControl1.Enabled = false;
                                lblTextWrong.Visible = true;
                                break;
                        default:panelBottom.BackColor = System.Drawing.SystemColors.Control;
                                break;
                    }
                }

                btnWorkoutState.Visible = bCanWorkout;
                btnSave.Visible = !bCanWorkout && textControl1.Enabled;
                ShowPanelTopButtons = (bCanWorkout || m_bCanDelete);

                if (m_fnGetTitle != null)
                    lblTitle.Text = m_fnGetTitle(m_iNoticeId);

            }
        }

        public new void Load(int iId, string strFileName, string strDocfileName,
                             GetTitleCallBack fnGetTitle,GetSolutionFileCallBack fnGetSolutionFile,
                             SaveNoticeCallBack fnSaveNotice,
                             GetWorkedOutStateCallBack fnGetWorkedOutState, DoSubmitCallBack fnDoSubmit=null,
                             bool bCanWorkout = false, SetWorkedOutStateCallBack fnSetWorkedOutState = null,
                             bool bCanDelete = false, DeleteNoticeCallBack fnOnDeleteNotice = null, 
                             bool bCanEditSolution = false)
        {
            m_iNoticeId = iId;
            m_fnGetWorkeOutState = fnGetWorkedOutState;
            m_fnSetWorkedOutState = fnSetWorkedOutState;
            m_fnDoSubmit = fnDoSubmit;
            m_fnOnDeleteNotice = fnOnDeleteNotice;
            m_fnGetTitle = fnGetTitle;
            m_fnSaveNotice = fnSaveNotice;
            m_fnGetSolutionFile = fnGetSolutionFile;
            m_bCanWorkOut = bCanWorkout;
            m_bCanDelete = bCanDelete;
            m_bIsSolutionFile = false;

            btnSolution.Checked = false;
            btnSolution.Visible = bCanEditSolution;

            if (m_fnGetTitle!=null)
                UpdateWorkoutState(m_fnGetTitle(m_iNoticeId));
            else
                UpdateWorkoutState("");

			m_strFilename = strFileName;
            m_strNoticeDocFilename = strDocfileName;

            int iState = 0;
            if (m_fnGetWorkeOutState != null)
                iState = m_fnGetWorkeOutState(m_iNoticeId);

            if (m_fnGetSolutionFile!=null && iState==0 && m_strNoticeDocFilename.Length>0)
                btnSubmit.Visible = m_fnGetSolutionFile(m_strNoticeDocFilename,ref m_strSolutionFileName) && fnDoSubmit != null;
            else
                btnSubmit.Visible = false;

            textControl1.Dock = DockStyle.Fill;
            textControl1.Visible = true;
            webBrowser1.Visible = false;
            webBrowser1.Dock = DockStyle.None;
            webBrowser1.DocumentText = "";

            LoadFile();
		}

        public void HideTopPanel()
        {
            ShowPanelTopButtons = false;
        }

        public void LoadFile()
        {
            string strFilename = m_strFilename;
            if (m_bIsSolutionFile)
            {
                if (!m_fnGetSolutionFile(strFilename,ref m_strSolutionFileName))
                    File.Copy(m_strFilename, m_strSolutionFileName, true);
                strFilename = m_strSolutionFileName;
            }

            try
            {
                textControl1.Load(strFilename, TXTextControl.StreamType.RichTextFormat);
            }
            catch (Exception /*e1*/)
            {
            }
        }


		public void SaveFile()
		{
            if (m_bIsSolutionFile)
            {
                try
                {
                    textControl1.Save(m_strSolutionFileName, TXTextControl.StreamType.RichTextFormat);
                }
                catch (Exception /*e1*/)
                {
                }

            }
            else
            {
                if (m_strFilename.Length > 0/* && textControl1.Text.Length>0*/)
                {
                    try
                    {
                        textControl1.Save(m_strFilename, TXTextControl.StreamType.RichTextFormat);
                    }
                    catch (Exception /*e1*/)
                    {
                    }
                }

                if (m_fnSaveNotice != null)
                    m_fnSaveNotice(m_strFilename);
            }
        }

		protected override void OnLostFocus(EventArgs e)
		{
			if (textControl1.Text.Length>0)
				SaveFile();		
			base.OnLostFocus(e);
		}

        
		private void PopupItemCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			textControl1.Cut();
		}
		
		private void PopupItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			textControl1.Copy();
		}

		private void PopupItemPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			textControl1.Paste();
		}

		private void PopupItemMarkAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
		}

		private void popupMenu1_Popup(object sender, System.EventArgs e)
		{
			PopupItemCut.Enabled = PopupItemCopy.Enabled = textControl1.CanCopy;
			PopupItemPaste.Enabled = textControl1.CanPaste;
			PopupItemMarkAll.Enabled = textControl1.Text !="";
        }

        private void SONotice_Load(object sender, EventArgs e)
        {

        }

        private void textControl1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                SaveFile();
        }

        private void textControl1_Leave(object sender, EventArgs e)
        {

        }

        private void btnCorrectWorkedOut_Click(object sender, EventArgs e)
        {
            btnCorrectWorkedOut.Checked = true;
            btnIncorrectWorkedOut.Checked = false;
            btnNotWorkedOut.Checked = false;
            ratingStarCorrect.Visible = true;
            ratingStarCorrect.Enabled = true;
            lblTextWrong.Visible = false;
            panelBottom.BackColor = System.Drawing.Color.LightGray;
            if (m_fnSetWorkedOutState != null)
                    m_fnSetWorkedOutState(m_iNoticeId,1);
        }

        private void btnNotWorkedOut_Click(object sender, EventArgs e)
        {
            btnCorrectWorkedOut.Checked = false;
            btnIncorrectWorkedOut.Checked = false;
            btnNotWorkedOut.Checked = true;
            ratingStarCorrect.Visible = false;
            lblTextWrong.Visible = false;
            panelBottom.BackColor = Color.Gray;
            if (m_fnSetWorkedOutState != null)
                m_fnSetWorkedOutState(m_iNoticeId,0);
        }

        private void btnIncorrectWorkedOut_Click(object sender, EventArgs e)
        {
            btnCorrectWorkedOut.Checked = false;
            btnIncorrectWorkedOut.Checked = true;
            btnNotWorkedOut.Checked = false;
            ratingStarCorrect.Visible = false;
            ratingStarCorrect.Enabled = false;
            lblTextWrong.Visible = true;
            panelBottom.BackColor = Color.OrangeRed;
            if (m_fnSetWorkedOutState != null)
                m_fnSetWorkedOutState(m_iNoticeId,6);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (m_fnSetWorkedOutState != null)
                m_fnSetWorkedOutState(m_iNoticeId,7);
            if (m_fnOnDeleteNotice != null)
                m_fnOnDeleteNotice(m_iNoticeId);
        }

        private void ratingStar1_RatingChanged(object sender, EventArgs e)
        {
            if (m_fnSetWorkedOutState != null)
                m_fnSetWorkedOutState(m_iNoticeId,ratingStarCorrect.Rating);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void switchButtonItem1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSolution_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is ButtonX btn)
            {
                if (textControl1.CanUndo)
                    SaveFile();
                m_bIsSolutionFile = btn.Checked;
                LoadFile();
            }
        }

        private void btnSolution_Click(object sender, EventArgs e)
        {
            if (sender is ButtonX btn)
            {
                btn.Checked = !btn.Checked;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (m_fnDoSubmit != null)
            {
                string strDiffResult="";
                bool bResult=m_fnDoSubmit(m_iNoticeId, m_strFilename, m_strSolutionFileName,ref strDiffResult);
                if (m_fnSetWorkedOutState != null)
                {
                    m_fnSetWorkedOutState(m_iNoticeId, bResult ? 1 : 6);
                    UpdateWorkoutState(m_fnGetTitle(m_iNoticeId));
                    btnSubmit.Visible = false;
                    if (!bResult)
                    {
                        textControl1.Dock = DockStyle.None;
                        textControl1.Visible = false;
                        webBrowser1.Visible = true;
                        webBrowser1.Dock = DockStyle.Fill;
                        webBrowser1.DocumentText = strDiffResult;
                    }
                }
            }
        }
    }
}
