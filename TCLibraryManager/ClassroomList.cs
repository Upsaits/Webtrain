using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    [XmlRootAttribute("ClassroomList", Namespace = "http://www.cpandl.com", IsNullable = false)]
    public class ClassroomList : ICloneable
    {
        [XmlArrayAttribute("Classrooms")]
        public ClassroomItem[] aClassrooms;

        public ClassroomList()
        {
        }

        public object Clone()
        {
            ClassroomList ul = new ClassroomList();
            ul.aClassrooms = (ClassroomItem[])aClassrooms.Clone();
            return ul;
        }
    }
}
