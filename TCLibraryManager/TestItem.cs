using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class TestItem : ICloneable
    {
        [XmlElementAttribute(IsNullable = false)]
        public string title;
        [XmlElementAttribute(IsNullable = false)]
        public bool randomChoose;
        [XmlElementAttribute(IsNullable = false)]
        public int questionCount;
        [XmlElementAttribute(IsNullable = false)]
        public int trialCount;
        [XmlElementAttribute(IsNullable = false)]
        public int successLevel;
        [XmlElementAttribute(IsNullable = true)]
        public string type;
        [XmlElementAttribute(IsNullable = true)]
        public string questionnare;
        [XmlElementAttribute(IsNullable = false)]
        public bool testAlwaysAllowed;
        [XmlArrayAttribute("TestQuestions")]
        public TestQuestionItem[] TestQuestions;

        public TestItem()
        {
            title = "";
            randomChoose = true;
            questionCount = 10;
            trialCount = 1;
            successLevel = 85;
            type="";
            questionnare = "";
            testAlwaysAllowed = false;
        }

        public TestItem(string _title,bool _randomChoose, int _questionCount, int _trialCount, int _successLevel, string _type, string _questionnare, bool _testAlwaysAllowed)
        {
            title = _title;
            randomChoose = _randomChoose;
            questionCount = _questionCount;
            trialCount = _trialCount;
            successLevel = _successLevel;
            type=_type;
            questionnare = _questionnare;
            testAlwaysAllowed = _testAlwaysAllowed;
        }

        public Object Clone()
        {
            TestItem item = new TestItem(title, randomChoose, questionCount, trialCount, successLevel, type, questionnare,testAlwaysAllowed);
            if (TestQuestions != null)
                item.TestQuestions = (TestQuestionItem[])TestQuestions.Clone();
            return item;
        }
    }
}
