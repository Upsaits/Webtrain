using SoftObject.TrainConcept.Controls;
using SoftObject.SOComponents.Controls;

namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmSetupWizard
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

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetupWizard));
            this.pageMaps = new WizardPage();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.grpMapMode = new System.Windows.Forms.GroupBox();
            this.rbnUnknownKnowledge = new System.Windows.Forms.RadioButton();
            this.rbnExpertKnowledge = new System.Windows.Forms.RadioButton();
            this.rbnBeginner = new System.Windows.Forms.RadioButton();
            this.rbnBasicKnowledge = new System.Windows.Forms.RadioButton();
            this.pageFinish = new WizardPage();
            this.wizardSample = new Wizard();
            this.pageWelcome = new WizardPage();
            this.pageProgress = new WizardPage();
            this.labelProgress = new System.Windows.Forms.Label();
            this.progressLongTask = new System.Windows.Forms.ProgressBar();
            this.pageClasses = new WizardPage();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.grpClassMode = new System.Windows.Forms.GroupBox();
            this.rbnNoClasses = new System.Windows.Forms.RadioButton();
            this.rbnMapFixedClass = new System.Windows.Forms.RadioButton();
            this.rbnUserFixedClass = new System.Windows.Forms.RadioButton();
            this.pageUsers3 = new WizardPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.schoolersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pageUsers2 = new WizardPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.fullName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.userName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.teachersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pageUsers1 = new WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.timerTask = new System.Windows.Forms.Timer(this.components);
            this.pageMaps.SuspendLayout();
            this.grpMapMode.SuspendLayout();
            this.wizardSample.SuspendLayout();
            this.pageProgress.SuspendLayout();
            this.pageClasses.SuspendLayout();
            this.grpClassMode.SuspendLayout();
            this.pageUsers3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schoolersBindingSource)).BeginInit();
            this.pageUsers2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teachersBindingSource)).BeginInit();
            this.pageUsers1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pageMaps
            // 
            this.pageMaps.Controls.Add(this.richTextBox3);
            this.pageMaps.Controls.Add(this.grpMapMode);
            this.pageMaps.Description = "Hier legen sie fest, welche Lernmappen erstellt werden sollen.";
            this.pageMaps.Location = new System.Drawing.Point(0, 0);
            this.pageMaps.Name = "pageMaps";
            this.pageMaps.Size = new System.Drawing.Size(428, 224);
            this.pageMaps.TabIndex = 11;
            this.pageMaps.Title = "2. Lernmappen erstellen";
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.Location = new System.Drawing.Point(12, 60);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.Size = new System.Drawing.Size(399, 77);
            this.richTextBox3.TabIndex = 4;
            this.richTextBox3.Text = "Die Lernmappen werden automatisch aufgrund der von Ihnen \nangegebenen Abschätzung" +
    " der Vorkenntnisse der Schüler, \nerstellt.\n\nWie würden sie die Vorkenntnisse der" +
    " Schüler einschätzen?";
            // 
            // grpMapMode
            // 
            this.grpMapMode.Controls.Add(this.rbnUnknownKnowledge);
            this.grpMapMode.Controls.Add(this.rbnExpertKnowledge);
            this.grpMapMode.Controls.Add(this.rbnBeginner);
            this.grpMapMode.Controls.Add(this.rbnBasicKnowledge);
            this.grpMapMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpMapMode.Location = new System.Drawing.Point(12, 143);
            this.grpMapMode.Name = "grpMapMode";
            this.grpMapMode.Size = new System.Drawing.Size(399, 156);
            this.grpMapMode.TabIndex = 0;
            this.grpMapMode.TabStop = false;
            this.grpMapMode.Text = "Möglichkeiten";
            // 
            // rbnUnknownKnowledge
            // 
            this.rbnUnknownKnowledge.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnUnknownKnowledge.Location = new System.Drawing.Point(18, 114);
            this.rbnUnknownKnowledge.Name = "rbnUnknownKnowledge";
            this.rbnUnknownKnowledge.Size = new System.Drawing.Size(260, 22);
            this.rbnUnknownKnowledge.TabIndex = 3;
            this.rbnUnknownKnowledge.Text = "Einschätzung im Moment nicht möglich";
            // 
            // rbnExpertKnowledge
            // 
            this.rbnExpertKnowledge.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnExpertKnowledge.Location = new System.Drawing.Point(18, 86);
            this.rbnExpertKnowledge.Name = "rbnExpertKnowledge";
            this.rbnExpertKnowledge.Size = new System.Drawing.Size(260, 22);
            this.rbnExpertKnowledge.TabIndex = 2;
            this.rbnExpertKnowledge.Text = "Experten";
            // 
            // rbnBeginner
            // 
            this.rbnBeginner.Checked = true;
            this.rbnBeginner.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnBeginner.Location = new System.Drawing.Point(18, 30);
            this.rbnBeginner.Name = "rbnBeginner";
            this.rbnBeginner.Size = new System.Drawing.Size(260, 22);
            this.rbnBeginner.TabIndex = 0;
            this.rbnBeginner.TabStop = true;
            this.rbnBeginner.Text = "Anfänger bzw. Neueinsteiger";
            // 
            // rbnBasicKnowledge
            // 
            this.rbnBasicKnowledge.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnBasicKnowledge.Location = new System.Drawing.Point(18, 58);
            this.rbnBasicKnowledge.Name = "rbnBasicKnowledge";
            this.rbnBasicKnowledge.Size = new System.Drawing.Size(260, 22);
            this.rbnBasicKnowledge.TabIndex = 1;
            this.rbnBasicKnowledge.Text = "Grundkenntnisse vorhanden";
            // 
            // pageFinish
            // 
            this.pageFinish.Description = "Thank you for using the Sample Wizard.\nPress OK to exit.";
            this.pageFinish.Location = new System.Drawing.Point(0, 0);
            this.pageFinish.Name = "pageFinish";
            this.pageFinish.Size = new System.Drawing.Size(505, 379);
            this.pageFinish.Style = WizardPageStyle.Finish;
            this.pageFinish.TabIndex = 12;
            this.pageFinish.Title = "Sample Wizard has finished";
            // 
            // wizardSample
            // 
            this.wizardSample.Controls.Add(this.pageWelcome);
            this.wizardSample.Controls.Add(this.pageFinish);
            this.wizardSample.Controls.Add(this.pageProgress);
            this.wizardSample.Controls.Add(this.pageClasses);
            this.wizardSample.Controls.Add(this.pageMaps);
            this.wizardSample.Controls.Add(this.pageUsers3);
            this.wizardSample.Controls.Add(this.pageUsers2);
            this.wizardSample.Controls.Add(this.pageUsers1);
            this.wizardSample.HeaderImage = ((System.Drawing.Image)(resources.GetObject("wizardSample.HeaderImage")));
            this.wizardSample.Location = new System.Drawing.Point(0, 0);
            this.wizardSample.Name = "wizardSample";
            this.wizardSample.Pages.AddRange(new WizardPage[] {
            this.pageWelcome,
            this.pageUsers1,
            this.pageUsers2,
            this.pageUsers3,
            this.pageMaps,
            this.pageClasses,
            this.pageProgress,
            this.pageFinish});
            this.wizardSample.Size = new System.Drawing.Size(505, 400);
            this.wizardSample.TabIndex = 0;
            this.wizardSample.WelcomeImage = ((System.Drawing.Image)(resources.GetObject("wizardSample.WelcomeImage")));
            this.wizardSample.BeforeSwitchPages += new Wizard.BeforeSwitchPagesEventHandler(this.wizardSample_BeforeSwitchPages);
            this.wizardSample.AfterSwitchPages += new Wizard.AfterSwitchPagesEventHandler(this.wizardSample_AfterSwitchPages);
            this.wizardSample.Cancel += new System.ComponentModel.CancelEventHandler(this.wizardSample_Cancel);
            this.wizardSample.Finish += new System.EventHandler(this.wizardSample_Finish);
            this.wizardSample.Help += new System.EventHandler(this.wizardSample_Help);
            // 
            // pageWelcome
            // 
            this.pageWelcome.Description = "Dieser Assistent wird sie beim Einrichten von WebTrain unterstützen";
            this.pageWelcome.Location = new System.Drawing.Point(0, 0);
            this.pageWelcome.Name = "pageWelcome";
            this.pageWelcome.Size = new System.Drawing.Size(505, 352);
            this.pageWelcome.Style = WizardPageStyle.Welcome;
            this.pageWelcome.TabIndex = 9;
            this.pageWelcome.Title = "Willkommen beim Einrichtungsassistent";
            // 
            // pageProgress
            // 
            this.pageProgress.Controls.Add(this.labelProgress);
            this.pageProgress.Controls.Add(this.progressLongTask);
            this.pageProgress.Description = "This simulates a long running sample task.";
            this.pageProgress.Location = new System.Drawing.Point(0, 0);
            this.pageProgress.Name = "pageProgress";
            this.pageProgress.Size = new System.Drawing.Size(428, 224);
            this.pageProgress.TabIndex = 10;
            this.pageProgress.Title = "Task Running";
            // 
            // labelProgress
            // 
            this.labelProgress.Location = new System.Drawing.Point(20, 90);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(252, 18);
            this.labelProgress.TabIndex = 1;
            this.labelProgress.Text = "Please wait while the wizard does a long task...";
            // 
            // progressLongTask
            // 
            this.progressLongTask.Location = new System.Drawing.Point(16, 112);
            this.progressLongTask.Name = "progressLongTask";
            this.progressLongTask.Size = new System.Drawing.Size(436, 22);
            this.progressLongTask.TabIndex = 0;
            // 
            // pageClasses
            // 
            this.pageClasses.Controls.Add(this.richTextBox4);
            this.pageClasses.Controls.Add(this.grpClassMode);
            this.pageClasses.Description = "Hier legen Sie den Klassenmodus fest.";
            this.pageClasses.Location = new System.Drawing.Point(0, 0);
            this.pageClasses.Name = "pageClasses";
            this.pageClasses.Size = new System.Drawing.Size(428, 224);
            this.pageClasses.TabIndex = 13;
            this.pageClasses.Title = "3. Klassenmodus festlegen";
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.Location = new System.Drawing.Point(12, 74);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.ReadOnly = true;
            this.richTextBox4.Size = new System.Drawing.Size(442, 64);
            this.richTextBox4.TabIndex = 5;
            this.richTextBox4.Text = "Webtrain kann falls erforderlich, zur besseren Übersicht über den Lernerfolg \nKla" +
    "ssenräume verwenden.\n\nIn welcher Art sollen die Klassenräume verwendet werden? ";
            // 
            // grpClassMode
            // 
            this.grpClassMode.Controls.Add(this.rbnNoClasses);
            this.grpClassMode.Controls.Add(this.rbnMapFixedClass);
            this.grpClassMode.Controls.Add(this.rbnUserFixedClass);
            this.grpClassMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpClassMode.Location = new System.Drawing.Point(12, 144);
            this.grpClassMode.Name = "grpClassMode";
            this.grpClassMode.Size = new System.Drawing.Size(442, 161);
            this.grpClassMode.TabIndex = 1;
            this.grpClassMode.TabStop = false;
            this.grpClassMode.Text = "Möglichkeiten";
            // 
            // rbnNoClasses
            // 
            this.rbnNoClasses.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnNoClasses.Location = new System.Drawing.Point(16, 88);
            this.rbnNoClasses.Name = "rbnNoClasses";
            this.rbnNoClasses.Size = new System.Drawing.Size(379, 22);
            this.rbnNoClasses.TabIndex = 3;
            this.rbnNoClasses.Text = "Zunächst noch keine Klassenaufteilung vornehmen";
            // 
            // rbnMapFixedClass
            // 
            this.rbnMapFixedClass.Checked = true;
            this.rbnMapFixedClass.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnMapFixedClass.Location = new System.Drawing.Point(16, 32);
            this.rbnMapFixedClass.Name = "rbnMapFixedClass";
            this.rbnMapFixedClass.Size = new System.Drawing.Size(401, 22);
            this.rbnMapFixedClass.TabIndex = 0;
            this.rbnMapFixedClass.TabStop = true;
            this.rbnMapFixedClass.Text = "Schüler mit gleicher Lernmappe sollten einer Klasse zugeordnet sein";
            // 
            // rbnUserFixedClass
            // 
            this.rbnUserFixedClass.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnUserFixedClass.Location = new System.Drawing.Point(16, 60);
            this.rbnUserFixedClass.Name = "rbnUserFixedClass";
            this.rbnUserFixedClass.Size = new System.Drawing.Size(344, 22);
            this.rbnUserFixedClass.TabIndex = 1;
            this.rbnUserFixedClass.Text = "Die Schüleranzahl sollte gleichmäßig auf Klassen aufgeteilt sein.";
            // 
            // pageUsers3
            // 
            this.pageUsers3.Controls.Add(this.richTextBox2);
            this.pageUsers3.Controls.Add(this.treeList2);
            this.pageUsers3.Description = "Hier legen sie fest, welche Benutzer (Lehrer und Schüler) am System angemeldet we" +
    "rden können.";
            this.pageUsers3.Location = new System.Drawing.Point(0, 0);
            this.pageUsers3.Name = "pageUsers3";
            this.pageUsers3.Size = new System.Drawing.Size(428, 224);
            this.pageUsers3.TabIndex = 16;
            this.pageUsers3.Title = "1. Benutzer einrichten";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(12, 73);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(406, 64);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "Geben Sie die Daten der Schüler im folgendem Format ein:\n\nVor-und Nachname:\t\t(bel" +
    "iebige Zeichen)\t\tSebastian Neudorfer\nBenutzername:\t\t(A-Z,a-z,0-9)\t\tsneudorfer";
            // 
            // treeList2
            // 
            this.treeList2.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.treeList2.DataSource = this.schoolersBindingSource;
            this.treeList2.Location = new System.Drawing.Point(12, 143);
            this.treeList2.Name = "treeList2";
            this.treeList2.Size = new System.Drawing.Size(406, 173);
            this.treeList2.TabIndex = 2;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Vor- und Nachname";
            this.treeListColumn1.FieldName = "FullName";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Benutzername";
            this.treeListColumn2.FieldName = "UserName";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            // 
            // schoolersBindingSource
            // 
            this.schoolersBindingSource.DataSource = typeof(UserEditTreeRecord);
            // 
            // pageUsers2
            // 
            this.pageUsers2.Controls.Add(this.richTextBox1);
            this.pageUsers2.Controls.Add(this.treeList1);
            this.pageUsers2.Description = "Hier legen sie fest, welche Benutzer (Lehrer und Schüler) am System angemeldet we" +
    "rden können.";
            this.pageUsers2.Location = new System.Drawing.Point(0, 0);
            this.pageUsers2.Name = "pageUsers2";
            this.pageUsers2.Size = new System.Drawing.Size(428, 224);
            this.pageUsers2.TabIndex = 15;
            this.pageUsers2.Title = "1. Benutzer einrichten";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(12, 73);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(442, 64);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "Geben Sie die Daten der Lehrer im folgendem Format ein:\n\nVor-und Nachname:\t\t(beli" +
    "ebige Zeichen)\t\tHr. Franz Mair\nBenutzername:\t\t(A-Z,a-z,0-9)\t\tfmair";
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.fullName,
            this.userName});
            this.treeList1.DataSource = this.teachersBindingSource;
            this.treeList1.Location = new System.Drawing.Point(12, 143);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(442, 173);
            this.treeList1.TabIndex = 0;
            // 
            // fullName
            // 
            this.fullName.Caption = "Vor- und Nachname";
            this.fullName.FieldName = "FullName";
            this.fullName.Name = "fullName";
            this.fullName.Visible = true;
            this.fullName.VisibleIndex = 0;
            // 
            // userName
            // 
            this.userName.Caption = "Benutzername";
            this.userName.FieldName = "UserName";
            this.userName.Name = "userName";
            this.userName.Visible = true;
            this.userName.VisibleIndex = 1;
            // 
            // teachersBindingSource
            // 
            this.teachersBindingSource.DataSource = typeof(UserEditTreeRecord);
            // 
            // pageUsers1
            // 
            this.pageUsers1.Controls.Add(this.label1);
            this.pageUsers1.Controls.Add(this.label2);
            this.pageUsers1.Controls.Add(this.spinEdit2);
            this.pageUsers1.Controls.Add(this.spinEdit1);
            this.pageUsers1.Description = "Hier legen sie fest, welche Benutzer (Lehrer und Schüler) am System angemeldet we" +
    "rden können.";
            this.pageUsers1.Location = new System.Drawing.Point(0, 0);
            this.pageUsers1.Name = "pageUsers1";
            this.pageUsers1.Size = new System.Drawing.Size(428, 224);
            this.pageUsers1.TabIndex = 14;
            this.pageUsers1.Title = "1. Benutzer einrichten";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Anzahl der Lehrer:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Anzahl der Schüler:";
            // 
            // spinEdit2
            // 
            this.spinEdit2.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinEdit2.Location = new System.Drawing.Point(187, 148);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit2.Properties.Mask.EditMask = "d";
            this.spinEdit2.Properties.MaxValue = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.spinEdit2.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit2.Size = new System.Drawing.Size(100, 20);
            this.spinEdit2.TabIndex = 2;
            this.spinEdit2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.spinEdit2.ToolTipTitle = "Anzahl der Schüler";
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(187, 103);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Properties.Mask.EditMask = "d";
            this.spinEdit1.Properties.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinEdit1.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit1.Size = new System.Drawing.Size(100, 20);
            this.spinEdit1.TabIndex = 0;
            // 
            // timerTask
            // 
            this.timerTask.Tick += new System.EventHandler(this.timerTask_Tick);
            // 
            // FrmSetupWizard
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(505, 400);
            this.Controls.Add(this.wizardSample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Dark Side";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetupWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Einrichtungsassistent";
            this.pageMaps.ResumeLayout(false);
            this.grpMapMode.ResumeLayout(false);
            this.wizardSample.ResumeLayout(false);
            this.pageProgress.ResumeLayout(false);
            this.pageClasses.ResumeLayout(false);
            this.grpClassMode.ResumeLayout(false);
            this.pageUsers3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schoolersBindingSource)).EndInit();
            this.pageUsers2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teachersBindingSource)).EndInit();
            this.pageUsers1.ResumeLayout(false);
            this.pageUsers1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private WizardPage pageMaps;
        private WizardPage pageFinish;
        private WizardPage pageWelcome;
        private WizardPage pageProgress;
        private Wizard wizardSample;
        private System.Windows.Forms.ProgressBar progressLongTask;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Timer timerTask;
        private System.Windows.Forms.GroupBox grpMapMode;
        private System.Windows.Forms.RadioButton rbnBeginner;
        private System.Windows.Forms.RadioButton rbnBasicKnowledge;
        private WizardPage pageClasses;
        private WizardPage pageUsers1;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SpinEdit spinEdit2;
        private WizardPage pageUsers2;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn fullName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn userName;
        private System.Windows.Forms.BindingSource teachersBindingSource;
        private WizardPage pageUsers3;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private System.Windows.Forms.BindingSource schoolersBindingSource;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbnUnknownKnowledge;
        private System.Windows.Forms.RadioButton rbnExpertKnowledge;
        private System.Windows.Forms.GroupBox grpClassMode;
        private System.Windows.Forms.RadioButton rbnNoClasses;
        private System.Windows.Forms.RadioButton rbnMapFixedClass;
        private System.Windows.Forms.RadioButton rbnUserFixedClass;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox4;

    }
}
