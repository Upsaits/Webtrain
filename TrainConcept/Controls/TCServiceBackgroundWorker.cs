using System.ComponentModel;
using System.Windows.Forms;

namespace SoftObject.TrainConcept.Controls
{
    public class TCServiceBackgroundWorker : BackgroundWorker
    {
        public Control ParentCtrl { get; set; }
        public string StrParam { get; set; }
        public string StrResult { get; set; }

        public TCServiceBackgroundWorker(Control parentCtrl, string strParam = "")
        {
            ParentCtrl = parentCtrl;
            StrParam = strParam;
            WorkerReportsProgress = false;
            WorkerSupportsCancellation = false;
            this.RunWorkerCompleted+=TCServiceBackgroundWorker_RunWorkerCompleted;
        }

        private void TCServiceBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var w = sender as TCServiceBackgroundWorker;
            if (w.ParentCtrl != null)
                DevComponents.DotNetBar.ToastNotification.Close(w.ParentCtrl);
        }
    };
}
