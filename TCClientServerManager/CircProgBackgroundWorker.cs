using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftObject.TrainConcept.ClientServer
{
    public class CircProgBackgroundWorker : System.ComponentModel.BackgroundWorker
    {
        int iDelayInMs;

        public CircProgBackgroundWorker(int iDelayInMs=30)
        {
            this.iDelayInMs = iDelayInMs;

            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
            DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnDoWork);
            ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OnProgressChanged);
        }

        public void Start()
        {
            CancelAsync();
            RunWorkerAsync();
        }

        public void Stop()
        {
            CancelAsync();
        }

        private void OnProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
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
