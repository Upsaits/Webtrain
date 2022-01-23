using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmChooseTest :  XtraForm
    {
        private string strMapTitle = "";
        private string strTestname = "";
        public string Testname
        {
            get { return strTestname; }
        }
        
        public XFrmChooseTest()
        {
            InitializeComponent();
        }

        public XFrmChooseTest(string strMapTitle)
        {
            InitializeComponent();

            this.strMapTitle = strMapTitle;
            FillTests();
        }

        
        private void FillTests()
        {
            if (strMapTitle.Length > 0)
            {
                int iCnt = Program.AppHandler.MapManager.GetTestCount(strMapTitle);
                for (int i = 0; i < iCnt; ++i)
                {
                    var ti = Program.AppHandler.MapManager.GetTest(strMapTitle, i);
                    TestType tType;
                    if (Utilities.Str2TestType(ti.type, out tType))
                    {
                        if (tType != TestType.Final)
                            this.lbctrlTests.Items.Add(ti.title);
                    }
                }
                if (lbctrlTests.ItemCount>0)
                    this.lbctrlTests.SelectedIndex = 0;
            }
        }

        private void lbctrlTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            strTestname = (string) this.lbctrlTests.GetDisplayItemValue(this.lbctrlTests.SelectedIndex);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            strTestname = "";
            DialogResult = DialogResult.Cancel;
        }
    }
}
