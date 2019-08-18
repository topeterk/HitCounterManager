namespace HitCounterManager
{
    partial class ProfileViewControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.DataGridView1 = new HitCounterManager.ProfileDataGridView();
            this.cTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cHits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cWayHits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDiff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboBox1
            // 
            this.ComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Location = new System.Drawing.Point(0, 0);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(624, 21);
            this.ComboBox1.Sorted = true;
            this.ComboBox1.TabIndex = 22;
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToResizeRows = false;
            this.DataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cTitle,
            this.cHits,
            this.cWayHits,
            this.cDiff,
            this.cPB,
            this.cSP});
            this.DataGridView1.Location = new System.Drawing.Point(0, 27);
            this.DataGridView1.MultiSelect = false;
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.Size = new System.Drawing.Size(624, 198);
            this.DataGridView1.TabIndex = 23;
            this.DataGridView1.Visible = false;
            // 
            // cTitle
            // 
            this.cTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cTitle.HeaderText = "Title";
            this.cTitle.MinimumWidth = 30;
            this.cTitle.Name = "cTitle";
            this.cTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cTitle.ToolTipText = "Title of the split";
            this.cTitle.Width = 300;
            // 
            // cHits
            // 
            this.cHits.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.NullValue = "0";
            this.cHits.DefaultCellStyle = dataGridViewCellStyle1;
            this.cHits.HeaderText = "Hits (Boss)";
            this.cHits.MinimumWidth = 30;
            this.cHits.Name = "cHits";
            this.cHits.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cHits.ToolTipText = "Actual split hit count (counter for all or boss hits only)";
            this.cHits.Width = 50;
            // 
            // cWayHits
            // 
            this.cWayHits.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.NullValue = "0";
            this.cWayHits.DefaultCellStyle = dataGridViewCellStyle2;
            this.cWayHits.HeaderText = "Hits (Way)";
            this.cWayHits.MinimumWidth = 30;
            this.cWayHits.Name = "cWayHits";
            this.cWayHits.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cWayHits.ToolTipText = "Actual split hit count (counter for hits outside of bosses)";
            this.cWayHits.Width = 50;
            // 
            // cDiff
            // 
            this.cDiff.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.NullValue = "0";
            this.cDiff.DefaultCellStyle = dataGridViewCellStyle3;
            this.cDiff.HeaderText = "Diff";
            this.cDiff.MinimumWidth = 30;
            this.cDiff.Name = "cDiff";
            this.cDiff.ReadOnly = true;
            this.cDiff.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cDiff.ToolTipText = "Difference between Hits and PB";
            this.cDiff.Width = 50;
            // 
            // cPB
            // 
            this.cPB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.NullValue = "0";
            this.cPB.DefaultCellStyle = dataGridViewCellStyle4;
            this.cPB.HeaderText = "PB";
            this.cPB.MinimumWidth = 30;
            this.cPB.Name = "cPB";
            this.cPB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cPB.ToolTipText = "Person best";
            this.cPB.Width = 50;
            // 
            // cSP
            // 
            this.cSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cSP.HeaderText = "SP";
            this.cSP.MinimumWidth = 30;
            this.cSP.Name = "cSP";
            this.cSP.ToolTipText = "Session progress";
            this.cSP.Width = 30;
            // 
            // ProfileViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.DataGridView1);
            this.Name = "ProfileViewControl";
            this.Size = new System.Drawing.Size(624, 225);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBox1;
        private ProfileDataGridView DataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn cHits;
        private System.Windows.Forms.DataGridViewTextBoxColumn cWayHits;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDiff;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPB;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSP;
    }
}
