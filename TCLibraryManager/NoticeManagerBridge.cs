using System;

namespace SoftObject.TrainConcept.Libraries
{
    public class NoticeManagerBridge
    {
        public event OnNoticeManagerHandler NoticeManagerEventHandler;
        public event OnNoticeManagerHandler NoticeManagerEvent
        {
            add { NoticeManagerEventHandler += value; }
            remove { NoticeManagerEventHandler -= value; }
        }

        private INoticeManager m_imp;

        public NoticeManagerBridge(INoticeManager imp)
        {
            m_imp = imp;
            m_imp.SetParent(this);
        }

        public bool Open(string fileName,string noticePath)
        {
            return m_imp.Open(fileName,noticePath);
        }

        public int CreateNotice(string userName, string title, string contentPath, int pageId, bool bCanWorkout)
        {
            return m_imp.CreateNotice(userName, title, contentPath, pageId, bCanWorkout);
        }

        public NoticeItem GetNotice(int id)
        {
            return m_imp.GetNotice(id);
        }

        public void SetNoticeTitle(int id, string title)
        {
            m_imp.SetNoticeTitle(id, title);
        }

        public int Find(ref NoticeItemCollection aCollection, string userName, string contentPath, int pageId)
        {
            return m_imp.Find(ref aCollection, userName, contentPath, pageId);
        }

        public int Find(ref NoticeItemCollection aCollection, string userName)
        {
            return m_imp.Find(ref aCollection, userName);
        }

        public int Find(string fileName)
        {
            return m_imp.Find(fileName);
        }

        public int Find(string userName,string title)
        {
            return m_imp.Find(userName, title);
        }

        public void DeleteNotice(int id)
        {
            m_imp.DeleteNotice(id);
        }

        public void Save()
        {
            m_imp.Save();
        }

        public void SetAdapter(INoticeAdapter adapter)
        {
            m_imp.SetAdapter(adapter);
        }

        public string GetNoticePath()
        {
            return m_imp.GetNoticePath();
        }

        public void SetDirty(int nId, bool bIsDirty)
        {
            m_imp.SetDirty(nId, bIsDirty);
        }

        public void DeleteNotices(string userName)
        {
            m_imp.DeleteNotices(userName);
        }

        public void FireEvent(ref NoticeManagerEventArgs ea)
        {
            if (NoticeManagerEventHandler != null)
                NoticeManagerEventHandler(this, ref ea);
        }
    }
}
