using System;
using System.Collections;

namespace SoftObject.TrainConcept.Libraries
{
    public class QuestionCollection : System.Collections.CollectionBase
    {
        private Hashtable m_dPaths = new Hashtable();
        private Hashtable m_dQuIds = new Hashtable();

        public void Add(QuestionItem question)
        {
            List.Add(question);
            m_dPaths.Add(question, "");
            m_dQuIds.Add(question, 0);
        }

        public void Add(QuestionItem question, string path, int quId)
        {
            List.Add(question);
            m_dPaths.Add(question, path);
            m_dQuIds.Add(question, quId);
        }

        public void Remove(int _id)
        {
            if (_id >= 0 && _id < Count)
                List.RemoveAt(_id);
        }

        public QuestionItem Item(int _id)
        {
            return (QuestionItem)List[_id];
        }

        public string GetPath(QuestionItem item)
        {
            if (m_dPaths[item] != null)
                return (string)m_dPaths[item];
            return null;
        }

        public int GetId(QuestionItem item)
        {
            if (m_dQuIds[item] != null)
                return (int)m_dQuIds[item];
            return -1;
        }
    }
}
