using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    /// <summary>
    /// Summary description for LearnmapManager.
    /// </summary>
    [XmlRootAttribute("Learnmap", Namespace = "http://www.cpandl.com", IsNullable = false)]
    public class LearnmapItem : ICloneable
    {
        [XmlElementAttribute(IsNullable = false)]
        public string title;
        [XmlElementAttribute(IsNullable = true)]
        public string color;
        [XmlArrayAttribute("Workings")]
        public WorkingItem[] Workings;
        [XmlArrayAttribute("Users")]
        public string[] Users;
        [XmlArrayAttribute("Tests")]
        public TestItem[] Tests;
        [XmlAttributeAttribute("IsClassMap")]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool isClassMap;
        [XmlAttributeAttribute("ShowProgressQuestionOrientated")]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool showProgressQuestionOrientated;
        [XmlAttributeAttribute("IsServerMap")]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool isServerMap;


        public LearnmapItem()
        {
            title = "";
            Workings = null;
            Users = null;
            isClassMap = false;
            showProgressQuestionOrientated = false;
            isServerMap = false;
        }

        public LearnmapItem(string _title, string _color, bool _isClassMap,bool _showProgressQuestionOrientated, bool _bIsServerMap)
        {
            title = _title;
            color = _color;
            isClassMap = _isClassMap;
            showProgressQuestionOrientated = _showProgressQuestionOrientated;
            isServerMap = _bIsServerMap;
        }

        public Object Clone()
        {
            LearnmapItem map = new LearnmapItem(title, color,isClassMap,showProgressQuestionOrientated,isServerMap);
            if (Workings != null)
                map.Workings = (WorkingItem[])Workings.Clone();
            if (Users != null)
                map.Users = (string[])Users.Clone();
            if (Tests != null)
                map.Tests = (TestItem[])Tests.Clone();
            return map;
        }

        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value; }
        }
    }
}
