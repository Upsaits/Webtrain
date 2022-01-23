using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class AnimationActionItem : ActionItem
    {
        [XmlAttributeAttribute()]
        public string fileName1;
        [XmlAttributeAttribute()]
        public string fileName2;
        [XmlAttributeAttribute()]
        public bool showDirect;
        [XmlIgnoreAttribute()]
        public bool showDirectSpecified;

        public AnimationActionItem()
        {
            fileName1 = "";
            fileName2 = "";
            showDirect = false;
            showDirectSpecified = true;
        }

        public AnimationActionItem(string id, string _fileName1, string _fileName2,
            bool _showDirect,bool _isLocal)
            : base(id,_isLocal)
        {
            fileName1 = _fileName1;
            fileName2 = _fileName2;
            showDirect = _showDirect;
            showDirectSpecified = true;
        }

        public override Object Clone()
        {
            return new AnimationActionItem(id, fileName1, fileName2, showDirect,isLocal);
        }
    }
}
