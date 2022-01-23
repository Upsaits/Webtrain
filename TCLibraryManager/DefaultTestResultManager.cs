using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;

namespace SoftObject.TrainConcept.Libraries
{
	/// <summary>
	/// Zusammendfassende Beschreibung für DefTestResultManager.
	/// </summary>
	public class DefaultTestResultManager : ITestResultManager
	{
		private TestResultItemCollection aTestResults=new TestResultItemCollection();
		private string m_fileName="";
		private TestResultManagerBridge m_parent=null;

		public DefaultTestResultManager()
		{
		}

		public void SetParent(object parent)
		{
			m_parent = (TestResultManagerBridge) parent;
		}

		public bool Open(string fileName)
		{
			m_fileName = fileName;
			if (!File.Exists(fileName))
				return false;

			try
			{
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				aTestResults = (TestResultItemCollection) formatter.Deserialize(stream);
				stream.Close();
			}
			catch(Exception)
			{
				return false;
			}

		    for (int i = 0; i < aTestResults.Count; ++i)
		    {
		        TestResultItem item = aTestResults.Item(i);
		        if (item.testName == null)
                    item.testName = Utilities.c_strDefaultFinalTest;
		    }

		    return true;
		}

		public int Start(string userName,string mapName,string testName,DateTime startTime)
		{
			// Neues Testergebnis anlegen
			TestResultItem resultItem=new TestResultItem(userName,mapName,testName,startTime);
			aTestResults.Add(resultItem);

			Save();

			m_parent.FireEvent(EventArgs.Empty);

			return aTestResults.Count-1;
		}
		
		public int End(string userName,string mapName,string testName,DateTime endTime,WorkoutInfoItemCollection aWorkouts)
		{
			// Unausgearbeitet Tests vorhanden?
			TestResultItemCollection aResults;
			if (Find(out aResults,userName,mapName,testName,false) == 0)
				return -1; // Keine vorhanden->nicht zulassen

			TestResultItem ri=aResults.Item(aResults.Count-1);
			if (ri.isWorkedOut || aWorkouts.Count==0)
				return -1;

			int workedOut=0,right=0,wrong=0;
			aWorkouts.GetWorkoutResult(ref workedOut,ref right,ref wrong);

			TestQuestionResultItem [] aQuResults=new TestQuestionResultItem[aWorkouts.Count];
            for (int i = 0; i < aWorkouts.Count; ++i)
            {
                var woi = aWorkouts.Item(i);
                aQuResults.SetValue(new TestQuestionResultItem(woi.Path, woi.Id, woi.ActCorrMask, woi.Question.type, woi.QuizAnswers, woi.QuizResult), i);
            }

			ri.Workout(endTime,aQuResults,(double)(right*100/aWorkouts.Count));

			Save();

			m_parent.FireEvent(EventArgs.Empty);

			return aTestResults.Count-1;
		}

		public int End(TestResultItem resultItem)
		{
            // Unausgearbeitet Tests vorhanden?
            TestResultItemCollection aResults;
		    TestResultItem tri = null;
		    if (Find(out aResults, resultItem.userName, resultItem.mapName, resultItem.testName, false) == 0)
		    {
		        int newId=Start(resultItem.userName, resultItem.mapName, resultItem.testName, resultItem.startTime);
		        tri = aTestResults.Item(newId);
		    }
		    else
		    {
                tri = aResults.Item(aResults.Count - 1);
		    }

            tri.Workout(resultItem.endTime,resultItem.aTestQuestionResults,resultItem.percRight);

		    Save();

			m_parent.FireEvent(EventArgs.Empty);

			return aTestResults.Count-1;
		}

		public TestResultItem Get(int id)
		{
			if (id<aTestResults.Count)
				return aTestResults.Item(id);
			return null;
		}


        public TestResultItem Get(string mapName, int id)
        {
            int found = 0;
            for (int i = 0; i < aTestResults.Count; ++i)
            {
                TestResultItem item = aTestResults.Item(i);
                if (item.mapName == mapName)
                    if (found++ == id)
                        return aTestResults.Item(i);
            }

            return null;
        }


		// Testergebnisse holen: Username,Testmappe und Aus-oder Unausgearbeitete filtern
		public int Find(out TestResultItemCollection aCollection,string userName,string mapName,string testName,bool bGetOnlyWorkedout)
		{
            aCollection = new TestResultItemCollection();
			for(int i=0;i<aTestResults.Count;++i)
			{
				TestResultItem item = aTestResults.Item(i);
				if (item.userName == userName && item.mapName == mapName && item.testName==testName)
				{
					if (bGetOnlyWorkedout)
					{
                        if (item.aTestQuestionResults != null && item.isWorkedOut)
							aCollection.Add(item);
					}
					else
					{
                        if (item.aTestQuestionResults == null)
                            aCollection.Add(item);
					}
				}
			}
			return aCollection.Count;
		}

