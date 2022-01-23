using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Reflection;
using System.Resources;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.UtilityLibrary.Win32;
using SoftObject.UtilityLibrary;



namespace SoftObject.SOComponents.Controls
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Notice.
	/// </summary>
	public class SONoticeCtrl : Panel
	{
        public delegate bool GetTitleCallback(ref string strTitle);
        public delegate bool CanDeleteCallback(string strTitle);

        private ListView m_lvwPages = new ListView();
		private Button []  m_aButtons = new Button[4];
		private const int c_lvwWidthPerc = 80;
		private ResourceHandler rh;
        private GetTitleCallback m_fnGetTitle;
        private CanDeleteCallback m_fnCanDelete;

		public delegate void NoticeHandler(object sender, ref NoticeEventArgs nea);
		public enum NoticeEventAction {New,Open,Delete,Rename,Import,Export};
		public class NoticeEventArgs : EventArgs 
		{
			private NoticeEventAction m_action;
			private int  m_listId;           // welcher Eintrag?
			private string m_title;
			private string m_fileName;

			public NoticeEventAction Action
			{
				get { return m_action; }
				set { m_action = value; }
			}

			public int ListId
			{
				get { return m_listId;}
				set { m_listId = value;}
			}

			public string Title
			{
				get {return m_title;}
				set {m_title = value;}
			}

			public string FileName
			{
				get {return m_fileName;}
				set {m_fileName = value;}
			}

			public NoticeEventArgs(NoticeEventAction action,int listId,string title)
			{
				m_action = action;
				m_listId = listId;
				m_title  = title;
				m_fileName = "";
			}

			public NoticeEventArgs(NoticeEventAction action,int listId,string title,string fileName)
			{
				m_action = action;
				m_listId = listId;
				m_title  = title;
				m_fileName = fileName;
			}
		}
		private event NoticeHandler NoticeEventHandler;

		public event NoticeHandler OnNotice
		{
			add { NoticeEventHandler += value;}
			remove { NoticeEventHandler -= value;}
		}

		public SONoticeCtrl(GetTitleCallback fnGetTitle,CanDeleteCallback fnCanDelete)
		{
            m_fnGetTitle = fnGetTitle;
            m_fnCanDelete = fnCanDelete;
#if(SKIN_EMCO)
			rh = new ResourceHandler("res.trainconcept.resources_emco",GetType().Assembly);
#elif(SKIN_WEBTRAIN)
			rh = new ResourceHandler("res.webtrain.resources_softobject",GetType().Assembly);
#else
			rh = new ResourceHandler("res.trainconcept.resources_emco",GetType().Assembly);
#endif
			CreateChildren();
		}

		public void CreateChildren()
		{
			Icon note = (Icon) rh.GetIcon("notice2");

			ImageList imgList = new ImageList();
			imgList.ImageSize = new Size(32,32);
			imgList.Images.Add(note);
			
			m_lvwPages.Parent = this;
			m_lvwPages.SmallImageList = imgList;
			m_lvwPages.LargeImageList = imgList;
			m_lvwPages.View = View.LargeIcon;
			m_lvwPages.Alignment = ListViewAlignment.Default;
			m_lvwPages.LabelEdit = true;
			m_lvwPages.LabelWrap = false;
			m_lvwPages.MultiSelect = false;

			m_lvwPages.ItemActivate += new EventHandler(OnItemActivate);
			m_lvwPages.SelectedIndexChanged += new System.EventHandler(OnSelectedIndexChanged);
			m_lvwPages.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(OnAfterLabelEdit);
			
			for(int i=0;i<m_aButtons.Length;++i)
			{
				m_aButtons[i] = new Button();
				m_aButtons[i].Parent = this;
				m_aButtons[i].Hide();
			}

			string exeFolder=Application.ExecutablePath.Substring(0,Application.ExecutablePath.LastIndexOf('\\'));
			ProfileHandler profHnd = new ProfileHandler(exeFolder+"\\TrainConcept.ini");
			string language=profHnd.GetProfileString("SYSTEM","Language","de");
			string workingFolder=profHnd.GetProfileString("SYSTEM","WorkingFolder",exeFolder);
			string languageFolder=profHnd.GetProfileString("SYSTEM","LanguageFolder",workingFolder);
			LanguageHandler langHnd= new LanguageHandler(languageFolder,"language",language);

			m_aButtons[0].Text = langHnd.GetText("FORMS","New","Neu");
			m_aButtons[0].Show();
			m_aButtons[0].Click += new System.EventHandler(OnBtnNew);
			
			m_aButtons[1].Text = langHnd.GetText("FORMS","Delete","Löschen");
			m_aButtons[1].Show();
			m_aButtons[1].Enabled=false;
			m_aButtons[1].Click += new System.EventHandler(OnBtnDelete);

			m_aButtons[2].Text = langHnd.GetText("FORMS","Import","Import");
			m_aButtons[2].Show();
			m_aButtons[2].Click += new System.EventHandler(OnBtnImport);

			m_aButtons[3].Text = langHnd.GetText("FORMS","Export","Export");
			m_aButtons[3].Show();
			m_aButtons[3].Enabled=false;
			m_aButtons[3].Click += new System.EventHandler(OnBtnExport);
			
			WindowsAPI.SendMessage(m_lvwPages.Handle,(int)ListViewMessages.LVM_SETICONSPACING,0,(50 * 65536) + (100 & 65535));
		}

		protected override void OnResize(EventArgs ea)
		{
			base.OnResize(ea);

			m_lvwPages.Size = new Size(ClientSize.Width*c_lvwWidthPerc/100,ClientSize.Height);

			for(int i=0;i<m_aButtons.Length;++i)
			{
				m_aButtons[i].Location = new Point(m_lvwPages.Size.Width,i*ClientSize.Height/m_aButtons.Length);
				m_aButtons[i].Size = new Size(ClientSize.Width-m_lvwPages.Size.Width,ClientSize.Height/m_aButtons.Length);
			}
		}

		public void SetNotice(int id,string title,string fileName)
		{
			if (id>m_lvwPages.Items.Count-1)
			{
				ListViewItem lvi=new ListViewItem(title,0);
				lvi.Tag = new NoticeEventArgs(NoticeEventAction.New,id,title,fileName);
				m_lvwPages.Items.Add(lvi);
			}
		}

		private void OnBtnNew(object sender, System.EventArgs e)
		{                
            string strTitle="";
            if (m_fnGetTitle(ref strTitle))
            {
                NoticeEventArgs nea = new NoticeEventArgs(NoticeEventAction.New, m_lvwPages.Items.Count + 1, strTitle, "");
                NoticeEventHandler(this, ref nea);
                ListViewItem lvi = new ListViewItem(nea.Title, 0);
                lvi.Tag = nea;
                m_lvwPages.Items.Add(lvi);
            }
	    }

		private void OnBtnDelete(object sender, System.EventArgs e)
		{
			if (m_lvwPages.SelectedItems!=null && m_lvwPages.SelectedItems.Count>0)
			{
				ListViewItem lvi = m_lvwPages.SelectedItems[0];
				if (lvi!=null)
				{
					NoticeEventArgs nea = (NoticeEventArgs)lvi.Tag;
					if (nea!=null)
					{
						nea.Action = NoticeEventAction.Delete;
						nea.ListId = m_lvwPages.SelectedIndices[0];
						NoticeEventHandler(this,ref nea);

						m_lvwPages.Items.RemoveAt(nea.ListId);

						if (m_lvwPages.Items.Count>0)
						{
							lvi = m_lvwPages.Items[m_lvwPages.Items.Count-1];
							lvi.Selected = true;
						}
						else
						{
							m_aButtons[1].Enabled=false;
							m_aButtons[3].Enabled=false;
						}
					}
				}
			}
		}

		private void OnBtnImport(object sender, System.EventArgs e)
		{
			NoticeEventArgs nea = new NoticeEventArgs(NoticeEventAction.Import,-1,"");
			nea.Action = NoticeEventAction.Import;
			NoticeEventHandler(this,ref nea);
		}

		private void OnBtnExport(object sender, System.EventArgs e)
		{
			if (m_lvwPages.SelectedItems!=null && m_lvwPages.SelectedItems.Count>0)
			{
				ListViewItem lvi = m_lvwPages.SelectedItems[0];
				if (lvi!=null)
				{
					NoticeEventArgs nea = (NoticeEventArgs)lvi.Tag;
					if (nea!=null)
					{
						nea.Action = NoticeEventAction.Export;
						nea.ListId = m_lvwPages.SelectedIndices[0];
						NoticeEventHandler(this,ref nea);
					}
				}
			}
		}

		private void OnItemActivate(object sender, System.EventArgs e)
		{
			ListViewItem lvi = m_lvwPages.SelectedItems[0];
			if (lvi!=null)
			{
				NoticeEventArgs nea = (NoticeEventArgs) lvi.Tag;
				if (nea!=null)
				{
					nea.Action = NoticeEventAction.Open;
					nea.ListId = m_lvwPages.SelectedIndices[0];
					NoticeEventHandler(this,ref nea);
				}
			}
		}

		private void OnSelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (m_lvwPages.SelectedItems.Count>0)
			{
                bool bCanDelete=true;
				ListViewItem lvi = m_lvwPages.SelectedItems[0];
                if (lvi != null)
                {
                    NoticeEventArgs nea = (NoticeEventArgs)lvi.Tag;
                    if (nea != null)
                    {
                        if (this.m_fnCanDelete != null)
                            bCanDelete = m_fnCanDelete(nea.Title);
                    }
                }

				m_aButtons[1].Enabled = bCanDelete;
                m_aButtons[3].Enabled = bCanDelete;
			}
			else
			{
				m_aButtons[1].Enabled = false;
				m_aButtons[3].Enabled = false;
			}
		}
				
		private void OnAfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
		{
			ListViewItem lvi = m_lvwPages.SelectedItems[0];
			if (lvi!=null)
			{
				NoticeEventArgs nea = (NoticeEventArgs) lvi.Tag;
				if (nea!=null)
				{
					nea.Title = e.Label;
					nea.Action = NoticeEventAction.Rename;
					nea.ListId = m_lvwPages.SelectedIndices[0];
					NoticeEventHandler(this,ref nea);
				}
			}
		} 

	}
}
