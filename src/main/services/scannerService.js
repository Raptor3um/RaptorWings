/**
 * src/main/services/scannerService.js
 */
const log = require('electron-log');

async function validateAddress(address) {
    log.info(`[VALIDATION] Starting check for: ${address}`);
    
    try {
        const url = `https://texp.raptoreum.com/api/getaddressbalance/${address}&json=true`;
        
        log.info(`[VALIDATION] Requesting URL: ${url}`);

        const response = await fetch(url, {
            headers: { 'User-Agent': 'Raptorwings-v2' }
        });

        log.info(`[VALIDATION] HTTP Status: ${response.status} ${response.statusText}`);

        const text = await response.text();
        log.info(`[VALIDATION] Raw Response: ${text}`);

        let data;
        try {
            data = JSON.parse(text);
        } catch (e) {
            log.error(`[VALIDATION] Response is not valid JSON`);
            return { valid: false, error: "Explorer sent invalid data format" };
        }

        if (data.success === "true" || data.address === address) {
            log.info(`[VALIDATION] SUCCESS: Address found. Balance: ${data.balanceRTM} RTM`);
            return { valid: true };
        } else {
            log.warn(`[VALIDATION] REJECTED: ${data.message || 'Unknown error'}`);
            return { valid: false, error: data.message || "Invalid Address" };
        }

    } catch (error) {
        log.error(`[VALIDATION] FATAL ERROR: ${error.message}`);
        return { valid: false, error: "Connection to Explorer failed" };
    }
}

module.exports = { validateAddress };