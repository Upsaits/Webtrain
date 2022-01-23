using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections;

namespace SoftObject.TrainConcept.Libraries
{
		
	public class DefaultNoticeManager : INoticeManager
	{
		private NoticeItemCollection m_aNotices=new NoticeItemCollection();
		private string m_fileName;
        private string m_noticePath;
        private INoticeAdapter m_adapter=null;
        private NoticeManagerBridge m_parent = null;

        public DefaultNoticeManager()
        {
        }

        public void SetParent(object parent)
        {
            m_parent = (NoticeManagerBridge)parent;
        }

        public int CreateNotice(string userName, string title, string contentPath, int pageId, bool bCanWorkout)
		{
            string sTxt = m_adapter.GetText("FORMS", "Notice", "Notiz");
			string dirName = String.Format("{0}\\{1}\\notices",m_noticePath,userName);
			string fileName = String.Format("notice_{0}.rtf",title);
            string filePath = dirName+'\\'+fileName;

			if (!Directory.Exists(dirName))
				Directory.CreateDirectory(dirName);

			m_aNotices.Add(new NoticeItem(userName,contentPath,pageId,title,fileName,DateTime.Now,bCanWorkout ? 0 : -1,1));
            return m_aNotices.Count-1;
		}

		public NoticeItem GetNotice(int id)
		{
            if (id>=0 && id< m_aNotices.Count)
			    return m_aNotices[id];
            return null;
		}

		public void SetNoticeTitle(int id,string title)
		{
			NoticeItem item = GetNotice(id);

            string dirName = String.Format("{0}\\{1}\\notices\\", m_noticePath, item.userName);
            string fileName = String.Format("notice_{0}.rtf", title);
            string newFilePath = dirName + fileName;
            string oldFilePath = dirName + item.fileName;


            if (File.Exists(oldFilePath))
            {
                File.Copy(oldFilePath, newFilePath, true);
                File.Delete(oldFilePath);
            }

			item.title = title;
            item.fileName = fileName; 
		}

		public int Find(ref NoticeItemCollection aCollection,string userName,string contentPath,int pageId)
		{
			return m_aNotices.Find(ref aCollection,userName,contentPath,pageId);
		}

		public int Find(string fileName)
		{
			return m_aNotices.Find(fileName);
		}

        public int Find(string userName,string title)
        {
            return m_aNotices.Find(userName, title);
        }

        public int Find(ref NoticeItemCollection aNotices, string userName)
        {
            return m_aNotices.Find(ref aNotices,userName);
        }

		public void DeleteNotice(int id)
		{
			NoticeItem item = m_aNotices[id];
            if (item != null)
            {
                DestroyNotice(id);
                m_aNotices.RemoveAt(id);
            }
		}

		public bool Open(string fileName,string noticePath)
		{
			m_fileName = fileName;
            m_noticePath = noticePath;

			if (!File.Exists(fileName))
				return false;

			XmlSerializer serializer = new XmlSerializer(typeof(NoticeList));
			FileStream fs = new FileStream(m_fileName, FileMode.Open, FileAccess.Read);
			
			NoticeList nl;
			nl = (NoticeList) serializer.Deserialize(fs);

            bool bChanged = false;
			if (nl.Notices!=null)
                foreach (NoticeItem item in nl.Notices)
                {
                    if (Path.IsPathRooted(item.fileName))
                    {
                        item.fileName = Path.GetFileName(item.fileName);
                        bChanged = true;
                    }
                    m_aNotices.Add(item);
                }

			fs.Close();

            NoticeManagerEventArgs ea = new NoticeManagerEventArgs(NoticeManagerEventArgs.CommandType.Open);
            m_parent.FireEvent(ref ea);

            if (bChanged)
                Save();
			return true;
		}

		public void Save()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(NoticeList));
			TextWriter writer = new StreamWriter(m_fileName);

			NoticeList aNotices = new NoticeList();
			m_aNotices.Get(ref aNotices);
            //foreach (var n in m_aNotices)
            //    n.ModificationDate = DateTime.Now;
			serializer.Serialize(writer,aNotices);
			writer.Close();

            NoticeManagerEventArgs ea = new NoticeManagerEventArgs(NoticeManagerEventArgs.CommandType.Save);
            m_parent.FireEvent(ref ea);
		}

        public void SetAdapter(INoticeAdapter adapter)
        {
            m_adapter = adapter;
        }

        public string GetNoticePath()
        {
            return m_noticePath;
        }

        public void SetDirty(int id, bool bIsDirty)
        {
            var ni = GetNotice(id);
            ni.dirtyFlag = bIsDirty ? 1 : 0;
            Save();
        }

        public void DeleteNotices(string userName)
        {
            for (int i=m_aNotices.Count;i>0;--i)
            {
                if (m_aNotices[i - 1].userName == userName)
                {
                    DestroyNotice(i - 1);
                    m_aNotices.RemoveAt(i - 1);
                }
            }
        }

        public bool DestroyNotice(int iId)
        {
            NoticeItem ni = m_aNotices[iId];

            string dirName = String.Format("{0}\\{1}\\notices", m_noticePath, ni.userName);
            string filePath = dirName + '\\' + ni.fileName;

            try
            {
	            if (File.Exists(filePath))
	                File.Delete(filePath);
                return true;
            }
            catch (System.Exception ex)
            {
            	
            }
            return false;
        }

	}
}
