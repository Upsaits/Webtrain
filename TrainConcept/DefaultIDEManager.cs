using System;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using DevComponents.DotNetBar;
using SoftObject.TrainConcept.Interfaces;


namespace SoftObject.TrainConcept
{
	/// <summary>
	/// Summary description for IDEManager.
	/// </summary>
	/// 
    public class DefaultIDEManager : IIDEManager
	{
		public  bool Create(Form form,System.ComponentModel.IContainer components)
		{
			return true;
		}

		public  void AddImageList(Form form,ImageList imageList)
		{
		}

		public  Bar CreateDockingBar(Form form,string name)
		{
            return null;
		}

		public  Bar CreateMenuBar(Form form,string menuName,bool isMainMenu)
		{
            return null;
		}

		public  Bar CreateMainMenu(Form form,string menuName)
		{
            return null;
		}

		public  Bar CreateStandardMenu(Form form,string menuName)
		{
            return null;
		}
		

		public  void AddDockingBarItem(Form form,System.Windows.Forms.UserControl control,Bar bar,string name)
		{
		}

		public  void AddDockingBar(Form form,Bar bar)
		{
		}

		public  object CreateSideBar(string name,ImageList imageList)
		{
            return null;
		}

		public object CreateExplorerBar(string name,ImageList lGroupImages,ImageList lBtnImages)
		{
			ExplorerBar explorerBar = new ExplorerBar();

            explorerBar.BackColor = System.Drawing.SystemColors.Control;
            explorerBar.BackStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ExplorerBarBackground2;
            explorerBar.BackStyle.BackColorGradientAngle = 90;
            explorerBar.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			explorerBar.Dock = DockStyle.Fill;
			explorerBar.GroupImages = lGroupImages;
			explorerBar.Images = lBtnImages;
			explorerBar.Font = Program.AppHandler.DefaultFont;
			explorerBar.AllowDrop = false;
			return explorerBar;
		}

		public  object AddSideBarPanel(object sideBar,
									   string name,string text,
									   string tooltipText)
		{
			SideBar l_sideBar = sideBar as SideBar;
			SideBarPanelItem panelItem = new SideBarPanelItem();
			panelItem.Name = name;
			panelItem.Text = text;
			panelItem.Tooltip = tooltipText;
			l_sideBar.Panels.Add(panelItem);
			return panelItem;									
		}

		public  object AddExplorerBarGroup(object sideBar,
										   string name,string text,
										   string tooltipText,
										   int imageListId)
		{
			ExplorerBar l_sideBar = sideBar as ExplorerBar;
			ExplorerBarGroupItem expBarGrpItem = new ExplorerBarGroupItem();

            expBarGrpItem.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((System.Byte)(63)), ((System.Byte)(61)), ((System.Byte)(61)));
            expBarGrpItem.TitleStyle.TextTrimming = DevComponents.DotNetBar.eStyleTextTrimming.EllipsisCharacter;
			expBarGrpItem.ImageIndex = imageListId;
			expBarGrpItem.Name = name;
			expBarGrpItem.Text = text;
			expBarGrpItem.Tooltip = tooltipText;
            if (imageListId==0)
                expBarGrpItem.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.OliveGreenSpecial;
            else if (imageListId == 2)
                expBarGrpItem.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.BlueSpecial;
            else
                expBarGrpItem.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Silver;
            
            expBarGrpItem.CanCustomize = false;
			l_sideBar.Groups.Add(expBarGrpItem);
			return expBarGrpItem;
		}

		public  object AddSideBarButton(object panelItem,
										string name,
										string text,
										string tooltipText,
										int imageListId)
		{
			SideBarPanelItem l_panelItem = panelItem as SideBarPanelItem;

			ButtonItem buttonItem = new ButtonItem();

			l_panelItem.ItemImageSize = DevComponents.DotNetBar.eBarImageSize.Medium;
			l_panelItem.SubItems.Add(buttonItem);

			buttonItem.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			buttonItem.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
			buttonItem.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			buttonItem.ImageIndex = imageListId;
			buttonItem.Name = name;
			buttonItem.Text = text;
			buttonItem.Tooltip = tooltipText;
			buttonItem.CanCustomize=false;
			
			return buttonItem;
		}


