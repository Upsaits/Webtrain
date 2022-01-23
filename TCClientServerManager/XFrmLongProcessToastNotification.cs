using System;
using System.ComponentModel;
using System.Threading;

namespace SoftObject.TrainConcept.ClientServer
{
    public partial class XFrmLongProcessToastNotification
    {
        private readonly LongProcessHandler longProcessHandler= null;
        private bool bWasCancelled=false;
        public bool WasCancelled { get { return bWasCancelled; } }
        
        public XFrmLongProcessToastNotification()
        {
            longProcessHandler = new LongProcessHandler(SetText, EnableCancelBtn);
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
            //BeginInvoke((Action)(() =>
            //{
            //    SetText(strText);
            //}));
        }

        public void ShowCancelButton(bool bVisible)
        {
            //BeginInvoke((Action)(() =>
            //{
            //    this.btnCancel.Visible = bVisible;
            //}));
        }

        private void SetText(string strText)
        {
            //this.lblStatus.Text = strText;
        }

        private void EnableCancelBtn(bool bIsEnabled)
        {
            //this.btnCancel.Enabled = bIsEnabled;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bWasCancelled = true;
        }
    }
}
