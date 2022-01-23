
namespace WebtrainWebPortal.Views
{
    partial class MainPage
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

        #region Wisej Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.navBarMain = new Wisej.Web.Ext.NavigationBar.NavigationBar();
            this.navBarItemTimeRecording = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.navBarItemUserAdmin = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.navBarItemUserAdminStudents = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.navBarItemUserAdminTrainer = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.navBarItemReports = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.navBarItemsReportAbsenceStudents = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.navBarItemsReportAbsenceTrainer = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.navBarItemsReportSickness = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.navBarItemDownloads = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.navBarItemLearnContent = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.navBarItemLogout = new Wisej.Web.Ext.NavigationBar.NavigationBarItem();
            this.dgvUsers = new Wisej.Web.DataGridView();
            this.panUserlist = new Wisej.Web.Panel();
            this.upload1 = new Wisej.Web.Upload();
            this.panTimeRecording = new Wisej.Web.Panel();
            this.panel1 = new Wisej.Web.Panel();
            this.panel3 = new Wisej.Web.Panel();
            this.dgvTimeRecords = new Wisej.Web.DataGridView();
            this.panel2 = new Wisej.Web.Panel();
            this.btnEnd = new Wisej.Web.Button();
            this.btnStart = new Wisej.Web.Button();
            this.btnSickness = new Wisej.Web.Button();
            this.coolClock1 = new Wisej.Web.Ext.CoolClock.CoolClock();
            this.panReportAbsenceStudents = new Wisej.Web.Panel();
            this.dgvAbsentStudents = new Wisej.Web.DataGridView();
            this.panel4 = new Wisej.Web.Panel();
            this.dtAbsStudentsActualDate = new Wisej.Web.DateTimePicker();
            this.btnAbsStudentsToday = new Wisej.Web.Button();
            this.panTrainerlist = new Wisej.Web.Panel();
            this.dgvTrainer = new Wisej.Web.DataGridView();
            this.panReportAbsenceTrainer = new Wisej.Web.Panel();
            this.dgvAbsentTrainer = new Wisej.Web.DataGridView();
            this.panel6 = new Wisej.Web.Panel();
            this.dtAbsTrainerActualDate = new Wisej.Web.DateTimePicker();
            this.btnAbsTrainerToday = new Wisej.Web.Button();
            this.panReportSickness = new Wisej.Web.Panel();
            this.dgvSickness = new Wisej.Web.DataGridView();
            this.panLearnContent = new Wisej.Web.Panel();
            this.label2 = new Wisej.Web.Label();
            this.button2 = new Wisej.Web.Button();
            this.label3 = new Wisej.Web.Label();
            this.button1 = new Wisej.Web.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panUserlist.SuspendLayout();
            this.panTimeRecording.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeRecords)).BeginInit();
            this.panel2.SuspendLayout();
            this.panReportAbsenceStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsentStudents)).BeginInit();
            this.panel4.SuspendLayout();
            this.panTrainerlist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrainer)).BeginInit();
            this.panReportAbsenceTrainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsentTrainer)).BeginInit();
            this.panel6.SuspendLayout();
            this.panReportSickness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSickness)).BeginInit();
            this.panLearnContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarMain
            // 
            this.navBarMain.Dock = Wisej.Web.DockStyle.Left;
            this.navBarMain.ItemHeight = 35;
            this.navBarMain.Items.AddRange(new Wisej.Web.Ext.NavigationBar.NavigationBarItem[] {
            this.navBarItemTimeRecording,
            this.navBarItemUserAdmin,
            this.navBarItemReports,
            this.navBarItemDownloads,
            this.navBarItemLearnContent,
            this.navBarItemLogout});
            this.navBarMain.Logo = "Images\\BFI_Logo5.jpg";
            this.navBarMain.Name = "navBarMain";
            this.navBarMain.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("navBarMain.ResponsiveProfiles"))));
            this.navBarMain.Size = new System.Drawing.Size(201, 851);
            this.navBarMain.TabIndex = 0;
            this.navBarMain.Text = "Webtrain@Metzentrum";
            this.navBarMain.UserName = "office@softobject.at";
            this.navBarMain.TitleClick += new System.EventHandler(this.navBarMain_TitleClick);
            this.navBarMain.UserClick += new System.EventHandler(this.navBarMain_UserClick);
            this.navBarMain.Tap += new System.EventHandler(this.navBarMain_Tap);
            // 
            // navBarItemTimeRecording
            // 
            this.navBarItemTimeRecording.BackColor = System.Drawing.Color.Transparent;
            this.navBarItemTimeRecording.Icon = "resource.wx/Wisej.Ext.ElegantIcons/time-watch.svg";
            this.navBarItemTimeRecording.Name = "navBarItemTimeRecording";
            this.navBarItemTimeRecording.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("navBarItemTimeRecording.ResponsiveProfiles"))));
            this.navBarItemTimeRecording.ShortcutIcon = "icon-right";
            this.navBarItemTimeRecording.ShowShortcut = true;
            this.navBarItemTimeRecording.Text = "Zeiterfassung";
            this.navBarItemTimeRecording.Click += new System.EventHandler(this.navBarItemTimeRecording_Click);
            // 
            // navBarItemUserAdmin
            // 
            this.navBarItemUserAdmin.BackColor = System.Drawing.Color.Transparent;
            this.navBarItemUserAdmin.Icon = "resource.wx/Wisej.Ext.MaterialDesign/user-outline.svg";
            this.navBarItemUserAdmin.Items.AddRange(new Wisej.Web.Ext.NavigationBar.NavigationBarItem[] {
            this.navBarItemUserAdminStudents,
            this.navBarItemUserAdminTrainer});
            this.navBarItemUserAdmin.Name = "navBarItemUserAdmin";
            this.navBarItemUserAdmin.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("navBarItemUserAdmin.ResponsiveProfiles"))));
            this.navBarItemUserAdmin.ShowShortcut = true;
            this.navBarItemUserAdmin.Text = "Benutzerverwaltung";
            this.navBarItemUserAdmin.PanelCollapsed += new System.EventHandler(this.navBarItemUserAdmin_PanelCollapsed);
            // 
            // navBarItemUserAdminStudents
            // 
            this.navBarItemUserAdminStudents.BackColor = System.Drawing.Color.FromName("@navbar-background");
            this.navBarItemUserAdminStudents.Icon = "resource.wx/Wisej.Ext.MaterialDesign/user-inside-bubble-speech.svg";
            this.navBarItemUserAdminStudents.IconVisible = false;
            this.navBarItemUserAdminStudents.Name = "navBarItemUserAdminStudents";
            this.navBarItemUserAdminStudents.ShortcutIcon = "icon-right";
            this.navBarItemUserAdminStudents.ShowShortcut = true;
            this.navBarItemUserAdminStudents.Text = "Teilnehmer";
            this.navBarItemUserAdminStudents.Click += new System.EventHandler(this.navBarItemUserAdminStudents_Click);
            // 
            // navBarItemUserAdminTrainer
            // 
            this.navBarItemUserAdminTrainer.BackColor = System.Drawing.Color.FromName("@navbar-background");
            this.navBarItemUserAdminTrainer.ExpandOnClick = false;
            this.navBarItemUserAdminTrainer.Icon = "resource.wx/Wisej.Ext.MaterialDesign/man-walking-directions-button.svg";
            this.navBarItemUserAdminTrainer.IconVisible = false;
            this.navBarItemUserAdminTrainer.Name = "navBarItemUserAdminTrainer";
            this.navBarItemUserAdminTrainer.ShortcutIcon = "scrollbar-arrow-right";
            this.navBarItemUserAdminTrainer.ShowShortcut = true;
            this.navBarItemUserAdminTrainer.Text = "Trainer";
            this.navBarItemUserAdminTrainer.Click += new System.EventHandler(this.navBarItemUserAdminTrainer_Click);
            // 
            // navBarItemReports
            // 
            this.navBarItemReports.BackColor = System.Drawing.Color.Transparent;
            this.navBarItemReports.Icon = "resource.wx/Wisej.Ext.MaterialDesign/receipt.svg";
            this.navBarItemReports.Items.AddRange(new Wisej.Web.Ext.NavigationBar.NavigationBarItem[] {
            this.navBarItemsReportAbsenceStudents,
            this.navBarItemsReportAbsenceTrainer,
            this.navBarItemsReportSickness});
            this.navBarItemReports.Name = "navBarItemReports";
            this.navBarItemReports.ShowShortcut = true;
            this.navBarItemReports.Text = "Berichte";
            this.navBarItemReports.PanelCollapsed += new System.EventHandler(this.navBarItemReports_PanelCollapsed);
            // 
            // navBarItemsReportAbsenceStudents
            // 
            this.navBarItemsReportAbsenceStudents.BackColor = System.Drawing.Color.FromName("@navbar-background");
            this.navBarItemsReportAbsenceStudents.Icon = "resource.wx/Wisej.Ext.MaterialDesign/users-social-symbol.svg";
            this.navBarItemsReportAbsenceStudents.IconVisible = false;
            this.navBarItemsReportAbsenceStudents.Name = "navBarItemsReportAbsenceStudents";
            this.navBarItemsReportAbsenceStudents.ShortcutIcon = "icon-right";
            this.navBarItemsReportAbsenceStudents.ShowShortcut = true;
            this.navBarItemsReportAbsenceStudents.Text = "An-/Abw. Teiln.";
            this.navBarItemsReportAbsenceStudents.Click += new System.EventHandler(this.navBarItemsReportAbsenceStudents_Click);
            // 
            // navBarItemsReportAbsenceTrainer
            // 
            this.navBarItemsReportAbsenceTrainer.BackColor = System.Drawing.Color.FromName("@navbar-background");
            this.navBarItemsReportAbsenceTrainer.Icon = "resource.wx/Wisej.Ext.MaterialDesign/apple-accessibility.svg";
            this.navBarItemsReportAbsenceTrainer.IconVisible = false;
            this.navBarItemsReportAbsenceTrainer.Name = "navBarItemsReportAbsenceTrainer";
            this.navBarItemsReportAbsenceTrainer.ShortcutIcon = "icon-right";
            this.navBarItemsReportAbsenceTrainer.ShowShortcut = true;
            this.navBarItemsReportAbsenceTrainer.Text = "An-/Abw. Trainer";
            this.navBarItemsReportAbsenceTrainer.ToolTipText = "An-/Abwesenheit Trainer";
            this.navBarItemsReportAbsenceTrainer.Click += new System.EventHandler(this.navBarItemsReportAbsenceTrainer_Click);
            // 
            // navBarItemsReportSickness
            // 
            this.navBarItemsReportSickness.BackColor = System.Drawing.Color.FromName("@navbar-background");
            this.navBarItemsReportSickness.Icon = "resource.wx/Wisej.Ext.MaterialDesign/copy-content.svg";
            this.navBarItemsReportSickness.IconVisible = false;
            this.navBarItemsReportSickness.Name = "navBarItemsReportSickness";
            this.navBarItemsReportSickness.ShortcutIcon = "icon-right";
            this.navBarItemsReportSickness.ShowShortcut = true;
            this.navBarItemsReportSickness.Text = "Krankmeldungen";
            this.navBarItemsReportSickness.Click += new System.EventHandler(this.navBarItemsReportSickness_Click);
            // 
            // navBarItemDownloads
            // 
            this.navBarItemDownloads.BackColor = System.Drawing.Color.Transparent;
            this.navBarItemDownloads.Icon = "resource.wx/Wisej.Ext.MaterialDesign/cloud-download.svg";
            this.navBarItemDownloads.Name = "navBarItemDownloads";
            this.navBarItemDownloads.Text = "Webtrain-Client";
            this.navBarItemDownloads.Click += new System.EventHandler(this.navBarItemDownloads_Click);
            // 
            // navBarItemLearnContent
            // 
            this.navBarItemLearnContent.BackColor = System.Drawing.Color.Transparent;
            this.navBarItemLearnContent.Icon = "resource.wx/Wisej.Ext.MaterialDesign/list-of-three-elements-on-black-background.s" +
    "vg";
            this.navBarItemLearnContent.Name = "NavigationBarItem";
            this.navBarItemLearnContent.Text = "Lerninhalte";
            this.navBarItemLearnContent.Click += new System.EventHandler(this.navBarItemLearnContent_Click);
            // 
            // navBarItemLogout
            // 
            this.navBarItemLogout.BackColor = System.Drawing.Color.Transparent;
            this.navBarItemLogout.ForeColor = System.Drawing.Color.FromName("@switchOn");
            this.navBarItemLogout.Icon = "resource.wx/Wisej.Ext.ElegantIcons/access-denied.svg";
            this.navBarItemLogout.Name = "navBarItemLogout";
            this.navBarItemLogout.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("navBarItemLogout.ResponsiveProfiles"))));
            this.navBarItemLogout.Text = "Abmelden";
            this.navBarItemLogout.Click += new System.EventHandler(this.navBarItemLogout_Click);
            // 
            // dgvUsers
            // 
            this.dgvUsers.Dock = Wisej.Web.DockStyle.Fill;
            this.dgvUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("dgvUsers.ResponsiveProfiles"))));
            this.dgvUsers.ShowColumnVisibilityMenu = false;
            this.dgvUsers.Size = new System.Drawing.Size(415, 282);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.CellFormatting += new Wisej.Web.DataGridViewCellFormattingEventHandler(this.dgvUsers_CellFormatting);
            this.dgvUsers.CellClick += new Wisej.Web.DataGridViewCellEventHandler(this.dgvUsers_CellClick);
            // 
            // panUserlist
            // 
            this.panUserlist.Controls.Add(this.upload1);
            this.panUserlist.Controls.Add(this.dgvUsers);
            this.panUserlist.Location = new System.Drawing.Point(236, 3);
            this.panUserlist.Name = "panUserlist";
            this.panUserlist.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("panUserlist.ResponsiveProfiles"))));
            this.panUserlist.ShowHeader = true;
            this.panUserlist.Size = new System.Drawing.Size(415, 310);
            this.panUserlist.TabIndex = 1;
            this.panUserlist.TabStop = true;
            this.panUserlist.Text = "Teilnehmerliste";
            this.panUserlist.Visible = false;
            // 
            // upload1
            // 
            this.upload1.AllowMultipleFiles = true;
            this.upload1.Dock = Wisej.Web.DockStyle.Bottom;
            this.upload1.Location = new System.Drawing.Point(0, 233);
            this.upload1.Name = "upload1";
            this.upload1.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("upload1.ResponsiveProfiles"))));
            this.upload1.Size = new System.Drawing.Size(415, 49);
            this.upload1.TabIndex = 1;
            this.upload1.Text = "Userdaten";
            this.upload1.Uploaded += new Wisej.Web.UploadedEventHandler(this.upload1_Uploaded);
            // 
            // panTimeRecording
            // 
            this.panTimeRecording.Controls.Add(this.panel1);
            this.panTimeRecording.Location = new System.Drawing.Point(236, 348);
            this.panTimeRecording.Name = "panTimeRecording";
            this.panTimeRecording.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("panTimeRecording.ResponsiveProfiles"))));
            this.panTimeRecording.ShowHeader = true;
            this.panTimeRecording.Size = new System.Drawing.Size(367, 374);
            this.panTimeRecording.TabIndex = 3;
            this.panTimeRecording.TabStop = true;
            this.panTimeRecording.Text = "Zeiterfassung";
            this.panTimeRecording.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = Wisej.Web.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(367, 346);
            this.panel1.TabIndex = 0;
            this.panel1.TabStop = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvTimeRecords);
            this.panel3.Dock = Wisej.Web.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(267, 346);
            this.panel3.TabIndex = 0;
            this.panel3.TabStop = true;
            // 
            // dgvTimeRecords
            // 
            this.dgvTimeRecords.AllowUserToResizeColumns = false;
            this.dgvTimeRecords.AllowUserToResizeRows = false;
            this.dgvTimeRecords.ColumnHeadersHeight = 50;
            this.dgvTimeRecords.Dock = Wisej.Web.DockStyle.Fill;
            this.dgvTimeRecords.EditMode = Wisej.Web.DataGridViewEditMode.EditOnEnter;
            this.dgvTimeRecords.Location = new System.Drawing.Point(0, 0);
            this.dgvTimeRecords.MultiSelect = false;
            this.dgvTimeRecords.Name = "dgvTimeRecords";
            this.dgvTimeRecords.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("dgvTimeRecords.ResponsiveProfiles"))));
            this.dgvTimeRecords.ShowColumnVisibilityMenu = false;
            this.dgvTimeRecords.Size = new System.Drawing.Size(267, 346);
            this.dgvTimeRecords.TabIndex = 0;
            this.dgvTimeRecords.CellValueChanged += new Wisej.Web.DataGridViewCellEventHandler(this.dgvTimeRecords_CellValueChanged);
            this.dgvTimeRecords.CellFormatting += new Wisej.Web.DataGridViewCellFormattingEventHandler(this.dgvTimeRecords_CellFormatting);
            this.dgvTimeRecords.EditingControlShowing += new Wisej.Web.DataGridViewEditingControlShowingEventHandler(this.dgvTimeRecords_EditingControlShowing);
            this.dgvTimeRecords.CurrentCellChanged += new System.EventHandler(this.dgvTimeRecords_CurrentCellChanged);
            this.dgvTimeRecords.CellClick += new Wisej.Web.DataGridViewCellEventHandler(this.dgvTimeRecords_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnEnd);
            this.panel2.Controls.Add(this.btnStart);
            this.panel2.Controls.Add(this.btnSickness);
            this.panel2.Controls.Add(this.coolClock1);
            this.panel2.Dock = Wisej.Web.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(267, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(100, 346);
            this.panel2.TabIndex = 1;
            this.panel2.TabStop = true;
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(3, 143);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(91, 27);
            this.btnEnd.TabIndex = 4;
            this.btnEnd.Text = "Ende";
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(3, 110);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(91, 27);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSickness
            // 
            this.btnSickness.AllowHtml = true;
            this.btnSickness.Location = new System.Drawing.Point(3, 177);
            this.btnSickness.Name = "btnSickness";
            this.btnSickness.Size = new System.Drawing.Size(91, 27);
            this.btnSickness.TabIndex = 2;
            this.btnSickness.Text = "Krankmeldung";
            this.btnSickness.Click += new System.EventHandler(this.btnSickness_Click);
            // 
            // coolClock1
            // 
            this.coolClock1.Dock = Wisej.Web.DockStyle.Top;
            this.coolClock1.Name = "coolClock1";
            this.coolClock1.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("coolClock1.ResponsiveProfiles"))));
            this.coolClock1.Size = new System.Drawing.Size(100, 346);
            this.coolClock1.Skin = Wisej.Web.Ext.CoolClock.CoolClockSkin.SwissRail;
            this.coolClock1.Text = "coolClock1";
            // 
            // panReportAbsenceStudents
            // 
            this.panReportAbsenceStudents.Controls.Add(this.dgvAbsentStudents);
            this.panReportAbsenceStudents.Controls.Add(this.panel4);
            this.panReportAbsenceStudents.Location = new System.Drawing.Point(657, 348);
            this.panReportAbsenceStudents.Name = "panReportAbsenceStudents";
            this.panReportAbsenceStudents.ShowHeader = true;
            this.panReportAbsenceStudents.Size = new System.Drawing.Size(372, 293);
            this.panReportAbsenceStudents.TabIndex = 4;
            this.panReportAbsenceStudents.TabStop = true;
            this.panReportAbsenceStudents.Text = "An-/Abwesenheit Teilnehmer";
            this.panReportAbsenceStudents.Visible = false;
            // 
            // dgvAbsentStudents
            // 
            this.dgvAbsentStudents.Dock = Wisej.Web.DockStyle.Fill;
            this.dgvAbsentStudents.Location = new System.Drawing.Point(0, 0);
            this.dgvAbsentStudents.Name = "dgvAbsentStudents";
            this.dgvAbsentStudents.ReadOnly = true;
            this.dgvAbsentStudents.ShowColumnVisibilityMenu = false;
            this.dgvAbsentStudents.Size = new System.Drawing.Size(372, 209);
            this.dgvAbsentStudents.TabIndex = 0;
            this.dgvAbsentStudents.CellFormatting += new Wisej.Web.DataGridViewCellFormattingEventHandler(this.dgvAbsentUsers_CellFormatting);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromName("@toolbar");
            this.panel4.Controls.Add(this.dtAbsStudentsActualDate);
            this.panel4.Controls.Add(this.btnAbsStudentsToday);
            this.panel4.Dock = Wisej.Web.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 209);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(372, 56);
            this.panel4.TabIndex = 1;
            this.panel4.TabStop = true;
            // 
            // dtAbsStudentsActualDate
            // 
            this.dtAbsStudentsActualDate.Checked = false;
            this.dtAbsStudentsActualDate.Location = new System.Drawing.Point(142, 15);
            this.dtAbsStudentsActualDate.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtAbsStudentsActualDate.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtAbsStudentsActualDate.Name = "dtAbsStudentsActualDate";
            this.dtAbsStudentsActualDate.Size = new System.Drawing.Size(200, 22);
            this.dtAbsStudentsActualDate.TabIndex = 1;
            this.dtAbsStudentsActualDate.Value = new System.DateTime(2021, 1, 11, 0, 0, 0, 0);
            this.dtAbsStudentsActualDate.ValueChanged += new System.EventHandler(this.dtAbsStudentsActualDate_ValueChanged);
            // 
            // btnAbsStudentsToday
            // 
            this.btnAbsStudentsToday.Location = new System.Drawing.Point(16, 13);
            this.btnAbsStudentsToday.Name = "btnAbsStudentsToday";
            this.btnAbsStudentsToday.Size = new System.Drawing.Size(100, 27);
            this.btnAbsStudentsToday.TabIndex = 0;
            this.btnAbsStudentsToday.Text = "Heute";
            this.btnAbsStudentsToday.Click += new System.EventHandler(this.btnAbsStudentsToday_Click);
            // 
            // panTrainerlist
            // 
            this.panTrainerlist.Controls.Add(this.dgvTrainer);
            this.panTrainerlist.Location = new System.Drawing.Point(673, 3);
            this.panTrainerlist.Name = "panTrainerlist";
            this.panTrainerlist.ShowHeader = true;
            this.panTrainerlist.Size = new System.Drawing.Size(541, 339);
            this.panTrainerlist.TabIndex = 6;
            this.panTrainerlist.TabStop = true;
            this.panTrainerlist.Text = "Trainerliste";
            // 
            // dgvTrainer
            // 
            this.dgvTrainer.Dock = Wisej.Web.DockStyle.Fill;
            this.dgvTrainer.Location = new System.Drawing.Point(0, 0);
            this.dgvTrainer.Name = "dgvTrainer";
            this.dgvTrainer.ShowColumnVisibilityMenu = false;
            this.dgvTrainer.Size = new System.Drawing.Size(541, 311);
            this.dgvTrainer.TabIndex = 0;
            // 
            // panReportAbsenceTrainer
            // 
            this.panReportAbsenceTrainer.Controls.Add(this.dgvAbsentTrainer);
            this.panReportAbsenceTrainer.Controls.Add(this.panel6);
            this.panReportAbsenceTrainer.Location = new System.Drawing.Point(1055, 348);
            this.panReportAbsenceTrainer.Name = "panReportAbsenceTrainer";
            this.panReportAbsenceTrainer.ShowHeader = true;
            this.panReportAbsenceTrainer.Size = new System.Drawing.Size(372, 293);
            this.panReportAbsenceTrainer.TabIndex = 5;
            this.panReportAbsenceTrainer.TabStop = true;
            this.panReportAbsenceTrainer.Text = "An-/Abwesenheit Trainer";
            this.panReportAbsenceTrainer.Visible = false;
            // 
            // dgvAbsentTrainer
            // 
            this.dgvAbsentTrainer.Dock = Wisej.Web.DockStyle.Fill;
            this.dgvAbsentTrainer.Location = new System.Drawing.Point(0, 0);
            this.dgvAbsentTrainer.Name = "dgvAbsentTrainer";
            this.dgvAbsentTrainer.ReadOnly = true;
            this.dgvAbsentTrainer.ShowColumnVisibilityMenu = false;
            this.dgvAbsentTrainer.Size = new System.Drawing.Size(372, 209);
            this.dgvAbsentTrainer.TabIndex = 0;
            this.dgvAbsentTrainer.CellFormatting += new Wisej.Web.DataGridViewCellFormattingEventHandler(this.dgvAbsentTrainer_CellFormatting);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromName("@toolbar");
            this.panel6.Controls.Add(this.dtAbsTrainerActualDate);
            this.panel6.Controls.Add(this.btnAbsTrainerToday);
            this.panel6.Dock = Wisej.Web.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 209);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(372, 56);
            this.panel6.TabIndex = 1;
            this.panel6.TabStop = true;
            // 
            // dtAbsTrainerActualDate
            // 
            this.dtAbsTrainerActualDate.Checked = false;
            this.dtAbsTrainerActualDate.Location = new System.Drawing.Point(142, 15);
            this.dtAbsTrainerActualDate.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dtAbsTrainerActualDate.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtAbsTrainerActualDate.Name = "dtAbsTrainerActualDate";
            this.dtAbsTrainerActualDate.Size = new System.Drawing.Size(200, 22);
            this.dtAbsTrainerActualDate.TabIndex = 1;
            this.dtAbsTrainerActualDate.Value = new System.DateTime(2021, 1, 11, 0, 0, 0, 0);
            this.dtAbsTrainerActualDate.ValueChanged += new System.EventHandler(this.dtAbsTrainerActualDate_ValueChanged);
            // 
            // btnAbsTrainerToday
            // 
            this.btnAbsTrainerToday.Location = new System.Drawing.Point(16, 13);
            this.btnAbsTrainerToday.Name = "btnAbsTrainerToday";
            this.btnAbsTrainerToday.Size = new System.Drawing.Size(100, 27);
            this.btnAbsTrainerToday.TabIndex = 0;
            this.btnAbsTrainerToday.Text = "Heute";
            this.btnAbsTrainerToday.Click += new System.EventHandler(this.btnAbsTrainerToday_Click);
            // 
            // panReportSickness
            // 
            this.panReportSickness.Controls.Add(this.dgvSickness);
            this.panReportSickness.Location = new System.Drawing.Point(657, 659);
            this.panReportSickness.Name = "panReportSickness";
            this.panReportSickness.ShowHeader = true;
            this.panReportSickness.Size = new System.Drawing.Size(274, 192);
            this.panReportSickness.TabIndex = 7;
            this.panReportSickness.TabStop = true;
            this.panReportSickness.Text = "Krankmeldungen";
            // 
            // dgvSickness
            // 
            this.dgvSickness.Dock = Wisej.Web.DockStyle.Fill;
            this.dgvSickness.Location = new System.Drawing.Point(0, 0);
            this.dgvSickness.Name = "dgvSickness";
            this.dgvSickness.ShowColumnVisibilityMenu = false;
            this.dgvSickness.Size = new System.Drawing.Size(274, 164);
            this.dgvSickness.TabIndex = 0;
            this.dgvSickness.CellFormatting += new Wisej.Web.DataGridViewCellFormattingEventHandler(this.dgvSickness_CellFormatting);
            this.dgvSickness.CellClick += new Wisej.Web.DataGridViewCellEventHandler(this.dgvSickness_CellClick);
            // 
            // panLearnContent
            // 
            this.panLearnContent.Controls.Add(this.label2);
            this.panLearnContent.Controls.Add(this.button2);
            this.panLearnContent.Controls.Add(this.label3);
            this.panLearnContent.Controls.Add(this.button1);
            this.panLearnContent.Location = new System.Drawing.Point(965, 426);
            this.panLearnContent.Name = "panLearnContent";
            this.panLearnContent.ShowHeader = true;
            this.panLearnContent.Size = new System.Drawing.Size(394, 422);
            this.panLearnContent.TabIndex = 8;
            this.panLearnContent.TabStop = true;
            this.panLearnContent.Text = "Lerninhalte";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(259, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Online-Inhalt";
            // 
            // button2
            // 
            this.button2.ImageSource = "Images\\Webtrain_logo2.jpg";
            this.button2.Location = new System.Drawing.Point(228, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 136);
            this.button2.TabIndex = 4;
            this.button2.TextImageRelation = Wisej.Web.TextImageRelation.Overlay;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(80, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Start Webtrain-Client";
            // 
            // button1
            // 
            this.button1.ImageSource = "Images\\Webtrain_logo.jpg";
            this.button1.Location = new System.Drawing.Point(70, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 136);
            this.button1.TabIndex = 2;
            this.button1.TextImageRelation = Wisej.Web.TextImageRelation.Overlay;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.BackgroundImageLayout = Wisej.Web.ImageLayout.Zoom;
            this.BackgroundImageSource = "Images/Background_metal_leer.jpg";
            this.Controls.Add(this.panLearnContent);
            this.Controls.Add(this.panReportSickness);
            this.Controls.Add(this.panReportAbsenceTrainer);
            this.Controls.Add(this.panTrainerlist);
            this.Controls.Add(this.panReportAbsenceStudents);
            this.Controls.Add(this.panTimeRecording);
            this.Controls.Add(this.panUserlist);
            this.Controls.Add(this.navBarMain);
            this.Name = "MainPage";
            this.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("$this.ResponsiveProfiles"))));
            this.Size = new System.Drawing.Size(1123, 494);
            this.ResponsiveProfileChanged += new Wisej.Web.ResponsiveProfileChangedEventHandler(this.MainPage_ResponsiveProfileChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panUserlist.ResumeLayout(false);
            this.panTimeRecording.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeRecords)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panReportAbsenceStudents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsentStudents)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panTrainerlist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrainer)).EndInit();
            this.panReportAbsenceTrainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsentTrainer)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panReportSickness.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSickness)).EndInit();
            this.panLearnContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Wisej.Web.Ext.NavigationBar.NavigationBar navBarMain;
        private Wisej.Web.DataGridView dgvUsers;
        private Wisej.Web.Panel panUserlist;
        private Wisej.Web.Upload upload1;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemUserAdmin;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemLogout;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemTimeRecording;
        private Wisej.Web.Panel panTimeRecording;
        private Wisej.Web.Ext.CoolClock.CoolClock coolClock1;
        private Wisej.Web.Button btnStart;
        private Wisej.Web.Button btnSickness;
        private Wisej.Web.Panel panel1;
        private Wisej.Web.DataGridView dgvTimeRecords;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemReports;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemsReportAbsenceStudents;
        private Wisej.Web.Panel panReportAbsenceStudents;
        private Wisej.Web.DataGridView dgvAbsentStudents;
        private Wisej.Web.Panel panel4;
        private Wisej.Web.DateTimePicker dtAbsStudentsActualDate;
        private Wisej.Web.Button btnAbsStudentsToday;
        private Wisej.Web.Panel panel2;
        private Wisej.Web.Panel panel3;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemDownloads;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemUserAdminStudents;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemUserAdminTrainer;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemsReportSickness;
        private Wisej.Web.Panel panTrainerlist;
        private Wisej.Web.DataGridView dgvTrainer;
        private Wisej.Web.Panel panReportAbsenceTrainer;
        private Wisej.Web.DataGridView dgvAbsentTrainer;
        private Wisej.Web.Panel panel6;
        private Wisej.Web.DateTimePicker dtAbsTrainerActualDate;
        private Wisej.Web.Button btnAbsTrainerToday;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemsReportAbsenceTrainer;
        private Wisej.Web.Panel panReportSickness;
        private Wisej.Web.DataGridView dgvSickness;
        private Wisej.Web.Button btnEnd;
        private Wisej.Web.Ext.NavigationBar.NavigationBarItem navBarItemLearnContent;
        private Wisej.Web.Panel panLearnContent;
        private Wisej.Web.Button button1;
        private Wisej.Web.Label label2;
        private Wisej.Web.Button button2;
        private Wisej.Web.Label label3;
    }
}
