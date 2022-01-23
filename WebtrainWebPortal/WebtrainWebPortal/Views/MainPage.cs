using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using DevExpress.Data.Extensions;
using DevExpress.Web.ASPxSpreadsheet.Internal;
using DevExpress.Web.Internal;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpreadsheet.Import.Xls;
using WebtrainWebPortal.Forms;
using WebtrainWebPortal.Repositories;
using Wisej.Web;
using Wisej.Web.Ext.NavigationBar;


namespace WebtrainWebPortal.Views
{

    public partial class MainPage : Page
    {
        public UserInfo UserInfo { get; set; } = null;

        public WorkflowMediator WorkflowMediator
        {
            get => Application.Session.workflowMediator;
        }

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(UserInfo ui)
        {
            UserInfo = ui;
            InitializeComponent();
        }

        private void UpdateView(eViewState eViewState,int iStateInfo=-1)
        {
            switch (eViewState)
            {
                case eViewState.eUser:
                case eViewState.eUserAdminStudents:
                case eViewState.eUserAdminTrainer:
                    navBarItemUserAdmin.Visible = UserInfo.IsAdmin;
                    navBarItemReports.Visible = UserInfo.IsAdmin;
                    try
                    {
                        if (eViewState == eViewState.eUserAdminStudents)
                        {
                            var dt = WorkflowMediator.UserRepo.GetStudentsForLocation(1);
                            dgvUsers.DataSource = dt;
                            if (dgvUsers.Columns["Aktivieren"] == null)
                            {

                                var btnCol = new DataGridViewButtonColumn();
                                btnCol.Name = "Aktivieren";
                                btnCol.Text = "";
                                dgvUsers.Columns.Insert(dgvUsers.Columns.Count, btnCol);
                            }

                            dgvUsers.Columns["Aktiv"].Visible = false;

                            if (dt.Rows.Count > 0)
                                dgvUsers.Sort(dgvUsers.Columns["Nachname"],ListSortDirection.Ascending);
                        }
                        else
                        {
                            var dt = WorkflowMediator.UserRepo.GetTrainerForLocation(1);
                            dgvTrainer.DataSource = dt;
                            if (dt.Rows.Count > 0)
                                dgvTrainer.Sort(dgvTrainer.Columns["Nachname"], ListSortDirection.Ascending);
                        }
                    }
                    catch (Exception ex)
                    {
                        AlertBox.Show(ex.Message, MessageBoxIcon.Error, true, ContentAlignment.MiddleCenter);
                        throw;
                    }
                    break;
                case eViewState.eTimeRecording:
                    {
                        panTimeRecording.Text = "Zeiterfassung " + UserInfo.Fullname;
                        var dt = WorkflowMediator.TimeRecordRepo.GetAllTimeRecordsByUserEmail(UserInfo.EMail);

                        dgvTimeRecords.DataSource = dt;

                        try
                        {
                            dgvTimeRecords.Columns["Datum"].DefaultCellStyle.Format = "dd/MM/yyyy";
                            dgvTimeRecords.Columns["absenceCompleted"].Visible = false;
                            dgvTimeRecords.Columns["absenceConfirmation"].Visible = false;
                            dgvTimeRecords.Columns["absenceFlag"].Visible = false;
                            dgvTimeRecords.Columns["id"].Visible = false;
                            dgvTimeRecords.Columns["Datum"].ReadOnly = true;
                            dgvTimeRecords.Columns["Start"].ReadOnly = true;
                            dgvTimeRecords.Columns["Ende"].ReadOnly = true;
                            dgvTimeRecords.Columns["Abwesenheit"].ReadOnly = true;

                            if (dgvTimeRecords.Columns["Gesundmeldung"] == null)
                            {
                                var chkCol = new DataGridViewCheckBoxColumn();
                                chkCol.Name = "Gesundmeldung";
                                chkCol.ValueType = typeof(bool);
                                dgvTimeRecords.Columns.Insert(dgvTimeRecords.Columns.Count, chkCol);
                            }

                            if (dgvTimeRecords.Columns["Bestätigung"] == null)
                            {
                                var btnCol = new DataGridViewButtonColumn();
                                btnCol.Name = "Bestätigung";
                                btnCol.Text = "";
                                dgvTimeRecords.Columns.Insert(dgvTimeRecords.Columns.Count, btnCol);
                            }

                            if (dgvTimeRecords.Columns["Abwesenheitsgrund"] == null)
                            {
                                var btnCol = new DataGridViewComboBoxColumn();
                                btnCol.Name = "Abwesenheitsgrund";
                                btnCol.HeaderText = "Abwesenheitsgrund";
                                dgvTimeRecords.Columns.Insert(dgvTimeRecords.Columns.Count, btnCol);
                            }

                            if (dgvTimeRecords.Columns["Geleistete Stunden"] == null)
                            {
                                var hourCol = new DataGridViewColumn();
                                hourCol.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                                hourCol.Name = "Geleistete Stunden";
                                hourCol.HeaderText = "Geleistete\r\nStunden";
                                hourCol.ValueType = typeof(string);
                                dgvTimeRecords.Columns.Insert(dgvTimeRecords.Columns.Count, hourCol);
                            }


                            if (dt.Rows.Count > 0)
                            {
                                var end = dt.Rows[dt.Rows.Count - 1]["Ende"];
                                var strAbsence = dt.Rows[dt.Rows.Count - 1]["Abwesenheit"];
                                int iAbsCompleted = (int)dt.Rows[dt.Rows.Count - 1]["absenceCompleted"];

                                if (end is DBNull)
                                {
                                    btnStart.Enabled = false;
                                    btnEnd.Enabled = true;
                                    btnSickness.Text = "Krankmeldung";
                                    btnSickness.Enabled = false;
                                }
                                else
                                {
                                    if (strAbsence is DBNull || ((string)strAbsence).Length == 0 ||
                                        ((string)strAbsence).IndexOf("Sickness", StringComparison.Ordinal) >= 0 &&
                                        iAbsCompleted == 1)
                                    {
                                        btnSickness.Text = "Krankmeldung";
                                        btnStart.Enabled = true;
                                        btnEnd.Enabled = false;
                                        btnSickness.Enabled = true;
                                    }
                                    else if (((string)strAbsence).IndexOf("Sickness", StringComparison.Ordinal) >= 0 &&
                                             iAbsCompleted == 0)
                                    {
                                        btnSickness.Text = "Gesundmeld.";
                                        btnStart.Enabled = false;
                                        btnEnd.Enabled = false;
                                    }
                                }

                                // StateInfo=x : Spalte x in den Fokus beim updaten
                                if (iStateInfo >= 0)
                                {
                                    dgvTimeRecords.SetCurrentCell(iStateInfo, dt.Rows.Count - 1);
                                    dgvTimeRecords.ScrollCellIntoView(iStateInfo, dt.Rows.Count - 1);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            AlertBox.Show(ex.Message, MessageBoxIcon.Error, true, ContentAlignment.MiddleCenter);
                            throw;
                        }
                        
                    }

                    break;
                case eViewState.eReportAbsenceStudents:
                    {
                        var dtNow = TimeZoneInfo.ConvertTime(dtAbsStudentsActualDate.Value, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));
                        var dt = WorkflowMediator.TimeRecordRepo.GetAllPresentStudents(dtNow);
                        this.dgvAbsentStudents.DataSource = dt;

                        dgvAbsentStudents.Columns["Status"].Visible = false;
                        if (dt.Rows.Count > 0)
                            dgvAbsentStudents.Sort(dgvAbsentStudents.Columns["Nachname"], ListSortDirection.Ascending);
                    }
                    break;
                case eViewState.eReportAbsenceTrainer:
                    {
                        var dtNow = TimeZoneInfo.ConvertTime(dtAbsTrainerActualDate.Value, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));
                        var dt = WorkflowMediator.TimeRecordRepo.GetAllPresentTrainer(dtNow);
                        this.dgvAbsentTrainer.DataSource = dt;

                        dgvAbsentTrainer.Columns["Status"].Visible = false;
                        if (dt.Rows.Count > 0)
                            dgvAbsentTrainer.Sort(dgvAbsentTrainer.Columns["Nachname"], ListSortDirection.Ascending);
                    }
                    break;

                case eViewState.eReportSickness:
                    {
                        var dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));
                        var dt = WorkflowMediator.TimeRecordRepo.GetAllSicknessTimeRecords();
                        this.dgvSickness.DataSource = dt;

                        if (dgvSickness.Columns["Bestätigung"] == null)
                        {
                            var btnCol = new DataGridViewButtonColumn();
                            btnCol.Name = "Bestätigung";
                            btnCol.Text = "";
                            dgvSickness.Columns.Insert(dgvSickness.Columns.Count, btnCol);
                            dgvSickness.Columns["absenceConfirmation"].Visible = false;
                        }

                        if (dgvSickness.Columns["Gesundmeldung"] == null)
                        {
                            var chkCol = new DataGridViewCheckBoxColumn();
                            chkCol.Name = "Gesundmeldung";
                            chkCol.ValueType = typeof(bool);
                            dgvSickness.Columns.Insert(dgvSickness.Columns.Count, chkCol);
                            dgvSickness.Columns["absenceCompleted"].Visible = false;
                        }

                        dgvSickness.Columns["id"].Visible = false;

                        if (dt.Rows.Count>0)
                            dgvSickness.Sort(dgvSickness.Columns["Nachname"], ListSortDirection.Ascending);
                    }
                    break;
            }
        }

        private void LoadFiles(HttpFileCollection files)
        {
            if (files == null)
                return;

            for (int i = 0; i < files.Count; ++i)
            {
                var f = files[i];
                if (f != null)
                {
                    string filePath = Application.MapPath($"~/Data/{f.FileName}");
                    f.SaveAs(filePath);
                    WorkflowMediator.UserRepo.AnalyzeExcelSheetForUsers(filePath);
                }
            }
        }


        private void upload1_Uploaded(object sender, UploadedEventArgs e)
        {
            LoadFiles(e.Files);
            UpdateView(eViewState.eUserAdminStudents);
        }


        public void SetViewState(eViewState eViewState, int iStateInfo=-1)
        {
            switch (eViewState)
            {
                case eViewState.eStart:
                case eViewState.eLogin:
                    navBarMain.Visible = false;

                    panUserlist.Visible = false;
                    panTrainerlist.Visible = false;
                    panReportAbsenceStudents.Visible = false;
                    panReportAbsenceTrainer.Visible = false;
                    panTimeRecording.Visible = false;
                    panReportSickness.Visible = false;
                    panLearnContent.Visible = false;
                    break;
                case eViewState.eUser:
                    navBarMain.Dock = DockStyle.Left;
                    navBarMain.UserName = UserInfo.Fullname;
                    navBarMain.Visible = true;

                    panUserlist.Visible = false;
                    panTrainerlist.Visible = false;
                    panReportAbsenceStudents.Visible = false;
                    panReportAbsenceTrainer.Visible = false; 
                    panTimeRecording.Visible = false;
                    panReportSickness.Visible = false;
                    panLearnContent.Visible = false;
                    break;

                case eViewState.eUserAdminStudents:
                    navBarMain.Dock = DockStyle.Left;
                    navBarMain.UserName = UserInfo.Fullname;
                    navBarMain.Visible = true;

                    panUserlist.Visible = true;
                    panTrainerlist.Visible = false;
                    panReportAbsenceStudents.Visible = false;
                    panReportAbsenceTrainer.Visible = false;
                    panTimeRecording.Visible = false;
                    panReportSickness.Visible = false;
                    panLearnContent.Visible = false;
                    panUserlist.Dock = DockStyle.Fill;
                    break;

                case eViewState.eUserAdminTrainer:
                    navBarMain.Dock = DockStyle.Left;
                    navBarMain.UserName = UserInfo.Fullname;
                    navBarMain.Visible = true;

                    panUserlist.Visible = false;
                    panTrainerlist.Visible = true;
                    panReportAbsenceStudents.Visible = false;
                    panReportAbsenceTrainer.Visible = false;
                    panTimeRecording.Visible = false;
                    panReportSickness.Visible = false;
                    panLearnContent.Visible = false;
                    panTrainerlist.Dock = DockStyle.Fill;
                    break;

                case eViewState.eTimeRecording:
                    navBarMain.Dock = DockStyle.Left;
                    navBarMain.UserName = UserInfo.Fullname;
                    navBarMain.Visible = true;

                    panUserlist.Visible = false;
                    panTrainerlist.Visible = false;
                    panReportAbsenceStudents.Visible = false;
                    panReportAbsenceTrainer.Visible = false;
                    panTimeRecording.Visible = true;
                    panReportSickness.Visible = false;
                    panLearnContent.Visible = false;
                    panTimeRecording.Dock = DockStyle.Fill;
                    break;

                case eViewState.eReportAbsenceStudents:
                    navBarMain.Dock = DockStyle.Left;
                    navBarMain.UserName = UserInfo.Fullname;
                    navBarMain.Visible = true;

                    panUserlist.Visible = false;
                    panTrainerlist.Visible = false;
                    panReportAbsenceStudents.Visible = true;
                    panReportAbsenceTrainer.Visible = false;
                    panTimeRecording.Visible = false;
                    panReportSickness.Visible = false;
                    panLearnContent.Visible = false;
                    panReportAbsenceStudents.Dock = DockStyle.Fill;

                    dtAbsStudentsActualDate.Value = DateTime.Now;
                    break;

                case eViewState.eReportAbsenceTrainer:
                    navBarMain.Dock = DockStyle.Left;
                    navBarMain.UserName = UserInfo.Fullname;
                    navBarMain.Visible = true;
                    
                    panUserlist.Visible = false;
                    panTrainerlist.Visible = false;
                    panReportAbsenceStudents.Visible = false;
                    panReportAbsenceTrainer.Visible = true;
                    panTimeRecording.Visible = false;
                    panReportSickness.Visible = false;
                    panLearnContent.Visible = false;
                    panReportAbsenceTrainer.Dock = DockStyle.Fill;

                    dtAbsTrainerActualDate.Value = DateTime.Now;
                    break;

                case eViewState.eReportSickness:
                    navBarMain.Dock = DockStyle.Left;
                    navBarMain.UserName = UserInfo.Fullname;
                    navBarMain.Visible = true;

                    panUserlist.Visible = false;
                    panTrainerlist.Visible = false;
                    panReportAbsenceStudents.Visible = false;
                    panReportAbsenceTrainer.Visible = true;
                    panTimeRecording.Visible = false;
                    panLearnContent.Visible = false;
                    panReportSickness.Visible = true;
                    panReportSickness.Dock = DockStyle.Fill;
                    break;

                case eViewState.eLearnContent:
                    navBarMain.Dock = DockStyle.Left;
                    navBarMain.UserName = UserInfo.Fullname;
                    navBarMain.Visible = true;

                    panUserlist.Visible = false;
                    panTrainerlist.Visible = false;
                    panReportAbsenceStudents.Visible = false;
                    panReportAbsenceTrainer.Visible = true;
                    panTimeRecording.Visible = false;
                    panReportSickness.Visible = false;
                    panLearnContent.Visible = true;
                    panLearnContent.Dock = DockStyle.Fill;

                    break;
            }

            UpdateView(eViewState,iStateInfo);
        }

        private void MainPage_ResponsiveProfileChanged(object sender, ResponsiveProfileChangedEventArgs e)
        {
            navBarMain.CompactView = (e.CurrentProfile.Name.Contains("Phone"));


            /*
            var display = Display.Both;

            if (Application.Browser.Size.Width < 450)
            {
                display = Display.Icon;
                this.Width = 90;
            }
            else
            {
                this.Width = 250;
            }

            foreach (Control c in this.Controls)
            {
                var button = c as Button;
                if (button != null)
                    button.Display = display;
            }*/
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var dtNow=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));
            if (WorkflowMediator.StartDay(UserInfo, dtNow))
                UpdateView(eViewState.eTimeRecording,1); // StateInfo=1 : Spalte 1 in den Fokus beim updaten
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            var dtNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));
            if (WorkflowMediator.EndDay(UserInfo, dtNow))
                UpdateView(eViewState.eTimeRecording,2); // StateInfo=2 : Spalte 2 in den Fokus beim updaten
        }

        
        private void btnAbsStudentsToday_Click(object sender, EventArgs e)
        {
            dtAbsStudentsActualDate.Value = DateTime.Now;
            UpdateView(eViewState.eReportAbsenceStudents);
        }

        private void dtAbsStudentsActualDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateView(eViewState.eReportAbsenceStudents);
        }

        private void navBarMain_Tap(object sender, EventArgs e)
        {
            navBarMain.CompactView = !navBarMain.CompactView;
        }
        
        private void navBarMain_TitleClick(object sender, EventArgs e)
        {
            navBarMain.CompactView = !navBarMain.CompactView;
        }

        private void navBarMain_UserClick(object sender, EventArgs e)
        {
            navBarMain.CompactView = !navBarMain.CompactView;
        }

        private void dgvAbsentUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = (DataGridView)sender;

            e.CellStyle.ForeColor = Color.White;
            if (Object.Equals(grid["Status", e.RowIndex].Value, 1))
            {
                e.CellStyle.BackColor = Color.LightGreen;
            }
            else if(Object.Equals(grid["Status", e.RowIndex].Value, 2))
            {
                e.CellStyle.BackColor = Color.Red;
            }
            else 
            {
                e.CellStyle.BackColor = Color.DarkGray;
            }
        }

        private void dgvAbsentTrainer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = (DataGridView)sender;

            e.CellStyle.ForeColor = Color.White;
            if (Object.Equals(grid["Status", e.RowIndex].Value, 1))
            {
                e.CellStyle.BackColor = Color.LightGreen;
            }
            else if (Object.Equals(grid["Status", e.RowIndex].Value, 2))
            {
                e.CellStyle.BackColor = Color.Red;
            }
            else
            {
                e.CellStyle.BackColor = Color.DarkGray;
            }
        }


        private void dgvUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = (DataGridView)sender;

            //e.CellStyle.ForeColor = Color.White;
            if (Object.Equals(grid["Aktiv", e.RowIndex].Value, 0))
            {
                e.CellStyle.ForeColor=Color.LightGray;
                var btn = grid["Aktivieren", e.RowIndex] as DataGridViewButtonCell;
                btn.Value = "Anmelden";
            }
            else
            {
                var btn = grid["Aktivieren", e.RowIndex] as DataGridViewButtonCell;
                btn.Value = "Abmelden";
            }
            
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                string username = (string) grid.Rows[e.RowIndex]["Benutzername"].Value;
                string firstname = (string)grid.Rows[e.RowIndex]["Vorname"].Value;
                string lastname = (string)grid.Rows[e.RowIndex]["Nachname"].Value;


                if (grid["Aktivieren", e.RowIndex] is DataGridViewButtonCell btn)
                {
                    bool isActive = ((string)btn.Value) == "Abmelden";
                    if (MessageBox.Show($"Wollen sie den Benutzer: {firstname} {lastname} wirklich {(isActive ? "abmelden" : "anmelden")}?", "Achtung") == DialogResult.OK)
                    {
                        WorkflowMediator.UserRepo.SetActivated(username, isActive ? 0:1);
                        UpdateView(eViewState.eUserAdminStudents);
                    }

                }
            }

        }

        private void btnSickness_Click(object sender, EventArgs e)
        {
            if (btnSickness.Text.IndexOf("Krankmeldung", StringComparison.Ordinal) >= 0)
            {
                using (var dlg = new FrmStartSickness())
                {
                    // show the dialog and wait for it to close.
                    if (dlg.ShowDialog() == Wisej.Web.DialogResult.OK)
                    {
                        if (WorkflowMediator.StartSickness(UserInfo.EMail, dlg.HasSicknessRange, dlg.SicknessStart, dlg.SicknessEnd))
                        {
                            AlertBox.Show("Sie wurden krank gemeldet, bitte Bestätigung hochladen!", MessageBoxIcon.Information, true, ContentAlignment.MiddleCenter, 0);
                            UpdateView(eViewState.eTimeRecording);
                        }
                    }
                }
            }
            else
            {
                if (MessageBox.Show($"Wollen sie sich wirklich gesund melden?", "Achtung") == DialogResult.OK)
                {
                    if (!WorkflowMediator.EndSickness(UserInfo.EMail, DateTime.Today))
                    {
                        AlertBox.Show("Unerwarteter Fehler: Gesundmelden derzeit nicht möglich!",
                            MessageBoxIcon.Error, true, ContentAlignment.MiddleCenter, 0);
                    }
                    else
                    {
                        AlertBox.Show("Sie wurden wieder gesund gemeldet!", MessageBoxIcon.Information, true,
                            ContentAlignment.MiddleCenter, 0);
                        btnSickness.Text = "Krankmeldung";
                        btnStart.Enabled = true;
                        btnEnd.Enabled = false;
                        UpdateView(eViewState.eTimeRecording);
                    }
                }
            }

        }

        private void navBarItemLogout_Click(object sender, EventArgs e)
        {
            WorkflowMediator.SetViewState(UserInfo, eViewState.eLogin);
            WorkflowMediator.RemoveUser(UserInfo);
        }

        private void navBarItemUserAdminStudents_Click(object sender, EventArgs e)
        {
            WorkflowMediator.SetViewState(UserInfo, eViewState.eUserAdminStudents);
        }


        private void navBarItemDownloads_Click(object sender, EventArgs e)
        {
            using (var dlg = new FrmDownloads())
            {
                // show the dialog and wait for it to close.
                if (dlg.ShowDialog() == Wisej.Web.DialogResult.OK)
                {
                }
            }
        }

        private void navBarItemReports_PanelCollapsed(object sender, EventArgs e)
        {

        }

        private void navBarItemUserAdmin_PanelCollapsed(object sender, EventArgs e)
        {

        }

        private void navBarItemUserAdminTrainer_Click(object sender, EventArgs e)
        {
            WorkflowMediator.SetViewState(UserInfo, eViewState.eUserAdminTrainer);
        }

        private void navBarItemsReportAbsenceStudents_Click(object sender, EventArgs e)
        {
            WorkflowMediator.SetViewState(UserInfo, eViewState.eReportAbsenceStudents);
        }

        private void navBarItemTimeRecording_Click(object sender, EventArgs e)
        {
            WorkflowMediator.SetViewState(UserInfo, eViewState.eTimeRecording);
        }

        private void navBarItemsReportAbsenceTrainer_Click(object sender, EventArgs e)
        {
            WorkflowMediator.SetViewState(UserInfo, eViewState.eReportAbsenceTrainer);
        }

        private void navBarItemsReportSickness_Click(object sender, EventArgs e)
        {
            WorkflowMediator.SetViewState(UserInfo, eViewState.eReportSickness);
        }

        private void dgvSickness_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = (DataGridView)sender;

            //e.CellStyle.ForeColor = Color.White;
            if (Object.Equals(grid["absenceConfirmation", e.RowIndex].Value, 0))
            {
                e.CellStyle.ForeColor = Color.LightGray;
                if (grid["Bestätigung", e.RowIndex] is DataGridViewButtonCell btn)
                {
                    btn.Value = "fehlt";
                    btn.ReadOnly = true;
                }
            }
            else
            {
                if (grid["Bestätigung", e.RowIndex] is DataGridViewButtonCell btn)
                {
                    btn.Value = "Anzeigen";
                    btn.ReadOnly = false;
                }
            }

            if (Object.Equals(grid["absenceCompleted", e.RowIndex].Value, 0))
            {
                if (grid["Gesundmeldung", e.RowIndex] is DataGridViewCheckBoxCell chk)
                {
                    chk.Value = false;
                    chk.ReadOnly = true;
                }
            }
            else
            {
                if (grid["Gesundmeldung", e.RowIndex] is DataGridViewCheckBoxCell btn)
                {
                    btn.Value = true;
                    btn.ReadOnly = true;
                }
            }


        }


        private void dgvTimeRecords_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = (DataGridView) sender;

            if (grid.Rows.Count > 0 && grid.Columns.Count > 0)
            {
                try
                {
                    int iAbsCompleted = (int)grid["absenceCompleted", e.RowIndex].Value;
                    if (iAbsCompleted==0) //7
                    {
                        if (grid["Gesundmeldung", e.RowIndex] is DataGridViewCheckBoxCell chk)
                        {
                            chk.Value = false;
                            chk.ReadOnly = true;
                        }
                    }
                    else
                    {
                        if (grid["Gesundmeldung", e.RowIndex] is DataGridViewCheckBoxCell btn)
                        {
                            btn.Value = true;
                            btn.ReadOnly = true;
                        }
                    }
                    
                    string strAbsenceReason = (string) grid["Abwesenheit", e.RowIndex].Value;
                    int iAbsConfirmation = (int)grid["absenceConfirmation", e.RowIndex].Value;
                    int iAbsFlag = 1;

                    if (strAbsenceReason.IndexOf("Sickness", StringComparison.OrdinalIgnoreCase)>=0)
                    {
                        if (strAbsenceReason.IndexOf("Range", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            iAbsFlag = (int)grid["absenceFlag", e.RowIndex].Value;
                        }

                        if (iAbsFlag == 1)
                        {
                            if (iAbsConfirmation == 0)
                            {
                                if (grid["Bestätigung", e.RowIndex] is DataGridViewButtonCell btn)
                                {
                                    btn.Value = "Hochladen";
                                    btn.ReadOnly = false;
                                }
                            }
                            else
                            {
                                if (grid["Bestätigung", e.RowIndex] is DataGridViewButtonCell btn)
                                {
                                    btn.Value = "Anzeigen";
                                    btn.ReadOnly = true;
                                }
                            }
                        }

                    }

                    if (strAbsenceReason.IndexOf("SicknessDaily",StringComparison.OrdinalIgnoreCase) >= 0) //3
                    {
                        grid["Abwesenheit", e.RowIndex].Value = "Krank (Tag)";
                    }

                    if (strAbsenceReason.IndexOf("SicknessRange", StringComparison.OrdinalIgnoreCase) >= 0) //3
                    {
                        grid["Abwesenheit", e.RowIndex].Value = "Krank";
                    }

                    if (strAbsenceReason.Length == 0 ||
                        (strAbsenceReason.IndexOf("Sickness", StringComparison.OrdinalIgnoreCase) < 0 &&
                         strAbsenceReason.IndexOf("Krank", StringComparison.OrdinalIgnoreCase) < 0))
                    {
                        if (grid["Abwesenheitsgrund", e.RowIndex] is DataGridViewComboBoxCell cboBoxCell)
                        {
                            if (cboBoxCell.Items.Count == 0)
                            {
                                cboBoxCell.Items.Add("Kein");
                                cboBoxCell.Items.Add("Arzt");
                                cboBoxCell.Items.Add("Urlaub");
                            }

                            cboBoxCell.Value = strAbsenceReason.Length > 0 ? strAbsenceReason : "Kein";
                        }
                    }
                    else
                    {
                        grid["Abwesenheitsgrund", e.RowIndex].ReadOnly = true;
                    }


                    if (!(grid["Ende", e.RowIndex].Value is DBNull))
                    {
                        var timeStart = (TimeSpan) grid["Start", e.RowIndex].Value;
                        var timeEnd = (TimeSpan) grid["Ende", e.RowIndex].Value;
                        TimeSpan ts = timeEnd - timeStart;
                        string strRes = String.Format("{0:0.00}", ts.TotalHours);
                        grid["Geleistete Stunden", e.RowIndex].Value = strRes;
                    }
                }
                catch (Exception ex)
                {
                    AlertBox.Show(ex.Message, MessageBoxIcon.Error, true, ContentAlignment.MiddleCenter);
                }
            }
        }

        
        private void dgvTimeRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                var grid = (DataGridView)sender;

                if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (grid["Bestätigung", e.RowIndex] is DataGridViewButtonCell btn)
                    {
                        if (btn.Value != null)
                        {
                            bool bUpload = ((string)btn.Value) == "Hochladen";
                            int iRecId = (int)grid["id", e.RowIndex].Value;
                            if (bUpload)
                            {
                                using (var dlg = new FrmUploadViewer("", UserInfo.Username, iRecId))
                                {
                                    // show the dialog and wait for it to close.
                                    if (dlg.ShowDialog() == DialogResult.OK)
                                    {
                                        if (!WorkflowMediator.AddSicknessConfirmation(UserInfo.EMail, iRecId, System.IO.Path.GetFileName(dlg.FilePath)))
                                        {
                                            AlertBox.Show("Unerwarteter Fehler: Bestätigung senden derzeit nicht möglich!",
                                                MessageBoxIcon.Information, true, ContentAlignment.MiddleCenter, 0);
                                        }
                                        else
                                        {
                                            UpdateView(eViewState.eTimeRecording);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!WorkflowMediator.GetSicknessConfirmation(UserInfo.EMail, iRecId, out string strFilepath))
                                {
                                    AlertBox.Show("Unerwarteter Fehler: Bestätigung anzeigen derzeit nicht möglich!",
                                        MessageBoxIcon.Information, true, ContentAlignment.MiddleCenter, 0);
                                }
                                else
                                {
                                    var dlg = new FrmUploadViewer("Datei anzeigen", UserInfo.Username, -1, strFilepath);
                                    dlg.ShowDialog();
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private void dgvSickness_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (grid["Bestätigung", e.RowIndex] is DataGridViewButtonCell btn)
                    {
                        bool bShow = ((string) btn.Value) == "Anzeigen";
                        if (bShow)
                        {
                            int iRecId = (int) grid["id", e.RowIndex].Value;
                            if (!WorkflowMediator.GetSicknessConfirmation(UserInfo.EMail, iRecId,
                                out string strFilepath))
                            {
                                AlertBox.Show("Unerwarteter Fehler: Bestätigung anzeigen derzeit nicht möglich!",
                                    MessageBoxIcon.Information, true, ContentAlignment.MiddleCenter, 0);
                            }
                            else
                            {
                                var dlg = new FrmUploadViewer("Datei anzeigen", UserInfo.Username, -1, strFilepath);
                                dlg.ShowDialog();
                            }
                        }
                    }
                }
            }
        }

        private void btnAbsTrainerToday_Click(object sender, EventArgs e)
        {
            dtAbsTrainerActualDate.Value = DateTime.Now;
            UpdateView(eViewState.eReportAbsenceTrainer);
        }

        private void dtAbsTrainerActualDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateView(eViewState.eReportAbsenceTrainer);
        }

        private void dgvTimeRecords_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTimeRecords_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void dgvTimeRecords_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (((DataGridView)sender).CurrentCell.ColumnIndex == 10) 
            {
                var cb = e.Control as ComboBox;
                if (cb != null)
                {
                    cb.SelectionChangeCommitted -= new EventHandler(cb_SelectedIndexChanged);
                    // now attach the event handler
                    cb.SelectionChangeCommitted += new EventHandler(cb_SelectedIndexChanged);
                }
            }
        }

        void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb != null)
            {
                var strText = cb.SelectedItem as string;
                var oldText = dgvTimeRecords.CurrentCell.Value as string;
                if (strText != oldText)
                {
                    int iRecId = (int)dgvTimeRecords["id", dgvTimeRecords.CurrentCell.RowIndex].Value;
                    WorkflowMediator.TimeRecordRepo.SetAbsenceReason(iRecId, strText);
                    dgvTimeRecords.CurrentCell.Value = strText;
                    dgvTimeRecords.EndEdit();
                    UpdateView(eViewState.eTimeRecording/*, 10*/); // StateInfo=1 : Spalte 10 in den Fokus beim updaten
                }
            }
        }

        private void navBarItemLearnContent_Click(object sender, EventArgs e)
        {
            WorkflowMediator.SetViewState(UserInfo, eViewState.eLearnContent);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Navigate("webtrain:\\test1=10?test2=20");
        }
    }
}
