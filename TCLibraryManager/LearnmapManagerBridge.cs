using System;
using System.Collections;
using System.Collections.Generic;

namespace SoftObject.TrainConcept.Libraries
{
    public class LearnmapManagerBridge
    {
        public event OnLearnmapManagerHandler LearnmapManagerEventHandler;
        public event OnLearnmapManagerHandler LearnmapManagerEvent
        {
            add { LearnmapManagerEventHandler += value; }
            remove { LearnmapManagerEventHandler -= value; }
        }

        private ILearnmapManager m_imp;

        public LearnmapManagerBridge(ILearnmapManager imp)
        {
            m_imp = imp;
            m_imp.SetParent(this);
        }

        public bool OpenFromDirectory(string directory)
        {
            return m_imp.OpenFromDirectory(directory);
        }
        
        public bool Open(string fileName)
        {
            return m_imp.Open(fileName);
        }

        public bool OpenFromImport(string mapsFolder, string fileName)
        {
            return m_imp.OpenFromImport(mapsFolder,fileName);
        }

        public bool OpenSilent(string fileName)
        {
            return m_imp.OpenSilent(fileName);
        }
        
        public void Close(string mapTitle)
        {
            m_imp.Close(mapTitle);
        }

        public void CloseAll()
        {
            m_imp.CloseAll();
        }

        public void Save(int mapId)
        {
            m_imp.Save(mapId);
        }

        public void Save(string mapTitle)
        {
            m_imp.Save(mapTitle);
        }

        public bool Create(string filename, string title,bool isServerMap)
        {
            return m_imp.Create(filename, title, isServerMap);
        }

        public bool CreateSilent(string filename, string title)
        {
            return m_imp.CreateSilent(filename, title);
        }

        public void Destroy(string title)
        {
            m_imp.Destroy(title);
        }

        public int GetMapCnt()
        {
            return m_imp.GetMapCnt();
        }

        public bool GetTitle(int id, out string title)
        {
            return m_imp.GetTitle(id, out title);
        }

        public bool GetColor(int mapId, out string color)
        {
            return m_imp.GetColor(mapId, out color);
        }

        public bool GetColor(string mapTitle, out string color)
        {
            return m_imp.GetColor(mapTitle, out color);
        }

        public bool SetColor(int mapId, string color)
        {
            return m_imp.SetColor(mapId, color);
        }

        public bool SetColor(string mapTitle, string color)
        {
            return m_imp.SetColor(mapTitle, color);
        }

        public int GetId(string title)
        {
            return m_imp.GetId(title);
        }

        public bool SetItem(string title, LearnmapItem item)
        {
            return m_imp.SetItem(title, item);
        }

        public bool GetWorkings(int id, ref string[] aWorkings, bool bIgnoreEmpty=true)
        {
            return m_imp.GetWorkings(id, ref aWorkings,bIgnoreEmpty);
        }

        public bool GetWorkings(string title, ref string[] aWorkings, bool bIgnoreEmpty=true)
        {
            return m_imp.GetWorkings(title, ref aWorkings,bIgnoreEmpty);
        }

        public bool GetWorkingTests(int id, ref string[] aTests)
        {
            return m_imp.GetWorkingTests(id, ref aTests);
        }

        public bool GetWorkingTests(string mapTitle, ref string[] aTests)
        {
            return m_imp.GetWorkingTests(mapTitle, ref aTests);
        }

        public bool AddWorking(string mapTitle, string link,string test,string version)
        {
            return m_imp.AddWorking(mapTitle, link, test,version);
        }

        public bool DeleteWorking(string mapTitle, string link,string test="")
        {
            return m_imp.DeleteWorking(mapTitle, link,test);
        }

        public void MoveWorking(string mapTitle, int iId, int iDelta)
        {
            m_imp.MoveWorking(mapTitle, iId, iDelta);
        }

        public string FindMapByWorking(string working)
        {
            return m_imp.FindMapByWorking(working);
        }

        public int FindMapsByWorking(string working, out List<KeyValuePair<string, string>> dMaps)
        {
            return m_imp.FindMapsByWorking(working, out dMaps);
        }

        public CollectionBase GetAllKeywords()
        {
            return m_imp.GetAllKeywords();
        }

        public bool SetUsers(string title, string[] aNames)
        {
            return m_imp.SetUsers(title, aNames);
        }

        public bool GetUsers(int id, ref string[] aNames)
        {
            return m_imp.GetUsers(id, ref aNames);
        }

        public bool GetUsers(string title, ref string[] aNames)
        {
            return m_imp.GetUsers(title, ref aNames);
        }

        public bool HasUser(int mapId, string name)
        {
            return m_imp.HasUser(mapId, name);
        }

        public bool HasUser(string title, string name)
        {
            return m_imp.HasUser(title, name);
        }

        public bool AddUser(string title, string name)
        {
            return m_imp.AddUser(title, name);
        }

        public bool DeleteUser(string title, string name)
        {
            return m_imp.DeleteUser(title, name);
        }

