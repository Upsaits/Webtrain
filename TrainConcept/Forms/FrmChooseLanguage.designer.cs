using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftObject.TrainConcept
{
    public partial class FrmChooseLanguage
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
        /// <summary/>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChooseLanguage));
            this.BtnGerman = new System.Windows.Forms.RadioButton();
            this.BtnEngland = new System.Windows.Forms.RadioButton();
            this.BtnFrench = new System.Windows.Forms.RadioButton();
            this.BtnItaly = new System.Windows.Forms.RadioButton();
            this.BtnSpain = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.BtnEstonia = new System.Windows.Forms.RadioButton();
            this.BtnChinese = new System.Windows.Forms.RadioButton();
            this.BtnHungarian = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnGerman
            // 
            this.BtnGerman.Appearance = System.Windows.Forms.Appearance.Button;
            this.BtnGerman.Image = ((System.Drawing.Image)(resources.GetObject("BtnGerman.Image")));
            this.BtnGerman.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnGerman.Location = new System.Drawing.Point(59, 28);
            this.BtnGerman.Name = "BtnGerman";
            this.BtnGerman.Size = new System.Drawing.Size(112, 54);
            this.BtnGerman.TabIndex = 0;
            this.BtnGerman.Text = "Deutsch";
            this.BtnGerman.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnEngland
            // 
            this.BtnEngland.Appearance = System.Windows.Forms.Appearance.Button;
            this.BtnEngland.Image = ((System.Drawing.Image)(resources.GetObject("BtnEngland.Image")));
            this.BtnEngland.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnEngland.Location = new System.Drawing.Point(59, 89);
            this.BtnEngland.Name = "BtnEngland";
            this.BtnEngland.Size = new System.Drawing.Size(112, 54);
            this.BtnEngland.TabIndex = 1;
            this.BtnEngland.Text = "English";
            this.BtnEngland.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnFrench
            // 
            this.BtnFrench.Appearance = System.Windows.Forms.Appearance.Button;
            this.BtnFrench.Image = ((System.Drawing.Image)(resources.GetObject("BtnFrench.Image")));
            this.BtnFrench.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnFrench.Location = new System.Drawing.Point(59, 151);
            this.BtnFrench.Name = "BtnFrench";
            this.BtnFrench.Size = new System.Drawing.Size(112, 54);
            this.BtnFrench.TabIndex = 2;
            this.BtnFrench.Text = "Français";
            this.BtnFrench.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnItaly
            // 
            this.BtnItaly.Appearance = System.Windows.Forms.Appearance.Button;
            this.BtnItaly.Image = ((System.Drawing.Image)(resources.GetObject("BtnItaly.Image")));
            this.BtnItaly.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnItaly.Location = new System.Drawing.Point(59, 274);
            this.BtnItaly.Name = "BtnItaly";
            this.BtnItaly.Size = new System.Drawing.Size(112, 53);
            this.BtnItaly.TabIndex = 3;
            this.BtnItaly.Text = "Italiano";
            this.BtnItaly.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnSpain
            // 
            this.BtnSpain.Appearance = System.Windows.Forms.Appearance.Button;
            this.BtnSpain.Image = ((System.Drawing.Image)(resources.GetObject("BtnSpain.Image")));
            this.BtnSpain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSpain.Location = new System.Drawing.Point(59, 212);
            this.BtnSpain.Name = "BtnSpain";
            this.BtnSpain.Size = new System.Drawing.Size(112, 54);
            this.BtnSpain.TabIndex = 4;
            this.BtnSpain.Text = "Español";
            this.BtnSpain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 389);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(152, 353);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(264, 353);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BtnEstonia
            // 
            this.BtnEstonia.Appearance = System.Windows.Forms.Appearance.Button;
            this.BtnEstonia.Image = ((System.Drawing.Image)(resources.GetObject("BtnEstonia.Image")));
            this.BtnEstonia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnEstonia.Location = new System.Drawing.Point(185, 28);
            this.BtnEstonia.Name = "BtnEstonia";
            this.BtnEstonia.Size = new System.Drawing.Size(112, 54);
            this.BtnEstonia.TabIndex = 9;
            this.BtnEstonia.Text = "Eesti";
            this.BtnEstonia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnChinese
            // 
            this.BtnChinese.Appearance = System.Windows.Forms.Appearance.Button;
            this.BtnChinese.Image = ((System.Drawing.Image)(resources.GetObject("BtnChinese.Image")));
            this.BtnChinese.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnChinese.Location = new System.Drawing.Point(185, 89);
            this.BtnChinese.Name = "BtnChinese";
            this.BtnChinese.Size = new System.Drawing.Size(112, 54);
            this.BtnChinese.TabIndex = 10;
            this.BtnChinese.Text = "Chinese";
            this.BtnChinese.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BtnHungarian
            // 
            this.BtnHungarian.Appearance = System.Windows.Forms.Appearance.Button;
            this.BtnHungarian.Image = ((System.Drawing.Image)(resources.GetObject("BtnHungarian.Image")));
            this.BtnHungarian.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnHungarian.Location = new System.Drawing.Point(185, 150);
            this.BtnHungarian.Name = "BtnHungarian";
            this.BtnHungarian.Size = new System.Drawing.Size(112, 54);
            this.BtnHungarian.TabIndex = 11;
            this.BtnHungarian.Text = "Magyar";
            this.BtnHungarian.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmChooseLanguage
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(492, 392);
            this.Controls.Add(this.BtnHungarian);
            this.Controls.Add(this.BtnChinese);
            this.Controls.Add(this.BtnEstonia);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.BtnSpain);
            this.Controls.Add(this.BtnItaly);
            this.Controls.Add(this.BtnFrench);
            this.Controls.Add(this.BtnEngland);
            this.Controls.Add(this.BtnGerman);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LookAndFeel.SkinName = "Dark Side";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChooseLanguage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sprachauswahl";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.RadioButton BtnGerman;
        private System.Windows.Forms.RadioButton BtnEngland;
        private System.Windows.Forms.RadioButton BtnFrench;
        private System.Windows.Forms.RadioButton BtnItaly;
        private System.Windows.Forms.RadioButton BtnSpain;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton BtnEstonia;
        private System.Windows.Forms.RadioButton BtnChinese;
        private System.Windows.Forms.RadioButton BtnHungarian;

    }
}
