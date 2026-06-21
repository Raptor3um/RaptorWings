/**
 * src/main/services/main.js
 */
const { app, BrowserWindow, ipcMain, dialog, shell } = require('electron');
const path = require('path'); 
const { initializeApp } = require('./modules/init'); 
const storage = require('./services/storageService'); 
const scanner = require('./services/scannerService'); 
const poolService = require('./services/poolService'); 
const minerService = require('./services/minerService'); 
const os = require('os'); 
const fs = require('fs'); 
const { exec, spawn, execSync } = require('child_process'); 
const axios = require('axios'); 
const AdmZip = require('adm-zip'); 

const CURRENT_VERSION = app.getVersion();
const GITHUB_REPO = "Raptor3um/RaptorWings";

let mainWindow; 
let splashWindow; 
let logWindow = null;  
let logger;  
let latestMinerData = null; 
let isStopping = false;  
let minerWasStartedAsAdmin = false;
const activeProcesses = {};  

const configPath = path.join(app.getPath('userData'), 'config.json'); 

function logToFile(level, message) {
    try {
        const logPath = path.join(app.getPath('userData'), 'app.log');
        const timestamp = new Date().toLocaleString('de-DE');
        const formattedMsg = `[${timestamp}] [${level.toUpperCase()}] ${message}\n`;
        
        fs.appendFileSync(logPath, formattedMsg, 'utf8');
        console.log(formattedMsg.trim());
    } catch (err) {
        console.error("Schreibfehler in Logdatei:", err);
    }
}

// Recipient for log requests from the frontend (for window.api.log)
ipcMain.on('log-to-file', (event, { level, message }) => {
    if (logger) {
        logger[level](message); 
    } else {
        console.log(`[PRE-INIT-LOG] ${level}: ${message}`);
    }
});

function findExecutable(dir, name) { 
    if (!fs.existsSync(dir)) return null; 
    const items = fs.readdirSync(dir); 
    for (const item of items) { 
        const fullPath = path.join(dir, item); 
        if (fs.statSync(fullPath).isDirectory()) { 
            const found = findExecutable(fullPath, name); 
            if (found) return found; 
        } else if (item === name) {
            return fullPath; 
        }
    } 
    return null; 
}

async function universalUnpack(archivePath, targetDir, logger, minerName, originalUrl) { 
    const checkString = (originalUrl || archivePath).toLowerCase(); 
    try { 
        if (checkString.includes('.tar.gz') || checkString.includes('.tgz')) { 
            execSync(`tar -xzf "${archivePath}" -C "${targetDir}"`); 
        } else if (checkString.includes('.zip')) { 
            try { execSync(`unzip -o "${archivePath}" -d "${targetDir}"`); } 
            catch (e) { const zip = new AdmZip(archivePath); zip.extractAllTo(targetDir, true); } 
        } else if (checkString.includes('.7z')) { 
            execSync(`7z x "${archivePath}" -o"${targetDir}" -y`); 
        } 
        return true; 
    } catch (err) { 
        if (logger) logger.error(`[${minerName}] Entpacken fehlgeschlagen: ${err.message}`); 
        throw err; 
    } 
} 

function loadConfig() { 
    try { 
        if (fs.existsSync(configPath)) {
            const data = fs.readFileSync(configPath, 'utf8');
            if (data.trim().length > 0) {
                return JSON.parse(data);
            }
        } 
    } catch (err) { 
        console.error("Fehler beim Laden der Config:", err);
    }
    return { 
        language: 'en', 
        minerDownloadPath: path.join(app.getPath('home'), 'Downloads', 'RaptorWingsMiner') 
    }; 
}

