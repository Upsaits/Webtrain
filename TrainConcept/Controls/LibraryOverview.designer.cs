using System.ComponentModel;
using System.Windows.Forms;

namespace SoftObject.TrainConcept.Controls
{
    /// <summary>
    /// Zusammendfassende Beschreibung für LibraryOverview.
    /// </summary>
    public partial class LibraryOverview
    {

        /// <summary> 
        /// Die verwendeten Ressourcen bereinigen.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        #region Component Designer generated code
        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnForward = new DevExpress.XtraEditors.SimpleButton();
            this.lBtnImages = new System.Windows.Forms.ImageList(this.components);
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrevPage = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextPage = new DevExpress.XtraEditors.SimpleButton();
            this.contentTreeView1 = new XContentTree();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contentTreeView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnForward);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnPrevPage);
            this.panel1.Controls.Add(this.btnNextPage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 338);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 45);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // btnForward
            // 
            this.btnForward.ImageIndex = 1;
            this.btnForward.ImageList = this.lBtnImages;
            this.btnForward.Location = new System.Drawing.Point(74, 3);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(41, 39);
            this.btnForward.TabIndex = 12;
            this.btnForward.Text = "fwd";
            // 
            // lBtnImages
            // 
            this.lBtnImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.lBtnImages.ImageSize = new System.Drawing.Size(32, 32);
            this.lBtnImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnBack
            // 
            this.btnBack.ImageIndex = 0;
            this.btnBack.ImageList = this.lBtnImages;
            this.btnBack.Location = new System.Drawing.Point(27, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(41, 39);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "back";
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Location = new System.Drawing.Point(3, 24);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(18, 18);
            this.btnPrevPage.TabIndex = 11;
            this.btnPrevPage.Text = "<";
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(3, 3);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(18, 18);
            this.btnNextPage.TabIndex = 10;
            this.btnNextPage.Text = ">";
            // 
            // contentTreeView1
            // 
            this.contentTreeView1.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.contentTreeView1.Appearance.Empty.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.contentTreeView1.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite;
            this.contentTreeView1.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 10F);
            this.contentTreeView1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.contentTreeView1.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.contentTreeView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.EvenRow.Options.UseFont = true;
            this.contentTreeView1.Appearance.EvenRow.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.contentTreeView1.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.contentTreeView1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.contentTreeView1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.FocusedCell.Options.UseFont = true;
            this.contentTreeView1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy;
            this.contentTreeView1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(178)))));
            this.contentTreeView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 10F);
            this.contentTreeView1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.contentTreeView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.FocusedRow.Options.UseFont = true;
            this.contentTreeView1.Appearance.FocusedRow.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver;
            this.contentTreeView1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver;
            this.contentTreeView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.contentTreeView1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.contentTreeView1.Appearance.FooterPanel.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.contentTreeView1.Appearance.FooterPanel.Options.UseFont = true;
            this.contentTreeView1.Appearance.FooterPanel.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver;
            this.contentTreeView1.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver;
            this.contentTreeView1.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.contentTreeView1.Appearance.GroupButton.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.GroupButton.Options.UseBorderColor = true;
            this.contentTreeView1.Appearance.GroupButton.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.contentTreeView1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.contentTreeView1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.contentTreeView1.Appearance.GroupFooter.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.contentTreeView1.Appearance.GroupFooter.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver;
            this.contentTreeView1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver;
            this.contentTreeView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.contentTreeView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.contentTreeView1.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.contentTreeView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.contentTreeView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray;
            this.contentTreeView1.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 10F);
            this.contentTreeView1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.contentTreeView1.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.HideSelectionRow.Options.UseFont = true;
            this.contentTreeView1.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver;
            this.contentTreeView1.Appearance.HorzLine.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.contentTreeView1.Appearance.OddRow.BackColor2 = System.Drawing.Color.White;
            this.contentTreeView1.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 10F);
            this.contentTreeView1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.contentTreeView1.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.contentTreeView1.Appearance.OddRow.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.OddRow.Options.UseFont = true;
            this.contentTreeView1.Appearance.OddRow.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.Preview.BackColor = System.Drawing.Color.White;
            this.contentTreeView1.Appearance.Preview.Font = new System.Drawing.Font("Tahoma", 10F);
            this.contentTreeView1.Appearance.Preview.ForeColor = System.Drawing.Color.Navy;
            this.contentTreeView1.Appearance.Preview.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.Preview.Options.UseFont = true;
            this.contentTreeView1.Appearance.Preview.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.contentTreeView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.contentTreeView1.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.contentTreeView1.Appearance.Row.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.Row.Options.UseFont = true;
            this.contentTreeView1.Appearance.Row.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(138)))));
            this.contentTreeView1.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 10F);
            this.contentTreeView1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.contentTreeView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.SelectedRow.Options.UseFont = true;
            this.contentTreeView1.Appearance.SelectedRow.Options.UseForeColor = true;
            this.contentTreeView1.Appearance.TreeLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.contentTreeView1.Appearance.TreeLine.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.VertLine.BackColor = System.Drawing.Color.Silver;
            this.contentTreeView1.Appearance.VertLine.Options.UseBackColor = true;
            this.contentTreeView1.Appearance.VertLine.Options.UseTextOptions = true;
            this.contentTreeView1.Appearance.VertLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.contentTreeView1.BestFitVisibleOnly = true;
            this.contentTreeView1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.contentTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentTreeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contentTreeView1.LearnmapName = null;
            this.contentTreeView1.Location = new System.Drawing.Point(0, 0);
            this.contentTreeView1.Name = "contentTreeView1";
            this.contentTreeView1.OptionsBehavior.AutoChangeParent = false;
            this.contentTreeView1.OptionsBehavior.AutoSelectAllInEditor = false;
            this.contentTreeView1.OptionsBehavior.CloseEditorOnLostFocus = false;
            this.contentTreeView1.OptionsBehavior.Editable = false;
            this.contentTreeView1.OptionsBehavior.KeepSelectedOnClick = false;
            this.contentTreeView1.OptionsBehavior.ShowToolTips = false;
            this.contentTreeView1.OptionsBehavior.SmartMouseHover = false;
            this.contentTreeView1.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Multiple;
            this.contentTreeView1.OptionsDragAndDrop.ExpandNodeOnDrag = false;
            this.contentTreeView1.OptionsMenu.EnableColumnMenu = false;
            this.contentTreeView1.OptionsMenu.EnableFooterMenu = false;
            this.contentTreeView1.OptionsNavigation.MoveOnEdit = false;
            this.contentTreeView1.OptionsPrint.AutoRowHeight = false;
            this.contentTreeView1.OptionsPrint.AutoWidth = false;
            this.contentTreeView1.OptionsPrint.PrintHorzLines = false;
            this.contentTreeView1.OptionsPrint.PrintImages = false;
            this.contentTreeView1.OptionsPrint.PrintPageHeader = false;
            this.contentTreeView1.OptionsPrint.PrintReportFooter = false;
            this.contentTreeView1.OptionsPrint.PrintTree = false;
            this.contentTreeView1.OptionsPrint.PrintTreeButtons = false;
            this.contentTreeView1.OptionsPrint.PrintVertLines = false;
            this.contentTreeView1.OptionsView.EnableAppearanceEvenRow = true;
            this.contentTreeView1.OptionsView.EnableAppearanceOddRow = true;
            this.contentTreeView1.OptionsView.ShowColumns = false;
            this.contentTreeView1.OptionsView.ShowIndentAsRowStyle = true;
            this.contentTreeView1.OptionsView.ShowIndicator = false;
            this.contentTreeView1.Size = new System.Drawing.Size(175, 338);
            this.contentTreeView1.TabIndex = 0;
            this.contentTreeView1.UseType = SoftObject.TrainConcept.Libraries.ContentTreeViewList.UseType.FullContent;
            this.contentTreeView1.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.contentTreeView1_BeforeFocusNode);
            this.contentTreeView1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.contentTreeView1_FocusedNodeChanged);
            this.contentTreeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.contentTreeView1_DragEnter);
            // 
            // LibraryOverview
            // 
            this.Controls.Add(this.contentTreeView1);
            this.Controls.Add(this.panel1);
            this.Name = "LibraryOverview";
            this.Size = new System.Drawing.Size(175, 383);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contentTreeView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnNextPage;
        private DevExpress.XtraEditors.SimpleButton btnPrevPage;
        private DevExpress.XtraEditors.SimpleButton btnForward;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private IContainer components;
        private ImageList lBtnImages;
        private XContentTree contentTreeView1;
    }
}