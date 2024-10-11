Imports System.Globalization
Imports System.Net
Imports Newtonsoft.Json.Linq 'Füge Newtonsoft.Json über NuGet hinzu

Module readbalance_rtm
    Public Function Readbalance()
        Form1.logging("Moul: Readbalance: Start")
        Cursor.Current = Cursors.WaitCursor

        Dim balancesummary As Double = 0
        Form1.Timer1.Stop()

        For i As Integer = 0 To Form1.DataGridView1.Rows.Count - 1
            If String.IsNullOrEmpty(Form1.DataGridView1.Item(1, i).Value.ToString()) Then
                Continue For
            End If

            Dim walletadress As String = Form1.DataGridView1.Item(1, i).Value.ToString
            Dim result As String = ""

            Try
                Dim client As New WebClient
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
                result = client.DownloadString(apiwalletbalanceurl + walletadress)
                Form1.logging("Moul: Readbalance: Read Walletbalance from" + apiwalletbalanceurl + walletadress)
            Catch ex As Exception
                Form1.logging("Moul: Readbalance: An error occurred connecting to the Raptoreum Explorer:" & Environment.NewLine & ex.Message)
                MessageBox.Show("An error occurred connecting to the Raptoreum Explorer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Continue For
            End Try

            If Not String.IsNullOrEmpty(result) Then
                Try
                    ' Parse das JSON-Ergebnis
                    Dim json As JObject = JObject.Parse(result)
                    Dim balanceValue As String = json(walletadress)("RTM")("balance").ToString()

                    ' Konvertiere das Balance-Wert in Double
                    Dim walletbalance As Double = CDbl(balanceValue) / 100000000

                    Form1.DataGridView1.Item(3, i).Value = walletbalance
                    balancesummary += walletbalance
                Catch ex As Exception
                    Form1.logging("Moul: Readbalance: Error parsing balance from result: " & ex.Message)
                    Form1.DataGridView1.Item(3, i).Value = 0
                End Try
            Else
                Form1.DataGridView1.Item(3, i).Value = 0
            End If
        Next

        balancesummyglobal = balancesummary
        Form1.Timer1.Start()
        Cursor.Current = Cursors.Default
        Readprice()
        Form1.logging("Moul: Readbalance: End")
    End Function
End Module
