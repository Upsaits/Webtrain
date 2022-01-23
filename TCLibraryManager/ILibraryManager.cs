using System;
using System.Collections.Generic;

namespace SoftObject.TrainConcept.Libraries
{
    public delegate void OnLibraryManagerHandler(object sender, ref LibraryManagerEventArgs ea);
	/// <summary>
	/// Summary description for LibraryManager.
	/// </summary>
	public interface ILibraryManager
	{
        void SetParent(object parent);
        bool Open(string libsFolder,string FileName);
        bool OpenFromImport(string libsFolder, string fileName);
		bool Create(string filename,string title);
		bool Initialize();
        void CloseAll();
		void Close(int libId);
		void Close(string libTitle);
		bool GetPageCnt(string path,out int pageCnt);
		int GetPageCnt();
		int GetPageCnt(int libId);
		PageItem GetPage(string path,int pageId);
        bool SetPage(string path, PageItem item, int pageId);
        bool AddPage(string path, PageItem pageItem);
        bool DeletePage(string path, int iPageId);
        bool MovePage(string path, int iPageId,int iDelta);
        void AddPageAction(PageItem page, ActionItem item);
        bool DeletePageAction(PageItem page, int iActionId);
		bool DeletePageAction(PageItem page, string actId);
		ActionItem GetPageAction(PageItem page, string actId);
		ActionItem GetPageAction(PageItem page,int actId);
		bool SetPageAction(PageItem page,ActionItem action,int actionId);
		int GetLibraryCnt();
		LibraryItem	GetLibrary(int libId);
		LibraryItem GetLibrary(string title);
		bool SetLibrary(string path,LibraryItem item);
        bool SetLibraryTitle(string path, string title);
        bool AddLibrary(string title,bool prev);
		bool DeleteLibrary(string title);
		string GetFilePath(string title);
		string GetFileName(string path);
        bool GetQuestionCnt(string pointPath, out int questionCnt,bool onlyForTests);
		QuestionItem GetQuestion(string path,int questionId);
		bool SetQuestion(string path,QuestionItem question,int questionId);
        bool AddQuestion(string path, QuestionItem question);
        bool DeleteQuestion(string path, int iQuestionId);
		int GetQuestions(string path,ref QuestionCollection aQuestions,bool useExaming,bool useTesting);
        void AddQuestionAction(QuestionItem question, ActionItem item);
        bool DeleteQuestionAction(QuestionItem question, int iActionId);
        ActionItem GetQuestionAction(QuestionItem question, string actId);
        ActionItem GetQuestionAction(QuestionItem question, int actId);
        bool SetQuestionAction(QuestionItem question, ActionItem action, int actionId);
		int GetKeywords(string path,ref KeywordCollection aKeywords);
        void AddKeyword(string path,int pageId, KeywordActionItem keywordAction);
		BookItem GetBook(string path);
		bool SetBook(string path,BookItem item);
        bool SetBookTitle(string path, string title);
		bool AddBook(string path,string title,bool prev);
		bool DeleteBook(string path);
        bool MoveBook(string libPath, string title1, string title2);
		int  GetBookCnt(string path);
		ChapterItem GetChapter(string path);
		bool SetChapter(string path,ChapterItem item);
        bool SetChapterTitle(string path, string title);
		bool AddChapter(string path,string title,bool prev);
		bool DeleteChapter(string path);
        bool MoveChapter(string bookPath, string title1, string title2);
        int GetChapterCnt(string path);
		PointItem GetPoint(string path);
		bool SetPoint(string path,PointItem item);
        bool SetPointTitle(string path, string title);
		bool AddPoint(string path,string title,bool prev);
		bool DeletePoint(string path);
        bool MovePoint(string chpPath, string title1, string title2);
        int GetPointCnt(string path);
		bool Reload(string path);
		void SetModified(string path);
        SortedList<string,Dictionary<string,string>> GetDocumentFilenames();
        void SetAdapter(ILibraryAdapter adapter);
    }
}