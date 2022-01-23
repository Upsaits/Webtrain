using System;
using System.Globalization;

namespace SoftObject.TrainConcept.Libraries
{

    [Serializable]
    public class TestResultItem : ICloneable
    {
        public int version=1;
        public string userName;
        public string mapName;
        public string testName;
        public DateTime startTime;
        public DateTime endTime;
        public bool isWorkedOut;
        public double percRight;
        public TestQuestionResultItem[] aTestQuestionResults;

        public TestResultItem(string _userName, string _mapName, string _testName,DateTime _startTime)
        {
            userName = _userName;
            mapName = _mapName;
            testName = _testName;
            startTime = _startTime;
            endTime = _startTime;
            percRight = 0;
            aTestQuestionResults = null;
        }

        public TestResultItem(string _userName, string _mapName, string _testName, DateTime _startTime, DateTime _endTime,
                              double _percRight)
        {
            userName = _userName;
            mapName = _mapName;
            testName = _testName;
            startTime = _startTime;
            endTime = _startTime;
            percRight = _percRight;
            aTestQuestionResults = null;
        }

        public void Workout(DateTime _endTime, TestQuestionResultItem[] _aTestQuestionResults, double _percRight)
        {
            endTime = _endTime;
            aTestQuestionResults = _aTestQuestionResults;
            percRight = _percRight;
            isWorkedOut = true;
        }

        public Object Clone()
        {
            TestResultItem item = new TestResultItem(userName, mapName,testName,startTime, endTime, percRight);
            if (aTestQuestionResults != null)
                item.aTestQuestionResults = (TestQuestionResultItem[])aTestQuestionResults.Clone();
            item.isWorkedOut = isWorkedOut;
            return item;
        }

        public override string ToString()
        {
            string sCmd = String.Format("{0};{1};{2};{3};{4};{5}", userName, mapName, testName, startTime.ToString(new CultureInfo("en-US")), endTime.ToString(new CultureInfo("en-US")), percRight.ToString());
            string sQuResults = "0;";
            if (aTestQuestionResults != null)
            {
                sQuResults = String.Format("{0};", aTestQuestionResults.Length);
                for (int i = 0; i < aTestQuestionResults.Length; ++i)
                {
                    if (i < aTestQuestionResults.Length - 1)
                        sQuResults = sQuResults + aTestQuestionResults[i].ToString() + ';';
                    else
                        sQuResults = sQuResults + aTestQuestionResults[i].ToString();
                }
            }
            return sCmd + ';' + sQuResults;
        }
    }
}
