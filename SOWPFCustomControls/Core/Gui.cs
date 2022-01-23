using mshtml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.SOWPFCustomControls
{
    public class Gui
    {
        private WPFWebBrowser webBrowser;
        private HtmlEditor htmlEditor;

        public Format Format
        {
            get { return webBrowser.Format; }
        }

        public Gui(WPFWebBrowser webBrowser, HtmlEditor htmlEditor)
        {
            this.webBrowser = webBrowser;
            this.htmlEditor = htmlEditor;
        }

        public static List<Items> RibbonComboboxFormatInitionalisation()
        {
            List<Items> list = new List<Items>();
            list.Add(new Items("<p>", "Paragraph"));
            list.Add(new Items("<h1>", "Heading 1"));
            list.Add(new Items("<h2>", "Heading 2"));
            list.Add(new Items("<h3>", "Heading 3"));
            list.Add(new Items("<h4>", "Heading 4"));
            list.Add(new Items("<h5>", "Heading 5"));
            list.Add(new Items("<h6>", "Heading 6"));
            list.Add(new Items("<address>", "Adress"));
            list.Add(new Items("<pre>", "Preformat"));
            return list;
        }

        public static List<string> RibbonComboboxFontSizeInitialisation()
        {
            List<string> items= new List<string>();

            for (int x = 1; x <= 7; x++)
            {
                items.Add(x.ToString());
            }
            return items;
        }

        public void SettingsFontColor()
        {
            webBrowser.doc = webBrowser.WebBrowser.Document as HTMLDocument;
            if (webBrowser.doc != null)
            {
                System.Windows.Media.Color col = DialogBox.Pick();
                string colorstr = string.Format("#{0:X2}{1:X2}{2:X2}", col.R, col.G, col.B);
                webBrowser.doc.execCommand("ForeColor", false, colorstr);
            }
        }

        public void SettingsBackColor()
        {
            webBrowser.doc = webBrowser.WebBrowser.Document as HTMLDocument;
            if (webBrowser.doc != null)
            {
                System.Windows.Media.Color col = DialogBox.Pick();
                string colorstr = string.Format("#{0:X2}{1:X2}{2:X2}", col.R, col.G, col.B);
                webBrowser.doc.body.style.background = colorstr;
            }
        }

        public void SettingsAddLink()
        {
            using (Link link = new Link(webBrowser.doc))
            {
                link.ShowDialog();
            }
        }

        public void SettingsAddImage()
        {
            using (Image image = new Image(webBrowser.doc))
            {
                image.ShowDialog();
            }

        }

        public void RibbonButtonSave(ref PageItem pageItem)
        {
            try
            {
	            var doc = webBrowser.doc;
	            if (pageItem != null)
	            {
	                int actCnt = pageItem.PageActions.Length;
	                for (int i = 0; i < actCnt; ++i)
	                {
	                    ActionItem item = pageItem.PageActions[i];
	                    if (item != null)
	                    {
	                        // Text
	                        if (item is TextActionItem)
	                        {
	                            TextActionItem txtItem = (TextActionItem)item;
	                            var elem = doc.getElementById(txtItem.id);
                                var divelem = doc.getElementById("div" + txtItem.id);
	                            if (elem == null || (elem.innerHTML == null || elem.innerText == null))
	                                elem = divelem;
                                if (elem != null && elem.innerHTML != null)
	                            {
	                                String strText = elem.innerHTML;
                                    strText = strText.Trim();
                                    if (strText.Length>0)
                                        txtItem.text = strText.Replace("<BR>", "{{+LF+}}"); ;
	                                if (divelem != null && divelem.style!=null)
	                                {
	                                    int iWidth = divelem.style.pixelWidth;
	                                    int iHeight = divelem.style.pixelHeight;
	                                    txtItem.customWidth = iWidth;
	                                    txtItem.customHeight = iHeight;
                                        Debug.WriteLine(String.Format("{0},{1}",iWidth,iHeight));
	                                }
	                            }
	                        }
	                    }
	                }
	            }
	            else
	            {
	                var htmlText = doc.documentElement.innerHTML;
	                string path = DialogBox.SaveFile();
	                if (path != "")
	                {
	                    //File.WriteAllText(DialogBox.SaveFile(), htmlText);
	                    FileStream fs = new FileStream(path, FileMode.Create);
	                    using (var writer = new StreamWriter(fs, Encoding.UTF8))
	                        writer.Write(htmlText);
	                }
	            }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Fehler beim Speichern. Bitte Support kontaktieren!");
            }
        }

        public void RibbonComboboxFonts(ComboBox RibbonComboboxFonts)
        {
            var doc = webBrowser.WebBrowser.Document as HTMLDocument;
            if (doc != null)
            {
                doc.execCommand("FontName", false, RibbonComboboxFonts.SelectedItem.ToString());
            }
        }

        public void RibbonComboboxFontHeight(ComboBox RibbonComboboxFontHeight)
        {
            IHTMLDocument2 doc = webBrowser.WebBrowser.Document as IHTMLDocument2;
            if (doc != null)
            {
                doc.execCommand("FontSize", false, RibbonComboboxFontHeight.SelectedItem);
            }
        }

        public  void RibbonComboboxFormat(ComboBox RibbonComboboxFormat)
        {
            string ID = ((Items)(RibbonComboboxFormat.SelectedItem)).Value;

            webBrowser.doc = webBrowser.WebBrowser.Document as HTMLDocument;
            if (webBrowser.doc != null)
            {
                webBrowser.doc.execCommand("FormatBlock", false, ID);
            }
        }

        public  void EditWeb()
        {
            if (webBrowser.Visibility == Visibility.Visible) return;
            htmlEditor.Visibility = Visibility.Hidden;
            webBrowser.Visibility = Visibility.Visible;
            htmlEditor.Editor.SelectAll();

            webBrowser.doc.body.innerHTML = htmlEditor.Editor.Selection.Text;

        }

        public  void ViewHTML()
        {
            if (htmlEditor.Visibility == Visibility.Visible) return;
            htmlEditor.Visibility = Visibility.Visible;
            webBrowser.Visibility = Visibility.Hidden;

            htmlEditor.Editor.Selection.Text = webBrowser.doc.documentElement.innerHTML;
        }

        public  void newdocument()
        {
            webBrowser.newWb("",null);
        }

        public  void newdocumentSelectFile()
        {
            webBrowser.newWb(DialogBox.SelectFile(),null);    
        }

        public  void newdocumentFile(string strFileName,PageItem pageItem)
        {
            webBrowser.newWb(strFileName,pageItem);
        }
    }
}
