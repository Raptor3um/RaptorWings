Imports System.Net

Module readbalance_rtm
    Public Function Readbalance()
        'Function Readbalance 
        'To read the wallet balance from the RTM Explorer API / Zum auslesen der Walletbalance aus der RTM Explorer API
        'This function is mainly started by Timer1 / Diese Funktion wird hauptsächlich durch den Timer1 gestartet
        Cursor.Current = Cursors.WaitCursor


        Dim balancesummary As Integer = 0 'Set variable to 0 to start over (Not Global Variable) / Globale Variable auf 0 setzen um neu zu beginnen (Nicht die Globale Variable)

        Form1.Timer1.Stop() 'Stop Timer 1 (Wallet Overwiev Refresh)

        'Create a variable that is needed to later determine whether the transactions should also be read after the balance has been read
        'Erstelle Variable, die benötigt wird um später fest zu legen, ob zusätzlich nach dem einlesne der Balance auch die ransaktionen gelesen werden sollen

        'Start For to read all Lines of DGV1 / Starte jede Zeile des DGV1 einzeln ein zu lesen
        For i As Integer = 0 To Form1.DataGridView1.Rows.Count - 1
            If Form1.DataGridView1.Item(1, i).Value.ToString = "" Or Form1.DataGridView1.Item(1, i).Value.ToString = Nothing Then
                'If there is nothing in DGV1 column 1 (address), continue with the next line / Wenn im DGV1 Spalte1 (Adresse) nichts steht, mit der nächsten Zeile weiter machen
                Continue For
            End If

            Dim walletadress As String = Form1.DataGridView1.Item(1, i).Value.ToString 'address of the current line / Adresse der aktuellen Zeile

            Dim client As New WebClient
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
            Dim result As String = client.DownloadString(apiwalletbalanceurl + walletadress)

            If Not result = Nothing Then
                'Splitt API Data / Splitte API Daten
                Dim responsesplitt() As String = result.Split("{") 'Splitt every { / splitte nach dem {
                Dim responsesplitt2() As String = responsesplitt(2).Split(",") 'Splitt every , / Splitte nach jedem Komma
                Dim balance() As String = responsesplitt2(0).Split(":") 'Splitt every : / Splitte nach jedem :
                Dim walletbalance As String = balance(1).Trim 'Delete Space / Lösche Leerzeichen
                walletbalance = walletbalance.Replace("""", "") 'Delete Quotes / Lösche Anführungszeichen
                walletbalance = CDbl(walletbalance) / 100000000 'recalculate the imported balance / eingelesene balance neu berechnen

                'Write Balance in DGV1 / Schreibe balance in DGV1
                Form1.DataGridView1.Item(3, i).Value = Format(CDbl(walletbalance), "##,##0.00000000")

                balancesummary = balancesummary + walletbalance 'Add the newly read value to the total / Rechne den neu eingelesen Wert zur Gesamtsumme dazu

            End If

        Next 'Next Line in DGV / Nächste Zeile des DGV

        balancesummyglobal = balancesummary 'Globale balance = balance Summary / Die Globale ballance entspricht der Balancesummary
        Form1.Label1.Text = Format(balancesummary, "##,##0.00") & " RTM" 'Enter balance on Overwiev page / balance auf Overwiev Seite eintragen

        Form1.Timer1.Start()
        Cursor.Current = Cursors.Default

        Readprice() 'Start Function Readprice / Starte Funktion readprice
    End Function
End Module