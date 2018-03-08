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

<Serializable()> Public Class SettingsRoot
    Public Version As Integer
    Public MainWidth As Integer
    Public MainHeight As Integer
    Public HotKeyMethod As Integer
    Public ShortcutResetEnable As Boolean
    Public ShortcutResetKeyCode As Integer
    Public ShortcutHitEnable As Boolean
    Public ShortcutHitKeyCode As Integer
    Public ShortcutSplitEnable As Boolean
    Public ShortcutSplitKeyCode As Integer
    Public Inputfile As String
    Public OutputFile As String
    Public ShowSplitsCountFinished As Integer
    Public ShowSplitsCountUpcoming As Integer
    Public StyleUseHighContrast As Boolean
    Public StyleUseCustom As Boolean
    Public StyleCssUrl As String
    Public StyleFontUrl As String
    Public ProfileSelected As String
    Public Profiles As Profiles
End Class

Partial Public Class Form1
    Private sm As SaveModule(Of SettingsRoot)
    Private _settings As SettingsRoot

    Private Sub LoadSettings()
        Dim key = New ShortcutsKey

        sm = New SaveModule(Of SettingsRoot)(Application.ProductName & "Save.xml")
        _settings = sm.ReadXML()

        If IsNothing(_settings) Then
            _settings = New SettingsRoot()

            ' prepare defaults..
            _settings.Version = 0
            _settings.MainWidth = 559
            _settings.MainHeight = 723
            _settings.HotKeyMethod = Shortcuts.SC_HotKeyMethod.SC_HotKeyMethod_Async
            _settings.ShortcutResetEnable = False
            _settings.ShortcutResetKeyCode = 65653 ' F6
            _settings.ShortcutHitEnable = False
            _settings.ShortcutHitKeyCode = 65654 ' F7
            _settings.ShortcutSplitEnable = False
            _settings.ShortcutSplitKeyCode = 65655 ' F8
            _settings.Inputfile = "HitCounter.template"
            _settings.OutputFile = "HitCounter.html"
            _settings.ProfileSelected = "Unnamed"
            _settings.Profiles = New Profiles()
        End If
        If _settings.Version = 0 Then ' Coming from version 1.9 or older
            _settings.Version = 1
            _settings.ShowSplitsCountFinished = 999
            _settings.ShowSplitsCountUpcoming = 999
            _settings.StyleUseHighContrast = False
            _settings.StyleUseCustom = False
            _settings.StyleCssUrl = "stylesheet_pink.css"
            _settings.StyleFontUrl = "https://fonts.googleapis.com/css?family=Fontdiner%20Swanky"
        End If

        'Apply settings..
        sc = New Shortcuts(Me.Handle, _settings.HotKeyMethod)
        om = New OutModule(DataGridView1)
        profs = _settings.Profiles

        ComboBox1.Items.AddRange(profs.GetProfileList)
        If ComboBox1.Items.Count = 0 Then
            ComboBox1.Items.Add("Unnamed")
        End If
        ComboBox1.SelectedItem = _settings.ProfileSelected

        key.key = New KeyEventArgs(_settings.ShortcutResetKeyCode)
        If _settings.ShortcutResetEnable Then
            sc.Key_Set(Shortcuts.SC_Type.SC_Type_Reset, key)
        Else
            sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_Reset, key)
        End If
        key.key = New KeyEventArgs(_settings.ShortcutHitKeyCode)
        If _settings.ShortcutHitEnable Then
            sc.Key_Set(Shortcuts.SC_Type.SC_Type_Hit, key)
        Else
            sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_Hit, key)
        End If
        key.key = New KeyEventArgs(_settings.ShortcutSplitKeyCode)
        If _settings.ShortcutSplitEnable Then
            sc.Key_Set(Shortcuts.SC_Type.SC_Type_Split, key)
        Else
            sc.Key_PreSet(Shortcuts.SC_Type.SC_Type_Split, key)
        End If

        DataGridView1.Rows.Item(0).Selected = True
        DataGridView1_CellValueChanged(Nothing, Nothing)

        If _settings.MainWidth > 400 Then MyClass.Width = _settings.MainWidth
        If _settings.MainHeight > 400 Then MyClass.Height = _settings.MainHeight

        om.FilePathIn = _settings.Inputfile
        om.FilePathOut = _settings.OutputFile
        om.ShowSplitsCountFinished = _settings.ShowSplitsCountFinished
        om.ShowSplitsCountUpcoming = _settings.ShowSplitsCountUpcoming
        om.StyleUseHighContrast = _settings.StyleUseHighContrast
        om.StyleUseCustom = _settings.StyleUseCustom
        om.StyleCssUrl = _settings.StyleCssUrl
        om.StyleFontUrl = _settings.StyleFontUrl

    End Sub

    Private Sub SaveSettings()
        Dim key = New ShortcutsKey

        _settings.MainWidth = MyClass.Width
        _settings.MainHeight = MyClass.Height
        _settings.HotKeyMethod = sc.NextStart_Method
        key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Reset)
        _settings.ShortcutResetEnable = key.used
        _settings.ShortcutResetKeyCode = key.key.KeyData
        key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Hit)
        _settings.ShortcutHitEnable = key.used
        _settings.ShortcutHitKeyCode = key.key.KeyData
        key = sc.Key_Get(Shortcuts.SC_Type.SC_Type_Split)
        _settings.ShortcutSplitEnable = key.used
        _settings.ShortcutSplitKeyCode = key.key.KeyData
        _settings.Inputfile = om.FilePathIn
        _settings.OutputFile = om.FilePathOut
        _settings.ShowSplitsCountFinished = om.ShowSplitsCountFinished
        _settings.ShowSplitsCountUpcoming = om.ShowSplitsCountUpcoming
        _settings.StyleUseHighContrast = om.StyleUseHighContrast
        _settings.StyleUseCustom = om.StyleUseCustom
        _settings.StyleCssUrl = om.StyleCssUrl
        _settings.StyleFontUrl = om.StyleFontUrl
        _settings.ProfileSelected = ComboBox1.SelectedItem
        _settings.Profiles = profs
        sm.WriteXML(_settings)

    End Sub

End Class
