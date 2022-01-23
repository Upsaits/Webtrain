using System;

namespace SoftObject.TrainConcept.Libraries
{
	public delegate void OnTestResultsChangedHandler(object sender,EventArgs e);

	public interface ITestResultManager
	{
		void SetParent(object parent);
		bool Open(string fileName);
		int Start(string userName,string mapName,string testName,DateTime startTime);
        int End(string userName, string mapName, string testName, DateTime endTime, WorkoutInfoItemCollection aWorkouts);
		int End(TestResultItem resultItem);
        void Delete(string userName, string mapName, string testName, int id);
		void Delete(string mapName,int id);
        void Delete(int id);
		TestResultItem Get(int id);
        TestResultItem Get(string mapName, int id);
        int Find(out TestResultItemCollection aCollection, string userName, string mapName, string testName, bool bGetOnlyWorkedout);
		int Find(ref TestResultItemCollection aCollection, string userName,string mapName,string testName);
	    int Find(ref TestResultItemCollection aCollection, string userName, string mapName);
		int Find(ref TestResultItemCollection aCollection, string mapName);
		TestResultItem CreateResultItem(string sStr);
		void Save();
        void SetAdapter(ITestResultAdapter adapter);
	}

}
