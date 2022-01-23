using System;
using System.Collections;
using System.Collections.Generic;
using SoftObject.TrainConcept.Libraries;


namespace SoftObject.TrainConcept.Libraries
{
    /// <summary>
	/// Diese Klasse befüllt die TreeList (class ContentTreeView) mit
	/// den Inhalten aus der XML-Datenbank. 
	/// Möglichkeiten:
	/// FullContent-> Gesamter Inhalt der Bibliothek
	/// QuestionContent-> Fragen
	/// NoContent-> Keine Inhalte
	/// </summary>
	/// 
	public class ContentTreeViewList : ArrayList
	{
        public enum UseType { FullContent, QuestionContent, LearnmapContent, LearnmapWorkingsContent, NoContent };
		private UseType	m_tType=UseType.FullContent;
        private Dictionary<string, int> m_dLibraries = new Dictionary<string, int>();
        private Dictionary<string, int> m_dBooks = new Dictionary<string, int>();
        private Dictionary<string, int> m_dChapters = new Dictionary<string, int>();
        private Dictionary<string, int> m_dPoints = new Dictionary<string, int>();
        private LibraryManagerBridge m_libManager;
        private LearnmapManagerBridge m_mapManager;
        
		private string m_learnmapName=null;
		public string LearnmapName 
		{
			get { return m_learnmapName;}
			set { m_learnmapName = value;}
		}

		public ContentTreeViewList()
		{
		}

        public ContentTreeViewList(LibraryManagerBridge libManager, LearnmapManagerBridge mapManager, UseType tType)
		{
			m_tType = tType;
            m_libManager = libManager;
            m_mapManager = mapManager;
        }

        public ContentTreeViewList(LibraryManagerBridge libManager, LearnmapManagerBridge mapManager, UseType tType, string _learnmapName)
		{
			m_tType = tType;
            m_libManager = libManager;
            m_mapManager = mapManager;
            m_learnmapName = _learnmapName;
        }

		public void Fill()
		{
			if (m_tType == UseType.FullContent)
			{
				int startId=0;
				//Anzahl an verfügbaren Bibliotheken holen
				for(int i=0;i<m_libManager.GetLibraryCnt();++i)
					AddLibrary(m_libManager.GetLibrary(i),ref startId);
			}
			else if (m_tType == UseType.QuestionContent)
			{
				if (m_learnmapName!=null && m_learnmapName.Length>0)
					AddLearnmap(m_learnmapName);
				else
				{
					int startId=0;
					//Anzahl an verfügbaren Bibliotheken holen
					for(int i=0;i<m_libManager.GetLibraryCnt();++i)
						AddLibrary(m_libManager.GetLibrary(i),ref startId);
				}
			}
            else if (m_tType == UseType.LearnmapContent || m_tType == UseType.LearnmapWorkingsContent)
			{
				if (m_learnmapName!=null && m_learnmapName.Length>0)
					AddLearnmap(m_learnmapName);
				else
				{
					int startId=0;
					for(int i=0;i<m_mapManager.GetMapCnt();++i)
					{
						string title;
						m_mapManager.GetTitle(i,out title);
						AddLearnmap(title,ref startId);
					}
				}
			}
		}

