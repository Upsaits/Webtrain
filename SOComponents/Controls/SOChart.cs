using System;
using System.ComponentModel; 
using System.Drawing;
using System.Threading;

namespace SoftObject.SOComponents.Controls
{
	/// <summary>
	/// Zusammendfassende Beschreibung für chart.
	/// </summary>
	public class SOChart:System.Windows.Forms.Panel
	{
		//Propertie Endgröße(X,Y)
		private Size endSize;
		public Size EndSize
		{
			get{return endSize;}
			set{endSize = value;}
		}
		//Properties für Rahmen-, Text- und Barfarben
		private Color frameColor;
		public Color FrameColor
		{
			get{return frameColor;}
			set{frameColor = value;}
		}		
		private Color textColor;
		public Color TextColor
		{
			get{return textColor;}
			set{textColor = value;}
		}
		//Colors Bar 1
		private Color color1ONE;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public Color Color1ONE
		{
			get{return color1ONE;}
			set{color1ONE = value;}
		}
		private Color color2ONE;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public Color Color2ONE
		{
			get{return color2ONE;}
			set{color2ONE = value;}
		}
		private int alpha1ONE;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public int Alpha1ONE
		{
			get{return alpha1ONE;}
			set{alpha1ONE = value;}
		}
		private int alpha2ONE;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public int Alpha2ONE
		{
			get{return alpha2ONE;}
			set{alpha2ONE = value;}
		}
		//Colors Bar 2
		private Color color1TWO;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public Color Color1TWO
		{
			get{return color1TWO;}
			set{color1TWO = value;}
		}
		private Color color2TWO;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public Color Color2TWO
		{
			get{return color2TWO;}
			set{color2TWO = value;}
		}
		private int alpha1TWO;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public int Alpha1TWO
		{
			get{return alpha1TWO;}
			set{alpha1TWO = value;}
		}
		private int alpha2TWO;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public int Alpha2TWO
		{
			get{return alpha2TWO;}
			set{alpha2TWO = value;}
		}
		//Colors Bar 3
		private Color color1THREE;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public Color Color1THREE
		{
			get{return color1THREE;}
			set{color1THREE = value;}
		}
		private Color color2THREE;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public Color Color2THREE
		{
			get{return color2THREE;}
			set{color2THREE = value;}
		}
		private int alpha1THREE;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public int Alpha1THREE
		{
			get{return alpha1THREE;}
			set{alpha1THREE = value;}
		}
		private int alpha2THREE;
		[Category("BarColors"), Description("Farbe und Alpha-Kanal der Bars")]
		public int Alpha2THREE
		{
			get{return alpha2THREE;}
			set{alpha2THREE = value;}
		}
		//Barhöhe [%] Bar1, Bar2, Bar3
		private int percentONE;
		/// <summary>
		/// Setzt den Prozentwert für die Bar 1 oder ruft diesen ab.
		/// </summary>
		public int PercentONE
		{
			get{return percentONE;}
			set
			{
				percentONE = value;
				Invalidate();
			}
		}
		private int percentTWO;
		/// <summary>
		/// Setzt den Prozentwert für die Bar 2 oder ruft diesen ab.
		/// </summary>
		public int PercentTWO
		{
			get{return percentTWO;}
			set
			{
				percentTWO = value;
				Invalidate();
			}
		}
		private int percentTHREE;
		/// <summary>
		/// Setzt den Prozentwert für die Bar 3 oder ruft diesen ab.
		/// </summary>
		public int PercentTHREE
		{
			get{return percentTHREE;}
			set
			{
				percentTHREE = value;
				Invalidate();
			}
		}
		//Barbeschriftung		
		private string textONE;
		/// <summary>
		/// Setzt den Text für die Bar 1 oder ruft diesen ab.
		/// </summary>
		public string TextONE
		{
			get{return textONE;}
			set{textONE = value;}
		}
		private string textTWO;
		/// <summary>
		/// Setzt den Text für die Bar 2 oder ruft diesen ab.
		/// </summary>
		public string TextTWO
		{
			get{return textTWO;}
			set{textTWO = value;}
		}
		private string textTHREE; 
		/// <summary>
		/// Setzt den Text für die Bar 3 oder ruft diesen ab.
		/// </summary>
		public string TextTHREE
		{
			get{return textTHREE;}
			set{textTHREE = value;}
		}
		private int delay;
		public int Delay
		{
			get{return delay;}
			set{delay = value;}
		}


