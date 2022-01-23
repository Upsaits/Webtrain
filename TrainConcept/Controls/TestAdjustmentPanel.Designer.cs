namespace SoftObject.TrainConcept.Controls
{
    partial class TestAdjustmentPanel
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
            this.rbnQuChoose = new System.Windows.Forms.RadioButton();
            this.rbnQuSelect = new System.Windows.Forms.RadioButton();
            this.spnQuCnt = new DevExpress.XtraEditors.SpinEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.spnSuccessLevel = new DevExpress.XtraEditors.SpinEdit();
            this.spnTrialCnt = new DevExpress.XtraEditors.SpinEdit();
            this.chkTestAlwaysAllowed = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.spnQuCnt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnSuccessLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTrialCnt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // rbnQuChoose
            // 
            this.rbnQuChoose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnQuChoose.Location = new System.Drawing.Point(12, 24);
            this.rbnQuChoose.Name = "rbnQuChoose";
            this.rbnQuChoose.Size = new System.Drawing.Size(168, 16);
            this.rbnQuChoose.TabIndex = 2;
            this.rbnQuChoose.TabStop = true;
            this.rbnQuChoose.Text = "Fragen zufällig wählen";
            this.rbnQuChoose.CheckedChanged += new System.EventHandler(this.rbnQuChoose_CheckedChanged);
            // 
            // rbnQuSelect
            // 
            this.rbnQuSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnQuSelect.Location = new System.Drawing.Point(12, 48);
            this.rbnQuSelect.Name = "rbnQuSelect";
            this.rbnQuSelect.Size = new System.Drawing.Size(168, 16);
            this.rbnQuSelect.TabIndex = 3;
            this.rbnQuSelect.Text = "Fragenliste verwenden";
            this.rbnQuSelect.CheckedChanged += new System.EventHandler(this.rbnQuSelect_CheckedChanged);
            // 
            // spnQuCnt
            // 
            this.spnQuCnt.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnQuCnt.Location = new System.Drawing.Point(121, 74);
            this.spnQuCnt.Name = "spnQuCnt";
            this.spnQuCnt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnQuCnt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spnQuCnt.Properties.Mask.EditMask = "d";
            this.spnQuCnt.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spnQuCnt.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnQuCnt.Size = new System.Drawing.Size(47, 20);
            this.spnQuCnt.TabIndex = 15;
            this.spnQuCnt.EditValueChanged += new System.EventHandler(this.spnQuCnt_EditValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 32);
            this.label1.TabIndex = 14;
            this.label1.Text = "Fragenanzahl:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 32);
            this.label2.TabIndex = 16;
            this.label2.Text = "Erfolgsgrenze:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(164, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 24);
            this.label3.TabIndex = 17;
            this.label3.Text = "%";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 32);
            this.label4.TabIndex = 18;
            this.label4.Text = "Anzahl Versuche:";
            // 
            // spnSuccessLevel
            // 
            this.spnSuccessLevel.EditValue = new decimal(new int[] {
            51,
            0,
            0,
            0});
            this.spnSuccessLevel.Location = new System.Drawing.Point(119, 106);
            this.spnSuccessLevel.Name = "spnSuccessLevel";
            this.spnSuccessLevel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnSuccessLevel.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spnSuccessLevel.Properties.Mask.EditMask = "d";
            this.spnSuccessLevel.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spnSuccessLevel.Properties.MinValue = new decimal(new int[] {
            51,
            0,
            0,
            0});
            this.spnSuccessLevel.Size = new System.Drawing.Size(49, 20);
            this.spnSuccessLevel.TabIndex = 19;
            this.spnSuccessLevel.EditValueChanged += new System.EventHandler(this.spnSuccessLevel_EditValueChanged);
            // 
            // spnTrialCnt
            // 
            this.spnTrialCnt.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnTrialCnt.Location = new System.Drawing.Point(119, 138);
            this.spnTrialCnt.Name = "spnTrialCnt";
            this.spnTrialCnt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnTrialCnt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spnTrialCnt.Properties.Mask.EditMask = "d";
            this.spnTrialCnt.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spnTrialCnt.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnTrialCnt.Size = new System.Drawing.Size(49, 20);
            this.spnTrialCnt.TabIndex = 20;
            this.spnTrialCnt.EditValueChanged += new System.EventHandler(this.spnTrialCnt_EditValueChanged);
            // 
            // chkTestAlwaysAllowed
            // 
            this.chkTestAlwaysAllowed.AutoSize = true;
            this.chkTestAlwaysAllowed.Location = new System.Drawing.Point(12, 176);
            this.chkTestAlwaysAllowed.Name = "chkTestAlwaysAllowed";
            this.chkTestAlwaysAllowed.Size = new System.Drawing.Size(121, 17);
            this.chkTestAlwaysAllowed.TabIndex = 21;
            this.chkTestAlwaysAllowed.Text = "Test immer zulassen";
            this.chkTestAlwaysAllowed.UseVisualStyleBackColor = true;
            this.chkTestAlwaysAllowed.CheckedChanged += new System.EventHandler(this.chkTestAlwaysAllowed_CheckedChanged);
            // 
            // TestAdjustmentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.chkTestAlwaysAllowed);
            this.Controls.Add(this.spnTrialCnt);
            this.Controls.Add(this.spnSuccessLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.spnQuCnt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbnQuChoose);
            this.Controls.Add(this.rbnQuSelect);
            this.Name = "TestAdjustmentPanel";
            this.Size = new System.Drawing.Size(211, 194);
            ((System.ComponentModel.ISupportInitialize)(this.spnQuCnt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnSuccessLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTrialCnt.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbnQuChoose;
        private System.Windows.Forms.RadioButton rbnQuSelect;
        private DevExpress.XtraEditors.SpinEdit spnQuCnt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SpinEdit spnSuccessLevel;
        private DevExpress.XtraEditors.SpinEdit spnTrialCnt;
        private System.Windows.Forms.CheckBox chkTestAlwaysAllowed;

    }
}
