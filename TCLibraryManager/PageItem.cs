using System;
using System.Xml.Serialization;
using System.Collections;

namespace SoftObject.TrainConcept.Libraries
{
    /// <remarks/>
    public class PageItem : ICloneable
    {
        [XmlArrayItemAttribute("ActionItem", typeof(ActionItem), IsNullable = false),
        XmlArrayItemAttribute("TextActionItem", typeof(TextActionItem), IsNullable = false),
        XmlArrayItemAttribute("ImageActionItem", typeof(ImageActionItem), IsNullable = false),
        XmlArrayItemAttribute("KeywordActionItem", typeof(KeywordActionItem), IsNullable = false),
        XmlArrayItemAttribute("AnimationActionItem", typeof(AnimationActionItem), IsNullable = false),
        XmlArrayItemAttribute("VideoActionItem", typeof(VideoActionItem), IsNullable = false),
        XmlArrayItemAttribute("SimulationActionItem", typeof(SimulationActionItem), IsNullable = false),
        XmlArrayItemAttribute("DocumentActionItem", typeof(DocumentActionItem), IsNullable = false)]
        public ActionItem[] PageActions;
        [XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool isLocal;

        [XmlAttributeAttribute()]
        public string templateName;

        public PageItem()
        {
            templateName = "";
            isLocal = false;
        }

        public PageItem(string _templateName, bool _isLocal)
        {
            templateName = _templateName;
            isLocal = _isLocal;
        }

        public Object Clone()
        {
            PageItem page = new PageItem(templateName,isLocal) { PageActions = (ActionItem[])PageActions.Clone() };
            return page;
        }

        public ArrayList GetImageActions()
        {
            ArrayList aImgIds = new ArrayList();

            foreach (ActionItem item in PageActions)
                if (item is ImageActionItem)
                    aImgIds.Add(item);
            return aImgIds;
        }
    }
}
