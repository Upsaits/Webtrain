using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftObject.TrainConcept.ClientServer
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
        private AutoResetEvent jobDone = new AutoResetEvent(false);
        private LPH_SetText fnLphSetText = null;
        private LPH_EnableCancelBtn fnLphEnableCancelBtn = null;

        public LongProcessHandler(LPH_SetText fnLphSetText = null, LPH_EnableCancelBtn fnLphEnableCancelBtn=null)
        {
            this.fnLphSetText = fnLphSetText;
            this.fnLphEnableCancelBtn = fnLphEnableCancelBtn;
            this.bShowUi = false;
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
                oCirProgBackWork.RunWorkerAsync();
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
                oCirProgBackWork.RunWorkerAsync();
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
                }

                //Change the status of the buttons on the UI accordingly
                if (fnLphEnableCancelBtn != null)
                    fnLphEnableCancelBtn(false);
                
                oCirProgBackWork.CancelAsync();
                oCirProgBackWork.Dispose();
            }
        }

    }
}
