using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.ComponentModel;
using SoftObject.TrainConcept.Libraries;
using SoftObject.TrainConcept.SOWPFCustomControls;
using System.Windows.Forms.Integration;  

namespace SoftObject.TrainConcept
{
    class ContentEditPageEditor
    {
        private readonly string m_strOrigTemplate;
        private readonly string m_strOrigResult;
        private readonly string m_strTemplatePath;
        private PageItem m_pageItem;

        public ContentEditPageEditor(string strTemplatePath,string strTemplateFilename, PageItem pageItem)
        {
            m_pageItem = pageItem;
            m_strTemplatePath = strTemplatePath;
            m_strOrigTemplate = strTemplatePath+strTemplateFilename;
            m_strOrigResult = Path.GetTempFileName();

            CreateOrigFile();
        }

        private void CreateOrigFile()
        {
            using (var reader = new StreamReader(m_strOrigTemplate, Encoding.UTF8))
            {
                FileStream fs = new FileStream(m_strOrigResult, FileMode.Create);
                using (var writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!ParseCSS(ref line))
                        {
                            ParseTextItems(ref line);
                        }
                        writer.WriteLine(line);
                    } 
                }
            }
        }

        private bool ParseCSS(ref string line)
        {
            int iCssFound = line.IndexOf("style/templstyle.css");
            if (iCssFound >= 0)
            {
                line = line.Replace("style/templstyle.css", m_strTemplatePath + "style/templstyle.css");
                return true;
            }

            return false;
        }

        private bool ParseTextItems(ref string line)
        {
            bool bFound = false;
            int actCnt = m_pageItem.PageActions.Length;
            for (int i = 0; i < actCnt; ++i)
            {
                ActionItem item = m_pageItem.PageActions[i];
                if (item != null)
                {
                    // Text
                    if (item is TextActionItem)
                    {
                        var txtItem = item as TextActionItem;
                        int iItemPos = -1;
                        iItemPos = line.IndexOf(txtItem.id);
                        if (iItemPos >= 0)
                        {
                            int iBracketsPos = -1;
                            string strId = String.Format(">{0}<", txtItem.id);
                            iBracketsPos = line.IndexOf(strId);
                            if (iBracketsPos >= 0)
                            {
                                String strBrowserText = txtItem.text.Replace("{{+LF+}}", "<BR>");
                                line = line.Replace(strId, String.Format(">{0}<", strBrowserText));
                                bFound = true;
                            }
                        }
                    }
                }
            }
            return bFound;
        }


        public bool? Show()
        {
            var wpfwindow = new WebEditor(ref m_pageItem, m_strOrigResult);
            ElementHost.EnableModelessKeyboardInterop(wpfwindow);
            return wpfwindow.ShowDialog();
        }
    }
}
