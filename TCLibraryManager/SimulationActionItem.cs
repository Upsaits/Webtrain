using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class SimulationActionItem : ActionItem
    {
        [XmlAttributeAttribute()]
        public string fileName1;
        [XmlAttributeAttribute()]
        public string fileName2;
        [XmlAttributeAttribute()]
        public bool isMill;
        [XmlAttributeAttribute()]
        public bool showDirect;
        [XmlIgnoreAttribute()]
        public bool showDirectSpecified;

        public SimulationActionItem()
        {
            fileName1 = "";
            fileName2 = "";
            isMill = true;
            showDirect = false;
            showDirectSpecified = true;
        }

        public SimulationActionItem(string id, string _fileName1, string _fileName2,
            bool _isMill, bool _showDirect,bool _isLocal)
            : base(id,_isLocal)
        {
            fileName1 = _fileName1;
            fileName2 = _fileName2;
            showDirect = _showDirect;
            isMill = _isMill;
            showDirectSpecified = true;
        }

        public override Object Clone()
        {
            return new SimulationActionItem(id, fileName1, fileName2, isMill, showDirect,isLocal);
        }
    }
}
