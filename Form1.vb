'This software is written for the RTM community. It is part of the Raptoreum program and was developed by
'Germardies (https://github.com/Germardies).
'It should be freely available To everyone.

'Diese Software ist für die RTM Community geschrieben.
'Sie ist Teil von Raptoreum (https://github.com/Raptor3um/raptoreum) und wurde
'Entwickelt durch Germardies (https://github.com/Germardies)
'Sie soll jedem frei zugänglich sein.

'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

'It was tried to leave all comments in German and English
'I would be happy to keep it that way so that many people understand what is happening here
'Es wurde versucht alle Kommentare in Deutsch und English zu hinterlassen
'Es würde mich freuen, dies so bei zu behalten, damit viele Menschen verstehen was hier passiert

'A Clue: / Ein Hinweis:
'I prefer to be able to read a code from top to bottom than to use a stylish modern codestyle'. 
'that ends up being read by only a handful of people. 
'Mir ist es lieber ein Code von oben nach unten lesen zu können als einen Stylischen modernen Codestyle 
'zu nutzen, der am Ende nur von einer Hand voll Leuten gelesen werden kann. 

'It was tried to store all global variables, if it was possible in the globale.vb out
'Es wurde versucht alle Globalen Variablen, sofern es möglich war in die globale.vb aus zu lagern

