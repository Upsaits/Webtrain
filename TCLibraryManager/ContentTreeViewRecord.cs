using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftObject.TrainConcept.Libraries
{
    public enum ContentTreeViewRecordType { LibraryItem, Book, Chapter, Point, Subpoint, Question };
    public class ContentTreeViewRecord
    {
        private string m_title;
        private int m_id;
        private int m_parentId;
        private ContentTreeViewRecordType m_status;
        private int m_quId;

        public string Title
        {
            get { return m_title; }
            set { m_title = value; }
        }

        //Weil der Feldname der TreeView ImageIndex heisst. -> Siehe Wizzard.
        //Sonst würde eine Spalte angezeigt werden
        public int ImageIndex
        {
            get
            {
                switch (m_status)
                {
                    case ContentTreeViewRecordType.LibraryItem: return 0;
                    case ContentTreeViewRecordType.Book: return 1;
                    case ContentTreeViewRecordType.Chapter: return 3;
                    case ContentTreeViewRecordType.Point:
                    case ContentTreeViewRecordType.Subpoint: return 5;
                    case ContentTreeViewRecordType.Question: return 6;
                    default: return 1;
                }
            }
        }

        public int ID
        {
            get { return m_id; }
        }

        public int ParentID
        {
            get { return m_parentId; }
        }

        public ContentTreeViewRecord(string title, ContentTreeViewRecordType status, int id)
        {
            m_title = title;
            m_id = id;
            m_parentId = -1;
            m_status = status;
        }

        public ContentTreeViewRecord(string title, ContentTreeViewRecordType status, int id, int parentId)
        {
            m_title = title;
            m_id = id;
            m_parentId = parentId;
            m_status = status;
        }

        public ContentTreeViewRecord(string title, int id, int parentId, int quId)
        {
            m_title = title;
            m_id = id;
            m_parentId = parentId;
            m_status = ContentTreeViewRecordType.Question;
            m_quId = quId;
        }

        public new ContentTreeViewRecordType GetType()
        {
            return m_status;
        }

        public int GetQuestionId()
        {
            return m_quId;
        }
    }
}
