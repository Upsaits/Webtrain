using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class WorkingItem : ICloneable
    {
        [XmlElementAttribute(IsNullable = true)]
        public string link;
        [XmlElementAttribute(IsNullable = true)]
        public string test;
        [XmlElementAttribute(IsNullable = true)]
        public string version;

        public WorkingItem()
        {
            link = "";
            test = "";
            version = "1.0.0.0";
        }

        public WorkingItem(string _link,string _test,string _version)
        {
            link = _link;
            test = _test;
            version = _version;
        }

        public Object Clone()
        {
            return new WorkingItem(link,test,version);
        }
    }
}
