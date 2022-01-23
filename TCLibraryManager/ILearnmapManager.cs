using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SoftObject.TrainConcept.Libraries
{
    public delegate void OnLearnmapManagerHandler(object sender, ref LearnmapManagerEventArgs ea);
    public enum TestType { Intermediate, Final };
    
	public interface ILearnmapManager
	{
        void SetParent(object parent);
        bool OpenFromDirectory(string directory);
		bool Open(string fileName);
        bool OpenFromImport(string mapsFolder, string fileName);
        bool OpenSilent(string fileName);
		void Close(string mapTitle);
		void CloseAll();
		void Save(int mapId);
		void Save(string mapTitle);
		bool Create(string filename,string title,bool isServerMap);
        bool CreateSilent(string filename, string title);
		void Destroy(string mapTitle);
		int  GetMapCnt();
		bool GetTitle(int mapId,out string title);
		bool GetColor(int mapId,out string color);
        bool GetColor(string mapTitle,out string color);
        bool SetColor(int mapId, string color);
        bool SetColor(string mapTitle, string color);
        int GetId(string title);
        LearnmapItem GetItem(string title);
        bool SetItem(string title, LearnmapItem item);
        bool GetWorkings(int id, ref string[] aWorkings, bool bIgnoreEmpty);
        bool GetWorkings(string mapTitle, ref string[] aWorkings, bool bIgnoreEmpty);
	    bool GetWorkingTests(int id, ref string[] aTests);
	    bool GetWorkingTests(string mapTitle, ref string[] aTests);
        bool AddWorking(string mapTitle, string link, string test, string version);
		bool DeleteWorking(string mapTitle,string link,string test);
        void MoveWorking(string mapTitle, int iId, int iDelta);
		string FindMapByWorking(string working);
        int FindMapsByWorking(string working, out List<KeyValuePair<string, string>> dMaps);
		bool SetUsers(string title,string[] aNames);
		bool GetUsers(int id,ref string[] aNames);
		bool GetUsers(string title,ref string[] aNames);
		bool HasUser(int mapId,string name);
		bool HasUser(string title,string name);
		bool AddUser(string title,string name);
		bool DeleteUser(string title,string name);
		bool DeleteUser(string name);
		string GetFileName(int id);
		string GetFileName(string title);
        bool SetTest(string mapTitle, int testId, string title, bool randomChoose, int questionCnt,
                            int trialCnt, int successLevel, string questionnaire, bool testAlwaysAllowed, TestType eType);
        bool SetTest(int mapId, int testId, string title, bool randomChoose, int questionCnt,
                            int trialCnt, int successLevel, string questionnaire, bool testAlwaysAllowed, TestType eType);
        TestItem GetTest(string mapTitle, int id);
	    TestItem GetTest(string mapTitle, string testName);
        bool GetTest(string mapTitle, int testId,ref bool randomChoose, ref int questionCnt,
                            ref int trialCnt, ref int successLevel, ref string questionnare, ref bool testAlwaysAllowed , ref TestType type);
        bool GetTest(int mapId, int testId, ref bool randomChoose, ref int questionCnt,
                            ref int trialCnt, ref int successLevel, ref string questionnaire, ref bool testAlwaysAllowed, ref TestType type);
	    int GetTestCount(string mapTitle);
		bool AddTestQuestion(string mapTitle,int testId, string contentPath,int questionId);
		bool AddTestQuestion(int mapId,int testId,string contentPath,int questionId);
        bool DeleteTestQuestion(int mapId, int testId, string contentPath, int questionId);
		bool DeleteTestQuestion(string mapTitle,int testId, string contentPath,int questionId);
        bool GetTestQuestions(string mapTitle, int testId, ref TestQuestionItem[] aTestQuestions);
        bool GetTestQuestions(int id, int testId, ref TestQuestionItem[] aTestQuestions);
	    bool DeleteTest(int mapId, int testId);
	    bool DeleteTest(string mapTitle, int testId);
        void SetAdapter(ILearnmapAdapter adapter);
        bool SetUsage(string mapTitle,bool bIsClassMap);
        bool GetUsage(string mapTitle, ref bool bIsClassMap);
        bool GetUsage(int mapId, ref bool bIsClassMap);
        bool GetProgressOrientation(string mapTitle, ref bool bIsContentOrientated);
        bool GetProgressOrientation(int mapId, ref bool bIsContentOrientated);
        bool SetProgressOrientation(string mapTitle,bool bIsContentOrientated);
		CollectionBase GetAllKeywords();
	}
}