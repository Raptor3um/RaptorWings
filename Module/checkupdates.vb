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
            MessageBox.Show(e.Message)
        End Try
        Form1.logging("Modul: CheckRTWupdate: End")
    End Sub

    Public Async Sub checkSRBupdate()
        Form1.logging("Modul: CheckSRBUpdate: Start")
        Dim githubapi = "https://api.github.com/repos/doktor83/SRBMiner-Multi/releases/latest"
        client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)")
        Try
            Using response As HttpResponseMessage = Await client.GetAsync(githubapi)
                response.EnsureSuccessStatusCode()
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                Dim responseBodysplitt() As String = responseBody.Split(",")

                For i As Integer = 0 To responseBodysplitt.Length - 2
                    If responseBodysplitt(i).Contains("browser_download_url") And responseBodysplitt(i).Contains("-win64.zip") Then
                        SRBMinerDownloadpathWinows = responseBodysplitt(i).Replace("browser_download_url", "")
                        SRBMinerDownloadpathWinows = SRBMinerDownloadpathWinows.replace("""", "")
                        SRBMinerDownloadpathWinows = SRBMinerDownloadpathWinows.replace("}", "")
                        SRBMinerDownloadpathWinows = SRBMinerDownloadpathWinows.Substring(1)
                        Dim splittpath() As String = SRBMinerDownloadpathWinows.split("/")
                        For i2 As Integer = 0 To splittpath.Length - 1
                            If splittpath(i2).Contains("-win64.zip") Then
                                SRBMinerDownloadnameWindows = splittpath(i2)
                                SRBdirectory = SRBMinerDownloadnameWindows.replace("-win64.zip", "")
                            End If
                        Next
                    End If
                    If responseBodysplitt(i).Contains("browser_download_url") And responseBodysplitt(i).Contains("-Linux.tar.xz") Then
                        SRBMinerDownloadpathLinux = responseBodysplitt(i).Replace("browser_download_url", "")
                        SRBMinerDownloadpathLinux = SRBMinerDownloadpathLinux.replace("""", "")
                        SRBMinerDownloadpathLinux = SRBMinerDownloadpathLinux.replace("}", "")
                        SRBMinerDownloadpathLinux = SRBMinerDownloadpathLinux.Substring(1)
                        Dim splittpath() As String = SRBMinerDownloadpathLinux.split("/")
                        For i2 As Integer = 0 To splittpath.Length - 1
                            If splittpath(i2).Contains("-Linux.tar.xz") Then
                                SRBMinerDownloadnameLinux = splittpath(i2)
                            End If
                        Next
                    End If
                Next

            End Using
        Catch e As HttpRequestException
            MessageBox.Show("Issues with Github API for doktor83/SRBMiner-Multi" + System.Environment.NewLine + System.Environment.NewLine + e.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        Form1.logging("Modul: CheckSRBUpdate: End")
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

    Public Async Sub checkPoolupdates()
        Form1.logging("Modul: CheckPoolpdate: Start")
        Dim downloadfile As String = "https://raw.githubusercontent.com/Raptor3um/RaptorWings/main/Config/pools.dat"
        Try
            Dim web_client As WebClient = New WebClient

            web_client.DownloadFile(downloadfile, pooldatafile)

        Catch ex As Exception
            MessageBox.Show("Error trying to download the pool list from Github:" + System.Environment.NewLine + System.Environment.NewLine + ex.Message + System.Environment.NewLine + System.Environment.NewLine + "Raptorwings uses the pool list stored on the device", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            My.Computer.FileSystem.CopyFile(selfpath + "Config\pools.backup", pooldatafile)
        End Try
        Form1.logging("Modul: CheckPoolpdate: End")
    End Sub

End Module
