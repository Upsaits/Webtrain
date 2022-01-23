using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftObject.SOComponents.UtilityLibrary
{
    public class CircProgBackgroundWorker : System.ComponentModel.BackgroundWorker
    {
        private DevComponents.DotNetBar.Controls.CircularProgress oCircProgress=null;
        int iDelayInMs;

        public CircProgBackgroundWorker(DevComponents.DotNetBar.Controls.CircularProgress oCircProgress, int iDelayInMs=30)
        {
            this.oCircProgress = oCircProgress;
            this.iDelayInMs = iDelayInMs;

            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
            DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnDoWork);
            ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OnProgressChanged);
        }

        public void Start()
        {
            CancelAsync();
            if (oCircProgress!=null)
                oCircProgress.Visible = true;
            RunWorkerAsync();
        }

        public void Stop()
        {
            if (oCircProgress != null)
                oCircProgress.Visible = false;
            CancelAsync();
        }

        private void OnProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (oCircProgress != null)
            {
                oCircProgress.Value = e.ProgressPercentage;
                if (oCircProgress.Value >= oCircProgress.Maximum)
                    oCircProgress.Value = 0;
               
            }
        }

        private void OnDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int i = 0;
            while (true)
            {
                if (this.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                System.Threading.Thread.Sleep(iDelayInMs);
                ReportProgress(i++);
                if (i == 100)
                    i = 0;
            }
        }
    }
}
