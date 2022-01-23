using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.IO;
using System.Resources;
using DevExpress.XtraEditors;
using SoftObject.UtilityLibrary;


namespace SoftObject.SOComponents.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für SOFlashPlayer.
	/// </summary>
	public partial class SOFlashPlayer : XtraForm
	{
		private int MouseDownX;
		private int MouseDownY;
		private bool isWindowMoving = false;
		private bool suppressClose=false;
		private bool bIsValid;

		public bool IsValid
		{
			get { return bIsValid;}
		}

		public SOFlashPlayer(bool _suppressClose)
		{
            try
			{
			    suppressClose=_suppressClose;

			    InitializeComponent();

			    MinimumSize = new Size(349,321); // Für Standardvideogröße 320x240 optimale Fenstergröße
			    
	            //Mache zu Anfang ganzes Fenster nicht sichtbar
			    Visible = false;
				bIsValid = true;
			}
			catch (System.Exception /*e*/)
			{
				bIsValid = false;
			}
		}

		public new void Close()
		{
			if (bIsValid)
			{
			if (suppressClose)
				Hide();
			else
				base.Close();
    		}
		}

		public void Play(string fileName)
		{
			try
			{

                if (!CheckURLValid(fileName))
                {
                    if (!File.Exists(fileName))
                    {
                        MessageBox.Show(String.Format("Die Datei {0} existiert nicht!", fileName));
                        return;
                    }
                    fileName = Path.GetFullPath(fileName);
                }

				if (bIsValid)
				{
			
				    Show();
	                Refresh();
			    }
			}
			catch (System.Exception ex)
			{
                MessageBox.Show(ex.ToString());
			}
		}

        private static bool CheckURLValid(string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }

		public void Stop()
		{

		}

		private void SOFlashPlayer_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isWindowMoving = true;
				MouseDownX = e.X;
				MouseDownY = e.Y;
			}
		}

		private void SOFlashPlayer_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( isWindowMoving )
			{
				Point temp = new Point(0,0);

				temp.X = this.Location.X + (e.X - MouseDownX);
				temp.Y = this.Location.Y + (e.Y - MouseDownY);
				this.Location = temp;
			}
		}

		private void SOFlashPlayer_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				isWindowMoving = false;
		}
	}
}