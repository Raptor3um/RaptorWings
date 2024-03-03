'Copyright(c) 2023 The Raptoreum developers
'Copyright(c) 2023 Germardies

Imports System.Globalization
Imports System.Security.Cryptography.X509Certificates

Module globale

    Public rtwVersion As String = "1-3-0-0"
    Public winDesktop = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
    Public selfpath As String = Application.StartupPath
    Public localfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "Local") + "\RaptorWings\"
    Public balancesummyglobal = 0
    Public rtmCoreAppDatapfad = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\RaptoreumCore\"

    Public rtmCorePortableWebPfad = ""
    Public rtmCorePortableDownloadName = ""
    Public rtmCorePortableName = ""
    Public rtmCoreInstallWebPfad = ""
    Public rtmCoreInstallName = ""
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
    Public pooldatafile As String = selfpath + "Config\pools.dat"

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

    Public XMRIG_MINER_DOWNLOAD_PATH As String = "https://github.com/xmrig/xmrig/releases/download/v6.21.1/xmrig-6.21.1-gcc-win64.zip"
    Public XMRIG_MINER_DOWNLOAD_DATENAME As String = "xmrig-6.21.1-gcc-win64.zip"
    Public XMRIG_MINER_DIRECTORYNAME = "XMRIG-6.21.1"

    Public def_ps As String = "Raptorhash"
    Public def_m As String = "XMRIG"
    Public def_s As String = "stratum+tcps://eu.raptorhash.net:6900"
    Public def_c As String = "Default"
    Public def_pw As String = "x"
    Public def_solo = True
End Module
