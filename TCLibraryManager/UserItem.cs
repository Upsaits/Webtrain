using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class UserItem : ICloneable
    {
        [XmlElementAttribute(IsNullable = false)]
        public string fullName;
        [XmlElementAttribute(IsNullable = false)]
        public string userName;
        [XmlElementAttribute(IsNullable = false)]
        public string password;
        [XmlElementAttribute(IsNullable = false)]
        public UserRights userRights;
        [XmlElementAttribute(IsNullable = false)]
        public int imgId;
        [XmlElementAttribute(IsNullable = false)]
        public bool isActive;

        public UserItem()
        {
            fullName = "";
            userName = "";
            password = "";
            userRights = new UserRights();
            imgId = 0;
            isActive = true;
        }

        public UserItem(string _fullName, string _userName, string _password, UserRights _userRights, int _imgId,bool _isActive)
        {
            fullName = _fullName;
            userName = _userName;
            password = _password;
            isActive = _isActive;
            userRights = _userRights;
            imgId = _imgId;
        }

        public Object Clone()
        {
            return new UserItem(fullName, userName, password, (UserRights)userRights.Clone(), imgId,isActive);
        }
    }
}
