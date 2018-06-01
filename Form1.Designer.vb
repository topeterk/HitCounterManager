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
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.cTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cHits = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDiff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cSP = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnHit = New System.Windows.Forms.Button()
        Me.btnSplit = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.btnRename = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnPB = New System.Windows.Forms.Button()
        Me.lbl_progress = New System.Windows.Forms.Label()
        Me.lbl_totals = New System.Windows.Forms.Label()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnWeb = New System.Windows.Forms.Button()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.btnAttempts = New System.Windows.Forms.Button()
        Me.Spacer1 = New System.Windows.Forms.Label()
        Me.Spacer2 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cTitle, Me.cHits, Me.cDiff, Me.cPB, Me.cSP})
        Me.DataGridView1.Location = New System.Drawing.Point(13, 123)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(547, 241)
        Me.DataGridView1.TabIndex = 4
        '
        'cTitle
        '
        Me.cTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.cTitle.HeaderText = "Title"
        Me.cTitle.MinimumWidth = 30
        Me.cTitle.Name = "cTitle"
        Me.cTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cTitle.ToolTipText = "Title of the split"
        Me.cTitle.Width = 300
        '
        'cHits
        '
        Me.cHits.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle1.NullValue = "0"
        Me.cHits.DefaultCellStyle = DataGridViewCellStyle1
        Me.cHits.HeaderText = "Hits"
        Me.cHits.MinimumWidth = 30
        Me.cHits.Name = "cHits"
        Me.cHits.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cHits.ToolTipText = "Counted hits"
        Me.cHits.Width = 50
        '
        'cDiff
        '
        Me.cDiff.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle2.NullValue = "0"
        Me.cDiff.DefaultCellStyle = DataGridViewCellStyle2
        Me.cDiff.HeaderText = "Diff"
        Me.cDiff.MinimumWidth = 30
        Me.cDiff.Name = "cDiff"
        Me.cDiff.ReadOnly = True
        Me.cDiff.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cDiff.ToolTipText = "Difference between Hits and PB"
        Me.cDiff.Width = 50
        '
        'cPB
        '
        Me.cPB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle3.NullValue = "0"
        Me.cPB.DefaultCellStyle = DataGridViewCellStyle3
        Me.cPB.HeaderText = "PB"
        Me.cPB.MinimumWidth = 30
        Me.cPB.Name = "cPB"
        Me.cPB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cPB.ToolTipText = "Person best"
        Me.cPB.Width = 50
        '
        'cSP
        '
        Me.cSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.cSP.HeaderText = "SP"
        Me.cSP.MinimumWidth = 30
        Me.cSP.Name = "cSP"
        Me.cSP.ToolTipText = "Session progress"
        Me.cSP.Width = 30
        '
        'btnReset
        '
        Me.btnReset.BackColor = System.Drawing.Color.Salmon
        Me.btnReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Image = Global.HitCounterManager.My.Resources.Resources.icons8_repeat_one_32
        Me.btnReset.Location = New System.Drawing.Point(12, 57)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(75, 40)
        Me.btnReset.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnReset, "RESET the current run")
        Me.btnReset.UseVisualStyleBackColor = False
        '
        'btnHit
        '
        Me.btnHit.BackColor = System.Drawing.Color.LightBlue
        Me.btnHit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHit.Image = Global.HitCounterManager.My.Resources.Resources.icons8_attack_32
        Me.btnHit.Location = New System.Drawing.Point(174, 57)
        Me.btnHit.Name = "btnHit"
        Me.btnHit.Size = New System.Drawing.Size(305, 40)
        Me.btnHit.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnHit, "Count a HIT on the current split")
        Me.btnHit.UseVisualStyleBackColor = False
        '
        'btnSplit
        '
        Me.btnSplit.BackColor = System.Drawing.Color.LightGreen
        Me.btnSplit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSplit.Image = Global.HitCounterManager.My.Resources.Resources.icons8_staircase_32
        Me.btnSplit.Location = New System.Drawing.Point(485, 57)
        Me.btnSplit.Name = "btnSplit"
        Me.btnSplit.Size = New System.Drawing.Size(75, 40)
        Me.btnSplit.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.btnSplit, "Jump to the next SPLIT")
        Me.btnSplit.UseVisualStyleBackColor = False
        '
        'btnDown
        '
        Me.btnDown.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_scroll_down_20
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(303, 4)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(20, 20)
        Me.btnDown.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnDown, "Move selected split DOWN")
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_scroll_up_20
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Location = New System.Drawing.Point(277, 4)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(20, 20)
        Me.btnUp.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.btnUp, "Move selected split UP")
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(12, 30)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(548, 21)
        Me.ComboBox1.Sorted = True
        Me.ComboBox1.TabIndex = 7
        '
        'btnRename
        '
        Me.btnRename.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_edit_20
        Me.btnRename.FlatAppearance.BorderSize = 0
        Me.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRename.Location = New System.Drawing.Point(158, 4)
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Size = New System.Drawing.Size(20, 20)
        Me.btnRename.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.btnRename, "Rename profile")
        Me.btnRename.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_add_20
        Me.btnNew.FlatAppearance.BorderSize = 0
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Location = New System.Drawing.Point(132, 4)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(20, 20)
        Me.btnNew.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.btnNew, "New profile")
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_trash_20
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Location = New System.Drawing.Point(210, 4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(20, 20)
        Me.btnDelete.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete profile")
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnPB
        '
        Me.btnPB.BackColor = System.Drawing.Color.LightYellow
        Me.btnPB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPB.Image = Global.HitCounterManager.My.Resources.Resources.icons8_trophy_32
        Me.btnPB.Location = New System.Drawing.Point(93, 57)
        Me.btnPB.Name = "btnPB"
        Me.btnPB.Size = New System.Drawing.Size(75, 40)
        Me.btnPB.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.btnPB, "Record run as PB (personal best)")
        Me.btnPB.UseVisualStyleBackColor = False
        '
        'lbl_progress
        '
        Me.lbl_progress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_progress.Location = New System.Drawing.Point(11, 100)
        Me.lbl_progress.Name = "lbl_progress"
        Me.lbl_progress.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lbl_progress.Size = New System.Drawing.Size(219, 20)
        Me.lbl_progress.TabIndex = 12
        Me.lbl_progress.Text = "Progress:  ?? / ??  # ???"
        Me.lbl_progress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_totals
        '
        Me.lbl_totals.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_totals.Location = New System.Drawing.Point(234, 100)
        Me.lbl_totals.Name = "lbl_totals"
        Me.lbl_totals.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lbl_totals.Size = New System.Drawing.Size(326, 20)
        Me.lbl_totals.TabIndex = 13
        Me.lbl_totals.Text = "Total: ??? Hits   ??? PB"
        Me.lbl_totals.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCopy
        '
        Me.btnCopy.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_copy_20
        Me.btnCopy.FlatAppearance.BorderSize = 0
        Me.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopy.Location = New System.Drawing.Point(184, 4)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(20, 20)
        Me.btnCopy.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.btnCopy, "Copy profile")
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnSettings
        '
        Me.btnSettings.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_settings_20
        Me.btnSettings.FlatAppearance.BorderSize = 0
        Me.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSettings.Location = New System.Drawing.Point(13, 4)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(20, 20)
        Me.btnSettings.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.btnSettings, "Settings")
        Me.btnSettings.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_save_20
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(39, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(20, 20)
        Me.btnSave.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.btnSave, "Save")
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnWeb
        '
        Me.btnWeb.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_website_20
        Me.btnWeb.FlatAppearance.BorderSize = 0
        Me.btnWeb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnWeb.Location = New System.Drawing.Point(65, 4)
        Me.btnWeb.Name = "btnWeb"
        Me.btnWeb.Size = New System.Drawing.Size(20, 20)
        Me.btnWeb.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.btnWeb, "Help")
        Me.btnWeb.UseVisualStyleBackColor = True
        '
        'btnAbout
        '
        Me.btnAbout.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_about_20
        Me.btnAbout.FlatAppearance.BorderSize = 0
        Me.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAbout.Location = New System.Drawing.Point(91, 4)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(20, 20)
        Me.btnAbout.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.btnAbout, "About")
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'btnAttempts
        '
        Me.btnAttempts.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_counter_20
        Me.btnAttempts.FlatAppearance.BorderSize = 0
        Me.btnAttempts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAttempts.Location = New System.Drawing.Point(251, 4)
        Me.btnAttempts.Name = "btnAttempts"
        Me.btnAttempts.Size = New System.Drawing.Size(20, 20)
        Me.btnAttempts.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.btnAttempts, "Set amount of attempts manually")
        Me.btnAttempts.UseVisualStyleBackColor = True
        '
        'Spacer1
        '
        Me.Spacer1.AutoSize = True
        Me.Spacer1.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Spacer1.Location = New System.Drawing.Point(117, 8)
        Me.Spacer1.Name = "Spacer1"
        Me.Spacer1.Size = New System.Drawing.Size(9, 13)
        Me.Spacer1.TabIndex = 19
        Me.Spacer1.Text = "|"
        '
        'Spacer2
        '
        Me.Spacer2.AutoSize = True
        Me.Spacer2.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Spacer2.Location = New System.Drawing.Point(236, 8)
        Me.Spacer2.Name = "Spacer2"
        Me.Spacer2.Size = New System.Drawing.Size(9, 13)
        Me.Spacer2.TabIndex = 20
        Me.Spacer2.Text = "|"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 376)
        Me.Controls.Add(Me.btnAttempts)
        Me.Controls.Add(Me.Spacer2)
        Me.Controls.Add(Me.Spacer1)
        Me.Controls.Add(Me.btnAbout)
        Me.Controls.Add(Me.btnWeb)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.lbl_totals)
        Me.Controls.Add(Me.lbl_progress)
        Me.Controls.Add(Me.btnPB)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnRename)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnHit)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnSplit)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(400, 400)
        Me.Name = "Form1"
        Me.Text = "HitCounterManager"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReset As Button
    Friend WithEvents btnHit As Button
    Friend WithEvents btnSplit As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnDown As Button
    Friend WithEvents btnUp As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents btnRename As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnPB As Button
    Friend WithEvents lbl_progress As Label
    Friend WithEvents lbl_totals As Label
    Friend WithEvents btnCopy As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents cSP As DataGridViewCheckBoxColumn
    Friend WithEvents cPB As DataGridViewTextBoxColumn
    Friend WithEvents cDiff As DataGridViewTextBoxColumn
    Friend WithEvents cHits As DataGridViewTextBoxColumn
    Friend WithEvents cTitle As DataGridViewTextBoxColumn
    Friend WithEvents btnSettings As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnWeb As Button
    Friend WithEvents btnAbout As Button
    Friend WithEvents Spacer1 As Label
    Friend WithEvents Spacer2 As Label
    Friend WithEvents btnAttempts As Button
End Class
