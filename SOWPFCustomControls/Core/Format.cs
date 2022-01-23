using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using mshtml;

namespace SoftObject.TrainConcept.SOWPFCustomControls
{
    public class Format
    {
        public HTMLDocument doc;
        public WebBrowser webBrowser;

        public Format(HTMLDocument doc, WebBrowser webBrowser)
        {
            this.doc = doc;
            this.webBrowser = webBrowser;
        }

        public void bold()
        {
            if (doc != null)
            {
                doc.execCommand("Bold", false, null);
            }
        }

        public void Italic()
        {
            if (doc != null)
            {
                doc.execCommand("Italic", false, null);
            }
        }

        public void Underline()
        {
            if (doc != null)
            {
                doc.execCommand("Underline", false, null);
            }
        }

        public void JustifyLeft()
        {
            if (doc != null)
            {
                doc.execCommand("JustifyLeft", false, null);
            }
        }

        public void JustifyCenter()
        {
            if (doc != null)
            {
                doc.execCommand("JustifyCenter", false, null);
            }
        }

        public void JustifyRight()
        {
            if (doc != null)
            {
                doc.execCommand("JustifyRight", false, null);
            }
        }

        public void JustifyFull()
        {
            if (doc != null)
            {
                doc.execCommand("JustifyFull", false, null);
            }
        }


        public void InsertOrderedList()
        {
            if (doc != null)
            {
                doc.execCommand("InsertOrderedList", false, null);
            }
        }

        public void InsertUnorderedList()
        {
            if (doc != null)
            {
                doc.execCommand("InsertUnorderedList", false, null);
            }
        }

        public void Outdent()
        {
            if (doc != null)
            {
                doc.execCommand("Outdent", false, null);
            }
        }

        public void Indent()
        {
            if (doc != null)
            {
                doc.execCommand("Indent", false, null);
            }
        }

        public void InsertText(string strText)
        {
            if (doc != null)
            {
                //doc.execCommand("insertHTML", false, strText);
                doc.activeElement.innerText += strText;
            }
        }


    }
}
