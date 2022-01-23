using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SoftObject.TrainConcept.Interfaces;

namespace SoftObject.TrainConcept
{
    public class IDEManagerBridge
    {
        private IIDEManager m_imp;

        public IDEManagerBridge(IIDEManager imp)
        {
            m_imp = imp;
        }

        public bool Create(Form form, System.ComponentModel.IContainer components)
        {
            return m_imp.Create(form, components);
        }
        public void AddImageList(Form form, ImageList imageList)
        {
            m_imp.AddImageList(form, imageList);
        }

        public Bar CreateDockingBar(Form form, string name)
        {
            return m_imp.CreateDockingBar(form, name);
        }

        public Bar CreateMenuBar(Form form, string menuName, bool isMainMenu)
        {
            return m_imp.CreateMenuBar(form, menuName, isMainMenu);
        }

        public Bar CreateMainMenu(Form form, string menuName)
        {
            return m_imp.CreateMainMenu(form, menuName);
        }

        public Bar CreateStandardMenu(Form form, string menuName)
        {
            return m_imp.CreateStandardMenu(form, menuName);
        }

        public void AddDockingBarItem(Form form, System.Windows.Forms.UserControl control, Bar bar, string name)
        {
            m_imp.AddDockingBarItem(form, control, bar, name);
        }

        public void AddDockingBar(Form form, Bar bar)
        {
            m_imp.AddDockingBar(form, bar);
        }

        public object CreateSideBar(string name, ImageList imageList)
        {
            return m_imp.CreateSideBar(name, imageList);
        }

        public object AddSideBarPanel(object sideBar,
                                      string name, string text,
                                      string tooltipText)
        {
            return m_imp.AddSideBarPanel(sideBar, name, text, tooltipText);
        }

        public object AddSideBarButton(object panelItem,
                                       string name,
                                       string text,
                                       string tooltipText,
                                       int imageListId)
        {
            return m_imp.AddSideBarButton(panelItem, name, text, tooltipText, imageListId);
        }

        public void RemoveSideBarButton(object panelItem, string text)
        {
            m_imp.RemoveSideBarButton(panelItem, text);
        }

        public bool HasSideBarButton(object panelItem, string text)
        {
            return m_imp.HasSideBarButton(panelItem, text);
        }

        public object CreateExplorerBar(string name, ImageList lGroupImages, ImageList lBtnImages)
        {
            return m_imp.CreateExplorerBar(name, lGroupImages, lBtnImages);
        }

        public object AddExplorerBarGroup(object sideBar, string name, string text, string tooltipText, int imageListId)
        {
            return m_imp.AddExplorerBarGroup(sideBar, name, text, tooltipText, imageListId);
        }

        public object AddExplorerBarButton(object panelItem, string name, string text, string tooltipText, int imageListId)
        {
            return m_imp.AddExplorerBarButton(panelItem, name, text, tooltipText, imageListId);
        }

        public void RemoveExplorerBarButton(object panelItem, string text)
        {
            m_imp.RemoveExplorerBarButton(panelItem, text);
        }

        public bool SetExplorerBarButtonImageId(object panelItem, string text, int imageListId)
        {
            return m_imp.SetExplorerBarButtonImageId(panelItem, text, imageListId);
        }

        public bool HasExplorerBarButton(object panelItem, string text)
        {
            return m_imp.HasExplorerBarButton(panelItem, text);
        }
    }
}
