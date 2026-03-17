RAPTORWINGS ©2024 by Raptoreum and Germardies
=============================================

Lightpaper
-----------
RAPTORWINGS is a fully open source free GUI software.\
It serves as a Raptoreum dashboard and an easy tool for both mining and tracking RTM for the everyday user.
 
This software was written to provide the Raptoreum community with the following:

1. An overview of own wallet address(es) with balance and price display
2. A graphical interface for local mining
3. A graphical interface for mining connection to external devices
4. One of the easiest routes in blockchain for both new and experienced miners to get started with Raptoreum mining with both
   local and cloud based deployments

It is totally free to use without any fee.\
Mining fee is set by the third party miner and the pool server.\
If Raptorwings proves popular, we will continue to expand it.

Documentation & Instructions
----------------------------
You can view the complete documentation, as well as some explanations about RAPTORWINGS at
https://github.com/Raptor3um/RaptorWings/blob/main/Documentation/index.md

## Technical

> [!IMPORTANT]
> This is the first BETA version with for support with XMRig. It is currently still being tested!

### APIs

RAPTORWINGS uses the following data points:

 - API: <https://explorer.raptoreum.com> 
 - API: <https://api.coingecko.com>
 - API: <https://raptorhash.com>
 - API: <https://raptoreum.zone>
 - API: <https://flockpool.com>
 
### Third-party software

The following third-party programs are used and packaged in the current Rapworwings version:

- [XMRIG](https://xmrig.com): Cryptocurrency mining
- [PuTTY](https://putty.org): Terminal emulation
- [Visual Studio CE][]: RAPTORWINGS is programmed in Visual Basic with [Visual Studio Community Edition][]

### Saved and stored data

In order to operate and be configured, RAPTORWINGS will place its required files on your machine at:

- Windows: `%LocalAppData%\Raptorwings` <!-- `Locale` is not a location under AppData ... -->
<!-- - Linux (Debian etc): `~/??????` --> <!-- Currently hidden because not sure if RAPTORWINGS runs through Mono or not -->

## Important

### DISCLAIMER

Raptoreum holds no liability for third-party API data, or the function of third-party programs used in the RAPTORWINGS system.\
You are solely responsible for the use of this software.
 
### PRIVACY

No data is collected, forwarded or stored externally by this software. **NO DATA COLLECTION TAKES PLACE AT ALL.**\
Any data stored by the user will only be stored on the user's devices.
 
> [!NOTE]
> This is official software of Raptoreum, Feathered Corp

Contributors
-------------------
- Germardies − Code
- Zlata Amaranth − Graphics
 
## Changes

### Bug Fixed:

1. Problem with SSL ports fixed

## Copyright

Copyright (c) 2024 The Raptoreum developers (<https://github.com/Raptor3um>)\
Copyright (c) 2024-2026 Germardies (<https://github.com/Germardies>)

### License

The MIT License (MIT) and the [License] file

[Visual Studio CE]: https://visualstudio.microsoft.com/de/vs/
[Visual Studio Community Edition]: https://visualstudio.microsoft.com/de/vs/
