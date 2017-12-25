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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
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
        Me.cTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cHits = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDiff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cPB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.SaveToolStripMenuItem, Me.HelpToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(548, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.FileToolStripMenuItem.Text = "Settings"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cTitle, Me.cHits, Me.cDiff, Me.cPB})
        Me.DataGridView1.Location = New System.Drawing.Point(13, 153)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(523, 211)
        Me.DataGridView1.TabIndex = 4
        '
        'btnReset
        '
        Me.btnReset.BackColor = System.Drawing.Color.Salmon
        Me.btnReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(13, 27)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(75, 40)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Reset"
        Me.ToolTip1.SetToolTip(Me.btnReset, "RESET the current run")
        Me.btnReset.UseVisualStyleBackColor = False
        '
        'btnHit
        '
        Me.btnHit.BackColor = System.Drawing.Color.LightBlue
        Me.btnHit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHit.Location = New System.Drawing.Point(175, 27)
        Me.btnHit.Name = "btnHit"
        Me.btnHit.Size = New System.Drawing.Size(280, 40)
        Me.btnHit.TabIndex = 2
        Me.btnHit.Text = "Hit"
        Me.ToolTip1.SetToolTip(Me.btnHit, "Count a HIT on the current split")
        Me.btnHit.UseVisualStyleBackColor = False
        '
        'btnSplit
        '
        Me.btnSplit.BackColor = System.Drawing.Color.LightGreen
        Me.btnSplit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSplit.Location = New System.Drawing.Point(461, 27)
        Me.btnSplit.Name = "btnSplit"
        Me.btnSplit.Size = New System.Drawing.Size(75, 40)
        Me.btnSplit.TabIndex = 3
        Me.btnSplit.Text = "Split"
        Me.ToolTip1.SetToolTip(Me.btnSplit, "Jump to the next SPLIT")
        Me.btnSplit.UseVisualStyleBackColor = False
        '
        'btnDown
        '
        Me.btnDown.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_scroll_down_20
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(515, 73)
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
        Me.btnUp.Location = New System.Drawing.Point(489, 73)
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
        Me.ComboBox1.Location = New System.Drawing.Point(13, 102)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(522, 21)
        Me.ComboBox1.Sorted = True
        Me.ComboBox1.TabIndex = 7
        '
        'btnRename
        '
        Me.btnRename.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_edit_20
        Me.btnRename.FlatAppearance.BorderSize = 0
        Me.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRename.Location = New System.Drawing.Point(39, 73)
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
        Me.btnNew.Location = New System.Drawing.Point(13, 73)
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
        Me.btnDelete.Location = New System.Drawing.Point(91, 73)
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
        Me.btnPB.Location = New System.Drawing.Point(94, 27)
        Me.btnPB.Name = "btnPB"
        Me.btnPB.Size = New System.Drawing.Size(75, 40)
        Me.btnPB.TabIndex = 11
        Me.btnPB.Text = "PB"
        Me.ToolTip1.SetToolTip(Me.btnPB, "Record run as PB (personal best)")
        Me.btnPB.UseVisualStyleBackColor = False
        '
        'lbl_progress
        '
        Me.lbl_progress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_progress.Location = New System.Drawing.Point(12, 130)
        Me.lbl_progress.Name = "lbl_progress"
        Me.lbl_progress.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lbl_progress.Size = New System.Drawing.Size(157, 20)
        Me.lbl_progress.TabIndex = 12
        Me.lbl_progress.Text = "Progress:  ?? / ??"
        Me.lbl_progress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_totals
        '
        Me.lbl_totals.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_totals.Location = New System.Drawing.Point(175, 130)
        Me.lbl_totals.Name = "lbl_totals"
        Me.lbl_totals.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lbl_totals.Size = New System.Drawing.Size(360, 20)
        Me.lbl_totals.TabIndex = 13
        Me.lbl_totals.Text = "Total: ??? Hits   ??? PB"
        Me.lbl_totals.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCopy
        '
        Me.btnCopy.BackgroundImage = Global.HitCounterManager.My.Resources.Resources.icons8_copy_20
        Me.btnCopy.FlatAppearance.BorderSize = 0
        Me.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopy.Location = New System.Drawing.Point(65, 73)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(20, 20)
        Me.btnCopy.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.btnCopy, "Copy profile")
        Me.btnCopy.UseVisualStyleBackColor = True
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
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(548, 376)
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
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(400, 400)
        Me.Name = "Form1"
        Me.Text = "HitCounterManager"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
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
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lbl_progress As Label
    Friend WithEvents lbl_totals As Label
    Friend WithEvents btnCopy As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents cPB As DataGridViewTextBoxColumn
    Friend WithEvents cDiff As DataGridViewTextBoxColumn
    Friend WithEvents cHits As DataGridViewTextBoxColumn
    Friend WithEvents cTitle As DataGridViewTextBoxColumn
End Class
