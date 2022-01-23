using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;

namespace SoftObject.TrainConcept.Forms
{
    public partial class Form2 : Form
    {
        private AppHandler AppHandler = Program.AppHandler;
        public Form2()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string strFile = AppHandler.ContentFolder + @"\cnc-praxis\docs\Koordinatentabelle_1.rtf";
            string strFileCorrect = AppHandler.ContentFolder + @"\cnc-praxis\docs\Koordinatentabelle_1_Loesung.rtf";

            richEditControl1.LoadDocument(strFile, DocumentFormat.Rtf);
            richEditControl2.LoadDocument(strFileCorrect, DocumentFormat.Rtf);

            string strText1 = richEditControl1.Document.HtmlText;
            string strText2 = richEditControl2.Document.HtmlText;

            HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(strText1, strText2);

            // Lets add a block expression to group blocks we care about (such as dates)
            diffHelper.AddBlockExpression(new Regex(@"[\d]{1,2}[\s]*(Jan|Feb)[\s]*[\d]{4}", RegexOptions.IgnoreCase));

            int iFoundDiffCount;
            int iFoundInsCount;
            int iFoundDelCount;
            int iFoundReplCount;

            string strResult = diffHelper.Build(out iFoundDiffCount,out iFoundInsCount,out iFoundDelCount,out iFoundReplCount);

            if (iFoundDiffCount>0 || iFoundReplCount>0 || iFoundInsCount>0 || iFoundDelCount>0)
                webBrowser1.DocumentText= strResult;
            else
               webBrowser1.DocumentText="<!DOCTYPE html><html><body><h1>Sie sind gleich</h1></body></html>";
        }

    }
}
