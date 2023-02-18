Imports System.Globalization

Module globale

    Public selfpath As String = Application.StartupPath
    Public localfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "Local") + "\RaptorWings\"
    Public balancesummyglobal = 0

    Public systemlanguage As String = CultureInfo.CurrentCulture.Name
    Public xmlLanguagesCodes As String = "EN"

    Public languagefile = selfpath + "\Languages\Language.xml"
    Public localwallet As String = localfolder & "main.dat"
    Public localwingsheet As String = localfolder & "sheet.dat"
    Public localdevice As String = localfolder & "device.dat"
    Public localpool As String = localfolder & "pool.dat"
    Public lovalreadmeEN As String = selfpath + "README_EN.txt"

    Public apiwalletbalanceurl = "https://explorer.raptoreum.com/api/getaddressbalance?address="
    Public walletexploererurl = "https://explorer.raptoreum.com/address/"

    Public gcapiurl = "https://api.coingecko.com/api/v3/coins/raptoreum?localization=false"

    Public poolurlraptorhash = "http://raptorhash.com/api/walletEx?address="
    Public poolurlraptorhash_wallet = "https://raptorhash.com/?address="

    Public poolurlflockpool = "https://flockpool.com/api/v1/wallets/rtm/"
    Public poolurlflockpool_wallet = "https://flockpool.com/miners/rtm/"

    Public poolurlraptoreumzone = "https://api.raptoreum.zone/v1/miners?method="
    Public poolurlraptoreumtone_wallet = "https://raptoreum.zone/miners/"

    Public donationadress As String = "REGCJ1eEiopwUFwaHVmUiXZTSPW9gfZdyH"

    Public SRBMinerDownloadpathWinows = "https://github.com/doktor83/SRBMiner-Multi/releases/download/2.0.2/SRBMiner-Multi-2-0-2-win64.zip"
    Public SRBMinerDownloadpathLinux = "https://github.com/doktor83/SRBMiner-Multi/releases/download/2.0.2/SRBMiner-Multi-2-0-2-Linux.tar.xz"
    Public SRBMinerDownloadnameWindows = "SRBMiner-Multi-2-0-2-win64.zip"
    Public SRBMinerDownloadnameLinux = "SRBMiner-Multi-2-0-2-Linux.tar.xz"
    Public SRBdirectory = "SRBMiner-Multi-2-0-2"

    Public def_ps As String = "Raptorhash.com"
    Public def_m As String = "SRBMiner-MULTI"
    Public def_s As String = "statum+tcp://na.raptorhash.com:6900"
    Public def_c As String = "Default"
    Public def_pw As String = "c=RTM"
    Public def_d As String = "True"
End Module
