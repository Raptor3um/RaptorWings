'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports Windows.Media.Protection.PlayReady

Module readprice_rtm

    Public Async Sub Readprice()
        Cursor.Current = Cursors.WaitCursor

        Dim client As HttpClient = New HttpClient()

        Try
            Using response As HttpResponseMessage = Await client.GetAsync(gcapiurl)
                response.EnsureSuccessStatusCode()
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                Dim resultsplitt() As String = responseBody.Split(",")

                Dim btc As String = Nothing
                Dim btccheck = False
                Dim euro As String = Nothing
                Dim eurocheck = False
                Dim usd As String = Nothing
                Dim usdcheck = False

                For i As Integer = 0 To resultsplitt.Length - 1
                    If btccheck = False And resultsplitt(i).Contains("btc") Then
                        btc = resultsplitt(i).Replace("""", "")
                        btc = btc.Replace("btc", "")
                        btc = btc.Replace(":", "")
                        btc = btc.Trim
                        btccheck = True
                    End If
                    If eurocheck = False And resultsplitt(i).Contains("eur") Then
                        euro = resultsplitt(i).Replace("""", "")
                        euro = euro.Replace("eur", "")
                        euro = euro.Replace(":", "")
                        euro = euro.Trim
                        eurocheck = True
                    End If
                    If usdcheck = False And resultsplitt(i).Contains("usd") Then
                        usd = resultsplitt(i).Replace("""", "")
                        usd = usd.Replace("usd", "")
                        usd = usd.Replace(":", "")
                        usd = usd.Trim
                        usdcheck = True
                    End If

                    If btccheck = True And eurocheck = True And usdcheck = True Then
                        Exit For
                    End If

                Next

                For i As Integer = 0 To Form1.DataGridView1.Rows.Count - 1
                    If Form1.DataGridView1.Item(3, i).Value.ToString = "" Or Form1.DataGridView1.Item(3, i).Value.ToString = Nothing Then
                        Continue For
                    End If
                    If Form1.DataGridView1.Item(3, i).Value.ToString > 0 Then
                        Dim balance As Double = Convert.ToDouble(Form1.DataGridView1.Item(3, i).Value.ToString, CultureInfo.CurrentCulture)
                        Form1.DataGridView1.Item(4, i).Value = balance * Convert.ToDouble(btc, CultureInfo.InvariantCulture)
                        Form1.DataGridView1.Item(4, i).Value = Format(Form1.DataGridView1.Item(4, i).Value, "##,##0.00000000")
                        Form1.DataGridView1.Item(5, i).Value = balance * Convert.ToDouble(usd, CultureInfo.InvariantCulture)
                        Form1.DataGridView1.Item(5, i).Value = Format(Form1.DataGridView1.Item(5, i).Value, "##,##0.000")
                        Form1.DataGridView1.Item(6, i).Value = balance * Convert.ToDouble(euro, CultureInfo.InvariantCulture)
                        Form1.DataGridView1.Item(6, i).Value = Format(Form1.DataGridView1.Item(6, i).Value, "##,##0.000")
                        Form1.DataGridView1.Item(3, i).Value = Format(balance, "##,##0.00000000")
                    End If
                Next

                Dim btcglobal As Double = balancesummyglobal * Convert.ToDouble(btc, CultureInfo.InvariantCulture)
                Dim usdglobal As Double = balancesummyglobal * Convert.ToDouble(usd, CultureInfo.InvariantCulture)
                Dim euroglobal As Double = balancesummyglobal * Convert.ToDouble(euro, CultureInfo.InvariantCulture)
                Form1.Label17.Text = Format(btcglobal, "##,##0.00000000") & " BTC / " & Format(usdglobal, "##,##0.000") & " $ / " & Format(euroglobal, "##,##0.000") & " €"

                Form1.ToolStripStatusLabel4.Text = Format(balancesummyglobal, "##,##0.00000000") + " RTM / " & Form1.Label17.Text

                Cursor.Current = Cursors.Default
            End Using
        Catch e As HttpRequestException
            MessageBox.Show("Error CoinGecko-API:" + System.Environment.NewLine + e.Message, "Error")
        End Try

        Form1.Label1.Text = Format(CDbl(balancesummyglobal), "##,##0.0000000") + " RTM"
    End Sub

End Module
