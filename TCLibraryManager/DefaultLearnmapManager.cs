using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Libraries
{

    public class DefaultLearnmapManager : ILearnmapManager
	{
        private SortedDictionary<string, LearnmapItem> m_dMaps = new SortedDictionary<string, LearnmapItem>();
        private Dictionary<string, string> m_dFiles = new  Dictionary<string, string>();
		private KeywordCollection m_aKeywords=null;
        private LearnmapManagerBridge m_parent = null;
        private ILearnmapAdapter m_adapter = null;

        public void SetParent(object parent)
        {
            m_parent = (LearnmapManagerBridge)parent;
        }

        public bool OpenFromDirectory(string directory)
        {
            CloseAll();

            string[] mapFiles = System.IO.Directory.GetFiles(directory, "*.xml");
            foreach (var m in mapFiles)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LearnmapItem));
                FileStream fs = new FileStream(m, FileMode.Open, FileAccess.Read);
                LearnmapItem map = (LearnmapItem)serializer.Deserialize(fs);
                fs.Close();

                if (GetId(map.title) < 0) //if not already there
                {
                    // learnmap should have format [Title].xml not learmapX.xml -> check
                    if (String.Compare(Path.GetFileNameWithoutExtension(m),map.title,true)!=0)
                    {
                        //string strTitle = map.title;
                        //int iId = 1;
                        //while (GetId(strTitle) >= 0) // if map already exists, take new na,e
                        //{
                        //    strTitle += strTitle + "_" + iId.ToString();
                        //    ++iId;
                        //}
                        // clone map and save map with new filename [title].xml
                        m_dMaps[map.title] = (LearnmapItem)map.Clone();
                        m_dFiles[map.title] = directory + @"\" + map.title + ".xml";
                        m_dMaps[map.title].IsModified = true;
                        Save(map.title);

                        File.Delete(m); // delete map in old format
                    }
                    else
                    {
                        m_dMaps[map.title] = (LearnmapItem)map.Clone();
                        m_dFiles[map.title] = m;
                    }
                }
            }
            return true;
        }

		public bool Open(string fileName)
		{
			if (m_dFiles.ContainsValue(fileName))
				return false;

			XmlSerializer serializer = new XmlSerializer(typeof(LearnmapItem));
			FileStream fs = new FileStream(fileName, FileMode.Open,FileAccess.Read);
			LearnmapItem map= (LearnmapItem) serializer.Deserialize(fs);
            fs.Close();

            m_dMaps[map.title]= (LearnmapItem) map.Clone();
			m_dFiles[map.title]=fileName;
			
            LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.Open, map.title, 0);
            m_parent?.FireEvent(ref ea);
            return true;
		}

        public bool OpenAs(string fileName,string newTitle, bool isServerMap)
        {
            if (m_dFiles.ContainsValue(fileName))
                return false;

            XmlSerializer serializer = new XmlSerializer(typeof(LearnmapItem));
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            LearnmapItem map = (LearnmapItem)serializer.Deserialize(fs);

            map.title = newTitle;
            map.isServerMap = isServerMap;
            m_dMaps[map.title] = (LearnmapItem)map.Clone();
            m_dFiles[map.title] = fileName;
            m_dMaps[map.title].IsModified = true;

            fs.Close();

            LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.Open, map.title, 0);
            m_parent?.FireEvent(ref ea);
            return true;
        }


        public bool OpenFromImport(string mapsFolder, string fileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LearnmapItem));
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                var mapNew = (LearnmapItem)serializer.Deserialize(fs);
                fs.Close();

                var mapOld = GetLearnmap(mapNew.title);
                if (mapOld != null) // map already there?
                {
                    // replace old map with new one
                    string oldFilename = (string)m_dFiles[mapOld.title]; 
                    Close(mapOld.title);
                    File.Copy(fileName, oldFilename);
                    Open(mapNew.title);
                }
                else
                {
                    string newFileName = mapsFolder + "\\" + mapNew.title.ToLower() + ".xml";
                    File.Copy(fileName, newFileName, true);
                    OpenAs(newFileName, mapNew.title, /*isServerMap=*/true);
                    Save(mapNew.title);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }



        public bool OpenSilent(string fileName)
        {
            if (m_dFiles.ContainsValue(fileName))
                return false;

            XmlSerializer serializer = new XmlSerializer(typeof(LearnmapItem));
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            LearnmapItem map = (LearnmapItem)serializer.Deserialize(fs);

            m_dMaps[map.title] = (LearnmapItem)map.Clone();
            m_dFiles[map.title] = fileName;

            fs.Close();

            return true;
        }
 
		public  bool Create(string filename,string title, bool isServerMap)
		{
            if (m_dMaps.ContainsKey(title))
                return false;

            LearnmapItem map=new LearnmapItem(title,"", false, false, isServerMap);
			m_dMaps[title]=map;
			m_dFiles[title]=filename;
			map.IsModified=true;

            LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.Create, map.title, 0);
            m_parent?.FireEvent(ref ea);

			return true;
		}

        public bool CreateSilent(string filename, string title)
        {
            if (m_dMaps.ContainsKey(title))
                return false;

            LearnmapItem map = new LearnmapItem(title, "", false, false, false);
            m_dMaps[title] = map;
            m_dFiles[title] = filename;
            map.IsModified = true;

            return true;
        }


        private bool AddWorking(int mapId,string link,string test,string version)
		{
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
            if (map != null)
                return AddWorking(map.title, link, test, version);
            return false;
        }

        public  bool AddWorking(string mapTitle,string link,string test,string version)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map==null)
                return false;

            if (map.Workings == null)
                map.Workings = new WorkingItem[1];
            else
            {
                WorkingItem[] aNewItems = new WorkingItem[map.Workings.Length + 1];
                map.Workings.CopyTo(aNewItems, 0);
                map.Workings = aNewItems;
            }

            map.Workings[map.Workings.Length - 1] = new WorkingItem(link,test,version);
            map.IsModified = true;

            LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.AddWorking, map.title, 0);
            m_parent?.FireEvent(ref ea);

            return true;
		}

		private bool DeleteWorking(int mapId,string link)
		{
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
            if (map != null)
                return DeleteWorking(map.title, link);
            return false;

		}

		public bool DeleteWorking(string mapTitle,string link,string test="")
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map==null)
                return false;
            if (map.Workings.Length == 0 || map.Workings == null)
                return false;
            else
            {
                try
                {
	                var item=map.Workings.First(w => w.link == link && w.test==test);
	                if (item != null)
	                    map.Workings = map.Workings.RemoveFromArray(item);
                }
                catch (System.Exception ex)
                {
                    return false;
                }
            }

            map.IsModified = true;

            LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.DeleteWorking, map.title, 0);
            m_parent?.FireEvent(ref ea);

            return true;
		}

        public bool SetItem(string title, LearnmapItem item)
        {
            var l_item = GetLearnmap(title);
            if (l_item != null)
            {
                l_item = (LearnmapItem)item.Clone();
                m_dMaps[title] = l_item;
                m_dMaps[title].IsModified = true;
                return true;
            }

            return false;
        }

        public  bool GetWorkings(int id,ref string[] aWorkings,bool bIgnoreEmpty=true)
		{
			if (id<0 || id>=m_dMaps.Count)
				return false;

            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[id]];
            if (map != null)
                return GetWorkings(map.title, ref aWorkings,bIgnoreEmpty);
            return false;

		}

        public bool GetWorkings(string mapTitle, ref string[] aWorkings, bool bIgnoreEmpty = true)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map==null || map.Workings == null || map.Workings.Length == 0)
                return false;

            int i = 0;
            aWorkings = new string[map.Workings.Length];
		    foreach (WorkingItem item in map.Workings)
		    {
		        if (!bIgnoreEmpty || item.link.Length > 0)
		            aWorkings[i++] = item.link;
                else
		            aWorkings=aWorkings.RemoveFromArray(aWorkings.Length - 1);
		    }

            return aWorkings.Length>0;
        }

        public bool GetWorkingTests(int id, ref string[] aTests)
        {
            if (id < 0 || id >= m_dMaps.Count)
                return false;

            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[id]];
            if (map != null)
                return GetWorkingTests(map.title, ref aTests);
            return false;

        }

        public bool GetWorkingTests(string mapTitle, ref string[] aTests)
        {
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map == null || map.Workings == null || map.Workings.Length == 0)
                return false;

            int i = 0;
            aTests = new string[map.Workings.Length];
            foreach (WorkingItem item in map.Workings)
                aTests[i++] = item.test;
            return true;
        }


        public void MoveWorking(string mapTitle, int iId, int iDelta)
        {
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map != null && map.Workings != null && map.Workings.Length > 0)
            {
                if (iDelta < 0)
                {
                    if (iId > 0)
                    {
                        Utilities.ShiftElement(map.Workings, iId, iId + iDelta);
                        Save(mapTitle);
                    }
                }
                else if (iDelta > 0)
                {
                    if (iId < (map.Workings.Length - 1))
                    {
                        Utilities.ShiftElement(map.Workings, iId, iId + iDelta);
                        Save(mapTitle);
                    }
                }
            }
        }

        public bool SetTest(string mapTitle,int testId, string title, bool randomChoose, int questionCnt, 
                            int trialCnt, int successLevel, string questionnaire, bool testAlwaysAllowed, TestType eType)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map==null)
                return false;

            if (map.Tests == null)
            {
                Array.Resize<TestItem>(ref map.Tests, 1);
                map.Tests[0] = new TestItem();
            }
            else if (map.Tests.Length < (testId + 1))
            {
                Array.Resize<TestItem>(ref map.Tests, testId + 1);
                map.Tests[testId] = new TestItem();
            }

            map.Tests[testId].title = title;
            map.Tests[testId].randomChoose = randomChoose;
            map.Tests[testId].questionCount = questionCnt;
            map.Tests[testId].trialCount = trialCnt;
            map.Tests[testId].successLevel = successLevel;
            map.Tests[testId].questionnare = questionnaire;
		    map.Tests[testId].testAlwaysAllowed = testAlwaysAllowed;
            map.Tests[testId].type = eType.ToString("G");
            map.IsModified = true;
            return true;
        }

        public bool SetTest(int mapId, int testId, string title, bool randomChoose, int questionCnt,
                            int trialCnt, int successLevel, string questionnaire, bool testAlwaysAllowed, TestType eType)
		{
			if (mapId<0 || mapId>=m_dMaps.Count)
				return false;

            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
		    if (map != null)
		        return SetTest(map.title, testId, title, randomChoose, questionCnt, trialCnt, successLevel, questionnaire, testAlwaysAllowed, eType);
            return false;
		}

        public TestItem GetTest(string mapTitle, int id)
        {
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map == null)
                return null;
            if (map.Tests == null)
            {
                SetTest(mapTitle, 0, Utilities.c_strDefaultFinalTest, true, 10, 5, 85, "", false, TestType.Final);
                Save(mapTitle);
            }
            return (id < map.Tests.Length) ? map.Tests[id] : null;
        }

        public TestItem GetTest(string mapTitle, string testName)
        {
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map != null && map.Tests != null)
            {
                var t = map.Tests.Single(x => x.title == testName);
                if (t != null)
                    return t;
            }
            return null;
        }

        public bool GetTest(string mapTitle, int testId,ref bool randomChoose, ref int questionCnt, 
                            ref int trialCnt, ref int successLevel, ref string questionnaire,ref bool testAlwaysAllowed, ref TestType type)
		{
            TestItem item = GetTest(mapTitle, testId);
            if (item != null)
            {
                randomChoose = item.randomChoose;
                questionCnt = item.questionCount;
                trialCnt = item.trialCount;
                successLevel = item.successLevel;
                questionnaire = item.questionnare;
                testAlwaysAllowed = item.testAlwaysAllowed;
                type = (item.type==TestType.Intermediate.ToString("g")) ? TestType.Intermediate : TestType.Final;
                return true;
            }
            return false;
        }


	    public int GetTestCount(string mapTitle)
	    {
	        LearnmapItem map = GetLearnmap(mapTitle);
	        if (map == null || map.Tests==null)
	            return 0;
	        return map.Tests.Length;
	    }


		public bool GetTest(int mapId, int testId, ref bool randomChoose,ref int questionCnt,
                            ref int trialCnt, ref int successLevel, ref string questionnaire, ref bool testAlwaysAllowed , ref TestType type)
		{
            if (mapId < 0 || mapId >= m_dMaps.Count)
                return false;
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
            if (map != null)
                return GetTest(map.title, testId, ref randomChoose, ref questionCnt, ref trialCnt, ref successLevel,ref questionnaire,ref testAlwaysAllowed, ref type);
            return false;
		}
		
		public  bool AddTestQuestion(string mapTitle,int testId, string contentPath,int questionId)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map==null || map.Tests == null || map.Tests.Length < (testId+1))
                return false;

            if (map.Tests[testId].TestQuestions == null || map.Tests[testId].TestQuestions.Length==0)
            {
                Array.Resize<TestQuestionItem>(ref map.Tests[testId].TestQuestions, 1);
                map.Tests[testId].TestQuestions[0] = new TestQuestionItem(contentPath, questionId);
            }
            else
            {
                int newLength = map.Tests[testId].TestQuestions.Length + 1;
                Array.Resize<TestQuestionItem>(ref map.Tests[testId].TestQuestions, newLength);
                map.Tests[testId].TestQuestions[newLength - 1] = new TestQuestionItem(contentPath, questionId);
            }

            map.IsModified = true;
            return true;
		}

		public bool AddTestQuestion(int mapId,int testId,string contentPath,int questionId)
		{
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
            if (map != null)
                return AddTestQuestion(map.title, testId, contentPath, questionId);
            return false;
        }

        public bool DeleteTestQuestion(int mapId, int testId, string contentPath, int questionId)
		{
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
            if (map != null)
                return DeleteTestQuestion(map.title,testId, contentPath, questionId);
            return false;
        }

		public bool DeleteTestQuestion(string mapTitle,int testId, string contentPath,int questionId)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map==null)
                return false;
            if (map.Tests == null || map.Tests.Length<(testId+1) || map.Tests[testId].TestQuestions == null || 
                map.Tests[testId].TestQuestions.Length == 0)
                return false;
            else
            {
                TestQuestionItem[] aNewItems = new TestQuestionItem[map.Tests[testId].TestQuestions.Length - 1];
                for (int i = 0, j = 0; i < map.Tests[testId].TestQuestions.Length; ++i)
                {
                    if (!(String.Compare(map.Tests[testId].TestQuestions[i].contentPath, contentPath) == 0 &&
                          map.Tests[testId].TestQuestions[i].questionId == questionId))
                        aNewItems[j++] = map.Tests[testId].TestQuestions[i];
                }

                map.Tests[testId].TestQuestions = aNewItems;
            }

            map.IsModified = true;
            return true;
		}

        public bool GetTestQuestions(string mapTitle, int testId, ref TestQuestionItem[] aTestQuestions)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map==null)
                return false;
            if (map.Workings == null || map.Tests == null || map.Tests.Length<(testId+1) || map.Tests[testId] == null ||
                map.Tests[testId].TestQuestions == null || map.Tests[testId].TestQuestions.Length == 0)
                return false;

            aTestQuestions = (TestQuestionItem[])map.Tests[testId].TestQuestions.Clone();
            return true;
		}

        public bool GetTestQuestions(int id, int testId, ref TestQuestionItem[] aTestQuestions)
		{
			if (id<0 || id>=m_dMaps.Count)
				return false;
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[id]];
            if (map!=null)
                return GetTestQuestions(map.title, testId, ref aTestQuestions);
            return false;

		}
        
        public bool DeleteTest(int mapId, int testId)
        {
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
            if (map != null)
                return DeleteTest(map.title, testId);
            return false;
        }


	    public bool DeleteTest(string mapTitle,int testId)
	    {
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map == null)
                return false;
            if (map.Workings == null || map.Tests == null || map.Tests.Length < (testId + 1) || map.Tests[testId] == null)
                return false;

	        map.Tests=map.Tests.RemoveFromArray(map.Tests[testId]);
	        
            return true;
	    }

        public void SetAdapter(ILearnmapAdapter adapter)
        {
            m_adapter = adapter;
        }

		private void Close(int mapId)
		{
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
            if (map!=null)
            {
                Save(map.title);
                Close(map.title);
            }

		}

        public void Close(string mapTitle)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map != null)
            {
                m_dMaps.Remove(map.title);
                m_dFiles.Remove(map.title);
                if (m_aKeywords != null)
                {
                    m_aKeywords.Clear();
                    m_aKeywords = null;
                }

                LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.Close, map.title, 0);
                m_parent?.FireEvent(ref ea);
            }
        }

		public void CloseAll()
		{
			while(m_dMaps.Count>0)
				Close(0);
		}

		private void Destroy(int mapId)
		{
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
            if (map != null)
                Destroy(map.title);
        }

        public void Destroy(string mapTitle)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map != null)
            {
                try
                {
                    File.Delete((string)m_dFiles[mapTitle]);
                    Close(mapTitle);

                    LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.Destroy, map.title, 0);
                    m_parent?.FireEvent(ref ea);
                }
                catch (Exception ex)
                {
                    m_adapter.ShowMessageBox("ERROR", "", ex.Message);
                }
            }
		}

	
		public void Save(int mapId)
		{
			if (mapId<0 || mapId>=m_dMaps.Count)
				return;
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
            if (map != null)
                Save(map.title);
        }

		public void Save(string mapTitle)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map != null)
            {
                string filename = m_dFiles[mapTitle];

                if (!Directory.Exists(Path.GetDirectoryName(filename)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filename));

                if (map.IsModified)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LearnmapItem));
                    TextWriter writer = new StreamWriter(filename);

                    map.IsModified = false;

                    // Serialize the map, and close the TextWriter.
                    serializer.Serialize(writer, map);
                    writer.Close();
                }
            }
		}

		public  int GetMapCnt()
		{
			return m_dMaps.Count;
		}

		public  bool GetTitle(int id,out string title)
		{
			if (id>=0 && id<m_dMaps.Count)
			{
                LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[id]];
                title = map.title;
				return true;
			}

			title="";
			return false;
		}
		

		public bool GetColor(int mapId,out string color)
		{
            if (mapId >= 0 && mapId < m_dMaps.Count)
            {
                LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
                if (map != null)
                    return GetColor(map.title, out color);
            }
            color = "";
            return false;
        }

        public bool GetColor(string mapTitle,out string color)
        {
            color = "";
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map==null)
                return false;
            color = map.color;
            if (color != null)
            {
                color = map.color;
                return true;
            }
            return false;
        }

		public bool SetColor(int mapId,string color)
		{
            if (mapId < 0 || mapId >= m_dMaps.Count)
                return false;
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[mapId]];
            if (map != null)
                return SetColor(map.title, color);
            return false;
		}

        public bool SetColor(string mapTitle, string color)
        {
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map == null)
                return false;
            map.color = color;
            map.IsModified = true;
            return true;
        }

		public int GetId(string title)
		{
            if (m_dMaps != null && m_dMaps.Count > 0)
            {
                int i = 0;
                foreach (var m in m_dMaps)
                {
                    if (m.Value.title == title)
                        return i;
                    else
                        ++i;
                }
            }
            return -1;
		}

        public LearnmapItem GetItem(string title)
        {
            return GetLearnmap(title);
        }

        public string GetFileName(int id)
		{
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[id]];
            if (map != null)
                return GetFileName(map.title);
            return null;
		}

		public string GetFileName(string title)
		{
            return m_dFiles[title];
        }

        public string FindMapByWorking(string working)
        {
            IDictionaryEnumerator iter = m_dMaps.GetEnumerator();
            while (iter.MoveNext())
            {
                LearnmapItem map = (LearnmapItem)iter.Value;
                if (map.Workings != null)
                    for (int i = 0; i < map.Workings.Length; ++i)
                        if (String.Compare(map.Workings[i].link, working, true) == 0)
                            return map.title;
            }
            return null;
        }

		public int FindMapsByWorking(string working,out List<KeyValuePair<string,string>> dMaps)
		{
            dMaps = new List<KeyValuePair<string,string>>();
			IDictionaryEnumerator iter = m_dMaps.GetEnumerator();
			while(iter.MoveNext())
			{
				LearnmapItem map = (LearnmapItem) iter.Value;
                if(map.Workings!=null)
                    for (int i = 0; i < map.Workings.Length; ++i)
                    {
                        string[] aTitles,aTitlesSearch;
                        Utilities.SplitPath(working, out aTitlesSearch);
                        Utilities.SplitPath(map.Workings[i].link,out aTitles);

                        if (aTitlesSearch.Length == 1)
                        {
                            if (aTitles[0] == aTitlesSearch[0])
                                dMaps.Add(new KeyValuePair<string,string>(map.title, map.Workings[i].link));
                        }
                        else if (aTitlesSearch.Length == 2)
                        {
                            if (aTitles[0] == aTitlesSearch[0] && 
                                aTitles[1] == aTitlesSearch[1])
                                dMaps.Add(new KeyValuePair<string, string>(map.title, map.Workings[i].link));
                        }
                        else if (aTitlesSearch.Length == 3)
                        {
                            if (aTitles[0] == aTitlesSearch[0] && 
                                aTitles[1] == aTitlesSearch[1] &&
                                aTitles[2] == aTitlesSearch[2])
                                dMaps.Add(new KeyValuePair<string, string>(map.title, map.Workings[i].link));
                        }
                        else 
                        {
                            if (String.Compare(map.Workings[i].link, working, true) == 0)
                                dMaps.Add(new KeyValuePair<string, string>(map.title, map.Workings[i].link));
                        }
                    }
			}
			return dMaps.Count;
		}

        public CollectionBase GetAllKeywords()
		{
			if (m_aKeywords==null)
			{
				m_aKeywords = new KeywordCollection();

				for(int i=0;i<GetMapCnt();++i)
				{
					string title;
					string [] aWorkings=null;

					GetTitle(i,out title);
					GetWorkings(title,ref aWorkings);

                    if (aWorkings != null)
                        for (int j = 0; j < aWorkings.Length; ++j)
                            m_adapter.GetKeywords(aWorkings[j], ref m_aKeywords);
							//AppHandler.LibManager.GetKeywords(aWorkings[j],ref m_aKeywords);
				}
			}
			return m_aKeywords;
		}
	
		private  bool SetUsers(int id,string[] aNames)
		{
			if (id<0 || id>=m_dMaps.Count)
				return false;

            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[id]];
            if (map != null)
                return SetUsers(map.title, aNames);
			return false;
		}

        public bool SetUsers(string mapTitle, string[] aNames)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map == null)
                return false;
            if (map != null)
            {
                map.Users = aNames;
                map.IsModified = true;

                LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.AddUser, map.title, 0);
                m_parent?.FireEvent(ref ea);
                return true;
            }
            return false;
        }

		public bool GetUsers(int id,ref string[] aNames)
		{
            if (id < 0 || id >= m_dMaps.Count)
                return false;
            LearnmapItem map = m_dMaps[m_dMaps.Keys.ToList()[id]];
            if (map != null)
                return GetUsers(map.title, ref aNames);
            return false;
		}

        public bool GetUsers(string mapTitle, ref string[] aNames)
		{
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map == null)
                return false;
            if (map != null && map.Users!=null)
            {
                aNames = map.Users;
                return true;
            }
			return false;
		}

		public bool HasUser(int mapId,string name)
		{
			string [] aUsers=null;
			GetUsers(mapId,ref aUsers);
			if (aUsers!=null)
				for(int i=0;i<aUsers.Length;++i)
					if (aUsers[i]==name)
						return true;
			return false;
		}

		public bool HasUser(string title,string name)
		{
			string [] aUsers=null;
			GetUsers(title,ref aUsers);
            return (aUsers!=null && Array.Find(aUsers, u => u==name) !=null );
		}

		public bool AddUser(string title,string name)
		{
			if (!HasUser(title,name))
			{
                bool bErg = false;
				string[] aNames=null;
				if (GetUsers(title,ref aNames))
				{
					string [] aNewUsers= new string[aNames.Length+1];
					Array.Copy(aNames,aNewUsers,aNames.Length);
					aNewUsers[aNewUsers.Length-1] = name;
					bErg=SetUsers(title,aNewUsers);
                 }
				else
			    {
					bErg=SetUsers(title,new string[] {name});
				}
                
                if (bErg)
                {
                    LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.AddUser, title, 0);
                    m_parent?.FireEvent(ref ea);
                }

                Save(title);

                return bErg;
			}
			return false;
		}

		public bool DeleteUser(string title,string name)
		{
			if (HasUser(title,name))
			{
			    string[] aNames=null;
                bool bErg = true;
			    if (GetUsers(title,ref aNames))
			    {
				    int newId=0;
			        string [] aNewUsers= new string[aNames.Length-1];
			        for(int j=0;j<aNames.Length;++j)
				        if (aNames[j] != name)
					        aNewUsers.SetValue(aNames[j],newId++);
                    bErg = SetUsers(title, aNewUsers);
			    }
			    else
			        bErg = SetUsers(title,null);

                if (bErg)
                {
                    LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.DeleteUser, title, 0);
                    m_parent?.FireEvent(ref ea);
                }

                Save(title);

                return bErg;
            }
			return false;
		}

		public bool DeleteUser(int mapId,string name)
		{
			string title=null;
			GetTitle(mapId,out title);
			if (title!=null)
				return DeleteUser(title,name);
			return false;
		}

		public bool DeleteUser(string name)
		{
            bool bRes = false;
            for (int i = 0; i < m_dMaps.Count; ++i)
                bRes |= DeleteUser(i, name);
			return bRes;
		}

		private void CreateDefault(LearnmapItem map)
		{
			map.title = "Testmappe";
			map.Workings = new WorkingItem[10];
			for(int i=0;i<10;++i)
				map.Workings[i]= new WorkingItem(String.Format("Bibliothek1/Buch{0}/Kapitel1/Punkt1",i),"","");
		}

        public bool SetUsage(string mapTitle, bool bIsClassMap)
        {
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map == null)
                return false;

            map.isClassMap = bIsClassMap;
            map.IsModified = true;
            LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.ChangeUsage, mapTitle, 0);
            m_parent?.FireEvent(ref ea);
            return true;
        }
        
        public bool GetUsage(string mapTitle, ref bool bIsClassMap)
        {
            if (m_dMaps.Values.Where(x => x.title == mapTitle).Count() == 0)
                return false;
            LearnmapItem map = (LearnmapItem)m_dMaps[mapTitle];
            if (map != null)
            {
                bIsClassMap = map.isClassMap;
                return true;
            }
            return false;
        }

        public bool GetUsage(int mapId, ref bool bIsClassMap)
        {
            string title;
            GetTitle(mapId, out title);
            if (title != null)
                return GetUsage(title, ref bIsClassMap);
            return false;
        }

        public bool GetProgressOrientation(string mapTitle, ref bool bIsContentOrientated)
        {
            if (!m_dMaps.Values.Where(x => x.title == mapTitle).Any())
                return false;
            LearnmapItem map = (LearnmapItem)m_dMaps[mapTitle];
            if (map != null)
            {
                bIsContentOrientated = !map.showProgressQuestionOrientated;
                return true;
            }
            return false;
        }

        public bool GetProgressOrientation(int mapId, ref bool bIsContentOrientated)
        {
            string title;
            GetTitle(mapId, out title);
            if (title != null)
                return GetProgressOrientation(title, ref bIsContentOrientated);
            return false;
        }

        public bool SetProgressOrientation(string mapTitle, bool bIsContentOrientated)
        {
            LearnmapItem map = GetLearnmap(mapTitle);
            if (map == null)
                return false;

            map.showProgressQuestionOrientated = !bIsContentOrientated;
            map.IsModified = true;
            LearnmapManagerEventArgs ea = new LearnmapManagerEventArgs(LearnmapManagerEventArgs.CommandType.ChangeUsage, mapTitle, 0);
            m_parent?.FireEvent(ref ea);
            return true;
        }

        private LearnmapItem GetLearnmap(string strMapTitle)
        {
            if (!m_dMaps.Values.Where(x => x.title == strMapTitle).Any())
                return null;
            return m_dMaps[strMapTitle];
        }
    }
}
