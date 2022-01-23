using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class VideoActionItem : ActionItem
    {
        [XmlAttributeAttribute()]
        public string fileName;
        [XmlAttributeAttribute()]
        public bool showDirect;
        [XmlIgnoreAttribute()]
        public bool showDirectSpecified;

        public VideoActionItem()
        {
            fileName = "";
            showDirect = false;
            showDirectSpecified = true;
        }

        public VideoActionItem(string id, string _fileName, bool _showDirect,bool _isLocal)
            : base(id,_isLocal)
        {
            fileName = _fileName;
            showDirect = _showDirect;
            showDirectSpecified = true;
        }

        public override Object Clone()
        {
            return new VideoActionItem(id, fileName, showDirect,isLocal);
        }

        public override void OnLoad(LibraryItem _lib, BookItem _book, ChapterItem _chp, PointItem _poi)
        {
        }
    }
}
