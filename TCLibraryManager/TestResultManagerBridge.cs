using System;

namespace SoftObject.TrainConcept.Libraries
{
    public class TestResultManagerBridge
    {
        public event OnTestResultsChangedHandler OnChangedEventHandler;
        public event OnTestResultsChangedHandler OnChangedEvent
        {
            add
            {
                OnChangedEventHandler += value;
            }
            remove
            {
                OnChangedEventHandler -= value;
            }
        }
        private ITestResultManager m_imp;

        public void SetAdapter(ITestResultAdapter adapter)
        {
            m_imp.SetAdapter(adapter);
        }

        public TestResultManagerBridge(ITestResultManager imp)
        {
            m_imp = imp;
            m_imp.SetParent(this);
        }

        public bool Open(string fileName)
        {
            return m_imp.Open(fileName);
        }

        public TestResultItem Get(int id)
        {
            return m_imp.Get(id);
        }

        public TestResultItem Get(string mapName, int id)
        {
            return m_imp.Get(mapName, id);
        }

        public int Start(string userName, string mapName, string testName, DateTime startTime)
        {
            return m_imp.Start(userName, mapName,testName,startTime);
        }

        public int End(string userName, string mapName, string testName, DateTime endTime, WorkoutInfoItemCollection aWorkouts)
        {
            return m_imp.End(userName, mapName, testName,endTime, aWorkouts);
        }

        public int End(TestResultItem resultItem)
        {
            return m_imp.End(resultItem);
        }

        public void Delete(string userName, string mapName, string testName, int id)
        {
            m_imp.Delete(userName, mapName,testName, id);
        }

        public void Delete(string mapName, int id)
        {
            m_imp.Delete(mapName, id);
        }

        public void Delete(int id)
        {
            m_imp.Delete(id);
        }
        
        public int Find(out TestResultItemCollection aCollection, string userName, string mapName, string testName, bool isWorkedout)
        {
            return m_imp.Find(out aCollection, userName, mapName,testName, isWorkedout);
        }

        public int Find(ref TestResultItemCollection aCollection, string userName, string mapName,string testName)
        {
            return m_imp.Find(ref aCollection, userName, mapName,testName);
        }

        public int Find(ref TestResultItemCollection aCollection, string userName, string mapName)
        {
            return m_imp.Find(ref aCollection, userName, mapName);
        }

        public int Find(ref TestResultItemCollection aCollection, string mapName)
        {
            return m_imp.Find(ref aCollection, mapName);
        }

        public TestResultItem CreateResultItem(string sStr)
        {
            return m_imp.CreateResultItem(sStr);
        }

        public void Save()
        {
            m_imp.Save();
        }

        public void FireEvent(EventArgs ea)
        {
            if (OnChangedEventHandler != null)
                OnChangedEventHandler(this, ea);
        }

    }
}
