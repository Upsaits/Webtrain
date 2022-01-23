using SoftObject.TrainConcept.Controls;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmClassroomEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFrmClassroomEdit));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnResetProgress = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.btnAdd = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xUsersTree1 = new XUsersTree();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnResetProgress);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1033, 111);
            this.panel1.TabIndex = 10;
            // 
            // btnResetProgress
            // 
            this.btnResetProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetProgress.Location = new System.Drawing.Point(106, 15);
            this.btnResetProgress.Name = "btnResetProgress";
            this.btnResetProgress.Size = new System.Drawing.Size(88, 36);
            this.btnResetProgress.TabIndex = 10;
            this.btnResetProgress.Text = "Lernforschritt löschen";
            this.btnResetProgress.Click += new System.EventHandler(this.btnResetProgress_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.radioGroup1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(478, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(555, 111);
            this.panel2.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.labelControl1);
            this.panel4.Location = new System.Drawing.Point(91, 13);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(545, 84);
            this.panel4.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Speichern";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(218, 39);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Die in der Klasse abzuarbeitenden Lerninhalte \r\nwerden über die Lernmappen jedes " +
    "einzelnen\r\nBenutzers definiert.";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.labelControl2);
            this.panel3.Controls.Add(this.listBoxControl1);
            this.panel3.Location = new System.Drawing.Point(6, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(551, 77);
            this.panel3.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 47);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Speichern";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(199, 39);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Die in der Klasse abzuarbeitenden Inhalte\r\nwerden über folgende Klassenmappen\r\nde" +
    "finiert:";
            // 
            // listBoxControl1
            // 
            this.listBoxControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.listBoxControl1.Location = new System.Drawing.Point(232, 0);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new System.Drawing.Size(319, 77);
            this.listBoxControl1.TabIndex = 9;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioGroup1.Location = new System.Drawing.Point(0, 0);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Benutzerfixiert"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Lernmappen fixiert")});
            this.radioGroup1.Size = new System.Drawing.Size(551, 30);
            this.radioGroup1.TabIndex = 10;
            this.radioGroup1.EditValueChanged += new System.EventHandler(this.radioGroup1_EditValueChanged);
            this.radioGroup1.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.radioGroup1_EditValueChanging);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(12, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 36);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Benutzer festlegen";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // xUsersTree1
            // 
            this.xUsersTree1.ClassName = "";
            this.xUsersTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xUsersTree1.IsCheckable = false;
            this.xUsersTree1.Location = new System.Drawing.Point(0, 0);
            this.xUsersTree1.LookAndFeel.SkinName = "Dark Side";
            this.xUsersTree1.Name = "xUsersTree1";
            this.xUsersTree1.Size = new System.Drawing.Size(1033, 400);
            this.xUsersTree1.TabIndex = 11;
            this.xUsersTree1.Type = XUsersTree.ViewType.ClassView;
            this.xUsersTree1.UserList = null;
            this.xUsersTree1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xUsersTree1_KeyDown);
            // 
            // XFrmClassroomEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(1033, 511);
            this.Controls.Add(this.xUsersTree1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XFrmClassroomEdit";
            this.Text = "XFrmClassroomEdit";
            this.Closed += new System.EventHandler(this.XFrmClassroomEdit_Closed);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnAdd;
        private XUsersTree xUsersTree1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnResetProgress;
    }
}
