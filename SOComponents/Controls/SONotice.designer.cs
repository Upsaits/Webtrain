using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SoftObject.SOComponents.UtilityLibrary;

namespace SoftObject.SOComponents.Controls
{
    /// <summary>
    /// Zusammendfassende Beschreibung für SONotice.
    /// </summary>
    public partial class SONotice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SONotice));
            this.textControl1 = new TXTextControl.TextControl();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.PopupItemCut = new DevExpress.XtraBars.BarButtonItem();
            this.PopupItemCopy = new DevExpress.XtraBars.BarButtonItem();
            this.PopupItemPaste = new DevExpress.XtraBars.BarButtonItem();
            this.PopupItemMarkAll = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageListMenu = new System.Windows.Forms.ImageList(this.components);
            this.panelTopButtons = new System.Windows.Forms.Panel();
            this.btnWorkoutState = new DevComponents.DotNetBar.ButtonX();
            this.btnNotWorkedOut = new DevComponents.DotNetBar.ButtonItem();
            this.btnCorrectWorkedOut = new DevComponents.DotNetBar.ButtonItem();
            this.btnIncorrectWorkedOut = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.panelTopTitle = new System.Windows.Forms.Panel();
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.btnSolution = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.lblTitle = new DevComponents.DotNetBar.LabelX();
            this.ratingStarCorrect = new DevComponents.DotNetBar.Controls.RatingStar();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblTextWrong = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.panelTopButtons.SuspendLayout();
            this.panelTopTitle.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // textControl1
            // 
            this.textControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textControl1.Font = new System.Drawing.Font("Arial", 10F);
            this.textControl1.Location = new System.Drawing.Point(0, 0);
            this.textControl1.Name = "textControl1";
            this.barManager1.SetPopupContextMenu(this.textControl1, this.popupMenu1);
            this.textControl1.Size = new System.Drawing.Size(548, 478);
            this.textControl1.TabIndex = 0;
            this.textControl1.TextChanged += new System.EventHandler(this.textControl1_TextChanged);
            this.textControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textControl1_KeyDown);
            this.textControl1.Leave += new System.EventHandler(this.textControl1_Leave);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.PopupItemCut),
            new DevExpress.XtraBars.LinkPersistInfo(this.PopupItemCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.PopupItemPaste),
            new DevExpress.XtraBars.LinkPersistInfo(this.PopupItemMarkAll)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.Popup += new System.EventHandler(this.popupMenu1_Popup);
            // 
            // PopupItemCut
            // 
            this.PopupItemCut.Caption = "PopupItemCut";
            this.PopupItemCut.Id = 4;
            this.PopupItemCut.ImageOptions.ImageIndex = 0;
            this.PopupItemCut.Name = "PopupItemCut";
            this.PopupItemCut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PopupItemCut_ItemClick);
            // 
            // PopupItemCopy
            // 
            this.PopupItemCopy.Caption = "PopupItemCopy";
            this.PopupItemCopy.Id = 5;
            this.PopupItemCopy.ImageOptions.ImageIndex = 1;
            this.PopupItemCopy.Name = "PopupItemCopy";
            this.PopupItemCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PopupItemCopy_ItemClick);
            // 
            // PopupItemPaste
            // 
            this.PopupItemPaste.Caption = "PopupItemPaste";
            this.PopupItemPaste.Id = 6;
            this.PopupItemPaste.ImageOptions.ImageIndex = 2;
            this.PopupItemPaste.Name = "PopupItemPaste";
            this.PopupItemPaste.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PopupItemPaste_ItemClick);
            // 
            // PopupItemMarkAll
            // 
            this.PopupItemMarkAll.Caption = "PopupItemMarkAll";
            this.PopupItemMarkAll.Id = 7;
            this.PopupItemMarkAll.Name = "PopupItemMarkAll";
            this.PopupItemMarkAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PopupItemMarkAll_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageListMenu;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.PopupItemCut,
            this.PopupItemCopy,
            this.PopupItemPaste,
            this.PopupItemMarkAll});
            this.barManager1.MaxItemId = 8;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(548, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 499);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(548, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 499);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(548, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 499);
            // 
            // imageListMenu
            // 
            this.imageListMenu.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListMenu.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListMenu.TransparentColor = System.Drawing.Color.Silver;
            // 
            // panelTopButtons
            // 
            this.panelTopButtons.Controls.Add(this.btnWorkoutState);
            this.panelTopButtons.Controls.Add(this.btnDelete);
            this.panelTopButtons.Location = new System.Drawing.Point(0, 47);
            this.panelTopButtons.Name = "panelTopButtons";
            this.panelTopButtons.Size = new System.Drawing.Size(479, 33);
            this.panelTopButtons.TabIndex = 5;
            this.panelTopButtons.Visible = false;
            // 
            // btnWorkoutState
            // 
            this.btnWorkoutState.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnWorkoutState.AutoExpandOnClick = true;
            this.btnWorkoutState.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWorkoutState.Location = new System.Drawing.Point(0, 0);
            this.btnWorkoutState.Name = "btnWorkoutState";
            this.btnWorkoutState.Size = new System.Drawing.Size(99, 33);
            this.btnWorkoutState.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnWorkoutState.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnNotWorkedOut,
            this.btnCorrectWorkedOut,
            this.btnIncorrectWorkedOut});
            this.btnWorkoutState.TabIndex = 0;
            this.btnWorkoutState.Text = "Bewertung";
            // 
            // btnNotWorkedOut
            // 
            this.btnNotWorkedOut.Checked = true;
            this.btnNotWorkedOut.GlobalItem = false;
            this.btnNotWorkedOut.Name = "btnNotWorkedOut";
            this.btnNotWorkedOut.Text = "Keine";
            this.btnNotWorkedOut.Click += new System.EventHandler(this.btnNotWorkedOut_Click);
            // 
            // btnCorrectWorkedOut
            // 
            this.btnCorrectWorkedOut.GlobalItem = false;
            this.btnCorrectWorkedOut.Name = "btnCorrectWorkedOut";
            this.btnCorrectWorkedOut.Text = "Richtig";
            this.btnCorrectWorkedOut.Click += new System.EventHandler(this.btnCorrectWorkedOut_Click);
            // 
            // btnIncorrectWorkedOut
            // 
            this.btnIncorrectWorkedOut.GlobalItem = false;
            this.btnIncorrectWorkedOut.Name = "btnIncorrectWorkedOut";
            this.btnIncorrectWorkedOut.Text = "Falsch";
            this.btnIncorrectWorkedOut.Click += new System.EventHandler(this.btnIncorrectWorkedOut_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(404, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 33);
            this.btnDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Löschen";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panelTopTitle
            // 
            this.panelTopTitle.Controls.Add(this.btnSubmit);
            this.panelTopTitle.Controls.Add(this.btnSolution);
            this.panelTopTitle.Controls.Add(this.btnSave);
            this.panelTopTitle.Controls.Add(this.lblTitle);
            this.panelTopTitle.Location = new System.Drawing.Point(106, 2);
            this.panelTopTitle.Name = "panelTopTitle";
            this.panelTopTitle.Size = new System.Drawing.Size(292, 30);
            this.panelTopTitle.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSubmit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSubmit.Location = new System.Drawing.Point(217, 0);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 30);
            this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubmit.TabIndex = 18;
            this.btnSubmit.Text = "Abgeben";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnSolution
            // 
            this.btnSolution.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSolution.ColorTable = DevComponents.DotNetBar.eButtonColor.MagentaWithBackground;
            this.btnSolution.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSolution.Location = new System.Drawing.Point(71, 0);
            this.btnSolution.Name = "btnSolution";
            this.btnSolution.Size = new System.Drawing.Size(26, 30);
            this.btnSolution.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSolution.TabIndex = 17;
            this.btnSolution.Text = "L";
            this.btnSolution.CheckedChanged += new System.EventHandler(this.btnSolution_CheckedChanged);
            this.btnSolution.Click += new System.EventHandler(this.btnSolution_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 30);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Speichern";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.LightGray;
            // 
            // 
            // 
            this.lblTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(292, 30);
            this.lblTitle.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.lblTitle.TabIndex = 16;
            this.lblTitle.Text = "NAME";
            this.lblTitle.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // ratingStarCorrect
            // 
            this.ratingStarCorrect.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.ratingStarCorrect.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ratingStarCorrect.Dock = System.Windows.Forms.DockStyle.Left;
            this.ratingStarCorrect.Location = new System.Drawing.Point(0, 0);
            this.ratingStarCorrect.Name = "ratingStarCorrect";
            this.ratingStarCorrect.Size = new System.Drawing.Size(262, 21);
            this.ratingStarCorrect.TabIndex = 10;
            this.ratingStarCorrect.Text = "Das ist RICHTIG! :";
            this.ratingStarCorrect.TextColor = System.Drawing.Color.Empty;
            this.ratingStarCorrect.RatingChanged += new System.EventHandler(this.ratingStar1_RatingChanged);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.SystemColors.Control;
            this.panelBottom.Controls.Add(this.lblTextWrong);
            this.panelBottom.Controls.Add(this.ratingStarCorrect);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 478);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(548, 21);
            this.panelBottom.TabIndex = 11;
            // 
            // lblTextWrong
            // 
            this.lblTextWrong.AutoSize = true;
            this.lblTextWrong.BackColor = System.Drawing.Color.Transparent;
            this.lblTextWrong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTextWrong.Location = new System.Drawing.Point(262, 0);
            this.lblTextWrong.Name = "lblTextWrong";
            this.lblTextWrong.Size = new System.Drawing.Size(114, 13);
            this.lblTextWrong.TabIndex = 11;
            this.lblTextWrong.Text = "Das ist leider FALSCH!";
            this.lblTextWrong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(95, 110);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(250, 250);
            this.webBrowser1.TabIndex = 16;
            this.webBrowser1.Visible = false;
            // 
            // SONotice
            // 
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panelTopButtons);
            this.Controls.Add(this.panelTopTitle);
            this.Controls.Add(this.textControl1);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "SONotice";
            this.Size = new System.Drawing.Size(548, 499);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.panelTopButtons.ResumeLayout(false);
            this.panelTopTitle.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion



        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private System.Windows.Forms.ImageList imageListMenu;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem PopupItemCut;
        private DevExpress.XtraBars.BarButtonItem PopupItemCopy;
        private DevExpress.XtraBars.BarButtonItem PopupItemPaste;
        private DevExpress.XtraBars.BarButtonItem PopupItemMarkAll;
        private TXTextControl.TextControl textControl1;
        private Panel panelTopButtons;
        private System.ComponentModel.IContainer components;
        private DevComponents.DotNetBar.ButtonX btnDelete;

        private Panel panelBottom;
        private DevComponents.DotNetBar.Controls.RatingStar ratingStarCorrect;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnWorkoutState;
        private DevComponents.DotNetBar.ButtonItem btnNotWorkedOut;
        private DevComponents.DotNetBar.ButtonItem btnCorrectWorkedOut;
        private DevComponents.DotNetBar.ButtonItem btnIncorrectWorkedOut;
        private Panel panelTopTitle;
        private Label lblTextWrong;
        private DevComponents.DotNetBar.ButtonX btnSolution;
        private DevComponents.DotNetBar.LabelX lblTitle;
        private ButtonX btnSubmit;
        private WebBrowser webBrowser1;
    }
};