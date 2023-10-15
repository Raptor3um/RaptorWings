'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.IO
Imports System.Text
Imports Windows.System.Profile

Module usersetting

    Public Function loadusersetting()
        Form1.logging("Modul: loadusersettings: Start")
        If File.Exists(localusersetting) Then
            Form1.logging("Modul: loadusersettings: File found")
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localusersetting)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        If currentRow(0) = "design" Then
                            If currentRow(1) = "2" Then
                                Form1.CheckBox4.Checked = True
                            Else
                                Form1.CheckBox4.Checked = False
                            End If
                        End If

                        If currentRow(0) = "sprache" Then
                            For i As Integer = 0 To Form1.ComboBox9.Items.Count - 1
                                If Form1.ComboBox9.Items(i) = currentRow(1) Then
                                    Form1.ComboBox9.SelectedIndex = i
                                    Exit For
                                End If
                            Next
                        End If

                        If currentRow(0).Contains("Profil") Then
                            Form1.DataGridView3.Rows.Add(currentRow(1), currentRow(2))
                            Form1.ComboBox11.Items.Add(currentRow(1) + " - " + currentRow(2))
                        End If

                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show("Line " & ex.Message & " in usersetting List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Raptorwings will end.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End While
                currentRow = Nothing
            End Using
        End If
        Form1.logging("Modul: loadusersettings: END")
    End Function


    Public Function saveusersetting()
        Form1.logging("Modul: saveusersttings: Start")
        Dim sb = New StringBuilder
        Dim aussehen As String = "1"
        Dim sprache As String = "English"

        If Form1.CheckBox4.Checked = True Then
            aussehen = "design,2"
            sb.AppendLine($"{aussehen}")
        Else
            aussehen = "design,1"
            sb.AppendLine($"{aussehen}")
        End If

        sprache = "sprache," + Form1.ComboBox9.Text
        sb.AppendLine($"{sprache}")
        For i As Integer = 0 To Form1.DataGridView3.Rows.Count - 1
            If Form1.DataGridView3.Item(1, i).Value.ToString = "Default" Then
                Continue For
            End If

            Dim datensatz As String = "Profile," + Form1.DataGridView3.Item(0, i).Value.ToString + "," + Form1.DataGridView3.Item(1, i).Value.ToString
            sb.AppendLine($"{datensatz}")
        Next

        System.IO.File.WriteAllText(localusersetting, sb.ToString)
        MessageBox.Show((Checkxmllanguage("Message3.1").trim), "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Form1.logging("Modul: saveusersttings: END")
    End Function
End Module
