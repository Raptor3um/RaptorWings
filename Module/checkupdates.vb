'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.Net.Http
Imports System.Net
Imports Windows.Media.Protection.PlayReady

Module checkupdates
    ReadOnly client As HttpClient = New HttpClient()
    Public Async Sub checkRTWupdate()
        Dim githubapi = "https://api.github.com/repos/Raptor3um/RaptorWings/releases/latest"
        client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
        Try
        Using response As HttpResponseMessage = Await client.GetAsync(githubapi)
                response.EnsureSuccessStatusCode()
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                Dim responseBodysplitt() As String = responseBody.Split(",")

                For i As Integer = 0 To responseBodysplitt.Length - 2
                    If responseBodysplitt(i).Contains("name") And responseBodysplitt(i).Contains(".zip") Then
                        If Not responseBodysplitt(i).Contains(rtwVersion) Then
                            Dim result = MessageBox.Show("A new version of Raptorwings is available." + System.Environment.NewLine + System.Environment.NewLine + "Do you want Github to open in your browser for download?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If result = DialogResult.Yes Then
                                Dim ProcessStartInfo = New ProcessStartInfo With {.FileName = "https://github.com/Raptor3um/RaptorWings/releases/latest/", .UseShellExecute = True}
                                Process.Start(ProcessStartInfo)
                            End If
                            responseBody = Nothing
                            Exit For
                        End If
                    End If
                Next
            End Using
        Catch e As HttpRequestException
            MessageBox.Show(e.Message)
        End Try

    End Sub
End Module
