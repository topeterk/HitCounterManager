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

Imports System.IO

' Reads a file, patches it and writes to a new file
Public Class OutModule

    Private _FilePathIn As String
    Private _FilePathOut As String
    Private template As String = ""
    Private dgv As DataGridView

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

    ' Use buffer to create outputfile while patching some data
    Public Sub Update()
        Dim sr As StreamWriter
        Dim IsPatching = False

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
            If InStr(line, "HITCOUNTER_LIST_START") Then
                Dim cells As DataGridViewCellCollection
                Dim title As String
                Dim diff As Integer
                Dim hits As Integer
                Dim PB As Integer
                Dim active As Integer
                For r = 0 To dgv.RowCount - 2 Step 1
                    cells = dgv.Rows.Item(r).Cells
                    title = cells.Item("cTitle").Value
                    If Not IsNothing(title) Then
                        title = title.ToString().Replace("&", "&amp;").Replace(" ", "&nbsp;")
                        title = title.Replace("ä", "&auml;").Replace("ö", "&ouml;").Replace("ü", "&uuml;")
                        title = title.Replace("Ä", "&Auml;").Replace("Ö", "&Ouml;").Replace("Ü", "&Uuml;")
                    End If
                    hits = cells.Item("cHits").Value
                    diff = cells.Item("cDiff").Value
                    PB = cells.Item("cPB").Value
                    If r = dgv.SelectedCells.Item(0).RowIndex Then active = 1 Else active = 0

                    sr.WriteLine("[""" & title & """, " & hits & ", " & PB & ", " & active & "],")
                Next
                IsPatching = True
            ElseIf InStr(line, "HITCOUNTER_LIST_END") Then
                IsPatching = False
            ElseIf Not IsPatching Then
                sr.WriteLine(line.Replace(vbLf, ""))
            End If
        Next
        sr.Close()
    End Sub
End Class
