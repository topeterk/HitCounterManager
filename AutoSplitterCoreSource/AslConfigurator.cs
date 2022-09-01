//MIT License

//Copyright (c) 2022 Ezequiel Medina

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Windows.Forms;
using System.Drawing;

namespace AutoSplitterCore
{
    public partial class AslConfigurator : Form
    {
        AslSplitter aslSplitter;
        public AslConfigurator(AslSplitter aslSplitter, bool darkMode)
        {
            InitializeComponent();
            this.aslSplitter = aslSplitter;
            panelReference.Hide();
            if (darkMode)
            {
                DarkMode();
            }
        }

        private void AslConfigurator_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages[0].Controls.Add(aslSplitter.control);

        }

        private void btnGetASLs_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://pastebin.com/FHbS9zsx");
        }

        private void btnUpdateScript_Click(object sender, EventArgs e)
        {
            aslSplitter.UpdateScript();
        }

        public void DarkMode()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.tabAsl.BackColor = Color.FromArgb(50, 50, 50);
        }
    }
}
