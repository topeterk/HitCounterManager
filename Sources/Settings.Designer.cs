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
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.cbApCustomCss = new System.Windows.Forms.CheckBox();
            this.txtCssUrl = new System.Windows.Forms.TextBox();
            this.txtFontUrl = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.btnApApply = new System.Windows.Forms.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.cbShowSessionProgress = new System.Windows.Forms.CheckBox();
            this.cbShowAttempts = new System.Windows.Forms.CheckBox();
            this.cbShowHeadline = new System.Windows.Forms.CheckBox();
            this.cbApHighContrast = new System.Windows.Forms.CheckBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.numStyleDesiredWidth = new System.Windows.Forms.NumericUpDown();
            this.numShowSplitsCountUpcoming = new System.Windows.Forms.NumericUpDown();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.numShowSplitsCountFinished = new System.Windows.Forms.NumericUpDown();
            this.Label3 = new System.Windows.Forms.Label();
            this.tab_globalshortcuts = new System.Windows.Forms.TabPage();
            this.Label6 = new System.Windows.Forms.Label();
            this.radioHotKeyMethod_async = new System.Windows.Forms.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.radioHotKeyMethod_sync = new System.Windows.Forms.RadioButton();
            this.cbScReset = new System.Windows.Forms.CheckBox();
            this.cbScHit = new System.Windows.Forms.CheckBox();
            this.txtNextSplit = new System.Windows.Forms.TextBox();
            this.cbScNextSplit = new System.Windows.Forms.CheckBox();
            this.txtHit = new System.Windows.Forms.TextBox();
            this.txtReset = new System.Windows.Forms.TextBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tab_appearance = new System.Windows.Forms.TabPage();
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
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStyleDesiredWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShowSplitsCountUpcoming)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShowSplitsCountFinished)).BeginInit();
            this.tab_globalshortcuts.SuspendLayout();
            this.tab_appearance.SuspendLayout();
            this.tab_filepaths.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.cbApCustomCss);
            this.GroupBox1.Controls.Add(this.txtCssUrl);
            this.GroupBox1.Controls.Add(this.txtFontUrl);
            this.GroupBox1.Controls.Add(this.Label11);
            this.GroupBox1.Controls.Add(this.btnApApply);
            this.GroupBox1.Controls.Add(this.Label12);
            this.GroupBox1.Location = new System.Drawing.Point(10, 103);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(563, 100);
            this.GroupBox1.TabIndex = 19;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Customization";
            // 
            // cbApCustomCss
            // 
            this.cbApCustomCss.AutoSize = true;
            this.cbApCustomCss.Location = new System.Drawing.Point(6, 19);
            this.cbApCustomCss.Name = "cbApCustomCss";
            this.cbApCustomCss.Size = new System.Drawing.Size(177, 17);
            this.cbApCustomCss.TabIndex = 10;
            this.cbApCustomCss.Text = "Use custom stylesheet and font:";
            this.ToolTip1.SetToolTip(this.cbApCustomCss, "Use a custom stylesheet and font for a personalized look and feel");
            this.cbApCustomCss.UseVisualStyleBackColor = true;
            this.cbApCustomCss.CheckedChanged += new System.EventHandler(this.cbApCustomCss_CheckedChanged);
            // 
            // txtCssUrl
            // 
            this.txtCssUrl.Enabled = false;
            this.txtCssUrl.Location = new System.Drawing.Point(71, 42);
            this.txtCssUrl.Name = "txtCssUrl";
            this.txtCssUrl.Size = new System.Drawing.Size(425, 20);
            this.txtCssUrl.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.txtCssUrl, "Personalize the design by switching to another stylesheet");
            // 
            // txtFontUrl
            // 
            this.txtFontUrl.Enabled = false;
            this.txtFontUrl.Location = new System.Drawing.Point(71, 68);
            this.txtFontUrl.Name = "txtFontUrl";
            this.txtFontUrl.Size = new System.Drawing.Size(425, 20);
            this.txtFontUrl.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.txtFontUrl, resources.GetString("txtFontUrl.ToolTip"));
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(3, 45);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(62, 13);
            this.Label11.TabIndex = 11;
            this.Label11.Text = "CSS: (URL)";
            // 
            // btnApApply
            // 
            this.btnApApply.BackColor = System.Drawing.Color.LightYellow;
            this.btnApApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnApApply.Location = new System.Drawing.Point(502, 19);
            this.btnApApply.Name = "btnApApply";
            this.btnApApply.Size = new System.Drawing.Size(55, 69);
            this.btnApApply.TabIndex = 15;
            this.btnApApply.Text = "Apply";
            this.ToolTip1.SetToolTip(this.btnApApply, "To prevent issues with loading incomplete URLs, please apply manually after editi" +
        "ng");
            this.btnApApply.UseVisualStyleBackColor = false;
            this.btnApApply.Click += new System.EventHandler(this.btnApApply_Click);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(3, 71);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(62, 13);
            this.Label12.TabIndex = 12;
            this.Label12.Text = "Font: (URL)";
            // 
            // cbShowSessionProgress
            // 
            this.cbShowSessionProgress.AutoSize = true;
            this.cbShowSessionProgress.Checked = true;
            this.cbShowSessionProgress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowSessionProgress.Location = new System.Drawing.Point(258, 55);
            this.cbShowSessionProgress.Name = "cbShowSessionProgress";
            this.cbShowSessionProgress.Size = new System.Drawing.Size(134, 17);
            this.cbShowSessionProgress.TabIndex = 18;
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
            this.cbShowAttempts.Location = new System.Drawing.Point(258, 9);
            this.cbShowAttempts.Name = "cbShowAttempts";
            this.cbShowAttempts.Size = new System.Drawing.Size(135, 17);
            this.cbShowAttempts.TabIndex = 17;
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
            this.cbShowHeadline.Location = new System.Drawing.Point(258, 32);
            this.cbShowHeadline.Name = "cbShowHeadline";
            this.cbShowHeadline.Size = new System.Drawing.Size(96, 17);
            this.cbShowHeadline.TabIndex = 16;
            this.cbShowHeadline.Text = "Show headline";
            this.ToolTip1.SetToolTip(this.cbShowHeadline, "Displays the headlines of the columns");
            this.cbShowHeadline.UseVisualStyleBackColor = true;
            this.cbShowHeadline.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // cbApHighContrast
            // 
            this.cbApHighContrast.AutoSize = true;
            this.cbApHighContrast.Location = new System.Drawing.Point(258, 78);
            this.cbApHighContrast.Name = "cbApHighContrast";
            this.cbApHighContrast.Size = new System.Drawing.Size(138, 17);
            this.cbApHighContrast.TabIndex = 14;
            this.cbApHighContrast.Text = "Use high contrast mode";
            this.ToolTip1.SetToolTip(this.cbApHighContrast, "Change design (like removing transparency, other colors, fatter text) for better " +
        "readability");
            this.cbApHighContrast.UseVisualStyleBackColor = true;
            this.cbApHighContrast.CheckedChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(151, 36);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(82, 13);
            this.Label9.TabIndex = 5;
            this.Label9.Text = "upcoming splits.";
            // 
            // numStyleDesiredWidth
            // 
            this.numStyleDesiredWidth.Location = new System.Drawing.Point(88, 60);
            this.numStyleDesiredWidth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numStyleDesiredWidth.Name = "numStyleDesiredWidth";
            this.numStyleDesiredWidth.Size = new System.Drawing.Size(57, 20);
            this.numStyleDesiredWidth.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.numStyleDesiredWidth, resources.GetString("numStyleDesiredWidth.ToolTip"));
            this.numStyleDesiredWidth.ValueChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // numShowSplitsCountUpcoming
            // 
            this.numShowSplitsCountUpcoming.Location = new System.Drawing.Point(88, 34);
            this.numShowSplitsCountUpcoming.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numShowSplitsCountUpcoming.Name = "numShowSplitsCountUpcoming";
            this.numShowSplitsCountUpcoming.Size = new System.Drawing.Size(57, 20);
            this.numShowSplitsCountUpcoming.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.numShowSplitsCountUpcoming, "Hide splits that are coming later to spare space");
            this.numShowSplitsCountUpcoming.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numShowSplitsCountUpcoming.ValueChanged += new System.EventHandler(this.ApplyAppearance);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(151, 62);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(18, 13);
            this.Label13.TabIndex = 3;
            this.Label13.Text = "px";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(7, 62);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(74, 13);
            this.Label14.TabIndex = 3;
            this.Label14.Text = "Desired width:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(7, 36);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(75, 13);
            this.Label10.TabIndex = 3;
            this.Label10.Text = "Show the next";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(151, 10);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(72, 13);
            this.Label8.TabIndex = 2;
            this.Label8.Text = "finished splits.";
            // 
            // numShowSplitsCountFinished
            // 
            this.numShowSplitsCountFinished.Location = new System.Drawing.Point(88, 8);
            this.numShowSplitsCountFinished.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numShowSplitsCountFinished.Name = "numShowSplitsCountFinished";
            this.numShowSplitsCountFinished.Size = new System.Drawing.Size(57, 20);
            this.numShowSplitsCountFinished.TabIndex = 1;
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
            this.Label3.Location = new System.Drawing.Point(7, 10);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(71, 13);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Show the last";
            // 
            // tab_globalshortcuts
            // 
            this.tab_globalshortcuts.Controls.Add(this.Label6);
            this.tab_globalshortcuts.Controls.Add(this.radioHotKeyMethod_async);
            this.tab_globalshortcuts.Controls.Add(this.Label1);
            this.tab_globalshortcuts.Controls.Add(this.radioHotKeyMethod_sync);
            this.tab_globalshortcuts.Controls.Add(this.cbScReset);
            this.tab_globalshortcuts.Controls.Add(this.cbScHit);
            this.tab_globalshortcuts.Controls.Add(this.txtNextSplit);
            this.tab_globalshortcuts.Controls.Add(this.cbScNextSplit);
            this.tab_globalshortcuts.Controls.Add(this.txtHit);
            this.tab_globalshortcuts.Controls.Add(this.txtReset);
            this.tab_globalshortcuts.Location = new System.Drawing.Point(4, 22);
            this.tab_globalshortcuts.Name = "tab_globalshortcuts";
            this.tab_globalshortcuts.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.tab_globalshortcuts.Size = new System.Drawing.Size(588, 209);
            this.tab_globalshortcuts.TabIndex = 0;
            this.tab_globalshortcuts.Text = "Global shortcuts";
            this.tab_globalshortcuts.UseVisualStyleBackColor = true;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(3, 125);
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
            this.radioHotKeyMethod_async.Location = new System.Drawing.Point(6, 164);
            this.radioHotKeyMethod_async.Name = "radioHotKeyMethod_async";
            this.radioHotKeyMethod_async.Size = new System.Drawing.Size(201, 17);
            this.radioHotKeyMethod_async.TabIndex = 8;
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
            this.Label1.Size = new System.Drawing.Size(338, 13);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Click into textbox and press any key combination to setup your hotkey:";
            // 
            // radioHotKeyMethod_sync
            // 
            this.radioHotKeyMethod_sync.AutoSize = true;
            this.radioHotKeyMethod_sync.Location = new System.Drawing.Point(6, 141);
            this.radioHotKeyMethod_sync.Name = "radioHotKeyMethod_sync";
            this.radioHotKeyMethod_sync.Size = new System.Drawing.Size(477, 17);
            this.radioHotKeyMethod_sync.TabIndex = 7;
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
            this.cbScReset.Location = new System.Drawing.Point(6, 35);
            this.cbScReset.Name = "cbScReset";
            this.cbScReset.Size = new System.Drawing.Size(111, 17);
            this.cbScReset.TabIndex = 0;
            this.cbScReset.Text = "Reset currect run:";
            this.ToolTip1.SetToolTip(this.cbScReset, "Enable hot key for resetting the run");
            this.cbScReset.UseVisualStyleBackColor = true;
            this.cbScReset.CheckedChanged += new System.EventHandler(this.cbScReset_CheckedChanged);
            // 
            // cbScHit
            // 
            this.cbScHit.AutoSize = true;
            this.cbScHit.Location = new System.Drawing.Point(6, 58);
            this.cbScHit.Name = "cbScHit";
            this.cbScHit.Size = new System.Drawing.Size(60, 17);
            this.cbScHit.TabIndex = 1;
            this.cbScHit.Text = "Got hit:";
            this.ToolTip1.SetToolTip(this.cbScHit, "Enable hot key for getting hit");
            this.cbScHit.UseVisualStyleBackColor = true;
            this.cbScHit.CheckedChanged += new System.EventHandler(this.cbScHit_CheckedChanged);
            // 
            // txtNextSplit
            // 
            this.txtNextSplit.Location = new System.Drawing.Point(123, 79);
            this.txtNextSplit.Name = "txtNextSplit";
            this.txtNextSplit.ReadOnly = true;
            this.txtNextSplit.Size = new System.Drawing.Size(459, 20);
            this.txtNextSplit.TabIndex = 5;
            this.txtNextSplit.Text = "None";
            this.txtNextSplit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtNextSplit, "Click into the field and press the hot key you want to use");
            this.txtNextSplit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNextSplit_KeyDown);
            // 
            // cbScNextSplit
            // 
            this.cbScNextSplit.AutoSize = true;
            this.cbScNextSplit.Location = new System.Drawing.Point(6, 81);
            this.cbScNextSplit.Name = "cbScNextSplit";
            this.cbScNextSplit.Size = new System.Drawing.Size(72, 17);
            this.cbScNextSplit.TabIndex = 2;
            this.cbScNextSplit.Text = "Next split:";
            this.ToolTip1.SetToolTip(this.cbScNextSplit, "Enable hot key for entering next split");
            this.cbScNextSplit.UseVisualStyleBackColor = true;
            this.cbScNextSplit.CheckedChanged += new System.EventHandler(this.cbScNextSplit_CheckedChanged);
            // 
            // txtHit
            // 
            this.txtHit.Location = new System.Drawing.Point(123, 56);
            this.txtHit.Name = "txtHit";
            this.txtHit.ReadOnly = true;
            this.txtHit.Size = new System.Drawing.Size(459, 20);
            this.txtHit.TabIndex = 4;
            this.txtHit.Text = "None";
            this.txtHit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtHit, "Click into the field and press the hot key you want to use");
            this.txtHit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHit_KeyDown);
            // 
            // txtReset
            // 
            this.txtReset.Location = new System.Drawing.Point(123, 33);
            this.txtReset.Name = "txtReset";
            this.txtReset.ReadOnly = true;
            this.txtReset.Size = new System.Drawing.Size(459, 20);
            this.txtReset.TabIndex = 3;
            this.txtReset.Text = "None";
            this.txtReset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip1.SetToolTip(this.txtReset, "Click into the field and press the hot key you want to use");
            this.txtReset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReset_KeyDown);
            // 
            // tab_appearance
            // 
            this.tab_appearance.Controls.Add(this.GroupBox1);
            this.tab_appearance.Controls.Add(this.cbShowSessionProgress);
            this.tab_appearance.Controls.Add(this.cbShowAttempts);
            this.tab_appearance.Controls.Add(this.cbShowHeadline);
            this.tab_appearance.Controls.Add(this.cbApHighContrast);
            this.tab_appearance.Controls.Add(this.Label9);
            this.tab_appearance.Controls.Add(this.numStyleDesiredWidth);
            this.tab_appearance.Controls.Add(this.numShowSplitsCountUpcoming);
            this.tab_appearance.Controls.Add(this.Label13);
            this.tab_appearance.Controls.Add(this.Label14);
            this.tab_appearance.Controls.Add(this.Label10);
            this.tab_appearance.Controls.Add(this.Label8);
            this.tab_appearance.Controls.Add(this.numShowSplitsCountFinished);
            this.tab_appearance.Controls.Add(this.Label3);
            this.tab_appearance.Location = new System.Drawing.Point(4, 22);
            this.tab_appearance.Name = "tab_appearance";
            this.tab_appearance.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.tab_appearance.Size = new System.Drawing.Size(588, 209);
            this.tab_appearance.TabIndex = 1;
            this.tab_appearance.Text = "Appearance";
            this.tab_appearance.UseVisualStyleBackColor = true;
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
            this.tab_filepaths.Size = new System.Drawing.Size(588, 209);
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
            this.TabControl1.Controls.Add(this.tab_appearance);
            this.TabControl1.Controls.Add(this.tab_filepaths);
            this.TabControl1.Location = new System.Drawing.Point(12, 12);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(596, 235);
            this.TabControl1.TabIndex = 10;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 259);
            this.Controls.Add(this.TabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStyleDesiredWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShowSplitsCountUpcoming)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShowSplitsCountFinished)).EndInit();
            this.tab_globalshortcuts.ResumeLayout(false);
            this.tab_globalshortcuts.PerformLayout();
            this.tab_appearance.ResumeLayout(false);
            this.tab_appearance.PerformLayout();
            this.tab_filepaths.ResumeLayout(false);
            this.tab_filepaths.PerformLayout();
            this.TabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.CheckBox cbApCustomCss;
        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.TextBox txtCssUrl;
        internal System.Windows.Forms.TextBox txtFontUrl;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Button btnApApply;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.CheckBox cbShowSessionProgress;
        internal System.Windows.Forms.CheckBox cbShowAttempts;
        internal System.Windows.Forms.CheckBox cbShowHeadline;
        internal System.Windows.Forms.CheckBox cbApHighContrast;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.NumericUpDown numStyleDesiredWidth;
        internal System.Windows.Forms.NumericUpDown numShowSplitsCountUpcoming;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
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
        internal System.Windows.Forms.TabPage tab_appearance;
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
    }
}