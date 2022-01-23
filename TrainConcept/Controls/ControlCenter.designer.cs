namespace SoftObject.TrainConcept.Controls
{
    public partial class ControlCenter
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
            this.components = new System.ComponentModel.Container();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.radialMenu1 = new DevComponents.DotNetBar.RadialMenu();
            this.radMenuItemAccept = new DevComponents.DotNetBar.RadialMenuItem();
            this.radMenuItemDelete = new DevComponents.DotNetBar.RadialMenuItem();
            this.radMenuItemShare = new DevComponents.DotNetBar.RadialMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // radialMenu1
            // 
            this.radialMenu1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.radMenuItemAccept,
            this.radMenuItemDelete,
            this.radMenuItemShare});
            this.radialMenu1.Location = new System.Drawing.Point(3, 119);
            this.radialMenu1.MenuType = DevComponents.DotNetBar.eRadialMenuType.Circular;
            this.radialMenu1.Name = "radialMenu1";
            this.radialMenu1.Size = new System.Drawing.Size(28, 28);
            this.radialMenu1.Symbol = "";
            this.radialMenu1.SymbolSize = 13F;
            this.radialMenu1.TabIndex = 0;
            this.radialMenu1.Text = "radialMenu1";
            this.radialMenu1.Visible = false;
            this.radialMenu1.MenuOpened += new System.EventHandler(this.radialMenu1_MenuOpened);
            this.radialMenu1.MenuClosed += new System.EventHandler(this.radialMenu1_MenuClosed);
            this.radialMenu1.ItemClick += new System.EventHandler(this.radialMenu1_ItemClick);
            this.radialMenu1.MouseEnter += new System.EventHandler(this.radialMenu1_MouseEnter);
            this.radialMenu1.MouseLeave += new System.EventHandler(this.radialMenu1_MouseLeave);
            // 
            // radMenuItemAccept
            // 
            this.radMenuItemAccept.CircularBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.radMenuItemAccept.CircularMenuDiameter = 40;
            this.radMenuItemAccept.Name = "radMenuItemAccept";
            this.radMenuItemAccept.Symbol = "";
            this.radMenuItemAccept.Text = "öffnen";
            // 
            // radMenuItemDelete
            // 
            this.radMenuItemDelete.CircularBackColor = System.Drawing.Color.Red;
            this.radMenuItemDelete.CircularMenuDiameter = 40;
            this.radMenuItemDelete.Name = "radMenuItemDelete";
            this.radMenuItemDelete.Symbol = "";
            this.radMenuItemDelete.Text = "löschen";
            // 
            // radMenuItemShare
            // 
            this.radMenuItemShare.Name = "radMenuItemShare";
            this.radMenuItemShare.Symbol = "";
            this.radMenuItemShare.Text = "Verteilen";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ControlCenter
            // 
            this.Controls.Add(this.radialMenu1);
            this.Name = "ControlCenter";
            this.Size = new System.Drawing.Size(200, 150);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.RadialMenu radialMenu1;
        private DevComponents.DotNetBar.RadialMenuItem radMenuItemAccept;
        private DevComponents.DotNetBar.RadialMenuItem radMenuItemDelete;
        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.RadialMenuItem radMenuItemShare;
    }
}
