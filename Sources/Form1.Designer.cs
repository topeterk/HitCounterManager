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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnWeb = new System.Windows.Forms.Button();
            this.btnCheckVersion = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAttempts = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnInsertSplit = new System.Windows.Forms.Button();
            this.btnOnTop = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnPB = new System.Windows.Forms.Button();
            this.btnHit = new System.Windows.Forms.Button();
            this.btnWayHit = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.btnSuccessionProceed = new System.Windows.Forms.Button();
            this.gpSuccession = new System.Windows.Forms.GroupBox();
            this.btnSuccessionVisibility = new System.Windows.Forms.Button();
            this.lbl_succession_pb = new System.Windows.Forms.Label();
            this.numPB = new System.Windows.Forms.NumericUpDown();
            this.lbl_succession_hitsway = new System.Windows.Forms.Label();
            this.numHitsWay = new System.Windows.Forms.NumericUpDown();
            this.lbl_succession_hits = new System.Windows.Forms.Label();
            this.numHits = new System.Windows.Forms.NumericUpDown();
            this.txtPredecessorTitle = new System.Windows.Forms.TextBox();
            this.cbShowPredecessor = new System.Windows.Forms.CheckBox();
            this.Spacer1 = new System.Windows.Forms.Label();
            this.Spacer2 = new System.Windows.Forms.Label();
            this.Spacer3 = new System.Windows.Forms.Label();
            this.lbl_totals = new System.Windows.Forms.Label();
            this.lbl_progress = new System.Windows.Forms.Label();
            this.cTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cHits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cWayHits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDiff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.DataGridView1 = new HitCounterManager.ProfileDataGridView();
            this.gpSuccession.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHitsWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSettings
            // 
            this.btnSettings.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_settings_20;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Location = new System.Drawing.Point(13, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(20, 20);
            this.btnSettings.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.btnSettings, "Settings");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_save_20;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(39, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(20, 20);
            this.btnSave.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.btnSave, "Save");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnWeb
            // 
            this.btnWeb.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_website_20;
            this.btnWeb.FlatAppearance.BorderSize = 0;
            this.btnWeb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeb.Location = new System.Drawing.Point(65, 4);
            this.btnWeb.Name = "btnWeb";
            this.btnWeb.Size = new System.Drawing.Size(20, 20);
            this.btnWeb.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.btnWeb, "Help / Website");
            this.btnWeb.UseVisualStyleBackColor = true;
            this.btnWeb.Click += new System.EventHandler(this.btnWeb_Click);
            // 
            // btnCheckVersion
            // 
            this.btnCheckVersion.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_cloud_20;
            this.btnCheckVersion.FlatAppearance.BorderSize = 0;
            this.btnCheckVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckVersion.Location = new System.Drawing.Point(91, 4);
            this.btnCheckVersion.Name = "btnCheckVersion";
            this.btnCheckVersion.Size = new System.Drawing.Size(20, 20);
            this.btnCheckVersion.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.btnCheckVersion, "Check for a new version");
            this.btnCheckVersion.UseVisualStyleBackColor = true;
            this.btnCheckVersion.Click += new System.EventHandler(this.btnCheckVersion_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_about_20;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Location = new System.Drawing.Point(117, 4);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(20, 20);
            this.btnAbout.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.btnAbout, "About");
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_add_20;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Location = new System.Drawing.Point(158, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(20, 20);
            this.btnNew.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.btnNew, "New profile");
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnRename
            // 
            this.btnRename.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_edit_20;
            this.btnRename.FlatAppearance.BorderSize = 0;
            this.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRename.Location = new System.Drawing.Point(184, 4);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(20, 20);
            this.btnRename.TabIndex = 6;
            this.ToolTip1.SetToolTip(this.btnRename, "Rename profile");
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_copy_20;
            this.btnCopy.FlatAppearance.BorderSize = 0;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Location = new System.Drawing.Point(210, 4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(20, 20);
            this.btnCopy.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.btnCopy, "Copy profile");
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_trash_20;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(236, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(20, 20);
            this.btnDelete.TabIndex = 8;
            this.ToolTip1.SetToolTip(this.btnDelete, "Delete profile");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAttempts
            // 
            this.btnAttempts.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_counter_20;
            this.btnAttempts.FlatAppearance.BorderSize = 0;
            this.btnAttempts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttempts.Location = new System.Drawing.Point(277, 4);
            this.btnAttempts.Name = "btnAttempts";
            this.btnAttempts.Size = new System.Drawing.Size(20, 20);
            this.btnAttempts.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.btnAttempts, "Set run number (amount of attempts) manually");
            this.btnAttempts.UseVisualStyleBackColor = true;
            this.btnAttempts.Click += new System.EventHandler(this.btnAttempts_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_scroll_up_20;
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Location = new System.Drawing.Point(303, 4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(20, 20);
            this.btnUp.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.btnUp, "Move selected split UP");
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_scroll_down_20;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(329, 4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(20, 20);
            this.btnDown.TabIndex = 11;
            this.ToolTip1.SetToolTip(this.btnDown, "Move selected split DOWN");
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnInsertSplit
            // 
            this.btnInsertSplit.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_add_list_20;
            this.btnInsertSplit.FlatAppearance.BorderSize = 0;
            this.btnInsertSplit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsertSplit.Location = new System.Drawing.Point(355, 4);
            this.btnInsertSplit.Name = "btnInsertSplit";
            this.btnInsertSplit.Size = new System.Drawing.Size(20, 20);
            this.btnInsertSplit.TabIndex = 12;
            this.ToolTip1.SetToolTip(this.btnInsertSplit, "Insert new split above selected");
            this.btnInsertSplit.UseVisualStyleBackColor = true;
            this.btnInsertSplit.Click += new System.EventHandler(this.BtnInsertSplit_Click);
            // 
            // btnOnTop
            // 
            this.btnOnTop.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_pin_20;
            this.btnOnTop.FlatAppearance.BorderSize = 0;
            this.btnOnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnTop.Location = new System.Drawing.Point(396, 4);
            this.btnOnTop.Name = "btnOnTop";
            this.btnOnTop.Size = new System.Drawing.Size(20, 20);
            this.btnOnTop.TabIndex = 13;
            this.ToolTip1.SetToolTip(this.btnOnTop, "Set window always on top");
            this.btnOnTop.UseVisualStyleBackColor = true;
            this.btnOnTop.Click += new System.EventHandler(this.BtnOnTop_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Salmon;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Image = global::HitCounterManager.Sources.Resources.icons8_repeat_one_32;
            this.btnReset.Location = new System.Drawing.Point(12, 57);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 40);
            this.btnReset.TabIndex = 15;
            this.ToolTip1.SetToolTip(this.btnReset, "RESET the current run");
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnPB
            // 
            this.btnPB.BackColor = System.Drawing.Color.LightYellow;
            this.btnPB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPB.Image = global::HitCounterManager.Sources.Resources.icons8_trophy_32;
            this.btnPB.Location = new System.Drawing.Point(93, 57);
            this.btnPB.Name = "btnPB";
            this.btnPB.Size = new System.Drawing.Size(75, 40);
            this.btnPB.TabIndex = 16;
            this.ToolTip1.SetToolTip(this.btnPB, "Record run as PB (personal best)");
            this.btnPB.UseVisualStyleBackColor = false;
            this.btnPB.Click += new System.EventHandler(this.btnPB_Click);
            // 
            // btnHit
            // 
            this.btnHit.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHit.Image = global::HitCounterManager.Sources.Resources.icons8_attack_32;
            this.btnHit.Location = new System.Drawing.Point(174, 57);
            this.btnHit.Name = "btnHit";
            this.btnHit.Size = new System.Drawing.Size(274, 40);
            this.btnHit.TabIndex = 17;
            this.ToolTip1.SetToolTip(this.btnHit, "Count a HIT (boss) on the current split");
            this.btnHit.UseVisualStyleBackColor = false;
            this.btnHit.Click += new System.EventHandler(this.btnHit_Click);
            // 
            // btnWayHit
            // 
            this.btnWayHit.BackColor = System.Drawing.Color.LightBlue;
            this.btnWayHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWayHit.Image = global::HitCounterManager.Sources.Resources.icons8_watch_your_step_32;
            this.btnWayHit.Location = new System.Drawing.Point(454, 57);
            this.btnWayHit.Name = "btnWayHit";
            this.btnWayHit.Size = new System.Drawing.Size(75, 40);
            this.btnWayHit.TabIndex = 18;
            this.ToolTip1.SetToolTip(this.btnWayHit, "Count a HIT (way) on the current split");
            this.btnWayHit.UseVisualStyleBackColor = false;
            this.btnWayHit.Click += new System.EventHandler(this.btnWayHit_Click);
            // 
            // btnSplit
            // 
            this.btnSplit.BackColor = System.Drawing.Color.LightGreen;
            this.btnSplit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSplit.Image = global::HitCounterManager.Sources.Resources.icons8_staircase_32;
            this.btnSplit.Location = new System.Drawing.Point(535, 57);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(75, 40);
            this.btnSplit.TabIndex = 19;
            this.ToolTip1.SetToolTip(this.btnSplit, "Jump to the next SPLIT");
            this.btnSplit.UseVisualStyleBackColor = false;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // btnSuccessionProceed
            // 
            this.btnSuccessionProceed.BackColor = System.Drawing.Color.LightPink;
            this.btnSuccessionProceed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuccessionProceed.Image = global::HitCounterManager.Sources.Resources.icons8_tasklist_32;
            this.btnSuccessionProceed.Location = new System.Drawing.Point(616, 57);
            this.btnSuccessionProceed.Name = "btnSuccessionProceed";
            this.btnSuccessionProceed.Size = new System.Drawing.Size(75, 40);
            this.btnSuccessionProceed.TabIndex = 20;
            this.ToolTip1.SetToolTip(this.btnSuccessionProceed, "At the end of your run: Click once to save succession and continue with your next" +
        " profile");
            this.btnSuccessionProceed.UseVisualStyleBackColor = false;
            this.btnSuccessionProceed.Click += new System.EventHandler(this.BtnSuccessionProceed_Click);
            // 
            // gpSuccession
            // 
            this.gpSuccession.Controls.Add(this.btnSuccessionVisibility);
            this.gpSuccession.Controls.Add(this.lbl_succession_pb);
            this.gpSuccession.Controls.Add(this.numPB);
            this.gpSuccession.Controls.Add(this.lbl_succession_hitsway);
            this.gpSuccession.Controls.Add(this.numHitsWay);
            this.gpSuccession.Controls.Add(this.lbl_succession_hits);
            this.gpSuccession.Controls.Add(this.numHits);
            this.gpSuccession.Controls.Add(this.txtPredecessorTitle);
            this.gpSuccession.Controls.Add(this.cbShowPredecessor);
            this.gpSuccession.Location = new System.Drawing.Point(12, 384);
            this.gpSuccession.Name = "gpSuccession";
            this.gpSuccession.Size = new System.Drawing.Size(679, 79);
            this.gpSuccession.TabIndex = 22;
            this.gpSuccession.TabStop = false;
            this.gpSuccession.Text = "Succession";
            this.ToolTip1.SetToolTip(this.gpSuccession, "E.g. Dark Souls trilogy run");
            // 
            // btnSuccessionVisibility
            // 
            this.btnSuccessionVisibility.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_double_up_20;
            this.btnSuccessionVisibility.FlatAppearance.BorderSize = 0;
            this.btnSuccessionVisibility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuccessionVisibility.Location = new System.Drawing.Point(653, 0);
            this.btnSuccessionVisibility.Name = "btnSuccessionVisibility";
            this.btnSuccessionVisibility.Size = new System.Drawing.Size(20, 20);
            this.btnSuccessionVisibility.TabIndex = 6;
            this.ToolTip1.SetToolTip(this.btnSuccessionVisibility, "Collapse Succession");
            this.btnSuccessionVisibility.UseVisualStyleBackColor = true;
            this.btnSuccessionVisibility.Click += new System.EventHandler(this.btnSuccessionVisibility_Click);
            // 
            // lbl_succession_pb
            // 
            this.lbl_succession_pb.AutoSize = true;
            this.lbl_succession_pb.Location = new System.Drawing.Point(270, 54);
            this.lbl_succession_pb.Name = "lbl_succession_pb";
            this.lbl_succession_pb.Size = new System.Drawing.Size(24, 13);
            this.lbl_succession_pb.TabIndex = 19;
            this.lbl_succession_pb.Text = "PB:";
            // 
            // numPB
            // 
            this.numPB.Location = new System.Drawing.Point(300, 52);
            this.numPB.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numPB.Name = "numPB";
            this.numPB.Size = new System.Drawing.Size(60, 20);
            this.numPB.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.numPB, "Hits (Boss) collected in previous runs");
            this.numPB.ValueChanged += new System.EventHandler(this.SuccessionChanged);
            // 
            // lbl_succession_hitsway
            // 
            this.lbl_succession_hitsway.AutoSize = true;
            this.lbl_succession_hitsway.Location = new System.Drawing.Point(138, 54);
            this.lbl_succession_hitsway.Name = "lbl_succession_hitsway";
            this.lbl_succession_hitsway.Size = new System.Drawing.Size(59, 13);
            this.lbl_succession_hitsway.TabIndex = 7;
            this.lbl_succession_hitsway.Text = "Hits (Way):";
            // 
            // numHitsWay
            // 
            this.numHitsWay.Location = new System.Drawing.Point(204, 52);
            this.numHitsWay.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numHitsWay.Name = "numHitsWay";
            this.numHitsWay.Size = new System.Drawing.Size(60, 20);
            this.numHitsWay.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.numHitsWay, "Hits (Boss) collected in previous runs");
            this.numHitsWay.ValueChanged += new System.EventHandler(this.SuccessionChanged);
            // 
            // lbl_succession_hits
            // 
            this.lbl_succession_hits.AutoSize = true;
            this.lbl_succession_hits.Location = new System.Drawing.Point(6, 54);
            this.lbl_succession_hits.Name = "lbl_succession_hits";
            this.lbl_succession_hits.Size = new System.Drawing.Size(60, 13);
            this.lbl_succession_hits.TabIndex = 5;
            this.lbl_succession_hits.Text = "Hits (Boss):";
            // 
            // numHits
            // 
            this.numHits.Location = new System.Drawing.Point(72, 52);
            this.numHits.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numHits.Name = "numHits";
            this.numHits.Size = new System.Drawing.Size(60, 20);
            this.numHits.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.numHits, "Hits (Boss) collected in previous runs");
            this.numHits.ValueChanged += new System.EventHandler(this.SuccessionChanged);
            // 
            // txtPredecessorTitle
            // 
            this.txtPredecessorTitle.Location = new System.Drawing.Point(109, 26);
            this.txtPredecessorTitle.Name = "txtPredecessorTitle";
            this.txtPredecessorTitle.Size = new System.Drawing.Size(251, 20);
            this.txtPredecessorTitle.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.txtPredecessorTitle, "Name of the precessor split");
            this.txtPredecessorTitle.TextChanged += new System.EventHandler(this.SuccessionChanged);
            // 
            // cbShowPredecessor
            // 
            this.cbShowPredecessor.AutoSize = true;
            this.cbShowPredecessor.Location = new System.Drawing.Point(6, 28);
            this.cbShowPredecessor.Name = "cbShowPredecessor";
            this.cbShowPredecessor.Size = new System.Drawing.Size(97, 17);
            this.cbShowPredecessor.TabIndex = 1;
            this.cbShowPredecessor.Text = "Show with title:";
            this.ToolTip1.SetToolTip(this.cbShowPredecessor, "Uncheck: No succession or just started, Check: Show predecessor during succession" +
        "");
            this.cbShowPredecessor.UseVisualStyleBackColor = true;
            this.cbShowPredecessor.CheckedChanged += new System.EventHandler(this.SuccessionChanged);
            // 
            // Spacer1
            // 
            this.Spacer1.AutoSize = true;
            this.Spacer1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Spacer1.Location = new System.Drawing.Point(143, 8);
            this.Spacer1.Name = "Spacer1";
            this.Spacer1.Size = new System.Drawing.Size(9, 13);
            this.Spacer1.TabIndex = 40;
            this.Spacer1.Text = "|";
            // 
            // Spacer2
            // 
            this.Spacer2.AutoSize = true;
            this.Spacer2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Spacer2.Location = new System.Drawing.Point(262, 8);
            this.Spacer2.Name = "Spacer2";
            this.Spacer2.Size = new System.Drawing.Size(9, 13);
            this.Spacer2.TabIndex = 41;
            this.Spacer2.Text = "|";
            // 
            // Spacer3
            // 
            this.Spacer3.AutoSize = true;
            this.Spacer3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Spacer3.Location = new System.Drawing.Point(381, 8);
            this.Spacer3.Name = "Spacer3";
            this.Spacer3.Size = new System.Drawing.Size(9, 13);
            this.Spacer3.TabIndex = 42;
            this.Spacer3.Text = "|";
            // 
            // lbl_totals
            // 
            this.lbl_totals.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_totals.Location = new System.Drawing.Point(236, 100);
            this.lbl_totals.Name = "lbl_totals";
            this.lbl_totals.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lbl_totals.Size = new System.Drawing.Size(455, 20);
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
            // ComboBox1
            // 
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Location = new System.Drawing.Point(12, 30);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(679, 21);
            this.ComboBox1.Sorted = true;
            this.ComboBox1.TabIndex = 14;
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToResizeRows = false;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cTitle,
            this.cHits,
            this.cWayHits,
            this.cDiff,
            this.cPB,
            this.cSP});
            this.DataGridView1.Location = new System.Drawing.Point(13, 123);
            this.DataGridView1.MultiSelect = false;
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.Size = new System.Drawing.Size(678, 254);
            this.DataGridView1.TabIndex = 21;
            this.DataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellValueChanged);
            this.DataGridView1.SelectionChanged += new System.EventHandler(this.DataGridView1_SelectionChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 475);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnWeb);
            this.Controls.Add(this.btnCheckVersion);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAttempts);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnInsertSplit);
            this.Controls.Add(this.btnOnTop);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnPB);
            this.Controls.Add(this.btnHit);
            this.Controls.Add(this.btnWayHit);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.btnSuccessionProceed);
            this.Controls.Add(this.Spacer1);
            this.Controls.Add(this.Spacer2);
            this.Controls.Add(this.Spacer3);
            this.Controls.Add(this.lbl_totals);
            this.Controls.Add(this.lbl_progress);
            this.Controls.Add(this.gpSuccession);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.DataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(530, 400);
            this.Name = "Form1";
            this.Text = "HitCounterManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.gpSuccession.ResumeLayout(false);
            this.gpSuccession.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHitsWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.Button btnSettings;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnWeb;
        internal System.Windows.Forms.Button btnCheckVersion;
        internal System.Windows.Forms.Button btnAbout;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.Button btnRename;
        internal System.Windows.Forms.Button btnCopy;
        internal System.Windows.Forms.Button btnDelete;
        internal System.Windows.Forms.Button btnAttempts;
        internal System.Windows.Forms.Button btnUp;
        internal System.Windows.Forms.Button btnDown;
        internal System.Windows.Forms.Button btnInsertSplit;
        internal System.Windows.Forms.Button btnOnTop;
        internal System.Windows.Forms.Button btnReset;
        internal System.Windows.Forms.Button btnPB;
        internal System.Windows.Forms.Button btnHit;
        internal System.Windows.Forms.Button btnWayHit;
        internal System.Windows.Forms.Button btnSplit;
        internal System.Windows.Forms.Button btnSuccessionProceed;
        internal System.Windows.Forms.Label Spacer1;
        internal System.Windows.Forms.Label Spacer2;
        internal System.Windows.Forms.Label Spacer3;
        internal System.Windows.Forms.Label lbl_totals;
        internal System.Windows.Forms.Label lbl_progress;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn cHits;
        private System.Windows.Forms.DataGridViewTextBoxColumn cWayHits;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDiff;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPB;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSP;
        private System.Windows.Forms.GroupBox gpSuccession;
        internal System.Windows.Forms.Button btnSuccessionVisibility;
        private System.Windows.Forms.CheckBox cbShowPredecessor;
        private System.Windows.Forms.TextBox txtPredecessorTitle;
        private System.Windows.Forms.Label lbl_succession_hits;
        private System.Windows.Forms.NumericUpDown numHits;
        private System.Windows.Forms.Label lbl_succession_hitsway;
        private System.Windows.Forms.NumericUpDown numHitsWay;
        private System.Windows.Forms.Label lbl_succession_pb;
        private System.Windows.Forms.NumericUpDown numPB;
        internal System.Windows.Forms.ComboBox ComboBox1;
        internal ProfileDataGridView DataGridView1;
    }
}