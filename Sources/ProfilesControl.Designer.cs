﻿namespace HitCounterManager
{
    partial class ProfilesControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.ptc = new HitCounterManager.ProfileTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pvc = new HitCounterManager.ProfileViewControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gpSuccession = new System.Windows.Forms.GroupBox();
            this.btnSuccessionVisibility = new System.Windows.Forms.Button();
            this.txtPredecessorTitle = new System.Windows.Forms.TextBox();
            this.cbShowPredecessor = new System.Windows.Forms.CheckBox();
            this.ptc.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gpSuccession.SuspendLayout();
            this.SuspendLayout();
            // 
            // ptc
            // 
            this.ptc.AllowDrop = true;
            this.ptc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ptc.Controls.Add(this.tabPage1);
            this.ptc.Controls.Add(this.tabPage2);
            this.ptc.Controls.Add(this.tabPage3);
            this.ptc.Location = new System.Drawing.Point(0, 0);
            this.ptc.Name = "ptc";
            this.ptc.SelectedIndex = 0;
            this.ptc.Size = new System.Drawing.Size(394, 302);
            this.ptc.TabIndex = 20;
            this.ptc.ProfileTabSelect += new System.EventHandler<HitCounterManager.ProfileTabControl.ProfileTabSelectAction>(this.ProfileTabSelect);
            this.ptc.ProfileChanged += new System.EventHandler<System.EventArgs>(this.ProfileChangedHandler);
            this.ptc.SelectedProfileChanged += new System.EventHandler<HitCounterManager.ProfileViewControl.SelectedProfileChangedCauseType>(this.SelectedProfileChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pvc);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(386, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pvc
            // 
            this.pvc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pvc.Location = new System.Drawing.Point(3, 3);
            this.pvc.Name = "pvc";
            this.pvc.Size = new System.Drawing.Size(380, 270);
            this.pvc.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(386, 276);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "+";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(386, 276);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "-";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gpSuccession
            // 
            this.gpSuccession.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpSuccession.Controls.Add(this.btnSuccessionVisibility);
            this.gpSuccession.Controls.Add(this.txtPredecessorTitle);
            this.gpSuccession.Controls.Add(this.cbShowPredecessor);
            this.gpSuccession.Location = new System.Drawing.Point(0, 308);
            this.gpSuccession.Name = "gpSuccession";
            this.gpSuccession.Size = new System.Drawing.Size(394, 59);
            this.gpSuccession.TabIndex = 21;
            this.gpSuccession.TabStop = false;
            this.gpSuccession.Text = "Succession";
            // 
            // btnSuccessionVisibility
            // 
            this.btnSuccessionVisibility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSuccessionVisibility.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_double_up_20;
            this.btnSuccessionVisibility.FlatAppearance.BorderSize = 0;
            this.btnSuccessionVisibility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuccessionVisibility.Location = new System.Drawing.Point(368, 0);
            this.btnSuccessionVisibility.Name = "btnSuccessionVisibility";
            this.btnSuccessionVisibility.Size = new System.Drawing.Size(20, 20);
            this.btnSuccessionVisibility.TabIndex = 0;
            this.btnSuccessionVisibility.UseVisualStyleBackColor = true;
            this.btnSuccessionVisibility.Click += new System.EventHandler(this.btnSuccessionVisibility_Click);
            // 
            // txtPredecessorTitle
            // 
            this.txtPredecessorTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPredecessorTitle.Location = new System.Drawing.Point(139, 26);
            this.txtPredecessorTitle.Name = "txtPredecessorTitle";
            this.txtPredecessorTitle.Size = new System.Drawing.Size(249, 20);
            this.txtPredecessorTitle.TabIndex = 2;
            this.txtPredecessorTitle.TextChanged += new System.EventHandler(this.ProfileChangedHandler);
            // 
            // cbShowPredecessor
            // 
            this.cbShowPredecessor.AutoSize = true;
            this.cbShowPredecessor.Location = new System.Drawing.Point(6, 28);
            this.cbShowPredecessor.Name = "cbShowPredecessor";
            this.cbShowPredecessor.Size = new System.Drawing.Size(127, 17);
            this.cbShowPredecessor.TabIndex = 1;
            this.cbShowPredecessor.Text = "Show previous totals:";
            this.cbShowPredecessor.UseVisualStyleBackColor = true;
            this.cbShowPredecessor.CheckedChanged += new System.EventHandler(this.ProfileChangedHandler);
            // 
            // ProfilesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpSuccession);
            this.Controls.Add(this.ptc);
            this.Name = "ProfilesControl";
            this.Size = new System.Drawing.Size(394, 367);
            this.ptc.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gpSuccession.ResumeLayout(false);
            this.gpSuccession.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ProfileTabControl ptc;
        private System.Windows.Forms.TabPage tabPage1;
        private ProfileViewControl pvc;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.GroupBox gpSuccession;
        private System.Windows.Forms.Button btnSuccessionVisibility;
        public System.Windows.Forms.TextBox txtPredecessorTitle;
        public System.Windows.Forms.CheckBox cbShowPredecessor;
    }
}
