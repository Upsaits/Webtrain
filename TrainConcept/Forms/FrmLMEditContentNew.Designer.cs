using SoftObject.TrainConcept.Controls;
using SoftObject.SOComponents.Controls;

namespace SoftObject.TrainConcept.Forms
{
    partial class FrmLMEditContentNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLMEditContentNew));
            this.panButtonBar = new System.Windows.Forms.Panel();
            this.chkShowProgressQuestionOrientated = new System.Windows.Forms.CheckBox();
            this.btnWorkings = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.rbgMapUsage = new DevExpress.XtraEditors.RadioGroup();
            this.panTestTabPage = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvwQuestions = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panTestAdjustment = new TestAdjustmentPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panWorkingBar = new System.Windows.Forms.Panel();
            this.lvwWorkings = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.panWorkingButtons = new System.Windows.Forms.Panel();
            this.btnInsertTest = new DevComponents.DotNetBar.ButtonX();
            this.btnMoveDown = new DevComponents.DotNetBar.ButtonX();
            this.btnMoveUp = new DevComponents.DotNetBar.ButtonX();
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.tooltipDisplayDelay = new System.Windows.Forms.Timer(this.components);
            this.superTooltip2 = new DevComponents.DotNetBar.SuperTooltip();
            this.axWebBrowser1 = new WebBrowserEx();
            this.tabCtrlTests = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panTestBar = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panTestButtons = new System.Windows.Forms.Panel();
            this.btnDeleteTest = new DevComponents.DotNetBar.ButtonX();
            this.btnNewTest = new DevComponents.DotNetBar.ButtonX();
            this.panButtonBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbgMapUsage.Properties)).BeginInit();
            this.panTestTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panWorkingBar.SuspendLayout();
            this.panWorkingButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabCtrlTests)).BeginInit();
            this.tabCtrlTests.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.panTestBar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panTestButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panButtonBar
            // 
            this.panButtonBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panButtonBar.Controls.Add(this.chkShowProgressQuestionOrientated);
            this.panButtonBar.Controls.Add(this.btnWorkings);
            this.panButtonBar.Controls.Add(this.btnTest);
            this.panButtonBar.Controls.Add(this.rbgMapUsage);
            this.panButtonBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panButtonBar.Location = new System.Drawing.Point(0, 520);
            this.panButtonBar.Name = "panButtonBar";
            this.panButtonBar.Size = new System.Drawing.Size(884, 48);
            this.panButtonBar.TabIndex = 4;
            // 
            // chkShowProgressQuestionOrientated
            // 
            this.chkShowProgressQuestionOrientated.AutoSize = true;
            this.chkShowProgressQuestionOrientated.Location = new System.Drawing.Point(350, 17);
            this.chkShowProgressQuestionOrientated.Name = "chkShowProgressQuestionOrientated";
            this.chkShowProgressQuestionOrientated.Size = new System.Drawing.Size(186, 17);
            this.chkShowProgressQuestionOrientated.TabIndex = 8;
            this.chkShowProgressQuestionOrientated.Text = "Zeige Fortschritt Fragenorientiert";
            this.chkShowProgressQuestionOrientated.UseVisualStyleBackColor = true;
            this.chkShowProgressQuestionOrientated.CheckedChanged += new System.EventHandler(this.chkShowProgressQuestionOrientated_CheckedChanged);
            // 
            // btnWorkings
            // 
            this.btnWorkings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWorkings.Location = new System.Drawing.Point(8, 8);
            this.btnWorkings.Name = "btnWorkings";
            this.btnWorkings.Size = new System.Drawing.Size(88, 32);
            this.btnWorkings.TabIndex = 2;
            this.btnWorkings.Text = "Inhalte";
            this.btnWorkings.Visible = false;
            this.btnWorkings.Click += new System.EventHandler(this.btnWorkings_Click);
            // 
            // btnTest
            // 
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.Location = new System.Drawing.Point(8, 8);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(88, 32);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Tests";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // rbgMapUsage
            // 
            this.rbgMapUsage.EditValue = 0;
            this.rbgMapUsage.Location = new System.Drawing.Point(117, 8);
            this.rbgMapUsage.Name = "rbgMapUsage";
            this.rbgMapUsage.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.rbgMapUsage.Properties.Appearance.Options.UseBackColor = true;
            this.rbgMapUsage.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Usermappe"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Klassenmappe")});
            this.rbgMapUsage.Size = new System.Drawing.Size(203, 32);
            this.rbgMapUsage.TabIndex = 6;
            this.rbgMapUsage.ToolTip = "Hier legen sie fest, wie die  Mappe benutzt wird:\r\n\r\nUsermappe: Sie wird direkt a" +
    "n Benutzer verteilt\r\nKlassenmappe: Sie wird nur an Klassen verteilt";
            this.rbgMapUsage.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.rbgMapUsage.ToolTipTitle = "Mappen-Benutzung";
            this.rbgMapUsage.EditValueChanged += new System.EventHandler(this.rbnMapUsage_EditValueChanged);
            this.rbgMapUsage.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rbgMapUsage_EditValueChanging);
            // 
            // panTestTabPage
            // 
            this.panTestTabPage.Controls.Add(this.panel1);
            this.panTestTabPage.Controls.Add(this.splitter2);
            this.panTestTabPage.Controls.Add(this.panTestAdjustment);
            this.panTestTabPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panTestTabPage.Location = new System.Drawing.Point(0, 0);
            this.panTestTabPage.Name = "panTestTabPage";
            this.panTestTabPage.Size = new System.Drawing.Size(826, 88);
            this.panTestTabPage.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvwQuestions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 88);
            this.panel1.TabIndex = 10;
            // 
            // lvwQuestions
            // 
            this.lvwQuestions.AllowDrop = true;
            // 
            // 
            // 
            this.lvwQuestions.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lvwQuestions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwQuestions.Enabled = false;
            this.lvwQuestions.Location = new System.Drawing.Point(0, 0);
            this.lvwQuestions.Name = "lvwQuestions";
            this.lvwQuestions.Size = new System.Drawing.Size(620, 88);
            this.lvwQuestions.TabIndex = 2;
            this.lvwQuestions.UseCompatibleStateImageBehavior = false;
            this.lvwQuestions.View = System.Windows.Forms.View.Details;
            this.lvwQuestions.SelectedIndexChanged += new System.EventHandler(this.lvwQuestions_SelectedIndexChanged);
            this.lvwQuestions.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwQuestions_DragDrop);
            this.lvwQuestions.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwQuestions_DragEnter);
            this.lvwQuestions.DoubleClick += new System.EventHandler(this.lvwQuestions_DoubleClick);
            this.lvwQuestions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwQuestions_KeyDown);
            this.lvwQuestions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwQuestions_MouseDown);
            this.lvwQuestions.MouseLeave += new System.EventHandler(this.lvwQuestions_MouseLeave);
            this.lvwQuestions.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvwQuestions_MouseMove);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(620, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(6, 88);
            this.splitter2.TabIndex = 11;
            this.splitter2.TabStop = false;
            // 
            // panTestAdjustment
            // 
            this.panTestAdjustment.BackColor = System.Drawing.Color.Transparent;
            this.panTestAdjustment.Dock = System.Windows.Forms.DockStyle.Right;
            this.panTestAdjustment.Location = new System.Drawing.Point(626, 0);
            this.panTestAdjustment.Name = "panTestAdjustment";
            this.panTestAdjustment.QuestionChooseChecked = false;
            this.panTestAdjustment.QuestionSelectChecked = false;
            this.panTestAdjustment.Size = new System.Drawing.Size(200, 88);
            this.panTestAdjustment.TabIndex = 12;
            this.panTestAdjustment.TestAlwaysAllowed = false;
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 250);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(884, 6);
            this.splitter1.TabIndex = 11;
            this.splitter1.TabStop = false;
            // 
            // panWorkingBar
            // 
            this.panWorkingBar.Controls.Add(this.lvwWorkings);
            this.panWorkingBar.Controls.Add(this.panWorkingButtons);
            this.panWorkingBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panWorkingBar.Location = new System.Drawing.Point(0, 256);
            this.panWorkingBar.Name = "panWorkingBar";
            this.panWorkingBar.Size = new System.Drawing.Size(884, 264);
            this.panWorkingBar.TabIndex = 12;
            // 
            // lvwWorkings
            // 
            this.lvwWorkings.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.lvwWorkings.AllowDrop = true;
            // 
            // 
            // 
            this.lvwWorkings.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lvwWorkings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwWorkings.FullRowSelect = true;
            this.lvwWorkings.GridLines = true;
            this.lvwWorkings.Location = new System.Drawing.Point(52, 0);
            this.lvwWorkings.Name = "lvwWorkings";
            this.lvwWorkings.Size = new System.Drawing.Size(832, 264);
            this.lvwWorkings.TabIndex = 6;
            this.lvwWorkings.UseCompatibleStateImageBehavior = false;
            this.lvwWorkings.View = System.Windows.Forms.View.Details;
            this.lvwWorkings.SelectedIndexChanged += new System.EventHandler(this.lvwWorkings_SelectedIndexChanged);
            this.lvwWorkings.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwWorkings_DragDrop);
            this.lvwWorkings.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwWorkings_DragEnter);
            this.lvwWorkings.DoubleClick += new System.EventHandler(this.lvwWorkings_DoubleClick);
            this.lvwWorkings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwWorkings_KeyDown);
            this.lvwWorkings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwWorkings_MouseDown);
            this.lvwWorkings.MouseLeave += new System.EventHandler(this.lvwWorkings_MouseLeave);
            this.lvwWorkings.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvwWorkings_MouseMove);
            // 
            // panWorkingButtons
            // 
            this.panWorkingButtons.Controls.Add(this.btnInsertTest);
            this.panWorkingButtons.Controls.Add(this.btnMoveDown);
            this.panWorkingButtons.Controls.Add(this.btnMoveUp);
            this.panWorkingButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.panWorkingButtons.Location = new System.Drawing.Point(0, 0);
            this.panWorkingButtons.Name = "panWorkingButtons";
            this.panWorkingButtons.Size = new System.Drawing.Size(52, 264);
            this.panWorkingButtons.TabIndex = 9;
            // 
            // btnInsertTest
            // 
            this.btnInsertTest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInsertTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnInsertTest.Enabled = false;
            this.btnInsertTest.Location = new System.Drawing.Point(3, 85);
            this.btnInsertTest.Name = "btnInsertTest";
            this.btnInsertTest.Size = new System.Drawing.Size(46, 32);
            this.btnInsertTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnInsertTest.TabIndex = 9;
            this.btnInsertTest.Text = "Test einfügen";
            this.btnInsertTest.Click += new System.EventHandler(this.btnInsertTest_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMoveDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.Location = new System.Drawing.Point(3, 31);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(46, 30);
            this.btnMoveDown.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMoveDown.TabIndex = 8;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMoveUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.Location = new System.Drawing.Point(3, 0);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(46, 30);
            this.btnMoveUp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMoveUp.TabIndex = 7;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // superTooltip1
            // 
            this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // tooltipDisplayDelay
            // 
            this.tooltipDisplayDelay.Interval = 1000;
            this.tooltipDisplayDelay.Tick += new System.EventHandler(this.tooltipDisplayDelay_Tick);
            // 
            // superTooltip2
            // 
            this.superTooltip2.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // axWebBrowser1
            // 
            this.axWebBrowser1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.axWebBrowser1.Name = "axWebBrowser1";
            this.axWebBrowser1.Size = new System.Drawing.Size(884, 250);
            this.axWebBrowser1.TabIndex = 10;
            // 
            // tabCtrlTests
            // 
            this.tabCtrlTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlTests.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlTests.Name = "tabCtrlTests";
            this.tabCtrlTests.SelectedTabPage = this.xtraTabPage1;
            this.tabCtrlTests.Size = new System.Drawing.Size(832, 116);
            this.tabCtrlTests.TabIndex = 13;
            this.tabCtrlTests.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            this.tabCtrlTests.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabCtrlTests1_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.panTestTabPage);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(826, 88);
            this.xtraTabPage1.Text = "Endtest";
            // 
            // panTestBar
            // 
            this.panTestBar.Controls.Add(this.panel3);
            this.panTestBar.Controls.Add(this.panTestButtons);
            this.panTestBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panTestBar.Location = new System.Drawing.Point(0, 404);
            this.panTestBar.Name = "panTestBar";
            this.panTestBar.Size = new System.Drawing.Size(884, 116);
            this.panTestBar.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabCtrlTests);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(52, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(832, 116);
            this.panel3.TabIndex = 15;
            // 
            // panTestButtons
            // 
            this.panTestButtons.Controls.Add(this.btnDeleteTest);
            this.panTestButtons.Controls.Add(this.btnNewTest);
            this.panTestButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.panTestButtons.Location = new System.Drawing.Point(0, 0);
            this.panTestButtons.Name = "panTestButtons";
            this.panTestButtons.Size = new System.Drawing.Size(52, 116);
            this.panTestButtons.TabIndex = 14;
            // 
            // btnDeleteTest
            // 
            this.btnDeleteTest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeleteTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDeleteTest.Enabled = false;
            this.btnDeleteTest.Location = new System.Drawing.Point(4, 34);
            this.btnDeleteTest.Name = "btnDeleteTest";
            this.btnDeleteTest.Size = new System.Drawing.Size(45, 23);
            this.btnDeleteTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDeleteTest.TabIndex = 1;
            this.btnDeleteTest.Text = "Löschen";
            this.btnDeleteTest.Click += new System.EventHandler(this.btnDeleteTest_Click);
            // 
            // btnNewTest
            // 
            this.btnNewTest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNewTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNewTest.Location = new System.Drawing.Point(4, 4);
            this.btnNewTest.Name = "btnNewTest";
            this.btnNewTest.Size = new System.Drawing.Size(45, 23);
            this.btnNewTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNewTest.TabIndex = 0;
            this.btnNewTest.Text = "Neu";
            this.btnNewTest.Click += new System.EventHandler(this.btnNewTest_Click);
            // 
            // FrmLMEditContentNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 568);
            this.Controls.Add(this.panTestBar);
            this.Controls.Add(this.panWorkingBar);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.axWebBrowser1);
            this.Controls.Add(this.panButtonBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Dark Side";
            this.Name = "FrmLMEditContentNew";
            this.Text = "FrmLMEditContentNew";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLMEditContentNew_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLMEditContentNew_FormClosed);
            this.Load += new System.EventHandler(this.FrmLMEditContentNew_Load);
            this.panButtonBar.ResumeLayout(false);
            this.panButtonBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbgMapUsage.Properties)).EndInit();
            this.panTestTabPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panWorkingBar.ResumeLayout(false);
            this.panWorkingButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabCtrlTests)).EndInit();
            this.tabCtrlTests.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.panTestBar.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panTestButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panButtonBar;
        private System.Windows.Forms.Button btnWorkings;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Panel panTestTabPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter2;
        private TestAdjustmentPanel panTestAdjustment;
        private WebBrowserEx axWebBrowser1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panWorkingBar;
        private DevExpress.XtraEditors.RadioGroup rbgMapUsage;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private System.Windows.Forms.Timer tooltipDisplayDelay;
        private DevComponents.DotNetBar.Controls.ListViewEx lvwQuestions;
        private DevComponents.DotNetBar.Controls.ListViewEx lvwWorkings;
        private DevComponents.DotNetBar.ButtonX btnMoveDown;
        private DevComponents.DotNetBar.ButtonX btnMoveUp;
        private System.Windows.Forms.Panel panWorkingButtons;
        private DevComponents.DotNetBar.SuperTooltip superTooltip2;
        private DevExpress.XtraTab.XtraTabControl tabCtrlTests;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.Panel panTestBar;
        private System.Windows.Forms.Panel panTestButtons;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.ButtonX btnInsertTest;
        private DevComponents.DotNetBar.ButtonX btnNewTest;
        private DevComponents.DotNetBar.ButtonX btnDeleteTest;
        private System.Windows.Forms.CheckBox chkShowProgressQuestionOrientated;
    }
}