function createLogWindow(minerName, minerKey) { 
    if (logWindow) { 
        logWindow.removeAllListeners('close');
        logWindow.close(); 
        logWindow = null;
    } 

    logWindow = new BrowserWindow({ 
        width: 900, height: 550, 
        title: `Miner Console: ${minerName}`, 
        backgroundColor: '#0c0c0c', 
        show: false,
        webPreferences: { nodeIntegration: true, contextIsolation: false } 
    }); 

    logWindow.setMenu(null);

    const html = `<html><head><style>body { background: #0c0c0c; color: #d1d1d1; font-family: monospace; font-size: 13px; margin: 0; padding: 15px; overflow-x: hidden; } #l { white-space: pre-wrap; word-wrap: break-word; line-height: 1.4; margin: 0; } .sys { color: #00ff41; font-weight: bold; border-bottom: 1px solid #333; padding-bottom: 5px; margin-bottom: 10px; display: block; } ::-webkit-scrollbar { width: 8px; } ::-webkit-scrollbar-track { background: #0c0c0c; } ::-webkit-scrollbar-thumb { background: #333; border-radius: 4px; }</style></head><body><span class="sys">MINER: ${minerName.toUpperCase()} - [Closing this window hides this window.]</span><pre id="l"></pre><script>const { ipcRenderer } = require('electron'); const el = document.getElementById('l'); ipcRenderer.on('d', (e, d) => { const clean = d.replace(/[\\u001b\\u009b][[()#;?]*(?:[0-9]{1,4}(?:;[0-9]{0,4})*)?[0-9A-ORZcf-nqry=><]/g, ''); el.innerText += clean; window.scrollTo(0, document.body.scrollHeight); });</script></body></html>`; 

    logWindow.loadURL(`data:text/html;base64,${Buffer.from(html).toString('base64')}`); 

    logWindow.on('close', (event) => {
        if (!isStopping) { 
            event.preventDefault(); 
            logWindow.hide();       
            logToFile('info', 'Miner console hidden (process stays active).');
        }
    });

    logWindow.on('closed', () => { 
        logWindow = null; 
        if (!isStopping) { 
            killAllMiners(); 
            if (mainWindow) mainWindow.webContents.send('miner-stopped'); 
        } 
    }); 
}

function killAllMiners() { 
    try { 
        if (process.platform === 'win32') {
            exec(`taskkill /F /IM xmrig.exe /T`); 
        } else {
            if (minerWasStartedAsAdmin) {
                exec(`pkexec pkill -9 xmrig`);
            } else {
                exec(`pkill -9 xmrig`);
            }
        }
    } catch (e) {
        logToFile('error', `KillAllMiners failed: ${e.message}`);
    } 
    
    minerWasStartedAsAdmin = false;

    Object.keys(activeProcesses).forEach(k => { 
        if (activeProcesses[k]) {
            try { activeProcesses[k].kill('SIGKILL'); } catch(e) {}
        }
        delete activeProcesses[k]; 
    }); 
}

ipcMain.handle('set-config', async (event, newConfig) => {
    try {
        const oldConfig = loadConfig(); 
        const mergedConfig = { ...oldConfig, ...newConfig };

        if (!mergedConfig.minerDownloadPath) {
            return { success: false, error: "Download path missing" };
        }

        fs.writeFileSync(configPath, JSON.stringify(mergedConfig, null, 2), 'utf8');
        return { success: true };
    } catch (err) {
        return { success: false, error: err.message };
    }
});

ipcMain.handle('check-server-pings', async (event, urls) => {
    const net = require('net');
    const results = {};
    for (const url of urls) {
        const cleanUrl = url.replace('stratum+tcp://', '').split(':');
        const host = cleanUrl[0];
        const port = parseInt(cleanUrl[1]) || 4444;
        const start = Date.now();
        try {
            await new Promise((resolve, reject) => {
                const socket = net.createConnection(port, host, () => { socket.end(); resolve(); });
                socket.setTimeout(2000);
                socket.on('timeout', () => { socket.destroy(); reject(); });
                socket.on('error', reject);
            });
            results[url] = Date.now() - start;
        } catch (e) { results[url] = 999; }
    }
    return results;
});

