using System;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Interfaces;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept
{
	/// <summary>
	/// Summary description for ContentManager.
	/// </summary>
	public class DefaultContentManager : IContentManager
	{
		private Hashtable	m_dContents = new Hashtable();
        private AppHandler AppHandler = Program.AppHandler;

		public void SetActiveChild(Form activeChild)
        {
            IEnumerator iter = m_dContents.GetEnumerator();
            while (iter.MoveNext())
            {
                DictionaryEntry item = (DictionaryEntry)iter.Current;
                if (item.Value is FrmContent)
                {
                    if (item.Value != activeChild)
                        ((FrmContent)item.Value).AfterActivation(false);
                }
                else if (item.Value is FrmLMEditContentNew)
                {
                    if (item.Value != activeChild)
                        ((FrmLMEditContentNew)item.Value).AfterActivation(false);
                }
                else if (item.Value is XFrmOnlineUsers)
                {
                    if (item.Value != activeChild)
                        ((XFrmOnlineUsers)item.Value).AfterActivation(false);
                }
                else if (item.Value is FrmChatroom)
                {
                    if (item.Value != activeChild)
                        ((FrmChatroom)item.Value).AfterActivation(false);
                }
                else if (item.Value is FrmBrowser)
                {
                    if (item.Value != activeChild)
                        ((FrmBrowser)item.Value).AfterActivation(false);
                }
                else if (item.Value is FrmEditContent)
                {
                    if (item.Value != activeChild)
                        ((FrmEditContent)item.Value).AfterActivation(false);
                }
                else if (item.Value is XFrmUserEdit)
                {
                    if (item.Value != activeChild)
                        ((XFrmUserEdit)item.Value).AfterActivation(false);
                }
                else if (item.Value is XFrmMapsEdit)
                {
                    if (item.Value != activeChild)
                        ((XFrmMapsEdit)item.Value).AfterActivation(false);
                }
                else if (item.Value is XFrmClassroomEdit)
                {
                    if (item.Value != activeChild)
                        ((XFrmClassroomEdit)item.Value).AfterActivation(false);
                }
            }

            if (activeChild is FrmContent)
                ((FrmContent)activeChild).AfterActivation(true);
            else if (activeChild is FrmLMEditContentNew)
                ((FrmLMEditContentNew)activeChild).AfterActivation(true);
            else if (activeChild is XFrmOnlineUsers)
                ((XFrmOnlineUsers)activeChild).AfterActivation(true);
            else if (activeChild is FrmChatroom)
                ((FrmChatroom)activeChild).AfterActivation(true);
            else if (activeChild is FrmBrowser)
                ((FrmBrowser)activeChild).AfterActivation(true);
            else if (activeChild is FrmEditContent)
                ((FrmEditContent)activeChild).AfterActivation(true);
            else if (activeChild is XFrmUserEdit)
                ((XFrmUserEdit)activeChild).AfterActivation(true);
            else if (activeChild is XFrmMapsEdit)
                ((XFrmMapsEdit)activeChild).AfterActivation(true);
            else if (activeChild is XFrmClassroomEdit)
                ((XFrmClassroomEdit)activeChild).AfterActivation(true);
        }

        public Form GetActiveChild()
        {
            IEnumerator iter = m_dContents.GetEnumerator();
            while (iter.MoveNext())
            {
                DictionaryEntry item = (DictionaryEntry)iter.Current;
                if (item.Value is Form)
                    return (item.Value as Form);
            }
            return null;
        }

		public  void OpenLearnmap(Form mdiParent, string mapTitle)
		{
			FrmContent actFrm = (FrmContent) m_dContents[mapTitle];
			if (actFrm!=null)
			{
				actFrm.Activate();
				return;
			}

			FrmContent frmContent = new FrmContent(mapTitle);
			frmContent.MdiParent = mdiParent;
			frmContent.Text = mapTitle;
			frmContent.WindowState = FormWindowState.Normal;

			m_dContents[mapTitle]=frmContent;
			frmContent.Show();
			frmContent.WindowState = FormWindowState.Maximized;
			frmContent.Show();

			/*
			String sTxt = "";
			if (frmContent.panel2.Visible)
				sTxt = String.Format("true,{0},{1},{2},{3}",frmContent.panel2.Location.X,frmContent.panel2.Location.Y,frmContent.panel2.Size.Width,frmContent.panel2.Size.Height);
			else
				sTxt = String.Format("false,{0},{1},{2},{3}",frmContent.panel2.Location.X,frmContent.panel2.Location.Y,frmContent.panel2.Size.Width,frmContent.panel2.Size.Height);
			MessageBox.Show(sTxt);
			*/
		}

		public  void OpenLearnmap(Form mdiParent, string mapTitle,string work)
		{
			OpenLearnmap(mdiParent,mapTitle);
			FrmContent frm = (FrmContent)m_dContents[mapTitle];
			if (frm!=null)
			{
				string [] aTitles;
				Utilities.SplitPath(work,out aTitles);
				frm.AddPage(aTitles[3],work,"",true);
			}
		}
			
		public bool HasLearnmap(string mapTitle)
		{
			return (m_dContents[mapTitle] != null);
		}

		public  void JumpToLearnmap(Form mdiParent,string fromWork,int fromPageId,string mapTitle,
									string toWork,int toPageId,bool withBackJump)
		{
			OpenLearnmap(mdiParent,mapTitle,toWork);
			FrmContent frm = (FrmContent)m_dContents[mapTitle];
			if (frm!=null)
				frm.JumpToLearningPage(fromWork,fromPageId,toPageId,withBackJump);
		}
	
		public void LearnmapClosed(string mapTitle)
		{
			Form frm = (Form)m_dContents[mapTitle];
			if (frm!=null)
				m_dContents.Remove(mapTitle);
		}

		public void CloseLearnmap(string mapTitle)
		{
			Form frm = (Form)m_dContents[mapTitle];
			if (frm!=null)
				frm.Close();
		}

		public void CloseAllLearnmaps()
		{
			IEnumerator e = m_dContents.GetEnumerator();
			while(e.MoveNext())
			{
				DictionaryEntry item = (DictionaryEntry) e.Current;

				if (item.Value is FrmContent)
					((Form) item.Value).Close();
			}
		}

		public void CloseAll()
		{
			try
			{
				while(m_dContents.Count>0)
				{
	    			IEnumerator e = m_dContents.GetEnumerator();
					e.MoveNext();
					DictionaryEntry item = (DictionaryEntry) e.Current;
                    if (item.Value is FrmContent)
                    {
                        var frm = item.Value as FrmContent;
                        frm.Close();
                    }
                    else
                    {
                        Form frm = (Form)item.Value;
                        frm.Close();
                    }


	                m_dContents.Remove(item.Key);
				}
			}
			catch (System.Exception ex)
			{
                Debug.WriteLine(ex.Message);
                Debug.Assert(false);
			}
		}

		public bool IsTestActive()
		{
			IEnumerator iter = m_dContents.GetEnumerator();
			while(iter.MoveNext())
			{
				DictionaryEntry item = (DictionaryEntry) iter.Current;
				if (item.Value is FrmContent)
					if (((FrmContent)item.Value).IsTestActive())
						return true;
			}
			return false;
		}

		public void OpenCTSStudents(Form mdiParent,bool isVisible)
		{
			string title = AppHandler.LanguageHandler.GetText("SYSTEM","StudentList","Schülerliste");
			Form frm = (Form)m_dContents[title];
			if (frm!=null)
			{
				frm.Visible=isVisible;
				frm.WindowState = FormWindowState.Maximized;
				frm.Activate();
				return;
			}
			
			XFrmOnlineUsers frmStudents = new XFrmOnlineUsers();
			frmStudents.MdiParent = mdiParent;
			frmStudents.Text = title;
			frmStudents.WindowState = FormWindowState.Maximized;
			
			if (isVisible)
				frmStudents.Show();
			else
				frmStudents.Hide();

			m_dContents[title]=frmStudents;
		}

        public void CloseCTSStudents()
        {
            string title = AppHandler.LanguageHandler.GetText("SYSTEM", "StudentList", "Schülerliste");
            object actFrm = m_dContents[title];
            if (actFrm != null && actFrm is XFrmOnlineUsers)
                ((XFrmOnlineUsers)actFrm).Close();
        }

		public Form GetCTSStudents()
		{
			foreach(Form f in m_dContents.Values)
				if (f is XFrmOnlineUsers)
					return (XFrmOnlineUsers)f;
			return null;
		}

		public  void CTSStudentsClosed()
		{
			string title=AppHandler.LanguageHandler.GetText("SYSTEM","StudentList","Schülerliste");
			object actFrm=m_dContents[title];
			if (actFrm!=null && actFrm is XFrmOnlineUsers)
				m_dContents.Remove(title);
		}

		public  void OpenLearnmapEditor(Form mdiParent, string mapTitle)
		{
			string txt=AppHandler.LanguageHandler.GetText("SYSTEM","Edit_Learnmap","Lernmappe editieren");
			string title = txt+" - "+mapTitle;

            FrmLMEditContentNew actFrm = (FrmLMEditContentNew)m_dContents[title];
			if (actFrm!=null)
			{
				actFrm.Activate();
				return;
			}
			else
				CloseAllLearnmapEditors();

			FrmLMEditContentNew frm= new FrmLMEditContentNew(mapTitle);
			frm.MdiParent = mdiParent;
			frm.Text = title;
			frm.WindowState = FormWindowState.Maximized;

			m_dContents[title] = frm;
			frm.Show();
		}

		public  void LearnmapEditorClosed(string mapTitle)
		{
			string txt=AppHandler.LanguageHandler.GetText("SYSTEM","Edit_Learnmap","Lernmappe editieren");
			string title = txt+" - "+mapTitle;

			object actFrm=m_dContents[title];
			if (actFrm!=null && actFrm is FrmLMEditContentNew)
				m_dContents.Remove(title);
		}

		public  void CloseLearnmapEditor(string mapTitle)
		{
			string txt=AppHandler.LanguageHandler.GetText("SYSTEM","Edit_Learnmap","Lernmappe editieren");
			string title = txt+" - "+mapTitle;

			object actFrm=m_dContents[title];
            if (actFrm != null && actFrm is FrmLMEditContentNew)
                ((FrmLMEditContentNew)actFrm).Close();
		}

		public void CloseAllLearnmapEditors()
		{
			Stack sToDel=new Stack();
			IDictionaryEnumerator e = m_dContents.GetEnumerator();
			while(e.MoveNext())
			{
				DictionaryEntry item = (DictionaryEntry) e.Current;
                if (item.Value is FrmLMEditContentNew)
					sToDel.Push(item.Value);
			}

			while(sToDel.Count>0)
				((Form)sToDel.Pop()).Close();
		}

		public  void OpenChatroom(Form mdiParent, int roomId,Queue qMsgs)
		{
			string txt=AppHandler.LanguageHandler.GetText("SIDEBAR","ChatroomBtnDescription","Chatraum");
			string title = String.Format("{0} {1}",txt,roomId);

			FrmChatroom actFrm = (FrmChatroom) m_dContents[title];
			if (actFrm!=null)
			{
				actFrm.Activate();
				return;
			}
			else
				CloseAllChatrooms();

			FrmChatroom frm= new FrmChatroom(roomId,qMsgs);
			frm.MdiParent = mdiParent;
			frm.Text = title;
			frm.WindowState = FormWindowState.Maximized;

			m_dContents[title] = frm;
			frm.Show();
		}

		public  void ChatroomClosed(int roomId)
		{
			string txt=AppHandler.LanguageHandler.GetText("SIDEBAR","ChatroomBtnDescription","Chatraum");
			string title = String.Format("{0} {1}",txt,roomId);
			
			object actFrm=m_dContents[title];
			if (actFrm!=null && actFrm is FrmChatroom)
				m_dContents.Remove(title);
		}

		public  void CloseChatroom(int roomId)
		{
			string txt=AppHandler.LanguageHandler.GetText("SIDEBAR","ChatroomBtnDescription","Chatraum");
			string title = String.Format("{0} {1}",txt,roomId);

			object actFrm=m_dContents[title];
			if (actFrm!=null && actFrm is FrmChatroom)
				((FrmChatroom)actFrm).Close();
		}

		public void CloseAllChatrooms()
		{
			Stack sToDel=new Stack();
			IDictionaryEnumerator e = m_dContents.GetEnumerator();
			while(e.MoveNext())
			{
				DictionaryEntry item = (DictionaryEntry) e.Current;
				if (item.Value is FrmChatroom)
					sToDel.Push(item.Value);
			}

			while(sToDel.Count>0)
				((Form)sToDel.Pop()).Close();
		}

		public Form GetChatroom(int roomId)
		{
			foreach(Form f in m_dContents.Values)
				if (f is FrmChatroom)
				{
					FrmChatroom cr =  f as FrmChatroom;
					if (cr.RoomId == roomId)
						return cr;
				}
			return null;
		}


        public void OpenBrowser(Form mdiParent, string title, string url, Rectangle? rect = null)
        {
			FrmBrowser actFrm = (FrmBrowser) m_dContents[title];
			if (actFrm!=null)
			{
				actFrm.Activate();
				return;
			}

			FrmBrowser frm= new FrmBrowser();
			frm.MdiParent = mdiParent;
			frm.Show();
			frm.URL=url;
			frm.Title=title;
			frm.Text = title;

            if (rect.HasValue)
            {
                frm.WindowState = FormWindowState.Normal;
                frm.Top = rect.Value.Top;
                frm.Left = rect.Value.Left;
                frm.Width = rect.Value.Width;
                frm.Height = rect.Value.Height;
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
            }

			m_dContents[title] = frm;
		}	

		public void CloseBrowser(string title,string url)
		{
			object actFrm=m_dContents[title];
			if (actFrm!=null && actFrm is FrmBrowser)
				((FrmBrowser)actFrm).Close();
		}

        public Form GetBrowser(string title)
        {
            Form frm = (Form)m_dContents[title];
            if (frm != null && frm is FrmBrowser)
                return frm;
            return null;
        }
        
        public void BrowserClosed(string title)
		{
			object actFrm=m_dContents[title];
			if (actFrm!=null && actFrm is FrmBrowser)
				m_dContents.Remove(title);
		}

		public void OpenContentEditor(Form mdiParent)
		{
			string title = "Inhalte editieren";

			FrmEditContent actFrm = (FrmEditContent) m_dContents[title];
			if (actFrm!=null)
			{
				actFrm.Activate();
				return;
			}

			FrmEditContent frm= new FrmEditContent();
			frm.MdiParent = mdiParent;
			frm.Text = title;
			frm.WindowState = FormWindowState.Maximized;

			m_dContents[title] = frm;
			frm.Show();
		}

		public  void CloseContentEditor()
		{
			string title = "Inhalte editieren";

			object actFrm=m_dContents[title];
			if (actFrm!=null && actFrm is FrmEditContent)
				((FrmEditContent)actFrm).Close();
		}

		public  void ContentEditorClosed()
		{
			string title = "Inhalte editieren";
			object actFrm=m_dContents[title];
			if (actFrm!=null && actFrm is FrmEditContent)
				m_dContents.Remove(title);
		}

		public void OpenUserEditor(Form mdiParent)
		{
			string title = "Userliste editieren";
			XFrmUserEdit actFrm = (XFrmUserEdit) m_dContents[title];
			if (actFrm!=null)
			{
				actFrm.Activate();
				return;
			}

			XFrmUserEdit frm= new XFrmUserEdit();
			frm.MdiParent = mdiParent;
			frm.Text = title;
			frm.WindowState = FormWindowState.Maximized;

			m_dContents[title] = frm;
			frm.Show();
		}
		
		public void CloseUserEditor()
		{
			object actFrm=m_dContents["Userliste editieren"];
			if (actFrm!=null && actFrm is XFrmUserEdit)
				((XFrmUserEdit)actFrm).Close();
		}

		public void UserEditorClosed()
		{
			string title = "Userliste editieren";
			Form frm = (Form)m_dContents[title];
			if (frm!=null)
				m_dContents.Remove(title);
		}


		public void OpenLearnmapDistributor(Form mdiParent)
		{
			string title = "Lernmappe verteilen";
			XFrmMapsEdit actFrm = (XFrmMapsEdit) m_dContents[title];
			if (actFrm!=null)
			{
				actFrm.Show();
				actFrm.Activate();
				return;
			}

			XFrmMapsEdit frm= new XFrmMapsEdit();
			frm.MdiParent = mdiParent;
			frm.Text = title;
			frm.WindowState = FormWindowState.Maximized;

			m_dContents[title] = frm;
			frm.Show();
		}

		public void CloseLearnmapDistributor()
		{
			object actFrm=m_dContents["Lernmappe verteilen"];
			if (actFrm!=null && actFrm is XFrmMapsEdit)
				((XFrmMapsEdit)actFrm).Close();
		}

		public void LearnmapDistributorClosed()
		{
			string title = "Lernmappe verteilen";
			Form frm = (Form)m_dContents[title];
			if (frm!=null)
				m_dContents.Remove(title);
		}

        public bool HasClassroom(string classname)
        {
            string txt = AppHandler.LanguageHandler.GetText("SIDEBAR", "ClassroomBtnDescription", "Klasse");
            string title = String.Format("{0}: {1}", txt, classname);
            return (m_dContents[title] != null);
        }


        public void OpenClassroom(Form mdiParent, string classname)
		{
			string txt=AppHandler.LanguageHandler.GetText("SIDEBAR","ClassroomBtnDescription","Klasse");
            string title = String.Format("{0}: {1}", txt, classname);

			XFrmClassroomEdit actFrm = (XFrmClassroomEdit) m_dContents[title];
			if (actFrm!=null)
			{
				actFrm.Activate();
				return;
			}
			else
				CloseAllClassrooms();

            XFrmClassroomEdit frm = new XFrmClassroomEdit(classname);
			frm.MdiParent = mdiParent;
			frm.Text = title;
			frm.WindowState = FormWindowState.Maximized;

			m_dContents[title] = frm;
			frm.Show();
		}

		public  void ClassroomClosed(string classname)
		{
            string txt = AppHandler.LanguageHandler.GetText("SIDEBAR", "ClassroomBtnDescription", "Klasse");
            string title = String.Format("{0}: {1}", txt, classname);

			object actFrm=m_dContents[title];
			if (actFrm!=null && actFrm is XFrmClassroomEdit)
				m_dContents.Remove(title);
		}

		public  void CloseClassroom(string classname)
		{
            string txt = AppHandler.LanguageHandler.GetText("SIDEBAR", "ClassroomBtnDescription", "Klasse");
            string title = String.Format("{0}: {1}", txt, classname);

			object actFrm=m_dContents[title];
			if (actFrm!=null && actFrm is XFrmClassroomEdit)
				((XFrmClassroomEdit)actFrm).Close();
		}

		public void CloseAllClassrooms()
		{
			Stack sToDel=new Stack();
			IDictionaryEnumerator e = m_dContents.GetEnumerator();
			while(e.MoveNext())
			{
				DictionaryEntry item = (DictionaryEntry) e.Current;
				if (item.Value is XFrmClassroomEdit)
					sToDel.Push(item.Value);
			}

			while(sToDel.Count>0)
				((Form)sToDel.Pop()).Close();
		}

	}
}
