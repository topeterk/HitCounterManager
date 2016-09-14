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

' a row as part of a profile (equals a row at datagridview)
Public Class ProfileRow
    Public Title As String
    Public Hits As Integer
    Public Diff As Integer
    Public PB As Integer

    ' creates an empty entry
    Public Sub New()
        Title = ""
        Hits = 0
        Diff = 0
        PB = 0
    End Sub

    ' creates an entry based on comma separated string
    Public Sub New(Line As String)
        Dim i As Integer
        Dim val As String
        Dim col = 0
        Do
            i = InStr(Line, ";")
            If i = 0 Then Exit Do ' no more data in this lne

            val = Mid(Line, 1, i - 1)
            Select Case (col)
                Case 0
                    Title = val
                Case 1
                    Hits = val
                Case 2
                    Diff = val
                Case 3
                    PB = val
            End Select

            col += 1
            Line = Mid(Line, i + 1)
        Loop
    End Sub
End Class

' single profile (equals a whole datagridview data collection)
Public Class Profile
    Public Name As String
    Public Rows As New List(Of ProfileRow)()

    Public Sub AddNewRow(Line As String)
        Rows.Add(New ProfileRow(Line))
    End Sub
End Class

' manages multiple profiles
Public Class Profiles

    Private _Profiles As New List(Of Profile)()

    ' loads all profiles which are part of a newline, pipe and comma separated string into the internal cache
    Public Sub LoadProfiles(ProfileStr As String)
        Dim line As String
        Dim prof As Profile

        For Each line In ProfileStr.Split(vbNewLine)
            line = line.Replace(vbLf, "")
            If 1 < line.Length Then ' skip empty lines
                If line.EndsWith("|") Then
                    prof = New Profile()
                    prof.Name = line.Replace("|", "")
                    _Profiles.Add(prof)
                ElseIf _Profiles.Count > 0 Then
                    _Profiles.Item(_Profiles.Count - 1).AddNewRow(line)
                End If
            End If
        Next
    End Sub

    ' builds a newline, pipe and comma separated string for all internally cached profiles
    Public Function GetProfilesString() As String
        Dim ProfileStr = ""

        For Each prof In _Profiles
            ProfileStr += prof.Name & "|" & vbNewLine
            For Each row In prof.Rows
                ProfileStr += row.Title & ";" & row.Hits & ";" & row.Diff & ";" & row.PB & ";" & vbNewLine
            Next row
        Next prof

        GetProfilesString = ProfileStr
    End Function

    ' returns a list of all available internally cached profile names
    Public Function GetProfileList() As Object()
        Dim ret = New List(Of String)
        For Each prof In _Profiles
            ret.Add(prof.Name)
        Next
        GetProfileList = ret.ToArray()
    End Function

    ' updates a datagrid based on a specific internally cached profile
    Public Sub LoadProfileInto(Name As String, DataGridView As DataGridView)
        For Each prof In _Profiles
            If prof.Name = Name Then

                With DataGridView
                    .Rows.Clear()
                    For Each row In prof.Rows
                        Dim cells() As String = {row.Title, row.Hits, row.Diff, row.PB}
                        .Rows.Add(cells)
                    Next row
                End With

                Exit Sub ' done
            End If
        Next prof
    End Sub

    ' updates internal profile cache of a specific profile by reading data from datagrid
    Public Sub SaveProfileFrom(Name As String, DataGridView As DataGridView, Optional AllowCreation As Boolean = False)
        Dim prof_save As Profile = Nothing
        Dim cells As DataGridViewCellCollection
        Dim ProfileRow As ProfileRow

        ' look for existing one
        For Each prof In _Profiles
            If prof.Name = Name Then
                prof_save = prof
            End If
        Next prof

        ' create if not exists
        If IsNothing(prof_save) Then
            If Not AllowCreation Then Exit Sub
            prof_save = New Profile()
            prof_save.Name = Name
            _Profiles.Add(prof_save)
        End If

        prof_save.Rows.Clear()

        ' collecting data, nom nom nom
        With DataGridView
            For r = 0 To .RowCount - 2 Step 1
                ProfileRow = New ProfileRow()
                cells = .Rows.Item(r).Cells
                ProfileRow.Title = cells.Item("cTitle").Value
                ProfileRow.Hits = cells.Item("cHits").Value
                ProfileRow.Diff = cells.Item("cDiff").Value
                ProfileRow.PB = cells.Item("cPB").Value
                prof_save.Rows.Add(ProfileRow)
            Next
        End With
    End Sub

    ' removes a specific profile from internal cache
    Public Sub DeleteProfile(Name As String)
        For Each prof In _Profiles
            If prof.Name = Name Then
                _Profiles.Remove(prof)
                Exit Sub
            End If
        Next
    End Sub

    ' renames a specific profile in internal cache
    Public Sub RenameProfile(NameOld As String, NameNew As String)
        For Each prof In _Profiles
            If prof.Name = NameOld Then
                prof.Name = NameNew
                Exit Sub
            End If
        Next
    End Sub
End Class
