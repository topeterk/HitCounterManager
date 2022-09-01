namespace HitCounterManager
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
            this.components = new System.ComponentModel.Container();
            this.ptc = new HitCounterManager.ProfileTabControl();
            this.menu_ptc = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pvc = new HitCounterManager.ProfileViewControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gpSuccession = new System.Windows.Forms.GroupBox();
            this.btnSuccessionVisibility = new System.Windows.Forms.Button();
            this.txtPredecessorTitle = new System.Windows.Forms.TextBox();
            this.cbShowPredecessor = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ptc.SuspendLayout();
            this.menu_ptc.SuspendLayout();
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
            this.ptc.ContextMenuStrip = this.menu_ptc;
            this.ptc.Controls.Add(this.tabPage1);
            this.ptc.Controls.Add(this.tabPage2);
            this.ptc.Controls.Add(this.tabPage3);
            this.ptc.Location = new System.Drawing.Point(0, 0);
            this.ptc.Name = "ptc";
            this.ptc.SelectedIndex = 0;
            this.ptc.ShowToolTips = true;
            this.ptc.Size = new System.Drawing.Size(394, 302);
            this.ptc.TabIndex = 20;
            this.ptc.ProfileTabSelect += new System.EventHandler<HitCounterManager.ProfileTabControl.ProfileTabSelectAction>(this.ProfileTabSelect);
            this.ptc.ProfileChanged += new System.EventHandler<HitCounterManager.ProfileChangedEventArgs>(this.ProfileChangedHandler);
            this.ptc.SelectedProfileChanged += new System.EventHandler<HitCounterManager.ProfileViewControl.SelectedProfileChangedCauseType>(this.SelectedProfileChanged);
            // 
            // menu_ptc
            // 
            this.menu_ptc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTabToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.menu_ptc.Name = "menu_ptc";
            this.menu_ptc.Size = new System.Drawing.Size(118, 48);
            this.menu_ptc.Opening += new System.ComponentModel.CancelEventHandler(this.Menu_ptc_Opening);
            // 
            // addTabToolStripMenuItem
            // 
            this.addTabToolStripMenuItem.Name = "addTabToolStripMenuItem";
            this.addTabToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addTabToolStripMenuItem.Text = "Add tab";
            this.addTabToolStripMenuItem.ToolTipText = "Creates a new tab";
            this.addTabToolStripMenuItem.Click += new System.EventHandler(this.AddTabToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.ToolTipText = "Removes a tab";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pvc);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(386, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1";
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
            this.tabPage2.ToolTipText = "Click on this tab to create a new one";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(386, 276);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "-";
            this.tabPage3.ToolTipText = "Drag and drop a tab on this tab to remove it";
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
            this.txtPredecessorTitle.Location = new System.Drawing.Point(157, 26);
            this.txtPredecessorTitle.Name = "txtPredecessorTitle";
            this.txtPredecessorTitle.Size = new System.Drawing.Size(231, 20);
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
            this.toolTip1.SetToolTip(this.cbShowPredecessor, "Adds a split at the beginning of a run showing the sum of hits taken in all prece" +
        "eding runs.\r\nDoes not appear on the initial run even when checked as there is no" +
        " preceeding run.\r\n\r\n");
            this.cbShowPredecessor.UseVisualStyleBackColor = true;
            this.cbShowPredecessor.CheckedChanged += new System.EventHandler(this.ProfileChangedHandler);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // ProfilesControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.gpSuccession);
            this.Controls.Add(this.ptc);
            this.Name = "ProfilesControl";
            this.Size = new System.Drawing.Size(394, 367);
            this.ptc.ResumeLayout(false);
            this.menu_ptc.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox gpSuccession;
        private System.Windows.Forms.Button btnSuccessionVisibility;
        private System.Windows.Forms.TextBox txtPredecessorTitle;
        private System.Windows.Forms.CheckBox cbShowPredecessor;
        private System.Windows.Forms.ContextMenuStrip menu_ptc;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTabToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
