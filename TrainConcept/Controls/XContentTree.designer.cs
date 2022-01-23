namespace SoftObject.TrainConcept.Controls
{
    public partial class XContentTree
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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.xtraTreeListBlending1 = new DevExpress.XtraTreeList.Blending.XtraTreeListBlending();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xtraTreeListBlending1
            // 
            this.xtraTreeListBlending1.TreeListControl = this;
            // 
            // XContentTree
            // 
            this.Appearance.EvenRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.EvenRow.Options.UseFont = true;
            this.Appearance.FocusedCell.BackColor = System.Drawing.Color.Green;
            this.Appearance.FocusedCell.Options.UseBackColor = true;
            this.Appearance.GroupButton.BackColor = System.Drawing.SystemColors.Window;
            this.Appearance.GroupButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Appearance.GroupButton.Options.UseBackColor = true;
            this.Appearance.GroupButton.Options.UseForeColor = true;
            this.Appearance.OddRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.OddRow.Options.UseFont = true;
            this.Appearance.VertLine.Options.UseTextOptions = true;
            this.Appearance.VertLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.BestFitVisibleOnly = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptionsBehavior.AutoChangeParent = false;
            this.OptionsBehavior.AutoSelectAllInEditor = false;
            this.OptionsBehavior.CloseEditorOnLostFocus = false;
            this.OptionsBehavior.KeepSelectedOnClick = false;
            this.OptionsBehavior.ShowToolTips = false;
            this.OptionsBehavior.SmartMouseHover = false;
            this.OptionsDragAndDrop.ExpandNodeOnDrag = false;
            this.OptionsMenu.EnableColumnMenu = false;
            this.OptionsMenu.EnableFooterMenu = false;
            this.OptionsNavigation.MoveOnEdit = false;
            this.OptionsPrint.AutoRowHeight = false;
            this.OptionsPrint.AutoWidth = false;
            this.OptionsPrint.PrintHorzLines = false;
            this.OptionsPrint.PrintImages = false;
            this.OptionsPrint.PrintPageHeader = false;
            this.OptionsPrint.PrintReportFooter = false;
            this.OptionsPrint.PrintTree = false;
            this.OptionsPrint.PrintTreeButtons = false;
            this.OptionsPrint.PrintVertLines = false;
            this.OptionsView.ShowColumns = false;
            this.OptionsView.ShowIndentAsRowStyle = true;
            this.OptionsView.ShowIndicator = false;
            this.SelectImageList = this.imageList1;
            this.StateImageList = this.imageList2;
            this.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.ContentTreeView_GetStateImage);
            this.GetSelectImage += new DevExpress.XtraTreeList.GetSelectImageEventHandler(this.ContentTreeView_GetSelectImage);
            this.StateImageClick += new DevExpress.XtraTreeList.NodeClickEventHandler(this.ContentTreeView_StateImageClick);
            this.CalcNodeDragImageIndex += new DevExpress.XtraTreeList.CalcNodeDragImageIndexEventHandler(this.XContentTree_CalcNodeDragImageIndex);
            this.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.XContentTree_CustomDrawNodeCell);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.XContentTree_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.XContentTree_DragOver);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContentTreeView_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContentTreeView_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ContentTreeView_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraTreeList.Blending.XtraTreeListBlending xtraTreeListBlending1;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}
