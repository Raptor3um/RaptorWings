Imports System.Net
Imports System.Net.Http

Module readApiPool_rtm
    Public Async Function Apipoolread() As Task
        Cursor.Current = Cursors.WaitCursor
        Dim nowsummary As Double = 0
        Dim avgsummy As Double = 0

        For i As Integer = 0 To Form1.DataGridView2.Rows.Count - 1
            Dim rigname As String = Form1.DataGridView2.Item(2, i).Value.ToString
            Dim pool As String = Form1.DataGridView2.Item(9, i).Value.ToString
            Dim wallet As String = Form1.DataGridView2.Item(10, i).Value.ToString

            Dim color1 As Color
            Dim color2 As Color
            If Form1.DataGridView2.BackgroundColor = Color.DimGray Then
                color1 = Color.YellowGreen
                color2 = Color.Yellow
            Else
                color1 = Color.Green
                color2 = Color.Red
            End If

            If pool = "" Or pool = Nothing Then
                Form1.DataGridView2.Rows(i).Cells(3).Style.ForeColor = color2
                Form1.DataGridView2.Item(3, i).Value = "offline"
            End If

            Dim client As HttpClient = New HttpClient()

            If pool = "Raptorhash" Then
                Try
                    Using response As HttpResponseMessage = Await client.GetAsync(poolurlraptorhash + wallet)
                        response.EnsureSuccessStatusCode()
                        Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                        Dim now As String = "k.A."
                        Dim avg As String = "k.A."
                        If responseBody.Contains(rigname) Then
                            Form1.DataGridView2.Rows(i).Cells(3).Style.ForeColor = color1
                            Form1.DataGridView2.Item(3, i).Value = pool & ": Rig online h/s=k.A."
                            Form1.DataGridView2.Item(12, i).Value = Date.Now
                        Else
                            Form1.DataGridView2.Rows(i).Cells(3).Style.ForeColor = color2
                            Form1.DataGridView2.Item(3, i).Value = pool & ": Rig offline"
                        End If
                        Form1.DataGridView2.Item(13, i).Value = now
                        Form1.DataGridView2.Item(14, i).Value = avg
                    End Using
                Catch e As HttpRequestException
                    MessageBox.Show("Raptorhash API Request: ", e.Message)
                End Try

            End If

            If pool = "FlockPool" Then
                Try
                    Using response As HttpResponseMessage = Await client.GetAsync(poolurlflockpool + wallet)
                        response.EnsureSuccessStatusCode()
                        Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                        Dim now As String = "k.A."
                        Dim avg As String = "k.A."
                        If responseBody.Contains(rigname) Then
                            Dim responssplitt() As String = responseBody.Split(",")

                            For i2 As Integer = 0 To responssplitt.Length - 1
                                If responssplitt(i2).Contains("now") Then
                                    now = responssplitt(i2).Replace(Chr(34), "")
                                    now = now.Replace("hashrate:{now:", "")
                                    now = now.Replace(".", ",")
                                    Dim nowsplitt() As String = now.Split(",")
                                    now = nowsplitt(0)
                                End If
                                If responssplitt(i2).Contains("avg") Then
                                    avg = responssplitt(i2).Replace(Chr(34), "")
                                    avg = avg.Replace("avg:", "")
                                    avg = avg.Replace("}", "")
                                    avg = avg.Replace(".", ",")
                                    Dim avgsplitt() As String = avg.Split(",")
                                    avg = avgsplitt(0)
                                End If
                            Next

                            Form1.DataGridView2.Rows(i).Cells(3).Style.ForeColor = color1
                            Form1.DataGridView2.Item(3, i).Value = pool & ": Rig online (NOW:" & now & "H/s  / AVG:" & avg & "H/s)"
                            Form1.DataGridView2.Item(12, i).Value = Date.Now
                            If now >= 0 Then
                                nowsummary += now
                            End If
                            If avg >= 0 Then
                                avgsummy += avg
                            End If
                        Else
                            Form1.DataGridView2.Rows(i).Cells(3).Style.ForeColor = color2
                            Form1.DataGridView2.Item(3, i).Value = pool & ": Rig offline"
                        End If
                        Form1.DataGridView2.Item(13, i).Value = now
                        Form1.DataGridView2.Item(14, i).Value = avg
                    End Using
                Catch e As HttpRequestException
                    MessageBox.Show("Flockpool API Request: ", e.Message)
                End Try
            End If


            If pool = "Raptoreum.Zone" Then
                Try
                    Using response As HttpResponseMessage = Await client.GetAsync("http://www.contoso.com/")
                        response.EnsureSuccessStatusCode()
                        Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                        Dim now As String = "k.A."
                        Dim avg As String = "k.A."

                        If responseBody.Contains(rigname) Then
                            Form1.DataGridView2.Rows(i).Cells(3).Style.ForeColor = color1
                            Form1.DataGridView2.Item(3, i).Value = pool & ": Rig online h/s=k.A."
                            Form1.DataGridView2.Item(12, i).Value = Date.Now
                        Else
                            Form1.DataGridView2.Rows(i).Cells(3).Style.ForeColor = color2
                            Form1.DataGridView2.Item(3, i).Value = pool & ": Rig offline"
                        End If
                        Form1.DataGridView2.Item(13, i).Value = now
                        Form1.DataGridView2.Item(14, i).Value = avg
                    End Using
                Catch e As HttpRequestException
                    MessageBox.Show("Raptoreum.Zone API Request: ", e.Message)
                End Try
            End If

        Next

        Form1.TextBox12.Text = "Refresh 30Sek.   Total: " & nowsummary & "H/s / " & avgsummy & " H/s"
        Cursor.Current = Cursors.Default
    End Function
End Module
