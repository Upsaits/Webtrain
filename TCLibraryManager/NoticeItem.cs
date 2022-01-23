using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class NoticeItem : ICloneable
    {
        [XmlElementAttribute(IsNullable = false)]
        public string userName;
        [XmlElementAttribute(IsNullable = false)]
        public string contentPath;
        [XmlElementAttribute(IsNullable = false)]
        public int pageId;
        [XmlElementAttribute(IsNullable = false)]
        public string title;
        [XmlElementAttribute(IsNullable = false)]
        public string fileName;
        [XmlElementAttribute(IsNullable = false)]
        public int workedOutState;
        [XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(0)]
        public int dirtyFlag;
        [XmlIgnore]
        public DateTime ModificationDate { get; set; }
        //[XmlElementAttribute(IsNullable = true)]
        [XmlElement("ModificationDate")]
        public string ModificationDateString
        {
            get { return this.ModificationDate.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { this.ModificationDate = DateTime.Parse(value); }
        }
        public NoticeItem()
        {
            userName = "";
            contentPath = "";
            pageId = 0;
            title = "";
            fileName = "";
            workedOutState = -1;
            dirtyFlag = 0;
            ModificationDate = DateTime.Now;
        }

        public NoticeItem(string _userName, string _contentPath, int _pageId, string _title, string _fileName, DateTime modificationDate, int _workedOutState=-1,int _dirtyFlag=1)
        {
            userName = _userName;
            contentPath = _contentPath;
            pageId = _pageId;
            title = _title;
            fileName = _fileName;
            workedOutState = _workedOutState;
            dirtyFlag = _dirtyFlag;
            ModificationDate = modificationDate;
        }

        public Object Clone()
        {
            return new NoticeItem(userName, contentPath, pageId, title, fileName,ModificationDate,workedOutState, dirtyFlag);
        }
    }
}
