using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.Controls;
using SoftObject.TrainConcept.Controls;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Summary description for SampleWizard.
	/// </summary>
    public partial class FrmSetupWizard : XtraForm
	{
        /// <summary>
        /// Creates a new instance of the <see cref="SampleWizard"/> class.
        /// </summary>
        private AppHandler AppHandler = Program.AppHandler; 
		public FrmSetupWizard()
		{
			// required for designer support
			this.InitializeComponent();
		}

		/// <summary>
		/// Starts a sample task simulation.
		/// </summary>
		private void StartTask()
		{
			// setup wizard page
			this.wizardSample.BackEnabled = false;
			this.wizardSample.NextEnabled = false;
			this.progressLongTask.Value = this.progressLongTask.Minimum;

			// start timer to simulate a long running task
			this.timerTask.Enabled = true;
            

            string[] aUsers;
            AppHandler.UserManager.GetUserNames(out aUsers);
            if (aUsers != null)
                for (int i = 0; i < aUsers.Length; ++i )
                    AppHandler.UserManager.DeleteUser(aUsers[i]);
            AppHandler.UserManager.CreateUser("Administrator", "admin", "admin");
            AppHandler.UserManager.SetUserRights("Administrator", true, true);

            while(AppHandler.MapManager.GetMapCnt()>0)
            {
                string strTitle = null;
                AppHandler.MapManager.GetTitle(0,out strTitle);
                AppHandler.MapManager.Destroy(strTitle);
            }

            bool bResult = true;
            for (int i = 0; i < this.teachersBindingSource.Count; ++i)
            {
                UserEditTreeRecord rec = this.teachersBindingSource[i] as UserEditTreeRecord;
                if (rec!=null && !AppHandler.UserManager.CreateUser(rec.FullName, rec.UserName, String.Format("lehrer{0}",i+1)))
                    bResult = false;

                if (!AppHandler.UserManager.SetUserRights(rec.UserName, true, true))
                    bResult = false;
            }

            for (int i = 0; i < this.schoolersBindingSource.Count; ++i)
            {
                UserEditTreeRecord rec = this.schoolersBindingSource[i] as UserEditTreeRecord;
                if (rec != null && !AppHandler.UserManager.CreateUser(rec.FullName, rec.UserName, String.Format("schueler{0}", i + 1)))
                    bResult = false;

                if (!AppHandler.UserManager.SetUserRights(rec.UserName, false, false))
                    bResult = false;
            }
            AppHandler.UserManager.Save();

            if (!bResult)
            {
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "Failed_to_create_userentry", "Fehler bei der Benutzererstellung!");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (this.rbnBeginner.Checked ||
                this.rbnBasicKnowledge.Checked ||
                this.rbnExpertKnowledge.Checked)
            {
                for (int i = 0; i < AppHandler.LibManager.GetLibraryCnt(); ++i)
                {
                    var lib = AppHandler.LibManager.GetLibrary(i);
                    string strTitle = lib.title;
                    string strVersion = lib.version;
                    AppHandler.MainForm.ControlCenter.CreateEditLearnmap(strTitle);
                    LearnmapBuilder learnmapBuilder = new LearnmapBuilder(strTitle);
                    learnmapBuilder.AddLibrary(strTitle,strVersion);

                    AppHandler.UserManager.GetUserNames(out aUsers);
                    AppHandler.MapManager.SetUsers(strTitle, aUsers);
                    AppHandler.MapManager.Save(strTitle);
                }
            }
            else
            {
            }
		}

		/// <summary>
		/// Handles the AfterSwitchPages event of wizardSample.
		/// </summary>
		private void wizardSample_AfterSwitchPages(object sender, Wizard.AfterSwitchPagesEventArgs e)
		{
			// get wizard page to be displayed
			WizardPage newPage = this.wizardSample.Pages[e.NewIndex];
			WizardPage oldPage = this.wizardSample.Pages[e.OldIndex];

			// check if progress page
			if (newPage == this.pageProgress)
			{
				// start the sample task
				this.StartTask();
			}
            else if (newPage == this.pageUsers2)
            {
                if (teachersBindingSource.Count == 0 ||
                    teachersBindingSource.Count != this.spinEdit1.Value)
                {
                    this.teachersBindingSource.Clear();
                    for (int i = 0; i < this.spinEdit1.Value; ++i)
                    {
                        string strFullName;
                        string strUserName;

                        strFullName = String.Format("Lehrer{0}", i + 1);
                        strUserName = String.Format("lehrer{0}", i + 1);
                        this.teachersBindingSource.Add(new UserEditTreeRecord(i + 1, 0, 0, strUserName, strFullName, "Lehrer"));
                    }
                }
            }
            else if (newPage == this.pageUsers3)
            {
                if (schoolersBindingSource.Count == 0  ||
                    schoolersBindingSource.Count != this.spinEdit1.Value)
                {
                    this.schoolersBindingSource.Clear();
                    for (int i = 0; i < this.spinEdit2.Value; ++i)
                    {
                        string strFullName;
                        string strUserName;

                        strFullName = String.Format("Schüler{0}", i + 1);
                        strUserName = String.Format("schueler{0}", i + 1);
                        this.schoolersBindingSource.Add(new UserEditTreeRecord(i + 1, 0, 0, strUserName, strFullName, "Lehrer"));
                    }
                }
            }
        }

		/// <summary>
		/// Handles the BeforeSwitchPages event of wizardSample.
		/// </summary>
		private void wizardSample_BeforeSwitchPages(object sender, Wizard.BeforeSwitchPagesEventArgs e)
		{
			// angezeigte Seite holen
			WizardPage oldPage = this.wizardSample.Pages[e.OldIndex];

			// weg von Map Page?
			if (oldPage == this.pageMaps && e.NewIndex > e.OldIndex)
			{
				if (this.rbnUnknownKnowledge.Checked == true)
				{
                    string txt = AppHandler.LanguageHandler.GetText("WARNING", "You_have_to_create_the_maps_later", "Sie müssen die Lernmappen zu einem späteren Zeitpunkt erstellen!");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt);
                    //e.Cancel = true;
				}
                //// check if user choosed to skip validation
                //else if (this.rbnBeginner.Checked)
                //{
                //    // skip the validation page
                //    e.NewIndex++;
                //}
			}
            else if (oldPage == this.pageClasses && e.NewIndex > e.OldIndex)
            {
                string[] aUsers;
                AppHandler.UserManager.GetUserNames(out aUsers);
                if ((aUsers != null && aUsers.Length > 1))
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Setup_Wizard_User_already_exist", "Sie haben bereits Benutzer angelegt.\nWenn sie mit dem Assistenten fortfahren, werden diese gelöscht.\nMöchten Sie fortfahren?");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Hand)==DialogResult.No)
                        e.Cancel = true;
                }

                if (AppHandler.MapManager.GetMapCnt() > 0)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Setup_Wizard_Maps_already_exist", "Sie haben bereits Lernmappen erstellt.\nWenn sie mit dem Assistenten fortfahren, werden diese gelöscht.\nMöchten Sie fortfahren?");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                        e.Cancel = true;
                }
            }

		}

		/// <summary>
		/// Handles the Cancel event of wizardSample.
		/// </summary>
		private void wizardSample_Cancel(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// check if task is running
			bool isTaskRunning = this.timerTask.Enabled;
			// stop the task
			this.timerTask.Enabled = false;

            string txt = AppHandler.LanguageHandler.GetText("WARNING", "are_you_sure_you_want_to_exit_the_setup_wizard", "Sind sie sicher, daß sie den Setup-Assistenten beenden wollen?");
            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
            if (MessageBox.Show(txt,cap,MessageBoxButtons.YesNo) == DialogResult.No)
			{
				// cancel closing
				e.Cancel = true;
				// restart the task
				this.timerTask.Enabled = isTaskRunning;
			}
		}

		/// <summary>
		/// Handles the Finish event of wizardSample.
		/// </summary>
        private void wizardSample_Finish(object sender, System.EventArgs e)
        {
            string txt = AppHandler.LanguageHandler.GetText("WARNING", "the_setup_wizard_has_successfully_completed", "Der Setup-Assist wurde erfolgreich beendet.\nTrainConcept ist fertig eingerichtet.");
            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
            MessageBox.Show(txt, cap);
        }

		/// <summary>
		/// Handles the Help event of wizardSample.
		/// </summary>
        private void wizardSample_Help(object sender, System.EventArgs e)
        {
            MessageBox.Show("This is a realy cool wizard control!\n:-)",
							this.Text,
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
        }

		/// <summary>
		/// Handles the Tick event of timerTask.
		/// </summary>
		private void timerTask_Tick(object sender, System.EventArgs e)
		{
			// check if task compleeted
			if (this.progressLongTask.Value == this.progressLongTask.Maximum)
			{
				// stop the timer & switch to next page
				this.timerTask.Enabled = false;
				this.wizardSample.Next();
			}
			else
			{
				// update progress bar
				this.progressLongTask.PerformStep();
			}
		}

        private void label3_Click(object sender, EventArgs e)
        {
        }

    }
}
