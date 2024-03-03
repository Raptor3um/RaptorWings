'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.ComponentModel
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Runtime.Intrinsics.X86
Imports System.Text

Public Class Form1

    Public Sub logging(ByVal Message As String)
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(selfpath + "logging.log", True)
        file.WriteLine(Format(DateTime.Now.ToString("HH:mm:ss_fffff") + " - " + Message))
        file.Close()
    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If File.Exists(selfpath + "logging.log") Then
            My.Computer.FileSystem.DeleteFile(selfpath + "logging.log")
            logging("Start: delet old Logfile")
        End If

        logging("Start: Start Raptorwings")
        Cursor.Current = Cursors.WaitCursor

        Me.Text = "Raptorwings v" + rtwVersion + " (Falcon)"

        checkupdates.checkRTWupdate()
        checkupdates.checkRTMupdate()
        checkupdates.checkXMRIG()
        Await checkupdates.CheckPoolUpdates()

        Me.TextBox1.Text = Environment.MachineName
        logging("Start: Rigname is:" + Me.TextBox1.Text)

        logging("Start: Check %Appdata% Folder")
        If Not My.Computer.FileSystem.DirectoryExists(localfolder) Then
            My.Computer.FileSystem.CreateDirectory(localfolder)
            logging("Start: Check %Appdata% Folder: create Directory")
        Else
            logging("Start: Check %Appdata% Folder: Directory availible")
        End If

        If Not My.Computer.FileSystem.FileExists(localusersetting) Then
            logging("Start: Check in %Appdata% Folder: usersteiing not found.")
            Dim fs As FileStream = File.Create(localusersetting)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes("design,2" + vbCrLf + "sprache,English - EN")
            fs.Write(info, 0, info.Length)
            fs.Close()
            logging("Start: Check %Appdata% Folder: create File usersetting")
        End If

        logging("Start: Check Wingsheet")
        If File.Exists(localwingsheet) Then
            logging("Start: Check Wingsheet: Load WIngsheet")
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        Me.ComboBox6.Items.Add(currentRow(0))
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show("Line " & ex.Message & " in Wingsheet List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Raptorwings will end.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End While
                currentRow = Nothing
            End Using
        Else
            logging("Start: Check Wingsheet: No File found")
        End If

        logging("Start: Check Corenumbers")
        Dim corenumbers As Integer = Environment.ProcessorCount
        Me.ComboBox5.Items.Add("Default")
        For i As Integer = 1 To corenumbers
            Me.ComboBox5.Items.Add(i)
        Next
        corenumbers = Nothing

        logging("Start: Set all Comboboxes on INdex 0")
        If ComboBox1.Items.Count - 1 >= 0 Then
            logging("Start: Set all Comboboxes on INdex 0: Combbobox1")
            Me.ComboBox1.SelectedIndex = 0
        End If
        logging("Start: Set all Comboboxes on INdex 0: Combbobox2")
        Me.ComboBox2.SelectedIndex = 0
        logging("Start: Set all Comboboxes on INdex 0: Combbobox3")
        Me.ComboBox3.SelectedIndex = 0
        logging("Start: Set all Comboboxes on INdex 0: Combbobox4")
        Me.ComboBox4.SelectedIndex = 0
        logging("Start: Set all Comboboxes on INdex 0: Combbobox5")
        Me.ComboBox5.SelectedIndex = 0
        logging("Start: Set all Comboboxes on INdex 0: Combbobox6")
        Me.ComboBox6.SelectedIndex = 0
        logging("Start: Set all Comboboxes on INdex 0: Combbobox10")
        Me.ComboBox10.SelectedIndex = 0
        logging("Start: Set all Comboboxes on INdex 0: Combbobox11")
        Me.ComboBox11.SelectedIndex = 0

        Readbalance()
        Timer2.Start()

        Me.DataGridView3.Rows.Add("1", "Default")

        Languagesxmlload()
        FindLandguage()

        logging("Start: Check usersettings")
        If File.Exists(loadusersetting) Then
            loadusersetting()
        Else
            Languagecontrolls()
        End If

        Cursor.Current = Cursors.Default
        logging("Start: END")
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
        logging("USER: Click new Wallet Entry")
        If Me.DataGridView1.Rows.Count - 1 = -1 Then
            logging("USER: Click new Wallet Entry: System create Entry 1")
            Me.DataGridView1.Rows.Add("1", "", "", "")
        Else
            logging("USER: Click new Wallet Entry: System create new Entry")
            Me.DataGridView1.Rows.Add(Me.DataGridView1.Item(0, Me.DataGridView1.Rows.Count - 1).Value.ToString + 1, "", "", "")
        End If
        logging("USER: Click new Wallet Entry: Enable Button Save Wallet")
        Me.Button2.Enabled = True
        MessageBox.Show(Checkxmllanguage("Message18.1").trim, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
        logging("USER: Click new Wallet Entry: END")
    End Sub
    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseHover
        logging("USER: MoseHover Button1 (New Wallet)")
        Me.ToolTip1.SetToolTip(Button1, Checkxmllanguage("Button1").trim)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        logging("USER: Click Button2 (Save Wallet List)")
        saveWalletList.saveWalletList()
    End Sub
    Private Sub Button2_MouseHover(sender As Object, e As EventArgs) Handles Button2.MouseHover
        logging("USER: MoseHover Button2 (New Wallet)")
        Me.ToolTip1.SetToolTip(Button2, Checkxmllanguage("Button2").trim)
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Cursor.Current = Cursors.WaitCursor
        logging("USER/SYSTEM: CellEdit Walletlist: Start")
        Dim selectRowDGV1 As Integer = Me.DataGridView1.CurrentCell.RowIndex.ToString
        Dim newwalletadress As String = Me.DataGridView1.Item(1, selectRowDGV1).Value.ToString
        For i As Integer = 0 To selectRowDGV1 - 1
            If Me.DataGridView1.Item(1, i).Value.ToString = newwalletadress Then
                MessageBox.Show(Checkxmllanguage("Message31.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(selectRowDGV1))
                Exit Sub
            End If
        Next

        logging("USER/SYSTEM: CellEdit Walletlist: Check new Wallet Entry")
        Dim walletadress As String = Me.DataGridView1.Item(1, Me.DataGridView1.Rows.Count - 1).Value.ToString
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim client As New WebClient
        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")

        Dim response As String = client.DownloadString(apiwalletbalanceurl + walletadress)
        If response = "{}" Then
            logging("USER/SYSTEM: CellEdit Walletlist: Wallet is no RTM Wallet")
            MessageBox.Show((Checkxmllanguage("Message17.1").trim), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.DataGridView1.Item(1, Me.DataGridView1.Rows.Count - 1).Value = ""
            Exit Sub
        End If

        Me.ComboBox1.Items.Clear()
        For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
            logging("USER/SYSTEM: CellEdit Walletlist: Create Entry in Combo1 (Walletlist in Mining): " + Me.DataGridView1.Item(0, i).Value.ToString + " - " + Me.DataGridView1.Item(2, i).Value.ToString + " (" + Me.DataGridView1.Item(1, i).Value.ToString + ")")
            Me.ComboBox1.Items.Add(Me.DataGridView1.Item(0, i).Value.ToString + " - " + Me.DataGridView1.Item(2, i).Value.ToString + " (" + Me.DataGridView1.Item(1, i).Value.ToString + ")")
        Next

        Me.Button2.Enabled = True
        Timer1.Stop()
        Readbalance()
        Timer1.Start()
        Cursor.Current = Cursors.Default
        logging("User/System: CellEdit Walletlist: END")
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        logging("User: Click Button3 (Minging Area)")
        Me.Button3.Visible = False
        Me.Label5.Visible = False
        Me.Label11.Visible = False
        Me.Panel1.Visible = True
        Me.ComboBox1.Items.Clear()
        If Me.DataGridView1.Rows.Count - 1 >= 0 Then
            logging("User: Click Button3 (Minging Area): System check Combo1")
            For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
                Me.ComboBox1.Items.Add(Me.DataGridView1.Item(0, i).Value.ToString + " - " + Me.DataGridView1.Item(2, i).Value.ToString + " (" + Me.DataGridView1.Item(1, i).Value.ToString + ")")
                logging("User: Click Button3 (Minging Area: System Check Combot1: add:" + Me.DataGridView1.Item(0, i).Value.ToString + " - " + Me.DataGridView1.Item(2, i).Value.ToString + " (" + Me.DataGridView1.Item(1, i).Value.ToString + ")")
            Next
            logging("User: Click Button3 (Minging Area): System set INdex 0 in Combo1")
            Me.ComboBox1.SelectedIndex = 0
            logging("User: Click Button3 (Minging Area): Sysem referesh Combo1")
            Me.ComboBox1.Refresh()
        End If
        logging("User: Click Button3 (Minging Area): END")
    End Sub

    Function IsProcessRunning(ByVal processName As String) As Boolean
        Dim processes() As Process = Process.GetProcessesByName(processName)
        Return processes.Length > 0
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        logging("User: Click Button4 (Start Mining)")
        Me.Timer4.Stop()
        Me.Timer5.Stop()
        Me.Timer4.Interval = 12000000
        Me.Timer5.Interval = 1200000

        Dim processName As String = "XMRig"
        logging("User: Click Button4 (Start Mining): System checks color Button4")
        If Me.Button4.BackColor = Color.PaleVioletRed Then
            logging("User: Click Button4 (Start Mining): System: stop Miner")
            Dim processes() As Process = Process.GetProcessesByName(processName)
            For Each process As Process In processes
                process.Kill()
                process.WaitForExit()
                Exit Sub
            Next

        End If

        logging("User: Click Button4 (Start Mining): Ask if it should start")
        Dim msgtext1 As String = Checkxmllanguage("Message4.1").trim
        Dim msgtext2 As String = Checkxmllanguage("Message4.2").trim

        Dim result = MessageBox.Show(msgtext1, msgtext2, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.No Then
            logging("User: Click Button4 (Start Mining): Ask if it should start: User->NO")
            logging("User: Click Button4 (Start Mining): Ask if it should start: Exit SUB")
            Exit Sub
        Else
            logging("User: Click Button4 (Start Mining): Ask if it should start: User->YES")
        End If

        logging("User: Click Button4 (Start Mining): System: Check whether XMRIG is present")
        If Me.ComboBox3.Text = "XMRIG" Then
            If Not File.Exists(selfpath + "mining\" + XMRIG_MINER_DIRECTORYNAME + "\XMRIG.exe") Then
                logging("User: Click Button4 (Start Mining): System: XMRIG Miner not found in: " + selfpath + "mining\" + XMRIG_MINER_DIRECTORYNAME + "\XMRIG.exe")
                Cursor.Current = Cursors.WaitCursor
                If Not Directory.Exists(selfpath + "mining\") Then
                    logging("User: Click Button4 (Start Mining): System: " + selfpath + "mining\ not found")
                    Directory.CreateDirectory(selfpath + "mining\")
                    logging("User: Click Button4 (Start Mining): System: Create Directory " + selfpath + "mining\")
                End If

                logging("User: Click Button4 (Start Mining): System: Start Download XMRIG")
                Dim downloadpath As String = selfpath + "mining\" + XMRIG_MINER_DOWNLOAD_DATENAME
                Dim client As New Net.WebClient
                client.DownloadFile(XMRIG_MINER_DOWNLOAD_PATH, downloadpath)
                logging("User: Click Button4 (Start Mining): System: Start Download XMRIG -> END")

                logging("User: Click Button4 (Start Mining): System: Start unZip XMRIG")
                If File.Exists(downloadpath) Then
                    ZipFile.ExtractToDirectory(downloadpath, selfpath + "mining\")
                    File.Delete(downloadpath)
                    logging("User: Click Button4 (Start Mining): System: Start unZip XMRIG -> END")
                End If
                Cursor.Current = Cursors.Default
            End If
        End If
        logging("User: Click Button4 (Start Mining): System: Check whether XMRIG is present -> END")

        logging("User: Click Button4 (Start Mining): System: Check selectet Wallet")
        If Me.ComboBox1.Text = Nothing Then
            MessageBox.Show(Checkxmllanguage("Message5.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            logging("User: Click Button4 (Start Mining): System: Check selectet Wallet: Nothing marked in Combobox1")
            Exit Sub
        End If
        logging("User: Click Button4 (Start Mining): System: Check selectet Wallet -> END")

        Dim walletsplitt() As String = Me.ComboBox1.Text.Split("(")
        Dim wallet As String = walletsplitt(1).Replace(")", "")

        Dim server As String = Me.ComboBox4.Text
        Dim SSL As String = ""
        If server.Contains("tcps") Then
            SSL = " --tls"
            server = server.Replace("stratum+tcps://", "")
        Else
            server = server.Replace("stratum+tcp://", "")
        End If

        Dim rig As String = Me.TextBox1.Text
        Dim password As String = Me.TextBox2.Text

        Dim threads As String = Me.ComboBox5.Text
        If threads = "Default" Then
            threads = ""
        Else
            threads = "--threads=" & Me.ComboBox5.Text
        End If

        Dim wingsheet_main As String = Nothing
        Dim wingsheet_cpumineropt01 As String = Nothing

        wingsheet_cpumineropt01 = Chr(34) & selfpath & "mining\" & XMRIG_MINER_DIRECTORYNAME & "\" & "xmrig.exe" & Chr(34) & " " & threads & "-a gr -o " & server & SSL & " -u " & wallet & "." & rig & " -p " & password

        Dim wingsheet_donation As String
        wingsheet_donation = Chr(34) & selfpath & "mining\" & XMRIG_MINER_DIRECTORYNAME & "\" & "xmrig.exe" & Chr(34) & " " & threads & "-a gr -o stratum+tcp://raptorhash.net:6900" & SSL & " -u " & donationadress & ".Donation_" & rig & " -p x"

        If Me.CheckBox1.Checked = True Then
            wingsheet_cpumineropt01 += " --background"
            wingsheet_donation += " --background"
        End If

        If Not Me.ComboBox10.Text = "Default" Then
            wingsheet_cpumineropt01 += " --cpu-priority=" + Me.ComboBox10.Text
            wingsheet_donation += " --cpu-priority=" + Me.ComboBox10.Text
        End If

        If Me.ComboBox2.Text = "Raptorhash.com" Then
            wingsheet_main = wingsheet_cpumineropt01
        End If

        If Me.ComboBox2.Text = "Raptoreum.Zone" Then
            wingsheet_main = wingsheet_cpumineropt01
        End If

        If Me.ComboBox2.Text = "FlockPool" Then
            wingsheet_main = wingsheet_cpumineropt01
        End If

        Dim filewriter As System.IO.StreamWriter
        filewriter = My.Computer.FileSystem.OpenTextFileWriter(selfpath & "mining\" & XMRIG_MINER_DIRECTORYNAME & "\" & "rtmtsheet.bat", False, Encoding.Default)
        filewriter.Write("@ " & wingsheet_main)
        filewriter.Close()
        logging("User: Click Button4 (Start Mining): System: Wingsheet create under: " + selfpath + "mining\cpuminergr\rtmtsheet.bat")
        logging("User: Click Button4 (Start Mining): System: wingsheet Text:" + "@ " & wingsheet_main)
        logging("User: Click Button4 (Start Mining): System: Start Process: " + selfpath + "mining\cpuminergr\rtmtsheet.bat")
        Process.Start(selfpath & "mining\" & XMRIG_MINER_DIRECTORYNAME & "\" & "rtmtsheet.bat")

        If Me.CheckBox2.Checked = True Then
            End
        End If

        If CheckBox5.Checked = True Then
            filewriter = My.Computer.FileSystem.OpenTextFileWriter(selfpath & "mining\" & XMRIG_MINER_DIRECTORYNAME & "\" & "donation.bat", False, Encoding.Default)
            filewriter.Write("@ " & wingsheet_donation)
            filewriter.Close()
            Timer4.Start()
        End If
        logging("User: Click Button4 (Start Mining) -> END")
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        logging("User/System: Combo2 (Chose a pool) Index Change -> START")
        Miningsetting()
    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        logging("User/System: Combo3 (Miner) index Change -> START")
        Miningsetting()
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        Cursor.Current = Cursors.WaitCursor
        logging("User/System: Combo6 (Wingsheet) index Change -> START")

        If Me.ComboBox6.Text = "Default" Then
            If Me.DataGridView1.Rows.Count - 1 >= 0 Then
                Me.ComboBox1.Text = Me.DataGridView1.Item(2, 0).Value.ToString + " (" + Me.DataGridView1.Item(1, 0).Value.ToString + ")"
            End If
            logging("User/System: Combo6 (Wingsheet) index Change -> Change Combo2 to: " + def_ps)
            Me.ComboBox2.Text = def_ps
            logging("User/System: Combo6 (Wingsheet) index Change -> Change Combo3 to: " + def_m)
            Me.ComboBox3.Text = def_m
            logging("User/System: Combo6 (Wingsheet) index Change -> Change Combo4 to: " + def_s)
            Me.ComboBox4.Text = def_s
            logging("User/System: Combo6 (Wingsheet) index Change -> Change Combo5 to: " + def_c)
            Me.ComboBox5.Text = def_c
            logging("User/System: Combo6 (Wingsheet) index Change -> Change Combo10 to: Default")
            Me.ComboBox10.Text = "Default"
            logging("User/System: Combo6 (Wingsheet) index Change -> Change TXTB2 to: " + def_pw)
            Me.TextBox2.Text = def_pw
            logging("User/System: Combo6 (Wingsheet) index Change -> Change TXTB3 to: none")
            Me.TextBox3.Text = ""
            logging("User/System: Combo6 (Wingsheet) index Change -> Change CheckBox3 enablde")
            Me.CheckBox3.Enabled = True
            logging("User/System: Combo6 (Wingsheet) index Change -> Change Checkstate Checkbox1 to: uncheck")
            Me.CheckBox1.CheckState = CheckState.Unchecked
            logging("User/System: Combo6 (Wingsheet) index Change -> Change Checkstate Checkbox2 to: uncheck")
            Me.CheckBox2.CheckState = CheckState.Unchecked
            If def_solo = False Then
                logging("User/System: Combo6 (Wingsheet) index Change -> Def_solo = False")
                logging("User/System: Combo6 (Wingsheet) index Change -> Change Checkstate Checkbox3 to: uncheck")
                Me.CheckBox3.CheckState = CheckState.Unchecked
                Me.CheckBox3.Enabled = False
                Exit Sub
            End If
        End If

        logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: START")
        Dim wingsheetname As String = Me.ComboBox6.Text

        If File.Exists(localwingsheet) Then
            logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: File found")
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Start Read File")
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()

                        If currentRow(0) = wingsheetname Then
                            Me.TextBox3.Text = currentRow(0)

                            Dim comboboxindex As Integer = 0
                            For i As Integer = 0 To ComboBox1.Items.Count - 1
                                Dim comboboxtext As String = Me.ComboBox1.Items(i).ToString
                                If comboboxtext.Contains(currentRow(1)) Then
                                    logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Combo1 set Index :" + i)
                                    ComboBox1.SelectedIndex = i
                                End If
                            Next
                            logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Combo2 set Index :" + currentRow(2))
                            Me.ComboBox2.Text = currentRow(2)
                            logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Combo4 set Index :" + currentRow(3))
                            Me.ComboBox4.Text = currentRow(3)
                            logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: TXTB1 set Index :" + currentRow(4))
                            Me.TextBox1.Text = currentRow(4)
                            logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: TXTB2 set Index :" + currentRow(5))
                            Me.TextBox2.Text = currentRow(5)
                            logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Combo3 set Index :" + currentRow(6))
                            Me.ComboBox3.Text = currentRow(6)
                            logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Combo5 set Index :" + currentRow(7))
                            Me.ComboBox5.Text = currentRow(7)
                            logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Combo10 set Index : Default")
                            Me.ComboBox10.Text = "Default"
                            logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Combo10 set Index :" + currentRow(12))
                            Me.ComboBox10.Text = currentRow(12)

                            If currentRow(8) = True Then
                                logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Ccheckbox1 set checked")
                                Me.CheckBox1.CheckState = CheckState.Checked
                            Else
                                logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Ccheckbox1 set unchecked")
                                Me.CheckBox1.CheckState = CheckState.Unchecked
                            End If

                            If currentRow(9) = True Then
                                logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Ccheckbox2 set checked")
                                Me.CheckBox2.CheckState = CheckState.Checked
                            Else
                                logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Ccheckbox2 set unchecked")
                                Me.CheckBox2.CheckState = CheckState.Unchecked
                            End If

                            If currentRow(10) = True Then
                                logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Ccheckbox3 set checked")
                                Me.CheckBox3.CheckState = CheckState.Checked
                            Else
                                logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: Ccheckbox3 set unchecked")
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
        End If
        Cursor.Current = Cursors.Default
        logging("User/System: Combo6 (Wingsheet) index Change: System: load Wingsheet: END")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        logging("User: Click Button5 (Save Wingsheet): START")
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
            logging("User: Click Button5 (Save Wingsheet): File exists: " + localwingsheet)
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

        logging("User: Click Button5 (Save Wingsheet): Save Data in File")
        System.IO.File.WriteAllText(localwingsheet, dataset.ToString)

        If Not Me.ComboBox6.Items.Contains(wingsheetname) Then
            logging("User: Click Button5 (Save Wingsheet): write " + wingsheetname + " in Combo6")
            Me.ComboBox6.Items.Add(wingsheetname)
        End If

        For i As Integer = 0 To ComboBox6.Items.Count - 1
            If Me.ComboBox6.Items(i).ToString = wingsheetname Then
                Me.ComboBox6.SelectedIndex = i
            End If
        Next

        MessageBox.Show(Checkxmllanguage("Message8.1").trim, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
        logging("User: Click Button5 (Save Wingsheet): END")
    End Sub
    Private Sub Button5_MouseHover(sender As Object, e As EventArgs) Handles Button5.MouseHover
        logging("USER: MoseHover Button5 (Save Wingsheet)")
        Me.ToolTip1.SetToolTip(Button5, Checkxmllanguage("Button5").trim)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        logging("USER: Click Button6 (Delete Wingsheet): START")
        Dim wingsheetname As String = Me.TextBox3.Text

        If wingsheetname = "Default" Then
            logging("USER: Click Button6 (Delete Wingsheet): System: Default Wingsheet -> abort")
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
            logging("USER: Click Button6 (Delete Wingsheet): User say: NO")
            Exit Sub
        Else
            logging("USER: Click Button6 (Delete Wingsheet): User say: YES")
        End If

        Dim dataset As New StringBuilder

        logging("USER: Click Button6 (Delete Wingsheet): Read File: " + localwingsheet)
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
        logging("USER: Click Button6 (Delete Wingsheet): Read File END")

        logging("USER: Click Button6 (Delete Wingsheet): Write File: " + localwingsheet)
        System.IO.File.WriteAllText(localwingsheet, dataset.ToString)

        logging("USER: Click Button6 (Delete Wingsheet): Remove from Combo6")
        Me.ComboBox6.Items.Remove(wingsheetname)

        If ComboBox1.Items.Count - 1 >= 0 Then
            logging("USER: Click Button6 (Delete Wingsheet): Index Combo1 is 0")
            Me.ComboBox1.SelectedIndex = 0
        End If
        logging("USER: Click Button6 (Delete Wingsheet): Index Combo2 is 0")
        Me.ComboBox2.SelectedIndex = 0
        logging("USER: Click Button6 (Delete Wingsheet): Index Combo3 is 0")
        Me.ComboBox3.SelectedIndex = 0
        logging("USER: Click Button6 (Delete Wingsheet): Index Combo4 is 0")
        Me.ComboBox4.SelectedIndex = 0
        logging("USER: Click Button6 (Delete Wingsheet): Index Combo5 is 0")
        Me.ComboBox5.SelectedIndex = 0
        logging("USER: Click Button6 (Delete Wingsheet): Index Combo6 is 0")
        Me.ComboBox6.SelectedIndex = 0
        logging("USER: Click Button6 (Delete Wingsheet): Text3 is nothing")
        Me.TextBox3.Text = ""
        logging("USER: Click Button6 (Delete Wingsheet): Checkbox1 = checket")
        Me.CheckBox1.CheckState = CheckState.Checked
        logging("USER: Click Button6 (Delete Wingsheet): Checkbox2 = unchecket")
        Me.CheckBox2.CheckState = CheckState.Unchecked
        logging("USER: Click Button6 (Delete Wingsheet): Checkbox3 = unchecket")
        Me.CheckBox3.CheckState = CheckState.Unchecked


        MessageBox.Show(Checkxmllanguage("Message11.1").trim, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
        logging("USER: Click Button6 (Delete Wingsheet): END")
    End Sub
    Private Sub Button6_MouseHover(sender As Object, e As EventArgs) Handles Button6.MouseHover
        logging("USER: MoseHover Button6(Delete Wingsheet)")
        Me.ToolTip1.SetToolTip(Button6, Checkxmllanguage("Button6").trim)
    End Sub

    Private Sub TabPage1_Enter(sender As Object, e As EventArgs) Handles TabPage1.Enter
        logging("USER: Enter Tabpage 1")
        Timer1.Start()
    End Sub

    Private Sub TabPage1_Leave(sender As Object, e As EventArgs) Handles TabPage1.Leave
        logging("USER: Leave Tabpage 1")
        Timer1.Stop()
    End Sub
    Private Sub TabPage2_Enter(sender As Object, e As EventArgs) Handles TabPage2.Enter
        logging("USER: Enter Tabpage 2")
        Timer1.Start()
    End Sub

    Private Sub TabPage2_Leave(sender As Object, e As EventArgs) Handles TabPage2.Leave
        logging("USER: Leave Tabpage 2")
        If Me.Button2.Enabled = True Then
            Dim msgtext1 As String = Checkxmllanguage("Message33.1").trim

            Dim result = MessageBox.Show(msgtext1, "Questtion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                logging("USER: Leave Tabpage 2: Start Save Wallet List")
                saveWalletList.saveWalletList()
            End If
        End If
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        logging("System: Timer1 tick")
        Readbalance()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        logging("System: Timer2 tick")
        Try
            Dim processName As String = "XMRig"
            If IsProcessRunning(processName) Then
                Me.Button4.BackColor = Color.PaleVioletRed
                Me.Button4.Text = "Stop Miner"
            Else
                Me.Button4.BackColor = Color.YellowGreen
                Me.Button4.Text = "Start Miner"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripStatusLabel2_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel2.Click
        logging("User: ToolstripStatusLabel2 click start")
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = "https://explorer.raptoreum.com/address/" + donationadress, .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        logging("User: click Button13 (Delet select Entry): Start")
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
        logging("User: click Button13 (Delet select Entry): End")
    End Sub

    Private Sub Button13_MouseHover(sender As Object, e As EventArgs) Handles Button13.MouseHover
        logging("User: MouseHover Button13 (Delete Wallet Entry)")
        Me.ToolTip1.SetToolTip(Button13, Checkxmllanguage("Button13").trim)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        logging("User: Click Button14): Start")
        If Me.DataGridView1.Rows.Count - 1 > -1 Then
            Dim selectRowDGV1 As Integer = Me.DataGridView1.CurrentCell.RowIndex.ToString
            Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = "https://explorer.raptoreum.com/address/" + Me.DataGridView1.Item(1, selectRowDGV1).Value.ToString, .UseShellExecute = True}
            Process.Start(ProcessStartInfo)
        End If
        logging("User: Click Button14): End")
    End Sub
    Private Sub Button14_MouseHover(sender As Object, e As EventArgs) Handles Button14.MouseHover
        logging("User: MouseHover Button14")
        Me.ToolTip1.SetToolTip(Button14, Checkxmllanguage("Button14").trim)
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        logging("User: Click Linklabel1")
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = "https://zlataamaranth.com", .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub

    Private Sub RichTextBox3_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles RichTextBox3.LinkClicked
        logging("User: Click Linklabel3")
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = e.LinkText, .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        logging("User: Click Button12 (Download RTM Core Portable): Start")
        Dim result = Nothing
        If File.Exists(winDesktop + "\" + rtmCorePortableName + "\raptoreum-qt.exe") Then
            logging("User: Click Button12 (Download RTM Core Portable): File found: " + winDesktop + "\" + rtmCorePortableName + "\raptoreum-qt.exe")
            result = MessageBox.Show(Checkxmllanguage("Message19.1").trim, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                logging("User: Click Button12 (Download RTM Core Portable): File found: User SAY NO")
                Exit Sub
            Else
                logging("User: Click Button12 (Download RTM Core Portable): File found: User SAY YES")
            End If

            If result = DialogResult.Yes Then
                logging("User: Click Button12 (Download RTM Core Portable): File found: Process start")
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
            logging("User: Click Button12 (Download RTM Core Portable): Unzip File: " + winDesktop + "\" + rtmCorePortableDownloadName)
            'ZipFile.ExtractToDirectory(winDesktop + "\" + rtmCorePortableDownloadName, winDesktop + "\" + rtmCorePortableName)
            File.Delete(winDesktop + "\" + rtmCorePortableDownloadName)
        End If

        If File.Exists(winDesktop + "\" + rtmCorePortableName + "\raptoreum-qt.exe") Then
            logging("User: Click Button12 (Download RTM Core Portable): Start Process: " + winDesktop + "\" + rtmCorePortableName + "\raptoreum-qt.exe")
            Process.Start(winDesktop + "\" + rtmCorePortableName + "\raptoreum-qt.exe")
        End If

        Cursor.Current = Cursors.Default
        logging("User: Click Button12 (Download RTM Core Portable): END")
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        logging("User: Click Button15 (INstall RTM CORE): Start")
        If Not File.Exists(winDesktop + "\" + rtmCoreInstallName) Then
            Dim result = Nothing
            result = MessageBox.Show(Checkxmllanguage("Message21.1").trim, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor
            Dim client As New Net.WebClient
            logging("User: Click Button15 (INstall RTM CORE): Start Download: " + winDesktop + "\" + rtmCoreInstallName)
            client.DownloadFile(rtmCoreInstallWebPfad, winDesktop + "\" + rtmCoreInstallName)
            Cursor.Current = Cursors.Default
            MessageBox.Show(Checkxmllanguage("Message22.1").trim, "Instruction", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show(Checkxmllanguage("Message23.1").trim, "Instruction", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        logging("User: Click Button15 (INstall RTM CORE): END")
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        logging("User: Click Button16 (Save RTM Core Walletlist): Start")
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
        logging("User: Click Button16 (Save RTM Core Walletlist): End")
    End Sub

    Private Async Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        logging("User: Click Button17 (Bootstraps): Start")
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
        logging("User: Click Button17 (Bootstraps): END")
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

            If File.Exists(rtmCoreAppDatapfad + "powcache.dat") Then
                File.Delete(rtmCoreAppDatapfad + "powcache.dat")
            End If
            If Directory.Exists(rtmCoreAppDatapfad + "blocks\") Then
                Directory.Delete(rtmCoreAppDatapfad + "blocks\", True)
            End If
            If Directory.Exists(rtmCoreAppDatapfad + "chainstate\") Then
                Directory.Delete(rtmCoreAppDatapfad + "chainstate\", True)
            End If
            If Directory.Exists(rtmCoreAppDatapfad + "evodb\") Then
                Directory.Delete(rtmCoreAppDatapfad + "evodb\", True)
            End If
            If Directory.Exists(rtmCoreAppDatapfad + "llmq\") Then
                Directory.Delete(rtmCoreAppDatapfad + "llmq\", True)
            End If
            If File.Exists(winDesktop & "\bootstrap.zip") Then
                Me.Label46.Text = ""
                Me.Label46.Text = "Unzipping"
                Me.Label46.Refresh()
                'ZipFile.ExtractToDirectory(winDesktop + "\" + rtmBootstrapDownloadName, rtmCoreAppDatapfad)
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
        logging("User: Combo11 (Profile) changed: Start")
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
        logging("User: Combo11 (Profile) changed: End")
    End Sub

    Private Sub ComboBox9_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged
        logging("User/Sytsem: Combo9 (Languages) changed: Start")
        Dim combtext As String = Me.ComboBox9.Text
        Dim combtextsplitt() As String = combtext.Split("-")
        Dim xmllanguagecode As String = combtextsplitt(1)
        xmllanguagecode = xmllanguagecode.Trim
        xmlLanguagesCodes = xmllanguagecode
        Languagecontrolls()
        logging("User/Sytsem: Combo9 (Languages) changed: End")
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        logging("User/Sytsem: Checkbox4 (Darmode) changed: Start")
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
        Me.TabPage6.BackColor = background
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
        Me.Button14.ForeColor = background
        Me.Button13.ForeColor = background
        Me.Button18.ForeColor = background
        Me.Button19.ForeColor = background
        Me.Button20.ForeColor = background

        Me.DataGridView1.BackgroundColor = background2
        Me.DataGridView1.ForeColor = textcolor
        Me.DataGridView1.DefaultCellStyle.BackColor = background2
        Me.DataGridView1.DefaultCellStyle.ForeColor = textcolor
        Me.DataGridView3.BackgroundColor = background2
        Me.DataGridView3.ForeColor = textcolor
        Me.DataGridView3.DefaultCellStyle.BackColor = background2
        Me.DataGridView3.DefaultCellStyle.ForeColor = textcolor
        Me.Label5.ForeColor = textcolor2
        Me.Label11.ForeColor = textcolor2
        Me.Label16.ForeColor = textcolor2
        Me.ToolStripStatusLabel1.ForeColor = textcolor3
        Me.ToolStripStatusLabel4.ForeColor = textcolor3
        Me.RichTextBox3.ForeColor = textcolor
        Me.RichTextBox3.BackColor = background2
        logging("User/Sytsem: Checkbox4 (Darmode) changed: End")
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        logging("User: Click Button20 (New Wallet): Start")
        Me.DataGridView3.Rows.Add(Me.DataGridView3.Item(0, Me.DataGridView3.Rows.Count - 1).Value.ToString + 1, "", "", "")
        logging("User: Click Button20 (New Wallet): End")
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        logging("User: Click Button18 (Delete Wallet): Start")
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
        logging("User: Click Button18 (Delete Wallet): End")
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
        logging("User: Click Button19 (Save usersettings): Start")
        saveusersetting()
        logging("User: Click Button19 (Save usersettings): End")
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        logging("System Timer4 tick: start")
        For Each p In Diagnostics.Process.GetProcesses()
            For Each Process In System.Diagnostics.Process.GetProcessesByName("SRBMiner-MULTI")
                Process.Kill()
            Next
        Next
        Process.Start(selfpath + "mining\cpuminergr\donation.bat")
        Timer5.Start()
        Timer4.Stop()
        logging("System Timer4 tick: End")
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        logging("System Timer5 tick: start")
        For Each p In Diagnostics.Process.GetProcesses()
            For Each Process In System.Diagnostics.Process.GetProcessesByName("SRBMiner-MULTI")
                Process.Kill()
            Next
        Next
        Process.Start(selfpath + "mining\cpuminergr\rtmtsheet.bat")
        Timer4.Start()
        logging("System Timer5 tick: End")
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        logging("User/System: Checkbox5 change: start")
        If Me.CheckBox5.Checked = True Then
            Me.CheckBox2.Checked = False
            Me.CheckBox2.Enabled = False
        End If
        If Me.CheckBox5.Checked = False Then
            Me.CheckBox2.Enabled = True
        End If
        logging("User/System: Checkbox5 change: End")
    End Sub

    Private Sub CheckBox5_MouseHover(sender As Object, e As EventArgs) Handles CheckBox5.MouseHover
        logging("User: MouseHover Checkbox5")
        Me.ToolTip1.SetToolTip(CheckBox5, Checkxmllanguage("Checkbox3").trim)
    End Sub

    Private Sub CheckBox3_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        Miningsetting()
    End Sub

    Private Sub Button21_MouseHover(sender As Object, e As EventArgs) Handles Button21.MouseHover
        logging("User: MouseHover Button21 (Export CSV)")
        Me.ToolTip1.SetToolTip(Button21, Checkxmllanguage("Button21").trim)
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        logging("User: Click Button21 (Export CSV): Start")

        If DataGridView1.Rows.Count - 1 = -1 Then
            logging("User: Click Button21 (Export CSV): Not Entry Found")
            MessageBox.Show(Checkxmllanguage("Message2.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim myStream As Stream
        Dim saveFileDialog1 As New SaveFileDialog()

        saveFileDialog1.Filter = "dat files (*.csv)|*.csv"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True

        Dim sb = New StringBuilder

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Item(0, i).Value.ToString = Nothing Then
                    Continue For
                End If

                Dim number As String = Chr(34) + DataGridView1.Item(0, i).Value.ToString + Chr(34)
                Dim walletadress As String = Chr(34) + DataGridView1.Item(1, i).Value.ToString + Chr(34)
                Dim description As String = Chr(34) + DataGridView1.Item(2, i).Value.ToString + Chr(34)
                Dim balance As String = Chr(34) + DataGridView1.Item(3, i).Value.ToString + Chr(34)
                Dim btc As String
                If DataGridView1.Item(4, i).Value = Nothing Then
                    btc = Chr(34) + "0" + Chr(34)
                Else
                    btc = Chr(34) + DataGridView1.Item(4, i).Value.ToString + Chr(34)
                End If
                Dim dollar As String
                If DataGridView1.Item(5, i).Value = Nothing Then
                    dollar = Chr(34) + "0" + Chr(34)
                Else
                    dollar = Chr(34) + DataGridView1.Item(5, i).Value.ToString + Chr(34)
                End If
                Dim euro As String
                If DataGridView1.Item(6, i).Value = Nothing Then
                    euro = Chr(34) + "0" + Chr(34)
                Else
                    euro = Chr(34) + DataGridView1.Item(6, i).Value.ToString + Chr(34)
                End If

                sb.AppendLine($"{number},{walletadress},{description},{balance},{btc},{dollar},{euro}")
            Next

            System.IO.File.WriteAllText(saveFileDialog1.FileName, sb.ToString)
            MessageBox.Show((Checkxmllanguage("Message3.1").trim), "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        logging("User: Click Button21 (Export CSV): End")
    End Sub

End Class
