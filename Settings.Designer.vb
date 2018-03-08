'MIT License

'Copyright(c) 2016-2018 Peter Kirmeier

'Permission Is hereby granted, free Of charge, to any person obtaining a copy
'of this software And associated documentation files (the "Software"), to deal
'in the Software without restriction, including without limitation the rights
'to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
'copies of the Software, And to permit persons to whom the Software Is
'furnished to do so, subject to the following conditions:

'The above copyright notice And this permission notice shall be included In all
'copies Or substantial portions of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
'IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
'FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
'AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
'LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
'OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
'SOFTWARE.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Settings
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Settings))
        Me.cbScReset = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.radioHotKeyMethod_async = New System.Windows.Forms.RadioButton()
        Me.radioHotKeyMethod_sync = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNextSplit = New System.Windows.Forms.TextBox()
        Me.txtHit = New System.Windows.Forms.TextBox()
        Me.txtReset = New System.Windows.Forms.TextBox()
        Me.cbScNextSplit = New System.Windows.Forms.CheckBox()
        Me.cbScHit = New System.Windows.Forms.CheckBox()
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.btnInput = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tab_globalshortcuts = New System.Windows.Forms.TabPage()
        Me.tab_appearance = New System.Windows.Forms.TabPage()
        Me.tab_filepaths = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.numShowSplitsCountFinished = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.numShowSplitsCountUpcoming = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtFontUrl = New System.Windows.Forms.TextBox()
        Me.cbApCustomCss = New System.Windows.Forms.CheckBox()
        Me.txtCssUrl = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbApHighContrast = New System.Windows.Forms.CheckBox()
        Me.btnApApply = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.tab_globalshortcuts.SuspendLayout()
        Me.tab_appearance.SuspendLayout()
        Me.tab_filepaths.SuspendLayout()
        CType(Me.numShowSplitsCountFinished, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numShowSplitsCountUpcoming, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbScReset
        '
        Me.cbScReset.AutoSize = True
        Me.cbScReset.Location = New System.Drawing.Point(6, 35)
        Me.cbScReset.Name = "cbScReset"
        Me.cbScReset.Size = New System.Drawing.Size(111, 17)
        Me.cbScReset.TabIndex = 0
        Me.cbScReset.Text = "Reset currect run:"
        Me.cbScReset.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(393, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Select method of global hotkey registration (changing needs restart of applicatio" &
    "n):"
        '
        'radioHotKeyMethod_async
        '
        Me.radioHotKeyMethod_async.AutoSize = True
        Me.radioHotKeyMethod_async.Checked = True
        Me.radioHotKeyMethod_async.Location = New System.Drawing.Point(6, 164)
        Me.radioHotKeyMethod_async.Name = "radioHotKeyMethod_async"
        Me.radioHotKeyMethod_async.Size = New System.Drawing.Size(201, 17)
        Me.radioHotKeyMethod_async.TabIndex = 8
        Me.radioHotKeyMethod_async.TabStop = True
        Me.radioHotKeyMethod_async.Text = "Asynchronous - *should* always work"
        Me.radioHotKeyMethod_async.UseVisualStyleBackColor = True
        '
        'radioHotKeyMethod_sync
        '
        Me.radioHotKeyMethod_sync.AutoSize = True
        Me.radioHotKeyMethod_sync.Location = New System.Drawing.Point(6, 141)
        Me.radioHotKeyMethod_sync.Name = "radioHotKeyMethod_sync"
        Me.radioHotKeyMethod_sync.Size = New System.Drawing.Size(477, 17)
        Me.radioHotKeyMethod_sync.TabIndex = 7
        Me.radioHotKeyMethod_sync.Text = "Synchronous - Safer, but may not always work (recommendation: test and keep it wh" &
    "en it works)"
        Me.radioHotKeyMethod_sync.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(338, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Click into textbox and press any key combination to setup your hotkey:"
        '
        'txtNextSplit
        '
        Me.txtNextSplit.Location = New System.Drawing.Point(123, 79)
        Me.txtNextSplit.Name = "txtNextSplit"
        Me.txtNextSplit.ReadOnly = True
        Me.txtNextSplit.Size = New System.Drawing.Size(459, 20)
        Me.txtNextSplit.TabIndex = 5
        Me.txtNextSplit.Text = "None"
        Me.txtNextSplit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtHit
        '
        Me.txtHit.Location = New System.Drawing.Point(123, 56)
        Me.txtHit.Name = "txtHit"
        Me.txtHit.ReadOnly = True
        Me.txtHit.Size = New System.Drawing.Size(459, 20)
        Me.txtHit.TabIndex = 4
        Me.txtHit.Text = "None"
        Me.txtHit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtReset
        '
        Me.txtReset.Location = New System.Drawing.Point(123, 33)
        Me.txtReset.Name = "txtReset"
        Me.txtReset.ReadOnly = True
        Me.txtReset.Size = New System.Drawing.Size(459, 20)
        Me.txtReset.TabIndex = 3
        Me.txtReset.Text = "None"
        Me.txtReset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbScNextSplit
        '
        Me.cbScNextSplit.AutoSize = True
        Me.cbScNextSplit.Location = New System.Drawing.Point(6, 81)
        Me.cbScNextSplit.Name = "cbScNextSplit"
        Me.cbScNextSplit.Size = New System.Drawing.Size(72, 17)
        Me.cbScNextSplit.TabIndex = 2
        Me.cbScNextSplit.Text = "Next split:"
        Me.cbScNextSplit.UseVisualStyleBackColor = True
        '
        'cbScHit
        '
        Me.cbScHit.AutoSize = True
        Me.cbScHit.Location = New System.Drawing.Point(6, 58)
        Me.cbScHit.Name = "cbScHit"
        Me.cbScHit.Size = New System.Drawing.Size(60, 17)
        Me.cbScHit.TabIndex = 1
        Me.cbScHit.Text = "Got hit:"
        Me.cbScHit.UseVisualStyleBackColor = True
        '
        'txtInput
        '
        Me.txtInput.Location = New System.Drawing.Point(70, 99)
        Me.txtInput.Name = "txtInput"
        Me.txtInput.ReadOnly = True
        Me.txtInput.Size = New System.Drawing.Size(432, 20)
        Me.txtInput.TabIndex = 6
        Me.txtInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Input file:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 159)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(561, 39)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = resources.GetString("Label7.Text")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(473, 78)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = resources.GetString("Label5.Text")
        '
        'btnOutput
        '
        Me.btnOutput.Location = New System.Drawing.Point(508, 123)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(74, 23)
        Me.btnOutput.TabIndex = 11
        Me.btnOutput.Text = "Search"
        Me.btnOutput.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Output file:"
        '
        'txtOutput
        '
        Me.txtOutput.Location = New System.Drawing.Point(70, 125)
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ReadOnly = True
        Me.txtOutput.Size = New System.Drawing.Size(432, 20)
        Me.txtOutput.TabIndex = 9
        Me.txtOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnInput
        '
        Me.btnInput.Location = New System.Drawing.Point(508, 97)
        Me.btnInput.Name = "btnInput"
        Me.btnInput.Size = New System.Drawing.Size(74, 23)
        Me.btnInput.TabIndex = 8
        Me.btnInput.Text = "Search"
        Me.btnInput.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "HitCounter*"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tab_globalshortcuts)
        Me.TabControl1.Controls.Add(Me.tab_appearance)
        Me.TabControl1.Controls.Add(Me.tab_filepaths)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(596, 235)
        Me.TabControl1.TabIndex = 9
        '
        'tab_globalshortcuts
        '
        Me.tab_globalshortcuts.Controls.Add(Me.Label6)
        Me.tab_globalshortcuts.Controls.Add(Me.radioHotKeyMethod_async)
        Me.tab_globalshortcuts.Controls.Add(Me.Label1)
        Me.tab_globalshortcuts.Controls.Add(Me.radioHotKeyMethod_sync)
        Me.tab_globalshortcuts.Controls.Add(Me.cbScReset)
        Me.tab_globalshortcuts.Controls.Add(Me.cbScHit)
        Me.tab_globalshortcuts.Controls.Add(Me.txtNextSplit)
        Me.tab_globalshortcuts.Controls.Add(Me.cbScNextSplit)
        Me.tab_globalshortcuts.Controls.Add(Me.txtHit)
        Me.tab_globalshortcuts.Controls.Add(Me.txtReset)
        Me.tab_globalshortcuts.Location = New System.Drawing.Point(4, 22)
        Me.tab_globalshortcuts.Name = "tab_globalshortcuts"
        Me.tab_globalshortcuts.Padding = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.tab_globalshortcuts.Size = New System.Drawing.Size(588, 209)
        Me.tab_globalshortcuts.TabIndex = 0
        Me.tab_globalshortcuts.Text = "Global shortcuts"
        Me.tab_globalshortcuts.UseVisualStyleBackColor = True
        '
        'tab_appearance
        '
        Me.tab_appearance.Controls.Add(Me.btnApApply)
        Me.tab_appearance.Controls.Add(Me.cbApHighContrast)
        Me.tab_appearance.Controls.Add(Me.Label13)
        Me.tab_appearance.Controls.Add(Me.Label12)
        Me.tab_appearance.Controls.Add(Me.Label11)
        Me.tab_appearance.Controls.Add(Me.Label9)
        Me.tab_appearance.Controls.Add(Me.cbApCustomCss)
        Me.tab_appearance.Controls.Add(Me.txtFontUrl)
        Me.tab_appearance.Controls.Add(Me.numShowSplitsCountUpcoming)
        Me.tab_appearance.Controls.Add(Me.txtCssUrl)
        Me.tab_appearance.Controls.Add(Me.Label10)
        Me.tab_appearance.Controls.Add(Me.Label8)
        Me.tab_appearance.Controls.Add(Me.numShowSplitsCountFinished)
        Me.tab_appearance.Controls.Add(Me.Label3)
        Me.tab_appearance.Location = New System.Drawing.Point(4, 22)
        Me.tab_appearance.Name = "tab_appearance"
        Me.tab_appearance.Padding = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.tab_appearance.Size = New System.Drawing.Size(588, 209)
        Me.tab_appearance.TabIndex = 1
        Me.tab_appearance.Text = "Appearance"
        Me.tab_appearance.UseVisualStyleBackColor = True
        '
        'tab_filepaths
        '
        Me.tab_filepaths.Controls.Add(Me.Label7)
        Me.tab_filepaths.Controls.Add(Me.Label5)
        Me.tab_filepaths.Controls.Add(Me.btnOutput)
        Me.tab_filepaths.Controls.Add(Me.txtInput)
        Me.tab_filepaths.Controls.Add(Me.Label4)
        Me.tab_filepaths.Controls.Add(Me.Label2)
        Me.tab_filepaths.Controls.Add(Me.txtOutput)
        Me.tab_filepaths.Controls.Add(Me.btnInput)
        Me.tab_filepaths.Location = New System.Drawing.Point(4, 22)
        Me.tab_filepaths.Name = "tab_filepaths"
        Me.tab_filepaths.Padding = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.tab_filepaths.Size = New System.Drawing.Size(588, 209)
        Me.tab_filepaths.TabIndex = 2
        Me.tab_filepaths.Text = "Filepaths"
        Me.tab_filepaths.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(124, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Show the last"
        '
        'numShowSplitsCountFinished
        '
        Me.numShowSplitsCountFinished.Location = New System.Drawing.Point(205, 13)
        Me.numShowSplitsCountFinished.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.numShowSplitsCountFinished.Name = "numShowSplitsCountFinished"
        Me.numShowSplitsCountFinished.Size = New System.Drawing.Size(57, 20)
        Me.numShowSplitsCountFinished.TabIndex = 1
        Me.numShowSplitsCountFinished.Value = New Decimal(New Integer() {999, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(268, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "finished splits."
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(268, 41)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 13)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "upcoming splits."
        '
        'numShowSplitsCountUpcoming
        '
        Me.numShowSplitsCountUpcoming.Location = New System.Drawing.Point(205, 39)
        Me.numShowSplitsCountUpcoming.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.numShowSplitsCountUpcoming.Name = "numShowSplitsCountUpcoming"
        Me.numShowSplitsCountUpcoming.Size = New System.Drawing.Size(57, 20)
        Me.numShowSplitsCountUpcoming.TabIndex = 4
        Me.numShowSplitsCountUpcoming.Value = New Decimal(New Integer() {999, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(124, 41)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "Show the next"
        '
        'txtFontUrl
        '
        Me.txtFontUrl.Enabled = False
        Me.txtFontUrl.Location = New System.Drawing.Point(75, 129)
        Me.txtFontUrl.Name = "txtFontUrl"
        Me.txtFontUrl.Size = New System.Drawing.Size(498, 20)
        Me.txtFontUrl.TabIndex = 7
        '
        'cbApCustomCss
        '
        Me.cbApCustomCss.AutoSize = True
        Me.cbApCustomCss.Location = New System.Drawing.Point(10, 80)
        Me.cbApCustomCss.Name = "cbApCustomCss"
        Me.cbApCustomCss.Size = New System.Drawing.Size(177, 17)
        Me.cbApCustomCss.TabIndex = 10
        Me.cbApCustomCss.Text = "Use custom stylesheet and font:"
        Me.cbApCustomCss.UseVisualStyleBackColor = True
        '
        'txtCssUrl
        '
        Me.txtCssUrl.Enabled = False
        Me.txtCssUrl.Location = New System.Drawing.Point(75, 103)
        Me.txtCssUrl.Name = "txtCssUrl"
        Me.txtCssUrl.Size = New System.Drawing.Size(498, 20)
        Me.txtCssUrl.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(7, 106)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 13)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "CSS: (URL)"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(7, 132)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 13)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Font: (URL)"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(72, 152)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(399, 26)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Keep the font's URL empty if no custom font needs to be loaded at all." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Make sure" &
    " the stylesheet uses the font otherwise the font gets loaded but not used."
        '
        'cbApHighContrast
        '
        Me.cbApHighContrast.AutoSize = True
        Me.cbApHighContrast.Location = New System.Drawing.Point(10, 186)
        Me.cbApHighContrast.Name = "cbApHighContrast"
        Me.cbApHighContrast.Size = New System.Drawing.Size(138, 17)
        Me.cbApHighContrast.TabIndex = 14
        Me.cbApHighContrast.Text = "Use high contrast mode"
        Me.cbApHighContrast.UseVisualStyleBackColor = True
        '
        'btnApApply
        '
        Me.btnApApply.BackColor = System.Drawing.Color.LightYellow
        Me.btnApApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnApApply.Location = New System.Drawing.Point(10, 13)
        Me.btnApApply.Name = "btnApApply"
        Me.btnApApply.Size = New System.Drawing.Size(90, 46)
        Me.btnApApply.TabIndex = 15
        Me.btnApApply.Text = "Apply"
        Me.btnApApply.UseVisualStyleBackColor = False
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(620, 259)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Settings"
        Me.Text = "Settings"
        Me.TabControl1.ResumeLayout(False)
        Me.tab_globalshortcuts.ResumeLayout(False)
        Me.tab_globalshortcuts.PerformLayout()
        Me.tab_appearance.ResumeLayout(False)
        Me.tab_appearance.PerformLayout()
        Me.tab_filepaths.ResumeLayout(False)
        Me.tab_filepaths.PerformLayout()
        CType(Me.numShowSplitsCountFinished, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numShowSplitsCountUpcoming, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cbScReset As CheckBox
    Friend WithEvents cbScHit As CheckBox
    Friend WithEvents cbScNextSplit As CheckBox
    Friend WithEvents txtNextSplit As TextBox
    Friend WithEvents txtHit As TextBox
    Friend WithEvents txtReset As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtInput As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnInput As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents btnOutput As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txtOutput As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents radioHotKeyMethod_async As RadioButton
    Friend WithEvents radioHotKeyMethod_sync As RadioButton
    Friend WithEvents Label7 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tab_globalshortcuts As TabPage
    Friend WithEvents tab_appearance As TabPage
    Friend WithEvents tab_filepaths As TabPage
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents cbApCustomCss As CheckBox
    Friend WithEvents txtFontUrl As TextBox
    Friend WithEvents numShowSplitsCountUpcoming As NumericUpDown
    Friend WithEvents txtCssUrl As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents numShowSplitsCountFinished As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents cbApHighContrast As CheckBox
    Friend WithEvents btnApApply As Button
End Class
