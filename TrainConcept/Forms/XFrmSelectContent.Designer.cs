using SoftObject.TrainConcept.Controls;

namespace SoftObject.TrainConcept.Forms
{
    partial class XFrmSelectContent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFrmSelectContent));
            this.xContentTree1 = new XContentTree();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xContentTree1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xContentTree1
            // 
            this.xContentTree1.Appearance.EvenRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xContentTree1.Appearance.EvenRow.Options.UseFont = true;
            this.xContentTree1.Appearance.FocusedCell.BackColor = System.Drawing.Color.Green;
            this.xContentTree1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.xContentTree1.Appearance.GroupButton.BackColor = System.Drawing.SystemColors.Window;
            this.xContentTree1.Appearance.GroupButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.xContentTree1.Appearance.GroupButton.Options.UseBackColor = true;
            this.xContentTree1.Appearance.GroupButton.Options.UseForeColor = true;
            this.xContentTree1.Appearance.OddRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xContentTree1.Appearance.OddRow.Options.UseFont = true;
            this.xContentTree1.Appearance.VertLine.Options.UseTextOptions = true;
            this.xContentTree1.Appearance.VertLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xContentTree1.BestFitVisibleOnly = true;
            this.xContentTree1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.xContentTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xContentTree1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xContentTree1.LearnmapName = "";
            this.xContentTree1.Location = new System.Drawing.Point(0, 0);
            this.xContentTree1.LookAndFeel.SkinName = "Dark Side";
            this.xContentTree1.Name = "xContentTree1";
            this.xContentTree1.OptionsBehavior.AutoChangeParent = false;
            this.xContentTree1.OptionsBehavior.AutoSelectAllInEditor = false;
            this.xContentTree1.OptionsBehavior.CloseEditorOnLostFocus = false;
            this.xContentTree1.OptionsBehavior.Editable = false;
            this.xContentTree1.OptionsBehavior.KeepSelectedOnClick = false;
            this.xContentTree1.OptionsBehavior.ShowToolTips = false;
            this.xContentTree1.OptionsBehavior.SmartMouseHover = false;
            this.xContentTree1.OptionsDragAndDrop.ExpandNodeOnDrag = false;
            this.xContentTree1.OptionsMenu.EnableColumnMenu = false;
            this.xContentTree1.OptionsMenu.EnableFooterMenu = false;
            this.xContentTree1.OptionsNavigation.MoveOnEdit = false;
            this.xContentTree1.OptionsPrint.AutoRowHeight = false;
            this.xContentTree1.OptionsPrint.AutoWidth = false;
            this.xContentTree1.OptionsPrint.PrintHorzLines = false;
            this.xContentTree1.OptionsPrint.PrintImages = false;
            this.xContentTree1.OptionsPrint.PrintPageHeader = false;
            this.xContentTree1.OptionsPrint.PrintReportFooter = false;
            this.xContentTree1.OptionsPrint.PrintTree = false;
            this.xContentTree1.OptionsPrint.PrintTreeButtons = false;
            this.xContentTree1.OptionsPrint.PrintVertLines = false;
            this.xContentTree1.OptionsView.ShowColumns = false;
            this.xContentTree1.OptionsView.ShowIndentAsRowStyle = true;
            this.xContentTree1.OptionsView.ShowIndicator = false;
            this.xContentTree1.Size = new System.Drawing.Size(335, 276);
            this.xContentTree1.TabIndex = 0;
            this.xContentTree1.UseType = SoftObject.TrainConcept.Libraries.ContentTreeViewList.UseType.FullContent;
            this.xContentTree1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.xContentTree1_FocusedNodeChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 231);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 45);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(173, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Abbrechen";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(62, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // XFrmSelectContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 276);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xContentTree1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XFrmSelectContent";
            this.Text = "Zielpunkt wählen";
            this.Load += new System.EventHandler(this.XFrmSelectContent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xContentTree1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private XContentTree xContentTree1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnOK;
    }
}