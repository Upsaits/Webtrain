using System;
using System.Collections.Generic;
using System.Text;
using SoftObject.TrainConcept.Libraries;
using SoftObject.UtilityLibrary;

namespace SoftObject.TrainConcept
{
    public class LearnmapBuilder
    {
        public delegate void fnAddWorking(string strPath);
        public delegate void fnAddQuestion(string strPath,int questionId,string strQuestion);

        private string          m_strMapTitle;
        private fnAddWorking    m_fnAddWorking;
        private fnAddQuestion   m_fnAddQuestion;
        private AppHandler AppHandler = Program.AppHandler;

        public LearnmapBuilder(string strMapTitle)
        {
            m_strMapTitle = strMapTitle;
            m_fnAddWorking = null;
            m_fnAddQuestion = null;
        }

        public LearnmapBuilder(string strMapTitle, fnAddWorking _fnAddWorking, fnAddQuestion _fnAddQuestion)
        {
            m_strMapTitle = strMapTitle;
            m_fnAddWorking = _fnAddWorking;
            m_fnAddQuestion = _fnAddQuestion;
        }

        public bool AddPoint(string path,string version)
        {
            if (!ExistsWorking(path))
            {
                AppHandler.MapManager.AddWorking(m_strMapTitle, path,"",version);
                AppHandler.MapManager.Save(m_strMapTitle);
                AppHandler.ContentManager.CloseLearnmap(m_strMapTitle);
                if (m_fnAddWorking!=null)
                    m_fnAddWorking(path);
                return true;
            }
            return false;
        }

        public bool AddChapter(string path, string version)
        {
            ChapterItem chp = AppHandler.LibManager.GetChapter(path);
            if (chp != null)
            {
                int cntAdded = 0;
                for (int i = 0; i < chp.Points.Length; ++i)
                {
                    string[] aPath = { path, chp.Points[i].title };
                    string sWorking = Utilities.MergePath(aPath);
                    if (!ExistsWorking(sWorking))
                    {
                        AppHandler.MapManager.AddWorking(m_strMapTitle, sWorking,"",version);
                        if (m_fnAddWorking != null)
                            m_fnAddWorking(sWorking);
                        ++cntAdded;
                    }
                }

                if (cntAdded > 0)
                {
                    AppHandler.MapManager.Save(m_strMapTitle);
                    AppHandler.ContentManager.CloseLearnmap(m_strMapTitle);
                    return true;
                }
            }
            return false;
        }

        public bool AddBook(string path, string version)
        {
            bool bResult = true;
            BookItem book = AppHandler.LibManager.GetBook(path);
            if (book != null)
                for (int i = 0; i < book.Chapters.Length; ++i)
                {
                    string[] aPath = { path, book.Chapters[i].title };
                    if (!AddChapter(Utilities.MergePath(aPath),version))
                        bResult = false;
                }
            return bResult;
        }

        public bool AddLibrary(string title, string version)
        {
            bool bResult = true;
            LibraryItem lib = AppHandler.LibManager.GetLibrary(title);
            if (lib != null)
                for (int i = 0; i < lib.Books.Length; ++i)
                {
                    string[] aPath = { title, lib.Books[i].title };
                    if (!AddBook(Utilities.MergePath(aPath),version))
                        bResult = false;
                }
            return bResult;
        }

        public bool AddTestQuestion(string path, int testId, int quId)
        {
            if (ExistsWorking(path) && !ExistsQuestion(path,testId, quId))
            {
                QuestionItem que = AppHandler.LibManager.GetQuestion(path, quId);
                AppHandler.MapManager.AddTestQuestion(m_strMapTitle,testId,path, quId);
                AppHandler.MapManager.Save(m_strMapTitle);

                if (m_fnAddQuestion != null)
                    m_fnAddQuestion(path, quId, que.question);
                return true;
            }
            return false;
        }

        private bool ExistsWorking(string working)
        {
            string[] aWorkings = null;
            if (AppHandler.MapManager.GetWorkings(m_strMapTitle, ref aWorkings))
                for (int i = 0; i < aWorkings.Length; ++i)
                    if (String.Compare(aWorkings[i], working, StringComparison.OrdinalIgnoreCase) == 0)
                        return true;
            return false;
        }

        private bool ExistsQuestion(string contentPath, int testId, int questionId)
        {
            TestQuestionItem[] aItems = null;
            if (AppHandler.MapManager.GetTestQuestions(m_strMapTitle,testId, ref aItems))
                for (int i = 0; i < aItems.Length; ++i)
                    if (String.Compare(aItems[i].contentPath, contentPath, StringComparison.OrdinalIgnoreCase) == 0 &&
                        aItems[i].questionId == questionId)
                        return true;
            return false;
        }

        public void SetUsage(bool bIsClassMap)
        {
            AppHandler.MapManager.SetUsage(m_strMapTitle, bIsClassMap);
            AppHandler.MapManager.Save(m_strMapTitle);
        }

        public void SetProgressOrientation(bool bIsContentOrientated)
        {
            AppHandler.MapManager.SetProgressOrientation(m_strMapTitle, bIsContentOrientated);
            AppHandler.MapManager.Save(m_strMapTitle);
        }
    }
}
