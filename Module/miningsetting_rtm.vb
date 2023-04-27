'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.IO

Module miningsetting_rtm
    Public Sub Miningsetting()
        Cursor.Current = Cursors.WaitCursor

        Dim pool As String = Form1.ComboBox2.Text

        Dim solo As String = False
        Form1.CheckBox3.Enabled = True

        If Form1.CheckBox3.Checked = True Then
            solo = True
        End If

        If pool = "Raptorhash.com" Then
            Form1.ComboBox4.Items.Clear()
            Dim poolname As String = "Raptorhash.com"
            If File.Exists(localusersetting) Then
                Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(pooldatafile)

                    MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                    MyReader.Delimiters = New String() {","}
                    Dim currentRow As String()
                    While Not MyReader.EndOfData
                        Try
                            currentRow = MyReader.ReadFields()
                            If currentRow(0) = poolname Then
                                If currentRow(1) = "server" Then
                                    If Not Form1.ComboBox4.Items.Contains(currentRow(2)) Then
                                        Form1.ComboBox4.Items.Add(currentRow(2))
                                    End If
                                End If
                            End If
                        Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                            MessageBox.Show("The file pools.dat could not be found." + System.Environment.NewLine + System.Environment.NewLine + "The process is aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End While
                    currentRow = Nothing
                End Using
            End If
            Form1.ComboBox4.SelectedIndex = 0
            Form1.TextBox2.Enabled = False
            If solo = True Then
                Form1.TextBox2.Text = "c=RTM,m=solo"
            Else
                Form1.TextBox2.Text = "c=RTM"
            End If
        End If

        If pool = "Raptoreum.Zone" And Form1.CheckBox3.Checked = False Then
            Form1.ComboBox4.Items.Clear()
            Dim poolname As String = "Raptoreum.Zone"
            If File.Exists(localusersetting) Then
                Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(pooldatafile)

                    MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                    MyReader.Delimiters = New String() {","}
                    Dim currentRow As String()
                    While Not MyReader.EndOfData
                        Try
                            currentRow = MyReader.ReadFields()
                            If currentRow(0) = poolname Then
                                If currentRow(1) = "poolserver" Then
                                    If Not Form1.ComboBox4.Items.Contains(currentRow(2)) Then
                                        Form1.ComboBox4.Items.Add(currentRow(2))
                                    End If
                                End If
                            End If
                        Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                            MessageBox.Show("The file pools.dat could not be found." + System.Environment.NewLine + System.Environment.NewLine + "The process is aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End While
                    currentRow = Nothing
                End Using
            End If

            Form1.ComboBox4.SelectedIndex = 0
            Form1.TextBox2.Enabled = True
            Form1.TextBox2.Text = "x"
            Form1.CheckBox3.Enabled = True
        End If

        If pool = "Raptoreum.Zone" And Form1.CheckBox3.Checked = True Then
            Form1.ComboBox4.Items.Clear()
            Dim poolname As String = "Raptoreum.Zone"
            If File.Exists(localusersetting) Then
                Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(pooldatafile)

                    MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                    MyReader.Delimiters = New String() {","}
                    Dim currentRow As String()
                    While Not MyReader.EndOfData
                        Try
                            currentRow = MyReader.ReadFields()
                            If currentRow(0) = poolname Then
                                If currentRow(1) = "soloserver" Then
                                    If Not Form1.ComboBox4.Items.Contains(currentRow(2)) Then
                                        Form1.ComboBox4.Items.Add(currentRow(2))
                                    End If
                                End If
                            End If
                        Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                            MessageBox.Show("The file pools.dat could not be found." + System.Environment.NewLine + System.Environment.NewLine + "The process is aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End While
                    currentRow = Nothing
                End Using
            End If
            Form1.ComboBox4.SelectedIndex = 0
            Form1.TextBox2.Enabled = True
            Form1.TextBox2.Text = "x"
            Form1.CheckBox3.Enabled = True
        End If

        If pool = "FlockPool" Then
            Form1.ComboBox4.Items.Clear()
            Dim poolname As String = "FlockPool"
            If File.Exists(localusersetting) Then
                Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(pooldatafile)

                    MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                    MyReader.Delimiters = New String() {","}
                    Dim currentRow As String()
                    While Not MyReader.EndOfData
                        Try
                            currentRow = MyReader.ReadFields()
                            If currentRow(0) = poolname Then
                                If currentRow(1) = "server" Then
                                    If Not Form1.ComboBox4.Items.Contains(currentRow(2)) Then
                                        Form1.ComboBox4.Items.Add(currentRow(2))
                                    End If
                                End If
                            End If
                        Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                            MessageBox.Show("The file pools.dat could not be found." + System.Environment.NewLine + System.Environment.NewLine + "The process is aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    End While
                    currentRow = Nothing
                End Using
            End If
            Form1.ComboBox4.SelectedIndex = 0
            Form1.TextBox2.Enabled = True
            Form1.TextBox2.Text = "x"

            Form1.CheckBox3.Checked = False
            Form1.CheckBox3.Enabled = False
        End If
        Cursor.Current = Cursors.Default
    End Sub
End Module
