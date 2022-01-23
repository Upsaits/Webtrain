using System;
using System.Collections;
using System.Collections.Generic;

namespace SoftObject.TrainConcept.Libraries
{
    public class NoticeItemCollection : List<NoticeItem>
    {
        public int Find(ref NoticeItemCollection aNotices, string userName, string contentPath, int pageId)
        {
            IEnumerator iter = GetEnumerator();
            while (iter.MoveNext())
            {
                NoticeItem item = (NoticeItem)iter.Current;
                if (userName.Length==0 || String.Compare(item.userName, userName) == 0)
                {
                    if (contentPath.Length==0 || String.Compare(item.contentPath, contentPath) == 0)
                    {
                        if (pageId >= 0)
                        {
                            if (item.pageId == pageId)
                                aNotices.Add(item);
                        }
                        else
                            aNotices.Add(item);
                    }
                }
            }
            return aNotices.Count;
        }

        public int Find(ref NoticeItemCollection aNotices, string userName)
        {
            IEnumerator iter = GetEnumerator();
            while (iter.MoveNext())
            {
                NoticeItem item = (NoticeItem)iter.Current;
                if (String.Compare(item.userName, userName) == 0)
                    aNotices.Add(item);
            }
            return aNotices.Count;
        }

        public int Find(string fileName)
        {
            IEnumerator iter = GetEnumerator();
            int i = 0;
            while (iter.MoveNext())
            {
                NoticeItem item = (NoticeItem)iter.Current;
                if (String.Compare(item.fileName, fileName) == 0)
                    return i;
                ++i;
            }
            return -1;
        }

        public int Find(string userName,string title)
        {
            IEnumerator iter = GetEnumerator();
            int i = 0;
            while (iter.MoveNext())
            {
                NoticeItem item = (NoticeItem)iter.Current;
                if (String.Compare(item.userName, userName) == 0 && String.Compare(item.title, title) == 0)
                    return i;
                ++i;
            }
            return -1;
        }

        public int Get(ref NoticeList aItems)
        {
            if (Count == 0)
                return 0;

            aItems.Notices = new NoticeItem[Count];
            int i = 0;

            IEnumerator iter = GetEnumerator();
            while (iter.MoveNext())
                aItems.Notices.SetValue(iter.Current, i++);
            return Count;
        }
    }
}
