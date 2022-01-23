using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class TextActionItem : ActionItem
    {
        [XmlAttributeAttribute()]
        public string text;
        [XmlAttributeAttribute(DataType = "int")]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public int customWidth;
        [XmlAttributeAttribute(DataType = "int")]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public int customHeight;


        public TextActionItem()
        {
            text = "";
            customWidth = 0;
            customHeight = 0;
        }

        public TextActionItem(string id, string _text,int _customWidth=0, int _customHeight=0, bool _isLocal=false)
            : base(id,_isLocal)
        {
            text = _text;
            customWidth = _customWidth;
            customHeight = _customHeight;
        }

        public override void CopyFrom(object obj)
        {
            base.CopyFrom(obj);
            text = (obj as TextActionItem).text;
            customWidth = (obj as TextActionItem).customWidth;
            customHeight = (obj as TextActionItem).customHeight;
        }

        public override Object Clone()
        {
            return new TextActionItem(id, text, customWidth, customHeight, isLocal);
        }
    }
}
