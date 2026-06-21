/**
 * src/renderer/views/addresses/addresses.js
 */
window.addressUI = {
    editingIndex: -1,

    async loadAddresses() {
        console.log("Loading addresses...");
        const addresses = await window.api.invoke('get-addresses');
        this.renderTable(addresses);
    },

    renderTable(addresses) {
        const tbody = document.getElementById('address-table-body');
        if (!tbody) return; 
        
        tbody.innerHTML = '';
        addresses.forEach((item, index) => {
            const row = `
                <tr>
                    <td class="addr-text">${item.address}</td>
                    <td>${item.description}</td>
                    <td>
                        <button onclick="window.addressUI.openModal(${index})" class="btn-icon">Edit</button>
                        <button onclick="window.addressUI.delete(${index})" class="btn-icon btn-danger">Del</button>
                    </td>
                </tr>`;
            tbody.insertAdjacentHTML('beforeend', row);
        });
    },

    async openModal(index = -1) {
        this.editingIndex = index;
        const modal = document.getElementById('address-modal');
        const title = document.getElementById('modal-title');
        
        if (!modal || !title) return;

        if (index > -1) {
            const addresses = await window.api.invoke('get-addresses');
            const addr = addresses[index];
            document.getElementById('inp-address').value = addr.address;
            document.getElementById('inp-desc').value = addr.description;
            title.innerText = "Edit Address";
        } else {
            document.getElementById('inp-address').value = '';
            document.getElementById('inp-desc').value = '';
            title.innerText = "Add New Address";
        }
        modal.style.display = 'flex';
    },

    closeModal() {
        document.getElementById('address-modal').style.display = 'none';
    },

    async save() {
        const address = document.getElementById('inp-address').value.trim();
        const description = document.getElementById('inp-desc').value.trim();
        if (!address) return alert("Bitte Adresse eingeben");

        try {
            const saveBtn = document.querySelector('.modal-actions .btn-primary');
            saveBtn.disabled = true;

            const validation = await window.api.invoke('validate-address', address);
            if (!validation || !validation.valid) {
                alert("Ungültige Adresse!");
                saveBtn.disabled = false;
                return;
            }

            const addresses = await window.api.invoke('get-addresses');
            const newEntry = { address, description, profile: 0, notes: "" };

            if (this.editingIndex > -1) {
                addresses[this.editingIndex] = newEntry;
            } else {
                addresses.push(newEntry);
            }

            await window.api.invoke('save-address', addresses);
            this.closeModal();
            this.loadAddresses();
        } catch (err) {
            console.error(err);
        }
    },

    async delete(index) {
        if (confirm("Wirklich löschen?")) {
            console.log("Lösche Index:", index);
            const result = await window.api.invoke('delete-address', index);
            console.log("Ergebnis vom Main:", result);
            
            if (result && result.success) {
                this.loadAddresses();
            }
        }
    }
};

window.addressUI.loadAddresses();