using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftObject.TrainConcept
{
    static class Program
    {
        public static AppHandler AppHandler { get; private set; }
        public static AppCommunication AppCommunication { get; private set; }
        public static AppHelpers AppHelpers { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptions);
            AppHandler = new AppHandler();
            AppCommunication = new AppCommunication();
            AppHelpers = new AppHelpers();
            AppHandler.Initialize();
        }

        private static void UnhandledExceptions(object sender, UnhandledExceptionEventArgs e)
        {
            if (e != null)
            {
                MessageBox.Show(((Exception) e.ExceptionObject).Message);
            }
        }
    }
}
