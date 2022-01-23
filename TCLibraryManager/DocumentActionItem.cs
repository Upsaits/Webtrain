using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept.Libraries
{
    public class DocumentActionItem  : ActionItem
    {
        [XmlAttributeAttribute()]
        public string fileName;
        [XmlAttributeAttribute(DataType = "int")]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public int typeId; // 0..rtf,1..pdf,2..ppt(pptx)

        public DocumentActionItem()
        {
            fileName = "";
            typeId = 0;
        }

        public DocumentActionItem(string id, string _fileName, int _typeId, bool _isLocal)
            : base(id,_isLocal)
        {
            fileName = _fileName;
            typeId = _typeId;
        }

        public override Object Clone()
        {
            return new DocumentActionItem(id, fileName, typeId, isLocal);
        }
    }
}
