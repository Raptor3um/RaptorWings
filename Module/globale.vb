'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.Globalization
Imports System.Security.Cryptography.X509Certificates

Module globale

    Public rtwVersion As String = "1-1-0"
    Public winDesktop = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
    Public selfpath As String = Application.StartupPath
    Public localfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "Local") + "\RaptorWings\"
    Public balancesummyglobal = 0
    Public rtmCoreAppDatapfad = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\RaptoreumCore\"

    Public rtmCorePortableWebPfad = "https://github.com/Raptor3um/raptoreum/releases/download/1.3.17.02/raptoreum-win-1.3.17.02.zip"
    Public rtmCorePortableDownloadName = "raptoreum-win-1.3.17.02.zip"
    Public rtmCorePortableName = "raptoreum-win-1.3.17.02"
    Public rtmCoreInstallWebPfad = "https://github.com/Raptor3um/raptoreum/releases/download/1.3.17.02/raptoreumcore-1.3.17-win64-setup.exe"
    Public rtmCoreInstallName = "raptoreumcore-1.3.17-win64-setup.exe"
    Public rtmBootstrapWebpfad = "https://bootstrap.raptoreum.com/bootstraps/bootstrap.zip"
    Public rtmBootstrapDownloadName = "bootstrap.zip"

    Public systemlanguage As String = CultureInfo.CurrentCulture.Name
    Public xmlLanguagesCodes As String = "EN"

    Public languagefile = selfpath + "\Languages\Language.xml"
    Public localwallet As String = localfolder & "main.dat"
    Public localwingsheet As String = localfolder & "sheet.dat"
    Public localdevice As String = localfolder & "device.dat"
    Public localpool As String = localfolder & "pool.dat"
    Public lovalreadmeEN As String = selfpath + "README_EN.txt"
    Public localusersetting As String = localfolder + "usersetting.dat"

    Public apiwalletbalanceurl = "https://explorer.raptoreum.com/api/getaddressbalance?address="
    Public walletexploererurl = "https://explorer.raptoreum.com/address/"

    Public gcapiurl = "https://api.coingecko.com/api/v3/coins/raptoreum?localization=false"

    Public poolurlraptorhash = "http://raptorhash.com/api/walletEx?address="
    Public poolurlraptorhash_wallet = "https://raptorhash.com/?address="

    Public poolurlflockpool = "https://flockpool.com/api/v1/wallets/rtm/"
    Public poolurlflockpool_wallet = "https://flockpool.com/miners/rtm/"

    Public poolurlraptoreumzone = "https://api.raptoreum.zone/v1/miners?method="
    Public poolurlraptoreumtone_wallet = "https://raptoreum.zone/miners/"

    Public donationadress As String = "RDuvGCXFspg9Pkkako32Sx3sbxi7whbXmb"

    Public SRBMinerDownloadpathWinows = "https://github.com/doktor83/SRBMiner-Multi/releases/download/2.2.3/SRBMiner-Multi-2-2-3-win64.zip"
    Public SRBMinerDownloadpathLinux = "https://github.com/doktor83/SRBMiner-Multi/releases/download/2.2.3/SRBMiner-Multi-2-2-3-Linux.tar.xz"
    Public SRBMinerDownloadnameWindows = "SRBMiner-Multi-2-2-3-win64.zip"
    Public SRBMinerDownloadnameLinux = "SRBMiner-Multi-2-2-3-Linux.tar.xz"
    Public SRBdirectory = "SRBMiner-Multi-2-2-3"

    Public def_ps As String = "Raptoreum.zone"
    Public def_m As String = "SRBMiner-MULTI"
    Public def_s As String = "stratum+tcps://europe.raptoreum.zone:4444"
    Public def_c As String = "Default"
    Public def_pw As String = "x"
    Public def_solo = True
End Module
