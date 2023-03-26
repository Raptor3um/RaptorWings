'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.Text

Module saveWalletList
    Public Function saveWalletList()
        Dim walletdat As String = Nothing
        If Form1.ComboBox11.Text = "1 - Default" Then
            walletdat = localwallet
        Else
            Dim textsplitt() As String = Form1.ComboBox11.Text.Split(" ")
            walletdat = localfolder + "main" + textsplitt(0) + ".dat"
        End If

        If Form1.DataGridView1.Rows.Count - 1 = -1 Then
            MessageBox.Show(Checkxmllanguage("Message2.1").trim, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Function
        End If

        Dim sb = New StringBuilder

        For i As Integer = 0 To Form1.DataGridView1.Rows.Count - 1
            If Form1.DataGridView1.Item(0, i).Value.ToString = Nothing Then
                Continue For
            End If

            Dim number As String = Chr(34) + Form1.DataGridView1.Item(0, i).Value.ToString + Chr(34)
            Dim walletadress As String = Chr(34) + Form1.DataGridView1.Item(1, i).Value.ToString + Chr(34)
            Dim description As String = Chr(34) + Form1.DataGridView1.Item(2, i).Value.ToString + Chr(34)

            sb.AppendLine($"{number},{walletadress},{description}")
        Next

        System.IO.File.WriteAllText(walletdat, sb.ToString)

        MessageBox.Show((Checkxmllanguage("Message3.1").trim), "Note", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Form1.Button2.Enabled = False
    End Function
End Module
