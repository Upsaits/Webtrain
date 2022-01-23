using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    /// <summary>
    /// Summary description for UserControl.
    /// </summary>
    [XmlRootAttribute("NoticeList", Namespace = "http://www.cpandl.com", IsNullable = false)]
    public class NoticeList : ICloneable
    {
        [XmlArrayAttribute("Notices")]
        public NoticeItem[] Notices;

        public Object Clone()
        {
            NoticeList nl = new NoticeList();
            nl.Notices = (NoticeItem[])Notices.Clone();
            return nl;
        }
    }
}
