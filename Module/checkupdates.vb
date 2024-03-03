'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.Net.Http
Imports System.Net
Imports Windows.Media.Protection.PlayReady
Imports System.IO

Module checkupdates
    ReadOnly client As HttpClient = New HttpClient()
    Public Async Sub checkRTWupdate()
        Form1.logging("Modul: CheckRTWupdate: Start")
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
            MessageBox.Show("Issues with Github API for Raptor3um\Raptorwings" + System.Environment.NewLine + System.Environment.NewLine + e.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Form1.logging("Modul: CheckRTWupdate: Issues with Github API for Raptor3um\Raptorwings")
            Form1.logging(e.Message)
        End Try
        Form1.logging("Modul: CheckRTWupdate: End")
    End Sub

    Public Async Sub checkRTMupdate()
        Form1.logging("Modul: CheckRTMUodate: Start")
        Dim githubapi = "https://api.github.com/repos/raptor3um/raptoreum/releases/latest"
        client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
        Try
            Using response As HttpResponseMessage = Await client.GetAsync(githubapi)
                response.EnsureSuccessStatusCode()
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                Dim responseBodysplitt() As String = responseBody.Split(",")

                For i As Integer = 0 To responseBodysplitt.Length - 2
                    If responseBodysplitt(i).Contains("browser_download_url") And
                    responseBodysplitt(i).Contains("raptoreum-win-") And
                    responseBodysplitt(i).Contains(".zip") Then
                        rtmCorePortableWebPfad = responseBodysplitt(i).Replace("browser_download_url", "")
                        rtmCorePortableWebPfad = rtmCorePortableWebPfad.replace("""", "")
                        rtmCorePortableWebPfad = rtmCorePortableWebPfad.replace("}", "")
                        rtmCorePortableWebPfad = rtmCorePortableWebPfad.Substring(1)

                        Dim splittpath() As String = rtmCorePortableWebPfad.split("/")
                        For i2 As Integer = 0 To splittpath.Length - 1
                            If splittpath(i2).Contains("raptoreum-win-") Then
                                rtmCorePortableDownloadName = splittpath(i2)
                                rtmCorePortableName = rtmCorePortableDownloadName.replace(".zip", "")
                            End If
                        Next
                    End If

                    If responseBodysplitt(i).Contains("browser_download_url") And
                    responseBodysplitt(i).Contains("raptoreumcore-") And
                    responseBodysplitt(i).Contains(".exe") Then
                        rtmCoreInstallWebPfad = responseBodysplitt(i).Replace("browser_download_url", "")
                        rtmCoreInstallWebPfad = rtmCoreInstallWebPfad.replace("""", "")
                        rtmCoreInstallWebPfad = rtmCoreInstallWebPfad.replace("}", "")
                        rtmCoreInstallWebPfad = rtmCoreInstallWebPfad.replace("]", "")
                        rtmCoreInstallWebPfad = rtmCoreInstallWebPfad.Substring(1)

                        Dim splittpath() As String = rtmCoreInstallWebPfad.split("/")
                        For i2 As Integer = 0 To splittpath.Length - 1
                            If splittpath(i2).Contains("raptoreumcore-") Then
                                rtmCoreInstallName = splittpath(i2)
                            End If
                        Next
                    End If
                Next
            End Using
        Catch e As HttpRequestException
            MessageBox.Show("Issues with Github API for Raptor3um\Raptoreum" + System.Environment.NewLine + System.Environment.NewLine + e.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        Form1.logging("Modul: CheckSRTMupdate: End")
    End Sub

    Public Async Function CheckPoolUpdates() As Task
        Form1.logging("Modul: CheckPoolUpdate: Start")

        Dim downloadFile As String = "https://raw.githubusercontent.com/Raptor3um/RaptorWings/main/Config/pools.dat"

        Try
            Using httpClient As New HttpClient()
                Dim response As HttpResponseMessage = Await httpClient.GetAsync(downloadFile)

                If response.IsSuccessStatusCode Then
                    Dim content As Byte() = Await response.Content.ReadAsByteArrayAsync()
                    Await File.WriteAllBytesAsync(pooldatafile, content)
                Else
                    Throw New Exception($"Failed to download pool list. Status code: {response.StatusCode}")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error trying to download the pool list from GitHub:{Environment.NewLine}{Environment.NewLine}{ex.Message}{Environment.NewLine}{Environment.NewLine}Raptorwings uses the pool list stored on the device", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            My.Computer.FileSystem.CopyFile(selfpath + "Config\pools.backup", pooldatafile, overwrite:=True)
        End Try

        Form1.logging("Modul: CheckPoolUpdate: End")
    End Function

    Public Async Sub checkXMRIG()
        Form1.logging("Modul: checkXMRIG: Start")
        Dim githubapi = "https://api.github.com/repos/xmrig/xmrig/releases/latest"
        client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
        Try
            Using response As HttpResponseMessage = Await client.GetAsync(githubapi)
                response.EnsureSuccessStatusCode()
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                Dim responseBodysplitt() As String = responseBody.Split(",")

                For i As Integer = 0 To responseBodysplitt.Length - 2
                    If responseBodysplitt(i).Contains("browser_download_url") And responseBodysplitt(i).Contains("-gcc-win64.zip") Then
                        XMRIG_MINER_DOWNLOAD_PATH = responseBodysplitt(i).Replace("browser_download_url", "")
                        XMRIG_MINER_DOWNLOAD_PATH = XMRIG_MINER_DOWNLOAD_PATH.Replace("""", "")
                        XMRIG_MINER_DOWNLOAD_PATH = XMRIG_MINER_DOWNLOAD_PATH.Replace("}", "")
                        XMRIG_MINER_DOWNLOAD_PATH = XMRIG_MINER_DOWNLOAD_PATH.Substring(1)
                        Dim splittpath() As String = XMRIG_MINER_DOWNLOAD_PATH.Split("/")
                        For i2 As Integer = 0 To splittpath.Length - 1
                            If splittpath(i2).Contains("-gcc-win64.zip") Then
                                XMRIG_MINER_DOWNLOAD_DATENAME = splittpath(i2)
                                XMRIG_MINER_DIRECTORYNAME = XMRIG_MINER_DOWNLOAD_DATENAME.Replace("-gcc-win64.zip", "")
                            End If
                        Next
                        Exit For
                    End If
                Next

            End Using
        Catch e As HttpRequestException
            MessageBox.Show("Issues with Github API for XMRIG" + System.Environment.NewLine + System.Environment.NewLine + e.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Form1.logging("Modul: checkXMRIG: Issues with Github API for XMRIG")
            Form1.logging(e.Message)
        End Try
        Form1.logging("Modul: checkXMRIG: End")
    End Sub
End Module
