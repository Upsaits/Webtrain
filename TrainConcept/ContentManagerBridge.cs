using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoftObject.TrainConcept.Interfaces;

namespace SoftObject.TrainConcept
{
    public class ContentManagerBridge
    {
        private IContentManager m_imp;

        public ContentManagerBridge(IContentManager imp)
        {
            m_imp = imp;
        }

        public void OpenLearnmap(Form mdiParent, string mapTitle)
        {
            m_imp.OpenLearnmap(mdiParent, mapTitle);
        }

        public void OpenLearnmap(Form mdiParent, string mapTitle, string work)
        {
            m_imp.OpenLearnmap(mdiParent, mapTitle, work);
        }

        public void JumpToLearnmap(Form mdiParent, string fromWork, int fromPageId,
                                   string mapTitle, string toWork, int toPageId, bool withBackJump)
        {
            m_imp.JumpToLearnmap(mdiParent, fromWork, fromPageId, mapTitle, toWork, toPageId, withBackJump);
        }

        public void CloseLearnmap(string mapTitle)
        {
            m_imp.CloseLearnmap(mapTitle);
        }

        public void LearnmapClosed(string mapTitle)
        {
            m_imp.LearnmapClosed(mapTitle);
        }

        public bool HasLearnmap(string mapTitle)
        {
            return m_imp.HasLearnmap(mapTitle);
        }

        public void CloseAllLearnmaps()
        {
            m_imp.CloseAllLearnmaps();
        }

        public void CloseAll()
        {
            m_imp.CloseAll();
        }

        public void SetActiveChild(Form activeChild)
        {
            m_imp.SetActiveChild(activeChild);
        }

        public Form GetActiveChild()
        {
            return m_imp.GetActiveChild();
        }

        public bool IsTestActive()
        {
            return m_imp.IsTestActive();
        }

        public void OpenCTSStudents(Form mdiParent, bool isVisible)
        {
            m_imp.OpenCTSStudents(mdiParent, isVisible);
        }

        public void CloseCTSStudents()
        {
            m_imp.CloseCTSStudents();
        }

        public Form GetCTSStudents()
        {
            return m_imp.GetCTSStudents();
        }

        public void CTSStudentsClosed()
        {
            m_imp.CTSStudentsClosed();
        }

        public void OpenLearnmapEditor(Form mdiParent, string mapTitle)
        {
            m_imp.OpenLearnmapEditor(mdiParent, mapTitle);
        }

        public void CloseLearnmapEditor(string mapTitle)
        {
            m_imp.CloseLearnmapEditor(mapTitle);
        }

        public void LearnmapEditorClosed(string mapTitle)
        {
            m_imp.LearnmapEditorClosed(mapTitle);
        }

        public void CloseAllLearnmapEditors()
        {
            m_imp.CloseAllLearnmapEditors();
        }

        public void OpenChatroom(Form mdiParent, int roomId, Queue qMsgs)
        {
            m_imp.OpenChatroom(mdiParent, roomId, qMsgs);
        }

        public void CloseChatroom(int roomId)
        {
            m_imp.CloseChatroom(roomId);
        }

        public void ChatroomClosed(int roomId)
        {
            m_imp.ChatroomClosed(roomId);
        }

        public void CloseAllChatrooms()
        {
            m_imp.CloseAllChatrooms();
        }

        public Form GetChatroom(int roomId)
        {
            return m_imp.GetChatroom(roomId);
        }

        public void OpenBrowser(Form mdiParent, string title, string url, Rectangle? rect = null)
        {
            m_imp.OpenBrowser(mdiParent, title, url, rect);
        }

        public void CloseBrowser(string title, string url)
        {
            m_imp.CloseBrowser(title, url);
        }

        public Form GetBrowser(string title)
        {
            return m_imp.GetBrowser(title);
        }

        public void BrowserClosed(string title)
        {
            m_imp.BrowserClosed(title);
        }

        public void OpenContentEditor(Form mdiParent)
        {
            m_imp.OpenContentEditor(mdiParent);
        }

        public void CloseContentEditor()
        {
            m_imp.CloseContentEditor();
        }

        public void ContentEditorClosed()
        {
            m_imp.ContentEditorClosed();
        }

        public void OpenUserEditor(Form mdiParent)
        {
            m_imp.OpenUserEditor(mdiParent);
        }

        public void CloseUserEditor()
        {
            m_imp.CloseUserEditor();
        }

        public void UserEditorClosed()
        {
            m_imp.UserEditorClosed();
        }

        public void OpenLearnmapDistributor(Form mdiParent)
        {
            m_imp.OpenLearnmapDistributor(mdiParent);
        }

        public void CloseLearnmapDistributor()
        {
            m_imp.CloseLearnmapDistributor();
        }

        public void LearnmapDistributorClosed()
        {
            m_imp.LearnmapDistributorClosed();
        }

        public bool HasClassroom(string classname)
        {
            return m_imp.HasClassroom(classname);
        }

        public void OpenClassroom(Form mdiParent, string classname)
        {
            m_imp.OpenClassroom(mdiParent, classname);
        }

        public void CloseClassroom(string classname)
        {
            m_imp.CloseClassroom(classname);
        }

        public void ClassroomClosed(string classname)
        {
            m_imp.ClassroomClosed(classname);
        }

        public void CloseAllClassrooms()
        {
            m_imp.CloseAllClassrooms();
        }
    }
}
