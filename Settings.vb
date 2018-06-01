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

Public Class Settings

    <Runtime.InteropServices.DllImport("User32.dll", CharSet:=Runtime.InteropServices.CharSet.Auto)>
    Public Shared Function GetKeyNameTextW(ByVal lParam As Long, ByVal lpBuffer As String, ByVal nSize As Integer) As Integer
    End Function
    <Runtime.InteropServices.DllImport("User32.dll")>
    Public Shared Function MapVirtualKey(ByVal uCode As Integer, ByVal uMapType As Integer) As Integer
    End Function

    Dim sc As Shortcuts
    Dim om As OutModule

    Private Function GetNameFromKeyCode(keyCode As Keys) As String
        If keyCode = Keys.None Then
            GetNameFromKeyCode = "None"
        Else
            Dim ScanCode = MapVirtualKey(keyCode, 0) ' MAPVK_VK_TO_VSC
            Dim lParam As Long
            Dim lpString As New String(vbNullChar, 256)
            lParam = ScanCode * &H10000
            Dim i = GetKeyNameTextW(lParam, lpString, lpString.Length)
            If 0 < GetKeyNameTextW(lParam, lpString, lpString.Length) Then
                GetNameFromKeyCode = lpString.ToString()
            Else
                GetNameFromKeyCode = "?"
            End If
        End If
    End Function

    Private Sub UpdateKeyName(txt As TextBox, e As KeyEventArgs)

        txt.Text = ""
        If e.Alt Then txt.Text += "ALT + "
        If e.Control Then txt.Text += "CTRL + "
        If e.Shift Then txt.Text += "SHIFT + "
        txt.Text += GetNameFromKeyCode(e.KeyCode)
    End Sub
    Private Sub RegisterHotKey(txt As TextBox, cb As CheckBox, Id As Shortcuts.SC_Type, e As KeyEventArgs)
        Dim key = New ShortcutsKey

        If e.KeyCode = Keys.ShiftKey Then Exit Sub
        If e.KeyCode = Keys.ControlKey Then Exit Sub
        If e.KeyCode = Keys.Alt Then Exit Sub
        If e.KeyCode = Keys.Menu Then Exit Sub ' = Alt

        ' register hotkey
        key.key = e
        sc.Key_Set(Id, key)

        UpdateKeyName(txt, e)
    End Sub
    Private Sub txtReset_KeyDown(sender As Object, e As KeyEventArgs) Handles txtReset.KeyDown
        RegisterHotKey(txtReset, Nothing, Shortcuts.SC_Type.SC_Type_Reset, e)
        cbScReset.Checked = True
    End Sub

    Private Sub txtHit_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHit.KeyDown
        RegisterHotKey(txtHit, Nothing, Shortcuts.SC_Type.SC_Type_Hit, e)
        cbScHit.Checked = True
    End Sub

    Private Sub txtNextSplit_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNextSplit.KeyDown
        RegisterHotKey(txtNextSplit, Nothing, Shortcuts.SC_Type.SC_Type_Split, e)
        cbScNextSplit.Checked = True
    End Sub

    Private Sub cbScReset_CheckedChanged(sender As Object, e As EventArgs) Handles cbScReset.CheckedChanged
        sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Reset, cbScReset.Checked)
    End Sub

    Private Sub cbScHit_CheckedChanged(sender As Object, e As EventArgs) Handles cbScHit.CheckedChanged
        sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Hit, cbScHit.Checked)
    End Sub

    Private Sub cbScNextSplit_CheckedChanged(sender As Object, e As EventArgs) Handles cbScNextSplit.CheckedChanged
        sc.Key_SetState(Shortcuts.SC_Type.SC_Type_Split, cbScNextSplit.Checked)
    End Sub
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sc = CType(Owner, HitCounterManager.Form1).sc
        om = CType(Owner, HitCounterManager.Form1).om
        Dim key As ShortcutsKey

        key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Reset)
        cbScReset.Checked = key.used
        If key.valid Then UpdateKeyName(txtReset, key.key)
        key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Hit)
        cbScHit.Checked = key.used
        If key.valid Then UpdateKeyName(txtHit, key.key)
        key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Split)
        cbScNextSplit.Checked = key.used
        If key.valid Then UpdateKeyName(txtNextSplit, key.key)

        Dim UserCfgFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath

        txtInput.Text = om.FilePathIn
        txtOutput.Text = om.FilePathOut

        If sc.NextStart_Method = Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync Then
            radioHotKeyMethod_sync.Checked = True
            radioHotKeyMethod_async.Checked = False
        ElseIf sc.NextStart_Method = Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async Then
            radioHotKeyMethod_sync.Checked = False
            radioHotKeyMethod_async.Checked = True
        Else
            radioHotKeyMethod_sync.Checked = False
            radioHotKeyMethod_async.Checked = False
        End If

        cbShowAttempts.Checked = om.ShowAttemptsCounter
        cbShowHeadline.Checked = om.ShowHeadline
        cbShowSessionProgress.Checked = om.ShowSessionProgress
        numShowSplitsCountFinished.Value = om.ShowSplitsCountFinished
        numShowSplitsCountUpcoming.Value = om.ShowSplitsCountUpcoming
        cbApCustomCss.Checked = om.StyleUseCustom
        txtCssUrl.Text = om.StyleCssUrl
        txtFontUrl.Text = om.StyleFontUrl
        numStyleDesiredWidth.Value = om.StyleDesiredWidth
        cbApHighContrast.Checked = om.StyleUseHighContrast
    End Sub

    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click
        If FileIO.FileSystem.FileExists(txtInput.Text) Then
            OpenFileDialog1.InitialDirectory = FileIO.FileSystem.GetParentPath(txtInput.Text)
            OpenFileDialog1.FileName = FileIO.FileSystem.GetName(txtInput.Text)
        Else
            OpenFileDialog1.InitialDirectory = Environment.CurrentDirectory
            OpenFileDialog1.FileName = "*.template"
        End If

        OpenFileDialog1.Filter = "Templates (*.template)|*.template|All files (*.*)|*.*"
        OpenFileDialog1.FilterIndex = 0
        If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
            txtInput.Text = OpenFileDialog1.FileName
            om.FilePathIn = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click
        If FileIO.FileSystem.FileExists(txtOutput.Text) Then
            OpenFileDialog1.InitialDirectory = FileIO.FileSystem.GetParentPath(txtOutput.Text)
            OpenFileDialog1.FileName = FileIO.FileSystem.GetName(txtOutput.Text)
        Else
            OpenFileDialog1.InitialDirectory = Environment.CurrentDirectory
            OpenFileDialog1.FileName = "*.html"
        End If

        OpenFileDialog1.Filter = "HTML (*.html)|*.html|All files (*.*)|*.*"
        OpenFileDialog1.FilterIndex = 0
        If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
            txtOutput.Text = OpenFileDialog1.FileName
            om.FilePathOut = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub radioHotKeyMethod_CheckedChanged(sender As Object, e As EventArgs) Handles radioHotKeyMethod_sync.CheckedChanged, radioHotKeyMethod_async.CheckedChanged
        If IsNothing(sc) Then Exit Sub ' when invoked during form load

        If radioHotKeyMethod_sync.Checked Then
            If Not sc.NextStart_Method = Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync Then
                sc.NextStart_Method = Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Sync
                MessageBox.Show("Changes only take effect after restarting the application.", "Restart required", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        ElseIf radioHotKeyMethod_async.Checked Then
            If Not sc.NextStart_Method = Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async Then
                sc.NextStart_Method = Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async
                MessageBox.Show("Changes only take effect after restarting the application.", "Restart required", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnApApply_Click(sender As Object, e As EventArgs) Handles btnApApply.Click
        om.ShowAttemptsCounter = cbShowAttempts.Checked
        om.ShowHeadline = cbShowHeadline.Checked
        om.ShowSessionProgress = cbShowSessionProgress.Checked
        om.ShowSplitsCountFinished = numShowSplitsCountFinished.Value
        om.ShowSplitsCountUpcoming = numShowSplitsCountUpcoming.Value
        om.StyleUseCustom = cbApCustomCss.Checked
        om.StyleCssUrl = txtCssUrl.Text
        om.StyleFontUrl = txtFontUrl.Text
        om.StyleUseHighContrast = cbApHighContrast.Checked
        om.StyleDesiredWidth = numStyleDesiredWidth.Value
        om.Update()
    End Sub

    Private Sub cbApCustomCss_CheckedChanged(sender As Object, e As EventArgs) Handles cbApCustomCss.CheckedChanged
        txtCssUrl.Enabled = cbApCustomCss.Checked
        txtFontUrl.Enabled = cbApCustomCss.Checked
    End Sub

End Class