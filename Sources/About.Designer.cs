namespace HitCounterManager
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.License_Information = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // License_Information
            // 
            this.License_Information.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.License_Information.Location = new System.Drawing.Point(13, 13);
            this.License_Information.Multiline = true;
            this.License_Information.Name = "License_Information";
            this.License_Information.ReadOnly = true;
            this.License_Information.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.License_Information.Size = new System.Drawing.Size(709, 486);
            this.License_Information.TabIndex = 1;
            this.License_Information.TabStop = false;
            this.License_Information.Text = resources.GetString("License_Information.Text");
            // 
            // About
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(734, 511);
            this.Controls.Add(this.License_Information);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 300);
            this.Name = "About";
            this.ShowInTaskbar = false;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox License_Information;
    }
}