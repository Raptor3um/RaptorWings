/**
 * src/main/services/preload.js
 */
const { contextBridge, ipcRenderer } = require('electron'); 

contextBridge.exposeInMainWorld('api', { 
    invoke: (channel, data) => { 
        let validChannels = [ 
            'get-addresses', 'save-address', 'delete-address', 'validate-address', 
            'get-pools', 'get-miner-info', 'get-cpu-threads', 'check-server-pings', 
            'select-folder', 'get-config', 'set-config', 'start-miner-process', 
            'stop-miner-process','show-miner-console', 'open-log-file'
        ]; 
        if (validChannels.includes(channel)) { 
            return ipcRenderer.invoke(channel, data); 
        }
    }, 
    log: (level, message) => ipcRenderer.send('log-to-file', { level, message }), 
    on: (channel, callback) => { 
        let validChannels = [
            'init-settings', 
            'miner-log', 
            'miner-exited', 
            'miner-stopped', 
            'language-changed', 
            'miner-line'
        ]; 
        if (validChannels.includes(channel)) { 
            ipcRenderer.on(channel, (event, ...args) => callback(...args)); 
        } 
    }
});