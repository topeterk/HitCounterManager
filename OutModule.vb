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

Imports System.IO

' Reads a file, patches it and writes to a new file
Public Class OutModule

    Private _FilePathIn As String
    Private _FilePathOut As String
    Private template As String = ""
    Private dgv As DataGridView
    Public AttemptsCount As Integer = 0
    Public ShowAttemptsCounter = True
    Public ShowHeadline = True
    Public ShowSplitsCountFinished As Integer = 999
    Public ShowSplitsCountUpcoming As Integer = 999
    Public StyleUseHighContrast As Boolean = False
    Public StyleUseCustom As Boolean = False
    Public StyleCssUrl As String = ""
    Public StyleFontUrl As String = ""

    ' bind object to a data grid
    Public Sub New(DataGridView As DataGridView)
        dgv = DataGridView
    End Sub

    ' Read a file into buffer
    Public Property FilePathIn() As String
        Get
            Return _FilePathIn
        End Get
        Set(InputFile As String)
            _FilePathIn = InputFile

            If FileIO.FileSystem.FileExists(_FilePathIn) Then
                Dim sr = New StreamReader(_FilePathIn)
                template = sr.ReadToEnd()
            End If
        End Set
    End Property

    ' Output file configuration
    Public Property FilePathOut() As String
        Get
            Return _FilePathOut
        End Get
        Set(OutputFile As String)
            _FilePathOut = OutputFile
        End Set
    End Property

    ' Escapes special HTML characters
    Private Function SimpleHtmlEscape(Str As String)
        If Not IsNothing(Str) Then
            Str = Str.ToString().Replace("&", "&amp;").Replace(" ", "&nbsp;")
            Str = Str.Replace("ä", "&auml;").Replace("ö", "&ouml;").Replace("ü", "&uuml;")
            Str = Str.Replace("Ä", "&Auml;").Replace("Ö", "&Ouml;").Replace("Ü", "&Uuml;")
        End If
        SimpleHtmlEscape = Str
    End Function

    ' Creates a JSON string of a boolean
    Private Function ToJsonBooleanString(ByRef bool As Boolean)
        If bool Then
            ToJsonBooleanString = "true"
        Else
            ToJsonBooleanString = "false"
        End If
    End Function

    ' Use buffer to create outputfile while patching some data
    Public Sub Update()
        Dim sr As StreamWriter
        Dim IsWritingList = False ' Kept for old designs before version 1.10
        Dim IsWritingJson = False

        If IsNothing(_FilePathOut) Then Exit Sub

        Try
            If Not FileIO.FileSystem.FileExists(_FilePathOut) Then
                File.Create(_FilePathOut).Close()
            End If
            sr = New StreamWriter(_FilePathOut)
        Catch ex As Exception
            Exit Sub
        End Try

        sr.NewLine = vbNewLine

        For Each line In template.Split(vbNewLine)
            If IsWritingJson Then
                If InStr(line, "HITCOUNTER_JSON_END") Then IsWritingJson = False
            ElseIf IsWritingList Then
                If InStr(line, "HITCOUNTER_LIST_END") Then IsWritingList = False
            Else
                If InStr(line, "HITCOUNTER_JSON_START") Then ' Format data according to RFC 4627 (JSON)
                    Dim cells As DataGridViewCellCollection
                    Dim title As String
                    Dim diff As Integer
                    Dim hits As Integer
                    Dim PB As Integer
                    Dim active = 0
                    Dim iTemp As Integer
                    Dim sTemp As String

                    sr.WriteLine("{")

                    sr.WriteLine("""list"": [")
                    For r = 0 To dgv.RowCount - 2 Step 1
                        cells = dgv.Rows.Item(r).Cells
                        title = SimpleHtmlEscape(cells.Item("cTitle").Value)
                        hits = cells.Item("cHits").Value
                        diff = cells.Item("cDiff").Value
                        PB = cells.Item("cPB").Value
                        If r = dgv.SelectedCells.Item(0).RowIndex Then active = r

                        If r <> 0 Then sr.WriteLine(",") ' separator
                        sr.Write("[""" & title & """, " & hits & ", " & PB & "]")
                    Next
                    sr.WriteLine("") ' no trailing separator
                    sr.WriteLine("],")

                    sr.WriteLine("""split_active"": " + active.ToString() + ",")
                    iTemp = active - ShowSplitsCountFinished
                    If iTemp < 0 Then iTemp = 0
                    sr.WriteLine("""split_first"": " + iTemp.ToString() + ",")
                    iTemp = active + ShowSplitsCountUpcoming
                    If 999 < iTemp Then iTemp = 999
                    sr.WriteLine("""split_last"": " + iTemp.ToString() + ",")

                    sr.WriteLine("""attempts"": " + AttemptsCount.ToString() + ",")
                    sr.WriteLine("""show_attempts"": " + ToJsonBooleanString(ShowAttemptsCounter) + ",")
                    sr.WriteLine("""show_headline"": " + ToJsonBooleanString(ShowHeadline) + ",")

                    If StyleUseCustom Then sTemp = StyleFontUrl Else sTemp = ""
                    sr.WriteLine("""font_url"": """ + sTemp + """,")
                    If StyleUseCustom Then sTemp = StyleCssUrl Else sTemp = "stylesheet.css"
                    sr.WriteLine("""css_url"": """ + sTemp + """,")
                    sr.WriteLine("""high_contrast"": " + ToJsonBooleanString(StyleUseHighContrast))

                    sr.WriteLine("}")
                    IsWritingJson = True
                ElseIf InStr(line, "HITCOUNTER_LIST_START") Then ' Kept for old designs before version 1.10
                    Dim cells As DataGridViewCellCollection
                    Dim title As String
                    Dim diff As Integer
                    Dim hits As Integer
                    Dim PB As Integer
                    Dim active As Integer
                    For r = 0 To dgv.RowCount - 2 Step 1
                        cells = dgv.Rows.Item(r).Cells
                        title = SimpleHtmlEscape(cells.Item("cTitle").Value)
                        hits = cells.Item("cHits").Value
                        diff = cells.Item("cDiff").Value
                        PB = cells.Item("cPB").Value
                        If r = dgv.SelectedCells.Item(0).RowIndex Then active = 1 Else active = 0

                        sr.WriteLine("[""" & title & """, " & hits & ", " & PB & ", " & active & "],")
                    Next
                    IsWritingList = True
                ElseIf Not IsWritingList And Not IsWritingJson Then
                    sr.WriteLine(line.Replace(vbLf, ""))
                End If
            End If
        Next
        sr.Close()
    End Sub
End Class
