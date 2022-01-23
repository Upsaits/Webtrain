using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace SoftObject.SOComponents.UtilityLibrary
{

    // usage:
    //backgroundWorker1 = new AbortableBackgroundWorker();
    ////...
    //backgroundWorker1.RunWorkerAsync();

    //if (backgroundWorker1.IsBusy == true)
    //{
    //    backgroundWorker1.Abort();
    //    backgroundWorker1.Dispose();
    //}

    public class AbortableBackgroundWorker : BackgroundWorker
    {

        private Thread workerThread;

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            workerThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);
            }
            catch (ThreadAbortException)
            {
                e.Cancel = true; //We must set Cancel property to true!
                Thread.ResetAbort(); //Prevents ThreadAbortException propagation
            }
        }


        public void Abort()
        {
            if (workerThread != null)
            {
                workerThread.Abort();
                workerThread = null;
            }
        }
    }
}