        public bool DeleteUser(string name)
        {
            return m_imp.DeleteUser(name);
        }

        public LearnmapItem GetItem(string title)
        {
            return m_imp.GetItem(title);
        }

        public string GetFileName(int id)
        {
            return m_imp.GetFileName(id);
        }

        public string GetFileName(string title)
        {
            return m_imp.GetFileName(title);
        }

        public bool SetTest(string mapTitle, int testId, string title, bool randomChoose, int questionCnt,
                            int trialCnt, int successLevel, string questionnaire, bool testAlwaysAllowed, TestType eType)
        {
            return m_imp.SetTest(mapTitle, testId, title, randomChoose, questionCnt, trialCnt, successLevel, questionnaire, testAlwaysAllowed, eType);}

        public bool SetTest(int mapId, int testId, string title, bool randomChoose, int questionCnt,
                            int trialCnt, int successLevel, string questionnaire, bool testAlwaysAllowed, TestType eType)
        {
            return m_imp.SetTest(mapId, testId, title, randomChoose, questionCnt,trialCnt, successLevel, questionnaire,testAlwaysAllowed,eType);
        }

        public TestItem GetTest(string mapTitle, int id)
        {
            return m_imp.GetTest(mapTitle, id);
        }

        public TestItem GetTest(string mapTitle, string testName)
        {
            return m_imp.GetTest(mapTitle, testName);
        }
        
        public bool GetTest(string mapTitle, int testId, ref bool randomChoose, ref int questionCnt,
                            ref int trialCnt, ref int successLevel, ref string questionnaire, ref bool testAlwaysAllowed, ref TestType type)
        {
            return m_imp.GetTest(mapTitle, testId, ref randomChoose, ref questionCnt, 
                                 ref trialCnt, ref successLevel,ref questionnaire,ref testAlwaysAllowed, ref type);
        }

        public bool GetTest(int mapId, int testId, ref bool randomChoose, ref int questionCnt,
                            ref int trialCnt, ref int successLevel, ref string questionnaire, ref bool testAlwaysAllowed, ref TestType type)
        {
            return m_imp.GetTest(mapId, testId, ref randomChoose, ref questionCnt,
                                 ref trialCnt, ref successLevel, ref questionnaire,ref testAlwaysAllowed, ref type);
        }

        public int GetTestCount(string mapTitle)
        {
            return m_imp.GetTestCount(mapTitle);
        }

        public bool AddTestQuestion(string mapTitle, int testId, string contentPath, int questionId)
        {
            return m_imp.AddTestQuestion(mapTitle, testId, contentPath, questionId);
        }

        public bool AddTestQuestion(int mapId, int testId, string contentPath, int questionId)
        {
            return m_imp.AddTestQuestion(mapId, testId,contentPath, questionId);
        }

        public bool DeleteTestQuestion(int mapId, int testId, string contentPath, int questionId)
        {
            return m_imp.DeleteTestQuestion(mapId, testId, contentPath, questionId);
        }

        public bool DeleteTestQuestion(string mapTitle, int testId, string contentPath, int questionId)
        {
            return m_imp.DeleteTestQuestion(mapTitle, testId, contentPath, questionId);
        }

        public bool GetTestQuestions(string mapTitle, int testId, ref TestQuestionItem[] aTestQuestions)
        {
            return m_imp.GetTestQuestions(mapTitle, testId,ref aTestQuestions);
        }

        public bool GetTestQuestions(int id, int testId, ref TestQuestionItem[] aTestQuestions)
        {
            return m_imp.GetTestQuestions(id, testId, ref aTestQuestions);
        }

        public bool DeleteTest(int mapId, int testId)
        {
            return m_imp.DeleteTest(mapId, testId);
        }

        public bool DeleteTest(string mapTitle, int testId)
        {
            return m_imp.DeleteTest(mapTitle, testId);
        }

        public void FireEvent(ref LearnmapManagerEventArgs ea)
        {
            if (LearnmapManagerEventHandler != null)
                LearnmapManagerEventHandler(this, ref ea);
        }

        public void SetAdapter(ILearnmapAdapter adapter)
        {
            m_imp.SetAdapter(adapter);
        }

        public bool SetUsage(string mapTitle, bool bIsClassMap)
        {
            return m_imp.SetUsage(mapTitle, bIsClassMap);
        }

        public bool GetUsage(string mapTitle, ref bool bIsClassMap)
        {
            return m_imp.GetUsage(mapTitle, ref bIsClassMap);
        }

        public bool GetUsage(int mapId, ref bool bIsClassMap)
        {
            return m_imp.GetUsage(mapId, ref bIsClassMap);
        }

        public bool GetProgressOrientation(string mapTitle, ref bool bIsContentOrientated)
        {
            return m_imp.GetProgressOrientation(mapTitle,ref bIsContentOrientated);
        }

        public bool SetProgressOrientation(string mapTitle, bool bIsContentOrientated)
        {
            return m_imp.SetProgressOrientation(mapTitle, bIsContentOrientated);
        }
    }
}
