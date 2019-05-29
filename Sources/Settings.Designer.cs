namespace HitCounterManager
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.cbShowSessionProgress = new System.Windows.Forms.CheckBox();
            this.cbShowAttempts = new System.Windows.Forms.CheckBox();
            this.cbShowHeadline = new System.Windows.Forms.CheckBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.numShowSplitsCountUpcoming = new System.Windows.Forms.NumericUpDown();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.numShowSplitsCountFinished = new System.Windows.Forms.NumericUpDown();
            this.Label3 = new System.Windows.Forms.Label();
            this.tab_globalshortcuts = new System.Windows.Forms.TabPage();
            this.txtPB = new System.Windows.Forms.TextBox();
            this.cbScPB = new System.Windows.Forms.CheckBox();
            this.txtWayHitUndo = new System.Windows.Forms.TextBox();
            this.txtWayHit = new System.Windows.Forms.TextBox();
            this.txtHitUndo = new System.Windows.Forms.TextBox();
            this.txtPrevSplit = new System.Windows.Forms.TextBox();
            this.txtNextSplit = new System.Windows.Forms.TextBox();
            this.txtHit = new System.Windows.Forms.TextBox();
            this.txtReset = new System.Windows.Forms.TextBox();
            this.cbScWayHitUndo = new System.Windows.Forms.CheckBox();
            this.cbScWayHit = new System.Windows.Forms.CheckBox();
            this.cbScHitUndo = new System.Windows.Forms.CheckBox();
            this.cbScPrevSplit = new System.Windows.Forms.CheckBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.radioHotKeyMethod_async = new System.Windows.Forms.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.radioHotKeyMethod_sync = new System.Windows.Forms.RadioButton();
            this.cbScReset = new System.Windows.Forms.CheckBox();
            this.cbScHit = new System.Windows.Forms.CheckBox();
            this.cbScNextSplit = new System.Windows.Forms.CheckBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tab_behavior = new System.Windows.Forms.TabPage();
            this.tab_filepaths = new System.Windows.Forms.TabPage();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnInput = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.tab_style = new System.Windows.Forms.TabPage();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtFontName = new System.Windows.Forms.TextBox();
            this.cbApCustomCss = new System.Windows.Forms.CheckBox();
            this.txtCssUrl = new System.Windows.Forms.TextBox();
            this.txtFontUrl = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.btnApApply = new System.Windows.Forms.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.cbApHighContrast = new System.Windows.Forms.CheckBox();
            this.numStyleDesiredWidth = new System.Windows.Forms.NumericUpDown();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.cbShowNumbers = new System.Windows.Forms.CheckBox();
            this.cbShowHitsCombined = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioSeverityAnyHitCritical = new System.Windows.Forms.RadioButton();
            this.radioSeverityComparePB = new System.Windows.Forms.RadioButton();
            this.radioSeverityBossHitCritical = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioPurposeSplitCounter = new System.Windows.Forms.RadioButton();
            this.radioPurposeDeathCounter = new System.Windows.Forms.RadioButton();
            this.radioPurposeChecklist = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numShowSplitsCountUpcoming)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShowSplitsCountFinished)).BeginInit();
            this.tab_globalshortcuts.SuspendLayout();
            this.tab_behavior.SuspendLayout();
            this.tab_filepaths.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.tab_style.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStyleDesiredWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbShowSessionProgress
            // 
            this.cbShowSessionProgress.AutoSize = true;
            this.cbShowSessionProgress.Checked = true;
            this.cbShowSessionProgress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowSessionProgress.Location = new System.Drawing.Point(6, 65);
            this.cbShowSessionProgress.Name = "cbShowSessionProgress";
            this.cbShowSessionProgress.Size = new System.Drawing.Size(134, 17);
            this.cbShowSessionProgress.TabIndex = 2;
            this.cbShowSessionProgress.Text = "Show session progress";
            this.ToolTip1.SetToolTip(this.cbShowSessionProgress, "Displays the icon to represent the farthest split since last time the application" +
        " started");
            this.cbShowSessionProgress.UseVisualStyleBackColor = true;
            this.cbShowSessionProgress.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // cbShowAttempts
            // 
            this.cbShowAttempts.AutoSize = true;
            this.cbShowAttempts.Checked = true;
            this.cbShowAttempts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowAttempts.Location = new System.Drawing.Point(6, 19);
            this.cbShowAttempts.Name = "cbShowAttempts";
            this.cbShowAttempts.Size = new System.Drawing.Size(135, 17);
            this.cbShowAttempts.TabIndex = 0;
            this.cbShowAttempts.Text = "Show attempts counter";
            this.ToolTip1.SetToolTip(this.cbShowAttempts, "Displays how many runs have been done yet");
            this.cbShowAttempts.UseVisualStyleBackColor = true;
            this.cbShowAttempts.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // cbShowHeadline
            // 
            this.cbShowHeadline.AutoSize = true;
            this.cbShowHeadline.Checked = true;
            this.cbShowHeadline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowHeadline.Location = new System.Drawing.Point(6, 42);
            this.cbShowHeadline.Name = "cbShowHeadline";
            this.cbShowHeadline.Size = new System.Drawing.Size(96, 17);
            this.cbShowHeadline.TabIndex = 1;
            this.cbShowHeadline.Text = "Show headline";
            this.ToolTip1.SetToolTip(this.cbShowHeadline, "Displays the headlines of the columns");
            this.cbShowHeadline.UseVisualStyleBackColor = true;
            this.cbShowHeadline.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(150, 186);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(82, 13);
            this.Label9.TabIndex = 5;
            this.Label9.Text = "upcoming splits.";
            // 
            // numShowSplitsCountUpcoming
            // 
            this.numShowSplitsCountUpcoming.Location = new System.Drawing.Point(87, 184);
            this.numShowSplitsCountUpcoming.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numShowSplitsCountUpcoming.Name = "numShowSplitsCountUpcoming";
            this.numShowSplitsCountUpcoming.Size = new System.Drawing.Size(57, 20);
            this.numShowSplitsCountUpcoming.TabIndex = 6;
            this.ToolTip1.SetToolTip(this.numShowSplitsCountUpcoming, "Hide splits that are coming later to spare space");
            this.numShowSplitsCountUpcoming.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numShowSplitsCountUpcoming.ValueChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(6, 186);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(75, 13);
            this.Label10.TabIndex = 3;
            this.Label10.Text = "Show the next";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(150, 160);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(72, 13);
            this.Label8.TabIndex = 2;
            this.Label8.Text = "finished splits.";
            // 
            // numShowSplitsCountFinished
            // 
            this.numShowSplitsCountFinished.Location = new System.Drawing.Point(87, 158);
            this.numShowSplitsCountFinished.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numShowSplitsCountFinished.Name = "numShowSplitsCountFinished";
            this.numShowSplitsCountFinished.Size = new System.Drawing.Size(57, 20);
            this.numShowSplitsCountFinished.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.numShowSplitsCountFinished, "Hide splits that are done already to spare space");
            this.numShowSplitsCountFinished.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numShowSplitsCountFinished.ValueChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(6, 160);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(71, 13);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Show the last";
            // 
            // tab_globalshortcuts
            // 
            this.tab_globalshortcuts.Controls.Add(this.txtPB);
            this.tab_globalshortcuts.Controls.Add(this.cbScPB);
            this.tab_globalshortcuts.Controls.Add(this.txtWayHitUndo);
            this.tab_globalshortcuts.Controls.Add(this.txtWayHit);
            this.tab_globalshortcuts.Controls.Add(this.txtHitUndo);
            this.tab_globalshortcuts.Controls.Add(this.txtPrevSplit);
            this.tab_globalshortcuts.Controls.Add(this.txtNextSplit);
            this.tab_globalshortcuts.Controls.Add(this.txtHit);
            this.tab_globalshortcuts.Controls.Add(this.txtReset);
            this.tab_globalshortcuts.Controls.Add(this.cbScWayHitUndo);
            this.tab_globalshortcuts.Controls.Add(this.cbScWayHit);
            this.tab_globalshortcuts.Controls.Add(this.cbScHitUndo);
            this.tab_globalshortcuts.Controls.Add(this.cbScPrevSplit);
            this.tab_globalshortcuts.Controls.Add(this.Label6);
            this.tab_globalshortcuts.Controls.Add(this.radioHotKeyMethod_async);
            this.tab_globalshortcuts.Controls.Add(this.Label1);
            this.tab_globalshortcuts.Controls.Add(this.radioHotKeyMethod_sync);
            this.tab_globalshortcuts.Controls.Add(this.cbScReset);
            this.tab_globalshortcuts.Controls.Add(this.cbScHit);
            this.tab_globalshortcuts.Controls.Add(this.cbScNextSplit);
            this.tab_globalshortcuts.Location = new System.Drawing.Point(4, 22);
            this.tab_globalshortcuts.Name = "tab_globalshortcuts";
            this.tab_globalshortcuts.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.tab_globalshortcuts.Size = new System.Drawing.Size(588, 235);
            this.tab_globalshortcuts.TabIndex = 0;
            this.tab_globalshortcuts.Text = "Global shortcuts";
            this.tab_globalshortcuts.UseVisualStyleBackColor = true;
            // 
            // txtPB
            // 
            this.txtPB.Location = new System.Drawing.Point(85, 108);
            this.txtPB.Name = "txtPB";
            this.txtPB.ReadOnly = true;
            this.txtPB.Size = new System.Drawing.Size(194, 20);
            this.txtPB.TabIndex = 14;
            this.txtPB.Text = "None";
            this.txtPB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtPB, "Click into the field and press the hot key you want to use");
            this.txtPB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPB_KeyDown);
            // 
            // cbScPB
            // 
            this.cbScPB.AutoSize = true;
            this.cbScPB.Location = new System.Drawing.Point(6, 110);
            this.cbScPB.Name = "cbScPB";
            this.cbScPB.Size = new System.Drawing.Size(62, 17);
            this.cbScPB.TabIndex = 13;
            this.cbScPB.Text = "Set PB:";
            this.ToolTip1.SetToolTip(this.cbScPB, "Enable hot key for setting run as personal best");
            this.cbScPB.UseVisualStyleBackColor = true;
            this.cbScPB.CheckedChanged += new System.EventHandler(this.cbScPB_CheckedChanged);
            // 
            // txtWayHitUndo
            // 
            this.txtWayHitUndo.Location = new System.Drawing.Point(384, 55);
            this.txtWayHitUndo.Name = "txtWayHitUndo";
            this.txtWayHitUndo.ReadOnly = true;
            this.txtWayHitUndo.Size = new System.Drawing.Size(194, 20);
            this.txtWayHitUndo.TabIndex = 8;
            this.txtWayHitUndo.Text = "None";
            this.txtWayHitUndo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtWayHitUndo, "Click into the field and press the hot key you want to use");
            this.txtWayHitUndo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtWayHitUndo_KeyDown);
            // 
            // txtWayHit
            // 
            this.txtWayHit.Location = new System.Drawing.Point(85, 56);
            this.txtWayHit.Name = "txtWayHit";
            this.txtWayHit.ReadOnly = true;
            this.txtWayHit.Size = new System.Drawing.Size(194, 20);
            this.txtWayHit.TabIndex = 6;
            this.txtWayHit.Text = "None";
            this.txtWayHit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtWayHit, "Click into the field and press the hot key you want to use");
            this.txtWayHit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtWayHit_KeyDown);
            // 
            // txtHitUndo
            // 
            this.txtHitUndo.Location = new System.Drawing.Point(384, 30);
            this.txtHitUndo.Name = "txtHitUndo";
            this.txtHitUndo.ReadOnly = true;
            this.txtHitUndo.Size = new System.Drawing.Size(194, 20);
            this.txtHitUndo.TabIndex = 4;
            this.txtHitUndo.Text = "None";
            this.txtHitUndo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtHitUndo, "Click into the field and press the hot key you want to use");
            this.txtHitUndo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHitUndo_KeyDown);
            // 
            // txtPrevSplit
            // 
            this.txtPrevSplit.Location = new System.Drawing.Point(384, 82);
            this.txtPrevSplit.Name = "txtPrevSplit";
            this.txtPrevSplit.ReadOnly = true;
            this.txtPrevSplit.Size = new System.Drawing.Size(194, 20);
            this.txtPrevSplit.TabIndex = 12;
            this.txtPrevSplit.Text = "None";
            this.txtPrevSplit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtPrevSplit, "Click into the field and press the hot key you want to use");
            this.txtPrevSplit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrevSplit_KeyDown);
            // 
            // txtNextSplit
            // 
            this.txtNextSplit.Location = new System.Drawing.Point(85, 82);
            this.txtNextSplit.Name = "txtNextSplit";
            this.txtNextSplit.ReadOnly = true;
            this.txtNextSplit.Size = new System.Drawing.Size(194, 20);
            this.txtNextSplit.TabIndex = 10;
            this.txtNextSplit.Text = "None";
            this.txtNextSplit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtNextSplit, "Click into the field and press the hot key you want to use");
            this.txtNextSplit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNextSplit_KeyDown);
            // 
            // txtHit
            // 
            this.txtHit.Location = new System.Drawing.Point(85, 30);
            this.txtHit.Name = "txtHit";
            this.txtHit.ReadOnly = true;
            this.txtHit.Size = new System.Drawing.Size(194, 20);
            this.txtHit.TabIndex = 2;
            this.txtHit.Text = "None";
            this.txtHit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtHit, "Click into the field and press the hot key you want to use");
            this.txtHit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHit_KeyDown);
            // 
            // txtReset
            // 
            this.txtReset.Location = new System.Drawing.Point(384, 108);
            this.txtReset.Name = "txtReset";
            this.txtReset.ReadOnly = true;
            this.txtReset.Size = new System.Drawing.Size(194, 20);
            this.txtReset.TabIndex = 16;
            this.txtReset.Text = "None";
            this.txtReset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtReset, "Click into the field and press the hot key you want to use");
            this.txtReset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReset_KeyDown);
            // 
            // cbScWayHitUndo
            // 
            this.cbScWayHitUndo.AutoSize = true;
            this.cbScWayHitUndo.Location = new System.Drawing.Point(285, 58);
            this.cbScWayHitUndo.Name = "cbScWayHitUndo";
            this.cbScWayHitUndo.Size = new System.Drawing.Size(97, 17);
            this.cbScWayHitUndo.TabIndex = 7;
            this.cbScWayHitUndo.Text = "Undo hit (way):";
            this.ToolTip1.SetToolTip(this.cbScWayHitUndo, "Enable hot key for undoing hits (way)");
            this.cbScWayHitUndo.UseVisualStyleBackColor = true;
            this.cbScWayHitUndo.CheckedChanged += new System.EventHandler(this.CbScWayHitUndo_CheckedChanged);
            // 
            // cbScWayHit
            // 
            this.cbScWayHit.AutoSize = true;
            this.cbScWayHit.Location = new System.Drawing.Point(6, 58);
            this.cbScWayHit.Name = "cbScWayHit";
            this.cbScWayHit.Size = new System.Drawing.Size(73, 17);
            this.cbScWayHit.TabIndex = 5;
            this.cbScWayHit.Text = "Hit (Way):";
            this.ToolTip1.SetToolTip(this.cbScWayHit, "Enable hot key for getting hit (way)");
            this.cbScWayHit.UseVisualStyleBackColor = true;
            this.cbScWayHit.CheckedChanged += new System.EventHandler(this.CbScWayHit_CheckedChanged);
            // 
            // cbScHitUndo
            // 
            this.cbScHitUndo.AutoSize = true;
            this.cbScHitUndo.Location = new System.Drawing.Point(285, 32);
            this.cbScHitUndo.Name = "cbScHitUndo";
            this.cbScHitUndo.Size = new System.Drawing.Size(100, 17);
            this.cbScHitUndo.TabIndex = 3;
            this.cbScHitUndo.Text = "Undo hit (boss):";
            this.ToolTip1.SetToolTip(this.cbScHitUndo, "Enable hot key for undoing hits (boss)");
            this.cbScHitUndo.UseVisualStyleBackColor = true;
            this.cbScHitUndo.CheckedChanged += new System.EventHandler(this.cbScHitUndo_CheckedChanged);
            // 
            // cbScPrevSplit
            // 
            this.cbScPrevSplit.AutoSize = true;
            this.cbScPrevSplit.Location = new System.Drawing.Point(285, 84);
            this.cbScPrevSplit.Name = "cbScPrevSplit";
            this.cbScPrevSplit.Size = new System.Drawing.Size(72, 17);
            this.cbScPrevSplit.TabIndex = 11;
            this.cbScPrevSplit.Text = "Prev split:";
            this.ToolTip1.SetToolTip(this.cbScPrevSplit, "Enable hot key for entering previous split");
            this.cbScPrevSplit.UseVisualStyleBackColor = true;
            this.cbScPrevSplit.CheckedChanged += new System.EventHandler(this.cbScPrevSplit_CheckedChanged);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(3, 151);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(393, 13);
            this.Label6.TabIndex = 9;
            this.Label6.Text = "Select method of global hotkey registration (changing needs restart of applicatio" +
    "n):";
            // 
            // radioHotKeyMethod_async
            // 
            this.radioHotKeyMethod_async.AutoSize = true;
            this.radioHotKeyMethod_async.Checked = true;
            this.radioHotKeyMethod_async.Location = new System.Drawing.Point(6, 193);
            this.radioHotKeyMethod_async.Name = "radioHotKeyMethod_async";
            this.radioHotKeyMethod_async.Size = new System.Drawing.Size(201, 17);
            this.radioHotKeyMethod_async.TabIndex = 18;
            this.radioHotKeyMethod_async.TabStop = true;
            this.radioHotKeyMethod_async.Text = "Asynchronous - *should* always work";
            this.ToolTip1.SetToolTip(this.radioHotKeyMethod_async, "Method that should always work, however the syncronous is the safer method and sh" +
        "ould be preferred if possible");
            this.radioHotKeyMethod_async.UseVisualStyleBackColor = true;
            this.radioHotKeyMethod_async.CheckedChanged += new System.EventHandler(this.radioHotKeyMethod_CheckedChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(6, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(462, 13);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Click into a textbox and press any key combination to setup your hotkey for the r" +
    "espective event:";
            // 
            // radioHotKeyMethod_sync
            // 
            this.radioHotKeyMethod_sync.AutoSize = true;
            this.radioHotKeyMethod_sync.Location = new System.Drawing.Point(6, 170);
            this.radioHotKeyMethod_sync.Name = "radioHotKeyMethod_sync";
            this.radioHotKeyMethod_sync.Size = new System.Drawing.Size(477, 17);
            this.radioHotKeyMethod_sync.TabIndex = 17;
            this.radioHotKeyMethod_sync.Text = "Synchronous - Safer, but may not always work (recommendation: test and keep it wh" +
    "en it works)";
            this.ToolTip1.SetToolTip(this.radioHotKeyMethod_sync, "Safer method, but may not always work (recommendation: test and keep it when it w" +
        "orks)");
            this.radioHotKeyMethod_sync.UseVisualStyleBackColor = true;
            this.radioHotKeyMethod_sync.CheckedChanged += new System.EventHandler(this.radioHotKeyMethod_CheckedChanged);
            // 
            // cbScReset
            // 
            this.cbScReset.AutoSize = true;
            this.cbScReset.Location = new System.Drawing.Point(285, 110);
            this.cbScReset.Name = "cbScReset";
            this.cbScReset.Size = new System.Drawing.Size(57, 17);
            this.cbScReset.TabIndex = 15;
            this.cbScReset.Text = "Reset:";
            this.ToolTip1.SetToolTip(this.cbScReset, "Enable hot key for resetting the run");
            this.cbScReset.UseVisualStyleBackColor = true;
            this.cbScReset.CheckedChanged += new System.EventHandler(this.cbScReset_CheckedChanged);
            // 
            // cbScHit
            // 
            this.cbScHit.AutoSize = true;
            this.cbScHit.Location = new System.Drawing.Point(6, 32);
            this.cbScHit.Name = "cbScHit";
            this.cbScHit.Size = new System.Drawing.Size(73, 17);
            this.cbScHit.TabIndex = 1;
            this.cbScHit.Text = "Hit (boss):";
            this.ToolTip1.SetToolTip(this.cbScHit, "Enable hot key for getting hit (boss)");
            this.cbScHit.UseVisualStyleBackColor = true;
            this.cbScHit.CheckedChanged += new System.EventHandler(this.cbScHit_CheckedChanged);
            // 
            // cbScNextSplit
            // 
            this.cbScNextSplit.AutoSize = true;
            this.cbScNextSplit.Location = new System.Drawing.Point(6, 84);
            this.cbScNextSplit.Name = "cbScNextSplit";
            this.cbScNextSplit.Size = new System.Drawing.Size(72, 17);
            this.cbScNextSplit.TabIndex = 9;
            this.cbScNextSplit.Text = "Next split:";
            this.ToolTip1.SetToolTip(this.cbScNextSplit, "Enable hot key for entering next split");
            this.cbScNextSplit.UseVisualStyleBackColor = true;
            this.cbScNextSplit.CheckedChanged += new System.EventHandler(this.cbScNextSplit_CheckedChanged);
            // 
            // tab_behavior
            // 
            this.tab_behavior.Controls.Add(this.groupBox5);
            this.tab_behavior.Controls.Add(this.groupBox4);
            this.tab_behavior.Controls.Add(this.groupBox2);
            this.tab_behavior.Location = new System.Drawing.Point(4, 22);
            this.tab_behavior.Name = "tab_behavior";
            this.tab_behavior.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.tab_behavior.Size = new System.Drawing.Size(588, 235);
            this.tab_behavior.TabIndex = 1;
            this.tab_behavior.Text = "Behavior";
            this.tab_behavior.UseVisualStyleBackColor = true;
            // 
            // tab_filepaths
            // 
            this.tab_filepaths.Controls.Add(this.Label7);
            this.tab_filepaths.Controls.Add(this.Label5);
            this.tab_filepaths.Controls.Add(this.btnOutput);
            this.tab_filepaths.Controls.Add(this.txtInput);
            this.tab_filepaths.Controls.Add(this.Label4);
            this.tab_filepaths.Controls.Add(this.Label2);
            this.tab_filepaths.Controls.Add(this.txtOutput);
            this.tab_filepaths.Controls.Add(this.btnInput);
            this.tab_filepaths.Location = new System.Drawing.Point(4, 22);
            this.tab_filepaths.Name = "tab_filepaths";
            this.tab_filepaths.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.tab_filepaths.Size = new System.Drawing.Size(588, 235);
            this.tab_filepaths.TabIndex = 2;
            this.tab_filepaths.Text = "Filepaths";
            this.tab_filepaths.UseVisualStyleBackColor = true;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(6, 159);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(561, 39);
            this.Label7.TabIndex = 14;
            this.Label7.Text = resources.GetString("Label7.Text");
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(6, 10);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(473, 78);
            this.Label5.TabIndex = 7;
            this.Label5.Text = resources.GetString("Label5.Text");
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(508, 123);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(74, 23);
            this.btnOutput.TabIndex = 11;
            this.btnOutput.Text = "Search";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(70, 99);
            this.txtInput.Name = "txtInput";
            this.txtInput.ReadOnly = true;
            this.txtInput.Size = new System.Drawing.Size(432, 20);
            this.txtInput.TabIndex = 6;
            this.txtInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(6, 128);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(58, 13);
            this.Label4.TabIndex = 10;
            this.Label4.Text = "Output file:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(6, 102);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(50, 13);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Input file:";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(70, 125);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(432, 20);
            this.txtOutput.TabIndex = 9;
            this.txtOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(508, 97);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(74, 23);
            this.btnInput.TabIndex = 8;
            this.btnInput.Text = "Search";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "HitCounter*";
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.tab_globalshortcuts);
            this.TabControl1.Controls.Add(this.tab_style);
            this.TabControl1.Controls.Add(this.tab_behavior);
            this.TabControl1.Controls.Add(this.tab_filepaths);
            this.TabControl1.Location = new System.Drawing.Point(12, 12);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(596, 261);
            this.TabControl1.TabIndex = 10;
            // 
            // tab_style
            // 
            this.tab_style.Controls.Add(this.GroupBox1);
            this.tab_style.Controls.Add(this.cbApHighContrast);
            this.tab_style.Controls.Add(this.numStyleDesiredWidth);
            this.tab_style.Controls.Add(this.Label13);
            this.tab_style.Controls.Add(this.Label14);
            this.tab_style.Location = new System.Drawing.Point(4, 22);
            this.tab_style.Name = "tab_style";
            this.tab_style.Size = new System.Drawing.Size(588, 235);
            this.tab_style.TabIndex = 3;
            this.tab_style.Text = "Style";
            this.tab_style.UseVisualStyleBackColor = true;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.label15);
            this.GroupBox1.Controls.Add(this.txtFontName);
            this.GroupBox1.Controls.Add(this.cbApCustomCss);
            this.GroupBox1.Controls.Add(this.txtCssUrl);
            this.GroupBox1.Controls.Add(this.txtFontUrl);
            this.GroupBox1.Controls.Add(this.Label11);
            this.GroupBox1.Controls.Add(this.btnApApply);
            this.GroupBox1.Controls.Add(this.Label12);
            this.GroupBox1.Location = new System.Drawing.Point(6, 106);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(571, 126);
            this.GroupBox1.TabIndex = 3;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Customization";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 71);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 13);
            this.label15.TabIndex = 17;
            this.label15.Text = "Font name:";
            // 
            // txtFontName
            // 
            this.txtFontName.Enabled = false;
            this.txtFontName.Location = new System.Drawing.Point(74, 68);
            this.txtFontName.Name = "txtFontName";
            this.txtFontName.Size = new System.Drawing.Size(222, 20);
            this.txtFontName.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.txtFontName, resources.GetString("txtFontName.ToolTip"));
            // 
            // cbApCustomCss
            // 
            this.cbApCustomCss.AutoSize = true;
            this.cbApCustomCss.Location = new System.Drawing.Point(6, 19);
            this.cbApCustomCss.Name = "cbApCustomCss";
            this.cbApCustomCss.Size = new System.Drawing.Size(177, 17);
            this.cbApCustomCss.TabIndex = 0;
            this.cbApCustomCss.Text = "Use custom stylesheet and font:";
            this.ToolTip1.SetToolTip(this.cbApCustomCss, "Use a custom stylesheet and font for a personalized look and feel");
            this.cbApCustomCss.UseVisualStyleBackColor = true;
            // 
            // txtCssUrl
            // 
            this.txtCssUrl.Enabled = false;
            this.txtCssUrl.Location = new System.Drawing.Point(74, 42);
            this.txtCssUrl.Name = "txtCssUrl";
            this.txtCssUrl.Size = new System.Drawing.Size(422, 20);
            this.txtCssUrl.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.txtCssUrl, "Personalize the design by switching to another stylesheet");
            // 
            // txtFontUrl
            // 
            this.txtFontUrl.Enabled = false;
            this.txtFontUrl.Location = new System.Drawing.Point(74, 94);
            this.txtFontUrl.Name = "txtFontUrl";
            this.txtFontUrl.Size = new System.Drawing.Size(422, 20);
            this.txtFontUrl.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.txtFontUrl, "Personalize the design by switching to another font.\r\nKeep the field empty if you" +
        " do not use an external font.\r\nOtherwise specify the path to a @font-face ressou" +
        "rce.");
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(6, 45);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(62, 13);
            this.Label11.TabIndex = 11;
            this.Label11.Text = "CSS: (URL)";
            // 
            // btnApApply
            // 
            this.btnApApply.BackColor = System.Drawing.Color.LightYellow;
            this.btnApApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnApApply.Location = new System.Drawing.Point(502, 42);
            this.btnApApply.Name = "btnApApply";
            this.btnApApply.Size = new System.Drawing.Size(55, 72);
            this.btnApApply.TabIndex = 4;
            this.btnApApply.Text = "Apply";
            this.ToolTip1.SetToolTip(this.btnApApply, "To prevent issues with loading incomplete URLs, please click the Apply button man" +
        "ually after editing.");
            this.btnApApply.UseVisualStyleBackColor = false;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(6, 97);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(62, 13);
            this.Label12.TabIndex = 12;
            this.Label12.Text = "Font: (URL)";
            // 
            // cbApHighContrast
            // 
            this.cbApHighContrast.AutoSize = true;
            this.cbApHighContrast.Location = new System.Drawing.Point(6, 13);
            this.cbApHighContrast.Name = "cbApHighContrast";
            this.cbApHighContrast.Size = new System.Drawing.Size(245, 17);
            this.cbApHighContrast.TabIndex = 0;
            this.cbApHighContrast.Text = "Use high contrast mode (Enables background)";
            this.ToolTip1.SetToolTip(this.cbApHighContrast, "Change design (like removing transparency, other colors, fatter text) for better " +
        "readability");
            this.cbApHighContrast.UseVisualStyleBackColor = true;
            // 
            // numStyleDesiredWidth
            // 
            this.numStyleDesiredWidth.Location = new System.Drawing.Point(496, 12);
            this.numStyleDesiredWidth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numStyleDesiredWidth.Name = "numStyleDesiredWidth";
            this.numStyleDesiredWidth.Size = new System.Drawing.Size(57, 20);
            this.numStyleDesiredWidth.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.numStyleDesiredWidth, resources.GetString("numStyleDesiredWidth.ToolTip"));
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(559, 14);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(18, 13);
            this.Label13.TabIndex = 20;
            this.Label13.Text = "px";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(415, 14);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(74, 13);
            this.Label14.TabIndex = 21;
            this.Label14.Text = "Desired width:";
            // 
            // cbShowNumbers
            // 
            this.cbShowNumbers.AutoSize = true;
            this.cbShowNumbers.Checked = true;
            this.cbShowNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowNumbers.Location = new System.Drawing.Point(6, 111);
            this.cbShowNumbers.Name = "cbShowNumbers";
            this.cbShowNumbers.Size = new System.Drawing.Size(150, 17);
            this.cbShowNumbers.TabIndex = 4;
            this.cbShowNumbers.Text = "Show numbers, no images";
            this.ToolTip1.SetToolTip(this.cbShowNumbers, "Show the actual hit count instead of images");
            this.cbShowNumbers.UseVisualStyleBackColor = true;
            this.cbShowNumbers.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // cbShowHitsCombined
            // 
            this.cbShowHitsCombined.AutoSize = true;
            this.cbShowHitsCombined.Checked = true;
            this.cbShowHitsCombined.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowHitsCombined.Location = new System.Drawing.Point(6, 88);
            this.cbShowHitsCombined.Name = "cbShowHitsCombined";
            this.cbShowHitsCombined.Size = new System.Drawing.Size(206, 17);
            this.cbShowHitsCombined.TabIndex = 3;
            this.cbShowHitsCombined.Text = "Show hits combined (bosses and way)";
            this.ToolTip1.SetToolTip(this.cbShowHitsCombined, "Show boss and way hits either separately or combined");
            this.cbShowHitsCombined.UseVisualStyleBackColor = true;
            this.cbShowHitsCombined.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioSeverityBossHitCritical);
            this.groupBox2.Controls.Add(this.radioSeverityComparePB);
            this.groupBox2.Controls.Add(this.radioSeverityAnyHitCritical);
            this.groupBox2.Location = new System.Drawing.Point(6, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 101);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hit severity (color) within a single split";
            // 
            // radioSeverityAnyHitCritical
            // 
            this.radioSeverityAnyHitCritical.AutoSize = true;
            this.radioSeverityAnyHitCritical.Checked = true;
            this.radioSeverityAnyHitCritical.Location = new System.Drawing.Point(7, 20);
            this.radioSeverityAnyHitCritical.Name = "radioSeverityAnyHitCritical";
            this.radioSeverityAnyHitCritical.Size = new System.Drawing.Size(104, 17);
            this.radioSeverityAnyHitCritical.TabIndex = 0;
            this.radioSeverityAnyHitCritical.TabStop = true;
            this.radioSeverityAnyHitCritical.Text = "Any hit as critical";
            this.ToolTip1.SetToolTip(this.radioSeverityAnyHitCritical, "Any hit counts as critical hit");
            this.radioSeverityAnyHitCritical.UseVisualStyleBackColor = true;
            this.radioSeverityAnyHitCritical.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // radioSeverityComparePB
            // 
            this.radioSeverityComparePB.AutoSize = true;
            this.radioSeverityComparePB.Location = new System.Drawing.Point(6, 66);
            this.radioSeverityComparePB.Name = "radioSeverityComparePB";
            this.radioSeverityComparePB.Size = new System.Drawing.Size(178, 17);
            this.radioSeverityComparePB.TabIndex = 2;
            this.radioSeverityComparePB.Text = "Compare with personal best (PB)";
            this.ToolTip1.SetToolTip(this.radioSeverityComparePB, "Any hit till PB is reached count as normal hit, any above as critical hits");
            this.radioSeverityComparePB.UseVisualStyleBackColor = true;
            this.radioSeverityComparePB.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // radioSeverityBossHitCritical
            // 
            this.radioSeverityBossHitCritical.AutoSize = true;
            this.radioSeverityBossHitCritical.Location = new System.Drawing.Point(6, 43);
            this.radioSeverityBossHitCritical.Name = "radioSeverityBossHitCritical";
            this.radioSeverityBossHitCritical.Size = new System.Drawing.Size(100, 17);
            this.radioSeverityBossHitCritical.TabIndex = 1;
            this.radioSeverityBossHitCritical.Text = "Boss hits critical";
            this.ToolTip1.SetToolTip(this.radioSeverityBossHitCritical, "Boss hits count as critical hits, hits on the way count as normal hits");
            this.radioSeverityBossHitCritical.UseVisualStyleBackColor = true;
            this.radioSeverityBossHitCritical.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioPurposeChecklist);
            this.groupBox4.Controls.Add(this.radioPurposeDeathCounter);
            this.groupBox4.Controls.Add(this.radioPurposeSplitCounter);
            this.groupBox4.Location = new System.Drawing.Point(6, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Purpose";
            // 
            // radioPurposeSplitCounter
            // 
            this.radioPurposeSplitCounter.AutoSize = true;
            this.radioPurposeSplitCounter.Checked = true;
            this.radioPurposeSplitCounter.Location = new System.Drawing.Point(7, 20);
            this.radioPurposeSplitCounter.Name = "radioPurposeSplitCounter";
            this.radioPurposeSplitCounter.Size = new System.Drawing.Size(84, 17);
            this.radioPurposeSplitCounter.TabIndex = 0;
            this.radioPurposeSplitCounter.Text = "Split counter";
            this.radioPurposeSplitCounter.UseVisualStyleBackColor = true;
            this.radioPurposeSplitCounter.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // radioPurposeDeathCounter
            // 
            this.radioPurposeDeathCounter.AutoSize = true;
            this.radioPurposeDeathCounter.Location = new System.Drawing.Point(7, 43);
            this.radioPurposeDeathCounter.Name = "radioPurposeDeathCounter";
            this.radioPurposeDeathCounter.Size = new System.Drawing.Size(93, 17);
            this.radioPurposeDeathCounter.TabIndex = 1;
            this.radioPurposeDeathCounter.Text = "Death counter";
            this.radioPurposeDeathCounter.UseVisualStyleBackColor = true;
            this.radioPurposeDeathCounter.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // radioPurposeChecklist
            // 
            this.radioPurposeChecklist.AutoSize = true;
            this.radioPurposeChecklist.Location = new System.Drawing.Point(7, 66);
            this.radioPurposeChecklist.Name = "radioPurposeChecklist";
            this.radioPurposeChecklist.Size = new System.Drawing.Size(68, 17);
            this.radioPurposeChecklist.TabIndex = 2;
            this.radioPurposeChecklist.Text = "Checklist";
            this.radioPurposeChecklist.UseVisualStyleBackColor = true;
            this.radioPurposeChecklist.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbShowAttempts);
            this.groupBox5.Controls.Add(this.cbShowHeadline);
            this.groupBox5.Controls.Add(this.cbShowSessionProgress);
            this.groupBox5.Controls.Add(this.Label9);
            this.groupBox5.Controls.Add(this.cbShowHitsCombined);
            this.groupBox5.Controls.Add(this.numShowSplitsCountUpcoming);
            this.groupBox5.Controls.Add(this.cbShowNumbers);
            this.groupBox5.Controls.Add(this.Label10);
            this.groupBox5.Controls.Add(this.Label8);
            this.groupBox5.Controls.Add(this.Label3);
            this.groupBox5.Controls.Add(this.numShowSplitsCountFinished);
            this.groupBox5.Location = new System.Drawing.Point(215, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(367, 216);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Appearance";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 285);
            this.Controls.Add(this.TabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numShowSplitsCountUpcoming)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShowSplitsCountFinished)).EndInit();
            this.tab_globalshortcuts.ResumeLayout(false);
            this.tab_globalshortcuts.PerformLayout();
            this.tab_behavior.ResumeLayout(false);
            this.tab_filepaths.ResumeLayout(false);
            this.tab_filepaths.PerformLayout();
            this.TabControl1.ResumeLayout(false);
            this.tab_style.ResumeLayout(false);
            this.tab_style.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStyleDesiredWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.CheckBox cbShowSessionProgress;
        internal System.Windows.Forms.CheckBox cbShowAttempts;
        internal System.Windows.Forms.CheckBox cbShowHeadline;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.NumericUpDown numShowSplitsCountUpcoming;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.NumericUpDown numShowSplitsCountFinished;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TabPage tab_globalshortcuts;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.RadioButton radioHotKeyMethod_async;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.RadioButton radioHotKeyMethod_sync;
        internal System.Windows.Forms.CheckBox cbScReset;
        internal System.Windows.Forms.CheckBox cbScHit;
        internal System.Windows.Forms.TextBox txtNextSplit;
        internal System.Windows.Forms.CheckBox cbScNextSplit;
        internal System.Windows.Forms.TextBox txtHit;
        internal System.Windows.Forms.TextBox txtReset;
        internal System.Windows.Forms.TabPage tab_behavior;
        internal System.Windows.Forms.TabPage tab_filepaths;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Button btnOutput;
        internal System.Windows.Forms.TextBox txtInput;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtOutput;
        internal System.Windows.Forms.Button btnInput;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.TabControl TabControl1;
        internal System.Windows.Forms.TextBox txtPrevSplit;
        internal System.Windows.Forms.CheckBox cbScPrevSplit;
        internal System.Windows.Forms.CheckBox cbScHitUndo;
        internal System.Windows.Forms.TextBox txtHitUndo;
        internal System.Windows.Forms.TextBox txtWayHitUndo;
        internal System.Windows.Forms.CheckBox cbScWayHitUndo;
        internal System.Windows.Forms.TextBox txtWayHit;
        internal System.Windows.Forms.CheckBox cbScWayHit;
        internal System.Windows.Forms.TextBox txtPB;
        internal System.Windows.Forms.CheckBox cbScPB;
        private System.Windows.Forms.TabPage tab_style;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.TextBox txtFontName;
        internal System.Windows.Forms.CheckBox cbApCustomCss;
        internal System.Windows.Forms.TextBox txtCssUrl;
        internal System.Windows.Forms.TextBox txtFontUrl;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Button btnApApply;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.CheckBox cbApHighContrast;
        internal System.Windows.Forms.NumericUpDown numStyleDesiredWidth;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.CheckBox cbShowNumbers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioSeverityBossHitCritical;
        private System.Windows.Forms.RadioButton radioSeverityComparePB;
        private System.Windows.Forms.RadioButton radioSeverityAnyHitCritical;
        internal System.Windows.Forms.CheckBox cbShowHitsCombined;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioPurposeChecklist;
        private System.Windows.Forms.RadioButton radioPurposeDeathCounter;
        private System.Windows.Forms.RadioButton radioPurposeSplitCounter;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}