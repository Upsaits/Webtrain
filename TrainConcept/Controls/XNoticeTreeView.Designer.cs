namespace SoftObject.TrainConcept.Controls
{
    partial class XNoticeTreeView
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // XNoticeTreeView
            // 
            this.OptionsBehavior.AllowExpandOnDblClick = false;
            this.OptionsBehavior.AutoChangeParent = false;
            this.OptionsBehavior.AutoSelectAllInEditor = false;
            this.OptionsBehavior.CloseEditorOnLostFocus = false;
            this.OptionsBehavior.Editable = false;
            this.OptionsDragAndDrop.ExpandNodeOnDrag = false;
            this.OptionsBehavior.KeepSelectedOnClick = false;
            this.OptionsNavigation.MoveOnEdit = false;
            this.OptionsBehavior.ShowToolTips = false;
            this.OptionsBehavior.SmartMouseHover = false;
            this.OptionsMenu.EnableFooterMenu = false;
            this.OptionsPrint.AutoRowHeight = false;
            this.OptionsPrint.AutoWidth = false;
            this.OptionsPrint.PrintHorzLines = false;
            this.OptionsPrint.PrintImages = false;
            this.OptionsPrint.PrintPageHeader = false;
            this.OptionsPrint.PrintReportFooter = false;
            this.OptionsPrint.PrintTree = false;
            this.OptionsPrint.PrintTreeButtons = false;
            this.OptionsPrint.PrintVertLines = false;
            this.OptionsSelection.MultiSelect = true;
            this.Size = new System.Drawing.Size(496, 384);
            this.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.XNoticeTreeView_CustomDrawNodeCell);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