		public void RemoveSideBarButton(object panelItem,string text)
		{
			SideBarPanelItem l_panelItem = panelItem as SideBarPanelItem;

            if (l_panelItem != null && l_panelItem.SubItems != null)
			for(int i=0;i<l_panelItem.SubItems.Count;++i)
				if (String.Compare(text,l_panelItem.SubItems[i].Text,true)==0)
				{
					l_panelItem.SubItems.Remove(i);
					return;
				}
			
		}

        public bool HasSideBarButton(object panelItem, string text)
        {
            SideBarPanelItem l_panelItem = panelItem as SideBarPanelItem;

            if (l_panelItem != null && l_panelItem.SubItems != null)
                for (int i = 0; i < l_panelItem.SubItems.Count; ++i)
                    if (String.Compare(text, l_panelItem.SubItems[i].Text, true) == 0)
                        return true;
            return false;
        }

        public object AddExplorerBarButton(object panelItem,
                                    string name,
                                    string text,
                                    string tooltipText,
                                    int imageListId)
        {
            ExplorerBarGroupItem l_panelItem = panelItem as ExplorerBarGroupItem;

            ButtonItem buttonItem = new ButtonItem();
            RadialMenuItem radMenItem = new RadialMenuItem();

            l_panelItem.SubItems.Add(buttonItem);
            l_panelItem.SubItems.Add(radMenItem);

            if (imageListId >= 0)
            {
                buttonItem.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
                buttonItem.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
                buttonItem.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
                buttonItem.ImageIndex = imageListId;
                buttonItem.Name = name;
                buttonItem.Text = text;
                buttonItem.Tooltip = tooltipText;
            }
            else
            {
                buttonItem.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
                buttonItem.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Default;
                buttonItem.Name = name;
                buttonItem.Text = text;
                buttonItem.Tooltip = tooltipText;
                buttonItem.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
                buttonItem.FontBold = true;
                buttonItem.HotFontBold = true;
                buttonItem.HotForeColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(10)), ((System.Byte)(0)));
            }
            buttonItem.CanCustomize = false;
            return buttonItem;
        }

		public void RemoveExplorerBarButton(object panelItem,string text)
		{
			ExplorerBarGroupItem l_panelItem = panelItem as ExplorerBarGroupItem;

            if (l_panelItem != null && l_panelItem.SubItems != null)
			for(int i=0;i<l_panelItem.SubItems.Count;++i)
				if (String.Compare(text,l_panelItem.SubItems[i].Text,true)==0)
				{
					l_panelItem.SubItems.Remove(i);
					return;
				}
		}

        public bool SetExplorerBarButtonImageId(object panelItem, string text, int imageListId)
        {
            ExplorerBarGroupItem l_panelItem = panelItem as ExplorerBarGroupItem;

            if (l_panelItem != null && l_panelItem.SubItems != null)
                for (int i = 0; i < l_panelItem.SubItems.Count; ++i)
                    if (String.Compare(text, l_panelItem.SubItems[i].Text, true) == 0)
                    {
                        ((ButtonItem) l_panelItem.SubItems[i]).ImageIndex = imageListId;
                        l_panelItem.SubItems[i].Refresh();
                        return true;
                    }

            return false;
        }


		public bool HasExplorerBarButton(object panelItem,string text)
		{
			ExplorerBarGroupItem l_panelItem = panelItem as ExplorerBarGroupItem;

            if (l_panelItem != null && l_panelItem.SubItems != null)
			for(int i=0;i<l_panelItem.SubItems.Count;++i)
				if (String.Compare(text,l_panelItem.SubItems[i].Text,true)==0)
					return true;
			return false;
		}

        /*
        public object AddExplorerBarRadialMenuButton(object panelItem,
                                                     string name,
                                                     string text,
                                                     string tooltipText,
                                                     ImageList imgList,
                                                     int imageListId)
        {
            ExplorerBarGroupItem l_panelItem = panelItem as ExplorerBarGroupItem;


            RadialMenuItem radMenItem = new RadialMenuItem();
            radMenItem.Symbol = "\uf040";
            radMenItem.Diameter = 220; // Make menu diameter larger

            l_panelItem.SubItems.Add(radMenItem);

            //radMenItem.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            //radMenItem.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            //radMenItem.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            radMenItem.Image = imgList.Images[imageListId];
            radMenItem.Name = name;
            radMenItem.Text = text;
            radMenItem.Tooltip = tooltipText;

            return radMenItem;
        }*/
    }

}
