using System;
using DevExpress.XtraEditors;
using SoftObject.TrainConcept.Controls;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmSelectContent : XtraForm
    {
        private String strSelectedPath="";
        public System.String SelectedPath
        {
            get { return strSelectedPath; }
            set { strSelectedPath = value;}
        }

        public XFrmSelectContent()
        {
            InitializeComponent();

            xContentTree1.FillTree();
            xContentTree1.PopulateColumns();
            xContentTree1.BestFitColumns();
            xContentTree1.CollapseAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void xContentTree1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.OldNode == null)
            {
                xContentTree1.SelectItem(strSelectedPath);
                return;
            }

            XContentTree.NodeType eNodeType;
            string strTitle;
            if (xContentTree1.AnalyseContentNode(out eNodeType, out strTitle))
            {
                string strPath = xContentTree1.ContentList.GetPath(e.Node.Id);
                btnOK.Enabled = (eNodeType == XContentTree.NodeType.Point);
                strSelectedPath = strPath;
            }
        }

        private void XFrmSelectContent_Load(object sender, EventArgs e)
        {
            if (this.strSelectedPath.Length > 0)
                this.xContentTree1.SelectItem(strSelectedPath);
        }
    }
}