'Imports System.DirectoryServices.ActiveDirectory
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Microsoft
Imports Windows.Media.Protection.PlayReady

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Formload / Programmstart
        Cursor.Current = Cursors.WaitCursor

        Me.TextBox1.Text = Environment.MachineName 'Machine Name as Rigname / name dieses Gerätes an Rigbezeichnung

        'Create RaptorWINGSS Folder in "%AppData%/Lokal if not exists
        If Not My.Computer.FileSystem.DirectoryExists(localfolder) Then
            My.Computer.FileSystem.CreateDirectory(localfolder)
        End If

        'Load Wallet file / Lade Wallet datei
        'if Wallet File exits, the read it / Wenn die Datei vorhanden ist, dann wird diese eingelesen

        If File.Exists(localwallet) Then
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwallet)

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
        End If

        'Load WingSheet
        If File.Exists(localwingsheet) Then
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        Me.ComboBox6.Items.Add(currentRow(0)) 'Mining My Device
                        Me.ComboBox7.Items.Add(currentRow(0)) 'Mining Other Device
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show("Line " & ex.Message & " in Wingsheet List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Raptorwings will end.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End While
                currentRow = Nothing
            End Using
        End If

        'Find out the number of CPU cores and enter them in CB5 (Mining) / Anzahl der CPU Kerne herrausfinden und in CB5 (Mining) eintragen
        Dim corenumbers As Integer = Environment.ProcessorCount
        Me.ComboBox5.Items.Add("Default")
        For i As Integer = 1 To corenumbers
            Me.ComboBox5.Items.Add(i)
        Next
        corenumbers = Nothing

        'Set all Comboboxes on INdex 0 / Setzte alle Comboboxen auf Index 0
        If ComboBox1.Items.Count - 1 >= 0 Then
            Me.ComboBox1.SelectedIndex = 0
        End If
        Me.ComboBox2.SelectedIndex = 0
        Me.ComboBox3.SelectedIndex = 0
        Me.ComboBox4.SelectedIndex = 0
        Me.ComboBox5.SelectedIndex = 0
        Me.ComboBox6.SelectedIndex = 0
        Me.ComboBox7.SelectedIndex = 0

        'Set Color
        Me.CheckBox4.Checked = True

        Readbalance() 'Start Function Readbalance / Starte Funktion readbalance
        Readprice() 'Start Function Readprice / Starte Funktion readprice
        Timer2.Start() ' Starte Timer 2 um nach zu sehen, ob die Minindungsoftware läuft

        'Find System Language / Erkenne Systemsprache
        languagesxmlload()
        Cursor.Current = Cursors.Default
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'New Wallet Entry / Neuer Wallet Eintrag
        If Me.DataGridView1.Rows.Count - 1 = -1 Then
            Me.DataGridView1.Rows.Add("1", "", "", "")
        Else
            Me.DataGridView1.Rows.Add(Me.DataGridView1.Item(0, Me.DataGridView1.Rows.Count - 1).Value.ToString + 1, "", "", "")
        End If
        MessageBox.Show(checkxmllanguage("Message18.1").trim, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseHover
        'Hover Efekt für Button1 (New Wallet Entry)
        Me.ToolTip1.SetToolTip(Button1, checkxmllanguage("Button1").trim)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Save the Wallet List / Speichere die Wallet Liste
        If Me.DataGridView1.Rows.Count - 1 = -1 Then 'If there is no entry, the process is aborted / Wenn kein Eintrag vorhanden ist wird abgebrochen
            MessageBox.Show(checkxmllanguage("Message2.1").trim)
            Exit Sub
        End If

        Dim sb = New StringBuilder

        'Read each line of the DGV1 (wallet) individually, write the data to the record variable / Lies jede zeile der DGV1 (Wallet) einzeln aus, schreibe die Daten in die record variable
        For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
            If Me.DataGridView1.Item(0, i).Value.ToString = Nothing Then
                Continue For
            End If

            Dim number As String = Chr(34) + Me.DataGridView1.Item(0, i).Value.ToString + Chr(34)
            Dim walletadress As String = Chr(34) + Me.DataGridView1.Item(1, i).Value.ToString + Chr(34)
            Dim description As String = Chr(34) + Me.DataGridView1.Item(2, i).Value.ToString + Chr(34)

            sb.AppendLine($"{number},{walletadress},{description}")
        Next

        System.IO.File.WriteAllText(localwallet, sb.ToString)

        'Confirmation that the procedure has been completed / Bestätigung, dass die Prozedur durchlaufen wurde

        MessageBox.Show((checkxmllanguage("Message3.1").trim))

    End Sub
    Private Sub Button2_MouseHover(sender As Object, e As EventArgs) Handles Button2.MouseHover
        'Hover Efekt für Button2 (Save Wallet List)
        Me.ToolTip1.SetToolTip(Button2, checkxmllanguage("Button2").trim)
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        'In the case of changes in the DGV1, a comparison is made with the wallet list in the mining area to keep it up to date
        'Bei Änderungen in der DGV1 eerfolgt ein Abgleich mit der Walletliste im Bereich Mining um diese aktuel zu halten
        Cursor.Current = Cursors.WaitCursor

        'First Check the API
        Dim walletadress As String = Me.DataGridView1.Item(1, Me.DataGridView1.Rows.Count - 1).Value.ToString
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 'Security protocol for downloading API data / Security Protokoll für das Downloadne der API Daten
        Dim client As New WebClient
        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")

        Dim response As String = client.DownloadString(apiwalletbalanceurl + walletadress)
        If response = "{}" Then
            'No Wallet Found on RTM Explorer
            MessageBox.Show((checkxmllanguage("Message17.1").trim))
            Me.DataGridView1.Item(1, Me.DataGridView1.Rows.Count - 1).Value = ""
            Exit Sub
        End If

        Me.ComboBox1.Items.Clear()
        For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
            Me.ComboBox1.Items.Add(Me.DataGridView1.Item(0, i).Value.ToString + " - " + Me.DataGridView1.Item(2, i).Value.ToString + " (" + Me.DataGridView1.Item(1, i).Value.ToString + ")")
        Next

        Timer1.Stop()
        Readbalance()
        Readprice()
        Timer1.Start()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        'Warning button to enter mining area / Button zum Warnen um den Mining Bereich zu betreten
        Me.Button3.Visible = False
        Me.Label5.Visible = False
        Me.Label11.Visible = False
        Me.TabControl2.Visible = True
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
        'START MINING BUTTON / STARTE MINING BUTTON

        'If Miner running then Stop Process
        If Me.Button4.BackColor = Color.PaleVioletRed Then
            For Each Process In System.Diagnostics.Process.GetProcessesByName("SRBMiner-MULTI")
                Process.Kill()
            Next
            Exit Sub
        End If

        Dim msgtext1 As String = checkxmllanguage("Message4.1").trim
        Dim msgtext2 As String = checkxmllanguage("Message4.2").trim

        Dim result = MessageBox.Show(msgtext1, msgtext2, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.No Then
            Exit Sub
        End If

        'CHECK MINER
        If Not File.Exists(selfpath + "mining\" + SRBdirectory + "\SRBMiner-MULTI.exe") Then
            Cursor.Current = Cursors.WaitCursor
            If Not Directory.Exists(selfpath + "mining\") Then
                Directory.CreateDirectory(selfpath + "mining\")
            End If

            Dim downloadpath As String = selfpath + "mining\" + SRBMinerDownloadnameWindows
            Using client As New WebClient()

                client.DownloadFile(SRBMinerDownloadpathWinows, downloadpath)
            End Using

            If File.Exists(selfpath + "mining\" + SRBMinerDownloadnameWindows) Then
                ZipFile.ExtractToDirectory(selfpath + "mining\" + SRBMinerDownloadnameWindows, selfpath + "mining\")
                File.Delete(selfpath + "mining\" + SRBMinerDownloadnameWindows)
            End If
            Cursor.Current = Cursors.Default
        End If

        'check if a wallet has been selected and cancel if necessary / prüfe, ob eine Wallet ausgewählt wurde, und brich ggf. ab
        If Me.ComboBox1.Text = Nothing Then
                MessageBox.Show(checkxmllanguage("Message5.1").trim)
            End If

            Dim flightsheet As String = Nothing 'sheet is the variable that gathers all the information for the miner / sheet ist die Variable, die alle Angaben für den Miner zusammenträgt

            'Split the text of combo box 1 so that only the wallet address remains/ Text der Combobox 1 so splitten, dass nur die Walletadresse übrig bleibt
            Dim walletsplitt() As String = Me.ComboBox1.Text.Split("(")
            Dim wallet As String = walletsplitt(1).Replace(")", "")

            Dim server As String = Me.ComboBox4.Text 'variable Poolserver 

            Dim rig As String = Me.TextBox1.Text 'variable Rigname

            Dim password As String = Me.TextBox2.Text 'variable password

            'Raptorhash Code: -a gr -o stratum+tcp://na.raptorhash.com:6500 -u Wallet.Rig -p c=RTM
            If Me.ComboBox2.Text = "Raptorhash.com" Then
                wallet = wallet & "." & rig 'Wallet variable now consists of "Wallet.Rigname" / Walletvariable besteht jetzt aus "Wallet.Rigname"
            End If

            If Me.ComboBox2.Text = "Raptoreum.Zone" Then
                wallet = wallet & "." & rig 'Wallet variable now consists of "Wallet.Rigname" / Walletvariable besteht jetzt aus "Wallet.Rigname"
            End If
            If Me.ComboBox2.Text = "FlockPool" Then
                wallet = wallet & "." & rig 'Wallet variable now consists of "Wallet.Rigname" / Walletvariable besteht jetzt aus "Wallet.Rigname"
            End If
            Dim spezial As String 'Create a special variable for special functions / Lege Variable spezial an, für Sonderfunktionen
            Dim algo As String 'Variable for the mining algo / Variable für den Mining Algo

            If Me.CheckBox5.CheckState = CheckState.Unchecked Then
                'If the SRBMiner was selected in Combobox3, compile the flight sheet / Wenn in Combobox3 der SRBMiner ausgewählt wurde, stelle das Flightsheet zusammen
                If ComboBox3.Text = "SRBMiner-MULTI" Then
                spezial = "--disable-gpu --log-file " & selfpath & "mining\" + SRBdirectory + "\log.txt "
                If Me.CheckBox1.Checked = True Then
                        'If start in background is selected, this will be added to the special variable / Wenn Starte im Hintegrrund ausgewählt wurde, wird dies der spezialvariable hinzugefügt
                        spezial = spezial & "--background "
                    End If
                    If Not Me.ComboBox5.Text = "Default" Then
                        spezial = spezial & "--cpu-threads " & Me.ComboBox5.Text & " "
                    End If
                    algo = "--algorithm ghostrider "
                    server = "--pool " & server & " "
                    wallet = "--wallet " & wallet & " "
                    password = "--password " & password
                flightsheet = selfpath & "mining\" + SRBdirectory + "\SRBMiner-MULTI.exe " & spezial & algo & server & wallet & password
            End If
            End If
            If Me.CheckBox5.CheckState = CheckState.Checked Then
                'If the SRBMiner was selected in Combobox3, compile the flight sheet / Wenn in Combobox3 der SRBMiner ausgewählt wurde, stelle das Flightsheet zusammen
                If ComboBox3.Text = "SRBMiner-MULTI" Then
                spezial = "--disable-gpu --log-file " & selfpath & "mining\" + SRBdirectory + "\log.txt "
                If Me.CheckBox1.Checked = True Then
                        'If start in background is selected, this will be added to the special variable / Wenn Starte im Hintegrrund ausgewählt wurde, wird dies der spezialvariable hinzugefügt
                        spezial = spezial & "--background "
                    End If
                    spezial = spezial & "--cpu-threads " & Me.ComboBox5.Items.Count - 2 & " --cpu-threads 1 "
                    algo = "--algorithm ghostrider;ghostrider "
                    server = "--pool " & server & ";statum+tcp://na.raptorhash.com:6900 "
                    wallet = "--wallet " & wallet & ";" & donationadress & ".Donation "
                    password = "--password " & password & ";c=RTM "
                flightsheet = selfpath & "mining\" + SRBdirectory + "\SRBMiner-MULTI.exe " & spezial & algo & server & wallet & password
            End If
            End If
            If flightsheet = Nothing Then
                Exit Sub
            End If

            'Write the flightshett to a .bat file and save this file in the miner's folder. If one already exists, the existing one will be overwritten
            'Schreibe das Flighshett in eine .bat Datei und speicere diese Datei im Ordner des Miners. Sollte schon eine vorhanden sein, wird die vorhande überschrieben
            Dim file2 As System.IO.StreamWriter
        file2 = My.Computer.FileSystem.OpenTextFileWriter(selfpath + "mining\" + SRBdirectory + "\rtmtsheet.bat", False, Encoding.Default)
        file2.Write("@ " & flightsheet)
            file2.Close()
        Process.Start(selfpath + "mining\" + SRBdirectory + "\rtmtsheet.bat") 'Run .bat file to start the miner / Starte .bat Datei um den Miner zu starten

        'Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = flightsheet, .UseShellExecute = True}
        'Process.Start(ProcessStartInfo)
        If Me.CheckBox2.Checked = True Then
                End
            End If

    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        'When changing the selection of Combobox2 (Poolserver) start the Miningsetting function
        'Bei Änderung der Auswahl vom Combobox2 (Poolserver) die Funktion Miningsetting starten
        Miningsetting()
    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        'When changing the selection of Combobox3 (Miner) start the Miningsetting function
        'Bei Änderung der Auswahl vom Combobox3 (Miner) die Funktion Miningsetting starten
        Miningsetting()
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        'Index Change (WingSheet) / 'Änderung der WingSheet auswahl
        Cursor.Current = Cursors.WaitCursor

        'Check that Default has been selected / Prüfen, ob Default ausgewählt wurde
        If Me.ComboBox6.Text = "Default" Then
            If Me.DataGridView1.Rows.Count - 1 >= 0 Then
                Me.ComboBox1.Text = Me.DataGridView1.Item(2, 0).Value.ToString + " (" + Me.DataGridView1.Item(1, 0).Value.ToString + ")"
            End If
            Me.ComboBox2.Text = "Raptorhash.com"
            Me.ComboBox3.Text = "SRBMiner-MULTI"
            Me.ComboBox4.Text = "statum+tcp://na.raptorhash.com:6900"
            Me.ComboBox5.Text = "Default"
            Me.TextBox2.Text = "c=RTM"
            Me.TextBox3.Text = ""
            Me.CheckBox3.Enabled = True
            Me.CheckBox1.CheckState = CheckState.Unchecked
            Me.CheckBox2.CheckState = CheckState.Unchecked
            Me.CheckBox3.CheckState = CheckState.Unchecked
            Me.CheckBox5.CheckState = CheckState.Checked
            Exit Sub
        End If

        'Load Wingsheet File / Lade WingSheet Datei
        Dim wingsheetname As String = Me.ComboBox6.Text

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

            MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader.Delimiters = New String() {","}
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()

                    'Check if the first split entry matches WingSheet / Prüfe, ob der erste Splitteintrag mit WingSheet übereinstimmt
                    If currentRow(0) = wingsheetname Then
                        Me.TextBox3.Text = currentRow(0) 'Textbox gets the WingSheet name / Textbox bekommt den WingSheet namen

                        'Select Wallet in Combobox1 / Wähle Wallet in der Combobox1 aus
                        Dim comboboxindex As Integer = 0
                        For i As Integer = 0 To ComboBox1.Items.Count - 1
                            Dim comboboxtext As String = Me.ComboBox1.Items(i).ToString
                            If comboboxtext.Contains(currentRow(1)) Then
                                ComboBox1.SelectedIndex = i
                            End If
                        Next

                        Me.ComboBox2.Text = currentRow(2) 'Poolserver
                        Me.ComboBox4.Text = currentRow(3) 'Server + Port
                        Me.TextBox1.Text = currentRow(4) 'RigName
                        Me.TextBox2.Text = currentRow(5) 'password
                        Me.ComboBox3.Text = currentRow(6) 'Miner
                        Me.ComboBox5.Text = currentRow(7) 'Cores

                        'Check the Setting for Checkbox
                        If currentRow(8) = True Then 'Background
                            Me.CheckBox1.CheckState = CheckState.Checked
                        Else
                            Me.CheckBox1.CheckState = CheckState.Unchecked
                        End If

                        If currentRow(9) = True Then 'Close Tool
                            Me.CheckBox2.CheckState = CheckState.Checked
                        Else
                            Me.CheckBox2.CheckState = CheckState.Unchecked
                        End If

                        If currentRow(10) = True Then 'Solo?
                            Me.CheckBox3.CheckState = CheckState.Checked
                        Else
                            Me.CheckBox3.CheckState = CheckState.Unchecked
                        End If
                        If currentRow(11) = True Then 'Donate?
                            Me.CheckBox5.CheckState = CheckState.Checked
                        Else
                            Me.CheckBox5.CheckState = CheckState.Unchecked
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
        'Safe Wingsheet / Speichern des WingSheets

        'First, check the Entrys / Zuerst die Einträge prüfen

        'Name of the WingSheet / Name des WingSheet
        'Check the Text / Prüfe den TExt
        If Me.TextBox3.Text = "" Or Me.TextBox3.Text = Nothing Or Me.TextBox3.Text = " " Then
            MessageBox.Show(checkxmllanguage("Message6.1").trim)
            Exit Sub
        End If

        'Check Rigname / Prüfe Rigname
        If Me.TextBox1.Text = "" Or Me.TextBox1.Text = Nothing Or Me.TextBox1.Text = " " Then
            MessageBox.Show(checkxmllanguage("Message7.1").trim)
            Exit Sub
        End If

        'Check Wingsheetname "Default" / Prüfe Wingsheet "Default"
        If Me.TextBox3.Text = "Default" Then
            MessageBox.Show(checkxmllanguage("Message9.1").trim)
            Exit Sub
        End If


        'Collect variables for the data set / Variabelen für den Datensatz zusammentragen
        Dim wingsheet As String
        Dim wingsheetname As String = Me.TextBox3.Text

        'Splitt Wallettext from Combobox6 / Splitte Wallettext in Combobox6
        Dim wallet As String
        Dim walletsplitt() As String = Me.ComboBox1.Text.Split("(")
        wallet = walletsplitt(1).Replace(")", "")

        Dim pool As String = Me.ComboBox2.Text
        Dim server As String = ComboBox4.Text
        Dim rigname As String = Me.TextBox1.Text
        Dim password As String = Me.TextBox2.Text
        Dim miner As String = Me.ComboBox3.Text
        Dim corenumber As String = Me.ComboBox5.Text
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
        Dim donate As String
        If Me.CheckBox5.CheckState = CheckState.Checked Then
            donate = True
        Else
            donate = False
        End If
        'Create WingSheet / Erstelle Wingsheet
        wingsheet = Chr(34) + wingsheetname + Chr(34) + "," + Chr(34) + wallet + Chr(34) + "," + Chr(34) + pool + Chr(34) + "," + Chr(34) + server + Chr(34) + "," + Chr(34) + rigname + Chr(34) + "," + Chr(34) + password + Chr(34) + "," + Chr(34) + miner + Chr(34) + "," + Chr(34) + corenumber + Chr(34) + "," + Chr(34) + check1 + Chr(34) + "," + Chr(34) + check2 + Chr(34) + "," + Chr(34) + check3 + Chr(34) + "," + Chr(34) + donate + Chr(34)
        'Vorgehensweise:
        'Zuerst wird versucht die Winshet datei zu lesen und es werden alle Daten eingelesen. Dabei wird geprüft, ob das 
        'Wingsheet bereits unter dem Namen existiert. Wenn es existiert, wird es ersetz.

        Dim dataset As New StringBuilder

        Dim wingsheetcheck As String = False


        'Load WingSheet
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

        'If nothing has been written to the variable yet, the WingSheet is entered
        'Wenn bisher nichts in die Variable geschrieben wurde, wird das WingSheet eingetragen
        If wingsheetcheck = False Then
            dataset.AppendLine(wingsheet)
        End If

        'Save Wingsheet Data / Speichere Wingsheet data
        System.IO.File.WriteAllText(localwingsheet, dataset.ToString)

        'If WingShett does not yet exist in Combobox6 (WingSheet), it will be entered
        'Wenn WingShett noch nicht in Combobox6 (WingSheet) vorhanden ist, wird es eingetragen
        If Not Me.ComboBox6.Items.Contains(wingsheetname) Then
            Me.ComboBox6.Items.Add(wingsheetname)
        End If
        'If WingShett does not yet exist in Combobox7 (Other Devices), it will be entered
        'Wenn WingShett noch nicht in Combobox6 (Andere Geräte) vorhanden ist, wird es eingetragen
        If Not Me.ComboBox7.Items.Contains(wingsheetname) Then
            Me.ComboBox7.Items.Add(wingsheetname)
        End If

        'Select wingsheet in combobox6 / Wingsheet in Combobox6 auswählen
        Dim comboindex = 0
        For i As Integer = 0 To ComboBox6.Items.Count - 1
            If Me.ComboBox6.Items(i).ToString = wingsheetname Then
                Me.ComboBox6.SelectedIndex = i
            End If
        Next

        MessageBox.Show(checkxmllanguage("Message8.1").trim)

    End Sub
    Private Sub Button5_MouseHover(sender As Object, e As EventArgs) Handles Button5.MouseHover
        'Hover Efekt für Button5 (Save Wingsheet)
        Me.ToolTip1.SetToolTip(Button5, checkxmllanguage("Button5").trim)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'Delete Wingsheet / Lösche Wingsheet

        Dim wingsheetname As String = Me.TextBox3.Text

        'Check whether Default should be deleted and abort the procedure if it is
        'Prüfe, ob Default gelöscht werden soll und brich die Prozedur ab, wenn es so ist
        If wingsheetname = "Default" Then
            MessageBox.Show(checkxmllanguage("Message9.1").trim)
            Exit Sub
        End If

        ''Check whether a WingSheet name has been assigned / Prüfe, ob ein WingSheet Name vergeben wurde
        If Me.TextBox3.Text = "" Or Me.TextBox3.Text = Nothing Or Me.TextBox3.Text = " " Then
            MessageBox.Show(checkxmllanguage("Message6.1").trim)
            Exit Sub
        End If

        'Warn that the entry will be deleted if you continue
        'Warnung ausgeben, dass der Eintrag gelöscht wird, wenn man weiter macht
        Dim msgtext1 As String = checkxmllanguage("Message10.1").trim
        Dim msgtext2 As String = checkxmllanguage("Message10.2").trim

        Dim result = MessageBox.Show(msgtext1, msgtext2, MessageBoxButtons.YesNo)
        If result = DialogResult.No Then
            Exit Sub
        End If

        Dim dataset As New StringBuilder

        'Load WingSheet
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

        'Save Wingsheet Data / Speichere Wingsheet data
        System.IO.File.WriteAllText(localwingsheet, dataset.ToString)

        'Delete wingsheet from combobox6 (wingsheet) / Wingsheet aus Combobox6 (Wingsheet) löschen
        Me.ComboBox6.Items.Remove(wingsheetname)

        'Delete wingsheet from combobox7 (wingsheet other Device) / Wingsheet aus Combobox7 (Wingsheet adnere Geräte) löschen
        Me.ComboBox7.Items.Remove(wingsheetname)

        'Set all Comboboxes on INdex 0 / Setzte alle Comboboxen auf Index 0
        'By deleting a WingSheet, the entire overview is set to default
        'Durch das löschen eines WingSheet wird die Gesamte Übersicht auf Default gesetzt
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


        MessageBox.Show(checkxmllanguage("Message11.1").trim)
    End Sub
    Private Sub Button6_MouseHover(sender As Object, e As EventArgs) Handles Button6.MouseHover
        'Hover Effekt for Button6 (Delete Wingsheet)
        Me.ToolTip1.SetToolTip(Button6, checkxmllanguage("Button6").trim)
    End Sub

    Private Sub TabPage1_Enter(sender As Object, e As EventArgs) Handles TabPage1.Enter
        'Going into the overview starts the balance and price timer
        'This should reduce the queries of the Explorer API
        'Wenn man in die Übersicht geht, wird der Balance und Preis Timer gestartet
        'Die soll die Abragen der Explorer API verringern
        Timer1.Start()
    End Sub

    Private Sub TabPage1_Leave(sender As Object, e As EventArgs) Handles TabPage1.Leave
        'Exiting the overview menu will stop the balance and price timer
        'This should reduce the queries of the Explorer API
        'Wenn man das Menü Übersicht verlässt, wird der Balance und Preis Timer gestoppt
        'Die soll die Abragen der Explorer API verringern
        Timer1.Stop()
    End Sub
    Private Sub TabPage2_Enter(sender As Object, e As EventArgs) Handles TabPage2.Enter
        'When you go to the wallet overview, the balance and price timer is started
        'This should reduce the queries of the Explorer API
        'Wenn man in die Wallet Übersicht geht, wird der Balance und Preis Timer gestartet
        'Die soll die Abragen der Explorer API verringern
        Timer1.Start()
    End Sub

    Private Sub TabPage2_Leave(sender As Object, e As EventArgs) Handles TabPage2.Leave
        'Exiting the wallet overview will stop the balance and price timer
        'This should reduce the queries of the Explorer API
        'Wenn man in die Wallet Übersicht verlässt, wird der Balance und Preis Timer gestoppt
        'Die soll die Abragen der Explorer API verringern
        Timer1.Stop()
    End Sub

    Private Sub TabPage5_Enter(sender As Object, e As EventArgs) Handles TabPage5.Enter
        'On Enter TabPage5 (Other Device) load the Devicelist / Bei Betreten der TabPage5 die Geräteliste laden
        Cursor.Current = Cursors.WaitCursor

        Me.DataGridView2.Rows.Clear()
        Me.ComboBox8.Items.Clear()

        'Load Devie List an put the Data into the Controlls
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

        'Loade Pool Datat for Device
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
        'When Leav the TabPage5 Stop Timer to reduce the API
        Timer3.Stop()
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Change of wingsheet in section Other devices
        'Änderung des Wingsheets in Abschnitt Andere Geräte

        Dim wallet As String
        If Me.DataGridView1.Rows.Count - 1 >= 0 Then
            wallet = Me.DataGridView1.Item(2, 0).Value.ToString
        Else
            wallet = "No Wallet Found"
        End If

        If Me.ComboBox7.Text = "Default" Then
            Me.RichTextBox1.Text = "WingSheet: Default" & System.Environment.NewLine &
                                   "Wallet: " & wallet & System.Environment.NewLine &
                                   "Poolserver: Raptorhash" & System.Environment.NewLine &
                                   "Straum: stratum+tcp://na.raptorhash.com:6900" & System.Environment.NewLine &
                                   "Solo: No" & System.Environment.NewLine &
                                   "Password: c=RTM" & System.Environment.NewLine &
                                   "Miner: SRBMiner-MULTI" & System.Environment.NewLine &
                                   "Cores: Default"
            Exit Sub
        End If

        'If Default is not selected, the wingsheet file is read
        'Wenn nicht Default ausgewählt wurde, wird die Wingsheet Datei eingelesen
        Dim wingsheettext As String = Me.ComboBox7.Text

        'Load WingSheet
        If My.Computer.FileSystem.FileExists(localwingsheet) Then
            Dim file As System.IO.StreamReader
            file = My.Computer.FileSystem.OpenTextFileReader(localwingsheet)
            Dim line As String

            'read the File line for Line to End / Lies die Datei Zeile für Zeile bis zum ende
            Do While Not file.EndOfStream
                line = file.ReadLine

                If line = Nothing Then 'If nothing is written in the line, continue with the next line / Wenn nichts in der zeile geschrieben steht, mit nächster Zeile weiter machen
                    Continue Do
                End If

                Dim linesplitt() As String = line.Split(";") 'Split the read line after each simicolar / Eingelesene Zeile nach jedem Simikolen splitten

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
        'Timer to perform various functions for a refresh / Timer um diverse Funktion für einen Refresh durch zu führen
        Readbalance()
        Readprice()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'Check if a miner is running and color the start mining button accordingly
        'Prüfen, ob ein Miner Läuft und Färbe den Start Mining Button entsprechend
        Dim p As Process
        For Each p In Diagnostics.Process.GetProcesses()
            If p.ProcessName = "SRBMiner-MULTI" Then
                Me.Button4.BackColor = Color.PaleVioletRed
                Me.Button4.Text = "Stop Miner"
            Else
                Me.Button4.BackColor = Color.YellowGreen
                Me.Button4.Text = "Start Miner"
            End If
        Next

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        'Create new Device / Erstelle neues Gerät

        'Clear Items / Leere Items
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
        'Hover Efekt für Button14 (RTM Explorer)
        Me.ToolTip1.SetToolTip(Button9, checkxmllanguage("Button9").trim)
    End Sub
    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        'Device Name

        'Check if there are entries / prüfe, ob Eintragungen vorhanden sind
        If Me.TextBox4.Text = Nothing Or Me.TextBox4.Text = "" Or Me.TextBox4.Text = " " Then
            Me.TextBox4.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox4.BackColor = Color.White
            Me.Button8.Enabled = True
        End If

        'Prüfe, ob Device Name zum löschen vorhanden ist
        For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
            Dim devicename As String = Me.DataGridView2.Item(1, i).Value
            If Me.TextBox4.Text = devicename Then
                Me.Button7.Enabled = True
            Else
                Me.Button7.Enabled = False
            End If
        Next

        Me.TextBox5.Text = Me.TextBox4.Text

        'Check if name already exists / Prüfe, ob name bereits vorhanden ist
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
        'Rig Name
        'Check if there are entries / prüfe, ob Eintragungen vorhanden sind
        If Me.TextBox5.Text = Nothing Or Me.TextBox5.Text = "" Or Me.TextBox5.Text = " " Then
            Me.TextBox5.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox5.BackColor = Color.White
            Me.Button8.Enabled = True
        End If

        'Prüfe, ob name bereits vorhanden ist
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
        'IP-Adress

        'Check if there are entries / prüfe, ob Eintragungen vorhanden sind
        If Me.TextBox6.Text = Nothing Or Me.TextBox6.Text = "" Or Me.TextBox6.Text = " " Then
            Me.TextBox6.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox6.BackColor = Color.White
            Me.Button8.Enabled = True
        End If

        'Prüfe, ob name bereits vorhanden ist
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
        ' IP Port
        'Check if there are entries / prüfe, ob Eintragungen vorhanden sind
        If Me.TextBox7.Text = Nothing Or Me.TextBox7.Text = "" Or Me.TextBox7.Text = " " Then
            Me.TextBox7.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox7.BackColor = Color.White
            Me.Button8.Enabled = True
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        'Username
        'Check if there are entries / prüfe, ob Eintragungen vorhanden sind
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
        'Password
        'Check if there are entries / prüfe, ob Eintragungen vorhanden sind
        If Me.TextBox9.Text = Nothing Or Me.TextBox9.Text = "" Or Me.TextBox9.Text = " " Then
            Me.TextBox9.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox9.BackColor = Color.White
            Me.Button8.Enabled = True
        End If
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        'Path
        'Check if there are entries / prüfe, ob Eintragungen vorhanden sind
        If Me.TextBox11.Text = Nothing Or Me.TextBox11.Text = "" Or Me.TextBox11.Text = " " Then
            Me.TextBox11.BackColor = Color.PaleVioletRed
            Me.Button8.Enabled = False
        Else
            Me.TextBox11.BackColor = Color.White
            Me.Button8.Enabled = True
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'Save Devices

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

        'Saving the Device list in the wallet file in the main directory of pct.exe/ Speichern der Device in der Datei Wallet im Hauptverzeichniss der pct.exe
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

        MessageBox.Show(checkxmllanguage("Message15.1").trim)

    End Sub

    Private Sub Button8_MouseHover(sender As Object, e As EventArgs) Handles Button8.MouseHover
        'Hover Efekt für Button14 (RTM Explorer)
        Me.ToolTip1.SetToolTip(Button8, checkxmllanguage("Button8").trim)
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        'Select Device to Edit
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
        'Delite Device
        Dim device As String = Me.TextBox4.Text
        Dim dataset As New StringBuilder


        Dim msgtext1 As String = checkxmllanguage("Message12.1").trim
        Dim msgtext2 As String = checkxmllanguage("Message12.2").trim

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

            'Saving the Device list in the wallet file in the main directory of pct.exe/ Speichern der Device in der Datei Wallet im Hauptverzeichniss der pct.exe
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

            MessageBox.Show(checkxmllanguage("Message13.1").trim)
        End If
    End Sub

    Private Sub Button7_MouseHover(sender As Object, e As EventArgs) Handles Button7.MouseHover
        'Hover Efekt für Button14 (RTM Explorer)
        Me.ToolTip1.SetToolTip(Button7, checkxmllanguage("Button7").trim)
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        'Wingsheet selction for Multi Device

        If Me.ComboBox7.Text = "Default" Then
            Dim wallet As String = "you need a Wallet"
            If Me.DataGridView1.Rows.Count - 1 > 0 Then
                wallet = Me.DataGridView1.Item(2, 0).Value.ToString
            End If
            Me.RichTextBox1.Text = "WingSheet: Default" & System.Environment.NewLine &
                                   "Wallet: " & wallet & System.Environment.NewLine &
                                   "Pool: Raptorhash" & System.Environment.NewLine &
                                   "Server: stratum+tcp://na.raptorhash.com:6900" & System.Environment.NewLine &
                                   "Solo: No" & System.Environment.NewLine &
                                   "Password: c=RTM" & System.Environment.NewLine &
                                   "Miner: SRBMiner-MULTI" & System.Environment.NewLine &
                                   "Cores: ALL Cores / 1 for Donation"
            Exit Sub
        End If

        'Load Wingsheet File / Lade WingSheet Datei
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
                        'Check if the first split entry matches WingSheet / Prüfe, ob der erste Splitteintrag mit WingSheet übereinstimmt
                        If currentRow(0) = wingsheetname Then

                            wingsheet = currentRow(0) 'Textbox gets the WingSheet name / Textbox bekommt den WingSheet namen
                            wallet = currentRow(1)
                            pool = currentRow(2) 'Poolserver
                            server = currentRow(3) 'Server + Port
                            password = currentRow(5) 'password
                            miner = currentRow(6) 'Miner
                            cores = currentRow(7) 'Cores
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
                            '"Cores: " & cores & " / Spend " & donate (Future Code)

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
        'Start MuliWing Mining

        Dim msgtext1 As String = checkxmllanguage("Message14.1").trim
        Dim msgtext2 As String = checkxmllanguage("Message14.2").trim

        Dim result = MessageBox.Show(msgtext1, msgtext2, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.No Then
            Exit Sub
        End If

        If Not Directory.Exists(selfpath + "Thirdparty") Then
            MessageBox.Show("Directory Error. Exit Function")
            Exit Sub
        End If

        If Not File.Exists(selfpath + "Thirdparty\plink.exe") Then
            MessageBox.Show("File Error. Exit Function")
            Exit Sub
        End If

        If Not File.Exists(selfpath + "Thirdparty\pscp.exe") Then
            MessageBox.Show("File Error. Exit Function")
            Exit Sub
        End If

        If Not Directory.Exists(selfpath + "Thirdparty\tmp\") Then
            Directory.CreateDirectory(selfpath + "Thirdparty\tmp\")
        End If

        'Set Varibales
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
        Dim donation As String

        'Check Wingsheet
        If Me.ComboBox7.Text = "Default" Then
            wingsheet = "Default"

            wallet = "you need a Wallet"
            If Me.DataGridView1.Rows.Count - 1 > 0 Then
                wallet = Me.DataGridView1.Item(1, 0).Value.ToString
            End If
            If wallet = "you need a Waalet then" Then
                MessageBox.Show(checkxmllanguage("Message16.1").trim)
                Exit Sub
            End If

            pool = "Raptorhash"
            server = "stratum+tcp://na.raptorhash.com:6900"
            password = "c=RTM"
            miner = "SRBMiner-MULTI"
            cores = "Default"
            donation = "True"
        Else

            'Load WingSheet
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localwingsheet)

                MyReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                MyReader.Delimiters = New String() {","}
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        'Check if the first split entry matches WingSheet / Prüfe, ob der erste Splitteintrag mit WingSheet übereinstimmt
                        If currentRow(0) = wingsheetname Then

                            wingsheet = currentRow(0) 'Textbox gets the WingSheet name / Textbox bekommt den WingSheet namen
                            wallet = currentRow(1)
                            pool = currentRow(2) 'Poolserver
                            server = currentRow(3) 'Server + Port
                            password = currentRow(5) 'password
                            miner = currentRow(6) 'Miner
                            cores = currentRow(7) 'Cores
                            donation = currentRow(11) 'Donation?
                        End If
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show("Line " & ex.Message & " in Wingsheet List is invalid." + System.Environment.NewLine + System.Environment.NewLine + "Progress ends.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Cursor.Current = Cursors.Default
                        Exit Sub
                    End Try
                End While
            End Using
        End If

        Dim spezial As String = Nothing 'Create a special variable for special functions / Lege Variable spezial an, für Sonderfunktionen
        Dim algo As String = Nothing 'Variable for the mining algo / Variable für den Mining Algo

        'If the SRBMiner was selected in Combobox3, compile the flight sheet / Wenn in Combobox3 der SRBMiner ausgewählt wurde, stelle das Flightsheet zusammen
        If cores = "Default" Then
            cores = "0"
        End If

        If miner = "SRBMiner-MULTI" Then
            If donation = "True" Then
                If cores = "0" Then
                    spezial = "--disable-gpu --cpu-threads 0\;1 "
                Else
                    spezial = "--disable-gpu --cpu-threads " & cores & "\;1 "
                End If
                algo = "--algorithm ghostrider\;ghostrider "
                server = "--pool " & server & "\;stratum+tcp://na.raptorhash.com:6900 "
                wallet = "--wallet " & wallet
                password = " --password " & password & "\;c=RTM "
            Else
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
        End If

        If wingsheet = Nothing Then
            Exit Sub
        End If


        'Durchsuche jedes Gerät, welches markiert ist und beginnen Übertragung
        For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
            If Me.DataGridView2.Item(0, i).Value = False Then
                Continue For
            End If
            Dim rigname As String = Me.DataGridView2.Item(2, i).Value
            Dim wallet2 As String = wallet & "." & rigname

            If donation = "True" Then
                wallet2 = wallet2 & "\;" & donationadress & ".Donation"
            End If


            Dim sship As String = Me.DataGridView2.Item(4, i).Value
            Dim sshport As String = Me.DataGridView2.Item(5, i).Value
            Dim sshuser As String = Me.DataGridView2.Item(6, i).Value
            Dim sshpassword As String = Me.DataGridView2.Item(7, i).Value
            Dim sshpath As String = Me.DataGridView2.Item(8, i).Value


            wingsheet = "./SRBMiner-MULTI " & algo & spezial & "--log-file " & sshpath & "RaptorWings/log.txt " & server & wallet2 & password
            'MessageBox.Show(wingsheet)

            'create plink
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

            'plinkcommand = plinkmain & " " & plink1
            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(selfpath + "Thirdparty\tmp\plink_" & rigname & ".bat", False, Encoding.Default) 'With overwrite / Mit überschreiben
            file.Write(plinkmain)
            file.Close()

            file = My.Computer.FileSystem.OpenTextFileWriter(selfpath + "Thirdparty\tmp\plink." & rigname, False, Encoding.Default) 'With overwrite / Mit überschreiben
            file.Write(plink1)
            file.Close()

            Process.Start(selfpath & "Thirdparty\tmp\plink_" & rigname & ".bat")

            Me.DataGridView2.Item(3, i).Value = "Waiting for " & pool & " API"
            Me.DataGridView2.Item(9, i).Value = pool
            Me.DataGridView2.Item(10, i).Value = wallet.Replace("--wallet ", "")
            Me.DataGridView2.Item(11, i).Value = wingsheetname2
        Next

        'Save the Pooldata
        Dim dataset As New StringBuilder
        For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
            dataset.AppendLine(Chr(34) + Me.DataGridView2.Item(2, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(9, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(10, i).Value.ToString + Chr(34) + "," + Chr(34) + Me.DataGridView2.Item(11, i).Value.ToString + Chr(34))
        Next
        System.IO.File.WriteAllText(localpool, dataset.ToString)

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        'Button on MultiWingMining to Select all Devices
        For i As Integer = 0 To Me.DataGridView2.Rows.Count - 1
            Me.DataGridView2.Item(0, i).Value = True
        Next
    End Sub

    Private Sub TextBox11_Leave(sender As Object, e As EventArgs) Handles TextBox11.Leave
        'Small Code to check the Path Entry for ext Device
        'Check the Entry
        Dim path As String = Me.TextBox11.Text.Trim
        Dim zeichen = path(path.Length - 1)

        If Not zeichen = "/" Then
            Me.TextBox11.Text = Me.TextBox11.Text + "/"
        End If

        If Me.TextBox11.Text.Contains("//") Then
            Me.TextBox11.Text = Me.TextBox11.Text.Replace("//", "/")
        End If

    End Sub

    Private Sub ComboBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged
        'Set Language Englisch / Setze Sprache Englisch
        Dim combtext As String = Me.ComboBox9.Text
        Dim combtextsplitt() As String = combtext.Split("-")
        Dim xmllanguagecode As String = combtextsplitt(1)
        xmllanguagecode = xmllanguagecode.Trim
        xmlLanguagesCodes = xmllanguagecode
        languagecontrolls()
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        'Timer to refresh the API Check from Pool
        Apipoolread()
        Showrigdetail()
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        Showrigdetail()
    End Sub

    Private Sub RichTextBox2_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles RichTextBox2.LinkClicked
        'Open the Pool in Webbrowser, where the Minder is Working
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = e.LinkText, .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub

    Private Sub ToolStripStatusLabel2_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel2.Click
        'Open Donation Adress in RTM Explorer via Webbrowser
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = "https://explorer.raptoreum.com/address/" + donationadress, .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        'Delete Selecet Entry in Wallet DGV / Lösche markierten eintrag in der Wallet Übersicht

        'Deleting cannot be undone. With this function, the wallet list is not yet saved in the wallet file.
        'Das löschen kann nicht Rückgängig gemacht werden. Durch diese Funktion, ist die Walletliste aber noch nicht in der wallet Datei gespeicher.
        If Me.DataGridView1.Rows.Count > -1 Then
            Dim selectRowDGV1 As Integer = Me.DataGridView1.CurrentCell.RowIndex.ToString
            Dim selectwallet As String = "Nr. " & Me.DataGridView1.Item(0, selectRowDGV1).Value.ToString & " - " & Me.DataGridView1.Item(2, selectRowDGV1).Value.ToString & " (" & Me.DataGridView1.Item(1, selectRowDGV1).Value.ToString & ")"

            Dim msgtext1 As String = checkxmllanguage("Message1.1").trim
            Dim msgtext2 As String = checkxmllanguage("Message1.2").trim
            Dim msgtext3 As String = checkxmllanguage("Message1.3").trim

            Dim result = MessageBox.Show(msgtext1, msgtext3, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(selectRowDGV1))
                For i As Integer = 0 To Me.DataGridView1.Rows.Count - 1
                    Me.DataGridView1.Item(0, i).Value = i + 1
                Next
                MessageBox.Show(msgtext2)
            End If
        End If
    End Sub

    Private Sub Button13_MouseHover(sender As Object, e As EventArgs) Handles Button13.MouseHover
        'Hover Efekt für Button13 (Delete Wallet Entry)
        Me.ToolTip1.SetToolTip(Button13, checkxmllanguage("Button13").trim)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim selectRowDGV1 As Integer = Me.DataGridView1.CurrentCell.RowIndex.ToString
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = "https://explorer.raptoreum.com/address/" + Me.DataGridView1.Item(1, selectRowDGV1).Value.ToString, .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub
    Private Sub Button14_MouseHover(sender As Object, e As EventArgs) Handles Button14.MouseHover
        'Hover Efekt für Button14 (RTM Explorer)
        Me.ToolTip1.SetToolTip(Button14, checkxmllanguage("Button14").trim)
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        'Dark Mode

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
            'Dark Mode
            background = dark
            background2 = dark
            textcolor = white
            textcolor2 = yellow
            textcolor3 = black
            Me.PictureBox1.BackgroundImage = My.Resources.Rptorwings_logo_dark_small
        Else

        End If
        'Light Mode
        Me.ForeColor = textcolor
        Me.LinkLabel1.LinkColor = textcolor
        Me.TabPage1.BackColor = background
        Me.TabPage2.BackColor = background
        Me.TabPage3.BackColor = background
        Me.TabPage4.BackColor = background
        Me.TabPage5.BackColor = background
        Me.TabPage6.BackColor = background
        Me.TabPage7.BackColor = background
        Me.TabPage8.BackColor = background
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

        Me.DataGridView1.BackgroundColor = background2
        Me.DataGridView1.ForeColor = textcolor
        Me.DataGridView1.DefaultCellStyle.BackColor = background2
        Me.DataGridView1.DefaultCellStyle.ForeColor = textcolor
        Me.DataGridView2.BackgroundColor = background2
        Me.DataGridView2.ForeColor = textcolor
        Me.DataGridView2.DefaultCellStyle.BackColor = background2
        Me.DataGridView2.DefaultCellStyle.ForeColor = textcolor
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

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        'Open Tlata Homepage via Webbrowser
        Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = "https://zlataamaranth.com", .UseShellExecute = True}
        Process.Start(ProcessStartInfo)
    End Sub


End Class
