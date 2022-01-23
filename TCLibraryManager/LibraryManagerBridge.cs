using System;
using System.Collections.Generic;

namespace SoftObject.TrainConcept.Libraries
{
    public class LibraryManagerBridge
    {
        public event OnLibraryManagerHandler LibraryManagerEventHandler;
        public event OnLibraryManagerHandler LibraryManagerEvent
        {
            add { LibraryManagerEventHandler += value; }
            remove { LibraryManagerEventHandler -= value; }
        }

        private ILibraryManager m_imp;

        public void SetAdapter(ILibraryAdapter adapter)
        {
            m_imp.SetAdapter(adapter);
        }

        public LibraryManagerBridge(ILibraryManager imp)
        {
            m_imp = imp;
            m_imp.SetParent(this);
        }

        public bool Open(string libsFolder, string fileName)
        {
            return m_imp.Open(libsFolder, fileName);
        }

        public bool OpenFromImport(string libsFolder, string fileName)
        {
            return m_imp.OpenFromImport(libsFolder, fileName);
        }

        public bool Create(string filename, string title)
        {
            return m_imp.Create(filename, title);
        }

        public bool Initialize()
        {
            return m_imp.Initialize();
        }

        public void Close(int libId)
        {
            m_imp.Close(libId);
        }

        public void Close(string libTitle)
        {
            m_imp.Close(libTitle);
        }

        public void CloseAll()
        {
            m_imp.CloseAll();
        }

        public bool GetPageCnt(string path, out int pageCnt)
        {
            return m_imp.GetPageCnt(path, out pageCnt);
        }

        public int GetPageCnt()
        {
            return m_imp.GetPageCnt();
        }

        public int GetPageCnt(int libId)
        {
            return m_imp.GetPageCnt(libId);
        }

        public PageItem GetPage(string path, int pageId)
        {
            return m_imp.GetPage(path, pageId);
        }

        public bool SetPage(string path, PageItem item, int pageId)
        {
            return m_imp.SetPage(path, item, pageId);
        }

        public bool AddPage(string path, PageItem pageItem)
        {
            return m_imp.AddPage(path, pageItem);
        }

        public bool DeletePage(string path, int iPageId)
        {
            return m_imp.DeletePage(path,iPageId);
        }

        public bool MovePage(string path, int iPageId, int iDelta)
        {
            return m_imp.MovePage(path, iPageId,iDelta);
        }

        public void AddPageAction(PageItem page, ActionItem item)
        {
            m_imp.AddPageAction(page, item);
        }
        
        public bool DeletePageAction(PageItem page, int iActionId)
        {
            return m_imp.DeletePageAction(page, iActionId);
        }

        public bool DeletePageAction(PageItem page, string actId)
        {
            return m_imp.DeletePageAction(page, actId);
        }

        public ActionItem GetPageAction(PageItem page, string actId)
        {
            return m_imp.GetPageAction(page, actId);
        }

        public ActionItem GetPageAction(PageItem page, int actId)
        {
            return m_imp.GetPageAction(page, actId);
        }

        public bool SetPageAction(PageItem page, ActionItem action, int actionId)
        {
            return m_imp.SetPageAction(page, action, actionId);
        }

        public bool GetQuestionCnt(string pointPath, out int questionCnt, bool onlyForTests = false)
        {
            return m_imp.GetQuestionCnt(pointPath, out questionCnt, onlyForTests);
        }

        public bool AddQuestion(string path, QuestionItem question)
        {
            return m_imp.AddQuestion(path, question);
        }

        public bool DeleteQuestion(string path, int iQuestionId)
        {
            return m_imp.DeleteQuestion(path, iQuestionId);
        }

        public QuestionItem GetQuestion(string path, int questionId)
        {
            return m_imp.GetQuestion(path, questionId);
        }

        public bool SetQuestion(string path, QuestionItem question, int questionId)
        {
            return m_imp.SetQuestion(path, question, questionId);
        }

        public void AddQuestionAction(QuestionItem question, ActionItem item)
        {
            m_imp.AddQuestionAction(question, item);
        }

        public bool DeleteQuestionAction(QuestionItem question, int iActionId)
        {
            return m_imp.DeleteQuestionAction(question, iActionId);
        }

        public ActionItem GetQuestionAction(QuestionItem question, string actId)
        {
            return m_imp.GetQuestionAction(question, actId);
        }

        public ActionItem GetQuestionAction(QuestionItem question, int actId)
        {
            return m_imp.GetQuestionAction(question, actId);
        }

