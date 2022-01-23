using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace SoftObject.TrainConcept.Interfaces
{
	/// <summary>
	/// Summary description for IContentManager.
	/// </summary>
	public interface IContentManager
	{
		void SetActiveChild(Form activeChild);
        Form GetActiveChild();
		bool IsTestActive();
		void CloseAll();
		void OpenLearnmap(Form mdiParent, string mapTitle);
		void OpenLearnmap(Form mdiParent, string mapTitle,string work);
		void JumpToLearnmap(Form mdiParent,string fromWork,int fromPageId,string mapTitle,
							string toWork,int toPageId,bool withBackJump);
		void CloseLearnmap(string mapTitle);
		void LearnmapClosed(string mapTitle);
		bool HasLearnmap(string mapTitle);
		void CloseAllLearnmaps();
		void OpenLearnmapEditor(Form mdiParent, string mapTtitle);
		void CloseLearnmapEditor(string mapTitle);
		void LearnmapEditorClosed(string mapTitle);
		void CloseAllLearnmapEditors();
		void OpenChatroom(Form mdiParent,int roomId,Queue qMsgs);
		void CloseChatroom(int roomId);
		void ChatroomClosed(int roomId);
		void CloseAllChatrooms();
		Form GetChatroom(int roomId);
		void OpenCTSStudents(Form mdiParent,bool isVisible);
        void CloseCTSStudents();
		Form GetCTSStudents();
		void CTSStudentsClosed();
		void OpenBrowser(Form mdiParent, string title,string url, Rectangle? rect = null);
		void CloseBrowser(string title,string url);
		void BrowserClosed(string title);
        Form GetBrowser(string title);
		void OpenContentEditor(Form mdiParent);
		void CloseContentEditor();
		void ContentEditorClosed();
		void OpenUserEditor(Form mdiParent);
		void CloseUserEditor();
		void UserEditorClosed();
		void OpenLearnmapDistributor(Form mdiParent);
		void CloseLearnmapDistributor();
		void LearnmapDistributorClosed();
        bool HasClassroom(string classname);
        void OpenClassroom(Form mdiParent,string classname);
		void CloseClassroom(string classname);
		void ClassroomClosed(string classname);
        void CloseAllClassrooms();
	}

}
