using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoftObject.SOComponents.Controls
{
	/// <summary>
	/// Bitmap Button mit dem Verhalten einer RadioBox
	/// </summary>
	public class SORadioBmpBtn:System.Windows.Forms.RadioButton
	{
        private bool bMouseInside=false;
        public bool MouseInside
        {
            get { return bMouseInside; }
        }
		private ToolTip toolTip = new ToolTip();
		private string toolTipText;
		public string ToolTipText
		{
			set
			{
				toolTipText = value;
				InitToolTip();
			}
			get
			{
				return toolTipText;
			}
		}
		

		public SORadioBmpBtn()
		{

		}
				
		public SORadioBmpBtn(ImageList _imageList,int _iImgId,int _btnWidth,int _btnHeight)
		{
			//Ausgangsbild laden
			this.ImageList = _imageList;
			this.ImageIndex= _iImgId;
			
			//Schaltflächenmaße nach den Abmaßen des Bildes einstellen
			this.Size = new Size(_btnWidth,_btnHeight);
			
			//Texteinstellungen
			this.ForeColor = Color.Black; 
			this.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

			//Diverse Einstellungen
			this.AutoCheck = true; //Ein Radio soll immer geckeckt bleiben
			this.TabStop = true;
			this.Appearance = Appearance.Button;
			this.FlatStyle = FlatStyle.Flat;
			this.Cursor = Cursors.Hand;
		}

		private void InitToolTip()
		{
			toolTip.AutoPopDelay = 3000;
			toolTip.InitialDelay = 100;
			toolTip.ReshowDelay = 100;
			toolTip.ShowAlways = true;
			toolTip.SetToolTip(this, toolTipText);
		}
		
		protected override void OnMouseEnter(System.EventArgs e)
		{
			base.OnMouseEnter(e);
		    ImageIndex = Checked ? 2 : 1;
		    bMouseInside = true;
		}

		protected override void OnMouseLeave(System.EventArgs e)
		{
			base.OnMouseLeave(e);
		    ImageIndex = Checked ? 2 : 0;
		    bMouseInside = false;
        }

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if(MouseButtons == MouseButtons.Left)	
			{
				if(e.X >= 0 || e.Y >= 0 || e.X <= Width ||e.Y <= Height)
				{
					this.ImageIndex = 2;
				}
			}
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);
			
			this.ImageIndex = 2;
		}

		protected override void OnCheckedChanged(System.EventArgs e)
		{
			base.OnCheckedChanged(e);
            ImageIndex = Checked ? 2 : 0;
        }
		
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);
			
			//Device Context holen
			Graphics grfx = e.Graphics;
			Pen pen = new Pen(BackColor); //Pen hat immer die Farbe des Fensters auf dem der Button ist.
			//Äußeren Rand mit der Hintergrundfarbe der RadioBox überzeichnen
			Rectangle rect = new Rectangle(0,0,Width-1,Height-1);
			grfx.DrawRectangle(pen,rect);
			//linken und oberen Rand mit der Hintergrundfarbe der RadioBox überzeichnen
			grfx.DrawLine(pen,1,1,Width-2,1);
			grfx.DrawLine(pen,1,1,1,Height-2);
		}
	}
}

