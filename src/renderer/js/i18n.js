/**
 * src/main/js/i18n.js
 */
window.currentLang = 'en';

window.translatePage = async function(lang) {
    if (lang) window.currentLang = lang;

    try {
        const response = await fetch(`./locales/${window.currentLang}.json`);
        
        if (!response.ok) {
            console.error(`Language file not found: ./locales/${window.currentLang}.json`);
            return;
        }

        const translations = await response.json();

        const elements = document.querySelectorAll('[data-i18n]');
        elements.forEach(el => {
            const key = el.getAttribute('data-i18n');
            const translation = key.split('.').reduce((obj, i) => obj?.[i], translations);

            if (translation) {
                if (el.tagName === 'INPUT' || el.tagName === 'TEXTAREA') {
                    el.placeholder = translation;
                } else {
                    el.innerHTML = translation;
                }
            }
        });
        
        console.log(`Global translation applied: ${window.currentLang}`);
    } catch (err) {
        console.error("Translation Error:", err);
    }
};