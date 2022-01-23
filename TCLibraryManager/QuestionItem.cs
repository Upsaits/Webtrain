using System;
using System.Xml.Serialization;
using System.Collections;

namespace SoftObject.TrainConcept.Libraries
{
    public class QuestionItem : ICloneable
    {
        /// <remarks/>
        [XmlArrayItemAttribute("ActionItem", typeof(ActionItem), IsNullable = false),
        XmlArrayItemAttribute("TextActionItem", typeof(TextActionItem), IsNullable = false),
        XmlArrayItemAttribute("ImageActionItem", typeof(ImageActionItem), IsNullable = false),
        XmlArrayItemAttribute("KeywordActionItem", typeof(KeywordActionItem), IsNullable = false),
        XmlArrayItemAttribute("AnimationActionItem", typeof(AnimationActionItem), IsNullable = false),
        XmlArrayItemAttribute("SimulationActionItem", typeof(SimulationActionItem), IsNullable = false)]
        public ActionItem[] QuestionActions;

        [System.Xml.Serialization.XmlElementAttribute("Answer")]
        public string[] Answers;
        //public AnswerItem Answers;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string templateName;
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type;
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string question;
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int correctAnswerMask;
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool useForExaming;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool useForExamingSpecified;
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool useForTesting;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool useForTestingSpecified;
        [XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool isLocal;

        public QuestionItem()
        {
            templateName = "";
            type = "Single";
            question = "";
            correctAnswerMask = 0;
            useForTesting = false;
            useForExaming = false;
            useForExamingSpecified = true;
            useForTestingSpecified = true;
            isLocal = false;
        }

        public QuestionItem(string _templateName, string _type, string _question,
            int _correctAnswerMask, bool _useForTesting, bool _useForExaming,bool _isLocal)
        {
            templateName = _templateName;
            type = _type;
            question = _question;
            correctAnswerMask = _correctAnswerMask;
            useForTesting = _useForTesting;
            useForExaming = _useForExaming;
            useForExamingSpecified = true;
            useForTestingSpecified = true;
            isLocal = _isLocal;
        }

        public Object Clone()
        {
            QuestionItem que = new QuestionItem(templateName, type, question,
                correctAnswerMask, useForTesting,
                useForExaming,isLocal);
            if (Answers!=null)
                que.Answers = (string[])Answers.Clone();
            if (QuestionActions!=null)
                que.QuestionActions = (ActionItem[])QuestionActions.Clone();
            return que;
        }

        public ArrayList GetImageActions()
        {
            ArrayList aImgIds = new ArrayList();
            if (QuestionActions != null)
            {
                foreach (ActionItem item in QuestionActions)
                    if (item is ImageActionItem)
                        aImgIds.Add(item);
            }
            return aImgIds;
        }

    }
}
