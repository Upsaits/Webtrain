using DevComponents.DotNetBar;

namespace SoftObject.TrainConcept.Forms
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.m_imageList = new System.Windows.Forms.ImageList(this.components);
            this.panStatus = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.dotNetBarManager1 = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.barBottomDockSite = new DevComponents.DotNetBar.DockSite();
            this.barLeftDockSite = new DevComponents.DotNetBar.DockSite();
            this.barNoticeBar = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer1 = new DevComponents.DotNetBar.PanelDockContainer();
            this.NoticeItem = new DevComponents.DotNetBar.DockContainerItem();
            this.barControlCenter = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer2 = new DevComponents.DotNetBar.PanelDockContainer();
            this.ControlCenterItem = new DevComponents.DotNetBar.DockContainerItem();
            this.barRightDockSite = new DevComponents.DotNetBar.DockSite();
            this.barNavigatorBar = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer3 = new DevComponents.DotNetBar.PanelDockContainer();
            this.NavigatorItem = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSite4 = new DevComponents.DotNetBar.DockSite();
            this.dockSite1 = new DevComponents.DotNetBar.DockSite();
            this.dockSite2 = new DevComponents.DotNetBar.DockSite();
            this.dockSite3 = new DevComponents.DotNetBar.DockSite();
            this.barMainMenu = new DevComponents.DotNetBar.Bar();
            this.bSystem = new DevComponents.DotNetBar.ButtonItem();
            this.bRegister = new DevComponents.DotNetBar.ButtonItem();
            this.bRegisterAs = new DevComponents.DotNetBar.ButtonItem();
            this.bAbout = new DevComponents.DotNetBar.ButtonItem();
            this.bHelp = new DevComponents.DotNetBar.ButtonItem();
            this.bExit = new DevComponents.DotNetBar.ButtonItem();
            this.bUpdates = new DevComponents.DotNetBar.ButtonItem();
            this.cpUpdates = new DevComponents.DotNetBar.CircularProgressItem();
            this.barCloseBar = new DevComponents.DotNetBar.Bar();
            this.bClose = new DevComponents.DotNetBar.ButtonItem();
            this.barToolBar = new DevComponents.DotNetBar.Bar();
            this.bRegister2 = new DevComponents.DotNetBar.ButtonItem();
            this.bPrint = new DevComponents.DotNetBar.ButtonItem();
            this.bNoticeCenter = new DevComponents.DotNetBar.ButtonItem();
            this.bMessages = new DevComponents.DotNetBar.ButtonItem();
            this.bLibOverview = new DevComponents.DotNetBar.ButtonItem();
            this.bGlossary = new DevComponents.DotNetBar.ButtonItem();
            this.bStartSim = new DevComponents.DotNetBar.ButtonItem();
            this.barTopDockSite = new DevComponents.DotNetBar.DockSite();
            this.messageTimer = new System.Windows.Forms.Timer(this.components);
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.bkgworkerClientLogin = new System.ComponentModel.BackgroundWorker();
            this.bkgworkerServerLogin = new System.ComponentModel.BackgroundWorker();
            this.connectionTimer = new System.Windows.Forms.Timer(this.components);
            this.transferTimer = new System.Windows.Forms.Timer(this.components);
            this.bkgWorkerStandAloneLogin = new System.ComponentModel.BackgroundWorker();
            this.panStatus.SuspendLayout();
            this.barLeftDockSite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barNoticeBar)).BeginInit();
            this.barNoticeBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barControlCenter)).BeginInit();
            this.barControlCenter.SuspendLayout();
            this.barRightDockSite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barNavigatorBar)).BeginInit();
            this.barNavigatorBar.SuspendLayout();
            this.dockSite3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barMainMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barCloseBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barToolBar)).BeginInit();
            this.SuspendLayout();
            // 
            // m_imageList
            // 
            this.m_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageList.ImageStream")));
            this.m_imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.m_imageList.Images.SetKeyName(0, "register.bmp");
            this.m_imageList.Images.SetKeyName(1, "printdisabled.bmp");
            this.m_imageList.Images.SetKeyName(2, "print.bmp");
            this.m_imageList.Images.SetKeyName(3, "notice.bmp");
            this.m_imageList.Images.SetKeyName(4, "pin.bmp");
            this.m_imageList.Images.SetKeyName(5, "mesgclose.bmp");
            this.m_imageList.Images.SetKeyName(6, "mesgopen.bmp");
            this.m_imageList.Images.SetKeyName(7, "Compass-32.png");
            this.m_imageList.Images.SetKeyName(8, "glossar.bmp");
            this.m_imageList.Images.SetKeyName(9, "help.bmp");
            this.m_imageList.Images.SetKeyName(10, "startsim.bmp");
            this.m_imageList.Images.SetKeyName(11, "Close-32.png");
            this.m_imageList.Images.SetKeyName(12, "Updates.bmp");
            this.m_imageList.Images.SetKeyName(13, "Depositphotos_205963030_s-2019.jpg");
            // 
            // panStatus
            // 
            this.panStatus.Controls.Add(this.lblUserName);
            this.panStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panStatus.Location = new System.Drawing.Point(146, 540);
            this.panStatus.Name = "panStatus";
            this.panStatus.Size = new System.Drawing.Size(734, 22);
            this.panStatus.TabIndex = 3;
            // 
            // lblUserName
            // 
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUserName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.Black;
            this.lblUserName.Location = new System.Drawing.Point(0, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(554, 22);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dotNetBarManager1
            // 
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this.dotNetBarManager1.BottomDockSite = this.barBottomDockSite;
            this.dotNetBarManager1.Images = this.m_imageList;
            this.dotNetBarManager1.LeftDockSite = this.barLeftDockSite;
            this.dotNetBarManager1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.dotNetBarManager1.ParentForm = this;
            this.dotNetBarManager1.PopupAnimation = DevComponents.DotNetBar.ePopupAnimation.Fade;
            this.dotNetBarManager1.RightDockSite = this.barRightDockSite;
            this.dotNetBarManager1.ShowCustomizeContextMenu = false;
            this.dotNetBarManager1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.dotNetBarManager1.ToolbarBottomDockSite = this.dockSite4;
            this.dotNetBarManager1.ToolbarLeftDockSite = this.dockSite1;
            this.dotNetBarManager1.ToolbarRightDockSite = this.dockSite2;
            this.dotNetBarManager1.ToolbarTopDockSite = this.dockSite3;
            this.dotNetBarManager1.TopDockSite = this.barTopDockSite;
            this.dotNetBarManager1.ItemClick += new System.EventHandler(this.OnItemClick);
            // 
            // barBottomDockSite
            // 
            this.barBottomDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barBottomDockSite.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barBottomDockSite.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.barBottomDockSite.Location = new System.Drawing.Point(0, 562);
            this.barBottomDockSite.Name = "barBottomDockSite";
            this.barBottomDockSite.Size = new System.Drawing.Size(1089, 0);
            this.barBottomDockSite.TabIndex = 10;
            this.barBottomDockSite.TabStop = false;
            // 
            // barLeftDockSite
            // 
            this.barLeftDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barLeftDockSite.Controls.Add(this.barNoticeBar);
            this.barLeftDockSite.Controls.Add(this.barControlCenter);
            this.barLeftDockSite.Dock = System.Windows.Forms.DockStyle.Left;
            this.barLeftDockSite.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barNoticeBar, 70, 476))),
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barControlCenter, 70, 476)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
            this.barLeftDockSite.Location = new System.Drawing.Point(0, 86);
            this.barLeftDockSite.Name = "barLeftDockSite";
            this.barLeftDockSite.Size = new System.Drawing.Size(146, 476);
            this.barLeftDockSite.TabIndex = 7;
            this.barLeftDockSite.TabStop = false;
            // 
            // barNoticeBar
            // 
            this.barNoticeBar.AccessibleDescription = "DotNetBar Bar (barNoticeBar)";
            this.barNoticeBar.AccessibleName = "DotNetBar Bar";
            this.barNoticeBar.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.barNoticeBar.BarType = DevComponents.DotNetBar.eBarType.DockWindow;
            this.barNoticeBar.CanHide = true;
            this.barNoticeBar.Controls.Add(this.panelDockContainer1);
            this.barNoticeBar.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.barNoticeBar.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.barNoticeBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.NoticeItem});
            this.barNoticeBar.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.barNoticeBar.Location = new System.Drawing.Point(0, 0);
            this.barNoticeBar.Name = "barNoticeBar";
            this.barNoticeBar.Size = new System.Drawing.Size(70, 476);
            this.barNoticeBar.Stretch = true;
            this.barNoticeBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.barNoticeBar.TabIndex = 0;
            this.barNoticeBar.TabStop = false;
            this.barNoticeBar.Text = "Notizen";
            this.barNoticeBar.Visible = false;
            this.barNoticeBar.WrapItemsFloat = false;
            // 
            // panelDockContainer1
            // 
            this.panelDockContainer1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.panelDockContainer1.Location = new System.Drawing.Point(3, 23);
            this.panelDockContainer1.Name = "panelDockContainer1";
            this.panelDockContainer1.Size = new System.Drawing.Size(64, 450);
            this.panelDockContainer1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer1.Style.GradientAngle = 90;
            this.panelDockContainer1.TabIndex = 0;
            // 
            // NoticeItem
            // 
            this.NoticeItem.Control = this.panelDockContainer1;
            this.NoticeItem.GlobalItem = true;
            this.NoticeItem.GlobalName = "NoticeItem";
            this.NoticeItem.Name = "NoticeItem";
            this.NoticeItem.Text = "Notizen";
            // 
            // barControlCenter
            // 
            this.barControlCenter.AccessibleDescription = "DotNetBar Bar (barControlCenter)";
            this.barControlCenter.AccessibleName = "DotNetBar Bar";
            this.barControlCenter.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.barControlCenter.BarType = DevComponents.DotNetBar.eBarType.DockWindow;
            this.barControlCenter.CanDockBottom = false;
            this.barControlCenter.CanDockTop = false;
            this.barControlCenter.Controls.Add(this.panelDockContainer2);
            this.barControlCenter.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.barControlCenter.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.barControlCenter.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ControlCenterItem});
            this.barControlCenter.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.barControlCenter.Location = new System.Drawing.Point(73, 0);
            this.barControlCenter.MinimumSize = new System.Drawing.Size(150, 0);
            this.barControlCenter.Name = "barControlCenter";
            this.barControlCenter.Size = new System.Drawing.Size(150, 476);
            this.barControlCenter.Stretch = true;
            this.barControlCenter.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.barControlCenter.TabIndex = 1;
            this.barControlCenter.TabStop = false;
            this.barControlCenter.Text = "ControlCenter";
            this.barControlCenter.Visible = false;
            this.barControlCenter.WrapItemsFloat = false;
            // 
            // panelDockContainer2
            // 
            this.panelDockContainer2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.panelDockContainer2.Location = new System.Drawing.Point(3, 23);
            this.panelDockContainer2.Name = "panelDockContainer2";
            this.panelDockContainer2.Size = new System.Drawing.Size(64, 450);
            this.panelDockContainer2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer2.Style.GradientAngle = 90;
            this.panelDockContainer2.TabIndex = 0;
            // 
            // ControlCenterItem
            // 
            this.ControlCenterItem.Control = this.panelDockContainer2;
            this.ControlCenterItem.GlobalItem = true;
            this.ControlCenterItem.GlobalName = "ControlCenterItem";
            this.ControlCenterItem.Name = "ControlCenterItem";
            this.ControlCenterItem.Text = "ControlCenter";
            // 
            // barRightDockSite
            // 
            this.barRightDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barRightDockSite.Controls.Add(this.barNavigatorBar);
            this.barRightDockSite.Dock = System.Windows.Forms.DockStyle.Right;
            this.barRightDockSite.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barNavigatorBar, 206, 476)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
            this.barRightDockSite.Location = new System.Drawing.Point(880, 86);
            this.barRightDockSite.Name = "barRightDockSite";
            this.barRightDockSite.Size = new System.Drawing.Size(209, 476);
            this.barRightDockSite.TabIndex = 8;
            this.barRightDockSite.TabStop = false;
            // 
            // barNavigatorBar
            // 
            this.barNavigatorBar.AccessibleDescription = "DotNetBar Bar (barNavigatorBar)";
            this.barNavigatorBar.AccessibleName = "DotNetBar Bar";
            this.barNavigatorBar.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.barNavigatorBar.BarType = DevComponents.DotNetBar.eBarType.DockWindow;
            this.barNavigatorBar.CanHide = true;
            this.barNavigatorBar.Controls.Add(this.panelDockContainer3);
            this.barNavigatorBar.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.barNavigatorBar.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.barNavigatorBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.NavigatorItem});
            this.barNavigatorBar.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.barNavigatorBar.Location = new System.Drawing.Point(3, 0);
            this.barNavigatorBar.Name = "barNavigatorBar";
            this.barNavigatorBar.Size = new System.Drawing.Size(206, 476);
            this.barNavigatorBar.Stretch = true;
            this.barNavigatorBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.barNavigatorBar.TabIndex = 0;
            this.barNavigatorBar.TabStop = false;
            this.barNavigatorBar.Text = "NavigatorBar";
            this.barNavigatorBar.Visible = false;
            this.barNavigatorBar.WrapItemsFloat = false;
            // 
            // panelDockContainer3
            // 
            this.panelDockContainer3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.panelDockContainer3.Location = new System.Drawing.Point(3, 23);
            this.panelDockContainer3.Name = "panelDockContainer3";
            this.panelDockContainer3.Size = new System.Drawing.Size(200, 450);
            this.panelDockContainer3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer3.Style.GradientAngle = 90;
            this.panelDockContainer3.TabIndex = 0;
            // 
            // NavigatorItem
            // 
            this.NavigatorItem.Control = this.panelDockContainer3;
            this.NavigatorItem.GlobalItem = true;
            this.NavigatorItem.GlobalName = "NavigatorItem";
            this.NavigatorItem.MinimumSize = new System.Drawing.Size(100, 32);
            this.NavigatorItem.Name = "NavigatorItem";
            this.NavigatorItem.Text = "Navigator";
            // 
            // dockSite4
            // 
            this.dockSite4.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite4.Location = new System.Drawing.Point(0, 562);
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.Size = new System.Drawing.Size(1089, 0);
            this.dockSite4.TabIndex = 17;
            this.dockSite4.TabStop = false;
            // 
            // dockSite1
            // 
            this.dockSite1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite1.Location = new System.Drawing.Point(0, 86);
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.Size = new System.Drawing.Size(0, 476);
            this.dockSite1.TabIndex = 14;
            this.dockSite1.TabStop = false;
            // 
            // dockSite2
            // 
            this.dockSite2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite2.Location = new System.Drawing.Point(1089, 86);
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.Size = new System.Drawing.Size(0, 476);
            this.dockSite2.TabIndex = 15;
            this.dockSite2.TabStop = false;
            // 
            // dockSite3
            // 
            this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite3.Controls.Add(this.barMainMenu);
            this.dockSite3.Controls.Add(this.barToolBar);
            this.dockSite3.Controls.Add(this.barCloseBar);
            this.dockSite3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite3.Location = new System.Drawing.Point(0, 0);
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.Size = new System.Drawing.Size(1089, 86);
            this.dockSite3.TabIndex = 16;
            this.dockSite3.TabStop = false;
            // 
            // barMainMenu
            // 
            this.barMainMenu.AccessibleDescription = "DotNetBar Bar (barMainMenu)";
            this.barMainMenu.AccessibleName = "DotNetBar Bar";
            this.barMainMenu.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.barMainMenu.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barMainMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.barMainMenu.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bSystem,
            this.bUpdates,
            this.cpUpdates});
            this.barMainMenu.Location = new System.Drawing.Point(0, 0);
            this.barMainMenu.MenuBar = true;
            this.barMainMenu.Name = "barMainMenu";
            this.barMainMenu.Size = new System.Drawing.Size(1089, 26);
            this.barMainMenu.Stretch = true;
            this.barMainMenu.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.barMainMenu.TabIndex = 0;
            this.barMainMenu.TabStop = false;
            this.barMainMenu.Text = "Main Menu";
            // 
            // bSystem
            // 
            this.bSystem.AccessibleName = "bSystem";
            this.bSystem.GlobalName = "bSystem";
            this.bSystem.Name = "bSystem";
            this.bSystem.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bRegister,
            this.bRegisterAs,
            this.bAbout,
            this.bHelp,
            this.bExit});
            this.bSystem.Text = "&System";
            // 
            // bRegister
            // 
            this.bRegister.GlobalName = "bRegister";
            this.bRegister.ImageIndex = 0;
            this.bRegister.Name = "bRegister";
            this.bRegister.Text = "&Anmelden";
            // 
            // bRegisterAs
            // 
            this.bRegisterAs.GlobalName = "bRegisterAs";
            this.bRegisterAs.ImageIndex = 0;
            this.bRegisterAs.Name = "bRegisterAs";
            this.bRegisterAs.Text = "Anmelden a&ls";
            // 
            // bAbout
            // 
            this.bAbout.GlobalName = "bAbout";
            this.bAbout.ImageIndex = 13;
            this.bAbout.Name = "bAbout";
            this.bAbout.Text = "&Über";
            // 
            // bHelp
            // 
            this.bHelp.GlobalName = "bHelp";
            this.bHelp.ImageIndex = 9;
            this.bHelp.Name = "bHelp";
            this.bHelp.Text = "&Hilfe";
            // 
            // bExit
            // 
            this.bExit.GlobalName = "bExit";
            this.bExit.ImageIndex = 1;
            this.bExit.Name = "bExit";
            this.bExit.Text = "&Beenden";
            // 
            // bUpdates
            // 
            this.bUpdates.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.bUpdates.FontBold = true;
            this.bUpdates.FontUnderline = true;
            this.bUpdates.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.bUpdates.Name = "bUpdates";
            this.bUpdates.ShowSubItems = false;
            this.bUpdates.Text = "&Updates";
            this.bUpdates.Click += new System.EventHandler(this.IdmUpdates_Click);
            // 
            // cpUpdates
            // 
            this.cpUpdates.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.cpUpdates.Name = "cpUpdates";
            // 
            // barCloseBar
            // 
            this.barCloseBar.AccessibleDescription = "DotNetBar Bar (barCloseBar)";
            this.barCloseBar.AccessibleName = "DotNetBar Bar";
            this.barCloseBar.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barCloseBar.CanDockBottom = false;
            this.barCloseBar.CanDockLeft = false;
            this.barCloseBar.CanDockRight = false;
            this.barCloseBar.CanDockTab = false;
            this.barCloseBar.CanDockTop = false;
            this.barCloseBar.CanMove = false;
            this.barCloseBar.CanUndock = false;
            this.barCloseBar.DockLine = 1;
            this.barCloseBar.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barCloseBar.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
            this.barCloseBar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.barCloseBar.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barCloseBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bClose});
            this.barCloseBar.Location = new System.Drawing.Point(691, 27);
            this.barCloseBar.Name = "barCloseBar";
            this.barCloseBar.PaddingBottom = 10;
            this.barCloseBar.PaddingTop = 10;
            this.barCloseBar.Size = new System.Drawing.Size(145, 59);
            this.barCloseBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.barCloseBar.TabIndex = 1;
            this.barCloseBar.TabStop = false;
            this.barCloseBar.Text = "CloseBar";
            this.barCloseBar.Visible = false;
            // 
            // bClose
            // 
            this.bClose.BeginGroup = true;
            this.bClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.bClose.GlobalName = "bClose";
            this.bClose.ImageIndex = 11;
            this.bClose.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.bClose.Name = "bClose";
            this.bClose.PopupType = DevComponents.DotNetBar.ePopupType.ToolBar;
            this.bClose.Text = "&Fenster schliessen";
            // 
            // barToolBar
            // 
            this.barToolBar.AccessibleDescription = "DotNetBar Bar (barToolBar)";
            this.barToolBar.AccessibleName = "DotNetBar Bar";
            this.barToolBar.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.barToolBar.DockLine = 1;
            this.barToolBar.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barToolBar.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
            this.barToolBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barToolBar.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Office2003;
            this.barToolBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.bRegister2,
            this.bPrint,
            this.bNoticeCenter,
            this.bMessages,
            this.bLibOverview,
            this.bGlossary,
            this.bStartSim});
            this.barToolBar.Location = new System.Drawing.Point(0, 27);
            this.barToolBar.Name = "barToolBar";
            this.barToolBar.PaddingBottom = 10;
            this.barToolBar.PaddingTop = 10;
            this.barToolBar.Size = new System.Drawing.Size(689, 59);
            this.barToolBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
            this.barToolBar.TabIndex = 2;
            this.barToolBar.TabStop = false;
            this.barToolBar.Text = "ToolBar";
            this.barToolBar.Visible = false;
            // 
            // bRegister2
            // 
            this.bRegister2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.bRegister2.GlobalName = "bRegister2";
            this.bRegister2.ImageIndex = 0;
            this.bRegister2.Name = "bRegister2";
            this.bRegister2.Text = "&Anmelden";
            // 
            // bPrint
            // 
            this.bPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.bPrint.DisabledImageIndex = 1;
            this.bPrint.GlobalName = "bPrint";
            this.bPrint.ImageIndex = 2;
            this.bPrint.Name = "bPrint";
            this.bPrint.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
            this.bPrint.Text = "&Drucken";
            // 
            // bNoticeCenter
            // 
            this.bNoticeCenter.AutoCollapseOnClick = false;
            this.bNoticeCenter.BeginGroup = true;
            this.bNoticeCenter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.bNoticeCenter.GlobalName = "bNoticeCenter";
            this.bNoticeCenter.ImageIndex = 4;
            this.bNoticeCenter.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.bNoticeCenter.Name = "bNoticeCenter";
            this.bNoticeCenter.PopupType = DevComponents.DotNetBar.ePopupType.Container;
            this.bNoticeCenter.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlN);
            this.bNoticeCenter.Text = "&Notizen";
            // 
            // bMessages
            // 
            this.bMessages.AutoCollapseOnClick = false;
            this.bMessages.BeginGroup = true;
            this.bMessages.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.bMessages.GlobalName = "bMessages";
            this.bMessages.ImageIndex = 5;
            this.bMessages.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.bMessages.Name = "bMessages";
            this.bMessages.PopupType = DevComponents.DotNetBar.ePopupType.Container;
            this.bMessages.Text = "&Nachrichten";
            // 
            // bLibOverview
            // 
            this.bLibOverview.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.bLibOverview.GlobalName = "bLibOverview";
            this.bLibOverview.ImageIndex = 7;
            this.bLibOverview.Name = "bLibOverview";
            this.bLibOverview.Text = "&Navigator";
            // 
            // bGlossary
            // 
            this.bGlossary.AutoCollapseOnClick = false;
            this.bGlossary.BeginGroup = true;
            this.bGlossary.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.bGlossary.GlobalName = "bGlossary";
            this.bGlossary.ImageIndex = 8;
            this.bGlossary.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.bGlossary.Name = "bGlossary";
            this.bGlossary.PopupType = DevComponents.DotNetBar.ePopupType.Container;
            this.bGlossary.Text = "&Indexsuche";
            // 
            // bStartSim
            // 
            this.bStartSim.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.bStartSim.GlobalName = "bStartSim";
            this.bStartSim.ImageIndex = 10;
            this.bStartSim.Name = "bStartSim";
            this.bStartSim.Text = "&Simulation";
            // 
            // barTopDockSite
            // 
            this.barTopDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barTopDockSite.Dock = System.Windows.Forms.DockStyle.Top;
            this.barTopDockSite.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.barTopDockSite.Location = new System.Drawing.Point(0, 86);
            this.barTopDockSite.Name = "barTopDockSite";
            this.barTopDockSite.Size = new System.Drawing.Size(1089, 0);
            this.barTopDockSite.TabIndex = 9;
            this.barTopDockSite.TabStop = false;
            // 
            // messageTimer
            // 
            this.messageTimer.Interval = 500;
            this.messageTimer.Tick += new System.EventHandler(this.messageTimer_Tick);
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = false;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            // 
            // bkgworkerClientLogin
            // 
            this.bkgworkerClientLogin.WorkerSupportsCancellation = true;
            this.bkgworkerClientLogin.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgworkerClientLogin_DoWork);
            this.bkgworkerClientLogin.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkgworkerClientLogin_RunWorkerCompleted);
            // 
            // bkgworkerServerLogin
            // 
            this.bkgworkerServerLogin.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgworkerServerLogin_DoWork);
            this.bkgworkerServerLogin.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkgworkerServerLogin_RunWorkerCompleted);
            // 
            // connectionTimer
            // 
            this.connectionTimer.Enabled = true;
            this.connectionTimer.Interval = 10000;
            this.connectionTimer.Tick += new System.EventHandler(this.connectionTimer_Tick);
            // 
            // transferTimer
            // 
            this.transferTimer.Interval = 60000;
            this.transferTimer.Tick += new System.EventHandler(this.transferTimer_Tick);
            // 
            // bkgWorkerStandAloneLogin
            // 
            this.bkgWorkerStandAloneLogin.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgworkerStandAloneLogin_DoWork);
            this.bkgWorkerStandAloneLogin.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkgworkerStandAloneLogin_RunWorkerCompleted);
            // 
            // FrmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(1089, 562);
            this.Controls.Add(this.panStatus);
            this.Controls.Add(this.barLeftDockSite);
            this.Controls.Add(this.barRightDockSite);
            this.Controls.Add(this.barTopDockSite);
            this.Controls.Add(this.barBottomDockSite);
            this.Controls.Add(this.dockSite1);
            this.Controls.Add(this.dockSite2);
            this.Controls.Add(this.dockSite3);
            this.Controls.Add(this.dockSite4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(500, 557);
            this.Name = "FrmMain";
            this.Text = "WebTrain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.MdiChildActivate += new System.EventHandler(this.FrmMain_MdiChildActivate);
            this.panStatus.ResumeLayout(false);
            this.barLeftDockSite.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barNoticeBar)).EndInit();
            this.barNoticeBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barControlCenter)).EndInit();
            this.barControlCenter.ResumeLayout(false);
            this.barRightDockSite.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barNavigatorBar)).EndInit();
            this.barNavigatorBar.ResumeLayout(false);
            this.dockSite3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barMainMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barCloseBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barToolBar)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ImageList m_imageList;
        private System.Windows.Forms.Panel panStatus;
        private System.Windows.Forms.Label lblUserName;
        private DevComponents.DotNetBar.DotNetBarManager dotNetBarManager1;
        private DockSite barBottomDockSite;
        private DockSite barLeftDockSite;
        private DockSite barRightDockSite;
        private DockSite barTopDockSite;
        private System.Windows.Forms.Timer messageTimer;
        private Bar barNoticeBar;
        private PanelDockContainer panelDockContainer1;
        private DockContainerItem NoticeItem;
        private Bar barControlCenter;
        private PanelDockContainer panelDockContainer2;
        private Bar barNavigatorBar;
        private PanelDockContainer panelDockContainer3;
        private DockSite dockSite1;
        private DockSite dockSite2;
        private DockSite dockSite3;
        private Bar barMainMenu;
        private ButtonItem bSystem;
        private ButtonItem bRegister;
        private ButtonItem bRegisterAs;
        private ButtonItem bExit;
        private ButtonItem bHelp;
        private ButtonItem bAbout;
        private Bar barCloseBar;
        private ButtonItem bClose;
        private Bar barToolBar;
        private ButtonItem bRegister2;
        private ButtonItem bPrint;
        private ButtonItem bNoticeCenter;
        private ButtonItem bMessages;
        private ButtonItem bLibOverview;
        private ButtonItem bGlossary;
        private ButtonItem bStartSim;
        private DockSite dockSite4;
        private DockContainerItem ControlCenterItem;
        private DockContainerItem NavigatorItem;
        private ControlContainerItem controlContainerItem1;
        private System.ComponentModel.BackgroundWorker bkgworkerClientLogin;
        private System.ComponentModel.BackgroundWorker bkgworkerServerLogin;
        private ButtonItem bUpdates;
        private System.Windows.Forms.Timer connectionTimer;
        private CircularProgressItem cpUpdates;
        private System.Windows.Forms.Timer transferTimer;
        private System.ComponentModel.BackgroundWorker bkgWorkerStandAloneLogin;
    }
}
