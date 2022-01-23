using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    /// <summary>
    /// Summary description for LibraryManager.
    /// </summary>
   [XmlRootAttribute("Library", Namespace = "", IsNullable = false)]
    public class LibraryItem : ICloneable
    {
        [XmlArrayItemAttribute("BookItem", IsNullable = false)]
        public BookItem[] Books;
        [XmlAttributeAttribute()] 
        public string title;
        [XmlAttributeAttribute()] 
        public bool useBinaryMask;
        [XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool isLocal;
        [XmlAttributeAttribute()]
        public string version;

        public LibraryItem()
        {
            title = "";
            useBinaryMask = false;
            isLocal = false;
            version = "1.0.0.0";
        }

        public LibraryItem(string _title, bool _useBinaryMask,bool _isLocal)
        {
            title = _title;
            useBinaryMask = _useBinaryMask;
            isLocal = _isLocal;
        }

        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value; }
        }

        public Object Clone()
        {
            LibraryItem lib = new LibraryItem(title, useBinaryMask, isLocal) { Books = (BookItem[])Books.Clone() };
            return lib;
        }
    }
}
