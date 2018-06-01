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

Public Class Form1
    Public Const WM_HOTKEY As Integer = &H312

    Public sc As Shortcuts
    Public om As OutModule
    Private profs As Profiles = New Profiles()
    Private _AttemptsCounter As Integer = 0
    Private ComboBox1PrevSelectedItem As Object = Nothing
    Private SettingsDialogOpen = False

    ' Sync AttemptsCounter with output module
    Private Property AttemptsCounter() As Integer
        Get
            Return _AttemptsCounter
        End Get
        Set(AttemptsCounter As Integer)
            _AttemptsCounter = AttemptsCounter
            om.AttemptsCount = AttemptsCounter
        End Set
    End Property

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = Text + " - v" + Application.ProductVersion
        LoadSettings()
        om.Update()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            If Not SettingsDialogOpen Then
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
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Dim form = New Settings()
        SettingsDialogOpen = True
        form.ShowDialog(Me)
        SettingsDialogOpen = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        profs.SaveProfileFrom(ComboBox1.SelectedItem, DataGridView1, AttemptsCounter)
        SaveSettings()
    End Sub

    Private Sub btnWeb_Click(sender As Object, e As EventArgs) Handles btnWeb.Click
        System.Diagnostics.Process.Start("https://github.com/topeterk/HitCounterManager")
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        Dim form = New About()
        form.ShowDialog(Me)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        AttemptsCounter = AttemptsCounter + 1 ' Increase attempts

        For r = 0 To DataGridView1.RowCount - 2 Step 1
            DataGridView1.Rows.Item(r).Cells.Item("cHits").Value = 0
        Next
        DataGridView1.ClearSelection()
        DataGridView1.Rows.Item(0).Selected = True
        DataGridView1.Rows.Item(0).Cells.Item("cSP").Value = True
        om.Update()
    End Sub

    Private Sub btnPB_Click(sender As Object, e As EventArgs) Handles btnPB.Click
        If DataGridView1.RowCount < 2 Then Exit Sub

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
        If idx <= DataGridView1.RowCount - 1 Then
            DataGridView1.ClearSelection()
            DataGridView1.Rows.Item(idx).Selected = True
        End If
        If idx <= DataGridView1.RowCount - 2 Then
            For r = 0 To DataGridView1.RowCount - 2 Step 1
                DataGridView1.Rows.Item(r).Cells.Item("cSP").Value = False
            Next
            DataGridView1.Rows.Item(idx).Cells.Item("cSP").Value = True
        End If
        om.Update()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedCells.Count = 0 Then Exit Sub
        UpdateProgressAndTotals()
        om.Update()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SaveSettings()
    End Sub

    Private Function IsInvalidConfigString(str As String) As Boolean
        IsInvalidConfigString = True
        If InStr(str, ";") Then
            MessageBox.Show("Not allowed to use "";""!")
            Exit Function
        End If
        If InStr(str, "|") Then
            MessageBox.Show("Not allowed to use ""|""!")
            Exit Function
        End If
        If InStr(str, "<") Then
            MessageBox.Show("Not allowed to use ""<""!")
            Exit Function
        End If
        If InStr(str, ">") Then
            MessageBox.Show("Not allowed to use "">""!")
            Exit Function
        End If
        IsInvalidConfigString = False
    End Function

    Private Sub DataGridView1_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).GetType.Name = "DataGridViewCheckBoxCell" Then Exit Sub

        If IsInvalidConfigString(e.FormattedValue) Then
            e.Cancel = True
            Exit Sub
        End If

        If e.ColumnIndex <> DataGridView1.Rows.Item(0).Cells.Item("cTitle").ColumnIndex Then
            Dim i As Integer
            If Not Integer.TryParse(e.FormattedValue, i) Then
                e.Cancel = True
                MessageBox.Show("Must be numeric!")
            End If
        End If
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Const Pad = 13

        ' fill
        ComboBox1.Width = Width - Pad * 2 - 15
        DataGridView1.Left = Pad
        DataGridView1.Width = ComboBox1.Width
        DataGridView1.Height = Height - DataGridView1.Top - Pad - btnSettings.Height - 15

        ' right aligned
        btnSplit.Left = Width - Pad - 15 - btnSplit.Width
        lbl_totals.Width = Width - Pad - 15 - lbl_totals.Left

        ' left aligned
        btnHit.Width = btnSplit.Left - Pad / 2 - btnHit.Left
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Static Dim SemaValueChange = False
        If SemaValueChange Then Exit Sub

        For r = 0 To DataGridView1.RowCount - 2 Step 1
            DataGridView1.Rows.Item(r).Cells.Item("cDiff").Value = DataGridView1.Rows.Item(r).Cells.Item("cHits").Value - DataGridView1.Rows.Item(r).Cells.Item("cPB").Value
        Next

        If DataGridView1.SelectedCells.Count <> 0 Then
            Dim idx = DataGridView1.SelectedCells.Item(0).RowIndex
            If idx <= DataGridView1.RowCount - 2 Then
                SemaValueChange = True
                For r = 0 To DataGridView1.RowCount - 2 Step 1
                    DataGridView1.Rows.Item(r).Cells.Item("cSP").Value = False
                Next
                DataGridView1.Rows.Item(idx).Cells.Item("cSP").Value = True
                SemaValueChange = False
            End If
        End If

        profs.SaveProfileFrom(ComboBox1.SelectedItem, DataGridView1, AttemptsCounter, True)
        UpdateProgressAndTotals()
    End Sub

    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp
        ' Workaround to fire CellValueChanged on a Checkbox change via mouse (left click) by switching cell focus
        If DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).GetType.Name = "DataGridViewCheckBoxCell" Then
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(DataGridView1.ColumnCount - 1).Selected = True
            DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyUp
        ' Workaround to fire CellValueChanged on a Checkbox change via keyboard (space) by switching cell focus
        If e.KeyCode = Keys.Space Then
            Dim SelectedCell = DataGridView1.SelectedCells.Item(0)
            If SelectedCell.GetType.Name = "DataGridViewCheckBoxCell" Then
                e.Handled = True
                SelectedCell.Value = Not SelectedCell.Value
                DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(DataGridView1.ColumnCount - 1).Selected = True
                DataGridView1.Rows(SelectedCell.RowIndex).Cells(SelectedCell.ColumnIndex).Selected = True
            End If
        End If
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        Dim idx_old = DataGridView1.SelectedCells.Item(0).RowIndex
        Dim idx_new = DataGridView1.SelectedCells.Item(0).RowIndex - 1
        If 0 <= idx_new And idx_old < DataGridView1.Rows.Count - 1 Then ' Do not move when UP is not possible
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
        If idx_new < DataGridView1.RowCount - 1 Then ' Do not move when DOWN is not possible
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
        Dim name = InputBox("Enter name of new profile", "New profile", ComboBox1.SelectedItem)
        If name.Length = 0 Or IsInvalidConfigString(name) Then Exit Sub

        If ComboBox1.Items.Contains(name) Then
            If Not DialogResult.OK = MessageBox.Show("A profile with this name already exists. Do you want to create as copy from the currently selected?", "Profile already exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) Then Exit Sub
            btnCopy_Click(sender, e)
            Exit Sub
        End If

        profs.SaveProfileFrom(ComboBox1.SelectedItem, DataGridView1, AttemptsCounter) ' save previous selected profile

        ' create, select and save new profile..
        ComboBox1.Items.Add(name)
        ComboBox1.SelectedItem = name
        DataGridView1.Rows.Clear()
        profs.SaveProfileFrom(name, DataGridView1, AttemptsCounter, True) ' save new empty profile
        UpdateProgressAndTotals()
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        Dim name = ComboBox1.SelectedItem

        Do
            name += " COPY"
            If Not ComboBox1.Items.Contains(name) Then Exit Do
        Loop

        profs.SaveProfileFrom(ComboBox1.SelectedItem, DataGridView1, AttemptsCounter) ' save previous selected profile

        ' create, select and save new profile..
        ComboBox1.Items.Add(name)
        profs.SaveProfileFrom(name, DataGridView1, AttemptsCounter, True) ' copy current data to new profile
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

            profs.LoadProfileInto(ComboBox1.SelectedItem, DataGridView1, AttemptsCounter)
        End If
    End Sub

    Private Sub btnRename_Click(sender As Object, e As EventArgs) Handles btnRename.Click
        If ComboBox1.Items.Count = 0 Then Exit Sub

        Dim name = InputBox("Enter new name for profile """ & ComboBox1.SelectedItem & """!", "Rename profile", ComboBox1.SelectedItem)
        If name.Length = 0 Or IsInvalidConfigString(name) Then Exit Sub

        If ComboBox1.Items.Contains(name) Then
            MessageBox.Show("A profile with this name already exists!", "Profile already exists")
            Exit Sub
        End If

        profs.RenameProfile(ComboBox1.SelectedItem, name)
        ComboBox1.Items.Item(ComboBox1.SelectedIndex) = name
    End Sub

    Private Sub btnAttempts_Click(sender As Object, e As EventArgs) Handles btnAttempts.Click
        Dim amount = InputBox("Enter amount to be set!", "Set amount of attempts", AttemptsCounter)
        If Not IsNumeric(amount) Then
            If amount.Equals("") Then Exit Sub ' Unfortunately this is the Cancel button
            MessageBox.Show("Only numbers are allowed!")
            Exit Sub
        End If
        AttemptsCounter = amount
        profs.SaveProfileFrom(ComboBox1PrevSelectedItem, DataGridView1, AttemptsCounter)
        UpdateProgressAndTotals()
        om.Update()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If Not IsNothing(ComboBox1PrevSelectedItem) Then
            profs.SaveProfileFrom(ComboBox1PrevSelectedItem, DataGridView1, AttemptsCounter)
        End If
        profs.LoadProfileInto(ComboBox1.SelectedItem, DataGridView1, AttemptsCounter)
        ComboBox1PrevSelectedItem = ComboBox1.SelectedItem
        UpdateProgressAndTotals()
        om.Update()
    End Sub

    Private Sub UpdateProgressAndTotals()
        Dim TotalHits = 0
        Dim TotalPB = 0
        Dim Splits = DataGridView1.RowCount - 2

        If Splits < 0 Then ' Check for valid entries
            lbl_progress.Text = "Progress:  ?? / ??  # " & AttemptsCounter.ToString("D3")
        Else
            For r = 0 To Splits Step 1
                TotalHits = TotalHits + DataGridView1.Rows.Item(r).Cells.Item("cHits").Value
                TotalPB = TotalPB + DataGridView1.Rows.Item(r).Cells.Item("cPB").Value
            Next

            lbl_progress.Text = "Progress:  " & DataGridView1.SelectedCells.Item(0).RowIndex & " / " & Splits + 1 & "  # " & AttemptsCounter.ToString("D3")
        End If
        lbl_totals.Text = "Total: " & TotalHits & " Hits   " & TotalPB & " PB"
    End Sub

End Class
