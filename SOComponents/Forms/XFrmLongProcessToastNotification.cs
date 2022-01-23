using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;


namespace SoftObject.SOComponents.Forms
{
    public partial class XFrmLongProcessToastNotification : XtraForm
    {
        private readonly LongProcessHandler longProcessHandler= null;
        private bool bWasCancelled=false;
        public bool WasCancelled { get { return bWasCancelled; } }
        
        public XFrmLongProcessToastNotification()
        {
            InitializeComponent();
            longProcessHandler = new LongProcessHandler(this, this.circProgress, SetText, EnableCancelBtn);
        }

        public void Start(string strText,
                          Action<string, string, DoWorkEventArgs> delDoWorkActionStrStr,
                          Action<RunWorkerCompletedEventArgs> delCompletedAction, 
                          string strParam1="", string strParam2="")
        {
            longProcessHandler.Start(strText,delDoWorkActionStrStr,delCompletedAction,strParam1,strParam2);
        }

        public void Start(string strText, Action<DoWorkEventArgs> delDoWorkAction)
        {
            longProcessHandler.Start(strText, delDoWorkAction);
        }

        public void SetStatusText(string strText)
        {
            BeginInvoke((Action)(() =>
            {
                SetText(strText);
            }));
        }

        public void ShowCancelButton(bool bVisible)
        {
            BeginInvoke((Action)(() =>
            {
                this.btnCancel.Visible = bVisible;
            }));
        }

        private void SetText(string strText)
        {
            this.lblStatus.Text = strText;
        }

        private void EnableCancelBtn(bool bIsEnabled)
        {
            this.btnCancel.Enabled = bIsEnabled;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bWasCancelled = true;
        }
    }
}
