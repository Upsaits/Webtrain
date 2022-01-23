using System;
using System.ComponentModel;
using Wisej.Web;
using DevExpress.Web.ASPxSpreadsheet;
using DevExpress.Spreadsheet;

namespace WebtrainWebPortal.Wrappers
{
    [ToolboxItem(true)]
    public partial class AspXSpreadsheetWrapper : Wisej.Web.Ext.AspNetControl.AspNetWrapper<ASPxSpreadsheet>
    {
        private ASPxSpreadsheet HostedControl;

        public IWorkbook Document  { get => HostedControl.Document; }

        public AspXSpreadsheetWrapper()
        {
            InitializeComponent();
        }
        protected override void OnInit(EventArgs e)
        {
            HostedControl = this.WrappedControl;
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            HostedControl = this.WrappedControl;
            base.OnLoad(e);
        }

        public void Open(string strFilename)
        {
            if (HostedControl == null)
            {
                HostedControl = (DevExpress.Web.ASPxSpreadsheet.ASPxSpreadsheet)CreateHostedControl();
            }

            HostedControl.Open(strFilename);
        }

        
    }
}
