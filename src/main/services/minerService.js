/**
 * src/main/services/minerService.js
 */
const axios = require('axios'); 
const os = require('os'); 

let logger = null; 

const minerService = { 
    setLogger(mainLogger) { logger = mainLogger; }, 

    async fetchMinerConfig() {
        try {
            const url = 'https://api.xmrig.com/1/latest_release';
            const response = await axios.get(url);
            const data = response.data;
            const platform = process.platform;
            
            const targetId = platform === 'win32' ? 'windows-x64' : 'linux-static-x64';
            
            let downloadUrl = null;

            if (data.assets && Array.isArray(data.assets)) {
                const asset = data.assets.find(a => a.id === targetId);
                downloadUrl = asset ? asset.url : null;
            }

            const results = {
                "XMRig": {
                    version: data.version || "latest",
                    downloadUrl: downloadUrl, 
                    platform: platform
                }
            };

            if (logger) {
                if (downloadUrl) logger.info(`MinerService: XMRig URL found: ${downloadUrl}`);
                else logger.error(`MinerService: No asset found for ID ${targetId}`);
            }
            
            return results;
        } catch (error) {
            if (logger) logger.error(`MinerService: API Error: ${error.message}`);
            return null;
        }
    }
}; 

module.exports = minerService;