using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    /// <remarks/>
    public class BookItem : ICloneable
    {
        [XmlArrayItemAttribute("ChapterItem", IsNullable = false)]
        public ChapterItem[] Chapters;
        [XmlAttributeAttribute()]
        public string title;
        [XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool isLocal;

        public BookItem()
        {
            title = "";
            isLocal = false;
        }

        public BookItem(string _title,bool _isLocal)
        {
            title = _title;
            isLocal = _isLocal;
        }

        public Object Clone()
        {
            BookItem book = new BookItem(title,isLocal) { Chapters = (ChapterItem[])Chapters.Clone() };
            return book;
        }
    }
}
