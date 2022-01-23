namespace WebtrainWebPortal
{
    public partial class AdminUserForm
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

        #region Wisej Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.asPxRichEditWrapper1 = new WebtrainWebPortal.Wrappers.ASPxRichEditWrapper();
            this.SuspendLayout();
            // 
            // asPxRichEditWrapper1
            // 
            this.asPxRichEditWrapper1.Dock = Wisej.Web.DockStyle.Fill;
            this.asPxRichEditWrapper1.Name = "asPxRichEditWrapper1";
            this.asPxRichEditWrapper1.Size = new System.Drawing.Size(617, 442);
            this.asPxRichEditWrapper1.TabIndex = 0;
            this.asPxRichEditWrapper1.Text = "asPxRichEditWrapper1";
            // 
            // AdminUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 442);
            this.Controls.Add(this.asPxRichEditWrapper1);
            this.Name = "AdminUserForm";
            this.Text = "Administratoren";
            this.Load += new System.EventHandler(this.Window1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Wrappers.ASPxRichEditWrapper asPxRichEditWrapper1;
    }
}