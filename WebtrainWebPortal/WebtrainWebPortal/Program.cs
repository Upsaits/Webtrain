using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using WebtrainWebPortal.Views;
using Wisej.Core;
using Wisej.Web;

namespace WebtrainWebPortal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Application.ShowLoader=true;
            Application.Session.workflowMediator = WorkflowMediator.Instance;
            Application.Session.workflowMediator.SetViewState(null);
            Application.SessionTimeout += Application_SessionTimeout;
            Application.ApplicationStart += Application_ApplicationStart;
            Application.ApplicationRefresh += Application_ApplicationRefresh;
            Application.HashChanged += Application_HashChanged;
        }


        // 
        // You can use the entry method below
        // to receive the parameters from the URL in the args collection.
        // 
        //static void Main(NameValueCollection args)
        //{
        //}
        private static void Application_HashChanged(object sender, HashChangedEventArgs e)
        {
            var query = System.Web.HttpUtility.ParseQueryString(e.Hash);

            //AlertBox.Show("User ID: " + query["id"]);
        }

        private static void Application_ApplicationRefresh(object sender, EventArgs e)
        {
            if (Application.QueryString["id"]!=null)
                AlertBox.Show("User ID: " + Application.QueryString["id"]);
        }

        private static void Application_ApplicationStart(object sender, EventArgs e)
        {
            //AlertBox.Show("User ID: " + Application.QueryString["id"]);
        }

        private static void Application_SessionTimeout(object sender, System.ComponentModel.HandledEventArgs e)
        {
            WorkflowMediator mediator = Application.Session.workflowMediator;
            while (mediator.MainPages.Count>0)
            {
                UserInfo ui = mediator.MainPages.ElementAt(0).Value.UserInfo;
                mediator.SetViewState(ui, eViewState.eLogin);
                mediator.RemoveUser(ui);
            }

            e.Handled = true;
        }




        [WebMethod]
        public static void AppExit(string id)
        {
            //Debug.WriteLine("Application exits with ID: " + id);
        }
        //
        // You can use the entry method below
        // to receive the parameters from the URL in the args collection.
        //
        //static void Main(NameValueCollection args)
        //{
        //}
    }
}