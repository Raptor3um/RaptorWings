Imports System.Net

Module readbalance_rtm
    Public Function Readbalance()
        Cursor.Current = Cursors.WaitCursor


        Dim balancesummary As Integer = 0

        Form1.Timer1.Stop()

        For i As Integer = 0 To Form1.DataGridView1.Rows.Count - 1
            If Form1.DataGridView1.Item(1, i).Value.ToString = "" Or Form1.DataGridView1.Item(1, i).Value.ToString = Nothing Then
                Continue For
            End If

            Dim walletadress As String = Form1.DataGridView1.Item(1, i).Value.ToString

            Dim client As New WebClient
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
            Dim result As String = client.DownloadString(apiwalletbalanceurl + walletadress)

            If Not result = Nothing Then
                Dim responsesplitt() As String = result.Split("{")
                Dim responsesplitt2() As String = responsesplitt(2).Split(",")
                Dim balance() As String = responsesplitt2(0).Split(":")
                Dim walletbalance As String = balance(1).Trim
                walletbalance = walletbalance.Replace("""", "")
                walletbalance = CDbl(walletbalance) / 100000000

                Form1.DataGridView1.Item(3, i).Value = Format(CDbl(walletbalance), "##,##0.00000000")

                balancesummary = balancesummary + walletbalance

            End If

        Next

        balancesummyglobal = balancesummary
        Form1.Label1.Text = Format(balancesummary, "##,##0.00") & " RTM"

        Form1.Timer1.Start()
        Cursor.Current = Cursors.Default

        Readprice()
    End Function
End Module