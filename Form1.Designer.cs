namespace HitCounterManager
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnAttempts = new System.Windows.Forms.Button();
            this.Spacer2 = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnWeb = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnPB = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnHit = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.Spacer1 = new System.Windows.Forms.Label();
            this.lbl_totals = new System.Windows.Forms.Label();
            this.lbl_progress = new System.Windows.Forms.Label();
            this.cSP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cPB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDiff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cHits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAttempts
            // 
            this.btnAttempts.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_counter_20;
            this.btnAttempts.FlatAppearance.BorderSize = 0;
            this.btnAttempts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttempts.Location = new System.Drawing.Point(251, 4);
            this.btnAttempts.Name = "btnAttempts";
            this.btnAttempts.Size = new System.Drawing.Size(20, 20);
            this.btnAttempts.TabIndex = 42;
            this.ToolTip1.SetToolTip(this.btnAttempts, "Set amount of attempts manually");
            this.btnAttempts.UseVisualStyleBackColor = true;
            this.btnAttempts.Click += new System.EventHandler(this.btnAttempts_Click);
            // 
            // Spacer2
            // 
            this.Spacer2.AutoSize = true;
            this.Spacer2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Spacer2.Location = new System.Drawing.Point(236, 8);
            this.Spacer2.Name = "Spacer2";
            this.Spacer2.Size = new System.Drawing.Size(9, 13);
            this.Spacer2.TabIndex = 41;
            this.Spacer2.Text = "|";
            // 
            // btnAbout
            // 
            this.btnAbout.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_about_20;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Location = new System.Drawing.Point(91, 4);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(20, 20);
            this.btnAbout.TabIndex = 39;
            this.ToolTip1.SetToolTip(this.btnAbout, "About");
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnWeb
            // 
            this.btnWeb.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_website_20;
            this.btnWeb.FlatAppearance.BorderSize = 0;
            this.btnWeb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeb.Location = new System.Drawing.Point(65, 4);
            this.btnWeb.Name = "btnWeb";
            this.btnWeb.Size = new System.Drawing.Size(20, 20);
            this.btnWeb.TabIndex = 38;
            this.ToolTip1.SetToolTip(this.btnWeb, "Help");
            this.btnWeb.UseVisualStyleBackColor = true;
            this.btnWeb.Click += new System.EventHandler(this.btnWeb_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_save_20;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(39, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(20, 20);
            this.btnSave.TabIndex = 37;
            this.ToolTip1.SetToolTip(this.btnSave, "Save");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_settings_20;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Location = new System.Drawing.Point(13, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(20, 20);
            this.btnSettings.TabIndex = 36;
            this.ToolTip1.SetToolTip(this.btnSettings, "Settings");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_copy_20;
            this.btnCopy.FlatAppearance.BorderSize = 0;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Location = new System.Drawing.Point(184, 4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(20, 20);
            this.btnCopy.TabIndex = 35;
            this.ToolTip1.SetToolTip(this.btnCopy, "Copy profile");
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPB
            // 
            this.btnPB.BackColor = System.Drawing.Color.LightYellow;
            this.btnPB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPB.Image = global::HitCounterManager.Properties.Resources.icons8_trophy_32;
            this.btnPB.Location = new System.Drawing.Point(93, 57);
            this.btnPB.Name = "btnPB";
            this.btnPB.Size = new System.Drawing.Size(75, 40);
            this.btnPB.TabIndex = 32;
            this.ToolTip1.SetToolTip(this.btnPB, "Record run as PB (personal best)");
            this.btnPB.UseVisualStyleBackColor = false;
            this.btnPB.Click += new System.EventHandler(this.btnPB_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_trash_20;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(210, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(20, 20);
            this.btnDelete.TabIndex = 31;
            this.ToolTip1.SetToolTip(this.btnDelete, "Delete profile");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRename
            // 
            this.btnRename.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_edit_20;
            this.btnRename.FlatAppearance.BorderSize = 0;
            this.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRename.Location = new System.Drawing.Point(158, 4);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(20, 20);
            this.btnRename.TabIndex = 30;
            this.ToolTip1.SetToolTip(this.btnRename, "Rename profile");
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_add_20;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Location = new System.Drawing.Point(132, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(20, 20);
            this.btnNew.TabIndex = 29;
            this.ToolTip1.SetToolTip(this.btnNew, "New profile");
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_scroll_up_20;
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Location = new System.Drawing.Point(277, 4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(20, 20);
            this.btnUp.TabIndex = 27;
            this.ToolTip1.SetToolTip(this.btnUp, "Move selected split UP");
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackgroundImage = global::HitCounterManager.Properties.Resources.icons8_scroll_down_20;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(303, 4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(20, 20);
            this.btnDown.TabIndex = 26;
            this.ToolTip1.SetToolTip(this.btnDown, "Move selected split DOWN");
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnHit
            // 
            this.btnHit.BackColor = System.Drawing.Color.LightBlue;
            this.btnHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHit.Image = global::HitCounterManager.Properties.Resources.icons8_attack_32;
            this.btnHit.Location = new System.Drawing.Point(174, 57);
            this.btnHit.Name = "btnHit";
            this.btnHit.Size = new System.Drawing.Size(305, 40);
            this.btnHit.TabIndex = 23;
            this.ToolTip1.SetToolTip(this.btnHit, "Count a HIT on the current split");
            this.btnHit.UseVisualStyleBackColor = false;
            this.btnHit.Click += new System.EventHandler(this.btnHit_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Salmon;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Image = global::HitCounterManager.Properties.Resources.icons8_repeat_one_32;
            this.btnReset.Location = new System.Drawing.Point(12, 57);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 40);
            this.btnReset.TabIndex = 22;
            this.ToolTip1.SetToolTip(this.btnReset, "RESET the current run");
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSplit
            // 
            this.btnSplit.BackColor = System.Drawing.Color.LightGreen;
            this.btnSplit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSplit.Image = global::HitCounterManager.Properties.Resources.icons8_staircase_32;
            this.btnSplit.Location = new System.Drawing.Point(485, 57);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(75, 40);
            this.btnSplit.TabIndex = 24;
            this.ToolTip1.SetToolTip(this.btnSplit, "Jump to the next SPLIT");
            this.btnSplit.UseVisualStyleBackColor = false;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // Spacer1
            // 
            this.Spacer1.AutoSize = true;
            this.Spacer1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Spacer1.Location = new System.Drawing.Point(117, 8);
            this.Spacer1.Name = "Spacer1";
            this.Spacer1.Size = new System.Drawing.Size(9, 13);
            this.Spacer1.TabIndex = 40;
            this.Spacer1.Text = "|";
            // 
            // lbl_totals
            // 
            this.lbl_totals.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_totals.Location = new System.Drawing.Point(234, 100);
            this.lbl_totals.Name = "lbl_totals";
            this.lbl_totals.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lbl_totals.Size = new System.Drawing.Size(326, 20);
            this.lbl_totals.TabIndex = 34;
            this.lbl_totals.Text = "Total: ??? Hits   ??? PB";
            this.lbl_totals.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_progress
            // 
            this.lbl_progress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_progress.Location = new System.Drawing.Point(11, 100);
            this.lbl_progress.Name = "lbl_progress";
            this.lbl_progress.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lbl_progress.Size = new System.Drawing.Size(219, 20);
            this.lbl_progress.TabIndex = 33;
            this.lbl_progress.Text = "Progress:  ?? / ??  # ???";
            this.lbl_progress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // cPB
            // 
            this.cPB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.NullValue = "0";
            this.cPB.DefaultCellStyle = dataGridViewCellStyle1;
            this.cPB.HeaderText = "PB";
            this.cPB.MinimumWidth = 30;
            this.cPB.Name = "cPB";
            this.cPB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cPB.ToolTipText = "Person best";
            this.cPB.Width = 50;
            // 
            // cDiff
            // 
            this.cDiff.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.NullValue = "0";
            this.cDiff.DefaultCellStyle = dataGridViewCellStyle2;
            this.cDiff.HeaderText = "Diff";
            this.cDiff.MinimumWidth = 30;
            this.cDiff.Name = "cDiff";
            this.cDiff.ReadOnly = true;
            this.cDiff.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cDiff.ToolTipText = "Difference between Hits and PB";
            this.cDiff.Width = 50;
            // 
            // cHits
            // 
            this.cHits.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.NullValue = "0";
            this.cHits.DefaultCellStyle = dataGridViewCellStyle3;
            this.cHits.HeaderText = "Hits";
            this.cHits.MinimumWidth = 30;
            this.cHits.Name = "cHits";
            this.cHits.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cHits.ToolTipText = "Counted hits";
            this.cHits.Width = 50;
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
            // ComboBox1
            // 
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Location = new System.Drawing.Point(12, 30);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(548, 21);
            this.ComboBox1.Sorted = true;
            this.ComboBox1.TabIndex = 28;
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToResizeRows = false;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cTitle,
            this.cHits,
            this.cDiff,
            this.cPB,
            this.cSP});
            this.DataGridView1.Location = new System.Drawing.Point(13, 123);
            this.DataGridView1.MultiSelect = false;
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.Size = new System.Drawing.Size(547, 241);
            this.DataGridView1.TabIndex = 25;
            this.DataGridView1.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseUp);
            this.DataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DataGridView1_CellValidating);
            this.DataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellValueChanged);
            this.DataGridView1.SelectionChanged += new System.EventHandler(this.DataGridView1_SelectionChanged);
            this.DataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DataGridView1_KeyUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 376);
            this.Controls.Add(this.btnAttempts);
            this.Controls.Add(this.Spacer2);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnWeb);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.Spacer1);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.lbl_totals);
            this.Controls.Add(this.lbl_progress);
            this.Controls.Add(this.btnPB);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnHit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.DataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "Form1";
            this.Text = "HitCounterManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnAttempts;
        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.Label Spacer2;
        internal System.Windows.Forms.Button btnAbout;
        internal System.Windows.Forms.Button btnWeb;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnSettings;
        internal System.Windows.Forms.Button btnCopy;
        internal System.Windows.Forms.Button btnPB;
        internal System.Windows.Forms.Button btnDelete;
        internal System.Windows.Forms.Button btnRename;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button btnUp;
        internal System.Windows.Forms.Button btnDown;
        internal System.Windows.Forms.Button btnHit;
        internal System.Windows.Forms.Button btnReset;
        internal System.Windows.Forms.Button btnSplit;
        internal System.Windows.Forms.Label Spacer1;
        internal System.Windows.Forms.Label lbl_totals;
        internal System.Windows.Forms.Label lbl_progress;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn cSP;
        internal System.Windows.Forms.DataGridViewTextBoxColumn cPB;
        internal System.Windows.Forms.DataGridViewTextBoxColumn cDiff;
        internal System.Windows.Forms.DataGridViewTextBoxColumn cHits;
        internal System.Windows.Forms.DataGridViewTextBoxColumn cTitle;
        internal System.Windows.Forms.ComboBox ComboBox1;
        internal System.Windows.Forms.DataGridView DataGridView1;
    }
}