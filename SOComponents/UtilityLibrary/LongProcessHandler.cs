using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using DevExpress.XtraEditors;

namespace SoftObject.SOComponents.UtilityLibrary
{
    public delegate void LPH_SetText(string strText);
    public delegate void LPH_EnableCancelBtn(bool bIsEnabled);

    public class LongProcessHandler
    {
        private BackgroundWorker backgroundWorker1=new BackgroundWorker();
        private CircProgBackgroundWorker oCirProgBackWork = null;
        private Action<string, string,DoWorkEventArgs> delDoWorkActionStrStr = null;
        private Action<DoWorkEventArgs> delDoWorkAction = null;
        private Action<RunWorkerCompletedEventArgs> delCompletedAction = null;
        private string strParam1;
        private string strParam2;
        private bool bShowUi;
        private Form frmUi=null;
        private AutoResetEvent jobDone = new AutoResetEvent(false);
        private LPH_SetText fnLphSetText = null;
        private LPH_EnableCancelBtn fnLphEnableCancelBtn = null;

        public LongProcessHandler(Form frm = null, CircularProgress circProgress = null, LPH_SetText fnLphSetText = null, LPH_EnableCancelBtn fnLphEnableCancelBtn=null)
        {
            this.frmUi = frm;
            this.fnLphSetText = fnLphSetText;
            this.fnLphEnableCancelBtn = fnLphEnableCancelBtn;
            this.bShowUi = (frmUi!=null);
            if (bShowUi)
                oCirProgBackWork = new CircProgBackgroundWorker(circProgress);
        }

        public void Start(string strText, 
                          Action<string, string, DoWorkEventArgs> delDoWorkActionStrStr,
                          Action<RunWorkerCompletedEventArgs> delCompletedAction, 
                          string strParam1="", string strParam2="")
        {
            this.delDoWorkActionStrStr = delDoWorkActionStrStr;
            this.delCompletedAction = delCompletedAction;
            this.strParam1 = strParam1;
            this.strParam2 = strParam2;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.RunWorkerAsync();

            if (bShowUi)
            {
                if (fnLphSetText!=null)
                    fnLphSetText(strText);
                if (fnLphEnableCancelBtn != null)
                    fnLphEnableCancelBtn(true);
                DialogResult res = DialogResult.Cancel;
                oCirProgBackWork.RunWorkerAsync();
                res = frmUi.ShowDialog();
            }
            else
                jobDone.WaitOne();
        }

        public void Start(string strText, Action<DoWorkEventArgs> delDoWorkAction)
        {
            this.delDoWorkAction = delDoWorkAction;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.RunWorkerAsync();
            if (bShowUi)
            {
                if (fnLphSetText != null)
                    fnLphSetText(strText);
                if (fnLphEnableCancelBtn != null)
                    fnLphEnableCancelBtn(true);
                DialogResult res = DialogResult.Cancel;
                oCirProgBackWork.RunWorkerAsync();
                res = frmUi.ShowDialog();
            }
            else
                jobDone.WaitOne();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (delDoWorkActionStrStr != null)
                delDoWorkActionStrStr.Invoke(strParam1, strParam2,e);
            else if (delDoWorkAction != null)
                delDoWorkAction(e);

            if (!bShowUi)
                jobDone.Set();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. We need to inspect
            // our response to see if an error occurred, a cancel was
            // requested or if we completed successfully.  
            if (delCompletedAction != null)
                delCompletedAction(e);

            if (bShowUi)
            {
                var res = DialogResult.Cancel;
                // task cancelled?
                if (e.Cancelled)
                {
                    if (fnLphSetText != null)
                        fnLphSetText("Task cancelled");
                }
                // Check to see if an error occurred in the background process.
                else if (e.Error != null)
                {
                    if (fnLphSetText != null)
                        fnLphSetText("error occured");
                }
                else
                {
                    // Everything completed normally.
                    //lblStatus.Text = "Task Completed...";
                    res = System.Windows.Forms.DialogResult.OK;
                }

                //Change the status of the buttons on the UI accordingly
                if (fnLphEnableCancelBtn != null)
                    fnLphEnableCancelBtn(false);
                
                oCirProgBackWork.CancelAsync();
                oCirProgBackWork.Dispose();

                frmUi.BeginInvoke((Action)(() =>
                {
                    if (frmUi.Visible)
                        frmUi.DialogResult = res;
                }));
            }
        }

    }
}
