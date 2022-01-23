using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    /// <remarks/>
    public class ChapterItem : ICloneable
    {
        [XmlArrayItemAttribute("PointItem", IsNullable = false)]
        public PointItem[] Points;
        [XmlAttributeAttribute()]
        public string title;
        [XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool isLocal;

        public ChapterItem()
        {
            title = "";
            isLocal = false;
        }

        public ChapterItem(string _title,bool _isLocal)
        {
            title = _title;
            isLocal = _isLocal;
        }

        public Object Clone()
        {
            ChapterItem chp = new ChapterItem(title,isLocal) { Points = (PointItem[])Points.Clone() };
            return chp;
        }
    }
}
