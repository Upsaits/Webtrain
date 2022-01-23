namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmEditKeywordAction
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtEditText = new DevExpress.XtraEditors.TextEdit();
            this.txtEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkIsGlossary = new System.Windows.Forms.CheckBox();
            this.chkIsTooltip = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditDescription.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 314);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 65);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(125, 22);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(23, 20);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtEditText
            // 
            this.txtEditText.EditValue = "textEdit1";
            this.txtEditText.Location = new System.Drawing.Point(62, 12);
            this.txtEditText.Name = "txtEditText";
            this.txtEditText.Size = new System.Drawing.Size(459, 20);
            this.txtEditText.TabIndex = 1;
            this.txtEditText.Leave += new System.EventHandler(this.txtEditText_Leave);
            // 
            // txtEditDescription
            // 
            this.txtEditDescription.EditValue = "memoEdit1";
            this.txtEditDescription.Location = new System.Drawing.Point(84, 38);
            this.txtEditDescription.Name = "txtEditDescription";
            this.txtEditDescription.Size = new System.Drawing.Size(437, 203);
            this.txtEditDescription.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkIsGlossary);
            this.panel2.Controls.Add(this.chkIsTooltip);
            this.panel2.Location = new System.Drawing.Point(0, 256);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(533, 61);
            this.panel2.TabIndex = 3;
            // 
            // chkIsGlossary
            // 
            this.chkIsGlossary.Location = new System.Drawing.Point(32, 31);
            this.chkIsGlossary.Name = "chkIsGlossary";
            this.chkIsGlossary.Size = new System.Drawing.Size(140, 25);
            this.chkIsGlossary.TabIndex = 1;
            this.chkIsGlossary.Text = "Im Lexikon anzeigen";
            // 
            // chkIsTooltip
            // 
            this.chkIsTooltip.Location = new System.Drawing.Point(32, 6);
            this.chkIsTooltip.Name = "chkIsTooltip";
            this.chkIsTooltip.Size = new System.Drawing.Size(140, 26);
            this.chkIsTooltip.TabIndex = 0;
            this.chkIsTooltip.Text = "Tooltip im Text zeigen";
            this.chkIsTooltip.CheckedChanged += new System.EventHandler(this.chkIsTooltip_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Stichwort:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Beschreibung:";
            // 
            // XFrmEditKeywordAction
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(533, 379);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtEditDescription);
            this.Controls.Add(this.txtEditText);
            this.Controls.Add(this.panel1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "XFrmEditKeywordAction";
            this.Text = "FrmEditKeywordAction";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtEditText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditDescription.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.TextEdit txtEditText;
        private DevExpress.XtraEditors.MemoEdit txtEditDescription;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkIsTooltip;
        private System.Windows.Forms.CheckBox chkIsGlossary;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
