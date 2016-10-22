'MIT License

'Copyright(c) 2016 Peter Kirmeier

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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.btnInput = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbScReset
        '
        Me.cbScReset.AutoSize = True
        Me.cbScReset.Location = New System.Drawing.Point(6, 41)
        Me.cbScReset.Name = "cbScReset"
        Me.cbScReset.Size = New System.Drawing.Size(108, 17)
        Me.cbScReset.TabIndex = 0
        Me.cbScReset.Text = "Reset currect run"
        Me.cbScReset.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.radioHotKeyMethod_async)
        Me.GroupBox1.Controls.Add(Me.radioHotKeyMethod_sync)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtNextSplit)
        Me.GroupBox1.Controls.Add(Me.txtHit)
        Me.GroupBox1.Controls.Add(Me.txtReset)
        Me.GroupBox1.Controls.Add(Me.cbScNextSplit)
        Me.GroupBox1.Controls.Add(Me.cbScHit)
        Me.GroupBox1.Controls.Add(Me.cbScReset)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(596, 204)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Global shortcuts"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 131)
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
        Me.radioHotKeyMethod_async.Location = New System.Drawing.Point(6, 170)
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
        Me.radioHotKeyMethod_sync.Location = New System.Drawing.Point(6, 147)
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
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(338, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Click into textbox and press any key combination to setup your hotkey:"
        '
        'txtNextSplit
        '
        Me.txtNextSplit.Location = New System.Drawing.Point(135, 85)
        Me.txtNextSplit.Name = "txtNextSplit"
        Me.txtNextSplit.ReadOnly = True
        Me.txtNextSplit.Size = New System.Drawing.Size(455, 20)
        Me.txtNextSplit.TabIndex = 5
        Me.txtNextSplit.Text = "None"
        Me.txtNextSplit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtHit
        '
        Me.txtHit.Location = New System.Drawing.Point(135, 62)
        Me.txtHit.Name = "txtHit"
        Me.txtHit.ReadOnly = True
        Me.txtHit.Size = New System.Drawing.Size(455, 20)
        Me.txtHit.TabIndex = 4
        Me.txtHit.Text = "None"
        Me.txtHit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtReset
        '
        Me.txtReset.Location = New System.Drawing.Point(135, 39)
        Me.txtReset.Name = "txtReset"
        Me.txtReset.ReadOnly = True
        Me.txtReset.Size = New System.Drawing.Size(455, 20)
        Me.txtReset.TabIndex = 3
        Me.txtReset.Text = "None"
        Me.txtReset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbScNextSplit
        '
        Me.cbScNextSplit.AutoSize = True
        Me.cbScNextSplit.Location = New System.Drawing.Point(6, 87)
        Me.cbScNextSplit.Name = "cbScNextSplit"
        Me.cbScNextSplit.Size = New System.Drawing.Size(69, 17)
        Me.cbScNextSplit.TabIndex = 2
        Me.cbScNextSplit.Text = "Next split"
        Me.cbScNextSplit.UseVisualStyleBackColor = True
        '
        'cbScHit
        '
        Me.cbScHit.AutoSize = True
        Me.cbScHit.Location = New System.Drawing.Point(6, 64)
        Me.cbScHit.Name = "cbScHit"
        Me.cbScHit.Size = New System.Drawing.Size(57, 17)
        Me.cbScHit.TabIndex = 1
        Me.cbScHit.Text = "Got hit"
        Me.cbScHit.UseVisualStyleBackColor = True
        '
        'txtInput
        '
        Me.txtInput.Location = New System.Drawing.Point(70, 106)
        Me.txtInput.Name = "txtInput"
        Me.txtInput.ReadOnly = True
        Me.txtInput.Size = New System.Drawing.Size(440, 20)
        Me.txtInput.TabIndex = 6
        Me.txtInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 109)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Input file:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnOutput)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtOutput)
        Me.GroupBox2.Controls.Add(Me.btnInput)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtInput)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 222)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(596, 215)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Filepaths"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 163)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(561, 39)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = resources.GetString("Label7.Text")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(416, 78)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = resources.GetString("Label5.Text")
        '
        'btnOutput
        '
        Me.btnOutput.Location = New System.Drawing.Point(516, 130)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(74, 23)
        Me.btnOutput.TabIndex = 11
        Me.btnOutput.Text = "Search"
        Me.btnOutput.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Output file:"
        '
        'txtOutput
        '
        Me.txtOutput.Location = New System.Drawing.Point(70, 132)
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ReadOnly = True
        Me.txtOutput.Size = New System.Drawing.Size(440, 20)
        Me.txtOutput.TabIndex = 9
        Me.txtOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnInput
        '
        Me.btnInput.Location = New System.Drawing.Point(516, 104)
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
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(620, 449)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Settings"
        Me.Text = "Settings"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cbScReset As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cbScHit As CheckBox
    Friend WithEvents cbScNextSplit As CheckBox
    Friend WithEvents txtNextSplit As TextBox
    Friend WithEvents txtHit As TextBox
    Friend WithEvents txtReset As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtInput As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
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
End Class