		private int startWidth;  
		private int startHeight;
		private int startLocX;
		private int startLocY;
		private bool done;
		private bool init;

		public SOChart()
		{
			//Defaultwerte
			endSize.Width = 300;
			endSize.Height = 150;
			frameColor = Color.Orange;
			textColor = Color.Blue;
			color1ONE = Color.DarkOliveGreen;
			color2ONE = Color.DarkGreen;
			alpha1ONE = 255;
			alpha2ONE = 155;
			color1TWO = Color.Maroon;
			color2TWO = Color.DarkRed;
			alpha1TWO = 255;
			alpha2TWO = 155;
			color1THREE = Color.DarkSlateBlue;
			color2THREE = Color.DarkBlue;
			alpha1THREE = 255;
			alpha2THREE = 155;
			delay = 0; //Thread delay für's zuklappen.
			
			this.Size = new System.Drawing.Size(60,50); //Default Anfangsgöße
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BackColor = Color.FloralWhite;
			done = false; //Schon vollständig aufgeklappt ?
			init = false; //Erstes mal zeichnen !
		}
			
		protected override void OnMouseHover(System.EventArgs e)
		{
			base.OnMouseHover(e);
			double loopFactor = (double)endSize.Width/(double)endSize.Height; //Breite könnte größer als Höhe sein. Schleife läuft aber nur über die Höhe
			int chartLocY = this.Location.Y + this.Height; //Festlegen der Y-Location zum vergößern
			for(int i=0; i<=endSize.Height; i+=10)         //Zeichne von 0 weg
			{
				Thread.Sleep(5);
				this.AutoScroll = false;
				this.Location= new System.Drawing.Point(this.Location.X,chartLocY -i); //Versetze Ursprungspunkt immer um den selben Wert nach oben wie Y-Richtung erhöht wird.
				this.Height = i;                //Mache in Y-Richtung größer
				if(this.Width < endSize.Width)  //Nur solange Endgröße nicht erreicht ist
					this.Width = i*(int)Math.Ceiling(loopFactor); //Mache in X-Richtung größer
				this.Invalidate();
			}
			done = true; //Vollständig aufgeklappt
		}