        public bool SetQuestionAction(QuestionItem question, ActionItem action, int actionId)
        {
            return m_imp.SetQuestionAction(question, action, actionId);
        }

        public int GetQuestions(string path, ref QuestionCollection aQuestions, bool useExaming, bool useTesting)
        {
            return m_imp.GetQuestions(path, ref aQuestions, useExaming, useTesting);
        }

        public int GetKeywords(string path, ref KeywordCollection aKeywords)
        {
            return m_imp.GetKeywords(path, ref aKeywords);
        }

        public void AddKeyword(string path, int pageId, KeywordActionItem keywordAction)
        {
            m_imp.AddKeyword(path, pageId, keywordAction);
        }

        public int GetLibraryCnt()
        {
            return m_imp.GetLibraryCnt();
        }

        public LibraryItem GetLibrary(int libId)
        {
            return m_imp.GetLibrary(libId);
        }

        public LibraryItem GetLibrary(string title)
        {
            return m_imp.GetLibrary(title);
        }

        public bool SetLibrary(string path, LibraryItem item)
        {
            return m_imp.SetLibrary(path, item);
        }

        public bool SetLibraryTitle(string path, string title)
        {
            return m_imp.SetLibraryTitle(path, title);
        }

        public bool AddLibrary(string title, bool prev)
        {
            return m_imp.AddLibrary(title, prev);
        }

        public bool DeleteLibrary(string path)
        {
            return m_imp.DeleteLibrary(path);
        }

        public string GetFilePath(string title)
        {
            return m_imp.GetFilePath(title);
        }

        public string GetFileName(string path)
        {
            return m_imp.GetFileName(path);
        }

        public BookItem GetBook(string path)
        {
            return m_imp.GetBook(path);
        }

        public bool SetBook(string path, BookItem item)
        {
            return m_imp.SetBook(path, item);
        }

        public bool SetBookTitle(string path, string title)
        {
            return m_imp.SetBookTitle(path, title);
        }

        public bool AddBook(string path, string title, bool prev)
        {
            return m_imp.AddBook(path, title, prev);
        }

        public bool DeleteBook(string path)
        {
            return m_imp.DeleteBook(path);
        }

        public bool MoveBook(string libPath, string title1, string title2)
        {
            return m_imp.MoveBook(libPath,title1, title2);
        }

        public int GetBookCnt(string path)
        {
            return m_imp.GetBookCnt(path);
        }

        public ChapterItem GetChapter(string path)
        {
            return m_imp.GetChapter(path);
        }

        public bool SetChapter(string path, ChapterItem item)
        {
            return m_imp.SetChapter(path, item);
        }

        public bool SetChapterTitle(string path, string title)
        {
            return m_imp.SetChapterTitle(path, title);
        }

        public bool AddChapter(string path, string title, bool prev)
        {
            return m_imp.AddChapter(path, title, prev);
        }

        public bool DeleteChapter(string path)
        {
            return m_imp.DeleteChapter(path);
        }

        public bool MoveChapter(string bookPath, string title1, string title2)
        { 
            return m_imp.MoveChapter(bookPath,title1,title2);
        }

        public int GetChapterCnt(string path)
        {
            return m_imp.GetChapterCnt(path);
        }

        public PointItem GetPoint(string path)
        {
            return m_imp.GetPoint(path);
        }

        public bool SetPoint(string path, PointItem item)
        {
            return m_imp.SetPoint(path, item);
        }

        public bool SetPointTitle(string path, string title)
        {
            return m_imp.SetPointTitle(path, title);
        }

        public bool AddPoint(string path, string title, bool prev)
        {
            return m_imp.AddPoint(path, title, prev);
        }

        public bool DeletePoint(string path)
        {
            return m_imp.DeletePoint(path);
        }

        public bool MovePoint(string chpPath, string title1, string title2)
        {
            return m_imp.MovePoint(chpPath, title1, title2);
        }

        public int GetPointCnt(string path)
        {
            return m_imp.GetPointCnt(path);
        }

        public bool Reload(string path)
        {
            return m_imp.Reload(path);
        }

        public void SetModified(string path)
        {
            m_imp.SetModified(path);
        }

        public SortedList<string, Dictionary<string, string>> GetDocumentFilenames()
        {
            return m_imp.GetDocumentFilenames();
        }

        public void FireEvent(ref LibraryManagerEventArgs ea)
        {
            if (LibraryManagerEventHandler != null)
                LibraryManagerEventHandler(this, ref ea);
        }

    }
}
