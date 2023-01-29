Imports System.Globalization

Module globale

    'Generell
    'Generell - Own path of the started exe / Eigener Pfad der gestarteten Exe
    Public selfpath As String = Application.StartupPath
    'Generell - Find Local Folder Roaming and Change in Local Folder /Lokal
    Public localfolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "Local") + "\RaptorWings\"
    'Generell - Summary of Walletbalance
    Public balancesummyglobal = 0 'Global variable for the total amount of coins in the wallet overview / Globale Variable für die Gesamtsumme der Coins in der Walletübersicht

    'Languages
    Public systemlanguage As String = CultureInfo.CurrentCulture.Name 'Systemsprache
    Public xmlLanguagesCodes As String = "EN"

    'Locale-Files
    Public languagefile = selfpath + "\Languages\Language.xml" 'Path to languages.xml
    Public localwallet As String = localfolder & "main.dat"
    Public localwingsheet As String = localfolder & "sheet.dat"
    Public localdevice As String = localfolder & "device.dat"
    Public localpool As String = localfolder & "pool.dat"
    Public lovalreadmeEN As String = selfpath + "README_EN.txt"

    'API's
    'API Webadress RTM
    Public apiwalletbalanceurl = "https://explorer.raptoreum.com/api/getaddressbalance?address=" 'URL to Walletbalance API / URL zur walletbalance API
    Public walletexploererurl = "https://explorer.raptoreum.com/address/" 'URL to the RTM Explorer wallet API / URL zur Wallet API des RTm Explorers

    'API Webadress Coingecko
    Public gcapiurl = "https://api.coingecko.com/api/v3/coins/raptoreum?localization=false" 'URL for Cooingecko API / URL der Coingecko API

    'API Miningpools
    'API Miningpools Raptorhash
    Public poolurlraptorhash = "http://raptorhash.com/api/walletEx?address="
    Public poolurlraptorhash_wallet = "https://raptorhash.com/?address="

    'API Miningpools Flockpool
    Public poolurlflockpool = "https://flockpool.com/api/v1/wallets/rtm/"
    Public poolurlflockpool_wallet = "https://flockpool.com/miners/rtm/"

    'API Miningpools Raptoreum.Zone
    Public poolurlraptoreumzone = "https://api.raptoreum.zone/v1/miners?method="
    Public poolurlraptoreumtone_wallet = "https://raptoreum.zone/miners/"

    'Donation Adress
    Public donationadress As String = "REGCJ1eEiopwUFwaHVmUiXZTSPW9gfZdyH"

    'SRB Miner
    Public SRBMinerDownloadpathWinows = "https://github.com/doktor83/SRBMiner-Multi/releases/download/2.0.2/SRBMiner-Multi-2-0-2-win64.zip"
    Public SRBMinerDownloadpathLinux = "https://github.com/doktor83/SRBMiner-Multi/releases/download/2.0.2/SRBMiner-Multi-2-0-2-Linux.tar.xz"
    Public SRBMinerDownloadnameWindows = "SRBMiner-Multi-2-0-2-win64.zip"
    Public SRBMinerDownloadnameLinux = "SRBMiner-Multi-2-0-2-Linux.tar.xz"
    Public SRBdirectory = "SRBMiner-Multi-2-0-2"


End Module
