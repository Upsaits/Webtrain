using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SoftObject.SOComponents.UtilityLibrary;


namespace SoftObject.SOComponents.Controls
{
	public class SOGlossar  : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.PictureBox pictureBoxAlphabet; //PictureBox die nur A..Z zeigt
		private System.Windows.Forms.ListBox listBoxGlossar;   //ListBox Stichwörter
		private System.Windows.Forms.Label labelGlossar;       //Label Anzeige Text aus Stichwörtern
		private ArrayList alphabetImageLists=new ArrayList(); //Arraylist für links oben angezeigten Buchstaben A..Z
		private ArrayList glossarImageLists=new ArrayList();
		private ImageList alphabetImageList;  //ImageList füe die links oben angezeigten Buchstaben A..Z
		private ImageList btnImageList;
		private SORadioBmpBtn glossarBtn;
		private int btnWidth;
		private int btnHeight;
		private char selectedChar;
		private Font font = new Font("Arial",10,FontStyle.Regular);
		private ResourceHandler rh;
		private bool wasDblClk=false;

		public char SelectedChar
		{
			get{return selectedChar;}
		}

		private event GlossarHandler GlossarEventHandler;
		public event GlossarHandler OnGlossar
		{
			add { GlossarEventHandler += value;}
			remove { GlossarEventHandler -= value;}
		}


		public SOGlossar()
		{
			InitializeComponent();
#if(SKIN_EMCO)
			rh = new ResourceHandler("res.trainconcept.resources_emco",GetType().Assembly);
#elif(SKIN_WEBTRAIN)
			rh = new ResourceHandler("res.webtrain.resources_softobject",GetType().Assembly);
#else
			rh = new ResourceHandler("res.trainconcept.resources_emco",GetType().Assembly);
#endif

			SuspendLayout();
			CreateImageLists();
			CreateButtons();
			CreatePictureBox();           //Anzeige welcher Buchstabe gewählt wurde
			CreateListBoxGlossar();  //Anzeige Stichwörter
			CreateLabelGlossar();    //Anzeige Text aus Stichwörtern
			ResumeLayout();

			Bitmap bmpBackground = new Bitmap(rh.GetBitmap("glossar"));
			Size = new Size(bmpBackground.Width,bmpBackground.Height);
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics grfx = e.Graphics;

			//Bitmap bmpBackground = new Bitmap(GetType(),"Images.Glossar.glossar.gif");
			Bitmap bmpBackground = new Bitmap(rh.GetBitmap("glossar"));
			grfx.DrawImage(bmpBackground,0,0,bmpBackground.Width,bmpBackground.Height);
			this.Size = new Size(bmpBackground.Width,bmpBackground.Height);
			this.BackColor = Color.FromArgb(214,221,228);
			//this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		}

		
		private void CreateImageLists()
		{
			//Hole dir höhe und breite eines Buttons
			Bitmap bmp = new Bitmap (rh.GetBitmap("a_normal"));
			btnWidth = bmp.Width;
			btnHeight = bmp.Height;

			for(char alphabet='a'; alphabet<='z'; alphabet++) //Hole alle Images für a..z
			{
				alphabetImageList = new ImageList();
				alphabetImageList.ImageSize = new Size(35,35);
				alphabetImageList.ColorDepth = ColorDepth.Depth24Bit;
				
				btnImageList = new ImageList();
				btnImageList.ImageSize = new Size(35,35);
				btnImageList.ColorDepth = ColorDepth.Depth24Bit;
				btnImageList.Images.Add(new Bitmap(rh.GetBitmap(alphabet+"_normal")));
				btnImageList.Images.Add(new Bitmap(rh.GetBitmap(alphabet+"_over")));
				btnImageList.Images.Add(new Bitmap(rh.GetBitmap(alphabet+"_pressed")));

				alphabetImageList.Images.Add(new Bitmap(rh.GetBitmap(alphabet+"_key")));
				
				alphabetImageLists.Add(alphabetImageList);
				glossarImageLists.Add(btnImageList);
			}
		}
		
		private void CreateButtons()
		{
			int startPointX = 12;			//Anfängliche Anfangsposition Buttons
			int startPointY = 355;
			int newPointX = startPointX;	//Neue Position Buttons
			int newPointY = startPointY;	
			int spaceV = -1;				//Vertikaler Abstand zwischen den Buttons
			int spaceH = 2;					//Horizontaler Abstand zwischen den Buttons
			int i = 0;
			
			for(char alphabet='a'; alphabet<='z'; alphabet++)
			{
				if(alphabet == 'n') //Umbruch bei Buchstabe 'm'
				{
					newPointX = startPointX;
					newPointY = startPointY +btnHeight + spaceV;
				}
				
				ImageList aImages = (ImageList)glossarImageLists[i];
				glossarBtn = new SORadioBmpBtn(aImages,0,aImages.Images[0].Width+3,aImages.Images[0].Height+3);
				glossarBtn.Click += new EventHandler(AlphabetBtn_Click);
				glossarBtn.Location = new Point(newPointX,newPointY);
				glossarBtn.AutoCheck = true; //true für bleibt gedrückt
				glossarBtn.Text = "";
				glossarBtn.TabStop = true;
				glossarBtn.TabIndex = i;
				newPointX = newPointX+btnWidth+spaceH;
				this.Controls.AddRange(new System.Windows.Forms.Control[] {glossarBtn});
				i++;
			}
		}


		private void AlphabetBtn_Click(object sender, EventArgs e)
		{
			int btnIndex;	 
			int charIndex=0; 
			
			btnIndex = this.Controls.IndexOf((Control)sender); //Bestimme den Index des Buttons

			for(char alphabet='A'; alphabet<='Z'; alphabet++)
			{
				if(btnIndex == charIndex)
				{
					selectedChar = alphabet;
					//labelSelectedChar.Text = "Index für  "+selectedChar;
					alphabetImageList = (ImageList)alphabetImageLists[btnIndex];
					pictureBoxAlphabet.Image = alphabetImageList.Images[0];
					break;
				}
				charIndex++;
			}

			listBoxGlossar.Items.Clear();
			labelGlossar.Text = "";

			GlossarEventArgs gea = new GlossarEventArgs(selectedChar);
			if (GlossarEventHandler!=null)
				GlossarEventHandler(this,ref gea);

			if (gea.Keywords != null && gea.Keywords.Count>0)
			{
				for(int i=0;i<gea.Keywords.Count;++i)
					listBoxGlossar.Items.Add((string) gea.Keywords[i]);
				listBoxGlossar.SelectedIndex=0;
			}
		}

	
		private void CreatePictureBox() //Anzeige A..Z
		{
			this.pictureBoxAlphabet = new System.Windows.Forms.PictureBox();
			this.pictureBoxAlphabet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxAlphabet.Location = new System.Drawing.Point(35, 23);
			this.pictureBoxAlphabet.Name = "pictureBoxAlphabet";
			this.pictureBoxAlphabet.TabStop = false;
			
			this.Controls.Add(pictureBoxAlphabet);
		}

		private void CreateListBoxGlossar()
		{
			listBoxGlossar = new ListBox();
			listBoxGlossar.Font = font;
			listBoxGlossar.BackColor = Color.White;
			listBoxGlossar.Location = new System.Drawing.Point(39, 60);
			listBoxGlossar.Name = "listBoxGlossar";
			listBoxGlossar.Size = new System.Drawing.Size(178, 230);
			listBoxGlossar.BorderStyle = System.Windows.Forms.BorderStyle.None;
			listBoxGlossar.TabStop = true;
			listBoxGlossar.TabIndex = 27;
			listBoxGlossar.SelectedIndexChanged += new EventHandler(listBox_SelectedIndexChanged);
			listBoxGlossar.DoubleClick += new EventHandler(listbox_ItemDblClick);
			listBoxGlossar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseUp);
			
			this.Controls.Add(listBoxGlossar);
		}

