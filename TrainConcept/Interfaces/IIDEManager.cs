using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SoftObject.TrainConcept.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IIDEManager
	{
		bool Create(Form form,System.ComponentModel.IContainer components);
		void AddImageList(Form form,ImageList imageList);
		Bar  CreateDockingBar(Form form,string name);
		Bar  CreateMenuBar(Form form,string menuName,bool isMainMenu);
		Bar  CreateMainMenu(Form form,string menuName);
		Bar  CreateStandardMenu(Form form,string menuName);
		void AddDockingBarItem(Form form,System.Windows.Forms.UserControl control,Bar bar,string name);
		void AddDockingBar(Form form,Bar bar);
		object CreateSideBar(string name,ImageList imageList);
		object AddSideBarPanel(object sideBar,
							   string name,string text,
							   string tooltipText);
		object AddSideBarButton(object panelItem,
								string name,
								string text,
								string tooltipText,
								int imageListId);
		void RemoveSideBarButton(object panelItem,string text);
		bool HasSideBarButton(object panelItem,string text);
		object CreateExplorerBar(string name,ImageList lGroupImages,ImageList lBtnImages);
		object AddExplorerBarGroup(object sideBar,string name,string text,string tooltipText,int imageListId);
		object AddExplorerBarButton(object panelItem,string name,string text,string tooltipText,int imageListId);
		void RemoveExplorerBarButton(object panelItem,string text);
		bool HasExplorerBarButton(object panelItem,string text);
        bool SetExplorerBarButtonImageId(object panelItem, string text, int imageListId);
    }
}