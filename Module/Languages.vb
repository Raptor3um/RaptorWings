'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.IO

Module Languages

    Dim xmlimport As String = Nothing
    Dim xmlline() As String = Nothing

    Public Function Languagesxmlload() As IEnumerable(Of String)
        Form1.logging("Modul: LanguagesXMLload start")
        If My.Computer.FileSystem.FileExists(languagefile) Then
            Form1.logging("Modul: LanguagesXMLload: File found")
            Dim file As System.IO.StreamReader
            file = My.Computer.FileSystem.OpenTextFileReader(languagefile)
            xmlimport = file.ReadToEnd
            file.Close()
            Form1.logging("Modul: LanguagesXMLload: read in complete")

            xmlline = xmlimport.Split(System.Environment.NewLine)

        Else
            Form1.logging("Modul: LanguagesXMLload: File not found")
            MessageBox.Show("The Languages.xml could not be found.")
            End
        End If
        Form1.logging("Modul: LanguagesXMLload END")
    End Function

    Public Function FindLandguage()
        Form1.logging("Modul: FindLandguage start")
        Form1.logging("Modul: FindLandguage start: Systemlanguage = " + systemlanguage)
        Dim foundLanguage As String = False
        Dim foundcountryCode As String = False
        Dim countrycode As String = Nothing

        Dim countrytitle As String = Nothing

        For i As Integer = 0 To xmlline.Length - 1
            If xmlline(i).Contains("<Languages>") And foundLanguage = False Then
                foundLanguage = True
                Continue For
            End If

            If xmlline(i).Contains("</Languages>") And foundLanguage = False Then
                Exit For
            End If

            If xmlline(i).Contains("<CountryCode>") And foundLanguage = True Then
                countrycode = xmlline(i)
                countrycode = countrycode.Replace("<CountryCode>", "")
                countrycode = countrycode.Replace("</CountryCode>", "")
                countrycode = countrycode.Trim
                foundcountryCode = True
                Continue For
            End If

            If xmlline(i).Contains("<title>") And foundLanguage = True And foundcountryCode = True Then
                countrytitle = xmlline(i)
                countrytitle = countrytitle.Replace("<title>", "")
                countrytitle = countrytitle.Replace("</title>", "")
                countrytitle = countrytitle.Trim
                Continue For
            End If

            If xmlline(i).Contains("<XMLCode>") And foundLanguage = True And foundcountryCode = True Then
                Dim xmlcode As String = xmlline(i)
                xmlcode = xmlcode.Replace("<XMLCode>", "")
                xmlcode = xmlcode.Replace("</XMLCode>", "")
                xmlcode = xmlcode.Trim

                Dim combentry As String = countrytitle & " - " & xmlcode

                If Not Form1.ComboBox9.Items.Contains(combentry) Then
                    Form1.logging("Modul: FindLandguage: Add Entry in Combo9(Langauages): " + combentry)
                    Form1.ComboBox9.Items.Add(combentry)
                End If

                If countrycode = systemlanguage Then
                    xmlLanguagesCodes = xmlcode
                    For i2 As Integer = 0 To Form1.ComboBox9.Items.Count - 1
                        If Form1.ComboBox9.Items(i2).ToString.Contains(xmlcode) Then
                            Form1.logging("Modul: FindLandguage: Set Entry " + Str(i) + " in Combo9(Langauages): ")
                            Form1.ComboBox9.SelectedIndex = i2
                            Exit Function
                        End If
                    Next
                End If

                foundcountryCode = False
                countrycode = ""
            End If

        Next

        Form1.ComboBox9.Text = "English - EN"
        xmlLanguagesCodes = "EN"
        Form1.logging("Modul: FindLandguage END")
    End Function

    Public Function Checkxmllanguage(ByVal bezeichnung As String)
        Form1.logging("Modul: heckxmllanguag start")
        Dim found As String = False

        For i As Integer = 0 To xmlline.Length - 1
            If xmlline(i).Contains("<" & bezeichnung & ">") And found = False Then
                found = True
                Continue For
            End If

            If xmlline(i).Contains("</" & bezeichnung & ">") And found = True Then
                found = False
                Continue For
            End If

            If xmlline(i).Contains("<" & xmlLanguagesCodes & ">") And found = True Then
                Dim text As String = xmlline(i)
                text = text.Replace("<" & xmlLanguagesCodes & ">", "")
                text = text.Replace("</" & xmlLanguagesCodes & ">", "")
                If text.Contains("+NewLine+") Then
                    text = text.Replace("+NewLine+", System.Environment.NewLine)
                End If
                Checkxmllanguage = text
                Exit For
            End If
        Next
        Form1.logging("Modul: heckxmllanguag END")
    End Function
    Public Function Languagecontrolls()
        Form1.logging("Modul: Languagecontrolls Start")
        Form1.Button3.Text = Checkxmllanguage("Button3")
        Form1.Button4.Text = Checkxmllanguage("Button4")
        Form1.Button12.Text = Checkxmllanguage("Button12")
        Form1.Button15.Text = Checkxmllanguage("Button15")
        Form1.Button16.Text = Checkxmllanguage("Button16")
        Form1.Button17.Text = Checkxmllanguage("Button17")
        Form1.Button21.Text = Checkxmllanguage("Button21")
        Form1.Label2.Text = Checkxmllanguage("Label2")
        Form1.Label4.Text = Checkxmllanguage("Label4")
        Form1.Label5.Text = Checkxmllanguage("Label5")
        Form1.Label6.Text = Checkxmllanguage("Label6")
        Form1.Label7.Text = Checkxmllanguage("Label7")
        Form1.Label8.Text = Checkxmllanguage("Label8")
        Form1.Label9.Text = Checkxmllanguage("Label9")
        Form1.Label10.Text = Checkxmllanguage("Label10")
        Form1.Label11.Text = Checkxmllanguage("Label11")
        Form1.Label12.Text = Checkxmllanguage("Label12")
        Form1.Label13.Text = Checkxmllanguage("Label13")
        Form1.Label15.Text = Checkxmllanguage("Label15")
        Form1.Label16.Text = Checkxmllanguage("Label16")
        Form1.Label18.Text = Checkxmllanguage("Label18")
        Form1.Label19.Text = Checkxmllanguage("Label19")
        Form1.Label20.Text = Checkxmllanguage("Label20")
        Form1.Label32.Text = Checkxmllanguage("Label32")
        Form1.Label35.Text = Checkxmllanguage("Label35")
        Form1.Label36.Text = Checkxmllanguage("Label36")
        Form1.Label37.Text = Checkxmllanguage("Label37")
        Form1.Label38.Text = Checkxmllanguage("Label38")
        Form1.Label39.Text = Checkxmllanguage("Label39")
        Form1.Label40.Text = Checkxmllanguage("Label40")
        Form1.Label41.Text = Checkxmllanguage("Label41")
        Form1.Label42.Text = Checkxmllanguage("Label42")
        Form1.Label44.Text = Checkxmllanguage("Label44")
        Form1.TabPage1.Text = Checkxmllanguage("TabPage1")
        Form1.TabPage2.Text = Checkxmllanguage("TabPage2")
        Form1.TabPage3.Text = Checkxmllanguage("TabPage3")
        Form1.TabPage6.Text = Checkxmllanguage("TabPage6")
        Form1.TabPage9.Text = Checkxmllanguage("TabPage9")
        Form1.TabPage10.Text = Checkxmllanguage("TabPage10")
        Form1.DataGridView1.Columns(1).HeaderText = Checkxmllanguage("Column11")
        Form1.DataGridView1.Columns(2).HeaderText = Checkxmllanguage("Column12")
        Form1.DataGridView3.Columns(1).HeaderText = Checkxmllanguage("Column12")
        Form1.CheckBox1.Text = Checkxmllanguage("Checkbox1")
        Form1.CheckBox2.Text = Checkxmllanguage("Checkbox2")
        Form1.ToolStripStatusLabel1.Text = Checkxmllanguage("ToolStripLabel1")
        Form1.ToolStripStatusLabel2.Text = donationadress
        Form1.GroupBox1.Text = "RTM Core Wallet " + rtmCorePortableName
        Form1.GroupBox1.Text = Form1.GroupBox1.Text.Replace("raptoreum-win-", "")

        If File.Exists(selfpath + "README_" + xmlLanguagesCodes + ".md") Then
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(selfpath + "README_" + xmlLanguagesCodes + ".md")
                While Not MyReader.EndOfData
                    Try
                        Form1.RichTextBox3.Text = MyReader.ReadToEnd
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End
                    End Try
                End While
            End Using
        Else
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(selfpath + "README.md")
                While Not MyReader.EndOfData
                    Try
                        Form1.RichTextBox3.Text = MyReader.ReadToEnd
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End
                    End Try
                End While
            End Using
        End If
        Form1.logging("Modul: Languagecontrolls End")
    End Function
End Module
