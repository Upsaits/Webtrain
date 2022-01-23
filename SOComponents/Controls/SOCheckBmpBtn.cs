using System;
using System.Drawing;
using System.Windows.Forms;

namespace SoftObject.SOComponents.Controls
{
	/// <summary>
	/// Bitmap Button mit dem Verhalten einer CheckBox
	/// </summary>
	public class SOCheckBmpBtn : System.Windows.Forms.CheckBox
	{
		private ToolTip toolTip = new ToolTip();
		//private Bitmap tempBmp_0, tempBmp_1, tempBmp_2;
		private int btnWidth = 0;
		private int btnHeight = 0;
		private bool wasClicked = false;
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
	
		public SOCheckBmpBtn()
		{
		}
		
		public SOCheckBmpBtn(ImageList _imageList)
		{
			//Ausgangsbild laden
			this.ImageList = _imageList;
			this.ImageIndex = 0;

			//Schaltflächenmaße nach den Abmaßen des Bildes einstellen
			btnWidth = Image.Width+3;
			btnHeight = Image.Height+3;
			Size btnSize = new Size(btnWidth,btnHeight);
			this.Size = btnSize;
				
			//Texteinstellungen
			this.ForeColor = Color.Black;
			this.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

			//Diverse Einstellungen
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

			if(wasClicked && AutoCheck)
			{
				this.ImageIndex = 2;
				this.ForeColor = Color.Black;
			}
			else
			{
				this.ImageIndex = 1;
				this.ForeColor = Color.Black;
			}
		}

		protected override void OnMouseLeave(System.EventArgs e)
		{
			base.OnMouseLeave(e);

			if(wasClicked && AutoCheck)
			{
				this.ImageIndex = 2;
				this.ForeColor = Color.Black;
			}
			else
			{
				this.ImageIndex = 0;
				this.ForeColor = Color.Black;
			}
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);
			
			if(MouseButtons == MouseButtons.Left)	
			{
				if(e.X <= 0 || e.Y <= 0 || e.X >= Width ||e.Y >= Height)
				{
					if(wasClicked && AutoCheck)
					{
						this.ImageIndex = 2;
						this.ForeColor = Color.Black;
					}
					else
					{
						this.ImageIndex = 0;
						this.ForeColor = Color.Black;
					}
				}
				else
				{
					if(e.X >= 0 || e.Y >= 0 || e.X <= Width ||e.Y <= Height)
					{
						this.ImageIndex = 2;
						this.ForeColor = Color.Black;
					}
				}
			}
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if(wasClicked && AutoCheck)
			{
				this.ImageIndex = 2;
				this.ForeColor = Color.Black;
			}
			else
			{
				this.ImageIndex = 1;
				this.ForeColor = Color.Black;
			}
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);

			this.ImageIndex = 2;
			this.ForeColor = Color.Black;
		}

		protected override void OnClick(System.EventArgs e)
		{
			base.OnClick(e);

			if(wasClicked)
				wasClicked = false;
			else
				wasClicked = true;
		}

		protected override void OnCheckedChanged(System.EventArgs e)
		{
			base.OnCheckedChanged(e);

			if(Checked)
			{
				this.ImageIndex = 2;
				this.ForeColor = Color.Black;
			}
			else
			{
				this.ImageIndex = 0;
				this.ForeColor = Color.Black;
			}
		}
		
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);
			
			//Device Context holen
			Graphics grfx = e.Graphics;
			Pen pen = new Pen(this.BackColor); //Pen hat immer die Farbe des Fensters auf dem der Button ist.
			//Äußeren Rand mit der Hintergrundfarbe der CheckBox zeichnen
			Rectangle rect = new Rectangle(0,0,Width-1,Height-1);
			grfx.DrawRectangle(pen,rect);
			//linken und oberen Rand mit der Hintergrundfarbe der CheckBox zeichnen
			grfx.DrawLine(pen,1,1,Width-2,1);
			grfx.DrawLine(pen,1,1,1,Height-2);
		}
	}
}
