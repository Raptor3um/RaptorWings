/**
 * Storage Service
 * Handles reading and writing the addresses.json in AppData
 */
const fs = require('fs');
const path = require('path');
const { app } = require('electron');

const addrPath = path.join(app.getPath('userData'), 'addresses.json');

const storageService = {
    async getAddresses() {
        try {
            if (!fs.existsSync(addrPath)) return [];
            const data = fs.readFileSync(addrPath, 'utf8');
            return JSON.parse(data || '[]');
        } catch (e) { return []; }
    },

    async saveAddresses(addresses) {
        try {
            fs.writeFileSync(addrPath, JSON.stringify(addresses, null, 2));
            return true;
        } catch (e) {
            console.error("Storage Error:", e);
            throw e;
        }
    }
};

module.exports = {
    getAddresses: storageService.getAddresses,
    saveAddresses: storageService.saveAddresses,
};