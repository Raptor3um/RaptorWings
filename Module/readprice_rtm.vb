'This software is written for the RTM community. It is part of the Raptoreum program and was developed by
'Germardies (https://github.com/Germardies).
'It should be freely available To everyone.

'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

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
                        btc = btc.Replace(".", ",")
                        btc = btc.Trim
                        btccheck = True
                    End If
                    If eurocheck = False And resultsplitt(i).Contains("eur") Then
                        euro = resultsplitt(i).Replace("""", "")
                        euro = euro.Replace("eur", "")
                        euro = euro.Replace(":", "")
                        euro = euro.Replace(".", ",")
                        euro = euro.Trim
                        eurocheck = True
                    End If
                    If usdcheck = False And resultsplitt(i).Contains("usd") Then
                        usd = resultsplitt(i).Replace("""", "")
                        usd = usd.Replace("usd", "")
                        usd = usd.Replace(":", "")
                        usd = usd.Replace(".", ",")
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
                        Form1.DataGridView1.Item(4, i).Value = Format(CDbl(Form1.DataGridView1.Item(3, i).Value.ToString * btc), "##,##0.00000000")
                        Form1.DataGridView1.Item(5, i).Value = Format(CDbl(Form1.DataGridView1.Item(3, i).Value.ToString * usd), "##,##0.00000")
                        Form1.DataGridView1.Item(6, i).Value = Format(CDbl(Form1.DataGridView1.Item(3, i).Value.ToString * euro), "##,##0.00000")
                    End If
                Next

                btc = CDbl(btc * balancesummyglobal)
                usd = CDbl(usd * balancesummyglobal)
                euro = CDbl(euro * balancesummyglobal)
                Form1.Label17.Text = Format(CDbl(btc), "##,##0.0000000 BTC") & " / " & Format(CDbl(usd), "##,##0.0000 $") & " / " & Format(CDbl(euro), "##,##0.0000 €")

                Form1.ToolStripStatusLabel4.Text = Format(balancesummyglobal, "##,##0.00") + " RTM / " & Form1.Label17.Text

                Cursor.Current = Cursors.Default
            End Using
        Catch e As HttpRequestException
            MessageBox.Show("Message :{0} ", e.Message)
        End Try


    End Sub

End Module
