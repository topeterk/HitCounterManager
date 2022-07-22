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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnTeamHitlessHispano = new System.Windows.Forms.Button();
            this.btnSplitter = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnWeb = new System.Windows.Forms.Button();
            this.btnTeamHitless = new System.Windows.Forms.Button();
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
            this.btnDeleteSplit = new System.Windows.Forms.Button();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnOnTop = new System.Windows.Forms.Button();
            this.btnDarkMode = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnPB = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnHit = new System.Windows.Forms.Button();
            this.btnWayHit = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.Spacer1 = new System.Windows.Forms.Label();
            this.Spacer2 = new System.Windows.Forms.Label();
            this.Spacer3 = new System.Windows.Forms.Label();
            this.lbl_totals = new System.Windows.Forms.Label();
            this.lbl_progress = new System.Windows.Forms.Label();
            this.lbl_time = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxGame = new System.Windows.Forms.ComboBox();
            this.profCtrl = new HitCounterManager.ProfilesControl();
            this.SuspendLayout();
            // 
            // ToolTip1
            // 
            this.ToolTip1.AutoPopDelay = 15000;
            this.ToolTip1.InitialDelay = 500;
            this.ToolTip1.ReshowDelay = 100;
            // 
            // btnTeamHitlessHispano
            // 
            this.btnTeamHitlessHispano.BackColor = System.Drawing.SystemColors.Control;
            this.btnTeamHitlessHispano.BackgroundImage = global::HitCounterManager.Sources.Resources._20p_logo_black_THH;
            this.btnTeamHitlessHispano.FlatAppearance.BorderSize = 0;
            this.btnTeamHitlessHispano.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeamHitlessHispano.Location = new System.Drawing.Point(117, 4);
            this.btnTeamHitlessHispano.Name = "btnTeamHitlessHispano";
            this.btnTeamHitlessHispano.Size = new System.Drawing.Size(20, 20);
            this.btnTeamHitlessHispano.TabIndex = 47;
            this.ToolTip1.SetToolTip(this.btnTeamHitlessHispano, "No Hit Hispano Discord\r\n(Spanish Community all about no hit runs)");
            this.btnTeamHitlessHispano.UseVisualStyleBackColor = false;
            this.btnTeamHitlessHispano.Click += new System.EventHandler(this.btnTeamHitlessHispano_Click);
            // 
            // btnSplitter
            // 
            this.btnSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSplitter.BackColor = System.Drawing.Color.Teal;
            this.btnSplitter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSplitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSplitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSplitter.Image = global::HitCounterManager.Sources.Resources.icons8_autosplitter_32;
            this.btnSplitter.Location = new System.Drawing.Point(607, 30);
            this.btnSplitter.Name = "btnSplitter";
            this.btnSplitter.Size = new System.Drawing.Size(75, 40);
            this.btnSplitter.TabIndex = 45;
            this.ToolTip1.SetToolTip(this.btnSplitter, "AutoSplitter Configuration");
            this.btnSplitter.UseVisualStyleBackColor = false;
            this.btnSplitter.Click += new System.EventHandler(this.btnSplitter_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.SystemColors.Control;
            this.btnSettings.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_settings_20;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Location = new System.Drawing.Point(13, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(20, 20);
            this.btnSettings.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.btnSettings, "Settings");
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_save_20;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(39, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(20, 20);
            this.btnSave.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.btnSave, "Save");
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnWeb
            // 
            this.btnWeb.BackColor = System.Drawing.SystemColors.Control;
            this.btnWeb.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_website_20;
            this.btnWeb.FlatAppearance.BorderSize = 0;
            this.btnWeb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeb.Location = new System.Drawing.Point(65, 4);
            this.btnWeb.Name = "btnWeb";
            this.btnWeb.Size = new System.Drawing.Size(20, 20);
            this.btnWeb.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.btnWeb, "Help / Website");
            this.btnWeb.UseVisualStyleBackColor = false;
            this.btnWeb.Click += new System.EventHandler(this.btnWeb_Click);
            // 
            // btnTeamHitless
            // 
            this.btnTeamHitless.BackColor = System.Drawing.SystemColors.Control;
            this.btnTeamHitless.BackgroundImage = global::HitCounterManager.Sources.Resources._20p_logo_black;
            this.btnTeamHitless.FlatAppearance.BorderSize = 0;
            this.btnTeamHitless.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeamHitless.Location = new System.Drawing.Point(91, 4);
            this.btnTeamHitless.Name = "btnTeamHitless";
            this.btnTeamHitless.Size = new System.Drawing.Size(20, 20);
            this.btnTeamHitless.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.btnTeamHitless, "Team Hitless Discord\r\n(Community all about no hit runs)");
            this.btnTeamHitless.UseVisualStyleBackColor = false;
            this.btnTeamHitless.Click += new System.EventHandler(this.btnTeamHitless_Click);
            // 
            // btnCheckVersion
            // 
            this.btnCheckVersion.BackColor = System.Drawing.SystemColors.Control;
            this.btnCheckVersion.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_cloud_20;
            this.btnCheckVersion.FlatAppearance.BorderSize = 0;
            this.btnCheckVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckVersion.Location = new System.Drawing.Point(146, 4);
            this.btnCheckVersion.Name = "btnCheckVersion";
            this.btnCheckVersion.Size = new System.Drawing.Size(20, 20);
            this.btnCheckVersion.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.btnCheckVersion, "Check for a new version");
            this.btnCheckVersion.UseVisualStyleBackColor = false;
            this.btnCheckVersion.Click += new System.EventHandler(this.btnCheckVersion_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.BackColor = System.Drawing.SystemColors.Control;
            this.btnAbout.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_about_20;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Location = new System.Drawing.Point(172, 4);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(20, 20);
            this.btnAbout.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.btnAbout, "About");
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.SystemColors.Control;
            this.btnNew.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_add_20;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Location = new System.Drawing.Point(213, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(20, 20);
            this.btnNew.TabIndex = 6;
            this.ToolTip1.SetToolTip(this.btnNew, "New profile");
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnRename
            // 
            this.btnRename.BackColor = System.Drawing.SystemColors.Control;
            this.btnRename.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_edit_20;
            this.btnRename.FlatAppearance.BorderSize = 0;
            this.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRename.Location = new System.Drawing.Point(239, 4);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(20, 20);
            this.btnRename.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.btnRename, "Rename profile");
            this.btnRename.UseVisualStyleBackColor = false;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.SystemColors.Control;
            this.btnCopy.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_copy_20;
            this.btnCopy.FlatAppearance.BorderSize = 0;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Location = new System.Drawing.Point(265, 4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(20, 20);
            this.btnCopy.TabIndex = 8;
            this.ToolTip1.SetToolTip(this.btnCopy, "Copy profile");
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_trash_20;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(291, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(20, 20);
            this.btnDelete.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.btnDelete, "Delete profile");
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAttempts
            // 
            this.btnAttempts.BackColor = System.Drawing.SystemColors.Control;
            this.btnAttempts.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_counter_20;
            this.btnAttempts.FlatAppearance.BorderSize = 0;
            this.btnAttempts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttempts.Location = new System.Drawing.Point(317, 4);
            this.btnAttempts.Name = "btnAttempts";
            this.btnAttempts.Size = new System.Drawing.Size(20, 20);
            this.btnAttempts.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.btnAttempts, "Set run number (amount of attempts) manually");
            this.btnAttempts.UseVisualStyleBackColor = false;
            this.btnAttempts.Click += new System.EventHandler(this.btnAttempts_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.SystemColors.Control;
            this.btnUp.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_scroll_up_20;
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Location = new System.Drawing.Point(358, 4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(20, 20);
            this.btnUp.TabIndex = 11;
            this.ToolTip1.SetToolTip(this.btnUp, "Move selected split UP");
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.SystemColors.Control;
            this.btnDown.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_scroll_down_20;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(384, 4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(20, 20);
            this.btnDown.TabIndex = 12;
            this.ToolTip1.SetToolTip(this.btnDown, "Move selected split DOWN");
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnInsertSplit
            // 
            this.btnInsertSplit.BackColor = System.Drawing.SystemColors.Control;
            this.btnInsertSplit.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_add_list_20;
            this.btnInsertSplit.FlatAppearance.BorderSize = 0;
            this.btnInsertSplit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsertSplit.Location = new System.Drawing.Point(410, 4);
            this.btnInsertSplit.Name = "btnInsertSplit";
            this.btnInsertSplit.Size = new System.Drawing.Size(20, 20);
            this.btnInsertSplit.TabIndex = 13;
            this.ToolTip1.SetToolTip(this.btnInsertSplit, "Insert new split above selected");
            this.btnInsertSplit.UseVisualStyleBackColor = false;
            this.btnInsertSplit.Click += new System.EventHandler(this.BtnInsertSplit_Click);
            // 
            // btnDeleteSplit
            // 
            this.btnDeleteSplit.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteSplit.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_delete_list_20;
            this.btnDeleteSplit.FlatAppearance.BorderSize = 0;
            this.btnDeleteSplit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSplit.Location = new System.Drawing.Point(436, 4);
            this.btnDeleteSplit.Name = "btnDeleteSplit";
            this.btnDeleteSplit.Size = new System.Drawing.Size(20, 20);
            this.btnDeleteSplit.TabIndex = 14;
            this.ToolTip1.SetToolTip(this.btnDeleteSplit, "Delete current split");
            this.btnDeleteSplit.UseVisualStyleBackColor = false;
            this.btnDeleteSplit.Click += new System.EventHandler(this.btnDeleteSplit_Click);
            // 
            // btnLock
            // 
            this.btnLock.BackColor = System.Drawing.SystemColors.Control;
            this.btnLock.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_padlock_20;
            this.btnLock.FlatAppearance.BorderSize = 0;
            this.btnLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLock.Location = new System.Drawing.Point(477, 4);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(20, 20);
            this.btnLock.TabIndex = 15;
            this.ToolTip1.SetToolTip(this.btnLock, "(Un)lock split/profile editing");
            this.btnLock.UseVisualStyleBackColor = false;
            this.btnLock.Click += new System.EventHandler(this.BtnSplitLock_Click);
            // 
            // btnOnTop
            // 
            this.btnOnTop.BackColor = System.Drawing.SystemColors.Control;
            this.btnOnTop.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_pin_20;
            this.btnOnTop.FlatAppearance.BorderSize = 0;
            this.btnOnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnTop.Location = new System.Drawing.Point(503, 4);
            this.btnOnTop.Name = "btnOnTop";
            this.btnOnTop.Size = new System.Drawing.Size(20, 20);
            this.btnOnTop.TabIndex = 16;
            this.ToolTip1.SetToolTip(this.btnOnTop, "Set window always on top");
            this.btnOnTop.UseVisualStyleBackColor = false;
            this.btnOnTop.Click += new System.EventHandler(this.BtnOnTop_Click);
            // 
            // btnDarkMode
            // 
            this.btnDarkMode.BackColor = System.Drawing.SystemColors.Control;
            this.btnDarkMode.BackgroundImage = global::HitCounterManager.Sources.Resources.icons8_taschenlampe_20;
            this.btnDarkMode.FlatAppearance.BorderSize = 0;
            this.btnDarkMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDarkMode.Location = new System.Drawing.Point(529, 4);
            this.btnDarkMode.Name = "btnDarkMode";
            this.btnDarkMode.Size = new System.Drawing.Size(20, 20);
            this.btnDarkMode.TabIndex = 17;
            this.ToolTip1.SetToolTip(this.btnDarkMode, "Dark mode / Light mode");
            this.btnDarkMode.UseVisualStyleBackColor = false;
            this.btnDarkMode.Click += new System.EventHandler(this.btnDarkMode_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Salmon;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Image = global::HitCounterManager.Sources.Resources.icons8_repeat_one_32;
            this.btnReset.Location = new System.Drawing.Point(12, 30);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 40);
            this.btnReset.TabIndex = 18;
            this.ToolTip1.SetToolTip(this.btnReset, "RESET the current run\r\nAlso stops and resets the timer");
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnPB
            // 
            this.btnPB.BackColor = System.Drawing.Color.LightYellow;
            this.btnPB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPB.Image = global::HitCounterManager.Sources.Resources.icons8_trophy_32;
            this.btnPB.Location = new System.Drawing.Point(93, 30);
            this.btnPB.Name = "btnPB";
            this.btnPB.Size = new System.Drawing.Size(75, 40);
            this.btnPB.TabIndex = 19;
            this.ToolTip1.SetToolTip(this.btnPB, "Record run as PB (personal best)\r\nAlso stops the timer\r\nDoes NOT reset the timer\r" +
        "\n");
            this.btnPB.UseVisualStyleBackColor = false;
            this.btnPB.Click += new System.EventHandler(this.btnPB_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Gold;
            this.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Image = global::HitCounterManager.Sources.Resources.icons8_time_32;
            this.btnPause.Location = new System.Drawing.Point(174, 30);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 40);
            this.btnPause.TabIndex = 20;
            this.ToolTip1.SetToolTip(this.btnPause, "Start/stop timer\r\nDoes NOT reset the timer");
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnHit
            // 
            this.btnHit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHit.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnHit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnHit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHit.Image = global::HitCounterManager.Sources.Resources.icons8_attack_32;
            this.btnHit.Location = new System.Drawing.Point(255, 30);
            this.btnHit.Name = "btnHit";
            this.btnHit.Size = new System.Drawing.Size(184, 40);
            this.btnHit.TabIndex = 21;
            this.ToolTip1.SetToolTip(this.btnHit, "Count a HIT (boss) on the current split");
            this.btnHit.UseVisualStyleBackColor = false;
            this.btnHit.Click += new System.EventHandler(this.btnHit_Click);
            this.btnHit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnHit_MouseDown);
            // 
            // btnWayHit
            // 
            this.btnWayHit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWayHit.BackColor = System.Drawing.Color.LightBlue;
            this.btnWayHit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnWayHit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWayHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWayHit.Image = global::HitCounterManager.Sources.Resources.icons8_watch_your_step_32;
            this.btnWayHit.Location = new System.Drawing.Point(445, 30);
            this.btnWayHit.Name = "btnWayHit";
            this.btnWayHit.Size = new System.Drawing.Size(75, 40);
            this.btnWayHit.TabIndex = 22;
            this.ToolTip1.SetToolTip(this.btnWayHit, "Count a HIT (way) on the current split");
            this.btnWayHit.UseVisualStyleBackColor = false;
            this.btnWayHit.Click += new System.EventHandler(this.btnWayHit_Click);
            this.btnWayHit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnWayHit_MouseDown);
            // 
            // btnSplit
            // 
            this.btnSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSplit.BackColor = System.Drawing.Color.LightGreen;
            this.btnSplit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSplit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSplit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSplit.Image = global::HitCounterManager.Sources.Resources.icons8_staircase_32;
            this.btnSplit.Location = new System.Drawing.Point(526, 30);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(75, 40);
            this.btnSplit.TabIndex = 23;
            this.ToolTip1.SetToolTip(this.btnSplit, "Jump to the next SPLIT");
            this.btnSplit.UseVisualStyleBackColor = false;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            this.btnSplit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSplit_MouseDown);
            // 
            // Spacer1
            // 
            this.Spacer1.AutoSize = true;
            this.Spacer1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Spacer1.Location = new System.Drawing.Point(198, 8);
            this.Spacer1.Name = "Spacer1";
            this.Spacer1.Size = new System.Drawing.Size(9, 13);
            this.Spacer1.TabIndex = 40;
            this.Spacer1.Text = "|";
            // 
            // Spacer2
            // 
            this.Spacer2.AutoSize = true;
            this.Spacer2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Spacer2.Location = new System.Drawing.Point(343, 8);
            this.Spacer2.Name = "Spacer2";
            this.Spacer2.Size = new System.Drawing.Size(9, 13);
            this.Spacer2.TabIndex = 41;
            this.Spacer2.Text = "|";
            // 
            // Spacer3
            // 
            this.Spacer3.AutoSize = true;
            this.Spacer3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Spacer3.Location = new System.Drawing.Point(462, 8);
            this.Spacer3.Name = "Spacer3";
            this.Spacer3.Size = new System.Drawing.Size(9, 13);
            this.Spacer3.TabIndex = 42;
            this.Spacer3.Text = "|";
            // 
            // lbl_totals
            // 
            this.lbl_totals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_totals.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_totals.Location = new System.Drawing.Point(381, 73);
            this.lbl_totals.Name = "lbl_totals";
            this.lbl_totals.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lbl_totals.Size = new System.Drawing.Size(310, 20);
            this.lbl_totals.TabIndex = 34;
            this.lbl_totals.Text = "Total: ??? Hits   ??? PB";
            this.lbl_totals.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_progress
            // 
            this.lbl_progress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_progress.Location = new System.Drawing.Point(11, 73);
            this.lbl_progress.Name = "lbl_progress";
            this.lbl_progress.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lbl_progress.Size = new System.Drawing.Size(193, 20);
            this.lbl_progress.TabIndex = 33;
            this.lbl_progress.Text = "Progress:  ?? / ??  # ???";
            this.lbl_progress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_time
            // 
            this.lbl_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_time.Location = new System.Drawing.Point(210, 73);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lbl_time.Size = new System.Drawing.Size(165, 20);
            this.lbl_time.TabIndex = 44;
            this.lbl_time.Text = "Time: ??? : ?? : ??";
            this.lbl_time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.ProfileChangedHandler);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 95);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 48;
            this.label1.Text = "Game to Split:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxGame
            // 
            this.comboBoxGame.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxGame.FormattingEnabled = true;
            this.comboBoxGame.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.comboBoxGame.Items.AddRange(new object[] {
            "None",
            "Sekiro",
            "Dark Souls 1",
            "Dark Souls 2",
            "Dark Souls 3",
            "Elden Ring",
            "Hollow Knight"});
            this.comboBoxGame.Location = new System.Drawing.Point(112, 97);
            this.comboBoxGame.Name = "comboBoxGame";
            this.comboBoxGame.Size = new System.Drawing.Size(95, 21);
            this.comboBoxGame.TabIndex = 49;
            this.comboBoxGame.SelectedIndexChanged += new System.EventHandler(this.comboBoxGame_SelectedIndexChanged);
            // 
            // profCtrl
            // 
            this.profCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.profCtrl.Location = new System.Drawing.Point(14, 123);
            this.profCtrl.Name = "profCtrl";
            this.profCtrl.Size = new System.Drawing.Size(677, 376);
            this.profCtrl.TabIndex = 24;
            this.profCtrl.ProfileChanged += new System.EventHandler<System.EventArgs>(this.ProfileChangedHandler);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(703, 511);
            this.Controls.Add(this.comboBoxGame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTeamHitlessHispano);
            this.Controls.Add(this.btnSplitter);
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.profCtrl);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnWeb);
            this.Controls.Add(this.btnTeamHitless);
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
            this.Controls.Add(this.btnDeleteSplit);
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.btnOnTop);
            this.Controls.Add(this.btnDarkMode);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnPB);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnHit);
            this.Controls.Add(this.btnWayHit);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.Spacer1);
            this.Controls.Add(this.Spacer2);
            this.Controls.Add(this.Spacer3);
            this.Controls.Add(this.lbl_totals);
            this.Controls.Add(this.lbl_progress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 400);
            this.Name = "Form1";
            this.Text = "HitCounterManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ToolTip1;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnWeb;
        private System.Windows.Forms.Button btnTeamHitless;
        private System.Windows.Forms.Button btnCheckVersion;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAttempts;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnInsertSplit;
        private System.Windows.Forms.Button btnDeleteSplit;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnOnTop;
        private System.Windows.Forms.Button btnDarkMode;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnPB;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnHit;
        private System.Windows.Forms.Button btnWayHit;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.Label Spacer1;
        private System.Windows.Forms.Label Spacer2;
        private System.Windows.Forms.Label Spacer3;
        private System.Windows.Forms.Label lbl_totals;
        private System.Windows.Forms.Label lbl_progress;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.Timer timer1;
        public ProfilesControl profCtrl;
        private System.Windows.Forms.Button btnSplitter;
        private System.Windows.Forms.Button btnTeamHitlessHispano;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxGame;
    }
}