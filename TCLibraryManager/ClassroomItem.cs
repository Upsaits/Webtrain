using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class ClassroomItem : ICloneable
    {
        [XmlElementAttribute(ElementName="ClassName",IsNullable = false)]
        public string className;
        [XmlArrayAttribute(ElementName="Learnmaps")]
        public string[] aLearnmaps;
        [XmlArrayAttribute(ElementName="Users",IsNullable = true)]
        public string[] aUsers;
        [XmlAttributeAttribute(AttributeName="UseClassMaps")]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool useClassMaps;

        public ClassroomItem()
        {
            className = "";
            aLearnmaps = null;
            aUsers = null;
            useClassMaps = true;
        }

        public ClassroomItem(string _className,bool _useClassMaps)
        {
            className = _className;
            aLearnmaps = null;
            aUsers = null;
            useClassMaps = _useClassMaps;
        }

        public object Clone()
        {
            ClassroomItem newItem=new ClassroomItem(className,useClassMaps);
            if (aLearnmaps != null)
                newItem.aLearnmaps = (string[])aLearnmaps.Clone();
            if (aUsers != null)
                newItem.aUsers = (string[])aUsers.Clone();
            return newItem;
        }

        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value; }
        }
    }
}
