using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmSelectServer : XtraForm
    {
        List<Tuple<int, Tuple<string, int>>> lServers = new List<Tuple<int, Tuple<string, int>>>();
        public int[] SelectedServers { get; private set; }

        public FrmSelectServer()
        {
            InitializeComponent();

            lServers.Add(new Tuple<int, Tuple<string, int>>(1,new Tuple<string, int>("SERVER2016", 1019)));
            //lServers.Add(new Tuple<int, Tuple<string, int>>(1,new Tuple<string, int>("CNC Lehrsaal", 1002)));
            //lServers.Add(new Tuple<int, Tuple<string, int>>(1,new Tuple<string, int>("Pneumatik Lehrsaal", 1005)));
            //lServers.Add(new Tuple<int, Tuple<string, int>>(1,new Tuple<string, int>("Lehrsaal 3", 1006)));
            lServers.Add(new Tuple<int, Tuple<string, int>>(2,new Tuple<string, int>("Testserver 1", 9999)));
            lServers.Add(new Tuple<int, Tuple<string, int>>(2,new Tuple<string, int>("Testserver 2", 9990)));
            SelectedServers = null;
        }

        private void imageListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxControl1.Items.Clear();

            int iSel = imageListBoxControl1.SelectedIndex;
            foreach (var s in lServers)
                if (s.Item1 == iSel+1)
                    listBoxControl1.Items.Add(s.Item2.Item1);
        }

        private void FrmSelectServer_Load(object sender, EventArgs e)
        {
            imageListBoxControl1.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var aSel = listBoxControl1.SelectedIndices.ToArray();
            int[] aRes = (int[])aSel.Clone();
            for(int i=0;i<aSel.Length;++i)
                if (imageListBoxControl1.SelectedIndex==0)
                    aRes[i] = lServers[aSel[i]].Item2.Item2;
                else if (imageListBoxControl1.SelectedIndex == 1)
                    aRes[i] = lServers[aSel[i]+1].Item2.Item2;

            SelectedServers = aRes;
            DialogResult = DialogResult.OK;
        }
    }
}
