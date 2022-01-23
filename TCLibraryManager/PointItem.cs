using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    /// <remarks/>
    public class PointItem : ICloneable
    {
        [XmlArrayItemAttribute("PageItem", IsNullable = false)]
        public PageItem[] Pages;
        [XmlArrayItemAttribute("QuestionItem", IsNullable = false)]
        public QuestionItem[] Questions;
        [XmlAttributeAttribute()]
        public string title;
        [XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool isLocal;

        public PointItem()
        {
            title = "";
            isLocal = false;
        }

        public PointItem(string _title, bool _isLocal)
        {
            title = _title;
            isLocal = _isLocal;
        }

        public Object Clone()
        {
            PointItem poi = new PointItem(title,isLocal) { Pages = (PageItem[])Pages.Clone(), Questions = (QuestionItem[])Questions.Clone() };
            return poi;
        }
    }
}