ipcMain.handle('start-miner-process', async (event, { minerName, command, downloadUrl, targetDir }) => { 
    if (isStopping) return { success: false, error: "Stop-Vorgang läuft..." }; 
     
    try { 
        if (!fs.existsSync(targetDir)) fs.mkdirSync(targetDir, { recursive: true }); 

        const config = loadConfig();
        const isDe = config.language === 'de';

        const btnNo = isDe ? 'Nein' : 'No';
        const btnYes = isDe ? 'Ja' : 'Yes';

        const { response } = await dialog.showMessageBox(mainWindow, {
            type: 'question',
            buttons: [btnNo, btnYes],
            defaultId: 1,
            cancelId: 0,
            title: isDe ? 'Admin-Rechte' : 'Admin Privileges',
            message: isDe 
                ? 'Möchten Sie den Miner mit Admin-Rechten starten, um die Hashrate durch MSR-Optimierung zu maximieren?' 
                : 'Would you like to start the miner with admin privileges to maximize hashrate via MSR optimization?'
        });

        const useAdmin = (response === 1);
        minerWasStartedAsAdmin = useAdmin;
        const isWin = process.platform === 'win32'; 
        const exeName = isWin ? 'xmrig.exe' : 'xmrig'; 

        let fullPath = findExecutable(targetDir, exeName); 

        if (!fullPath) { 
            const archivePath = path.join(targetDir, `xmrig_download.tmp`); 
            const res = await axios({ url: downloadUrl, method: 'GET', responseType: 'stream', timeout: 30000 }); 
            const writer = fs.createWriteStream(archivePath); 
            res.data.pipe(writer); 
            await new Promise((resolve, reject) => { 
                writer.on('finish', resolve); 
                writer.on('error', reject);
                res.data.on('error', reject);
            }); 
            await universalUnpack(archivePath, targetDir, logger, 'XMRig', downloadUrl); 
            if (fs.existsSync(archivePath)) fs.unlinkSync(archivePath); 
            fullPath = findExecutable(targetDir, exeName); 
        } 

        if (!isWin && fullPath) fs.chmodSync(fullPath, '755'); 
        
        let finalCommand = command.replace(/^\.\/xmrig/, `"${fullPath}"`); 

        if (useAdmin && !isWin) {
            finalCommand = `pkexec "${fullPath}" ${finalCommand.split(`"${fullPath}"`)[1]}`;
        }

        createLogWindow('XMRig', 'XMRig'); 
         
        const child = spawn(finalCommand, { 
            cwd: path.dirname(fullPath), 
            shell: true,
            env: { ...process.env },
            windowsHide: true 
        }); 
        activeProcesses['XMRig'] = child; 

        child.stdout.on('data', (data) => {
            const output = data.toString();
            if (logWindow && !logWindow.isDestroyed()) {
                logWindow.webContents.send('d', output);
            }
            const lines = output.split(/\r?\n/);
            lines.forEach(line => {
                const cleanLine = line.replace(/[\u001b\u009b][[()#;?]*(?:[0-9]{1,4}(?:;[0-9]{0,4})*)?[0-9A-ORZcf-nqry=><]/g, '').trim();
                if (cleanLine && mainWindow && !mainWindow.isDestroyed()) {
                    mainWindow.webContents.send('miner-line', cleanLine);
                }
            });
        });

        child.stderr.on('data', (data) => { 
            if (logWindow && !logWindow.isDestroyed()) {
                logWindow.webContents.send('d', `ERROR: ${data.toString()}`); 
            }
        }); 

        child.on('exit', (code, signal) => {
            logToFile('info', `Miner process exited with code ${code} and signal ${signal}`);
            delete activeProcesses['XMRig']; 
            if (mainWindow && !mainWindow.isDestroyed()) {
                mainWindow.webContents.send('miner-stopped'); 
            }
            if (logWindow && !logWindow.isDestroyed()) {
                logWindow.removeAllListeners('close'); 
                logWindow.close(); 
                logWindow = null;
            }
        }); 

        return { success: true };  
    } catch (err) { 
        logToFile('error', `Start failed: ${err.message}`);
        return { success: false, error: err.message }; 
    } 
});

ipcMain.handle('stop-miner-process', async () => { 
    isStopping = true; 
    
    if (logWindow) { 
        logWindow.removeAllListeners('close'); 
        logWindow.close(); 
        logWindow = null; 
    } 

    killAllMiners(); 
    
    setTimeout(() => { isStopping = false; }, 1000); 
    return { success: true }; 
});

app.on('will-quit', killAllMiners); 
ipcMain.handle('get-pools', async () => JSON.stringify(poolService.getPools())); 
ipcMain.handle('get-addresses', async () => storage.getAddresses()); 
ipcMain.handle('get-config', async () => loadConfig()); 
ipcMain.handle('get-cpu-threads', () => os.cpus().length); 
ipcMain.handle('get-miner-info', () => latestMinerData); 
ipcMain.handle('select-folder', async () => { 
    const result = await dialog.showOpenDialog({ properties: ['openDirectory'] }); 
    return result.canceled ? null : result.filePaths[0]; 
}); 

function createWindows() { 
    splashWindow = new BrowserWindow({ 
        width: 400, 
        height: 300, 
        frame: false, 
        transparent: true,
        icon: path.join(__dirname, '../assets/icon.png')
    }); 
    
    splashWindow.loadFile(path.join(__dirname, '../renderer/splash.html')); 

    mainWindow = new BrowserWindow({ 
        width: 1200, 
        height: 800, 
        show: false,
        autoHideMenuBar: true, 
        icon: path.join(__dirname, '../assets/icon.png'), 
        webPreferences: { 
            preload: path.join(__dirname, 'preload.js'), 
            contextIsolation: true 
        } 
    });
    mainWindow.loadFile(path.join(__dirname, '../renderer/index.html')); 
    setTimeout(() => { if (splashWindow) splashWindow.close(); mainWindow.show(); }, 3000);  
} 

app.whenReady().then(async () => { 
    const initData = await initializeApp(); 
    logger = initData.log; 
    
    minerService.setLogger(logger); 
    poolService.setLogger(logger); 

    let updateResult = null;
    try {
        updateResult = await checkAppUpdate();
        if (updateResult && updateResult.updateAvailable) {
            logger.warn(`UpdateCheck: ALERT! New version v${updateResult.version} is ready for download.`);
        }
    } catch (err) {
        logger.error(`UpdateCheck: Logic Error: ${err.message}`);
    }

    try {
        logger.info('Fetching Miner Config...');
        latestMinerData = await minerService.fetchMinerConfig(); 
        if (latestMinerData && latestMinerData.XMRig) {
            logger.info(`Miner Found: v${latestMinerData.XMRig.version}`);
        } else {
            logger.error('Miner Data structure invalid or empty!');
        }
    } catch (e) {
        logger.error(`Miner Fetch failed: ${e.message}`);
    }

    await poolService.fetchPools(); 
    
    createWindows(); 

    mainWindow.once('ready-to-show', () => {
        if (updateResult && updateResult.updateAvailable) {
            const isDe = loadConfig().language === 'de';
            dialog.showMessageBox(mainWindow, {
                type: 'info',
                title: isDe ? 'Update verfügbar' : 'Update Available',
                message: isDe 
                    ? `Eine neue Version (v${updateResult.version}) von RaptorWings ist verfügbar!` 
                    : `A new version (v${updateResult.version}) of RaptorWings is available!`,
                buttons: [isDe ? 'Später' : 'Later', 'GitHub'],
                defaultId: 1
            }).then(selection => {
                if (selection.response === 1) {
                    shell.openExternal(updateResult.url);
                }
            });
        }
    });
});

ipcMain.handle('open-external', async (event, url) => {
    await shell.openExternal(url);
});

ipcMain.handle('validate-address', async (event, address) => {
    if (!address || typeof address !== 'string') return { valid: false };
    
    const isValid = address.startsWith('R') && address.length >= 30 && address.length <= 45;
    return { valid: isValid };
});

ipcMain.handle('save-address', async (event, addresses) => {
    try {
        await storage.saveAddresses(addresses);
        return { success: true };
    } catch (err) {
        return { success: false, error: err.message };
    }
});

ipcMain.handle('delete-address', async (event, index) => {
    try {
        console.log("Main Process: Delete address at index", index);
        const addresses = await storage.getAddresses(); 
        
        if (index >= 0 && index < addresses.length) {
            addresses.splice(index, 1);
            await storage.saveAddresses(addresses);
            console.log("Main Process: Delete address at index", index, "successful.");
            return { success: true };
        } else {
            console.error("Main Process: Invalid index", index);
            return { success: false, error: "Index out of bounds" };
        }
    } catch (err) {
        console.error("Main Process: Error deleting address:", err);
        return { success: false, error: err.message };
    }
});

ipcMain.handle('show-miner-console', () => {
    if (logWindow) {
        logWindow.show();      
        logWindow.focus();
        logToFile('info', 'Miner console window displayed to user.');
    } else {
        logToFile('warn', 'User clicked Show Miner, but logWindow is null.');
    }
});

ipcMain.handle('open-log-file', async () => {
    const logPath = path.join(app.getPath('userData'),'logs', 'main.log'); 

    try {
        if (!fs.existsSync(logPath)) {
            fs.writeFileSync(logPath, `--- Raptorwings Log Start: ${new Date().toLocaleString()} ---\n`, 'utf8');
        }
        
        console.log("Opening log file:", logPath);
        await shell.openPath(logPath);
        return { success: true };
    } catch (error) {
        console.error("Error opening log file:", error);
        return { success: false, error: error.message };
    }
});

async function checkAppUpdate() {
    const CURRENT_VERSION = app.getVersion();

    try {
        logger.info(`UpdateCheck: Starting. Local: v${CURRENT_VERSION} | Repo: ${GITHUB_REPO}`);

        const res = await axios.get(`https://api.github.com/repos/${GITHUB_REPO}/releases`, {
            headers: { 'User-Agent': 'RaptorWings-Miner-App' },
            timeout: 5000 
        });

        if (!res.data || res.data.length === 0) {
            logger.info('UpdateCheck: No releases found in repository.');
            return { updateAvailable: false };
        }

        const latestRelease = res.data[0];
        const latestVersion = latestRelease.tag_name.replace('v', '');
        
        logger.info(`UpdateCheck: Remote version found: v${latestVersion}`);

        const isNewer = latestVersion.localeCompare(CURRENT_VERSION, undefined, { numeric: true, sensitivity: 'base' }) > 0;

        return { 
            updateAvailable: isNewer, 
            version: latestVersion, 
            url: latestRelease.html_url 
        };
    } catch (err) {
        logger.error(`UpdateCheck: Error fetching from GitHub: ${err.message}`);
        return { updateAvailable: false };
    }
}