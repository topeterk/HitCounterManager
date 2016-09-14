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
Public Class Form1
    Public Const WM_HOTKEY As Integer = &H312

    Public sc As Shortcuts
    Public om As OutModule
    Private profs As Profiles = New Profiles()
    Private ComboBox1PrevSelectedItem As Object = Nothing

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim key = New ShortcutsKey

        Text = Text + " - v" + Application.ProductVersion

        With My.Settings
            sc = New Shortcuts(Me.Handle, .HotKeyMethod)
            om = New OutModule(DataGridView1)

            key.key = New KeyEventArgs(.ShortcutResetKeyCode)
            If .ShortcutResetEnable Then
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_Reset, key)
            Else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_Reset, key)
            End If
            key.key = New KeyEventArgs(.ShortcutHitKeyCode)
            If .ShortcutHitEnable Then
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_Hit, key)
            Else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_Hit, key)
            End If
            key.key = New KeyEventArgs(.ShortcutSplitKeyCode)
            If .ShortcutSplitEnable Then
                sc.Key_Set(Shortcuts.SC_Type.SC_Type_Split, key)
            Else
                sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_Split, key)
            End If

            profs.LoadProfiles(.ProfilesString)

            ComboBox1.Items.AddRange(profs.GetProfileList)
            If ComboBox1.Items.Count = 0 Then
                ComboBox1.Items.Add("Unnamed")
            End If
            ComboBox1.SelectedItem = .ProfilesSelected
            profs.LoadProfileInto(ComboBox1.SelectedItem, DataGridView1)
            DataGridView1.Rows.Item(0).Selected = True
            DataGridView1_CellValueChanged(Nothing, Nothing)

            If .MainWidth > 400 Then MyClass.Width = .MainWidth
            If .MainHeight > 400 Then MyClass.Height = .MainHeight

            ' set at last to avoid generating output while data is still loading

            If .Inputfile = "" Then .Inputfile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\" & Application.ProductName & "\HitCounter.template"
            If .OutputFile = "" Then .OutputFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\" & Application.ProductName & "\HitCounter.html"

            om.FilePathIn = .Inputfile
            om.FilePathOut = .OutputFile
        End With

    End Sub

    Private Sub SaveSettings()
        Dim key = New ShortcutsKey

        With My.Settings
            .HotKeyMethod = sc.NextStart_Method

            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Reset)
            .ShortcutResetEnable = key.used
            .ShortcutResetKeyCode = key.key.KeyData
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Hit)
            .ShortcutHitEnable = key.used
            .ShortcutHitKeyCode = key.key.KeyData
            key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Split)
            .ShortcutSplitEnable = key.used
            .ShortcutSplitKeyCode = key.key.KeyData

            profs.SaveProfileFrom(ComboBox1.SelectedItem, DataGridView1)
            .ProfilesString = profs.GetProfilesString()
            .ProfilesSelected = ComboBox1.SelectedItem

            .MainWidth = MyClass.Width
            .MainHeight = MyClass.Height

            .Inputfile = om.FilePathIn
            .OutputFile = om.FilePathOut

            .Save()
        End With

    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim Id As Shortcuts.SC_Type = m.WParam
            Select Case (Id)
                Case Shortcuts.SC_Type.SC_Type_Reset
                    btnReset_Click(Nothing, Nothing)
                Case Shortcuts.SC_Type.SC_Type_Hit
                    btnHit_Click(Nothing, Nothing)
                Case Shortcuts.SC_Type.SC_Type_Split
                    btnSplit_Click(Nothing, Nothing)
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click
        Dim form = New Settings()
        form.ShowDialog(Me)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim form = New About()
        form.ShowDialog(Me)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        For r = 0 To DataGridView1.RowCount - 2 Step 1
            DataGridView1.Rows.Item(r).Cells.Item("cHits").Value = 0
        Next
        DataGridView1.ClearSelection()
        DataGridView1.Rows.Item(0).Selected = True
        om.Update()
    End Sub

    Private Sub btnPB_Click(sender As Object, e As EventArgs) Handles btnPB.Click
        For r = 0 To DataGridView1.RowCount - 2 Step 1
            DataGridView1.Rows.Item(r).Cells.Item("cPB").Value = DataGridView1.Rows.Item(r).Cells.Item("cHits").Value
        Next

        DataGridView1.ClearSelection()
        DataGridView1.Rows.Item(DataGridView1.RowCount - 2).Selected = True
        om.Update()
    End Sub

    Private Sub btnHit_Click(sender As Object, e As EventArgs) Handles btnHit.Click
        If DataGridView1.SelectedCells.Count = 0 Then Exit Sub
        Dim idx = DataGridView1.SelectedCells.Item(0).RowIndex

        DataGridView1.Rows.Item(DataGridView1.SelectedCells.Item(0).RowIndex).Cells.Item("cHits").Value += 1

        DataGridView1.ClearSelection()
        DataGridView1.Rows.Item(idx).Selected = True
        om.Update()
    End Sub

    Private Sub btnSplit_Click(sender As Object, e As EventArgs) Handles btnSplit.Click
        Dim idx = DataGridView1.SelectedCells.Item(0).RowIndex + 1
        If idx < DataGridView1.RowCount - 1 Then
            DataGridView1.ClearSelection()
            DataGridView1.Rows.Item(idx).Selected = True
        End If
        om.Update()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedCells.Count = 0 Then Exit Sub
        om.Update()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SaveSettings()
    End Sub

    Private Sub DataGridView1_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If InStr(e.FormattedValue, ";") Then
            e.Cancel = True
            MessageBox.Show("Not allowed to use "";""!")
            Exit Sub
        End If
        If InStr(e.FormattedValue, "|") Then
            e.Cancel = True
            MessageBox.Show("Not allowed to use ""|""!")
            Exit Sub
        End If
        If e.ColumnIndex > 0 Then
            Dim i As Integer
            If Not Integer.TryParse(e.FormattedValue, i) Then
                e.Cancel = True
                MessageBox.Show("Must be numeric!")
            End If
        End If
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Const Pad = 13

        DataGridView1.Left = Pad
        DataGridView1.Width = Width - Pad * 2 - 15
        DataGridView1.Height = Height - DataGridView1.Top - Pad - MenuStrip1.Height - 15

        btnSplit.Left = Width - Pad - 15 - btnSplit.Width
        btnUp.Left = Width - Pad - 15 - btnUp.Width
        btnDown.Left = Width - Pad - 15 - btnDown.Width

        btnHit.Width = btnSplit.Left - Pad / 2 - btnHit.Left
        ComboBox1.Width = btnDown.Left - Pad / 2 - ComboBox1.Left
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        For r = 0 To DataGridView1.RowCount - 2 Step 1
            DataGridView1.Rows.Item(r).Cells.Item("cDiff").Value = DataGridView1.Rows.Item(r).Cells.Item("cHits").Value - DataGridView1.Rows.Item(r).Cells.Item("cPB").Value
        Next
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        Dim idx_old = DataGridView1.SelectedCells.Item(0).RowIndex
        Dim idx_new = DataGridView1.SelectedCells.Item(0).RowIndex - 1
        If 0 <= idx_new Then
            For i = 0 To DataGridView1.Columns.Count - 1 Step 1
                Dim cell As Object = DataGridView1.Rows(idx_old).Cells(i).Value
                DataGridView1.Rows(idx_old).Cells(i).Value = DataGridView1.Rows(idx_new).Cells(i).Value
                DataGridView1.Rows(idx_new).Cells(i).Value = cell
            Next
            DataGridView1.ClearSelection()
            DataGridView1.Rows.Item(idx_new).Selected = True
        End If
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        Dim idx_old = DataGridView1.SelectedCells.Item(0).RowIndex
        Dim idx_new = DataGridView1.SelectedCells.Item(0).RowIndex + 1
        If idx_new < DataGridView1.RowCount - 1 Then
            For i = 0 To DataGridView1.Columns.Count - 1 Step 1
                Dim cell As Object = DataGridView1.Rows(idx_old).Cells(i).Value
                DataGridView1.Rows(idx_old).Cells(i).Value = DataGridView1.Rows(idx_new).Cells(i).Value
                DataGridView1.Rows(idx_new).Cells(i).Value = cell
            Next
            DataGridView1.ClearSelection()
            DataGridView1.Rows.Item(idx_new).Selected = True
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Dim IsCopy = False
        Dim name = InputBox("Enter name of new profile", "New profile", ComboBox1.SelectedItem)
        If name.Length = 0 Then Exit Sub

        If InStr(name, ";") Then
            MessageBox.Show("Not allowed to use "";""!")
            Exit Sub
        End If
        If InStr(name, "|") Then
            MessageBox.Show("Not allowed to use ""|""!")
            Exit Sub
        End If

        If ComboBox1.Items.Contains(name) Then
            If Not DialogResult.OK = MessageBox.Show("A profile with this name already exists. Do you want to create as copy from the currently selected?", "Profile already exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) Then Exit Sub
            IsCopy = True
            Do
                name += " COPY"
                If Not ComboBox1.Items.Contains(name) Then Exit Do
            Loop
        End If

        profs.SaveProfileFrom(ComboBox1.SelectedItem, DataGridView1)
        If IsCopy Then
            profs.SaveProfileFrom(name, DataGridView1, True) ' copy old data to new profile
        Else
            DataGridView1.Rows.Clear()
            profs.SaveProfileFrom(name, DataGridView1, True)
        End If
        ComboBox1.Items.Add(name)
        ComboBox1.SelectedItem = name
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If ComboBox1.Items.Count = 0 Then Exit Sub

        If DialogResult.OK = MessageBox.Show("Do you really want to delete profile """ & ComboBox1.SelectedItem & """?", "Deleting profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) Then
            Dim idx = ComboBox1.SelectedIndex

            profs.DeleteProfile(ComboBox1.SelectedItem)
            ComboBox1.Items.RemoveAt(idx)

            If ComboBox1.Items.Count = 0 Then
                ComboBox1.Items.Add("Unnamed")
                ComboBox1.SelectedIndex = 0
            ElseIf ComboBox1.Items.Count >= idx Then
                ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
            Else
                ComboBox1.SelectedIndex = idx
            End If

            profs.LoadProfileInto(ComboBox1.SelectedItem, DataGridView1)
        End If
    End Sub

    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click
        If ComboBox1.Items.Count = 0 Then Exit Sub

        Dim name = InputBox("Enter new name for profile """ & ComboBox1.SelectedItem & """!", "Rename profile", ComboBox1.SelectedItem)
        If InStr(name, ";") Then
            MessageBox.Show("Not allowed to use "";""!")
            Exit Sub
        End If
        If InStr(name, "|") Then
            MessageBox.Show("Not allowed to use ""|""!")
            Exit Sub
        End If

        If ComboBox1.Items.Contains(name) Then
            MessageBox.Show("A profile with this name already exists!", "Profile already exists")
            Exit Sub
        End If

        profs.RenameProfile(ComboBox1.SelectedItem, name)
        ComboBox1.Items.Item(ComboBox1.SelectedIndex) = name
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If Not IsNothing(ComboBox1PrevSelectedItem) Then
            profs.SaveProfileFrom(ComboBox1PrevSelectedItem, DataGridView1)
        End If
        profs.LoadProfileInto(ComboBox1.SelectedItem, DataGridView1)
        ComboBox1PrevSelectedItem = ComboBox1.SelectedItem
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        System.Diagnostics.Process.Start("https://github.com/topeterk/HitCounterManager")
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveSettings()
    End Sub
End Class
