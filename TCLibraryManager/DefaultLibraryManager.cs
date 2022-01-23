using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace SoftObject.TrainConcept.Libraries
{

	public class DefaultLibraryManager : ILibraryManager
	{
		private readonly SortedList m_dLibraries = new SortedList();
		private readonly SortedList m_dFiles = new SortedList();
        private readonly SortedList<string, Dictionary<string, string>> m_dDocuments = new SortedList<string, Dictionary<string, string>>();
        private string m_libsFolder;
        private LibraryManagerBridge m_parent = null;

		/// <summary>
		/// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
		/// </summary>
		/// <param name="characters">Unicode Byte Array to be converted to String</param>
		/// <returns>String converted from Unicode Byte Array</returns>
		private static String UTF8ByteArrayToString ( Byte[] characters )
		{
			UTF8Encoding encoding = new UTF8Encoding();
			String constructedString = encoding.GetString(characters);
			return (constructedString);
		}
 

		/// <summary>
		/// Converts the String to UTF8 Byte array and is used in De serialization
		/// </summary>
		/// <param name="pXmlString"></param>
		/// <returns></returns>
		private static Byte[] StringToUTF8ByteArray(String pXmlString)

		{
			UTF8Encoding encoding = new UTF8Encoding();
			Byte[] byteArray = encoding.GetBytes(pXmlString);
			return byteArray;
		}

        public void SetParent(object parent)
        {
            m_parent = (LibraryManagerBridge)parent;
        }

        public bool Open(string libsFolder,string fileName)
        {
            m_libsFolder = libsFolder;
            if (m_dFiles.ContainsValue(fileName))
                return false;
            try
            {
                string libTitle;
                XmlSerializer serializer = new XmlSerializer(typeof(LibraryItem));
                using (StreamReader reader = new StreamReader(fileName, System.Text.Encoding.UTF8))
                {
                    LibraryItem lib = (LibraryItem)serializer.Deserialize(reader);
                    reader.Close();
                    lib.IsModified = false;
                    m_dLibraries[lib.title] = lib;
                    m_dFiles[lib.title] = fileName;
                    libTitle = lib.title;
                }

                LibraryManagerEventArgs ea = new LibraryManagerEventArgs(LibraryManagerEventArgs.CommandType.Open, libTitle);
                m_parent.FireEvent(ref ea);

                return true;
            }
            catch (Exception e)
            {
                string txt = e.Message;
                return false;
            }
        }

        public bool OpenFromImport(string libsFolder, string fileName)
        {
            m_libsFolder = libsFolder;
			try
            {
                string libTitle;
                XmlSerializer serializer = new XmlSerializer(typeof(LibraryItem));
                using (StreamReader reader = new StreamReader(fileName, System.Text.Encoding.UTF8))
                {
                    var libNew = (LibraryItem)serializer.Deserialize(reader);
                    reader.Close();

                    libTitle = libNew.title;
					var libOld = GetLibrary(libNew.title);
                    if (libOld != null)
                    {
                        var vOld = new Version();
                        var vNew = new Version();
                        string strError = "";

                        string oldFilename = (string) m_dFiles[libOld.title];
                        Close(libOld.title);
						
                        if (Utilities.ParseVersion(libOld.version, ref vOld, ref strError) &&
                            Utilities.ParseVersion(libNew.version, ref vNew, ref strError))
                        {
							// New library more recent that older one
                            if (vNew > vOld)
                            {
                                File.Copy(fileName, oldFilename,true);
                                Open(m_libsFolder, oldFilename);
                                LibraryManagerEventArgs ea = new LibraryManagerEventArgs(LibraryManagerEventArgs.CommandType.Open, libTitle);
                                m_parent.FireEvent(ref ea);
								return true;
                            }
						}
					}
                    else
                    {
                        string newFileName = m_libsFolder + "\\" + Path.GetFileNameWithoutExtension(fileName) + ".xml";
		                File.Copy(fileName, newFileName, true);
                        Open(m_libsFolder, newFileName);
                        LibraryManagerEventArgs ea = new LibraryManagerEventArgs(LibraryManagerEventArgs.CommandType.Open, libTitle);
                        m_parent.FireEvent(ref ea);
						return true;
                    }
				}
				
                return false;
            }
            catch (Exception e)
            {
                string txt = e.Message;
                return false;
            }
        }
		public  bool Create(string filename,string title)
		{
			if (m_dLibraries[title]==null)
			{
                LibraryItem lib = new LibraryItem(title,/*_useBinaryMask=*/false,/*_isLocal=*/true);
				CreateDefault(lib);
				m_dLibraries[title]=lib;
				m_dFiles[title]=filename;
				lib.IsModified=true;
                
                LibraryManagerEventArgs ea = new LibraryManagerEventArgs(LibraryManagerEventArgs.CommandType.Create, lib.title);
                m_parent.FireEvent(ref ea);
                
				return true;
			}
			return false;
		}

		public  void Close(int libId)
		{
			try
			{
				if (libId<0 || libId>=m_dLibraries.Count)
					return;
				LibraryItem lib=(LibraryItem) m_dLibraries.GetByIndex(libId);
				string filename=(string) m_dFiles.GetByIndex(libId);
                string strError="";

                if (lib.IsModified)
				{
                    Version v=new Version();
                    if (Utilities.ParseVersion(lib.version, ref v, ref strError))
                    {
                        v = v.IncrementRevision();
                        lib.version = v.ToString();
                    }

					XmlSerializer serializer = new XmlSerializer(typeof(LibraryItem));
					TextWriter writer = new StreamWriter(filename);

					serializer.Serialize(writer, lib);
					writer.Close();

                    LibraryManagerEventArgs ea = new LibraryManagerEventArgs(LibraryManagerEventArgs.CommandType.Close, lib.title);
                    m_parent.FireEvent(ref ea);
				}

				m_dLibraries.RemoveAt(libId);
				m_dFiles.RemoveAt(libId);
			}
			catch (System.Exception /*e*/)
			{
			}
		}

		public  void Close(string libTitle)
		{
			int id = m_dLibraries.IndexOfKey(libTitle);
			if (id>=0)
				Close(id);
		}

        public void CloseAll()
        {
            while (m_dLibraries.Count > 0)
                Close(0);
        }

		public bool Reload(string path)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				string filePath=GetFilePath(aTitles[0]);
				if (filePath!=null)
				{
					Close(aTitles[0]);
					return Open(m_libsFolder,filePath);
				}
			}
			return false;
		}


		public void SetModified(string path)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
					lib.IsModified = true;
			}
		}
		
		public  bool GetPageCnt(string path,out int pageCnt)
		{
            pageCnt = 0;
            string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
                if (aTitles.Length == 4)
                {
                    PointItem poi = GetPoint(path);
                    if (poi != null && poi.Pages!=null)
                    {
                        pageCnt=poi.Pages.Length;
                    }
                    return true;
                }
                else if (aTitles.Length == 3)
                {
                    ChapterItem chp = GetChapter(path);
                    if (chp != null && chp.Points!=null)
                    {
                        foreach (var poi in chp.Points)
                            if (poi != null && poi.Pages!=null)
                                pageCnt += poi.Pages.Length;
                    }
                    return true;
                }
                else if (aTitles.Length == 2)
                {
                    BookItem book = GetBook(path);
                    if (book != null && book.Chapters!=null)
                    {
                        if (book.Chapters != null)
                            foreach (var chp in book.Chapters)
                            {
                                if (chp.Points!=null)
                                    foreach (var poi in chp.Points)
                                        if (poi != null && poi.Pages != null)
                                            pageCnt += poi.Pages.Length;
                            }
                        return true;
                    }
                }
                else if (aTitles.Length == 1)
                {
                    LibraryItem lib = GetLibrary(path);
                    if (lib != null && lib.Books!=null)
                        foreach (var book in lib.Books)
                        {
                            if (book.Chapters!=null)
                                foreach (var chp in book.Chapters)
                                {
                                    if (chp.Points != null)
                                        foreach (var poi in chp.Points)
                                            if (poi != null && poi.Pages != null)
                                                pageCnt += poi.Pages.Length;
                                }
                            return true;
                        }
                }
            }
			return false;
		}

		public  int GetPageCnt(int libId)
		{
			int pages=0;
			LibraryItem lib=GetLibrary(libId);
			if (lib!=null && lib.Books!=null)
				for(int b=0;b<lib.Books.Length;++b)
				{
					BookItem book=lib.Books[b];
                    if (book.Chapters!=null)
					    for(int c=0;c<book.Chapters.Length;++c)
					    {
						    ChapterItem chp=book.Chapters[c];
                            if (chp.Points!=null)
						        for(int p=0;p<chp.Points.Length;++p)
						        {
							        PointItem poi=chp.Points[p];
							        if (poi.Pages!=null)
								        pages+=poi.Pages.Length;
						        }
					    }
				}
			return pages;
		}

		public int GetPageCnt()
		{
			int pages=0;
            for (int i = 0; i < GetLibraryCnt(); ++i)
                pages += GetPageCnt(i);
			return pages;
		}
        public  PageItem GetPage(string path,int pageId)
		{
			PointItem poi = GetPoint(path);
            if (poi != null && poi.Pages!=null)
			{
				if (pageId>=0 && pageId<poi.Pages.Length)
					return poi.Pages[pageId];
			}
			return null;
		}

        public bool SetPage(string path, PageItem item, int pageId)
        {
            PointItem poi = GetPoint(path);
            if (poi != null)
            {
                if (pageId >= 0 && pageId < poi.Pages.Length)
                {
                    poi.Pages[pageId] = item;
                    SetModified(path);
                    return true;
                }
            }
            return false;
        }

        public bool AddPage(string path, PageItem pageItem)
        {
            string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles))
            {
                LibraryItem lib = (LibraryItem)m_dLibraries[aTitles[0]];
                if (lib != null)
                {
                    BookItem book = FindBook(lib, aTitles[1]);
                    if (book != null)
                    {
                        ChapterItem chp = FindChapter(book, aTitles[2]);
                        if (chp != null)
                        {
                            PointItem poi = FindPoint(chp, aTitles[3]);
                            if (poi != null)
                            {
                                if (poi.Pages == null)
                                {
                                    poi.Pages = new PageItem[1];
                                    poi.Pages[0] = pageItem;
                                }
                                else
                                {
                                    ArrayList aEntries = new ArrayList(poi.Pages);
                                    aEntries.Insert(poi.Pages.Length, pageItem);
                                    poi.Pages = new PageItem[aEntries.Count];
                                    aEntries.CopyTo(poi.Pages);
                                }
                                lib.IsModified = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool DeletePage(string path, int iPageId)
        {
            string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles))
            {
                LibraryItem lib = (LibraryItem)m_dLibraries[aTitles[0]];
                if (lib != null)
                {
                    BookItem book = FindBook(lib, aTitles[1]);
                    if (book != null)
                    {
                        ChapterItem chp = FindChapter(book, aTitles[2]);
                        if (chp != null)
                        {
                            PointItem poi = FindPoint(chp, aTitles[3]);
                            if (poi != null)
                            {
                                ArrayList aEntries = new ArrayList(poi.Pages);
                                aEntries.RemoveAt(iPageId);
                                poi.Pages = new PageItem[aEntries.Count];
                                aEntries.CopyTo(poi.Pages);
                                lib.IsModified = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool MovePage(string path, int iPageId, int iDelta)
        {
            string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles))
            {
                LibraryItem lib = (LibraryItem)m_dLibraries[aTitles[0]];
                if (lib != null)
                {
                    BookItem book = FindBook(lib, aTitles[1]);
                    if (book != null)
                    {
                        ChapterItem chp = FindChapter(book, aTitles[2]);
                        if (chp != null)
                        {
                            PointItem poi = FindPoint(chp, aTitles[3]);
                            if (poi != null)
                            {
                                //var l = new List<PageItem>(poi.Pages);
                                Utilities.ShiftElement<PageItem>(poi.Pages, iPageId, iPageId + iDelta);
                                lib.IsModified = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        
        public void AddPageAction(PageItem page, ActionItem item)
        {
            if (page.PageActions == null)
            {
                page.PageActions = new ActionItem[1];
                page.PageActions[0] = item;
            }
            else
            {
                ArrayList aEntries = new ArrayList(page.PageActions);
                aEntries.Insert(page.PageActions.Length, item);
                page.PageActions = new ActionItem[aEntries.Count];
                aEntries.CopyTo(page.PageActions);
            }
        }

        public bool DeletePageAction(PageItem page, int iActionId)
        {
            if (page.PageActions!=null)
            {
                ArrayList aEntries = new ArrayList(page.PageActions);
                aEntries.RemoveAt(iActionId);
                if (aEntries.Count > 0)
                {
                    page.PageActions = new ActionItem[aEntries.Count];
                    aEntries.CopyTo(page.PageActions);
                }
                else
                    page.PageActions = null;

                return true;
            }
            return false;
        }

		public bool DeletePageAction(PageItem page, string actId)
		{
			for (int i = 0; i < page.PageActions.Length; ++i)
				if (page.PageActions[i].id == actId)
					return DeletePageAction(page,i);
			return false;
		}

		public ActionItem GetPageAction(PageItem page,string actId)
		{
			for(int i=0;i<page.PageActions.Length;++i)
				if (page.PageActions[i].id == actId)
					return page.PageActions[i];
			return null;
		}

		public ActionItem GetPageAction(PageItem page,int actId)
		{
			if (page!=null && page.PageActions!=null && page.PageActions.Length>0 && 
				actId>=0 && actId<page.PageActions.Length)
				return page.PageActions[actId];
			return null;
		}

		public bool SetPageAction(PageItem page,ActionItem action,int actionId)
		{
			if (page!=null && page.PageActions!=null && page.PageActions.Length>0 && actionId<page.PageActions.Length)
			{
				page.PageActions[actionId] = action;
				return true;
			}
			return false;
		}

        public bool GetQuestionCnt(string pointPath, out int questionCnt,bool onlyForTests=false)
		{
			questionCnt=0;
            PointItem poi = GetPoint(pointPath);
			if (poi!=null)
			{
                if (poi.Questions != null)
                {
                    if (onlyForTests)
                    {
                        foreach (var q in poi.Questions)
                            if (q.useForTesting)
                                ++questionCnt;
                    }
                    else
                        questionCnt = poi.Questions.Length;

                }
				return true;
			}
			return false;
		}
	
		public  QuestionItem GetQuestion(string path,int questionId)
		{
			PointItem poi = GetPoint(path);
            if (poi != null && poi.Questions!=null)
			{
				if (questionId>=0 && questionId<poi.Questions.Length)
					return poi.Questions[questionId];
			}
			return null;
		}

		public bool SetQuestion(string path,QuestionItem question,int questionId)
		{
            string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles))
            {
                LibraryItem lib = (LibraryItem)m_dLibraries[aTitles[0]];
                if (lib != null)
                {
                    PointItem poi = GetPoint(path);
                    if (poi != null)
                    {
                        if (questionId >= 0 && questionId < poi.Questions.Length)
                        {
                            poi.Questions[questionId] = question;
                            lib.IsModified = true;
                            return true;
                        }
                    }
                }
            }
			return false;
		}

        public bool AddQuestion(string path, QuestionItem question)
        {
            string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles))
            {
                LibraryItem lib = (LibraryItem)m_dLibraries[aTitles[0]];
                if (lib != null)
                {
                    BookItem book = FindBook(lib, aTitles[1]);
                    if (book != null)
                    {
                        ChapterItem chp = FindChapter(book, aTitles[2]);
                        if (chp != null)
                        {
                            PointItem poi = FindPoint(chp, aTitles[3]);
                            if (poi!=null)
                            {
                                if (poi.Questions == null)
                                {
                                    poi.Questions = new QuestionItem[1];
                                    poi.Questions[0] = question;
                                }
                                else
                                {
                                    ArrayList aEntries = new ArrayList(poi.Questions);
                                    var newQu = question.Clone();
                                    aEntries.Insert(poi.Questions.Length,newQu);
                                    poi.Questions = new QuestionItem[aEntries.Count];
                                    aEntries.CopyTo(poi.Questions);
                                }
                                lib.IsModified = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool DeleteQuestion(string path,int iQuestionId)
        {
            string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles))
            {
                LibraryItem lib = (LibraryItem)m_dLibraries[aTitles[0]];
                if (lib != null)
                {
                    BookItem book = FindBook(lib, aTitles[1]);
                    if (book != null)
                    {
                        ChapterItem chp = FindChapter(book, aTitles[2]);
                        if (chp != null)
                        {
                            PointItem poi = FindPoint(chp, aTitles[3]);
                            if (poi!=null)
                            {
                                ArrayList aEntries = new ArrayList(poi.Questions);
                                aEntries.RemoveAt(iQuestionId);
                                poi.Questions = new QuestionItem[aEntries.Count];
                                aEntries.CopyTo(poi.Questions);
                                lib.IsModified = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }


		public  int GetQuestions(string path,ref QuestionCollection aQuestions,bool useExaming,bool useTesting)
		{
			string[] aTitles;
			Utilities.SplitPath(path,out aTitles);

			if (aTitles.Length == 1)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					for(int b=0;b<lib.Books.Length;++b)
					{
						BookItem book=lib.Books[b];
						for(int c=0;c<book.Chapters.Length;++c)
						{
							ChapterItem chp=book.Chapters[c];
							for(int p=0;p<chp.Points.Length;++p)
							{
								PointItem poi=chp.Points[p];
								if (poi!=null && poi.Questions!=null)
									for(int q=0;q<poi.Questions.Length;++q)
									{
										if (useExaming && !useTesting)
										{
											if (poi.Questions[q].useForExaming == true)
											{
												string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
                                                aQuestions.Add(poi.Questions[q], Utilities.MergePath(l_aTitles), q);
											}
										}
										else if (!useExaming && useTesting)
										{
											if (poi.Questions[q].useForTesting == true)
											{
												string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
												aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
											}
										}
										else if (useExaming && useTesting)
										{
											if ((poi.Questions[q].useForExaming == true) ||
												(poi.Questions[q].useForTesting == true))
											{
												string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
												aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
											}
										}
									}
							}
						}
					}
				}
			}
			else if (aTitles.Length == 2)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						for(int c=0;c<book.Chapters.Length;++c)
						{
							ChapterItem chp=book.Chapters[c];
							for(int p=0;p<chp.Points.Length;++p)
							{
								PointItem poi=chp.Points[p];
								if (poi!=null && poi.Questions!=null)
									for(int q=0;q<poi.Questions.Length;++q)
									{
										if (useExaming && !useTesting)
										{
											if (poi.Questions[q].useForExaming == true)
											{
												string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
												aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
											}
										}
										else if (!useExaming && useTesting)
										{
											if (poi.Questions[q].useForTesting == true)
											{
												string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
												aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
											}
										}
										else if (useExaming && useTesting)
										{
											if ((poi.Questions[q].useForExaming == true) ||
												(poi.Questions[q].useForTesting == true))
											{
												string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
												aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
											}
										}
									}
							}
						}
					}
				}
			}
			else if (aTitles.Length == 3)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						ChapterItem chp=FindChapter(book,aTitles[2]);
						if (chp!=null)
						{
							for(int p=0;p<chp.Points.Length;++p)
							{
								PointItem poi=chp.Points[p];
								if (poi!=null && poi.Questions!=null)
									for(int q=0;q<poi.Questions.Length;++q)
									{
										if (useExaming && !useTesting)
										{
											if (poi.Questions[q].useForExaming == true)
											{
												string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
												aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
											}
										}
										else if (!useExaming && useTesting)
										{
											if (poi.Questions[q].useForTesting == true)
											{
												string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
												aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
											}
										}
										else if (useExaming && useTesting)
										{
											if ((poi.Questions[q].useForExaming == true) ||
												(poi.Questions[q].useForTesting == true))
											{
												string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
												aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
											}
										}
									}
							}
						}
					}
				}
			}
			else if (aTitles.Length	==4)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						ChapterItem chp=FindChapter(book,aTitles[2]);
						if (chp!=null)
						{
							PointItem poi=FindPoint(chp,aTitles[3]);
							if (poi!=null && poi.Questions!=null)
								for(int q=0;q<poi.Questions.Length;++q)
								{
									if (useExaming && !useTesting)
									{
										if (poi.Questions[q].useForExaming == true)
										{
											string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
											aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
										}
									}
									else if (!useExaming && useTesting)
									{
										if (poi.Questions[q].useForTesting == true)
										{
											string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
											aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
										}
									}
									else if (useExaming && useTesting)
									{
										if ((poi.Questions[q].useForExaming == true) ||
											(poi.Questions[q].useForTesting == true))
										{
											string [] l_aTitles={aTitles[0],book.title,chp.title,poi.title};
											aQuestions.Add(poi.Questions[q],Utilities.MergePath(l_aTitles),q);
										}
									}
								}
						}
					}
				}
			}
			
			return aQuestions.Count;
		}


        public void AddQuestionAction(QuestionItem question, ActionItem item)
        {
            if (question.QuestionActions == null)
            {
                question.QuestionActions = new ActionItem[1];
                question.QuestionActions[0] = item;
            }
            else
            {
                ArrayList aEntries = new ArrayList(question.QuestionActions);
                aEntries.Insert(question.QuestionActions.Length, item);
                question.QuestionActions = new ActionItem[aEntries.Count];
                aEntries.CopyTo(question.QuestionActions);
            }
        }

        public bool DeleteQuestionAction(QuestionItem question, int iActionId)
        {
            if (question.QuestionActions != null)
            {
                ArrayList aEntries = new ArrayList(question.QuestionActions);
                aEntries.RemoveAt(iActionId);
                if (aEntries.Count > 0)
                {
                    question.QuestionActions = new ActionItem[aEntries.Count];
                    aEntries.CopyTo(question.QuestionActions);
                }
                else
                    question.QuestionActions = null;
                return true;

            }
            return false;
        }

        public ActionItem GetQuestionAction(QuestionItem question, string actId)
        {
            for (int i = 0; i < question.QuestionActions.Length; ++i)
                if (question.QuestionActions[i].id == actId)
                    return question.QuestionActions[i];
            return null;
        }

        public ActionItem GetQuestionAction(QuestionItem question, int actId)
        {
            if (question != null && question.QuestionActions != null && question.QuestionActions.Length > 0 &&
                actId >= 0 && actId < question.QuestionActions.Length)
                return question.QuestionActions[actId];
            return null;
        }

        public bool SetQuestionAction(QuestionItem question, ActionItem action, int actionId)
        {
            if (question != null && question.QuestionActions != null && question.QuestionActions.Length > 0 && actionId < question.QuestionActions.Length)
            {
                question.QuestionActions[actionId] = action;
                return true;
            }
            return false;
        }

		public  int GetKeywords(string path,ref KeywordCollection aKeywords)
		{
			string[] aTitles;
			Utilities.SplitPath(path,out aTitles);

			if (aTitles.Length == 1)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					for(int b=0;b<lib.Books.Length;++b)
					{
						BookItem book=lib.Books[b];
						for(int c=0;c<book.Chapters.Length;++c)
						{
							ChapterItem chp=book.Chapters[c];
							for(int p=0;p<chp.Points.Length;++p)
							{
								PointItem poi=chp.Points[p];
								if (poi!=null)
								{
									for(int pg=0;pg<poi.Pages.Length;++pg)
									{
										PageItem page = poi.Pages[pg];
										for(int act=0;act<page.PageActions.Length;++act)
											if (page.PageActions[act] is KeywordActionItem)
											{
												string [] aPath= {lib.title,book.title,chp.title,poi.title};
												KeywordActionItem item = (KeywordActionItem) page.PageActions[act];
												aKeywords.Add(item,Utilities.MergePath(aPath),pg);
											}
									}
								}
							}
						}
					}
				}
			}
			else if (aTitles.Length == 2)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						for(int c=0;c<book.Chapters.Length;++c)
						{
							ChapterItem chp=book.Chapters[c];
							for(int p=0;p<chp.Points.Length;++p)
							{
								PointItem poi=chp.Points[p];
								if (poi!=null)
								{
									for(int pg=0;pg<poi.Pages.Length;++pg)
									{
										PageItem page = poi.Pages[pg];
										for(int act=0;act<page.PageActions.Length;++act)
											if (page.PageActions[act] is KeywordActionItem)
											{
												string [] aPath= {lib.title,book.title,chp.title,poi.title};
												aKeywords.Add((KeywordActionItem)page.PageActions[act],Utilities.MergePath(aPath),pg);
											}
									}
								}
							}
						}
					}
				}
			}
			else if (aTitles.Length == 3)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						ChapterItem chp=FindChapter(book,aTitles[2]);
						if (chp!=null)
						{
							for(int p=0;p<chp.Points.Length;++p)
							{
								PointItem poi=chp.Points[p];
								if (poi!=null)
								{
									for(int pg=0;pg<poi.Pages.Length;++pg)
									{
										PageItem page = poi.Pages[pg];
										for(int act=0;act<page.PageActions.Length;++act)
											if (page.PageActions[act] is KeywordActionItem)
											{
												string [] aPath= {lib.title,book.title,chp.title,poi.title};
												aKeywords.Add((KeywordActionItem)page.PageActions[act],Utilities.MergePath(aPath),pg);
											}
									}
								}
							}
						}
					}
				}
			}
			else if (aTitles.Length	==4)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						ChapterItem chp=FindChapter(book,aTitles[2]);
						if (chp!=null)
						{
							PointItem poi=FindPoint(chp,aTitles[3]);
							if (poi!=null && poi.Pages!=null)
							{
								for(int pg=0;pg<poi.Pages.Length;++pg)
								{
									PageItem page = poi.Pages[pg];
									for(int act=0;act<page.PageActions.Length;++act)
										if (page.PageActions[act] is KeywordActionItem)
										{
											string [] aPath= {lib.title,book.title,chp.title,poi.title};
											aKeywords.Add((KeywordActionItem)page.PageActions[act],Utilities.MergePath(aPath),pg);
										}
								}
							}
						}
					}
				}
			}
			
			return aKeywords.Count;
		}

        public void AddKeyword(string path, int pageId, KeywordActionItem keywordAction)
        {
            string[] aTitles;
            Utilities.SplitPath(path, out aTitles);
            if (aTitles.Length == 4)
            {
                LibraryItem lib = (LibraryItem)m_dLibraries[aTitles[0]];
                if (lib != null)
                {
                    BookItem book = FindBook(lib, aTitles[1]);
                    if (book != null)
                    {
                        ChapterItem chp = FindChapter(book, aTitles[2]);
                        if (chp != null)
                        {
                            PointItem poi = FindPoint(chp, aTitles[3]);
                            if (poi != null && poi.Pages != null)
                            {
                                PageItem pg = poi.Pages[pageId];
                                if (pg != null)
                                    AddPageAction(pg, keywordAction);
                            }
                        }
                    }
                }
            }

        }


		public int GetLibraryCnt()
		{
			return m_dLibraries.Count;
		}

		public LibraryItem GetLibrary(int libId)
		{
			return (LibraryItem) m_dLibraries.GetByIndex(libId);
		}

		public LibraryItem GetLibrary(string title)
		{
			return (LibraryItem) m_dLibraries[title];
		}

		public bool SetLibrary(string path,LibraryItem item)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					lib=item;
					lib.IsModified=true;
					m_dLibraries[lib.title]=lib;
					m_dFiles[lib.title]=m_dFiles[aTitles[0]];
					int id = m_dLibraries.IndexOfKey(aTitles[0]);
					m_dLibraries.RemoveAt(id);
					m_dFiles.RemoveAt(id);
					return true;
				}
			}
			return false;
		}

        public bool SetLibraryTitle(string path, string title)
        {
            if (GetLibrary(title) != null) // check if library already exists
                return false;
        
            var lib = GetLibrary(path);
            if (lib != null)
            {
                m_dFiles[title] = m_dFiles[lib.title];
                m_dFiles.Remove(lib.title);
                m_dLibraries.Remove(lib.title);
                lib.title = title;
                lib.IsModified = true;
                m_dLibraries[lib.title] = lib;
                return true;
            }
            return false;
        }
        
        public bool AddLibrary(string title,bool prev)
		{
			string fileName=m_libsFolder + "\\" + title.ToLower() +".xml";
			return Create(fileName,title);
		}

		public bool DeleteLibrary(string title)
		{
			if (m_dLibraries[title]!=null)
			{
				int id = m_dLibraries.IndexOfKey(title);
				m_dLibraries.RemoveAt(id);
				File.Delete((string)m_dFiles[title]);
				m_dFiles.RemoveAt(id);
				return true;
			}
			return false;
		}

		public string GetFilePath(string title)
		{
			return (string) m_dFiles[title];
		}

		public string GetFileName(string path)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				string filePath=GetFilePath(aTitles[0]);
				if (filePath!=null)
					return Path.GetFileNameWithoutExtension(filePath);
			}
			return null;
		}

		private void CreateDefault(LibraryItem lib)
		{
			lib.Books = new BookItem[1];
			lib.Books[0] = new BookItem("Book1",true);
			lib.Books[0].Chapters = new ChapterItem[1];
			lib.Books[0].Chapters[0] = new ChapterItem("Chapter1",true);
			lib.Books[0].Chapters[0].Points = new PointItem[1];
			lib.Books[0].Chapters[0].Points[0] = new PointItem("Punkt1",true);
			lib.Books[0].Chapters[0].Points[0].Pages = new PageItem[1];
			lib.Books[0].Chapters[0].Points[0].Questions = new QuestionItem[1];
			lib.Books[0].Chapters[0].Points[0].Pages[0]=new PageItem("t_f.htm",true);
			lib.Books[0].Chapters[0].Points[0].Pages[0].PageActions = new ActionItem[3];
			lib.Books[0].Chapters[0].Points[0].Pages[0].PageActions[0]=new TextActionItem("hdln","HeadLine");
			lib.Books[0].Chapters[0].Points[0].Pages[0].PageActions[1]=new TextActionItem("subhdln","Sub-Headline");
			lib.Books[0].Chapters[0].Points[0].Pages[0].PageActions[2]=new TextActionItem("text1","Text1");

			lib.Books[0].Chapters[0].Points[0].Questions[0]=new QuestionItem("qtempl_A_frm.htm","MultipleChoice","Frage",1,true,true,true);
			lib.Books[0].Chapters[0].Points[0].Questions[0].QuestionActions = new ActionItem[1];
			lib.Books[0].Chapters[0].Points[0].Questions[0].Answers = new String[] {"Antwort1","Antwort2","Antwort3","Antwort4"};
            lib.Books[0].Chapters[0].Points[0].Questions[0].QuestionActions[0] = new ImageActionItem("quimg1", "question.gif", ImageActionItem.DefaultImgPosX, ImageActionItem.DefaultImgPosY, 
                                                                                                     ImageActionItem.DefaultImgWidth, ImageActionItem.DefaultImgHeight, 
                                                                                                     ImageActionItem.DefaultImgWidth, ImageActionItem.DefaultImgHeight, true);

			/*
			lib.Books = new BookItem[2];
			for(int b=0;b<2;++b)
			{
				BookItem book = new BookItem(String.Format("Buch{0}",b));
				book.Chapters = new ChapterItem[2];
				for(int c=0;c<2;++c)
				{
					ChapterItem chp = new ChapterItem(String.Format("Kapitel{0}",c));
					chp.Points = new PointItem[2];
					for(int p=0;p<2;++p)
					{
						PointItem poi= new PointItem(String.Format("Punkt{0}",p));
						poi.Pages = new PageItem[10];
						poi.Questions = new QuestionItem[10];
						for(int pg=0;pg<10;++pg)
						{
							poi.Pages[pg]=new PageItem("so_tmpl1_frm.htm");
							poi.Pages[pg].PageActions = new ActionItem[6];
							poi.Pages[pg].PageActions[0]=new TextActionItem("textTitle","Text-Titel");
							poi.Pages[pg].PageActions[1]=new TextActionItem("text1","Text1");
							poi.Pages[pg].PageActions[2]=new TextActionItem("text2","Text2");
							poi.Pages[pg].PageActions[3]=new TextActionItem("text3","Text3");
							poi.Pages[pg].PageActions[4]=new ImageActionItem("img1","image1.gif",0,0);
							poi.Pages[pg].PageActions[5]=new ImageActionItem("img2","image2.gif",0,0);

							poi.Questions[pg]=new QuestionItem("QueTempl1.htm","MultipleChoice","Frage",1,true,true);
							poi.Questions[pg].QuestionActions = new ActionItem[1];
							poi.Questions[pg].Answers = new String[] {"Antwort1","Antwort2","Antwort3","Antwort4"};
							poi.Questions[pg].QuestionActions[0]=new ImageActionItem("quimg1","question1.gif",0,0);
						}
						chp.Points[p]=poi;
					}
					book.Chapters[c]=chp;
				}
				lib.Books[b]=book;
			}
			*/
			lib.IsModified=true;
		}

		public BookItem GetBook(string path)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
					return FindBook(lib,aTitles[1]);
			}
			return null;
		}

		public bool SetBook(string path,BookItem item)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					for(int i=0;i<lib.Books.Length;++i)
						if (lib.Books[i].title == aTitles[1])
						{
							lib.Books[i]=item;
							lib.IsModified=true;
							return true;
						}
				}
			}
			return false;
		}


        public bool SetBookTitle(string path, string title)
        {
            string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles) && aTitles.Length==2 && 
                FindBook(GetLibrary(aTitles[0]), title) != null)
                return false;

            var bok = GetBook(path);
            if (bok != null)
            {
                bok.title = title;
                SetModified(path);
                return true;
            }
            return false;
        }

		public bool AddBook(string path,string title,bool prev)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					if (FindBook(lib,title)!=null)
						return false;

                    if (lib.Books == null || aTitles.Length < 2)
                    {
                        lib.Books = new BookItem[1];
                        lib.Books[0] = new BookItem(title, true);
                    }
                    else
                    {
                        for (int i = 0; i < lib.Books.Length; ++i)
                            if (lib.Books[i].title == aTitles[1])
                            {
                                ArrayList aEntries = new ArrayList(lib.Books);
                                if (prev)
                                    aEntries.Insert(i, new BookItem(title, true));
                                else
                                    aEntries.Insert(i + 1, new BookItem(title, true));

                                lib.Books = new BookItem[aEntries.Count];
                                aEntries.CopyTo(lib.Books);
                                break;
                            }
                    }

                    string[] aPath = { lib.title, title };
                    AddChapter(Utilities.MergePath(aPath), "Chapter1", false);
                    lib.IsModified = true;
                    return true;
				}
			}
			return false;
		}

		public bool DeleteBook(string path)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					if (aTitles.Length<2 || FindBook(lib,aTitles[1])==null)
						return false;

					for(int i=0;i<lib.Books.Length;++i)
						if (lib.Books[i].title == aTitles[1])
						{
							ArrayList aEntries = new ArrayList(lib.Books);
							aEntries.RemoveAt(i);
							lib.Books = new BookItem[aEntries.Count];
							aEntries.CopyTo(lib.Books);
							lib.IsModified = true;
							return true;
						}
				}
			}
			return false;
		}

        public bool MoveBook(string libPath, string title1, string title2)
        {
            var lib=GetLibrary(libPath);
            if (lib!=null)
            {
                var b1 = Array.FindIndex(lib.Books,x => x.title == title1);
                var b2 = Array.FindIndex(lib.Books,x => x.title == title2);

                if (b1>=0 && b2>=0)
                    Utilities.ShiftElement(lib.Books, b1, b2);

                SetModified(libPath);
                return true;
            }
            return false;        
        }

		public int  GetBookCnt(string path)
		{
			string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles) && aTitles.Length >= 1)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					if (lib.Books!=null)
						return lib.Books.Length;
				}
			}
			return 0;
		}

		public ChapterItem GetChapter(string path)
		{
			string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
						return FindChapter(book,aTitles[2]);
				}
			}
			return null;
		}

		public bool SetChapter(string path,ChapterItem item)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						for(int i=0;i<book.Chapters.Length;++i)
							if (book.Chapters[i].title == aTitles[2])
							{
								book.Chapters[i]=item;
								lib.IsModified=true;
								return true;
							}
					}
				}
			}
			return false;
		}

        public bool SetChapterTitle(string path, string title)
        {
            string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles) && aTitles.Length == 3 &&
                FindChapter(FindBook(GetLibrary(aTitles[0]),aTitles[1]),title) != null)
                return false;

            var chp = GetChapter(path);
            if (chp != null)
            {
                chp.title = title;
                SetModified(path);
                return true;
            }
            return false;
        }

		public bool AddChapter(string path,string title,bool prev)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						if (FindChapter(book,title)!=null)
							return false;

                        if (book.Chapters == null || aTitles.Length < 3)
                        {
                            book.Chapters = new ChapterItem[1];
                            book.Chapters[0] = new ChapterItem(title, true);
                        }
                        else
                        {
                            for (int i = 0; i < book.Chapters.Length; ++i)
                                if (book.Chapters[i].title == aTitles[2])
                                {
                                    ArrayList aEntries = new ArrayList(book.Chapters);
                                    if (prev)
                                        aEntries.Insert(i, new ChapterItem(title, true));
                                    else
                                        aEntries.Insert(i + 1, new ChapterItem(title, true));

                                    book.Chapters = new ChapterItem[aEntries.Count];
                                    aEntries.CopyTo(book.Chapters);
                                    break;
                                }
                        }
                        
                        string[] aPath = { lib.title, book.title, title };
                        AddPoint(Utilities.MergePath(aPath), "Point1", false);
                        lib.IsModified = true;
                        return true;
					}
				}
			}
			return false;
		}

		public bool DeleteChapter(string path)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						if (aTitles.Length<3 || FindChapter(book,aTitles[2])==null)
							return false;

						for(int i=0;i<book.Chapters.Length;++i)
							if (book.Chapters[i].title == aTitles[2])
							{
								ArrayList aEntries = new ArrayList(book.Chapters);
								aEntries.RemoveAt(i);
								book.Chapters = new ChapterItem[aEntries.Count];
								aEntries.CopyTo(book.Chapters);
								lib.IsModified = true;
								return true;
							}
					}
				}
			}
			return false;
		}


        public bool MoveChapter(string bookPath, string title1, string title2)
        {
            var book = GetBook(bookPath);
            if (book != null)
            {
                var c1 = Array.FindIndex(book.Chapters, x => x.title == title1);
                var c2 = Array.FindIndex(book.Chapters, x => x.title == title2);

                if (c1 >= 0 && c2 >= 0)
                    Utilities.ShiftElement(book.Chapters, c1, c2);
                SetModified(bookPath);
                return true;
            }
            return false;
        }

		public int  GetChapterCnt(string path)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles) && aTitles.Length>=2)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
						return book.Chapters.Length;
				}
			}
			return 0;
		}


		public PointItem GetPoint(string path)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles) && aTitles.Length>=4)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						ChapterItem chp=FindChapter(book,aTitles[2]);
						if (chp!=null )
							return FindPoint(chp,aTitles[3]);
					}
				}
			}
			return null;
		}

		public bool SetPoint(string path,PointItem item)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						ChapterItem chp=FindChapter(book,aTitles[2]);
						if (chp!=null)
						{
							for(int i=0;i<chp.Points.Length;++i)
								if (chp.Points[i].title == aTitles[3])
								{
									chp.Points[i]=item;
									lib.IsModified=true;
									return true;
								}
						}
					}
				}
			}
			return false;
		}

		public bool AddPoint(string path,string title,bool prev)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						ChapterItem chp=FindChapter(book,aTitles[2]);
						if (chp!=null)
						{
							if (FindPoint(chp,title)!=null)
								return false;

                            if (chp.Points== null || aTitles.Length < 4)
                            {
                                chp.Points = new PointItem[1];
                                chp.Points[0] = new PointItem(title, true);
                            }
                            else
                            {
                                for (int i = 0; i < chp.Points.Length; ++i)
                                    if (chp.Points[i].title == aTitles[3])
                                    {
                                        ArrayList aEntries = new ArrayList(chp.Points);
                                        if (prev)
                                            aEntries.Insert(i, new PointItem(title, true));
                                        else
                                            aEntries.Insert(i + 1, new PointItem(title, true));

                                        chp.Points = new PointItem[aEntries.Count];
                                        aEntries.CopyTo(chp.Points);
                                        lib.IsModified = true;
                                        return true;
                                    }
                            }
						}
					}
				}
			}
			return false;
		}

		public bool DeletePoint(string path)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles))
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						ChapterItem chp=FindChapter(book,aTitles[2]);
						if (chp!=null)
						{
							if (aTitles.Length<4 || FindPoint(chp,aTitles[3])==null)
								return false;

							for(int i=0;i<chp.Points.Length;++i)
								if (chp.Points[i].title == aTitles[3])
								{
									ArrayList aEntries = new ArrayList(chp.Points);
									aEntries.RemoveAt(i);
									chp.Points = new PointItem[aEntries.Count];
									aEntries.CopyTo(chp.Points);
									lib.IsModified = true;
									return true;
								}
						}
					}
				}
			}
			return false;
		}

        public bool MovePoint(string chpPath, string title1, string title2)
        {
            var chp = GetChapter(chpPath);
            if (chp != null)
            {
                var p1 = Array.FindIndex(chp.Points, x => x.title == title1);
                var p2 = Array.FindIndex(chp.Points, x => x.title == title2);

                if (p1 >= 0 && p2 >= 0)
                    Utilities.ShiftElement(chp.Points, p1, p2);
                SetModified(chpPath);
                return true;
            }
            return false;
        }

        public int  GetPointCnt(string path)
		{
			string[] aTitles;
			if (Utilities.SplitPath(path,out aTitles) && aTitles.Length>=3)
			{
				LibraryItem lib=(LibraryItem) m_dLibraries[aTitles[0]];
				if (lib!=null)
				{
					BookItem book=FindBook(lib,aTitles[1]);
					if (book!=null)
					{
						ChapterItem chp=FindChapter(book,aTitles[2]);
						if (chp!=null)
							return chp.Points.Length;
					}
				}
			}
			return 0;
		}

        public bool SetPointTitle(string path, string title)
        {
            string[] aTitles;
            if (Utilities.SplitPath(path, out aTitles) && aTitles.Length == 4 &&
                FindPoint(FindChapter(FindBook(GetLibrary(aTitles[0]), aTitles[1]),aTitles[2]),title)!=null)
                return false;

            PointItem poi= GetPoint(path);
            if (poi != null)
            {
                poi.title = title;
                SetModified(path);
                return true;
            }
            return false;
        }

		private BookItem FindBook(LibraryItem lib,string title)
		{
            if (lib.Books !=null)
			    for(int i=0;i<lib.Books.Length;++i)
				    if (lib.Books[i].title == title)
					    return lib.Books[i];
			return null;
		}

		private ChapterItem FindChapter(BookItem book,string title)
		{
            if (book.Chapters != null)
                for (int i = 0; i < book.Chapters.Length; ++i)
				    if (book.Chapters[i].title == title)
					    return book.Chapters[i];
			return null;
		}

		private PointItem FindPoint(ChapterItem chp,string title)
		{
            if (chp.Points != null)
                for (int i = 0; i < chp.Points.Length; ++i)
				    if (chp.Points[i].title == title)
					    return chp.Points[i];
			return null;
		}


		public bool Initialize()
		{
			for(int l=0;l<GetLibraryCnt();++l)
			{
				LibraryItem lib=GetLibrary(l);
				if (lib!=null)
				{
                    var dLibDocuments = new Dictionary<string,string>();
                    m_dDocuments[lib.title] = dLibDocuments;
                    if (lib.Books!=null)
					    for(int b=0;b<lib.Books.Length;++b)
					    {
						    BookItem book=lib.Books[b];
                            if (book.Chapters!=null)
						        for(int c=0;c<book.Chapters.Length;++c)
						        {
							        ChapterItem chp=book.Chapters[c];
                                    if (chp.Points!=null)
							            for(int p=0;p<chp.Points.Length;++p)
							            {
								            PointItem poi=chp.Points[p];
								            if (poi!=null && poi.Pages!=null && poi.Pages.Length>0)
								            {
									            for(int pg=0;pg<poi.Pages.Length;++pg)
									            {
										            PageItem page=poi.Pages[pg];
										            if (page!=null && page.PageActions.Length>0)
										            {
                                                        foreach (var a in page.PageActions)
                                                        {
                                                            a.OnLoad(lib, book, chp, poi);
                                                            if (a is DocumentActionItem)
                                                            {
                                                                var docAction = a as DocumentActionItem;
                                                                if (docAction.typeId != 3)
                                                                {
                                                                    dLibDocuments[docAction.id] = GetFileName(lib.title).ToLower() + "\\docs\\" + System.IO.Path.GetFileName(docAction.fileName);
                                                                }
															}
                                                        }
										            }
									            }
								            }
							            }
						        }
					    }
				}
			}

            LibraryManagerEventArgs ea = new LibraryManagerEventArgs(LibraryManagerEventArgs.CommandType.ChangeManagement, "");
            m_parent.FireEvent(ref ea);
			return true;
		}

        public SortedList<string, Dictionary<string, string>> GetDocumentFilenames()
        {
            return m_dDocuments;    
        }

        public void SetAdapter(ILibraryAdapter adapter)
        {
            
        }
	}
}