		private void CreateLabelGlossar()//Anzeige Text aus Stichwort
		{
			labelGlossar = new Label();
			labelGlossar.Font = font;
			labelGlossar.Location = new System.Drawing.Point(290,40);
			labelGlossar.Name = "labelGlossar";
			labelGlossar.Size = new System.Drawing.Size(175,241);
			labelGlossar.BackColor = Color.White;

			this.Controls.Add(labelGlossar);
		}

		private void listBox_SelectedIndexChanged(object sender, EventArgs ea)
		{
			GlossarEventArgs gea = new GlossarEventArgs((string) listBoxGlossar.SelectedItem,false);

			if (GlossarEventHandler!=null)
				GlossarEventHandler(this,ref gea);
			
			labelGlossar.Text = gea.Description;
		}

		public void listbox_ItemDblClick(object sender,EventArgs ea)
		{
			wasDblClk=true;
		}

		private void listBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (wasDblClk)
			{
				wasDblClk=false;
				GlossarEventArgs gea = new GlossarEventArgs((string) listBoxGlossar.SelectedItem,true);
				GlossarEventHandler(this,ref gea);
			}
		} 

		private void InitializeComponent()
		{
            this.SuspendLayout();
			this.Load += new System.EventHandler(this.Glossar_Load);
			this.ResumeLayout(false);
		}

		private void Glossar_Load(object sender, System.EventArgs e)
		{
			AlphabetBtn_Click(this.Controls[0],EventArgs.Empty);
		}
	}

    public delegate void GlossarHandler(object sender, ref GlossarEventArgs gea);
    public class GlossarEventArgs : EventArgs
    {
        private char m_char;	           // welcher Eintrag?
        private string m_keyword;
        private string m_description;
        private ArrayList m_aKeywords;
        private bool m_gotoKeyword;

        public char Char
        {
            get { return m_char; }
            set { m_char = value; }
        }

        public string Keyword
        {
            get { return m_keyword; }
            set { m_keyword = value; }
        }

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public ArrayList Keywords
        {
            get { return m_aKeywords; }
            set { m_aKeywords = value; }
        }

        public bool GotoKeyword
        {
            get { return m_gotoKeyword; }
            set { m_gotoKeyword = value; }
        }

        public GlossarEventArgs(char _char)
        {
            m_char = _char;
            m_keyword = "";
            m_aKeywords = null;
            m_gotoKeyword = false;
        }

        public GlossarEventArgs(string _keyword, bool gotoKeyword)
        {
            m_char = ' ';
            m_keyword = _keyword;
            m_description = "";
            m_aKeywords = null;
            m_gotoKeyword = gotoKeyword;
        }
    }

}
