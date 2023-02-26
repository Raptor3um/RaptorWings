RAPTORWINGS ©2023 by Raptoreum and Germardies
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

Documentation & Instructions
----------------------------
You can see the complete documentation, as well as some explanations about Raptorwings at
https://github.com/Raptor3um/RaptorWings/blob/main/Documentation/index.md

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


Changelog v1.0.0
----------------
1. Spanish language integration
*Thanks to Team RTM Spain for the support*

2. New Window/Tab: RTM Support
*A small support page to download the latest corewallet, backup your wallet and for automatic bootstrap updates*

3. New Mining Setting: Worker Thread Priority
*Integrated as community wish for BenjixLeGaulois#7543*

4. New Function: Query for saving the wallet list
*If the wallet list has not yet been saved by the user and the user leaves the wallet menu or wants to close Raptorwings, *
*a prompt now appears asking if the list should still be saved.*

5. Change the Donation Adress to a Smartnode on SullyNodes

6. Change Roadmap.md

Bug Fixed:
----------
1. Bug fixed: The function for querying whether the SRBMiner multi is running as a Windows task has been changed. In the previous version the running process was not always
2. Bug fixed: An old code fragment from the testnet caused the SRB miner to not use the full number of cores for a specific setting.

Third Party Changes:
--------------------
Change SRBMiner-MULTI from Version 2.0.2 to 2.1.0 