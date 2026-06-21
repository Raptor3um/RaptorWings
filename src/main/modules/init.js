/**
 * src/main/modules/init.js
 */
const { app } = require('electron');
const fs = require('fs');
const path = require('path');
const log = require('electron-log');
const os = require('os');
const axios = require('axios');
const storageService = require('../services/storageService'); 

async function initializeApp() {
    const userDataPath = app.getPath('userData');
    const logPath = path.join(userDataPath, 'logs');
    const configPath = path.join(userDataPath, 'config.json');

    // Secure folders
    if (!fs.existsSync(userDataPath)) fs.mkdirSync(userDataPath, { recursive: true });
    if (!fs.existsSync(logPath)) fs.mkdirSync(logPath, { recursive: true });

    log.transports.file.resolvePathFn = () => path.join(logPath, 'main.log');
    log.info('=== SYSTEM STARTUP CHECKLIST ===');

    // 1. Opperating System
    log.info(`OS: ${os.type()} ${os.release()} (${os.arch()})`);
    log.info(`Platform: ${process.platform}`);

    // 2. System Paths & Config
    log.info(`UserData Path: ${userDataPath}`);
    if (fs.existsSync(configPath)) {
        const cfg = fs.readFileSync(configPath, 'utf8');
        log.info(`Config found: ${cfg.replace(/\s+/g, ' ')}`);
    } else {
        log.warn('Config file missing (will be created on first save)');
    }

    // 3. check stored addresses
    try {
        const addresses = await storageService.getAddresses();
        log.info(`Addresses found: ${addresses.length}`);
        if (addresses.length > 0) {
            log.info(`First address check: ${addresses[0].address.substring(0,5)}...`);
        }
    } catch (e) {
        log.error(`Address file error: ${e.message}`);
    }

    // 4. API Connectivity
    // Check connectivity to essential APIs
    const apis = [
        { name: 'Raptoreum Explorer', url: 'https://explorer.raptoreum.com/api/getblockcount' },
        { name: 'XMRig API', url: 'https://api.xmrig.com/1/latest_release' }
    ];

    for (const api of apis) {
        try {
            await axios.get(api.url, { timeout: 3000 });
            log.info(`API Connectivity: ${api.name} is ONLINE`);
        } catch (e) {
            log.warn(`API Connectivity: ${api.name} is OFFLINE or TIMEOUT (${e.message})`);
        }
    }

    log.info('=== CHECKLIST COMPLETE ===');
    return { log, userDataPath };
}

module.exports = { initializeApp };