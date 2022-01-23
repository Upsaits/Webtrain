using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;

namespace SoftObject.SOComponents.Forms
{
    public partial class XFrmLongProcessToastNotificationSmall : XtraForm
    {
        private readonly LongProcessHandler longProcessHandler= null;
        
        public XFrmLongProcessToastNotificationSmall()
        {
            InitializeComponent();
            longProcessHandler = new LongProcessHandler(this, this.circProgress, SetText, EnableCancelBtn);
        }

        public void Start(string strText, 
                          Action<string, string, DoWorkEventArgs> delDoWorkActionStrStr,
                          Action<RunWorkerCompletedEventArgs> delCompletedAction, 
                          string strParam1="", string strParam2="")
        {
            longProcessHandler.Start(strText, delDoWorkActionStrStr, delCompletedAction, strParam1, strParam2);
        }

        public void Start(string strText, Action<DoWorkEventArgs> delDoWorkAction)
        {
            longProcessHandler.Start(strText, delDoWorkAction);
        }

        private void SetText(string strText)
        {
            this.lblStatus.Text = strText;
        }

        private void EnableCancelBtn(bool bIsEnabled)
        {
            //this.btnCancel.Enabled = bIsEnabled;
        }
    }
}