		protected override void OnMouseLeave(System.EventArgs e)
		{
			base.OnMouseLeave(e);
			if(done) //Nur zumachen wenn vorher auch ganz offen
			{
				double loopFactor = (double)endSize.Width/(double)endSize.Height; //Breite könnte größer als Höhe sein. Schleife läuft aber nur über die Höhe
				int chartLocY = (this.Location.Y); //Hole die aktuelle Y-Location damit's auch wieder da zugeht wo's aufgegangen ist
				Thread.Sleep(delay*30);
				for(int i=0; i<=(endSize.Height-startHeight); i+=10)
				{
					Thread.Sleep(delay);
					this.AutoScroll = false;
					this.Location= new System.Drawing.Point(this.Location.X,chartLocY+i); //Versetze Ursprungspunkt immer um den selben Wert nach unten wie Y-Richtung erniedrigt wird.
					this.Height = (endSize.Height -i);     //Mache in Y-Richtung wieder kleiner
					if(this.Width <= startWidth)		   //Wenn Ausgangsgröße schon wieder erreicht
					{
						//Tue nichts mehr
					}
					else //mach weiter kleiner
						this.Width = (endSize.Width -i*(int)Math.Ceiling(loopFactor)); //Mache in X-Richtung wieder kleiner
				}
				done = false;
				//Anfangsgröße sicherstellen
				this.Height = startHeight; 
				this.Width = startWidth;
				this.Location = new Point(startLocX,startLocY);
				this.Invalidate(); 
			}
			else
			{
				//Wenn nicht gewartet wird bis Vollständig offen dann belasse alles auf den Ausgangswerten (Maus streift nur)
				return;
			}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics grfx = e.Graphics;
			Pen framePen = new Pen(frameColor);               //Rahmenfarbe
			SolidBrush textBrush = new SolidBrush(textColor); //Textfarbe
			
			if(!init)
			{
				//Zeichne beim ersten mal mit versetzter Y-Location
				this.Location= new System.Drawing.Point(Location.X, Location.Y); 
				//Merken der Anfangsgröße (für MouseLeave)
				startWidth = this.Size.Width;
				startHeight = this.Size.Height;
				startLocX = this.Location.X; //Anfangslocation merken
				startLocY = this.Location.Y; //Anfangslocation merken
				DrawFrame(grfx,framePen);  //Rahmen um das Panel
				DrawBars(grfx,textBrush);
				init = true;
			}
			else
			{
				DrawFrame(grfx,framePen); //Rahmen um das Panel
				DrawBars(grfx,textBrush);
			}
			textBrush.Dispose();
			framePen.Dispose();
		}

		private void DrawFrame(Graphics grfx_, Pen framePen_)
		{
			grfx_.DrawRectangle(framePen_,0,0,this.Width-1,this.Height-1);
		}

		private void DrawBars(Graphics grfx_, Brush textBrush_)
		{
			//Festlegung des verwendeten Fonts zur Barbeschriftung
			int size;
			System.Drawing.FontStyle style;
			int textMargin;
			//grfx_.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			if(this.Width >= 250 && this.Height >= 150)
			{
				size = 12;
				style = FontStyle.Regular;
				textMargin = 21; //Abstand des oberen Textes von der Bar
			}
			else
			{
				size = 8;
				style = FontStyle.Regular;
				textMargin = 14; //Abstand des oberen Textes von der Bar
			}

			this.Font = new Font("Arial",size,style);

			//**********  BAR 1  **********//
			double barWidthONE_ = (double)this.Width*0.17;  //Barbreite Proportional zur Breite des Panels
			int barWidthONE = (int)Math.Round(barWidthONE_);

			//Abstand der ERSTEN Bar vom linken Rand. Es werden immer 3 Bars vorausgesetzt!!
			double leftPointONE_ = (((double)this.Width - (barWidthONE_ *3))/4)*1 + barWidthONE_ * 0; //3.. Anzahl Bars, 4.. Abstände zwischen den Bars, 1.. für erte Bar, 0.. für erste Bar 
			int leftPointONE = (int)Math.Round(leftPointONE_);
			
			//Startpunkt festlegen 
			//this.Height*0.666 -> tatsächlich benutzte Höhe des Panels == 100% Bar
			//this.Height*0.166 -> Oberer Randabstand der Bar vom Panel
			double startPointONE_ = (double)this.Height-((double)this.Height*0.666*(double)percentONE/100)-((double)this.Height *0.166);
			int startPointONE     = (int)Math.Round(startPointONE_);
			double barHeightONE_  = (double)this.Height-(startPointONE_+ (double)this.Height*0.166);
			int barHeightONE      = (int) Math.Round(barHeightONE_);
			
			if(barHeightONE == 0) //Stelle sicher, dass die Bar immer einen gültige Höhe hat
				barHeightONE = 1;
						
			if(percentONE <0 || percentONE > 100)  //Tue nichts gibts es nicht
				return;
			else
			{
				//Textgröße holen und Bar beschriften -> siehe auch Petzold S.85
				SizeF sizef1;
				sizef1 = grfx_.MeasureString(percentONE+"%",Font);
				float text1Pos = ((leftPointONE + barWidthONE/2) - (sizef1.Width/2));
				
				SizeF sizef2;
				sizef2 = grfx_.MeasureString(textONE,Font);
				float text2Pos = (((float)leftPointONE + (float)barWidthONE/2) - (sizef2.Width/2));

				if(this.Width < 250 && this.Height < 150) //Wenn keine vernünftige größe mehr
				{
					//Beschrifte die Bar nicht
				}
				else
				{
					grfx_.DrawString(percentONE +"%", Font, textBrush_, text1Pos, startPointONE-(textMargin));        //Beschriftung um 20 pixel über der Bar
					grfx_.DrawString(textONE, Font, textBrush_, text2Pos, (startPointONE+barHeightONE-1));  //Beschriftung um 8 pixel unter der Bar
				}
				if(percentONE == 0)
				{
					//Zeichne die Bar nicht beschrifte sie aber 
				}
				else
				{
					Rectangle rect = new Rectangle(leftPointONE, startPointONE, barWidthONE, barHeightONE);
					Brush gBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rect,(Color.FromArgb(alpha1ONE,color1ONE)),(Color.FromArgb(alpha2ONE,color2ONE)),90);
					grfx_.FillRectangle(gBrush,rect);
					//Rahmen einer 100% Bar zeichnen
					/*
						double temp = ((double)this.Height)*0.166;  //Oberen Startpunkt holen. +1 ist die Linienstärke des Randes
						int upperPointONE = (int)Math.Round(temp);
						double temp1 = (double)this.Height*0.666;
						int wholeBarONE = (int) Math.Round(temp1);
						grfx_.DrawRectangle(pen,leftPointONE,upperPointONE,barWidthONE,wholeBarONE);
						*/
					//Rahmen um die Bar zeichnen
					//Pen barPen = new Pen(Color.Black);
					//grfx_.DrawRectangle(barPen,leftPointONE,startPointONE,barWidthONE,barHeightONE);
				}
			}


