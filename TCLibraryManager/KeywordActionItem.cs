using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class KeywordActionItem : ActionItem
    {
        [XmlAttributeAttribute()]
        public string text;
        [XmlAttributeAttribute()]
        public string description;
        [XmlAttributeAttribute()]
        public bool isTooltip;
        [XmlIgnoreAttribute()]
        public bool isTooltipSpecified;
        [XmlAttributeAttribute()]
        public bool isGlossar;
        [XmlIgnoreAttribute()]
        public bool isGlossarSpecified;

        public KeywordActionItem()
        {
            text = "";
            description = "";
            isTooltip = true;
            isGlossar = true;
            isTooltipSpecified = true;
            isGlossarSpecified = true;
        }

        public KeywordActionItem(string id, string _text, string _description, bool _isTooltip, bool _isGlossar,bool _isLocal)
            : base(id,_isLocal)
        {
            text = _text;
            description = _description;
            isTooltip = _isTooltip;
            isGlossar = _isGlossar;
            isTooltipSpecified = true;
            isGlossarSpecified = true;
        }

        public override Object Clone()
        {
            return new KeywordActionItem(id, text, description, isTooltip, isGlossar,isLocal);
        }
    }
}
