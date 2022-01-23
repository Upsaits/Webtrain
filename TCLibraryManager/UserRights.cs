using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class UserRights : ICloneable
    {
        [XmlElementAttribute(IsNullable = false)]
        public bool isAdministrator;
        [XmlElementAttribute(IsNullable = false)]
        public bool isTeacher;

        public UserRights()
        {
            isAdministrator = false;
            isTeacher = false;
        }

        public UserRights(bool _isAdministrator, bool _isTeacher)
        {
            isAdministrator = _isAdministrator;
            isTeacher = _isTeacher;
        }

        public Object Clone()
        {
            return new UserRights(isAdministrator, isTeacher);
        }
    }
}