			//**********  BAR 2  **********//
			double barWidthTWO_ = (double)this.Width*0.17;  //Barbreite Proportional zur Breite des Panels
			int barWidthTWO = (int)Math.Round(barWidthTWO_);

			//Abstand der ZWEITEN Bar vom linken Rand. Es werden immer 3 Bars vorausgesetzt!!
			double leftPointTWO_ = (((double)this.Width - (barWidthONE_ *3))/4)*2 + barWidthONE_ * 1; //3.. Anzahl Bars, 4.. Abstände zwischen den Bars, 1.. für erte Bar, 0.. für erste Bar 
			int leftPointTWO = (int)Math.Round(leftPointTWO_);

			//Startpunkt festlegen 
			double startPointTWO_ = (double)this.Height-((double)this.Height*0.666*(double)percentTWO/100)-((double)this.Height *0.166);
			int startPointTWO     = (int)Math.Round(startPointTWO_);
			double barHeightTWO_  = (double)this.Height-(startPointTWO_+ (double)this.Height*0.166);
			int barHeightTWO      = (int) Math.Round(barHeightTWO_);
			
			if(barHeightTWO == 0) //Stelle sicher, dass die Bar immer einen gültige Höhe hat
				barHeightTWO = 1;
						
			if(percentTWO <0 || percentTWO > 100)  //Tue nichts gibts es nicht
			{
				//Zeichne die Bar nicht und beschrifte sie nicht
			}
			else
			{
				//Textgröße holen und Bar beschriften
				SizeF sizef1;
				sizef1 = grfx_.MeasureString(percentONE+"%",Font);
				float text1Pos = ((leftPointTWO + barWidthTWO/2) - (sizef1.Width/2));
				
				SizeF sizef2;
				sizef2 = grfx_.MeasureString(textTWO,Font);
				float text2Pos = (((float)leftPointTWO + (float)barWidthTWO/2) - (sizef2.Width/2));
				
				if(this.Width < 250 && this.Height < 150) //Wenn keine vernünftige größe mehr
				{
					//Beschrifte die Bar nicht zeichne sie aber
				}
				else
				{
					grfx_.DrawString(percentTWO +"%", Font, textBrush_, text1Pos, startPointTWO-(textMargin));        //Beschriftung um 20 pixel über der Bar
					grfx_.DrawString(textTWO, Font, textBrush_, text2Pos, (startPointTWO+barHeightTWO-1));  //Beschriftung um 8 pixel unter der Bar
				}

				if(percentONE == 0)
				{
					//Zeichne die Bar nicht beschrifte sie aber 
				}
				else
				{
					Rectangle rect = new Rectangle(leftPointTWO, startPointTWO, barWidthTWO, barHeightTWO);
					Brush gBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rect,(Color.FromArgb(alpha1TWO,color1TWO)),(Color.FromArgb(alpha2TWO,color2TWO)),90);
					grfx_.FillRectangle(gBrush,rect);
				}
			}

