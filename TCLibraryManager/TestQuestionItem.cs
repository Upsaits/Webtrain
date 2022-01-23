using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class TestQuestionItem : ICloneable
    {
        [XmlElementAttribute(IsNullable = false)]
        public string contentPath;
        [XmlElementAttribute(IsNullable = false)]
        public int questionId;

        public TestQuestionItem()
        {
            contentPath = "";
            questionId = 0;
        }

        public TestQuestionItem(string _contentPath, int _questionId)
        {
            contentPath = _contentPath;
            questionId = _questionId;
        }

        public Object Clone()
        {
            return new TestQuestionItem(contentPath, questionId);
        }
    }
}
