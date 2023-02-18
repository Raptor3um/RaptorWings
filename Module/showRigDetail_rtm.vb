Module showRigDetail_rtm
    Public Sub Showrigdetail()
        If Form1.DataGridView2.Rows.Count - 1 = -1 Then
            Exit Sub
        End If

        Dim i As Integer = Form1.DataGridView2.CurrentCell.RowIndex.ToString
        Dim selectrig As String = Form1.DataGridView2.Item(2, i).Value
        Dim selectpool As String = Form1.DataGridView2.Item(9, i).Value
        Dim selectwallet As String = Form1.DataGridView2.Item(10, i).Value
        Dim selectwingsheet As String = Form1.DataGridView2.Item(11, i).Value
        Dim selectlastseen As String = Form1.DataGridView2.Item(12, i).Value
        Dim selectnow As String = Form1.DataGridView2.Item(13, i).Value
        Dim selectavg As String = Form1.DataGridView2.Item(14, i).Value
        Dim url As String = Nothing
        If selectpool = "FlockPool" Then
            url = poolurlflockpool_wallet & selectwallet
        End If
        If selectpool = "Raptoreum.Zone" Then
            url = poolurlraptoreumtone_wallet & selectwallet
        End If
        If selectpool = "Raptorhash" Then
            url = poolurlraptorhash_wallet & selectwallet
        End If

        Form1.RichTextBox2.Text = "Rigname: " & selectrig & System.Environment.NewLine &
                             "Pool: " & selectpool & System.Environment.NewLine &
                             "Wallet: " & selectwallet & System.Environment.NewLine &
                             "Wingsheet: " & selectwingsheet & System.Environment.NewLine &
                             "Last seen: " & selectlastseen & System.Environment.NewLine &
                             "Hashrate NOW: " & selectnow & System.Environment.NewLine &
                             "Hashrate AVG: " & selectavg & System.Environment.NewLine &
                             "Weblink: " & url

    End Sub
End Module
