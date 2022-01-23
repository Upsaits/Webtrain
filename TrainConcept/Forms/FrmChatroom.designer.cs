using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept
{
    public partial class FrmChatroom
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
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChatroom));
            this.panButtonBar = new System.Windows.Forms.Panel();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.lstTalking = new System.Windows.Forms.ListBox();
            this.panButtonBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panButtonBar
            // 
            this.panButtonBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panButtonBar.Controls.Add(this.txtTo);
            this.panButtonBar.Controls.Add(this.label1);
            this.panButtonBar.Controls.Add(this.btnSend);
            this.panButtonBar.Controls.Add(this.txtMessage);
            this.panButtonBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panButtonBar.Location = new System.Drawing.Point(0, 511);
            this.panButtonBar.Name = "panButtonBar";
            this.panButtonBar.Size = new System.Drawing.Size(632, 51);
            this.panButtonBar.TabIndex = 4;
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(40, 17);
            this.txtTo.Name = "txtTo";
            this.txtTo.ReadOnly = true;
            this.txtTo.Size = new System.Drawing.Size(88, 21);
            this.txtTo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "An:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(544, 17);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 25);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Senden";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(136, 17);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(400, 21);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            // 
            // lstUsers
            // 
            this.lstUsers.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstUsers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstUsers.Location = new System.Drawing.Point(0, 0);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(160, 511);
            this.lstUsers.TabIndex = 5;
            this.lstUsers.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstUsers_DrawItem);
            this.lstUsers.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lstUsers_MeasureItem);
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(160, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(6, 511);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // lstTalking
            // 
            this.lstTalking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTalking.Location = new System.Drawing.Point(166, 0);
            this.lstTalking.Name = "lstTalking";
            this.lstTalking.Size = new System.Drawing.Size(466, 511);
            this.lstTalking.TabIndex = 7;
            // 
            // FrmChatroom
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(632, 562);
            this.Controls.Add(this.lstTalking);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lstUsers);
            this.Controls.Add(this.panButtonBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmChatroom";
            this.Text = "FrmChatroom";
            this.Closed += new System.EventHandler(this.FrmChatroom_Closed);
            this.panButtonBar.ResumeLayout(false);
            this.panButtonBar.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panButtonBar;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListBox lstTalking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTo;

    }
}
