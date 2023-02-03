Imports System.Globalization
Imports System.IO

'Module for language exploration / Modul für die Erkuung der Sprache
Module Languages

    'Set System Variables / Setzte System Variablen
    Dim xmlimport As String = Nothing
    Dim xmlline() As String = Nothing

    Public Function languagesxmlload() As IEnumerable(Of String)
        'Load Languages.xml
        If My.Computer.FileSystem.FileExists(languagefile) Then
            'If FIle exsits load it
            Dim file As System.IO.StreamReader
            file = My.Computer.FileSystem.OpenTextFileReader(languagefile)
            xmlimport = file.ReadToEnd
            file.Close()

            'Splitt xmlimport to every Line
            xmlline = xmlimport.Split(System.Environment.NewLine)

            'Start Function findlanguage
            findLandguage()
        Else
            MessageBox.Show("The Languages.xml could not be found.")
            End
        End If

    End Function

    Public Function findLandguage()
        'Here it is checked whether there is a language in the Languages.xml that matches the current system language
        'Hier wird die nachgesehen, ob es in der Languages.xml eine Sprache gibt, die zu der aktuellen Systemsprache passt

        'We need a few variables to navigate the Languages.xml
        'Wir benötigen ein paar Variablen, um in der Languages.xml zu navigieren
        Dim foundLanguage As String = False
        Dim foundcountryCode As String = False
        Dim countrycode As String = Nothing

        'Basic principle: You go through the XML line by line.
        'If the Languages part is found, the variable is set to True
        'If a national language (CountryCode) is then found, this is also set to True
        'Up to the level of that the CountryCode was found

        'Grundprinzip: Es wird zeilenweise durch die XML gegangen.
        'Wenn der Part Languages gefunden wird, wird die Variable auf True gesetzt
        'Wenn dann eine Landessprache (CountryCode) gefunden wird, wird auch diese auf True gestezt
        'Bis in die Ebene des das der CountryCode gefunden wurde

        'Iterate through each line of the xmline variable
        'Durchlaufe jede Zeile der Variable xmline
        Dim countrytitle As String = Nothing

        For i As Integer = 0 To xmlline.Length - 1
            If xmlline(i).Contains("<Languages>") And foundLanguage = False Then
                foundLanguage = True
                Continue For
            End If

            'If the current line contains </Languages>, the part of the XML with the languages has been passed and it can be canceled
            'Wenn die aktuelle Zeile </Languages> enthält, ist der Teil der XML mit den Sprachen durchlaufen und es kann abgebrochen werden
            If xmlline(i).Contains("</Languages>") And foundLanguage = False Then
                Exit For 'abort the function / Abbruch der Funktion
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

                'Write the country into the ComboxBox9 on the start page for the language selection
                'Schreibe das Land in die ComboxBox9 auf der Startseite für die Sprachauswahl
                If Not Form1.ComboBox9.Items.Contains(combentry) Then
                    Form1.ComboBox9.Items.Add(combentry)
                End If

                If countrycode = systemlanguage Then
                    xmlLanguagesCodes = xmlcode
                    For i2 As Integer = 0 To Form1.ComboBox9.Items.Count - 1
                        If Form1.ComboBox9.Items(i2).ToString.Contains(xmlcode) Then
                            Form1.ComboBox9.SelectedIndex = i2
                            'Diese Funktion Ändert den Wert in der Combobox, wodurch automatisch die Funktion languagecontrolls() gestartet wird
                            Exit Function
                        End If
                    Next
                End If

                foundcountryCode = False
                countrycode = ""
            End If

        Next

        'If the listet Languages not the systemlanguage, set English as default
        Form1.ComboBox9.Text = "English - EN"
        xmlLanguagesCodes = "EN"
        languagecontrolls()
    End Function

    Public Function checkxmllanguage(ByVal bezeichnung As String)
        'Chechxmllangauages is a function that iterates through the XML content line by line.
        'The value that is being searched for is always passed to this function and returned by ByVal

        'Chechxmllangauages ist eine Funktion, die den XML Inhalt zeilenwiese durchläuft.
        'Der Wert der Gesucht wird, wird immer durch ByVal an diese Funktion übergeben und auch zurück gegeben

        Dim found As String = False 'Set Found to False / setzte Found auf False

        For i As Integer = 0 To xmlline.Length - 1
            'If xmline contains the label and found is False, fund will be set to True since the label was found first
            'Wenn xmline die Bezeichung beeinhaltet und found False ist, wird fund auf True gesetzt, da erstmal die Bezeichnung gefunden wurde
            If xmlline(i).Contains("<" & bezeichnung & ">") And found = False Then
                found = True
                Continue For
            End If

            'If the label was found with a /, it has read too far and will be aborted
            'Wenn die Bezeichung mit einem / gefunden wurde, wurde zu weit gelesen und es wird abgebrochen
            If xmlline(i).Contains("</" & bezeichnung & ">") And found = True Then
                found = False
                Continue For
            End If

            'If the line now contains the system language and forand is True, the correct value was found
            'Wenn die Zeile jetzt die Systemsprache beeinhaltet und forund True ist, wurde der richtoge Wert gefudnen
            If xmlline(i).Contains("<" & xmlLanguagesCodes & ">") And found = True Then
                Dim text As String = xmlline(i)
                'Bereiningung
                text = text.Replace("<" & xmlLanguagesCodes & ">", "")
                text = text.Replace("</" & xmlLanguagesCodes & ">", "")
                If text.Contains("+NewLine+") Then
                    text = text.Replace("+NewLine+", System.Environment.NewLine)
                End If
                checkxmllanguage = text
                found = False
                Exit For
            End If
        Next
    End Function
    Public Function languagecontrolls()
        'Here all controls, labels, ect are provided with texts
        'With checkxmllangauges, the text that is in quotes passed to the function is returned to us later.
        'The designation that is in the quotation marks must also be present in the Languages.xml

        'Hier werden alle Steuerelemente, Labels, ect mit Texten versehen
        'Mit checkxmllangauges wird der Text, der in Anführunsgstrichen steht an die Funktion übergeben uns später zurück gegeben.
        'Die Bezeichnung die in den Anführungsstrichen steht, muss auch in der Languages.xml vorhanden sein

        Form1.Button3.Text = checkxmllanguage("Button3")
        Form1.Button4.Text = checkxmllanguage("Button4")
        Form1.Button10.Text = checkxmllanguage("Button10")
        Form1.Button11.Text = checkxmllanguage("Button11")
        Form1.Label2.Text = checkxmllanguage("Label2")
        Form1.Label3.Text = checkxmllanguage("Label3")
        Form1.Label4.Text = checkxmllanguage("Label4")
        Form1.Label5.Text = checkxmllanguage("Label5")
        Form1.Label6.Text = checkxmllanguage("Label6")
        Form1.Label7.Text = checkxmllanguage("Label7")
        Form1.Label8.Text = checkxmllanguage("Label8")
        Form1.Label9.Text = checkxmllanguage("Label9")
        Form1.Label10.Text = checkxmllanguage("Label10")
        Form1.Label11.Text = checkxmllanguage("Label11")
        Form1.Label12.Text = checkxmllanguage("Label12")
        Form1.Label13.Text = checkxmllanguage("Label13")
        Form1.Label14.Text = checkxmllanguage("Label14")
        Form1.Label15.Text = checkxmllanguage("Label15")
        Form1.Label16.Text = checkxmllanguage("Label16")
        Form1.Label18.Text = checkxmllanguage("Label18")
        Form1.Label19.Text = checkxmllanguage("Label19")
        Form1.Label20.Text = checkxmllanguage("Label20")
        Form1.Label24.Text = checkxmllanguage("Label24")
        Form1.Label25.Text = checkxmllanguage("Label25")
        Form1.Label26.Text = checkxmllanguage("Label26")
        Form1.Label28.Text = checkxmllanguage("Label28")
        Form1.Label31.Text = checkxmllanguage("Label31")
        Form1.TabPage1.Text = checkxmllanguage("TabPage1")
        Form1.TabPage2.Text = checkxmllanguage("TabPage2")
        Form1.TabPage3.Text = checkxmllanguage("TabPage3")
        Form1.TabPage4.Text = checkxmllanguage("TabPage4")
        Form1.TabPage5.Text = checkxmllanguage("TabPage5")
        Form1.TabPage6.Text = checkxmllanguage("TabPage6")
        Form1.TabPage7.Text = checkxmllanguage("TabPage7")
        Form1.TabPage8.Text = checkxmllanguage("TabPage8")
        Form1.DataGridView1.Columns(1).HeaderText = checkxmllanguage("Column11")
        Form1.DataGridView1.Columns(2).HeaderText = checkxmllanguage("Column12")
        Form1.DataGridView2.Columns(1).HeaderText = checkxmllanguage("Column21")
        Form1.CheckBox1.Text = checkxmllanguage("Checkbox1")
        Form1.CheckBox2.Text = checkxmllanguage("Checkbox2")
        'Form1.RichTextBox3.Text = checkxmllanguage("TextBox3").trim
        Form1.TextBox10.Text = checkxmllanguage("TextBox10").trim
        Form1.ToolStripStatusLabel1.Text = checkxmllanguage("ToolStripLabel1")
        Form1.ToolStripStatusLabel2.Text = "REGCJ1eEiopwUFwaHVmUiXZTSPW9gfZdyH"

        'reas Readme_XXX.txt for About Page
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
    End Function
End Module
