using System;
using System.Collections;

namespace SoftObject.TrainConcept.Libraries
{
    public class KeywordCollection : System.Collections.CollectionBase
    {
        private Hashtable m_dPaths = new Hashtable();
        private Hashtable m_dPageIds = new Hashtable();

        public void Add(KeywordActionItem item, string path, int pageId)
        {
            if (item.text.Length > 0 && Find(item.text) == null)
            {
                List.Add(item);
                m_dPaths.Add(item, path);
                m_dPageIds.Add(item, pageId);
            }
        }

        public void Remove(int _id)
        {
            if (_id >= 0 && _id < Count)
                List.RemoveAt(_id);
        }

        public KeywordActionItem Item(int _id)
        {
            return (KeywordActionItem)List[_id];
        }

        public void Add(KeywordActionItem[] aItems)
        {
            for (int i = 0; i < aItems.Length; ++i)
                List.Add(aItems[i]);
        }

        public string GetPath(KeywordActionItem item)
        {
            if (m_dPaths[item] != null)
                return (string)m_dPaths[item];
            return null;
        }

        public int GetPageId(KeywordActionItem item)
        {
            if (m_dPageIds[item] != null)
                return (int)m_dPageIds[item];
            return -1;
        }

        public KeywordActionItem Find(string title)
        {
            IEnumerator iter = GetEnumerator();
            while (iter.MoveNext())
            {
                KeywordActionItem item = (KeywordActionItem)iter.Current;
                if (String.Compare(item.text, title) == 0)
                    return item;
            }
            return null;
        }
    }
}
