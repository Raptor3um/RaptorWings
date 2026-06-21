/**
 * src/renderer/views/settings/settings.js
 */
window.settingsUI = {
    async init() {
        const config = await window.api.invoke('get-config');
        
        if (config) {
            console.log("SettingsUI: Config receptions", config);
            
            const langSelect = document.getElementById('select-language');
            if (langSelect) langSelect.value = config.language || 'en';

            const pathInput = document.getElementById('input-miner-path');
            if (pathInput) pathInput.value = config.minerDownloadPath || '';
        }
    },

    async changeLanguage(lang) {
        const currentConfig = await window.api.invoke('get-config');
        
        const updatedConfig = { 
            ...currentConfig, 
            language: lang 
        };
        
        const result = await window.api.invoke('set-config', updatedConfig);
        
        if (result.success) {
            location.reload(); 
        } else {
            console.error("Error saving:", result.error);
        }
    },

    async browseMinerPath() {
        const path = await window.api.invoke('select-folder');
        if (path) {
            const pathInput = document.getElementById('input-miner-path');
            pathInput.value = path;
            
            await window.api.invoke('set-config', { minerDownloadPath: path });
            
            pathInput.style.borderColor = '#2ecc71';
            setTimeout(() => pathInput.style.borderColor = '', 2000);
            
            window.api.log('info', `Miner path updated to: ${path}`);
        }
    },

    async openLogFile() {
        const result = await window.api.invoke('open-log-file');
        if (!result.success) {
            console.error("Log-Error:", result.error);
        }
    }
};

document.addEventListener('DOMContentLoaded', () => {
    if (document.getElementById('input-miner-path')) {
        window.settingsUI.init();
    }
});