		private void AddLibrary(LibraryItem lib,ref int _startId)
		{
			ContentTreeViewRecord rec;
					
			rec = new ContentTreeViewRecord(lib.title,ContentTreeViewRecordType.LibraryItem, _startId++);//Kein Parent weil Wurzelelement
			int libId = Add(rec);

			if (lib.Books!=null)
				for(int b=0;b<lib.Books.Length;++b)
				{
					BookItem book = lib.Books[b];
					rec = new ContentTreeViewRecord(book.title,ContentTreeViewRecordType.Book, _startId++,libId); 
					int bookId = this.Add(rec); //ist eine ArrayList
					if (book.Chapters!=null)
						for(int c=0;c<book.Chapters.Length;++c)
						{
							ChapterItem chp = book.Chapters[c];
							rec = new ContentTreeViewRecord(chp.title,ContentTreeViewRecordType.Chapter,_startId++,bookId); //ID der TreeList und Parent innerhalb der TreeList
							int chpId = this.Add(rec);
							if (chp.Points!=null)
								for(int p=0;p<chp.Points.Length;++p)
								{
									PointItem poi=chp.Points[p];
									rec = new ContentTreeViewRecord(poi.title,ContentTreeViewRecordType.Point,_startId++,chpId);
									int pId = this.Add(rec);
									if (m_tType == UseType.QuestionContent)
									{
										if (poi.Questions!=null)
											for(int q=0;q<poi.Questions.Length;++q)
											{
												QuestionItem que=poi.Questions[q];
												rec = new ContentTreeViewRecord(que.question,_startId++,pId,q);
												this.Add(rec);
											}
									}
								}
						}
					
				}
		}

		private void AddLearnmap(string title)
		{
			int startId=0;
			AddLearnmap(title,ref startId);
		}

		private void AddLearnmap(string title,ref int _startId)
		{
			string [] aWorkings=null;
		
			if(m_mapManager.GetWorkings(title,ref aWorkings))
			{
                bool bIsWorkingsContent = (m_tType == UseType.LearnmapWorkingsContent);
                int lastlibId = -1;
                int lastbookId = -1;
                int lastchpId = -1;
                int lastpoiId = -1;

                ContentTreeViewRecord rec;
				for(int i=0;i<aWorkings.Length;++i)
				{
					int libId=-1;
					int bookId=-1;
					int chpId=-1;
					int poiId=-1;
					string [] aTitles;
					Utilities.SplitPath(aWorkings[i],out aTitles);

                    if (m_dLibraries.ContainsKey(aTitles[0]))
                        libId = m_dLibraries[aTitles[0]];

                    if (libId<0 ||
                        (bIsWorkingsContent && ((libId >= 0 && lastlibId >= 0) && libId != lastlibId)))
                    {
                        rec = new ContentTreeViewRecord(aTitles[0], ContentTreeViewRecordType.LibraryItem, _startId++);
                        libId = Add(rec);
                        m_dLibraries[aTitles[0]] = libId;
                    }

					string bookPath = Utilities.MergePath(new string[] {aTitles[0],aTitles[1]});
                    if (m_dBooks.ContainsKey(bookPath))
						bookId = m_dBooks[bookPath];
                    if (bookId<0 ||
                        (bIsWorkingsContent && ((bookId >= 0 && lastbookId >= 0) && bookId != lastbookId)))
                    {
                        rec = new ContentTreeViewRecord(aTitles[1], ContentTreeViewRecordType.Book, _startId++, libId);
                        bookId = Add(rec);
                        m_dBooks[bookPath] = bookId;
                    }

					string chpPath = Utilities.MergePath(new string[] {aTitles[0],aTitles[1],aTitles[2]});
                    if (m_dChapters.ContainsKey(chpPath))
						chpId = m_dChapters[chpPath];

                    if (chpId<0 ||
                        (bIsWorkingsContent && ((chpId >= 0 && lastchpId >= 0) && chpId != lastchpId)))
                    {
						rec = new ContentTreeViewRecord(aTitles[2],ContentTreeViewRecordType.Chapter,_startId++,bookId); //ID der TreeList und Parent innerhalb der TreeList
						chpId = Add(rec);
						m_dChapters[chpPath]=chpId;
					}

					if (m_tType == UseType.QuestionContent)
					{
						string poiPath = aWorkings[i];
						if (m_dPoints.ContainsKey(poiPath))
							poiId = m_dPoints[poiPath];
                        else
						{
							rec = new ContentTreeViewRecord(aTitles[3],ContentTreeViewRecordType.Point,_startId++,chpId);
							poiId = Add(rec);
							m_dPoints[poiPath]=poiId;
						}

						PointItem poi = m_libManager.GetPoint(aWorkings[i]);
						if (poi.Questions!=null)
							for(int q=0;q<poi.Questions.Length;++q)
							{
								QuestionItem que=poi.Questions[q];
								rec = new ContentTreeViewRecord(que.question,_startId++,poiId,q);
								this.Add(rec);
							}
					}
					else
					{
						rec = new ContentTreeViewRecord(aTitles[3],ContentTreeViewRecordType.Point,_startId++,chpId);
						Add(rec);
					}

                    lastlibId = libId;
                    lastbookId = bookId;
                    lastchpId = chpId;
                    lastpoiId = poiId;
				}
			}
		}


