using System;

namespace SoftObject.TrainConcept.Libraries
{
    public delegate void OnNoticeManagerHandler(object sender, ref NoticeManagerEventArgs ea);

	/// <summary>
	/// Zusammendfassende Beschreibung für INoticeManager.
	/// </summary>
	public interface INoticeManager
	{
        void SetParent(object parent);
        bool Open(string fileName, string noticePath);
		int CreateNotice(string userName,string title,string contentPath,int pageId,bool bCanWorkout);
		NoticeItem GetNotice(int id);
		void DeleteNotice(int id);
        void DeleteNotices(string userName);
		int Find(ref NoticeItemCollection aCollection,string userName,string contentPath,int pageId);
        int Find(ref NoticeItemCollection aNotices, string userName);
		int Find(string fileName);
        int Find(string userName, string title);
        void SetNoticeTitle(int id, string title);
		void Save();
        void SetAdapter(INoticeAdapter adapter);
        string GetNoticePath();
        void SetDirty(int id, bool bIsDirty);
	}
}