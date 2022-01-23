using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    /// <summary>
    /// Summary description for UserControl.
    /// </summary>
    [XmlRootAttribute("UserList", Namespace = "http://www.cpandl.com", IsNullable = false)]
    public class UserList : ICloneable
    {
        [XmlArrayAttribute("Users")]
        public UserItem[] Users;
        [XmlElementAttribute(IsNullable = false)]
        public bool isLocal;

        public UserList()
        {
            isLocal = true;
        }

        public Object Clone()
        {
            UserList ul = new UserList();
            ul.Users = (UserItem[])Users.Clone();
            ul.isLocal = isLocal;
            return ul;
        }
    }
}
