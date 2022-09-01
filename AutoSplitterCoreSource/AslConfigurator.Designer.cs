namespace AutoSplitterCore
{
    partial class AslConfigurator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AslConfigurator));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAsl = new System.Windows.Forms.TabPage();
            this.btnUpdateScript = new System.Windows.Forms.Button();
            this.panelReference = new System.Windows.Forms.Panel();
            this.btnGetASLs = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabAsl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAsl);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(567, 607);
            this.tabControl1.TabIndex = 0;
            // 
            // tabAsl
            // 
            this.tabAsl.BackColor = System.Drawing.SystemColors.Control;
            this.tabAsl.Controls.Add(this.btnUpdateScript);
            this.tabAsl.Controls.Add(this.panelReference);
            this.tabAsl.Controls.Add(this.btnGetASLs);
            this.tabAsl.Location = new System.Drawing.Point(4, 22);
            this.tabAsl.Name = "tabAsl";
            this.tabAsl.Padding = new System.Windows.Forms.Padding(3);
            this.tabAsl.Size = new System.Drawing.Size(559, 581);
            this.tabAsl.TabIndex = 0;
            this.tabAsl.Text = "ASL";
            // 
            // btnUpdateScript
            // 
            this.btnUpdateScript.Location = new System.Drawing.Point(258, 521);
            this.btnUpdateScript.Name = "btnUpdateScript";
            this.btnUpdateScript.Size = new System.Drawing.Size(99, 23);
            this.btnUpdateScript.TabIndex = 2;
            this.btnUpdateScript.Text = "Update Script";
            this.btnUpdateScript.UseVisualStyleBackColor = true;
            this.btnUpdateScript.Click += new System.EventHandler(this.btnUpdateScript_Click);
            // 
            // panelReference
            // 
            this.panelReference.Location = new System.Drawing.Point(3, 3);
            this.panelReference.Name = "panelReference";
            this.panelReference.Size = new System.Drawing.Size(476, 512);
            this.panelReference.TabIndex = 1;
            // 
            // btnGetASLs
            // 
            this.btnGetASLs.Location = new System.Drawing.Point(363, 521);
            this.btnGetASLs.Name = "btnGetASLs";
            this.btnGetASLs.Size = new System.Drawing.Size(99, 23);
            this.btnGetASLs.TabIndex = 0;
            this.btnGetASLs.Text = "Get ASL Scripts";
            this.btnGetASLs.UseVisualStyleBackColor = true;
            this.btnGetASLs.Click += new System.EventHandler(this.btnGetASLs_Click);
            // 
            // AslConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 631);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AslConfigurator";
            this.Text = "Scripteable AutoSplit";
            this.Load += new System.EventHandler(this.AslConfigurator_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabAsl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAsl;
        private System.Windows.Forms.Button btnGetASLs;
        private System.Windows.Forms.Panel panelReference;
        private System.Windows.Forms.Button btnUpdateScript;
    }
}