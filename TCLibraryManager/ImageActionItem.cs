using System;
using System.Xml.Serialization;

namespace SoftObject.TrainConcept.Libraries
{
    public class ImageActionItem : ActionItem
    {
        [XmlAttributeAttribute()]
        public string fileName;
        [XmlAttributeAttribute(DataType = "int")]
        public int width;
        [XmlAttributeAttribute(DataType = "int")]
        public int height;
        [XmlAttributeAttribute(DataType = "int")]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public int top;
        [XmlAttributeAttribute(DataType = "int")]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public int left;
        [XmlAttributeAttribute(DataType = "int")]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public int origwidth;
        [XmlAttributeAttribute(DataType = "int")]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public int origheight;

        public ImageActionItem()
        {
            fileName = "";
            width = 0;
            height = 0;
            top = 0;
            left = 0;
            origwidth = 0;
            origheight = 0;
        }

        public ImageActionItem(string id, string _fileName, int _top, int _left, int _width, int _height, int _origwidth, int _origheight, bool _isLocal)
            : base(id,_isLocal)
        {
            fileName = _fileName;
            width = _width;
            height = _height;
            origwidth = _origwidth;
            origheight = _origheight;
            top = _top;
            left = _left;
        }

        public override Object Clone()
        {
            return new ImageActionItem(id, fileName, top, left, width, height, origwidth, origheight, isLocal);
        }

        public override void OnLoad(LibraryItem _lib, BookItem _book, ChapterItem _chp, PointItem _poi)
        {
            if (origwidth == 0)
                origwidth = width;
            if (origheight == 0)
                origheight = height;
        }

        public void CopyFrom(ImageActionItem itemFrom)
        {
            fileName = itemFrom.fileName;
            width = itemFrom.width;
            height = itemFrom.height;
            origwidth = itemFrom.origwidth;
            origheight = itemFrom.origheight;
            top = itemFrom.top;
            left = itemFrom.left;
        }

        public override void CopyFrom(object obj)
        {
            base.CopyFrom(obj);
            var _obj = (obj as ImageActionItem);
            fileName = _obj.fileName;
            width = _obj.width;
            height = _obj.height;
            origwidth = _obj.origwidth;
            origheight = _obj.origheight;
            top = _obj.top;
            left = _obj.left;
        }


        public static int DefaultImgWidth = 640;
        public static int DefaultImgHeight= 480;
        public static int DefaultImgPosX  = 370;
        public static int DefaultImgPosY  = 44;
        public static int DefaultImgWidthQu = 295;
        public static int DefaultImgHeightQu= 345;
    }
}
