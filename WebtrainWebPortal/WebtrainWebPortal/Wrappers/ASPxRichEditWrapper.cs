using System;
using Wisej.Web;
using System.ComponentModel;
using Wisej.Web.Ext.AspNetControl;
using DevExpress.Web.ASPxRichEdit;

namespace WebtrainWebPortal.Wrappers
{
    [ToolboxItem(true)]
	public class ASPxRichEditWrapper : Wisej.Web.Ext.AspNetControl.AspNetWrapper<ASPxRichEdit>
	{
		public ASPxRichEditWrapper()
		{			
		}
		protected override void OnInit(EventArgs e)
		{
			var editor = this.WrappedControl;

            editor.WorkDirectory = $"~/Data/";


            string filePath = Application.MapPath($"~/Data/Sozialanamnese Test.pdf");
            editor.Open(filePath);

			if (this.Saving != null)
				editor.Saving += Editor_Saving;
        }

		/// <summary>
		/// Handle saving event
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void Editor_Saving(object source, DevExpress.Web.Office.DocumentSavingEventArgs e)
		{
			if (MessageBox.Show ("Do you really want to save", buttons:MessageBoxButtons.YesNo) == DialogResult.No)
			{
				AlertBox.Show("Ok, it´s cancelled.");
				e.Handled = true;
			}
		}

		/// <summary>
		/// Saving event
		/// </summary>
		public event DevExpress.Web.Office.DocumentSavingEventHandler Saving;		
	}
}