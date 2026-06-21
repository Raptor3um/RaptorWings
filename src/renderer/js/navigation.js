/**
 * src/main/js/navigation.js
 */
async function loadView(viewName) {
    const contentArea = document.getElementById('main-content');
    
    try {
        const response = await fetch(`./views/${viewName}/${viewName}.html`);
        const html = await response.text();
        contentArea.innerHTML = html;

        const script = document.createElement('script');
        script.src = `./views/${viewName}/${viewName}.js?t=` + Date.now();
        
        script.onload = () => {
            if (viewName === 'settings' && window.settingsUI) {
                window.settingsUI.init();
            }
        };
        document.body.appendChild(script);

        if (window.translatePage) {
            await window.translatePage(); 
        }

    } catch (err) {
        console.error(err);
    }
}