			//**********  BAR 3  **********//
			double barWidthTHREE_ = (double)this.Width*0.17;  //Barbreite Proportional zur Breite des Panels
			int barWidthTHREE = (int)Math.Round(barWidthTHREE_);

			//Abstand der DRITTEN Bar vom linken Rand. Es werden immer 3 Bars vorausgesetzt!!
			double leftPointTHREE_ = (((double)this.Width - (barWidthTHREE_ *3))/4)*3 + barWidthTHREE_ * 2; //3.. Anzahl Bars, 4.. Abstände zwischen den Bars, 3.. für dritte Bar, 2.. für dritte Bar 
			int leftPointTHREE = (int)Math.Round(leftPointTHREE_);

			
			//Startpunkt festlegen 
			double startPointTHREE_ = (double)this.Height-((double)this.Height*0.666*(double)percentTHREE/100)-((double)this.Height *0.166);
			int startPointTHREE     = (int)Math.Round(startPointTHREE_);
			double barHeightTHREE_  = (double)this.Height-(startPointTHREE_+ (double)this.Height*0.166);
			int barHeightTHREE      = (int) Math.Round(barHeightTHREE_);
			
			if(barHeightTHREE == 0) //Stelle sicher, dass die Bar immer einen gültige Höhe hat
				barHeightTHREE = 1;
						
			if(percentTHREE <0 || percentTHREE > 100)  //Tue nichts gibts es nicht
				return;
			else
			{
				//Textgröße bestimmen und Bar beschriften
				SizeF sizef1;
				sizef1 = grfx_.MeasureString(percentONE+"%",Font);
				float text1Pos = ((leftPointTHREE + barWidthTHREE/2) - (sizef1.Width/2));
				
				SizeF sizef2;
				sizef2 = grfx_.MeasureString(textTHREE,Font);
				float text2Pos = (((float)leftPointTHREE + (float)barWidthTHREE/2) - (sizef2.Width/2));

				if(this.Width < 250 && this.Height < 150) //Wenn keine vernünftige größe mehr
				{
					//Beschrifte die Bar nicht
				}
				else
				{
					grfx_.DrawString(percentTHREE +"%", Font, textBrush_, text1Pos, startPointTHREE-(textMargin));        //Beschriftung um 20 pixel über der Bar
					grfx_.DrawString(textTHREE, Font, textBrush_, text2Pos, (startPointTHREE+barHeightTHREE-1));  //Beschriftung um 8 pixel unter der Bar
				}

				if(percentONE == 0)
				{
					//Zeichne die Bar nicht beschrifte sie aber 
				}
				else
				{
					Rectangle rect = new Rectangle(leftPointTHREE, startPointTHREE, barWidthTHREE, barHeightTHREE);
					Brush gBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rect,(Color.FromArgb(alpha1THREE,color1THREE)),(Color.FromArgb(alpha2THREE,color2THREE)),90);
					grfx_.FillRectangle(gBrush,rect);
				}
			}
		} //end DrawBars
	}
}