		public int GetId(string path)
		{
			string [] aTitles;
			Utilities.SplitPath(path,out aTitles);

			IEnumerator iter = GetEnumerator();
			ContentTreeViewRecord lib=null;
			ContentTreeViewRecord book=null;
			ContentTreeViewRecord chp=null;
			ContentTreeViewRecord poi=null;

			while(iter.MoveNext())
			{
				ContentTreeViewRecord r = (ContentTreeViewRecord) iter.Current;
				if (r.GetType() == ContentTreeViewRecordType.LibraryItem && aTitles.Length>=1 && String.Compare(r.Title,aTitles[0])==0)
					lib = r;
				else if (r.GetType() == ContentTreeViewRecordType.Book && aTitles.Length>=2 && String.Compare(r.Title,aTitles[1])==0)
					book = r;
				else if (r.GetType() == ContentTreeViewRecordType.Chapter && aTitles.Length>=3 &&String.Compare(r.Title,aTitles[2])==0)
					chp = r;
				else if (r.GetType() == ContentTreeViewRecordType.Point && aTitles.Length>=4 && String.Compare(r.Title,aTitles[3])==0)
					poi = r;

				if (aTitles.Length==4)
				{
					if (lib!=null && book!=null && chp!=null && poi!=null)
					{
						if ((poi.ParentID == chp.ID) && (chp.ParentID == book.ID) && (book.ParentID == lib.ID))
							return poi.ID;
					}
				}
				else if (aTitles.Length==3)
				{
					if (lib!=null && book!=null && chp!=null)
					{
						if ((chp.ParentID == book.ID) && (book.ParentID == lib.ID))
							return chp.ID;
					}
				}
				else if (aTitles.Length==2)
				{
					if (lib!=null && book!=null)
					{
						if (book.ParentID == lib.ID)
							return book.ID;
					}
				}
				else if (aTitles.Length==1)
				{
					if (lib!=null)
						return lib.ID;
				}
			}

			return -1;
		}
	
		public string GetPath(int id)
		{
			ContentTreeViewRecord r = this[id] as ContentTreeViewRecord;

			if (r!=null)
			{
				if (r.GetType()==ContentTreeViewRecordType.Point ||
					r.GetType()==ContentTreeViewRecordType.Chapter ||
					r.GetType()==ContentTreeViewRecordType.Book)
				{
					string [] aPath= {GetPath(r.ParentID),r.Title};
					return Utilities.MergePath(aPath);
				}
				else
					return r.Title;
			}
			return null;
		}

        public int FindNextPointId(int id)
        { 
            if (id>=0)
                for (int i=id+1; i<this.Count; ++i)
                {
                    ContentTreeViewRecord r = this[i] as ContentTreeViewRecord;
                    if (r != null && r.GetType() == ContentTreeViewRecordType.Point)
                        return i;
                }
            return -1;
        }

        public int FindPrevPointId(int id)
        {
            if (id > 0)
                for (int i = id - 1; i >= 0; --i)
                {
                    ContentTreeViewRecord r = this[i] as ContentTreeViewRecord;
                    if (r != null && r.GetType() == ContentTreeViewRecordType.Point)
                        return i;
                }
            return -1;
        }

	}
}
