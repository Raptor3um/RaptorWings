'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Text

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.WaitCursor

        checkupdates.checkRTWupdate()

        Me.TextBox1.Text = Environment.MachineName

        If Not My.Computer.FileSystem.DirectoryExists(localfolder) Then
            My.Computer.FileSystem.CreateDirectory(localfolder)
        End If

        If File.Exists(localwingsheet) Then
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        Me.ComboBox6.Items.Add(currentRow(0))
                        Me.ComboBox7.Items.Add(currentRow(0))
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show("Line " & ex.Message & " in Wingsheet List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Raptorwings will end.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End While
                currentRow = Nothing
            End Using
        End If

        Dim corenumbers As Integer = Environment.ProcessorCount
        Me.ComboBox5.Items.Add("Default")
        For i As Integer = 1 To corenumbers
            Me.ComboBox5.Items.Add(i)
        Next
        corenumbers = Nothing

        If ComboBox1.Items.Count - 1 >= 0 Then
            Me.ComboBox1.SelectedIndex = 0
        End If
        Me.ComboBox2.SelectedIndex = 0
        Me.ComboBox3.SelectedIndex = 0
        Me.ComboBox4.SelectedIndex = 0
        Me.ComboBox5.SelectedIndex = 0
        Me.ComboBox6.SelectedIndex = 0
        Me.ComboBox7.SelectedIndex = 0
        Me.ComboBox10.SelectedIndex = 0

        Me.ComboBox11.SelectedIndex = 0

        Readbalance()
        Timer2.Start()

        Me.DataGridView3.Rows.Add("1", "Default")

        Languagesxmlload()
        FindLandguage()

        If File.Exists(loadusersetting) Then
            loadusersetting()
        Else
            Languagecontrolls()
        End If

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
        If Me.Button2.Enabled = True Then
            Dim msgtext1 As String = Checkxmllanguage("Message33.1").trim

            Dim result = MessageBox.Show(msgtext1, "Questtion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                saveWalletList.saveWalletList()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.DataGridView1.Rows.Count - 1 = -1 Then
            Me.DataGridView1.Rows.Add("1", "", "", "")
        Else
            Me.DataGridView1.Rows.Add(Me.DataGridView1.Item(0, Me.DataGridView1.Rows.Count - 1).Value.ToString + 1, "", "", "")
        End If
        Me.Button2.Enabled = True
        MessageBox.Show(Checkxmllanguage("Message18.1").trim, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseHover
        Me.ToolTip1.SetToolTip(Button1, Checkxmllanguage("Button1").trim)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        saveWalletList.saveWalletList()
    End Sub
    Private Sub Button2_MouseHover(sender As Object, e As EventArgs) Handles Button2.MouseHover
        Me.ToolTip1.SetToolTip(Button2, Checkxmllanguage("Button2").trim)
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Cursor.Current = Cursors.WaitCursor

        Dim selectRowDGV1 As Integer = Me.DataGridView1.CurrentCell.RowIndex.ToString
        Dim newwalletadress As String = Me.DataGridView1.Item(1, selectRowDGV1).Value.ToString
        For i As Integer = 0 To selectRowDGV1 - 1
            If Me.DataGridView1.Item(1, i).Value.ToString = newwalletadress Then
                MessageBox.Show(Checkxmllanguage("Message31.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(selectRowDGV1))
                Exit Sub
            End If
        Next

        Dim walletadress As String = Me.DataGridView1.Item(1, Me.DataGridView1.Rows.Count - 1).Value.ToString
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim client As New WebClient
        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")

        Dim response As String = client.DownloadString(apiwalletbalanceurl + walletadress)
        If response = "{}" Then
            MessageBox.Show((Checkxmllanguage("Message17.1").trim), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.DataGridView1.Item(1, Me.DataGridView1.Rows.Count - 1).Value = ""
            Exit Sub
        End If

        Me.ComboBox1.Items.Clear()
        For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
            Me.ComboBox1.Items.Add(Me.DataGridView1.Item(0, i).Value.ToString + " - " + Me.DataGridView1.Item(2, i).Value.ToString + " (" + Me.DataGridView1.Item(1, i).Value.ToString + ")")
        Next

        Me.Button2.Enabled = True
        Timer1.Stop()
        Readbalance()
        Timer1.Start()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Button3.Visible = False
        Me.Label5.Visible = False
        Me.Label11.Visible = False
        Me.Panel1.Visible = True
        Me.ComboBox1.Items.Clear()
        If Me.DataGridView1.Rows.Count - 1 >= 0 Then
            For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
                Me.ComboBox1.Items.Add(Me.DataGridView1.Item(0, i).Value.ToString + " - " + Me.DataGridView1.Item(2, i).Value.ToString + " (" + Me.DataGridView1.Item(1, i).Value.ToString + ")")
            Next
            Me.ComboBox1.SelectedIndex = 0
            Me.ComboBox1.Refresh()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Timer4.Stop()
        Me.Timer5.Stop()
        Me.Timer4.Interval = 12000000
        Me.Timer5.Interval = 1200000

        For Each p In Diagnostics.Process.GetProcesses()
            If p.ProcessName = "SRBMiner-MULTI" And Me.Button4.BackColor = Color.YellowGreen Then
                MessageBox.Show(Checkxmllanguage("Message32.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Next

        If Me.Button4.BackColor = Color.PaleVioletRed Then
            For Each Process In System.Diagnostics.Process.GetProcessesByName("SRBMiner-MULTI")
                Process.Kill()
            Next
            Exit Sub
        End If

        Dim msgtext1 As String = Checkxmllanguage("Message4.1").trim
        Dim msgtext2 As String = Checkxmllanguage("Message4.2").trim

        Dim result = MessageBox.Show(msgtext1, msgtext2, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.No Then
            Exit Sub
        End If

        If Me.ComboBox3.Text = "SRBMiner-MULTI" Then
            If Not File.Exists(selfpath + "mining\" + SRBdirectory + "\SRBMiner-MULTI.exe") Then
                Cursor.Current = Cursors.WaitCursor
                If Not Directory.Exists(selfpath + "mining\") Then
                    Directory.CreateDirectory(selfpath + "mining\")
                End If

                Dim downloadpath As String = selfpath + "mining\" + SRBMinerDownloadnameWindows
                Dim client As New Net.WebClient
                client.DownloadFile(SRBMinerDownloadpathWinows, downloadpath)

                If File.Exists(selfpath + "mining\" + SRBMinerDownloadnameWindows) Then
                    ZipFile.ExtractToDirectory(selfpath + "mining\" + SRBMinerDownloadnameWindows, selfpath + "mining\")
                    File.Delete(selfpath + "mining\" + SRBMinerDownloadnameWindows)
                End If
                Cursor.Current = Cursors.Default
            End If
        End If

        If Me.ComboBox1.Text = Nothing Then
            MessageBox.Show(Checkxmllanguage("Message5.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim walletsplitt() As String = Me.ComboBox1.Text.Split("(")
        Dim wallet As String = walletsplitt(1).Replace(")", "")

        Dim server As String = Me.ComboBox4.Text
        Dim rig As String = Me.TextBox1.Text
        Dim password As String = Me.TextBox2.Text

        Dim threads As String = Me.ComboBox5.Text
        If threads = "Default" Then
            threads = ""
        Else
            threads = " --cpu-threads " & Me.ComboBox5.Items.Count - 1
        End If

        Dim wingsheet_main As String = Nothing
        Dim wingsheet_srb01 As String = Nothing

        wingsheet_srb01 = Chr(34) & selfpath & "mining\" + SRBdirectory + "\SRBMiner-MULTI.exe" & Chr(34) & " --disable-gpu" & threads & " --algorithm ghostrider --pool " & server & " --wallet " & wallet & "." & rig & " --password " & password

        Dim wingsheet_donation As String
        wingsheet_donation = Chr(34) & selfpath & "mining\" + SRBdirectory + "\SRBMiner-MULTI.exe" & Chr(34) & " --disable-gpu" & threads & " --algorithm ghostrider --pool stratum+tcps://europe.raptoreum.zone:4444 --wallet " & donationadress & ".Donation_" & rig & " --password x"

        If Me.CheckBox1.Checked = True Then
            wingsheet_srb01 += " --background"
            wingsheet_donation += " --background"
        End If

        If Not Me.ComboBox10.Text = "Default" Then
            wingsheet_srb01 += " --cpu-threads-priority " + Me.ComboBox10.Text
            wingsheet_donation += " --cpu-threads-priority " + Me.ComboBox10.Text
        End If

        If Me.ComboBox2.Text = "Raptorhash.com" Then
            wingsheet_main = wingsheet_srb01
        End If

        If Me.ComboBox2.Text = "Raptoreum.Zone" Then
            wingsheet_main = wingsheet_srb01
        End If

        If Me.ComboBox2.Text = "FlockPool" Then
            wingsheet_main = wingsheet_srb01
        End If

        Dim filewriter As System.IO.StreamWriter
        filewriter = My.Computer.FileSystem.OpenTextFileWriter(selfpath + "mining\" + SRBdirectory + "\rtmtsheet.bat", False, Encoding.Default)
        filewriter.Write("@ " & wingsheet_main)
        filewriter.Close()
        Process.Start(selfpath + "mining\" + SRBdirectory + "\rtmtsheet.bat")

        If Me.CheckBox2.Checked = True Then
            End
        End If

        If CheckBox5.Checked = True Then
            filewriter = My.Computer.FileSystem.OpenTextFileWriter(selfpath + "mining\" + SRBdirectory + "\donation.bat", False, Encoding.Default)
            filewriter.Write("@ " & wingsheet_donation)
            filewriter.Close()
            Timer4.Start()
        End If

    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Miningsetting()
    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Miningsetting()
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        Cursor.Current = Cursors.WaitCursor

        If Me.ComboBox6.Text = "Default" Then
            If Me.DataGridView1.Rows.Count - 1 >= 0 Then
                Me.ComboBox1.Text = Me.DataGridView1.Item(2, 0).Value.ToString + " (" + Me.DataGridView1.Item(1, 0).Value.ToString + ")"
            End If
            Me.ComboBox2.Text = def_ps
            Me.ComboBox3.Text = def_m
            Me.ComboBox4.Text = def_s
            Me.ComboBox5.Text = def_c
            Me.ComboBox10.Text = "Default"
            Me.TextBox2.Text = def_pw
            Me.TextBox3.Text = ""
            Me.CheckBox3.Enabled = True
            Me.CheckBox1.CheckState = CheckState.Unchecked
            Me.CheckBox2.CheckState = CheckState.Unchecked
            If def_solo = False Then
                Me.CheckBox3.CheckState = CheckState.Unchecked
                Me.CheckBox3.Enabled = False
                Exit Sub
            End If
        End If

        Dim wingsheetname As String = Me.ComboBox6.Text

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

            MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader.Delimiters = New String() {","}
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()

                    If currentRow(0) = wingsheetname Then
                        Me.TextBox3.Text = currentRow(0)

                        Dim comboboxindex As Integer = 0
                        For i As Integer = 0 To ComboBox1.Items.Count - 1
                            Dim comboboxtext As String = Me.ComboBox1.Items(i).ToString
                            If comboboxtext.Contains(currentRow(1)) Then
                                ComboBox1.SelectedIndex = i
                            End If
                        Next

                        Me.ComboBox2.Text = currentRow(2)
                        Me.ComboBox4.Text = currentRow(3)
                        Me.TextBox1.Text = currentRow(4)
                        Me.TextBox2.Text = currentRow(5)
                        Me.ComboBox3.Text = currentRow(6)
                        Me.ComboBox5.Text = currentRow(7)
                        Me.ComboBox10.Text = "Default"
                        Me.ComboBox10.Text = currentRow(12)

                        If currentRow(8) = True Then
                            Me.CheckBox1.CheckState = CheckState.Checked
                        Else
                            Me.CheckBox1.CheckState = CheckState.Unchecked
                        End If

                        If currentRow(9) = True Then
                            Me.CheckBox2.CheckState = CheckState.Checked
                        Else
                            Me.CheckBox2.CheckState = CheckState.Unchecked
                        End If

                        If currentRow(10) = True Then
                            Me.CheckBox3.CheckState = CheckState.Checked
                        Else
                            Me.CheckBox3.CheckState = CheckState.Unchecked
                        End If
                    End If

                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show("Line " & ex.Message & " in Wingsheet List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Progress ends.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Cursor.Current = Cursors.Default
                    Exit Sub
                End Try
            End While
        End Using

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Me.TextBox3.Text = "" Or Me.TextBox3.Text = Nothing Or Me.TextBox3.Text = " " Then
            MessageBox.Show(Checkxmllanguage("Message6.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.TextBox1.Text = "" Or Me.TextBox1.Text = Nothing Or Me.TextBox1.Text = " " Then
            MessageBox.Show(Checkxmllanguage("Message7.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.TextBox3.Text = "Default" Then
            MessageBox.Show(Checkxmllanguage("Message9.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If


        Dim wingsheet As String
        Dim wingsheetname As String = Me.TextBox3.Text

        Dim wallet As String
        Dim walletsplitt() As String = Me.ComboBox1.Text.Split("(")
        wallet = walletsplitt(1).Replace(")", "")

        Dim pool As String = Me.ComboBox2.Text
        Dim server As String = ComboBox4.Text
        Dim rigname As String = Me.TextBox1.Text
        Dim password As String = Me.TextBox2.Text
        Dim miner As String = Me.ComboBox3.Text
        Dim corenumber As String = Me.ComboBox5.Text
        Dim priority As String = Me.ComboBox10.Text
        Dim check1 As String
        If Me.CheckBox1.CheckState = CheckState.Checked Then
            check1 = True
        Else
            check1 = False
        End If
        Dim check2 As String
        If Me.CheckBox2.CheckState = CheckState.Checked Then
            check2 = True
        Else
            check2 = False
        End If
        Dim check3 As String
        If Me.CheckBox3.CheckState = CheckState.Checked Then
            check3 = True
        Else
            check3 = False
        End If
        wingsheet = Chr(34) + wingsheetname + Chr(34) + "," + Chr(34) + wallet + Chr(34) + "," + Chr(34) + pool + Chr(34) + "," + Chr(34) + server + Chr(34) + "," + Chr(34) + rigname + Chr(34) + "," + Chr(34) + password + Chr(34) + "," + Chr(34) + miner + Chr(34) + "," + Chr(34) + corenumber + Chr(34) + "," + Chr(34) + check1 + Chr(34) + "," + Chr(34) + check2 + Chr(34) + "," + Chr(34) + check3 + Chr(34) + "," + Chr(34) + "Donattion" + Chr(34) + "," + Chr(34) + priority + Chr(34)
        Dim dataset As New StringBuilder

        Dim wingsheetcheck As String = False


        If File.Exists(localwingsheet) Then
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        If currentRow(0) = wingsheetname Then
                            dataset.AppendLine(wingsheet)
                            wingsheetcheck = True
                        Else
                            Dim dataline As String = Nothing
                            For i = 0 To currentRow.Length - 1
                                dataline = dataline + Chr(34) + currentRow(i) + Chr(34) + ","
                            Next
                            dataset.AppendLine(dataline)
                        End If
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show("Line " & ex.Message & " in Wingsheet List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Progress ends.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Cursor.Current = Cursors.Default
                        Exit Sub
                    End Try
                End While
            End Using
        End If

        If wingsheetcheck = False Then
            dataset.AppendLine(wingsheet)
        End If

        System.IO.File.WriteAllText(localwingsheet, dataset.ToString)

        If Not Me.ComboBox6.Items.Contains(wingsheetname) Then
            Me.ComboBox6.Items.Add(wingsheetname)
        End If
        If Not Me.ComboBox7.Items.Contains(wingsheetname) Then
            Me.ComboBox7.Items.Add(wingsheetname)
        End If

        For i As Integer = 0 To ComboBox6.Items.Count - 1
            If Me.ComboBox6.Items(i).ToString = wingsheetname Then
                Me.ComboBox6.SelectedIndex = i
            End If
        Next

        MessageBox.Show(Checkxmllanguage("Message8.1").trim, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
    Private Sub Button5_MouseHover(sender As Object, e As EventArgs) Handles Button5.MouseHover
        Me.ToolTip1.SetToolTip(Button5, Checkxmllanguage("Button5").trim)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim wingsheetname As String = Me.TextBox3.Text

        If wingsheetname = "Default" Then
            MessageBox.Show(Checkxmllanguage("Message9.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.TextBox3.Text = "" Or Me.TextBox3.Text = Nothing Or Me.TextBox3.Text = " " Then
            MessageBox.Show(Checkxmllanguage("Message6.1").trim)
            Exit Sub
        End If

        Dim msgtext1 As String = Checkxmllanguage("Message10.1").trim
        Dim msgtext2 As String = Checkxmllanguage("Message10.2").trim

        Dim result = MessageBox.Show(msgtext1, msgtext2, MessageBoxButtons.YesNo)
        If result = DialogResult.No Then
            Exit Sub
        End If

        Dim dataset As New StringBuilder

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

            MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader.Delimiters = New String() {","}
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    If currentRow(0) = wingsheetname Then
                        Continue While
                    Else
                        Dim dataline As String = Nothing
                        For i = 0 To currentRow.Length - 1
                            dataline = dataline + Chr(34) + currentRow(i) + Chr(34) + ","
                        Next
                        dataset.AppendLine(dataline)
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MessageBox.Show("Line " & ex.Message & " in Wingsheet List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Progress ends.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Cursor.Current = Cursors.Default
                    Exit Sub
                End Try
            End While
        End Using

        System.IO.File.WriteAllText(localwingsheet, dataset.ToString)

        Me.ComboBox6.Items.Remove(wingsheetname)

        Me.ComboBox7.Items.Remove(wingsheetname)

        If ComboBox1.Items.Count - 1 >= 0 Then
            Me.ComboBox1.SelectedIndex = 0
        End If
        Me.ComboBox2.SelectedIndex = 0
        Me.ComboBox3.SelectedIndex = 0
        Me.ComboBox4.SelectedIndex = 0
        Me.ComboBox5.SelectedIndex = 0
        Me.ComboBox6.SelectedIndex = 0
        Me.TextBox3.Text = ""
        Me.CheckBox1.CheckState = CheckState.Checked
        Me.CheckBox2.CheckState = CheckState.Unchecked
        Me.CheckBox3.CheckState = CheckState.Unchecked


        MessageBox.Show(Checkxmllanguage("Message11.1").trim, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub Button6_MouseHover(sender As Object, e As EventArgs) Handles Button6.MouseHover
        Me.ToolTip1.SetToolTip(Button6, Checkxmllanguage("Button6").trim)
    End Sub

    Private Sub TabPage1_Enter(sender As Object, e As EventArgs) Handles TabPage1.Enter
        Timer1.Start()
    End Sub

    Private Sub TabPage1_Leave(sender As Object, e As EventArgs) Handles TabPage1.Leave
        Timer1.Stop()
    End Sub
    Private Sub TabPage2_Enter(sender As Object, e As EventArgs) Handles TabPage2.Enter
        Timer1.Start()
    End Sub

    Private Sub TabPage2_Leave(sender As Object, e As EventArgs) Handles TabPage2.Leave
        If Me.Button2.Enabled = True Then
            Dim msgtext1 As String = Checkxmllanguage("Message33.1").trim

            Dim result = MessageBox.Show(msgtext1, "Questtion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                saveWalletList.saveWalletList()
            End If
        End If
        Timer1.Stop()
    End Sub

    Private Sub TabPage5_Enter(sender As Object, e As EventArgs) Handles TabPage5.Enter
        Cursor.Current = Cursors.WaitCursor

        Me.DataGridView2.Rows.Clear()
        Me.ComboBox8.Items.Clear()

        If My.Computer.FileSystem.FileExists(localdevice) Then
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localdevice)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        Me.DataGridView2.Rows.Add(False, currentRow(0), currentRow(1), "", currentRow(2), currentRow(3), currentRow(4), currentRow(5), currentRow(6), "", "", "")
                        Me.ComboBox8.Items.Add(currentRow(0) & " {" & currentRow(1) & "}")
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show("Line " & ex.Message & " in Device List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Progress ends.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Cursor.Current = Cursors.Default
                        Exit Sub
                    End Try
                End While
            End Using
        End If

        If My.Computer.FileSystem.FileExists(localpool) Then
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localpool)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
                            If Me.DataGridView2.Item(2, i).Value.ToString = currentRow(0) Then
                                Me.DataGridView2.Item(3, i).Value = "Connecting to " & currentRow(1) & " API"
                                Me.DataGridView2.Item(9, i).Value = currentRow(1)
                                Me.DataGridView2.Item(10, i).Value = currentRow(2)
                                Me.DataGridView2.Item(11, i).Value = currentRow(3)
                            End If
                        Next
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show("Line " & ex.Message & " in Device Pool List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Progress ends.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Cursor.Current = Cursors.Default
                        Exit Sub
                    End Try
                End While
            End Using
        End If

        Apipoolread()
        Timer3.Start()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub TabPage5_Leave(sender As Object, e As EventArgs) Handles TabPage5.Leave
        Timer3.Stop()
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim wallet As String
        If Me.DataGridView1.Rows.Count - 1 >= 0 Then
            wallet = Me.DataGridView1.Item(2, 0).Value.ToString
        Else
            wallet = "No Wallet Found"
        End If

        If Me.ComboBox7.Text = "Default" Then
            Me.RichTextBox1.Text = "WingSheet: Default" & System.Environment.NewLine &
                                   "Wallet: " & wallet & System.Environment.NewLine &
                                   "Poolserver: " & def_ps & System.Environment.NewLine &
                                   "Straum: " & def_s & System.Environment.NewLine &
                                   "Solo: No" & System.Environment.NewLine &
                                   "Password: " & def_pw & System.Environment.NewLine &
                                   "Miner: " & def_m & System.Environment.NewLine &
                                   "Cores: " & def_c
            Exit Sub
        End If

        Dim wingsheettext As String = Me.ComboBox7.Text

        If My.Computer.FileSystem.FileExists(localwingsheet) Then
            Dim file As System.IO.StreamReader
            file = My.Computer.FileSystem.OpenTextFileReader(localwingsheet)
            Dim line As String

            Do While Not file.EndOfStream
                line = file.ReadLine

                If line = Nothing Then
                    Continue Do
                End If

                Dim linesplitt() As String = line.Split(";")

                If linesplitt(0) = wingsheettext Then
                    For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
                        If Me.DataGridView1.Item(1, i).Value.ToString = linesplitt(1) Then
                            wallet = Me.DataGridView1.Item(2, i).Value.ToString
                            file.Close()
                            Exit For
                        End If
                    Next
                    Me.RichTextBox1.Text = "WingSheet: " & linesplitt(0) & System.Environment.NewLine &
                                   "Wallet: " & wallet & System.Environment.NewLine &
                                   "Poolserver: " & linesplitt(2) & System.Environment.NewLine &
                                   "Straum: " & linesplitt(3) & System.Environment.NewLine &
                                   "Solo: " & linesplitt(10) & System.Environment.NewLine &
                                   "Password: " & linesplitt(5) & System.Environment.NewLine &
                                   "Miner: " & linesplitt(6) & System.Environment.NewLine &
                                   "Cores: " & linesplitt(7)
                    Exit Sub
                End If
            Loop
            file.Close()
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Readbalance()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            Dim listProc() As System.Diagnostics.Process
            listProc = System.Diagnostics.Process.GetProcessesByName("SRBMiner-MULTI")
            If listProc.Length > 0 Then
                Me.Button4.BackColor = Color.PaleVioletRed
                Me.Button4.Text = "Stop Miner"
            Else
                Me.Button4.BackColor = Color.YellowGreen
                Me.Button4.Text = "Start Miner"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.ComboBox8.Text = ""
        Me.TextBox4.Text = ""
        Me.TextBox5.Text = ""
        Me.TextBox6.Text = ""
        Me.TextBox7.Text = "22"
        Me.TextBox8.Text = ""
        Me.TextBox9.Text = ""
        Me.TextBox11.Text = "/home/"
        Me.Label30.Text = "save"
    End Sub

    Private Sub Button9_MouseHover(sender As Object, e As EventArgs) Handles Button9.MouseHover
        Me.ToolTip1.SetToolTip(Button9, Checkxmllanguage("Button9").trim)
    End Sub
    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If Me.TextBox4.Text = Nothing Or Me.TextBox4.Text = "" Or Me.TextBox4.Text = " " Then
            Me.TextBox4.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox4.BackColor = Color.White
            Me.Button8.Enabled = True
        End If

        For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
            Dim devicename As String = Me.DataGridView2.Item(1, i).Value
            If Me.TextBox4.Text = devicename Then
                Me.Button7.Enabled = True
            Else
                Me.Button7.Enabled = False
            End If
        Next

        Me.TextBox5.Text = Me.TextBox4.Text

        If Me.Label30.Text = "save" Then
            For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
                Dim devicename As String = Me.DataGridView2.Item(1, i).Value
                If Me.TextBox4.Text = devicename Then
                    Me.TextBox4.BackColor = Color.PaleVioletRed
                    Me.Button8.Enabled = False
                    Exit Sub
                Else
                    Me.TextBox4.BackColor = Color.White
                    Me.Button8.Enabled = True
                End If
            Next
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If Me.TextBox5.Text = Nothing Or Me.TextBox5.Text = "" Or Me.TextBox5.Text = " " Then
            Me.TextBox5.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox5.BackColor = Color.White
            Me.Button8.Enabled = True
        End If

        If Me.Label30.Text = "save" Then
            For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
                Dim rigname As String = Me.DataGridView2.Item(2, i).Value
                If Me.TextBox5.Text = rigname Then
                    Me.TextBox5.BackColor = Color.PaleVioletRed
                    Me.Button8.Enabled = False
                    Exit Sub
                Else
                    Me.TextBox5.BackColor = Color.White
                    Me.Button8.Enabled = True
                End If
            Next
        End If
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If Me.TextBox6.Text = Nothing Or Me.TextBox6.Text = "" Or Me.TextBox6.Text = " " Then
            Me.TextBox6.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox6.BackColor = Color.White
            Me.Button8.Enabled = True
        End If

        If Me.Label30.Text = "save" Then
            For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
                Dim ipadress As String = Me.DataGridView2.Item(4, i).Value
                If Me.TextBox6.Text = ipadress Then
                    Me.TextBox6.BackColor = Color.PaleVioletRed
                    Me.Button8.Enabled = False
                    Exit Sub
                Else
                    Me.TextBox6.BackColor = Color.White
                    Me.Button8.Enabled = True
                End If
            Next
        End If
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        If Me.TextBox7.Text = Nothing Or Me.TextBox7.Text = "" Or Me.TextBox7.Text = " " Then
            Me.TextBox7.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox7.BackColor = Color.White
            Me.Button8.Enabled = True
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If Me.TextBox8.Text = Nothing Or Me.TextBox8.Text = "" Or Me.TextBox8.Text = " " Then
            Me.TextBox8.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox8.BackColor = Color.White
            Me.Button8.Enabled = True
        End If

        Me.TextBox11.Text = "/home/" & Me.TextBox8.Text & "/mining/"
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If Me.TextBox9.Text = Nothing Or Me.TextBox9.Text = "" Or Me.TextBox9.Text = " " Then
            Me.TextBox9.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox9.BackColor = Color.White
            Me.Button8.Enabled = True
        End If
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        If Me.TextBox11.Text = Nothing Or Me.TextBox11.Text = "" Or Me.TextBox11.Text = " " Then
            Me.TextBox11.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox11.BackColor = Color.White
            Me.Button8.Enabled = True
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim device As String = Me.TextBox4.Text
        Dim rigname As String = Me.TextBox5.Text
        Dim ip As String = Me.TextBox6.Text
        Dim port As String = Me.TextBox7.Text
        Dim username As String = Me.TextBox8.Text
        Dim password As String = Me.TextBox9.Text
        Dim path As String = Me.TextBox11.Text

        Dim dataset As New StringBuilder

        If Me.Label30.Text = "save" Then
            Me.DataGridView2.Rows.Add(False, device, rigname, "", ip, port, username, password, path, "", "")
            Me.ComboBox8.Items.Add(device & " {" & rigname & "}")
            For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
                dataset.AppendLine(Chr(34) + Me.DataGridView2.Item(1, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(2, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(4, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(5, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(6, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(7, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(8, i).Value.ToString + Chr(34))
            Next

        End If

        If Me.Label30.Text = "update" Then
            Me.ComboBox8.Items.Clear()
            Me.ComboBox8.Text = ""
            For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1

                If Me.DataGridView2.Item(1, i).Value.ToString = device Then
                    Me.DataGridView2.Item(2, i).Value = rigname
                    Me.DataGridView2.Item(4, i).Value = ip
                    Me.DataGridView2.Item(5, i).Value = port
                    Me.DataGridView2.Item(6, i).Value = username
                    Me.DataGridView2.Item(7, i).Value = password
                    Me.DataGridView2.Item(8, i).Value = path

                End If
                Me.ComboBox8.Items.Add(Me.DataGridView2.Item(1, i).Value & "{" & Me.DataGridView2.Item(2, i).Value & "}")

                dataset.AppendLine(Chr(34) + Me.DataGridView2.Item(1, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(2, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(4, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(5, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(6, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(7, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(8, i).Value.ToString + Chr(34))

            Next
            Me.ComboBox8.Sorted = True
        End If

        Me.Label30.Text = "save"

        System.IO.File.WriteAllText(localdevice, dataset.ToString)

        Me.TextBox4.Text = ""
        Me.TextBox5.Text = ""
        Me.TextBox6.Text = ""
        Me.TextBox7.Text = "22"
        Me.TextBox8.Text = ""
        Me.TextBox8.BackColor = Color.White
        Me.TextBox9.Text = ""
        Me.TextBox9.BackColor = Color.White
        Me.TextBox11.Text = "/home/"

        MessageBox.Show(Checkxmllanguage("Message15.1").trim, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub Button8_MouseHover(sender As Object, e As EventArgs) Handles Button8.MouseHover
        Me.ToolTip1.SetToolTip(Button8, Checkxmllanguage("Button8").trim)
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        If ComboBox8.Items.Count >= 0 Then
            Dim comboselect As String = Me.ComboBox8.Text
            Dim comboselectsplitt() As String = comboselect.Split("{")
            Dim device As String = comboselectsplitt(0).Trim

            For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
                If Me.DataGridView2.Item(1, i).Value.ToString = device Then
                    Me.Label30.Text = "update"
                    Me.TextBox4.Text = Me.DataGridView2.Item(1, i).Value.ToString
                    Me.TextBox5.Text = Me.DataGridView2.Item(2, i).Value.ToString
                    Me.TextBox6.Text = Me.DataGridView2.Item(4, i).Value.ToString
                    Me.TextBox7.Text = Me.DataGridView2.Item(5, i).Value.ToString
                    Me.TextBox8.Text = Me.DataGridView2.Item(6, i).Value.ToString
                    Me.TextBox9.Text = Me.DataGridView2.Item(7, i).Value.ToString
                    Me.TextBox11.Text = Me.DataGridView2.Item(8, i).Value.ToString
                    Me.Button7.Enabled = True
                    Exit Sub
                End If
            Next
        End If

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim device As String = Me.TextBox4.Text
        Dim dataset As New StringBuilder


        Dim msgtext1 As String = Checkxmllanguage("Message12.1").trim
        Dim msgtext2 As String = Checkxmllanguage("Message12.2").trim

        Dim result = MessageBox.Show(msgtext1, msgtext2, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1

                If Me.DataGridView2.Item(1, i).Value.ToString = device Then
                    Me.DataGridView2.Rows.RemoveAt(i)
                    Exit For
                End If
            Next

            Me.ComboBox8.Items.Clear()

            For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
                Me.ComboBox8.Items.Add(Me.DataGridView2.Item(1, i).Value & "{" & Me.DataGridView2.Item(2, i).Value & "}")
                dataset.AppendLine(Chr(34) + Me.DataGridView2.Item(1, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(2, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(4, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(5, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(6, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(7, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(8, i).Value.ToString + Chr(34))
            Next
            Me.ComboBox8.Sorted = True

            System.IO.File.WriteAllText(localdevice, dataset.ToString)

            Me.ComboBox8.Text = ""
            Me.TextBox4.Text = ""
            Me.TextBox4.BackColor = Color.White
            Me.TextBox5.Text = ""
            Me.TextBox5.BackColor = Color.White
            Me.TextBox6.Text = ""
            Me.TextBox6.BackColor = Color.White
            Me.TextBox7.Text = "22"
            Me.TextBox8.Text = ""
            Me.TextBox8.BackColor = Color.White
            Me.TextBox9.Text = ""
            Me.TextBox9.BackColor = Color.White
            Me.TextBox11.Text = "/home/"

            Me.Button7.Enabled = False

            MessageBox.Show(Checkxmllanguage("Message13.1").trim, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button7_MouseHover(sender As Object, e As EventArgs) Handles Button7.MouseHover
        Me.ToolTip1.SetToolTip(Button7, Checkxmllanguage("Button7").trim)
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        If Me.ComboBox7.Text = "Default" Then
            Dim wallet As String = "you need a Wallet"
            If Me.DataGridView1.Rows.Count - 1 > 0 Then
                wallet = Me.DataGridView1.Item(2, 0).Value.ToString
            End If
            Me.RichTextBox1.Text = "WingSheet: Default" & System.Environment.NewLine &
                                   "Wallet: " & wallet & System.Environment.NewLine &
                                   "Pool: " & def_ps & System.Environment.NewLine &
                                   "Server: " & def_s & System.Environment.NewLine &
                                   "Solo: No" & System.Environment.NewLine &
                                   "Password: " & def_pw & System.Environment.NewLine &
                                   "Miner: " & def_m & System.Environment.NewLine &
                                   "Cores: ALL Cores"
            Exit Sub
        End If

        Dim wingsheetname As String = Me.ComboBox7.Text

        If My.Computer.FileSystem.FileExists(localwingsheet) Then
            Dim wingsheet As String
            Dim wallet As String
            Dim pool As String
            Dim server As String
            Dim solo As String
            Dim password As String
            Dim miner As String
            Dim cores As String
            Dim donate As String

            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        If currentRow(0) = wingsheetname Then

                            wingsheet = currentRow(0)
                            wallet = currentRow(1)
                            pool = currentRow(2)
                            server = currentRow(3)
                            password = currentRow(5)
                            miner = currentRow(6)
                            cores = currentRow(7)
                            solo = currentRow(10)
                            donate = currentRow(11)

                            If donate = "True" Then
                                donate = 1
                            Else
                                donate = 0
                            End If

                            If solo = False Then
                                solo = "no"
                            Else
                                solo = True
                            End If

                            For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
                                If Me.DataGridView1.Item(1, i).Value.ToString = wallet Then
                                    wallet = Me.DataGridView1.Item(2, i).Value.ToString
                                    Exit For
                                End If
                            Next

                            Me.RichTextBox1.Text = "WingSheet: " & wingsheet & System.Environment.NewLine &
                                   "Wallet: " & wallet & System.Environment.NewLine &
                                   "Pool: " & pool & System.Environment.NewLine &
                                   "Server: " & server & System.Environment.NewLine &
                                   "Solo: " & solo & System.Environment.NewLine &
                                   "Password: " & password & System.Environment.NewLine &
                                   "Miner: " & miner & System.Environment.NewLine &
                                   "Cores: " + cores + " / " + donate + " for Donate"
                            Exit Sub
                        End If
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message & " is invalid.  Skipping")
                    End Try
                End While
                currentRow = Nothing
            End Using
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If Me.DataGridView2.Rows.Count = 0 Then
            MessageBox.Show(Checkxmllanguage("Message30.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim selectrownumber As Integer = 0
        For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
            If Me.DataGridView2.Item(0, i).Value = False Then
                Continue For
            Else
                selectrownumber += 1
            End If
        Next

        If selectrownumber < 0 Then
            MessageBox.Show(Checkxmllanguage("Message30.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim msgtext1 As String = Checkxmllanguage("Message14.1").trim
        Dim msgtext2 As String = Checkxmllanguage("Message14.2").trim

        Dim result = MessageBox.Show(msgtext1, msgtext2, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.No Then
            Exit Sub
        End If

        If Not Directory.Exists(selfpath + "Thirdparty") Then
            MessageBox.Show("Directory Error. Exit Function", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not File.Exists(selfpath + "Thirdparty\plink.exe") Then
            MessageBox.Show("File Error. Exit Function", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not File.Exists(selfpath + "Thirdparty\pscp.exe") Then
            MessageBox.Show("File Error. Exit Function", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Not Directory.Exists(selfpath + "Thirdparty\tmp\") Then
            Directory.CreateDirectory(selfpath + "Thirdparty\tmp\")
        End If

        Dim wingsheet As String = Nothing
        Dim wallet As String = Nothing
        Dim pool As String = Nothing
        Dim server As String = Nothing
        Dim password As String = Nothing
        Dim miner As String = Nothing
        Dim cores As String = Nothing
        Dim spezials As String = Nothing
        Dim wingsheetname As String = Me.ComboBox7.Text
        Dim wingsheetname2 As String = wingsheetname

        If Me.ComboBox7.Text = "Default" Then
            wingsheet = "Default"

            wallet = "you need a Wallet"
            If Me.DataGridView1.Rows.Count - 1 > 0 Then
                wallet = Me.DataGridView1.Item(1, 0).Value.ToString
            End If
            If wallet = "you need a Waalet then" Then
                MessageBox.Show(Checkxmllanguage("Message16.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            pool = def_ps
            server = def_s
            password = def_pw
            miner = def_m
            cores = def_c
        Else

            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        If currentRow(0) = wingsheetname Then

                            wingsheet = currentRow(0)
                            wallet = currentRow(1)
                            pool = currentRow(2)
                            server = currentRow(3)
                            password = currentRow(5)
                            miner = currentRow(6)
                            cores = currentRow(7)
                        End If
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show("Line " & ex.Message & " in Wingsheet List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Progress ends.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Cursor.Current = Cursors.Default
                        Exit Sub
                    End Try
                End While
            End Using
        End If

        Dim spezial As String = Nothing
        Dim algo As String = Nothing

        If cores = "Default" Then
            cores = "0"
        End If

        If miner = "SRBMiner-MULTI" Then
            If cores = "0" Then
                spezial = "--disable-gpu "
            Else
                spezial = "--disable-gpu --cpu-threads " & cores & " "
            End If
            algo = "--algorithm ghostrider "
            server = "--pool " & server & " "
            wallet = "--wallet " & wallet & ""
            password = " --password " & password
        End If

        If wingsheet = Nothing Then
            Exit Sub
        End If


        For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
            If Me.DataGridView2.Item(0, i).Value = False Then
                Continue For
            End If
            Dim rigname As String = Me.DataGridView2.Item(2, i).Value
            Dim wallet2 As String = wallet & "." & rigname

            Dim sship As String = Me.DataGridView2.Item(4, i).Value
            Dim sshport As String = Me.DataGridView2.Item(5, i).Value
            Dim sshuser As String = Me.DataGridView2.Item(6, i).Value
            Dim sshpassword As String = Me.DataGridView2.Item(7, i).Value
            Dim sshpath As String = Me.DataGridView2.Item(8, i).Value


            wingsheet = "./SRBMiner-MULTI " & algo & spezial & "--log-file " & sshpath & "RaptorWings/log.txt " & server & wallet2 & password
            Dim plinkmain

            plinkmain = selfpath & "Thirdparty\plink.exe -ssh " & sship & " -l " & sshuser & " -pw " & sshpassword & " -batch -m " & selfpath & "Thirdparty\tmp\plink." & rigname

            Dim plink1 As String

            plink1 = "pkill SRBMiner-MULTI" & System.Environment.NewLine &
                     "cd " & sshpath & System.Environment.NewLine &
                     "mkdir -p RaptorWINGS" & System.Environment.NewLine &
                     "cd RaptorWINGS" & System.Environment.NewLine &
                     "wget " & SRBMinerDownloadpathLinux & System.Environment.NewLine &
                     "rm -R -f SRBMiner-MULTI " & System.Environment.NewLine &
                     "tar vxf " & SRBMinerDownloadnameLinux & System.Environment.NewLine &
                     "rm " & SRBMinerDownloadnameLinux & System.Environment.NewLine &
                     "mv " & SRBdirectory & " SRBMiner-MULTI" & System.Environment.NewLine &
                     "cd SRBMiner-MULTI" & System.Environment.NewLine &
                     wingsheet & "> /dev/null 2>&1 &"

            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(selfpath + "Thirdparty\tmp\plink_" & rigname & ".bat", False, Encoding.Default)
            file.Write(plinkmain)
            file.Close()

            file = My.Computer.FileSystem.OpenTextFileWriter(selfpath + "Thirdparty\tmp\plink." & rigname, False, Encoding.Default)
            file.Write(plink1)
            file.Close()

            Process.Start(selfpath & "Thirdparty\tmp\plink_" & rigname & ".bat")

            Me.DataGridView2.Item(3, i).Value = "Waiting for " & pool & " API"
            Me.DataGridView2.Item(9, i).Value = pool
            Me.DataGridView2.Item(10, i).Value = wallet.Replace("--wallet ", "")
            Me.DataGridView2.Item(11, i).Value = wingsheetname2
        Next

        Dim dataset As New StringBuilder
        For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
            dataset.AppendLine(Chr(34) + Me.DataGridView2.Item(2, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(9, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(10, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(11, i).Value.ToString + Chr(34))
        Next
        System.IO.File.WriteAllText(localpool, dataset.ToString)

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
            Me.DataGridView2.Item(0, i).Value = True
        Next
    End Sub

    Private Sub TextBox11_Leave(sender As Object, e As EventArgs) Handles TextBox11.Leave
        Dim path As String = Me.TextBox11.Text.Trim
        Dim zeichen = path(path.Length - 1)

        If Not zeichen = "/" Then
            Me.TextBox11.Text = Me.TextBox11.Text + "/"
        End If

        If Me.TextBox11.Text.Contains("//") Then
            Me.TextBox11.Text = Me.TextBox11.Text.Replace("//", "/")
        End If

    End Sub
    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Apipoolread()
        Showrigdetail()
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        Showrigdetail()
    End Sub

    Private Sub RichTextBox2_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles RichTextBox2.LinkClicked
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = e.LinkText, .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub

    Private Sub ToolStripStatusLabel2_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel2.Click
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = "https://explorer.raptoreum.com/address/" + donationadress, .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If Me.DataGridView1.Rows.Count - 1 > -1 Then
            Dim selectRowDGV1 As Integer = Me.DataGridView1.CurrentCell.RowIndex.ToString
            Dim selectwallet As String = "Nr. " & Me.DataGridView1.Item(0, selectRowDGV1).Value.ToString & " - " & Me.DataGridView1.Item(2, selectRowDGV1).Value.ToString & " (" & Me.DataGridView1.Item(1, selectRowDGV1).Value.ToString & ")"

            Dim msgtext1 As String = Checkxmllanguage("Message1.1").trim
            Dim msgtext2 As String = Checkxmllanguage("Message1.2").trim
            Dim msgtext3 As String = Checkxmllanguage("Message1.3").trim

            Dim result = MessageBox.Show(msgtext1, msgtext3, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(selectRowDGV1))
                For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
                    Me.DataGridView1.Item(0, i).Value = i + 1
                Next
                MessageBox.Show(msgtext2, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Readbalance()
            End If
            Me.Button2.Enabled = True
        End If
    End Sub

    Private Sub Button13_MouseHover(sender As Object, e As EventArgs) Handles Button13.MouseHover
        Me.ToolTip1.SetToolTip(Button13, Checkxmllanguage("Button13").trim)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If Me.DataGridView1.Rows.Count - 1 > -1 Then
            Dim selectRowDGV1 As Integer = Me.DataGridView1.CurrentCell.RowIndex.ToString
            Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = "https://explorer.raptoreum.com/address/" + Me.DataGridView1.Item(1, selectRowDGV1).Value.ToString, .UseShellExecute = True}
            Process.Start(ProcessStartInfo)
        End If
    End Sub
    Private Sub Button14_MouseHover(sender As Object, e As EventArgs) Handles Button14.MouseHover
        Me.ToolTip1.SetToolTip(Button14, Checkxmllanguage("Button14").trim)
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = "https://zlataamaranth.com", .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub

    Private Sub RichTextBox3_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles RichTextBox3.LinkClicked
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = e.LinkText, .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim result = Nothing
        If File.Exists(winDesktop + "\" + rtmCorePortableName + "\raptoreum-qt.exe") Then
            result = MessageBox.Show(Checkxmllanguage("Message19.1").trim, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                Exit Sub
            End If
            If result = DialogResult.Yes Then
                Process.Start(winDesktop + "\" + rtmCorePortableName + "\raptoreum-qt.exe")
                Exit Sub
            End If
        End If

        result = MessageBox.Show(Checkxmllanguage("Message20.1").trim, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then
            Exit Sub
        End If

        Cursor.Current = Cursors.WaitCursor
        Dim client As New Net.WebClient
        client.DownloadFile(rtmCorePortableWebPfad, winDesktop + "\" + rtmCorePortableDownloadName)

        If File.Exists(winDesktop + "\" + rtmCorePortableDownloadName) Then
            ZipFile.ExtractToDirectory(winDesktop + "\" + rtmCorePortableDownloadName, winDesktop + "\" + rtmCorePortableName)
            File.Delete(winDesktop + "\" + rtmCorePortableDownloadName)
        End If

        If File.Exists(winDesktop + "\" + rtmCorePortableName + "\raptoreum-qt.exe") Then
            Process.Start(winDesktop + "\" + rtmCorePortableName + "\raptoreum-qt.exe")
        End If

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If Not File.Exists(winDesktop + "\" + rtmCoreInstallName) Then
            Dim result = Nothing
            result = MessageBox.Show(Checkxmllanguage("Message21.1").trim, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor
            Dim client As New Net.WebClient
            client.DownloadFile(rtmCoreInstallWebPfad, winDesktop + "\" + rtmCoreInstallName)
            Cursor.Current = Cursors.Default
            MessageBox.Show(Checkxmllanguage("Message22.1").trim, "Instruction", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show(Checkxmllanguage("Message23.1").trim, "Instruction", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If Not File.Exists(rtmCoreAppDatapfad + "\wallets\wallet.dat") Then
            MessageBox.Show(Checkxmllanguage("Message24.1").trim, "Instruction", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim myStream As Stream
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "dat files (*.dat)|*.dat"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            File.Copy(rtmCoreAppDatapfad + "\wallets\wallet.dat", saveFileDialog1.FileName)
            If File.Exists(saveFileDialog1.FileName) Then
                MessageBox.Show(Checkxmllanguage("Message25.1").trim, "Instruction", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
    End Sub

    Private Async Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim p As Process
        For Each p In Diagnostics.Process.GetProcesses()
            If p.ProcessName = "Raptoreum Core - Wallet" Then
                MessageBox.Show(Checkxmllanguage("Message29.1").trim, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Next

        Dim result = Nothing
        If Not Directory.Exists(rtmCoreAppDatapfad) Then
            MessageBox.Show(Checkxmllanguage("Message26.1").trim, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        result = MessageBox.Show(Checkxmllanguage("Message27.1").trim, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then
            Exit Sub
        End If

        Cursor.Current = Cursors.WaitCursor
        Dim download As New WebClient
        Dim url As String = rtmBootstrapWebpfad
        Dim savePath As String = winDesktop + "\" + rtmBootstrapDownloadName
        AddHandler download.DownloadProgressChanged, AddressOf Download_ProgressChanged
        download.DownloadFileAsync(New Uri(url), savePath)
        Me.Label46.Text = "Wait until the download is complete."
    End Sub
    Public Sub Download_ProgressChanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs)
        Try
            ProgressBar1.Value = CInt(Math.Round((e.BytesReceived / e.TotalBytesToReceive) * 100, 0, MidpointRounding.AwayFromZero))
            Me.Label45.Text = Math.Round((e.BytesReceived / 1024) / 1024) & "MB / " & Math.Round((e.TotalBytesToReceive / 1024) / 1024) & "MB"
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try
        If e.BytesReceived = e.TotalBytesToReceive Then
            Cursor.Current = Cursors.WaitCursor

            Me.Label46.Text = ""
            Me.Label46.Text = "Delet old Files"
            Me.Label46.Refresh()

            If File.Exists("C:\Users\marcu\AppData\Roaming\RaptoreumCore\powcache.dat") Then
                File.Delete("C:\Users\marcu\AppData\Roaming\RaptoreumCore\powcache.dat")
            End If
            If Directory.Exists("C:\Users\marcu\AppData\Roaming\RaptoreumCore\blocks\") Then
                Directory.Delete("C:\Users\marcu\AppData\Roaming\RaptoreumCore\blocks\", True)
            End If
            If Directory.Exists("C:\Users\marcu\AppData\Roaming\RaptoreumCore\chainstate\") Then
                Directory.Delete("C:\Users\marcu\AppData\Roaming\RaptoreumCore\chainstate\", True)
            End If
            If Directory.Exists("C:\Users\marcu\AppData\Roaming\RaptoreumCore\evodb\") Then
                Directory.Delete("C:\Users\marcu\AppData\Roaming\RaptoreumCore\evodb\", True)
            End If
            If Directory.Exists("C:\Users\marcu\AppData\Roaming\RaptoreumCore\llmq\") Then
                Directory.Delete("C:\Users\marcu\AppData\Roaming\RaptoreumCore\llmq\", True)
            End If
            If File.Exists(winDesktop & "\bootstrap.zip") Then
                Me.Label46.Text = ""
                Me.Label46.Text = "Unzipping"
                Me.Label46.Refresh()
                ZipFile.ExtractToDirectory(winDesktop + "\" + rtmBootstrapDownloadName, rtmCoreAppDatapfad)
                Me.Label46.Text = "Unzip complete"
                Me.Label46.Refresh()
            End If

            Me.ProgressBar1.Value = 0
            Me.Label45.Text = "Status"
            Me.Label46.Text = "No activities"
            MessageBox.Show(Checkxmllanguage("Message28.1").trim, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Cursor.Current = Cursors.Default
        End If
    End Sub

    Private Sub ComboBox11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox11.SelectedIndexChanged
        Dim walletdata As String = Nothing
        Dim walletprofile As String = Me.ComboBox11.Text
        If walletprofile = "1 - Default" Then
            walletdata = localwallet
        Else
            Dim textsplitt() As String = Me.ComboBox11.Text.Split(" ")
            walletdata = localfolder + "main" + textsplitt(0).Trim + ".dat"
        End If

        Me.DataGridView1.Rows.Clear()

        If File.Exists(walletdata) Then
            Timer1.Stop()

            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(walletdata)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        Me.DataGridView1.Rows.Add(currentRow)
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show("Line " & ex.Message & " in Wallet List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Raptorwings will end.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End
                    End Try
                End While
                currentRow = Nothing
            End Using
            Timer1.Start()
            Readbalance()

            Me.ComboBox1.Items.Clear()
            For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
                Me.ComboBox1.Items.Add(Me.DataGridView1.Item(0, i).Value.ToString + " - " + Me.DataGridView1.Item(2, i).Value.ToString + " (" + Me.DataGridView1.Item(1, i).Value.ToString + ")")
            Next
            If Me.ComboBox1.Items.Count > -1 Then
                Me.ComboBox1.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub ComboBox9_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged
        Dim combtext As String = Me.ComboBox9.Text
        Dim combtextsplitt() As String = combtext.Split("-")
        Dim xmllanguagecode As String = combtextsplitt(1)
        xmllanguagecode = xmllanguagecode.Trim
        xmlLanguagesCodes = xmllanguagecode
        Languagecontrolls()
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        Dim dark As Color = Color.DimGray
        Dim transparent As Color = Color.Transparent
        Dim white As Color = Color.White
        Dim black As Color = Color.Black
        Dim red As Color = Color.Red
        Dim yellow As Color = Color.Yellow

        Dim background As Color = transparent
        Dim background2 As Color = white
        Dim textcolor As Color = black
        Dim textcolor2 As Color = red
        Dim textcolor3 As Color = black

        Me.PictureBox1.BackgroundImage = My.Resources.Rptorwings_logo_small

        If Me.CheckBox4.Checked = True Then
            background = dark
            background2 = dark
            textcolor = white
            textcolor2 = yellow
            textcolor3 = black
            Me.PictureBox1.BackgroundImage = My.Resources.Rptorwings_logo_dark_small
        Else

        End If
        Me.ForeColor = textcolor
        Me.LinkLabel1.LinkColor = textcolor
        Me.TabPage1.BackColor = background
        Me.TabPage2.BackColor = background
        Me.TabPage3.BackColor = background
        Me.TabPage5.BackColor = background
        Me.TabPage6.BackColor = background
        Me.TabPage7.BackColor = background
        Me.TabPage8.BackColor = background
        Me.TabPage9.BackColor = background
        Me.TabPage10.BackColor = background
        Me.Panel1.BackColor = background
        Me.Button1.ForeColor = background
        Me.Button2.ForeColor = background
        Me.Button3.BackColor = background
        Me.Button3.ForeColor = textcolor
        Me.Button4.ForeColor = textcolor3
        Me.Button5.ForeColor = background
        Me.Button6.ForeColor = background
        Me.Button7.ForeColor = background
        Me.Button8.ForeColor = background
        Me.Button9.ForeColor = background
        Me.Button10.ForeColor = textcolor3
        Me.Button11.ForeColor = textcolor3
        Me.Button14.ForeColor = background
        Me.Button13.ForeColor = background
        Me.Button18.ForeColor = background
        Me.Button19.ForeColor = background
        Me.Button20.ForeColor = background

        Me.DataGridView1.BackgroundColor = background2
        Me.DataGridView1.ForeColor = textcolor
        Me.DataGridView1.DefaultCellStyle.BackColor = background2
        Me.DataGridView1.DefaultCellStyle.ForeColor = textcolor
        Me.DataGridView2.BackgroundColor = background2
        Me.DataGridView2.ForeColor = textcolor
        Me.DataGridView2.DefaultCellStyle.BackColor = background2
        Me.DataGridView2.DefaultCellStyle.ForeColor = textcolor
        Me.DataGridView3.BackgroundColor = background2
        Me.DataGridView3.ForeColor = textcolor
        Me.DataGridView3.DefaultCellStyle.BackColor = background2
        Me.DataGridView3.DefaultCellStyle.ForeColor = textcolor
        Me.Label5.ForeColor = textcolor2
        Me.Label11.ForeColor = textcolor2
        Me.Label16.ForeColor = textcolor2
        Me.ToolStripStatusLabel1.ForeColor = textcolor3
        Me.ToolStripStatusLabel4.ForeColor = textcolor3
        Me.TextBox10.BackColor = background2
        Me.TextBox10.ForeColor = textcolor
        Me.TextBox12.BackColor = background2
        Me.TextBox12.ForeColor = textcolor
        Me.RichTextBox3.ForeColor = textcolor
        Me.RichTextBox3.BackColor = background2
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Me.DataGridView3.Rows.Add(Me.DataGridView3.Item(0, Me.DataGridView3.Rows.Count - 1).Value.ToString + 1, "", "", "")
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If Me.DataGridView3.Rows.Count - 1 > -1 Then
            Dim selectRowDGV3 As Integer = Me.DataGridView3.CurrentCell.RowIndex.ToString
            Dim selectwallet As String = Me.DataGridView3.Item(0, selectRowDGV3).Value.ToString & " - " & Me.DataGridView3.Item(1, selectRowDGV3).Value.ToString
            Dim selectProfilNummer As String = Me.DataGridView3.Item(0, selectRowDGV3).Value.ToString

            If selectwallet = "1 - Default" Then
                Exit Sub
            End If

            Dim msgtext1 As String = Checkxmllanguage("Message34.1").trim
            Dim msgtext2 As String = Checkxmllanguage("Message34.2").trim

            Dim result = MessageBox.Show(msgtext1, "Questtion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Me.DataGridView3.Rows.Remove(Me.DataGridView3.Rows(selectRowDGV3))
                For i As Integer = 0 To Me.DataGridView3.Rows.Count - 1
                    Me.DataGridView3.Item(0, i).Value = i + 1
                    If File.Exists(localfolder + "main" + selectProfilNummer + ".dat") Then
                        File.Delete(localfolder + "main" + selectProfilNummer + ".dat")
                    End If
                Next

                Me.ComboBox11.Items.Clear()
                For i As Integer = 0 To Me.DataGridView3.Rows.Count - 1
                    Me.ComboBox11.Items.Add(Me.DataGridView3.Item(0, i).Value.ToString + " - " + Me.DataGridView3.Item(1, i).Value.ToString)
                Next
                Me.ComboBox11.SelectedIndex = 0

                MessageBox.Show(msgtext2, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub DataGridView3_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellEndEdit
        Dim selectRowDGV3 As Integer = Me.DataGridView3.CurrentCell.RowIndex.ToString

        Me.DataGridView3.Item(1, 0).Value = "Default"

        If Me.DataGridView3.Item(1, selectRowDGV3).Value.ToString = "" Or Me.DataGridView3.Item(1, selectRowDGV3).Value.ToString = Nothing Or Me.DataGridView3.Item(1, selectRowDGV3).Value.ToString = " " Then
            Exit Sub
        Else
            Me.ComboBox11.Items.Clear()
            For i As Integer = 0 To Me.DataGridView3.Rows.Count - 1
                Me.ComboBox11.Items.Add(Me.DataGridView3.Item(0, i).Value.ToString + " - " + Me.DataGridView3.Item(1, i).Value.ToString)
            Next
            Me.ComboBox11.SelectedIndex = 0
        End If
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        saveusersetting()
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        For Each p In Diagnostics.Process.GetProcesses()
            For Each Process In System.Diagnostics.Process.GetProcessesByName("SRBMiner-MULTI")
                Process.Kill()
            Next
        Next
        Process.Start(selfpath + "mining\" + SRBdirectory + "\donation.bat")
        Timer5.Start()
        Timer4.Stop()
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        For Each p In Diagnostics.Process.GetProcesses()
            For Each Process In System.Diagnostics.Process.GetProcessesByName("SRBMiner-MULTI")
                Process.Kill()
            Next
        Next
        Process.Start(selfpath + "mining\" + SRBdirectory + "\rtmtsheet.bat")
        Timer4.Start()
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If Me.CheckBox5.Checked = True Then
            Me.CheckBox2.Checked = False
            Me.CheckBox2.Enabled = False
        End If
        If Me.CheckBox5.Checked = False Then
            Me.CheckBox2.Enabled = True
        End If
    End Sub

    Private Sub CheckBox5_MouseHover(sender As Object, e As EventArgs) Handles CheckBox5.MouseHover
        Me.ToolTip1.SetToolTip(CheckBox5, Checkxmllanguage("Checkbox3").trim)
    End Sub

    Private Sub CheckBox3_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        Miningsetting()
    End Sub
End Class
