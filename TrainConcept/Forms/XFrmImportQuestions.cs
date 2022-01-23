using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmImportQuestions : DevExpress.XtraEditors.XtraForm
    {
        private DevExpress.XtraRichEdit.API.Native.Document doc = null;
        private QuestionItem question = null;
        private List<QuestionItem> aQuestions = new List<QuestionItem>();
        private List<String> aAnswers = new List<String>();

        public System.Collections.Generic.List<SoftObject.TrainConcept.Libraries.QuestionItem> Questions
        {
            get { return aQuestions; }
        }


        public XFrmImportQuestions()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            aQuestions.Clear();
            question = null;
            aAnswers.Clear();

            openFileDialog1.InitialDirectory = Program.AppHandler.ContentEditMediaFolder;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!bLoadDocument(openFileDialog1.FileName))
                //if (!bLoadDocument(@"D:\a11.docx"))
                    MessageBox.Show("Die gewählte Datei ist kein gültiges Word-Document!", "Error");
                if (!bParseDocument())
                    MessageBox.Show("Die Formattierung der Fragen ist nicht korrekt!", "Error");

                lblAnalysis.Text = String.Format("{0} Fragen gefunden",Questions.Count);
                lblAnalysis.Visible = true;
                btnImport.Enabled = true;
            }
        }

        private bool bLoadDocument(string strFilename)
        {
            try
            {
                this.richEditControl1.LoadDocument(strFilename, DocumentFormat.OpenXml);
                doc = this.richEditControl1.Document;
                return true;
            }
            catch (System.Exception /*ex*/)
            {
                            	
            }

            return false;
        }

        private bool bParseDocument()
        {
            bool bQuFound = false;

            foreach (var p in doc.Paragraphs)
            {
                string strTxt = doc.GetText(p.Range);
                if (strTxt.Length>0)
                {
                    Debug.WriteLine(doc.GetText(p.Range).ToString());
                    int iLeadCharPos = strTxt.IndexOfAny(new char[] { ',', ';', ')', '-', '.' });
                    if (iLeadCharPos >= 0 && iLeadCharPos < 3)
                    {
                        string strType = strTxt.Substring(0, iLeadCharPos);
                        int iRes = 0;
                        if (int.TryParse(strType, out iRes))
                        {
                            if (bQuFound && question != null)
                            {
                                question.Answers = aAnswers.ToArray();
                                Questions.Add(question);
                                question = null;
                                aAnswers.Clear();
                            }
                            string strQu = strTxt.Substring(iLeadCharPos + 1);
                            question = new QuestionItem();
                            question.question = strQu;
                            question.useForExaming = true;
                            question.useForTesting = true;
                            bQuFound = true;
                        }
                        else if (bQuFound)
                        {
                            string strAnswer = strTxt.Substring(iLeadCharPos + 1);
                            if (strAnswer.IndexOf("**") == 0)
                            {
                                int id = aAnswers.Count;
                                if (question != null)
                                    question.correctAnswerMask += (1 << id);
                                strAnswer = strAnswer.Substring(2);
                            }

                            if (question != null)
                                aAnswers.Add(strAnswer);
                        }
                    }
                }
            }


            if (question != null)
            {
                question.Answers = aAnswers.ToArray();
                Questions.Add(question);
                question = null;
                aAnswers.Clear();
            }

            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            aQuestions.Clear();
            DialogResult = DialogResult.OK;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}