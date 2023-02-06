Imports System.Net
Imports System.Net.Http
Imports Windows.Media.Protection.PlayReady

Module readprice_rtm

    Public Async Sub Readprice()
        'Function to read the RTM Price / Funktion zum einlesen des RTM Preises
        'Read from Congecko API / Eingelesne über die Coingecko API
        'This function is mainly started by Timer1 / Diese Funktion wird hauptsächlich durch den Timer1 gestartet
        Cursor.Current = Cursors.WaitCursor

        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 'Security protocol for downloading API data / Security Protokoll für das Downloadne der API Daten
        'Dim webClient As New System.Net.WebClient
        'Dim result As String = webClient.DownloadString(gcapiurl) 'Download API DATA / Download der API Daten

        Dim client As HttpClient = New HttpClient()

        Try
            Using response As HttpResponseMessage = Await client.GetAsync(gcapiurl)
                response.EnsureSuccessStatusCode()
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                'Splitt the API Data / Zerteilen der API Daten
                Dim resultsplitt() As String = responseBody.Split(",") 'Split after every comma / Splitte nach jedme Komma

                'Create variables for the price units as well as for the confirmation of the entry / Erstelle variablen für die Preiseinheiten sowie für die Bestätigung der Erfassug
                Dim btc As String = Nothing
                Dim btccheck = False
                Dim euro As String = Nothing
                Dim eurocheck = False
                Dim usd As String = Nothing
                Dim usdcheck = False

                For i As Integer = 0 To resultsplitt.Length - 1 'Left every entry of the splittet dataset / Ließ jeden Eintrag des zerlegten Datensatzes
                    If btccheck = False And resultsplitt(i).Contains("btc") Then 'If Splitt Datatset Contains btc then ... / Wenn der Datensatz btc enthält
                        btc = resultsplitt(i).Replace("""", "") 'Remove quotes / entferne Anfürhungsstriche 
                        btc = btc.Replace("btc", "") 'Remove Remove the word / entferne das Wort
                        btc = btc.Replace(":", "") 'Remove colons / Entferne Doppelpunkte
                        btc = btc.Replace(".", ",") 'Replace periods with commas / Ersteze unkte durch Komma
                        btc = btc.Trim 'Replace space / Entferne Leerzeichen
                        btccheck = True
                    End If
                    If eurocheck = False And resultsplitt(i).Contains("eur") Then
                        euro = resultsplitt(i).Replace("""", "") 'Remove quotes / entferne Anfürhungsstriche
                        euro = euro.Replace("eur", "") 'Remove Remove the word / entferne das Wort
                        euro = euro.Replace(":", "") 'Remove colons / Entferne Doppelpunkte
                        euro = euro.Replace(".", ",") 'Replace periods with commas / Ersteze unkte durch Komma
                        euro = euro.Trim 'Replace space / Entferne Leerzeichen
                        eurocheck = True
                    End If
                    If usdcheck = False And resultsplitt(i).Contains("usd") Then
                        usd = resultsplitt(i).Replace("""", "") 'Remove quotes / entferne Anfürhungsstriche
                        usd = usd.Replace("usd", "") 'Remove Remove the word / entferne das Wort
                        usd = usd.Replace(":", "") 'Remove colons / Entferne Doppelpunkte
                        usd = usd.Replace(".", ",") 'Replace periods with commas / Ersteze unkte durch Komma
                        usd = usd.Trim 'Replace space / Entferne Leerzeichen
                        usdcheck = True
                    End If

                    'If all check are true, then it can be aborted / Wenn alle check True sind, dann kann abgebrochen werden
                    If btccheck = True And eurocheck = True And usdcheck = True Then
                        Exit For
                    End If

                Next

                'Search each row of DGV1 and calculate its value / Durchsuche jede Zeile des DGV1 und berechne den Wert
                For i As Integer = 0 To Form1.DataGridView1.Rows.Count - 1
                    If Form1.DataGridView1.Item(3, i).Value.ToString = "" Or Form1.DataGridView1.Item(3, i).Value.ToString = Nothing Then
                        Continue For
                    End If
                    If Form1.DataGridView1.Item(3, i).Value.ToString > 0 Then
                        Form1.DataGridView1.Item(4, i).Value = Format(CDbl(Form1.DataGridView1.Item(3, i).Value.ToString * btc), "##,##0.00000000")
                        Form1.DataGridView1.Item(5, i).Value = Format(CDbl(Form1.DataGridView1.Item(3, i).Value.ToString * usd), "##,##0.00000")
                        Form1.DataGridView1.Item(6, i).Value = Format(CDbl(Form1.DataGridView1.Item(3, i).Value.ToString * euro), "##,##0.00000")
                    End If
                Next

                'Calculate the total value and write it down on the main page under the number of coins / Berechne gesamtwert und schreibe diesen auf der Hauptseite unter die Anzhal der Coins
                btc = CDbl(btc * balancesummyglobal)
                usd = CDbl(usd * balancesummyglobal)
                euro = CDbl(euro * balancesummyglobal)
                Form1.Label17.Text = Format(CDbl(btc), "##,##0.0000000 BTC") & " / " & Format(CDbl(usd), "##,##0.0000 $") & " / " & Format(CDbl(euro), "##,##0.0000 €")

                Form1.ToolStripStatusLabel4.Text = Format(balancesummyglobal, "##,##0.00") + " RTM / " & Form1.Label17.Text 'enter balance at the end of DGV1 (wallet). / balance am ende des DGV1 (Wallet) eintragen

                Cursor.Current = Cursors.Default
            End Using
        Catch e As HttpRequestException
            MessageBox.Show("Message :{0} ", e.Message)
        End Try


    End Sub

End Module
