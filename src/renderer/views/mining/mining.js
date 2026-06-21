/**
 * src/renderer/views/mining/mining.js
 */
window.miningUI = { 
    allPools: {}, 
    minerData: {}, 
    isMiningActive: false, 

    async init() { 
        this.log('info', 'Initializing Mining UI...');
        try {
            await this.loadAddresses(); 
            await this.loadPools(); 
            await this.loadMinerInfo(); 
            await this.loadThreadCount(); 
            
            if (window.api && typeof window.api.on === 'function') {
                window.api.on('miner-line', (line) => {
                    const content = document.getElementById('ticker-content');
                    if (content) {
                        const shortLine = line.includes('] ') ? line.split('] ')[1] : line;
                        content.innerText = shortLine;
                        content.style.color = '#fff';
                        setTimeout(() => { if(content) content.style.color = '#aaa'; }, 100);
                    }
                });

                window.api.on('miner-stopped', () => { 
                    this.log('info', 'Received miner-stopped signal');
                    this.isMiningActive = false; 
                    this.updateToggleButton(false); 
                }); 

                window.api.on('language-changed', (newLang) => {
                    if (window.i18n) {
                        window.i18n.changeLanguage(newLang).then(() => this.generateCommand());
                    }
                });
            } 

            setTimeout(() => this.generateCommand(), 200); 
            this.log('info', 'Mining UI initialization complete.');
        } catch (err) {
            this.log('error', `Mining UI Init failed: ${err.message}`);
        }
    }, 

    log(level, msg) {
        if (window.api && window.api.log) {
            window.api.log(level, `[MiningUI] ${msg}`);
        }
    },

    async toggleMining() { 
        if (this.isMiningActive) await this.stopMining(); 
        else await this.startMining(); 
    }, 

    async startMining() {
        const addressSelect = document.getElementById('select-address');
        const selectedAddress = addressSelect ? addressSelect.value : '';

        if (!selectedAddress || selectedAddress === "" || selectedAddress === "YOUR_WALLET") {
            
            let errorMsg = "Please select an address!";

            try {
                const response = await fetch(`./locales/${window.currentLang}.json`);
                const translations = await response.json();
                
                const key = 'mining.error_no_address';
                const translation = key.split('.').reduce((obj, i) => obj?.[i], translations);
                
                if (translation) errorMsg = translation;
            } catch (err) {
                console.error("Could not fetch translation for alert:", err);
            }

            alert(errorMsg);
            return; 
        }

        const miner = document.getElementById('select-miner').value;
        const minerInfo = this.minerData[miner];
        const cmd = document.getElementById('command-preview').value;

        if (!minerInfo?.downloadUrl) {
            alert("Miner download URL not found!");
            return;
        }

        const result = await window.api.invoke('start-miner-process', {
            minerName: miner,
            command: cmd,
            downloadUrl: minerInfo.downloadUrl,
            targetDir: (await window.api.invoke('get-config')).minerDownloadPath
        });
        
        if (result?.success) {
            this.isMiningActive = true;
            this.updateToggleButton(true);
        } else {
            alert("Error starting miner: " + (result?.error || "Unknown"));
        }
    },

    updateToggleButton(running) {
        const btn = document.getElementById('btn-toggle-mining');
        const consoleBtn = document.getElementById('btn-show-console');
        const ticker = document.getElementById('miner-live-ticker');

        if (running) {
            btn.innerHTML = `<span>Stop Mining</span>`;
            btn.className = 'btn-primary stop';
            if (consoleBtn) consoleBtn.style.display = 'inline-block';
            if (ticker) ticker.style.display = 'block';
        } else {
            btn.innerHTML = `<span>Start Mining</span>`;
            btn.className = 'btn-primary start';
            if (consoleBtn) consoleBtn.style.display = 'none';
            if (ticker) {
                ticker.style.display = 'none';
                document.getElementById('ticker-content').innerText = "";
            }
        }
    },

    async loadAddresses() { 
        const addresses = await window.api.invoke('get-addresses') || []; 
        const select = document.getElementById('select-address'); 
        if (select) select.innerHTML = addresses.map(addr => `<option value="${addr.address}">${addr.description || addr.address}</option>`).join('');
    }, 

    async loadPools() { 
        const rawData = await window.api.invoke('get-pools'); 
        const data = (typeof rawData === 'string') ? JSON.parse(rawData) : rawData; 
        this.allPools = Array.isArray(data) ? data[0] : data; 
        this.updatePoolDropdown(); 
    }, 

    updatePoolDropdown() { 
        const mode = document.getElementById('select-mode').value; 
        const selectPool = document.getElementById('select-pool'); 
        if (!selectPool) return;
        
        selectPool.innerHTML = ''; 
        
        for (const poolName in this.allPools) { 
            const hasServers = this.allPools[poolName][mode] && this.allPools[poolName][mode].length > 0;
            
            if (hasServers) {
                const opt = document.createElement('option');
                opt.value = poolName; 
                opt.innerText = poolName;
                selectPool.appendChild(opt);
            }
        } 
        
        if (selectPool.options.length > 0) {
            this.onPoolChange(selectPool.value); 
        } else {
            const selectServer = document.getElementById('select-server');
            if (selectServer) selectServer.innerHTML = '<option value="">No servers available</option>';
            this.generateCommand();
        }
    }, 

    async loadMinerInfo() { 
        const rawData = await window.api.invoke('get-miner-info'); 
        this.minerData = rawData || {}; 
        const selectMiner = document.getElementById('select-miner'); 
        if (selectMiner) {
            selectMiner.innerHTML = Object.keys(this.minerData).map(name => `<option value="${name}">${name}</option>`).join('');
            this.updateMinerVersion();
        }
    }, 

    updateMinerVersion() { 
        const selected = document.getElementById('select-miner').value; 
        const display = document.getElementById('display-version');
        if (display) display.value = this.minerData[selected]?.version || ''; 
        this.generateCommand(); 
    }, 

    onModeChange() { this.updatePoolDropdown(); }, 

    onPoolChange(poolName) { 
        const mode = document.getElementById('select-mode').value; 
        const selectServer = document.getElementById('select-server'); 
        const groupServer = document.getElementById('group-server');
        const servers = this.allPools[poolName]?.[mode] || []; 
        
        if (servers.length > 0) { 
            selectServer.innerHTML = servers.map(s => `<option value="${s}">${s.replace('stratum+tcp://','')}</option>`).join(''); 
            if (groupServer) groupServer.style.display = 'block'; 
        } else { 
            if (groupServer) groupServer.style.display = 'none'; 
        } 
        this.generateCommand(); 
    }, 

    async checkPings() {
        const select = document.getElementById('select-server');
        if (!select || select.options.length === 0) return;
        const urls = Array.from(select.options).map(o => o.value);
        const btn = document.getElementById('btn-ping-servers');
        const display = document.getElementById('ping-result');
        
        if (btn) btn.disabled = true;
        if (display) display.innerText = "Checking...";

        try {
            const results = await window.api.invoke('check-server-pings', urls);
            if (display) {
                display.innerHTML = Object.entries(results)
                    .map(([url, ms]) => {
                        const hostAndPort = url.replace(/^stratum\+tcps?:\/\//, '');
                        
                        const host = hostAndPort.split(':')[0];
                        const isSSL = url.includes('tcps');
                        
                        const color = ms < 100 ? '#00ff41' : ms < 250 ? '#f1c40f' : '#e74c3c';
                        
                        return `<span style="color: ${color}">${host}${isSSL ? ' (SSL)' : ''}: ${ms}ms</span>`;
                    }).join(' | ');
            }
        } catch (e) { 
            if (display) display.innerText = "Ping failed."; 
        }
        finally { 
            if (btn) btn.disabled = false; 
        }
    },
    
    async loadThreadCount() { 
        const threadCount = await window.api.invoke('get-cpu-threads') || 1; 
        const select = document.getElementById('select-threads'); 
        if (!select) return;
        let h = `<option value="auto">Auto</option>`;
        for (let i = 1; i <= threadCount; i++) h += `<option value="${i}">${i}</option>`;
        select.innerHTML = h; 
        select.value = 'auto';
    }, 

    generateCommand() { 
        const address = document.getElementById('select-address')?.value || 'YOUR_WALLET'; 
        const rigName = document.getElementById('input-rigname')?.value || 'worker'; 
        const password = document.getElementById('input-password')?.value || 'x'; 
        const threads = document.getElementById('select-threads')?.value; 
        const pool = document.getElementById('select-server')?.value || 'pool_url'; 
        const expert = document.getElementById('input-expert-args')?.value || '';
        
        let cmd = `./xmrig -a gr -o ${pool} -u ${address}.${rigName} -p ${password}`; 
        if (threads !== 'auto') cmd += ` -t ${threads}`;
        if (expert.trim()) cmd += ` ${expert.trim()}`;
        
        const preview = document.getElementById('command-preview');
        if (preview) preview.value = cmd; 
    }
}; 

window.miningUI.init();