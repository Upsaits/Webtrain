using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class ActionItem : ICloneable, IComparable
    {
        [XmlAttributeAttribute()]
        public string id;
        [XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool isLocal;

        public ActionItem()
        {
            id = "";
            isLocal = false;
        }

        public ActionItem(string _id,bool _isLocal)
        {
            id = _id;
            isLocal = _isLocal;
        }

        public virtual Object Clone()
        {
            return new ActionItem(id,isLocal);
        }

        public virtual void CopyFrom(object obj)
        {
            id = (obj as ActionItem).id;
        }

        public Int32 CompareTo(Object obj)
        {
            return String.Compare(id, ((ActionItem)obj).id, true);
        }

        public virtual void OnLoad(LibraryItem _lib,BookItem _book, ChapterItem _chp, PointItem _poi)
        {
        }
    }
}
