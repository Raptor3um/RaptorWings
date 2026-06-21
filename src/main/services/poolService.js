/**
 * src/main/services/poolService.js
 */
const axios = require('axios');
const fs = require('fs');
const path = require('path');
const { app } = require('electron');

const POOL_URL = 'https://raw.githubusercontent.com/Raptor3um/RaptorWings/main/Config/pools.json';
const CACHE_PATH = path.join(app.getPath('userData'), 'pools_cache.json');

let poolData = null;
let logger = null; // Reference to our main logger

const poolService = {
    // Inject the logger from main.js
    setLogger(mainLogger) {
        logger = mainLogger;
    },

    async fetchPools() {
        if (logger) logger.info('PoolService: Starting fetch from GitHub...');
        
        try {
            const response = await axios.get(POOL_URL, { 
                headers: { 'Cache-Control': 'no-cache' },
                timeout: 5000 
            });
            
            poolData = response.data;
            fs.writeFileSync(CACHE_PATH, JSON.stringify(poolData, null, 4));
            
            if (logger) logger.info('PoolService: Successfully fetched and cached pools from GitHub.');
            return poolData;
        } catch (error) {
            if (logger) logger.error(`PoolService: GitHub fetch failed: ${error.message}`);
            
            if (fs.existsSync(CACHE_PATH)) {
                if (logger) logger.info('PoolService: Loading pools from local cache.');
                poolData = JSON.parse(fs.readFileSync(CACHE_PATH, 'utf-8'));
                return poolData;
            }
            
            if (logger) logger.error('PoolService: No local cache available.');
            return null;
        }
    },

    getPools() {
        return poolData;
    }
};

module.exports = poolService;