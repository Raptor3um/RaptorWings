RAPTORWINGS ©2023 by Raptoreum and Germardies
16/02/2023
=============================================
Lightpaper
-----------
RAPTORWINGS is a fully open source free GUI software.
It serves as a Raptoreum dashboard and an easy tool for both mining and tracking RTM for the everyday user.
 
This software was written to provide the Raptoreum community with the following:
1. An overview of own wallet address(es) with balance and price display
2. A graphical interface for local mining
3. A graphical interface for mining connection to external devices
4. One of the easiest routes in blockchain for both new and experienced miners to get started with Raptoreum mining with both
   local and cloud based deployments

It is totally free to use without any fee.
Mining fee is set by the third party miner and the pool server.
If Raptorwings proves popular, we will continue to expand it.

API's: 
------
RAPTORWINGS uses the following data points: 
 - API: https://explorer.raptoreum.com 
 - API: https://api.coingecko.com
 - API: https://raptorhash.com/
 - API: https://raptoreum.zone/
 - API: https://flockpool.com
 
THIRD PARTY SOFTWARE: 
-----------------------
The following third-party programs are used and packaged in the current Rapworwings version: 
- SRBMINER-MULTI: https://srbminer.com
- Putty: https://putty.org
- RAPTORWINGS is programmed in Visual Basic with Visual Studio Community Edition (https://visualstudio.microsoft.com/de/vs/)
 
DISCLAIMER
----------------
Raptoreum holds no liability for third party API data, or the function of third party programs used in the Raptorwings system. 
You are solely responsible for the use of this software.
 
PRIVACY
-----------
No data is collected, forwarded or stored externally by this software. 
NO DATA COLLECTION TAKES PLACE AT ALL. 
Any data stored by the user will only be stored on your devices.
 
NOTE
-------
- This is official software of Raptoreum, Feathered Corp
- Files will be placed on your PC "C:\Users\<user>\Appdata\Locale\Raptorwings\" which is used to store your data.

CONTRIBUTORS
-------------------
Germardies - Code
Zlata Amaranth - Graphics
 
COPYRIGHT
---------
The MIT License (MIT)
Copyright (c) 2023 The Raptoreum developers (https://github.com/Raptor3um)
Copyright (c) 2023 Germardies (https://github.com/Germardies)

Changelog
---------
Version 0.99h
Changes:
01) Message after deleting a wallet address has been adjusted.
02) Message after saving the wallet list was adjusted.
03) Message after wallet address verification has been adjusted.
04) Adjusted message that is displayed when you have not created a wallet address for mining.
05) Adjusted messages that are displayed when entries are missing when saving a wingsheet.
06) Adjusted message that is displayed when you want to overwrite the default wingsheet.
07) Adjusted message that is displayed when you have created a MultinwingMining device.
08) Adjusted message that is displayed when one has deleted a MultinwingMining device.
09) Adjusted message that is displayed when something is wrong with the file system for mining.
10) Adjusted message displayed when trying to mine without wallet (MultiWingmining). 

Bug Fixed:
01) Button "Delete wallet address" no longer executes a function if there is no entry in the list.
Bug found by Discord User: OvErLoDe#4871

02) Button "Open RTM Explorer" no longer performs a function if there is no entry in the list.
Bug found by Discord OvErLoDe#4871

03) Run Miner in background works again.
Bug found by Discord User: abdani#6797

04) Under MultiWingMining, "1 Core Donation" was still displayed under Default (Pure display error). Bug fixed
Bug found by Discord User: abdani#6797

05) After deleting a wallet address, the balance and price was not adjusted immediately. This is due to the 60 second 
interval of the API query. The function has been rewritten so that a query is started when an address is deleted,
regardless of the interval.
Bug found by Discord User: all_danger#5769

06) If no device is selected under MultiWingMining or there is no device in the list, clicking "Start Mining" will 
now give an error message.
Bug found by Discord User: abdani#6797

07) Duplicate addresses in wallet overview are now prevented.
Bug found by Discord User: ヾ((ﾒ`◣ ̧◢ ́) ﾉ ･ﾟ。゜゜#8220

08) Miner can be started multiple times: Normally Raptorwings detects if the SRB Miner is already running and colors 
the "Start Mining Button" red. Since this does not always seem to happen, when you click on the "Start Mining" 
button, Raptorwings now also checks if the SRB Miner is running as a Windows task.
Bug found by Discord User: ヾ((ﾒ`◣ ̧◢ ́) ﾉ ･ﾟ。゜゜#8220

09) Small English adjustments in the language file.
Bug found from Discord User: vladislav_kosko#1605

10) Header in the Instruction.pdf was adjusted
Found from Discord User: OvErLoDe#4871

11) Page 4 of the Instruction.pdf was adjusted
Found from Discord User: OvErLoDe#4871

12) Bug fixed: In the "About" window it was possible to click on the web links, but this did not lead to any further
action. Now clicking the web links opens the default browser with the clicked address.
Bug found by Discord User: ヾ((ﾒ`◣ ̧◢ ́) ﾉ ･ﾟ。゜゜#8220