		// Testergebnisse holen: Username und Testmappe filtern
        public int Find(ref TestResultItemCollection aCollection, string userName, string mapName, string testName)
		{
			for(int i=0;i<aTestResults.Count;++i)
			{
				TestResultItem item = aTestResults.Item(i);
                if (item.userName == userName && item.mapName == mapName && item.testName == testName)
					aCollection.Add(item);
			}
			return aCollection!=null ? aCollection.Count:0;
		}

        // Testergebnisse holen: Testmappe filtern
        public int Find(ref TestResultItemCollection aCollection, string userName, string mapName)
        {
            for (int i = 0; i < aTestResults.Count; ++i)
            {
                TestResultItem item = aTestResults.Item(i);
                if (item.userName==userName && item.mapName == mapName)
                    aCollection.Add(item);
            }
            return aCollection.Count;
        }


		// Testergebnisse holen: Testmappe filtern
		public int Find(ref TestResultItemCollection aCollection,string mapName)
		{
			for(int i=0;i<aTestResults.Count;++i)
			{
				TestResultItem item = aTestResults.Item(i);
				if (item.mapName == mapName)
					aCollection.Add(item);
			}
			return aCollection.Count;
		}

		// Bestimmtes Testergebnis löschen: Username, Testmappe und Testname filtern
        public void Delete(string userName, string mapName, string testName, int id)
		{
			int found=0;
			for(int i=0;i<aTestResults.Count;++i)
			{
				TestResultItem item = aTestResults.Item(i);
                if (item.userName == userName && item.mapName == mapName && item.testName == testName)
				{
					if (found++ == id)
					{
						aTestResults.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Bestimmtes Testergebnis löschen: Testmappe filtern
		public void Delete(string mapName,int id)
		{
			int found=0;
			for(int i=0;i<aTestResults.Count;++i)
			{
				TestResultItem item = aTestResults.Item(i);
				if (item.mapName == mapName)
				{
					if (found++ == id)
					{
						aTestResults.RemoveAt(i);
						break;
					}
				}
			}
		}

        // Bestimmtes Testergebnis löschen: Testmappe filtern
        public void Delete(int id)
        {
            Debug.Assert(id >= 0 && id < aTestResults.Count);
            if (id>=0 && id<aTestResults.Count)
                aTestResults.RemoveAt(id);
        }


		public void Save()
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(m_fileName, FileMode.Create, FileAccess.Write, FileShare.None);
			formatter.Serialize(stream, aTestResults);
			stream.Close();		
		}

		public TestResultItem CreateResultItem(string sStr)
		{
			string[] aTokens = sStr.Split(new char[] {';'},8);

			if (aTokens.Length!=8)
				return null;

            DateTime dtFrom;
            DateTime dtTo;

            if (!DateTime.TryParse(aTokens[3], new CultureInfo("en-US"), DateTimeStyles.None, out dtFrom))
                dtFrom = DateTime.Now;

            if (!DateTime.TryParse(aTokens[4], new CultureInfo("en-US"), DateTimeStyles.None, out dtTo))
                dtTo = DateTime.Now;

			TestResultItem tri = new TestResultItem(aTokens[0],aTokens[1],aTokens[2],
                                                    dtFrom,
                                                    dtTo,
                                                    System.Convert.ToDouble(aTokens[5]));

			int quCnt=System.Convert.ToInt32(aTokens[6]);
			if (quCnt>0)
			{
				string[] aTokens1=aTokens[7].Split(new Char[] {';'});

                if (aTokens1.Length != quCnt * 5)
                    return null;

				tri.aTestQuestionResults = new TestQuestionResultItem[quCnt];
				for(int i=0;i<quCnt;++i)
				{
					int answerMask=System.Convert.ToInt32(aTokens1[i*5+2]);
					tri.aTestQuestionResults[i] = new TestQuestionResultItem(aTokens1[i*5],
						System.Convert.ToInt32(aTokens1[i*5+1]),
                        new BitArray(new int[] { System.Convert.ToInt32(aTokens1[i * 5 + 2]) }), "", aTokens1[i * 5 + 3], System.Convert.ToInt32(aTokens1[i * 5 + 4])> 0);
                }
			}
			return tri;
		}

        public void SetAdapter(ITestResultAdapter adapter)
        {
            
        }
	}
}
