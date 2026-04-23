namespace MadeyeWsdlCSharp;

internal static class WebUi
{
    public static string IndexHtml => """
<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>VisionA64 Web Console</title>
  <style>
    :root {
      --bg: #ffffff;
      --surface: #ffffff;
      --surface-soft: #f8fafc;
      --border: #dbe4ee;
      --text: #0f172a;
      --muted: #64748b;
      --accent: #2563eb;
      --accent-soft: #eff6ff;
      --good: #10b981;
      --warn: #dc2626;
      --shadow: 0 16px 40px rgba(15, 23, 42, 0.08);
      font-family: Inter, "SF Pro Display", "Segoe UI", system-ui, -apple-system, sans-serif;
    }

    * { box-sizing: border-box; }

    body {
      margin: 0;
      background: linear-gradient(180deg, #f8fbff 0%, #ffffff 22%);
      color: var(--text);
    }

    .app {
      min-height: 100vh;
      padding: 18px;
      position: relative;
    }

    .topbar {
      display: flex;
      align-items: center;
      justify-content: space-between;
      gap: 16px;
      padding-bottom: 14px;
    }

    .brand {
      display: flex;
      align-items: center;
      gap: 12px;
    }

    .menu-btn {
      width: 44px;
      height: 36px;
      border-radius: 11px;
      border: 1px solid var(--border);
      background: #fff;
      color: var(--text);
      font-size: 18px;
      cursor: pointer;
      box-shadow: 0 1px 2px rgba(15, 23, 42, 0.04);
    }

    h1 {
      margin: 0;
      font-size: 28px;
      line-height: 1.1;
    }

    .subtitle {
      margin-top: 4px;
      color: var(--muted);
      font-size: 14px;
    }

    .endpoint {
      padding: 10px 14px;
      border: 1px solid #bfdbfe;
      border-radius: 12px;
      background: var(--accent-soft);
      color: #1d4ed8;
      font-size: 13px;
      white-space: nowrap;
    }

    .layout {
      display: grid;
      grid-template-columns: 1fr;
      gap: 16px;
    }

    .card {
      background: var(--surface);
      border: 1px solid var(--border);
      border-radius: 24px;
      box-shadow: var(--shadow);
    }

    .card-inner {
      padding: 22px;
    }

    .response-head {
      display: flex;
      align-items: flex-start;
      justify-content: space-between;
      gap: 12px;
      margin-bottom: 20px;
    }

    .response-title {
      margin: 0;
      font-size: 24px;
    }

    .operation {
      margin-top: 4px;
      color: var(--muted);
      font-size: 14px;
    }

    .chip {
      display: inline-flex;
      align-items: center;
      justify-content: center;
      min-width: 84px;
      padding: 8px 14px;
      border-radius: 999px;
      color: #fff;
      font-weight: 700;
      font-size: 13px;
      background: #64748b;
    }

    .summary-grid {
      display: grid;
      grid-template-columns: 3fr 7fr;
      gap: 14px;
      margin-bottom: 16px;
    }

    .mini-card {
      background: var(--surface-soft);
      border: 1px solid var(--border);
      border-radius: 18px;
      padding: 18px;
      min-height: 128px;
    }

    .mini-label {
      color: var(--muted);
      font-size: 13px;
      margin-bottom: 14px;
    }

    .result-value {
      font-size: 30px;
      font-weight: 800;
    }

    .error-value {
      color: var(--warn);
      font-size: 15px;
      line-height: 1.4;
      white-space: pre-wrap;
    }

    .section {
      background: #fff;
      border: 1px solid var(--border);
      border-radius: 18px;
      padding: 18px;
      margin-bottom: 16px;
    }

    .section h2 {
      margin: 0 0 10px;
      font-size: 18px;
    }

    .details {
      display: grid;
      gap: 10px;
    }

    .detail-row {
      display: grid;
      grid-template-columns: auto 1fr;
      gap: 16px;
      align-items: center;
      padding: 12px 14px;
      background: var(--surface-soft);
      border: 1px solid var(--border);
      border-radius: 14px;
    }

    .detail-key {
      color: var(--muted);
      font-size: 13px;
      min-width: 120px;
    }

    .detail-value {
      text-align: right;
      font-size: 14px;
      overflow-wrap: anywhere;
    }

    pre {
      margin: 0;
      font-family: ui-monospace, SFMono-Regular, Menlo, Consolas, monospace;
      font-size: 13px;
      line-height: 1.5;
      white-space: pre-wrap;
      word-break: break-word;
    }

    .toolbar {
      display: flex;
      flex-wrap: wrap;
      gap: 10px;
      margin-bottom: 12px;
    }

    .btn {
      appearance: none;
      border: 1px solid var(--border);
      background: #fff;
      color: var(--text);
      border-radius: 14px;
      padding: 11px 14px;
      font-weight: 600;
      cursor: pointer;
      transition: transform 0.12s ease, box-shadow 0.12s ease, border-color 0.12s ease;
    }

    .btn:hover { transform: translateY(-1px); border-color: #cbd5e1; }
    .btn.primary {
      background: linear-gradient(180deg, #3b82f6 0%, #2563eb 100%);
      color: #fff;
      border-color: #2563eb;
    }

    .form-shell {
      display: none;
      gap: 10px;
      margin-bottom: 16px;
    }

    .form-shell.active {
      display: grid;
    }

    .form-grid {
      display: grid;
      gap: 12px;
      grid-template-columns: repeat(3, minmax(0, 1fr));
    }

    .form-grid.firmware {
      grid-template-columns: 1fr 1fr;
    }

    input[type="text"], input[type="file"], textarea {
      width: 100%;
      border: 1px solid var(--border);
      border-radius: 12px;
      padding: 11px 12px;
      font: inherit;
      background: #fff;
    }

    .menu-bar {
      margin-top: 10px;
      margin-bottom: 18px;
      display: grid;
      gap: 10px;
    }

    .menu-tabs {
      display: flex;
      gap: 10px;
      flex-wrap: wrap;
      align-items: center;
    }

    .menu-tab {
      border: 1px solid var(--border);
      border-radius: 999px;
      background: #fff;
      color: var(--text);
      padding: 7px 10px;
      font: inherit;
      font-weight: 600;
      font-size: 12px;
      cursor: pointer;
      box-shadow: 0 1px 2px rgba(15, 23, 42, 0.04);
    }

    .menu-tab.active {
      background: linear-gradient(180deg, #3b82f6 0%, #2563eb 100%);
      border-color: #2563eb;
      color: #fff;
    }

    .menu-dropdown {
      display: none;
      background: #fff;
      border: 1px solid var(--border);
      border-radius: 20px;
      box-shadow: var(--shadow);
      padding: 12px;
      max-height: 280px;
      overflow-y: auto;
    }

    .menu-dropdown.open {
      display: block;
    }

    .menu-dropdown-heading {
      color: var(--muted);
      font-size: 12px;
      font-weight: 800;
      margin: 2px 4px 10px;
      text-transform: uppercase;
      letter-spacing: 0.03em;
    }

    .menu-group-tabs {
      display: flex;
      gap: 8px;
      flex-wrap: wrap;
      margin-bottom: 12px;
    }

    .menu-group-tab {
      border: 1px solid var(--border);
      border-radius: 999px;
      background: #fff;
      color: var(--text);
      padding: 6px 9px;
      font: inherit;
      font-size: 12px;
      cursor: pointer;
    }

    .menu-group-tab.active {
      background: var(--accent-soft);
      color: #1d4ed8;
      border-color: #bfdbfe;
    }

    .menu-items {
      display: grid;
      gap: 8px;
    }

    .menu-item {
      width: 100%;
      text-align: left;
      display: block;
      border: 1px solid var(--border);
      background: #fff;
      padding: 8px 10px;
      border-radius: 11px;
      cursor: pointer;
      font: inherit;
      font-size: 13px;
      color: var(--text);
    }

    .menu-item:hover {
      border-color: #cbd5e1;
      background: #f8fafc;
    }

    .status {
      margin-left: auto;
      color: var(--muted);
      font-size: 12px;
    }

    @media (max-width: 900px) {
      .summary-grid { grid-template-columns: 1fr; }
      .topbar { align-items: flex-start; flex-direction: column; }
      .endpoint { white-space: normal; }
      .form-grid, .form-grid.firmware { grid-template-columns: 1fr; }
    }
  </style>
</head>
  <body>
  <div class="app">
    <div class="topbar">
      <div class="brand">
        <div>
          <h1>VisionA64 Web Console</h1>
          <div class="subtitle">Browser UI for the VisionA64 camera SOAP service</div>
        </div>
      </div>
      <div class="endpoint">Device endpoint 192.168.18.244:8080</div>
    </div>

    <div class="menu-bar">
      <div class="menu-tabs" id="menuTabs"></div>
      <div class="menu-dropdown" id="menuDropdown">
        <div class="menu-dropdown-heading" id="menuDropdownHeading">Menu</div>
        <div id="menuDropdownBody"></div>
      </div>
    </div>

    <div class="layout">
      <div class="card">
        <div class="card-inner">
          <div class="response-head">
            <div>
              <h2 class="response-title">Latest Response</h2>
              <div class="operation" id="operationLabel">Tap the burger menu to begin</div>
            </div>
            <div class="chip" id="statusChip">READY</div>
          </div>

          <div class="form-shell" id="formShell"></div>

          <div class="summary-grid">
            <div class="mini-card">
              <div class="mini-label">Result Code</div>
              <div class="result-value" id="resultValue">No data yet</div>
            </div>
            <div class="mini-card">
              <div class="mini-label">Error Message</div>
              <div class="error-value" id="errorValue">No request has run yet</div>
            </div>
          </div>

          <div class="section">
            <h2>Details</h2>
            <div class="details" id="details"></div>
          </div>

          <div class="section">
            <h2>Report</h2>
            <pre id="report">Choose an action from the top-left menu to begin.</pre>
          </div>

          <div class="section">
            <h2>Raw XML</h2>
            <pre id="rawXml">No response yet.</pre>
          </div>

          <div class="toolbar">
            <span class="status" id="statusText">Idle</span>
          </div>
        </div>
      </div>
    </div>
  </div>

  <script>
    const menuTabs = document.getElementById('menuTabs');
    const menuDropdown = document.getElementById('menuDropdown');
    const menuDropdownHeading = document.getElementById('menuDropdownHeading');
    const menuDropdownBody = document.getElementById('menuDropdownBody');
    const formShell = document.getElementById('formShell');
    const operationLabel = document.getElementById('operationLabel');
    const resultValue = document.getElementById('resultValue');
    const errorValue = document.getElementById('errorValue');
    const details = document.getElementById('details');
    const report = document.getElementById('report');
    const rawXml = document.getElementById('rawXml');
    const statusChip = document.getElementById('statusChip');
    const statusText = document.getElementById('statusText');
    let currentCategory = null;
    let currentSubcategory = 'Overview';
    let activeSmartcardSection = 'Config';

    const menuCategories = {
      'Quick Checks': {
        items: [
          ['System Check', 'system-check'],
          ['System Check Extra', 'system-check-extra']
        ]
      },
      'System Settings': {
        items: [
          ['Device ID Get', 'system-device-id-get'],
          ['Description Get', 'system-description-get'],
          ['Description Set', 'show-description-set']
        ]
      },
      'Maintenance': {
        items: [
          ['System Restart', 'system-restart'],
          ['Firmware Update', 'show-firmware-update']
        ]
      },
      'Smartcard': {
        subcategories: {
          Config: [
            ['Desfire Set', 'smartcard-desfire-set'],
            ['Desfire Get', 'smartcard-desfire-get'],
            ['Desfire Secondary Set', 'smartcard-desfire-secondary-set'],
            ['Desfire Secondary Get', 'smartcard-desfire-secondary-get'],
            ['Mifare Set', 'smartcard-mifare-set'],
            ['Mifare Get', 'smartcard-mifare-get'],
            ['Wiegand Set', 'smartcard-wiegand-set'],
            ['Wiegand Get', 'smartcard-wiegand-get']
          ],
          Detect: [
            ['Smartcard Detect', 'smartcard-detect'],
            ['Card UID Detect', 'card-uid-detect'],
            ['Wiegand Detect', 'wiegand-detect']
          ],
          Operations: [
            ['Desfire Erase', 'smartcard-desfire-erase'],
            ['Desfire Format', 'smartcard-desfire-format'],
            ['Desfire Write', 'smartcard-desfire-write'],
            ['Desfire Read', 'smartcard-desfire-read'],
            ['Mifare Write', 'smartcard-mifare-write'],
            ['Mifare Read', 'smartcard-mifare-read'],
            ['Mifare Badge Write', 'smartcard-mifare-badge-write'],
            ['Mifare Badge Read', 'smartcard-mifare-badge-read'],
            ['Desfire Badge Create', 'smartcard-desfire-badge-create'],
            ['Desfire Badge Write', 'smartcard-desfire-badge-write'],
            ['Desfire Badge Read', 'smartcard-desfire-badge-read'],
            ['Desfire Face Create', 'smartcard-desfire-face-create'],
            ['Desfire Face Write', 'smartcard-desfire-face-write'],
            ['Desfire Face Read', 'smartcard-desfire-face-read'],
            ['Ask Read', 'smartcard-ask-read']
          ]
        }
      },
      'Users': {
        subcategories: {
          Overview: [
            ['Identify Count', 'user-identify-count'],
            ['Identify List All', 'user-identify-list-all'],
            ['Smartcard Count', 'user-smartcard-count'],
            ['Smartcard List All', 'user-smartcard-list-all'],
            ['Elevator Count', 'user-elevator-count'],
            ['Elevator List All', 'user-elevator-list-all'],
            ['Restricted Count', 'user-restricted-count'],
            ['Restricted List All', 'user-restricted-list-all'],
            ['Schedule Count', 'user-schedule-count'],
            ['Schedule List All', 'user-schedule-list-all']
          ],
          Management: [
            ['Identify Add', 'user-identify-add'],
            ['Identify Delete', 'user-identify-delete'],
            ['Identify Delete All', 'user-identify-delete-all'],
            ['Identify List', 'user-identify-list'],
            ['Identify Check', 'user-identify-check'],
            ['Identify Template', 'user-identify-template'],
            ['Identify Activate', 'user-identify-activate'],
            ['Identify Deactivate', 'user-identify-deactivate'],
            ['Identify Activate All', 'user-identify-activate-all'],
            ['Restrict Enable', 'user-identify-restrict-enable'],
            ['Time Activate', 'user-identify-time-activate'],
            ['Time Deactivate', 'user-identify-time-deactivate'],
            ['Time Deactivate All', 'user-identify-time-deactivate-all']
          ]
        }
      }
    };

    function renderTabs() {
      menuTabs.innerHTML = '';
      Object.keys(menuCategories).forEach((category) => {
        const button = document.createElement('button');
        button.type = 'button';
        button.className = 'menu-tab';
        button.textContent = category;
        button.addEventListener('click', () => openMenu(category));
        menuTabs.appendChild(button);
      });
    }

    function setActiveTab(category) {
      menuTabs.querySelectorAll('.menu-tab').forEach((button) => {
        button.classList.toggle('active', button.textContent === category);
      });
    }

    function renderMenu(category, subcategory = null) {
      const config = menuCategories[category] || {};
      const hasSubcategories = Boolean(config.subcategories);
      const activeSubcategory = subcategory || (hasSubcategories ? Object.keys(config.subcategories)[0] : null);
      const items = hasSubcategories
        ? (config.subcategories?.[activeSubcategory] || [])
        : (config.items || []);

      currentCategory = category;
      if (category === 'Smartcard') {
        activeSmartcardSection = activeSubcategory || 'Config';
      } else {
        currentSubcategory = activeSubcategory || 'Overview';
      }
      menuDropdown.classList.add('open');
      menuDropdownHeading.textContent = hasSubcategories && activeSubcategory
        ? `${category} - ${activeSubcategory}`
        : category;
      menuDropdownBody.innerHTML = '';

      if (hasSubcategories) {
        const tabRow = document.createElement('div');
        tabRow.className = 'menu-group-tabs';
        Object.keys(config.subcategories || {}).forEach((groupName) => {
          const tab = document.createElement('button');
          tab.type = 'button';
          tab.className = 'menu-group-tab';
          tab.textContent = groupName;
          tab.addEventListener('click', (event) => {
            event.stopPropagation();
            renderMenu(category, groupName);
          });
          tab.classList.toggle('active', groupName === (category === 'Smartcard' ? activeSmartcardSection : currentSubcategory));
          tabRow.appendChild(tab);
        });
        menuDropdownBody.appendChild(tabRow);
      }

      const itemList = document.createElement('div');
      itemList.className = 'menu-items';
      for (const [label, action] of items) {
        const button = document.createElement('button');
        button.className = 'menu-item';
        button.type = 'button';
        button.textContent = label;
        button.dataset.action = action;
        button.addEventListener('click', async (event) => {
          event.stopPropagation();
          menuDropdown.classList.remove('open');
          await handleAction(action);
        });
        itemList.appendChild(button);
      }
      menuDropdownBody.appendChild(itemList);
      setActiveTab(category);
    }

    function openMenu(category) {
      if (currentCategory === category && menuDropdown.classList.contains('open')) {
        menuDropdown.classList.remove('open');
        return;
      }

      if (category === 'Smartcard') {
        renderMenu(category, activeSmartcardSection || 'Config');
        return;
      }

      renderMenu(category, category === 'Users' ? currentSubcategory || 'Overview' : null);
    }

    renderTabs();
    menuDropdown.classList.remove('open');

    menuTabs.addEventListener('click', (event) => event.stopPropagation());
    menuDropdown.addEventListener('click', (event) => event.stopPropagation());
    document.addEventListener('click', (event) => {
      const clickedInside = menuTabs.contains(event.target) || menuDropdown.contains(event.target);
      if (!clickedInside) {
        menuDropdown.classList.remove('open');
      }
    });

    function setStatus(text, kind) {
      statusChip.textContent = text;
      statusChip.style.background = kind === 'good' ? '#10b981' : kind === 'bad' ? '#dc2626' : kind === 'busy' ? '#2563eb' : '#64748b';
      statusText.textContent = text;
    }

    function renderResult(data) {
      operationLabel.textContent = data.operation || 'Response';
      resultValue.textContent = data.result;
      errorValue.textContent = data.errorMessage && data.errorMessage.trim() ? data.errorMessage : '(empty)';
      report.textContent = data.reportText && data.reportText.trim() ? data.reportText : `${data.reportLabel || 'Report'} is empty.`;
      rawXml.textContent = data.rawXml || 'No response payload.';

      details.innerHTML = '';
      const entries = data.details ? Object.entries(data.details) : [];
      if (entries.length === 0) {
        const empty = document.createElement('div');
        empty.className = 'detail-row';
        empty.innerHTML = '<div class="detail-key">Details</div><div class="detail-value">No additional details returned.</div>';
        details.appendChild(empty);
      } else {
        for (const [key, value] of entries) {
          const row = document.createElement('div');
          row.className = 'detail-row';
          row.innerHTML = `<div class="detail-key"></div><div class="detail-value"></div>`;
          row.querySelector('.detail-key').textContent = key;
          row.querySelector('.detail-value').textContent = value && value.trim() ? value : '(empty)';
          details.appendChild(row);
        }
      }

      setStatus(data.result === 0 ? 'OK' : 'ERROR', data.result === 0 ? 'good' : 'bad');
    }

    function setLoading(operation) {
      operationLabel.textContent = `${operation} in progress...`;
      setStatus('WORKING', 'busy');
      statusText.textContent = `${operation} in progress...`;
    }

    function clearForm() {
      formShell.className = 'form-shell';
      formShell.innerHTML = '';
    }

    function showDescriptionForm() {
      formShell.className = 'form-shell active';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">System Description Set</div>
          <div class="form-grid">
            <input id="label1" type="text" placeholder="Label 1">
            <input id="label2" type="text" placeholder="Label 2">
            <input id="label3" type="text" placeholder="Label 3">
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelBtn">Cancel</button>
            <button class="btn primary" id="submitBtn">Send</button>
          </div>
        </div>`;

      document.getElementById('cancelBtn').onclick = clearForm;
      document.getElementById('submitBtn').onclick = async () => {
        const payload = {
          label1: document.getElementById('label1').value,
          label2: document.getElementById('label2').value,
          label3: document.getElementById('label3').value
        };
        clearForm();
        await postJson('/api/system-description-set', payload, 'System Description Set');
      };
    }

    function showFirmwareForm() {
      formShell.className = 'form-shell active';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">Firmware Update</div>
          <div class="form-grid firmware">
            <input id="firmwareFile" type="file" accept=".zip">
            <input id="firmwareMd5" type="text" placeholder="First 32 chars of MD5">
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelFirmwareBtn">Cancel</button>
            <button class="btn primary" id="submitFirmwareBtn">Upload</button>
          </div>
        </div>`;

      document.getElementById('cancelFirmwareBtn').onclick = clearForm;
      document.getElementById('submitFirmwareBtn').onclick = async () => {
        const fileInput = document.getElementById('firmwareFile');
        const md5 = document.getElementById('firmwareMd5').value;
        if (!fileInput.files.length) {
          alert('Choose a firmware ZIP file first.');
          return;
        }
        const formData = new FormData();
        formData.append('file', fileInput.files[0]);
        formData.append('md5', md5);
        clearForm();
        await postForm('/api/system-firmware-update', formData, 'System Firmware Update');
      };
    }

    function showBadgeActionForm(title, endpoint, operation, submitLabel = 'Send', fieldKey = 'badgeID', placeholder = 'Badge ID') {
      formShell.className = 'form-shell active';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">${title}</div>
          <div class="form-grid" style="grid-template-columns: 1fr;">
            <input id="badgeId" type="text" placeholder="${placeholder}">
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelGenericBtn">Cancel</button>
            <button class="btn primary" id="submitGenericBtn">${submitLabel}</button>
          </div>
        </div>`;

      document.getElementById('cancelGenericBtn').onclick = clearForm;
      document.getElementById('submitGenericBtn').onclick = async () => {
        const badgeId = document.getElementById('badgeId').value.trim();
        if (!badgeId) {
          alert(`${placeholder} is required.`);
          return;
        }
        clearForm();
        await postJson(endpoint, { [fieldKey]: badgeId }, operation);
      };
    }

    function showTypeActionForm(title, endpoint, operation, defaultType = 1) {
      formShell.className = 'form-shell active';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">${title}</div>
          <div class="form-grid" style="grid-template-columns: 1fr;">
            <input id="typeValue" type="text" value="${defaultType}" placeholder="Type">
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelTypeBtn">Cancel</button>
            <button class="btn primary" id="submitTypeBtn">Send</button>
          </div>
        </div>`;

      document.getElementById('cancelTypeBtn').onclick = clearForm;
      document.getElementById('submitTypeBtn').onclick = async () => {
        const typeValue = Number(document.getElementById('typeValue').value);
        if (Number.isNaN(typeValue)) {
          alert('Type must be a number.');
          return;
        }
        clearForm();
        await postJson(endpoint, { type: typeValue }, operation);
      };
    }

    function showTimeoutForm(title, endpoint, operation, defaultTimeout = 10) {
      formShell.className = 'form-shell active';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">${title}</div>
          <div class="form-grid" style="grid-template-columns: 1fr;">
            <input id="timeoutValue" type="text" value="${defaultTimeout}" placeholder="Timeout">
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelTimeoutBtn">Cancel</button>
            <button class="btn primary" id="submitTimeoutBtn">Send</button>
          </div>
        </div>`;

      document.getElementById('cancelTimeoutBtn').onclick = clearForm;
      document.getElementById('submitTimeoutBtn').onclick = async () => {
        const timeoutValue = Number(document.getElementById('timeoutValue').value);
        if (Number.isNaN(timeoutValue)) {
          alert('Timeout must be a number.');
          return;
        }
        clearForm();
        await postJson(endpoint, { timeout: timeoutValue }, operation);
      };
    }

    function showTimeoutTypeForm(title, endpoint, operation, defaultTimeout = 10, defaultType = 0) {
      formShell.className = 'form-shell active';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">${title}</div>
          <div class="form-grid">
            <input id="timeoutValue" type="text" value="${defaultTimeout}" placeholder="Timeout">
            <input id="typeValue" type="text" value="${defaultType}" placeholder="Type">
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelTimeoutTypeBtn">Cancel</button>
            <button class="btn primary" id="submitTimeoutTypeBtn">Send</button>
          </div>
        </div>`;

      document.getElementById('cancelTimeoutTypeBtn').onclick = clearForm;
      document.getElementById('submitTimeoutTypeBtn').onclick = async () => {
        const timeoutValue = Number(document.getElementById('timeoutValue').value);
        const typeValue = Number(document.getElementById('typeValue').value);
        if (Number.isNaN(timeoutValue) || Number.isNaN(typeValue)) {
          alert('Timeout and Type must be numbers.');
          return;
        }
        clearForm();
        await postJson(endpoint, { timeout: timeoutValue, type: typeValue }, operation);
      };
    }

    function showSingleFieldForm(title, endpoint, operation, fieldKey, label, defaultValue = '', fieldType = 'text') {
      formShell.className = 'form-shell active';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">${title}</div>
          <div class="form-grid" style="grid-template-columns: 1fr;">
            <input id="singleField" type="${fieldType}" value="${defaultValue}" placeholder="${label}">
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelSingleBtn">Cancel</button>
            <button class="btn primary" id="submitSingleBtn">Send</button>
          </div>
        </div>`;

      document.getElementById('cancelSingleBtn').onclick = clearForm;
      document.getElementById('submitSingleBtn').onclick = async () => {
        const value = document.getElementById('singleField').value;
        if (!String(value).trim()) {
          alert(`${label} is required.`);
          return;
        }
        clearForm();
        await postJson(endpoint, { [fieldKey]: fieldType === 'number' ? Number(value) : value }, operation);
      };
    }

    function showSmartcardWriteForm(title, endpoint, operation, includeUserData, includeFaceData, includeType = false) {
      formShell.className = 'form-shell active';
      const typeField = includeType ? '<input id="typeValue" type="text" value="0" placeholder="Type">' : '';
      const userField = includeUserData
        ? '<input id="userData" type="text" placeholder="User Data">'
        : '';
      const faceField = includeFaceData
        ? '<input id="faceFile" type="file" accept="*/*">'
        : '';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">${title}</div>
          <div class="form-grid">
            <input id="timeoutValue" type="text" value="10" placeholder="Timeout">
            ${typeField}
          </div>
          <div class="form-grid" style="grid-template-columns: 1fr;">
            ${userField}
            ${faceField}
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelSmartcardBtn">Cancel</button>
            <button class="btn primary" id="submitSmartcardBtn">Send</button>
          </div>
        </div>`;

      document.getElementById('cancelSmartcardBtn').onclick = clearForm;
      document.getElementById('submitSmartcardBtn').onclick = async () => {
        const timeoutValue = Number(document.getElementById('timeoutValue').value);
        const typeValue = includeType ? Number(document.getElementById('typeValue').value) : 0;
        if (Number.isNaN(timeoutValue) || (includeType && Number.isNaN(typeValue))) {
          alert('Timeout and Type must be numbers.');
          return;
        }

        const payload = { timeout: timeoutValue };
        if (includeType) {
          payload.type = typeValue;
        }
        if (includeUserData) {
          payload.userData = document.getElementById('userData').value;
        }
        if (includeFaceData) {
          const fileInput = document.getElementById('faceFile');
          if (!fileInput.files.length) {
            alert('Choose a face data file first.');
            return;
          }
          payload.faceDataBase64 = await readFileAsBase64(fileInput.files[0]);
        }
        clearForm();
        await postJson(endpoint, payload, operation);
      };
    }

    function showSmartcardConfigForm(title, endpoint, operation, fields) {
      formShell.className = 'form-shell active';
      const inputMarkup = fields.map((field) => {
        if (field.type === 'file') {
          return `<input id="${field.id}" type="file" accept="${field.accept || '*/*'}">`;
        }
        const value = field.value !== undefined ? field.value : '';
        const inputType = field.type || 'text';
        return `<input id="${field.id}" type="${inputType}" value="${value}" placeholder="${field.placeholder || field.label}">`;
      }).join('');

      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">${title}</div>
          <div class="form-grid">
            ${inputMarkup}
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelSmartcardConfigBtn">Cancel</button>
            <button class="btn primary" id="submitSmartcardConfigBtn">Send</button>
          </div>
        </div>`;

      document.getElementById('cancelSmartcardConfigBtn').onclick = clearForm;
      document.getElementById('submitSmartcardConfigBtn').onclick = async () => {
        const payload = {};
        for (const field of fields) {
          const element = document.getElementById(field.id);
          if (field.type === 'file') {
            if (!element.files.length) {
              alert(`${field.label} is required.`);
              return;
            }
            payload[field.key] = await readFileAsBase64(element.files[0]);
          } else if ((field.type || 'text') === 'number') {
            const parsed = Number(element.value);
            if (Number.isNaN(parsed)) {
              alert(`${field.label} must be a number.`);
              return;
            }
            payload[field.key] = parsed;
          } else {
            payload[field.key] = element.value;
          }
        }
        clearForm();
        await postJson(endpoint, payload, operation);
      };
    }

    function showDesfireSetForm() {
      showSmartcardConfigForm('Desfire Set', '/api/system-desfire-set', 'Smartcard Desfire Set', [
        { id: 'keyType', key: 'keyType', label: 'Key Type', type: 'number', value: 2 },
        { id: 'keySize', key: 'keySize', label: 'Key Size', type: 'number', value: 16 },
        { id: 'keyMaster', key: 'keyMaster', label: 'Key Master', value: 'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF' },
        { id: 'keyApplication', key: 'keyApplication', label: 'Key Application', value: 'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF' },
        { id: 'keyReadWrite', key: 'keyReadWrite', label: 'Key Read Write', value: 'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF' },
        { id: 'keyReadOnly', key: 'keyReadOnly', label: 'Key Read Only', value: 'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF' },
        { id: 'applicationID', key: 'applicationID', label: 'Application ID', type: 'number', value: 144 },
        { id: 'userFileType', key: 'userFileType', label: 'User File Type', type: 'number', value: 1 },
        { id: 'userFileNumber', key: 'userFileNumber', label: 'User File Number', type: 'number', value: 1 },
        { id: 'userFileSize', key: 'userFileSize', label: 'User File Size', type: 'number', value: 256 },
        { id: 'faceFileType', key: 'faceFileType', label: 'Face File Type', type: 'number', value: 1 },
        { id: 'faceFileNumber', key: 'faceFileNumber', label: 'Face File Number', type: 'number', value: 7 },
        { id: 'faceFileSize', key: 'faceFileSize', label: 'Face File Size', type: 'number', value: 640 },
        { id: 'keyMasterNumber', key: 'keyMasterNumber', label: 'Key Master Number', type: 'number', value: 0 },
        { id: 'keyApplicationNumber', key: 'keyApplicationNumber', label: 'Key Application Number', type: 'number', value: 1 },
        { id: 'keyReadWriteNumber', key: 'keyReadWriteNumber', label: 'Key Read Write Number', type: 'number', value: 2 },
        { id: 'keyReadOnlyNumber', key: 'keyReadOnlyNumber', label: 'Key Read Only Number', type: 'number', value: 3 }
      ]);
    }

    function showDesfireSecondarySetForm() {
      showSmartcardConfigForm('Desfire Secondary Set', '/api/system-desfire-secondary-set', 'Smartcard Desfire Secondary Set', [
        { id: 'keyReadWriteSecondary', key: 'keyReadWriteSecondary', label: 'Key Read Write Secondary', value: 'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF' },
        { id: 'keyReadOnlySecondary', key: 'keyReadOnlySecondary', label: 'Key Read Only Secondary', value: 'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF' },
        { id: 'keyReadWriteNumberSecondary', key: 'keyReadWriteNumberSecondary', label: 'Key Read Write Number Secondary', type: 'number', value: 2 },
        { id: 'keyReadOnlyNumberSecondary', key: 'keyReadOnlyNumberSecondary', label: 'Key Read Only Number Secondary', type: 'number', value: 3 },
        { id: 'userSecondary', key: 'userSecondary', label: 'User Secondary', type: 'number', value: 0 },
        { id: 'faceSecondary', key: 'faceSecondary', label: 'Face Secondary', type: 'number', value: 0 }
      ]);
    }

    function showMifareSetForm() {
      showSmartcardConfigForm('Mifare Set', '/api/system-mifare-set', 'Smartcard Mifare Set', [
        { id: 'keyA', key: 'keyA', label: 'Key A', value: 'A0A1A2A3A4A5' },
        { id: 'keyB', key: 'keyB', label: 'Key B', value: 'B0B1B2B3B4B5' },
        { id: 'userStart', key: 'userStart', label: 'User Start', type: 'number', value: 4 },
        { id: 'userSize', key: 'userSize', label: 'User Size', type: 'number', value: 64 },
        { id: 'userKey', key: 'userKey', label: 'User Key', type: 'number', value: 0 },
        { id: 'userFormat', key: 'userFormat', label: 'User Format', type: 'number', value: 0 },
        { id: 'faceStart', key: 'faceStart', label: 'Face Start', type: 'number', value: 16 },
        { id: 'faceSize', key: 'faceSize', label: 'Face Size', type: 'number', value: 512 },
        { id: 'faceKey', key: 'faceKey', label: 'Face Key', type: 'number', value: 1 }
      ]);
    }

    function showWiegandSetForm() {
      showSmartcardConfigForm('Wiegand Set', '/api/system-wiegand-set', 'Smartcard Wiegand Set', [
        { id: 'inputEnable', key: 'inputEnable', label: 'Input Enable', type: 'number', value: 1 },
        { id: 'outputEnable', key: 'outputEnable', label: 'Output Enable', type: 'number', value: 1 },
        { id: 'outputType', key: 'outputType', label: 'Output Type', type: 'number', value: 1 },
        { id: 'outputPulseWidth', key: 'outputPulseWidth', label: 'Output Pulse Width', type: 'number', value: 50 },
        { id: 'outputPulsePeriod', key: 'outputPulsePeriod', label: 'Output Pulse Period', type: 'number', value: 200 },
        { id: 'outputFailEnable', key: 'outputFailEnable', label: 'Output Fail Enable', type: 'number', value: 0 },
        { id: 'outputFailStartBit', key: 'outputFailStartBit', label: 'Output Fail Start Bit', type: 'number', value: 0 },
        { id: 'outputFailLength', key: 'outputFailLength', label: 'Output Fail Length', type: 'number', value: 0 },
        { id: 'outputFailCode', key: 'outputFailCode', label: 'Output Fail Code', type: 'number', value: 0 },
        { id: 'serviceEndpoint', key: 'serviceEndpoint', label: 'Service Endpoint', value: '' },
        { id: 'serviceTimeout', key: 'serviceTimeout', label: 'Service Timeout', type: 'number', value: 10 },
        { id: 'webOrTcp', key: 'webOrTCP', label: 'Web Or TCP', type: 'number', value: 0 },
        { id: 'tcpAddress', key: 'tcpAddress', label: 'TCP Address', value: '' },
        { id: 'tcpPort', key: 'tcpPort', label: 'TCP Port', type: 'number', value: 0 },
        { id: 'cardNumberEnable', key: 'cardNumberEnable', label: 'Card Number Enable', type: 'number', value: 0 },
        { id: 'cardNumberStart', key: 'cardNumberStart', label: 'Card Number Start', type: 'number', value: 0 },
        { id: 'cardNumberLength', key: 'cardNumberLength', label: 'Card Number Length', type: 'number', value: 0 },
        { id: 'identifyFailCode', key: 'identifyFailCode', label: 'Identify Fail Code', value: '' },
        { id: 'identifyFailLength', key: 'identifyFailLength', label: 'Identify Fail Length', type: 'number', value: 0 }
      ]);
    }

    function showSmartcardDetectForm() {
      showTimeoutTypeForm('Smartcard Detect', '/api/smartcard-detect', 'Smartcard Detect', 10, 0);
    }

    function showCardUidDetectForm() {
      showTypeActionForm('Card UID Detect', '/api/card-uid-detect', 'Card UID Detect', 0);
    }

    function showWiegandDetectForm() {
      showTimeoutTypeForm('Wiegand Detect', '/api/wiegand-detect', 'Wiegand Detect', 10, 0);
    }

    function showDesfireEraseForm() {
      showTimeoutTypeForm('Desfire Erase', '/api/smartcard-desfire-erase', 'Smartcard Desfire Erase', 10, 2);
    }

    function showDesfireFormatForm() {
      showTimeoutTypeForm('Desfire Format', '/api/smartcard-desfire-format', 'Smartcard Desfire Format', 10, 2);
    }

    function showDesfireWriteForm() {
      showSmartcardWriteForm('Desfire Write', '/api/smartcard-desfire-write', 'Smartcard Desfire Write', true, true, true);
    }

    function showDesfireReadForm() {
      showTimeoutTypeForm('Desfire Read', '/api/smartcard-desfire-read', 'Smartcard Desfire Read', 10, 2);
    }

    function showMifareWriteForm() {
      showSmartcardWriteForm('Mifare Write', '/api/smartcard-mifare-write', 'Smartcard Mifare Write', true, true, false);
    }

    function showMifareReadForm() {
      showTimeoutForm('Mifare Read', '/api/smartcard-mifare-read', 'Smartcard Mifare Read', 10);
    }

    function showMifareBadgeWriteForm() {
      showBadgeActionForm('Mifare Badge Write', '/api/smartcard-mifare-badge-write', 'Smartcard Mifare Badge Write', 'Send', 'badge', 'Badge');
    }

    function showMifareBadgeReadForm() {
      showTimeoutForm('Mifare Badge Read', '/api/smartcard-mifare-badge-read', 'Smartcard Mifare Badge Read', 10);
    }

    function showDesfireBadgeCreateForm() {
      showTimeoutForm('Desfire Badge Create', '/api/smartcard-desfire-badge-create', 'Smartcard Desfire Badge Create', 10);
    }

    function showDesfireBadgeWriteForm() {
      showBadgeActionForm('Desfire Badge Write', '/api/smartcard-desfire-badge-write', 'Smartcard Desfire Badge Write', 'Send', 'badge', 'Badge');
    }

    function showDesfireBadgeReadForm() {
      showTimeoutForm('Desfire Badge Read', '/api/smartcard-desfire-badge-read', 'Smartcard Desfire Badge Read', 10);
    }

    function showDesfireFaceCreateForm() {
      showTimeoutForm('Desfire Face Create', '/api/smartcard-desfire-face-create', 'Smartcard Desfire Face Create', 10);
    }

    function showDesfireFaceWriteForm() {
      showSmartcardWriteForm('Desfire Face Write', '/api/smartcard-desfire-face-write', 'Smartcard Desfire Face Write', false, true, false);
    }

    function showDesfireFaceReadForm() {
      showTimeoutForm('Desfire Face Read', '/api/smartcard-desfire-face-read', 'Smartcard Desfire Face Read', 10);
    }

    function showAskReadForm() {
      showTimeoutForm('Ask Read', '/api/smartcard-ask-read', 'Smartcard Ask Read', 10);
    }

    function showRestrictEnableForm() {
      formShell.className = 'form-shell active';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">Restrict Enable</div>
          <div class="form-grid" style="grid-template-columns: 1fr;">
            <input id="statusValue" type="text" value="1" placeholder="Status">
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelStatusBtn">Cancel</button>
            <button class="btn primary" id="submitStatusBtn">Send</button>
          </div>
        </div>`;

      document.getElementById('cancelStatusBtn').onclick = clearForm;
      document.getElementById('submitStatusBtn').onclick = async () => {
        const statusValue = Number(document.getElementById('statusValue').value);
        if (Number.isNaN(statusValue)) {
          alert('Status must be a number.');
          return;
        }
        clearForm();
        await postJson('/api/user-identify-restrict-enable', { status: statusValue }, 'User Identify Restrict Enable');
      };
    }

    function showTimeActivateForm() {
      formShell.className = 'form-shell active';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">Time Activate</div>
          <div class="form-grid">
            <input id="badgeId" type="text" placeholder="Badge ID">
            <input id="startTime" type="text" placeholder="Start Time">
            <input id="endTime" type="text" placeholder="End Time">
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelTimeBtn">Cancel</button>
            <button class="btn primary" id="submitTimeBtn">Send</button>
          </div>
        </div>`;

      document.getElementById('cancelTimeBtn').onclick = clearForm;
      document.getElementById('submitTimeBtn').onclick = async () => {
        const badgeId = document.getElementById('badgeId').value.trim();
        const startTime = document.getElementById('startTime').value.trim();
        const endTime = document.getElementById('endTime').value.trim();
        if (!badgeId || !startTime || !endTime) {
          alert('Badge ID, Start Time, and End Time are required.');
          return;
        }
        clearForm();
        await postJson('/api/user-identify-time-activate', {
          badgeID: badgeId,
          startTime,
          endTime
        }, 'User Identify Time Activate');
      };
    }

    function showIdentifyAddForm() {
      formShell.className = 'form-shell active';
      formShell.innerHTML = `
        <div class="mini-card" style="margin:0; min-height:auto;">
          <div class="mini-label">Identify Add</div>
          <div class="form-grid">
            <input id="badgeId" type="text" placeholder="Badge ID">
            <input id="faceFile" type="file" accept="*/*">
            <input id="relayActive" type="text" value="1" placeholder="Relay Active">
            <input id="relayStrike" type="text" value="0" placeholder="Relay Strike">
            <input id="wiegandActive" type="text" value="0" placeholder="Wiegand Active">
            <input id="wiegandLength" type="text" value="0" placeholder="Wiegand Length">
          </div>
          <div class="form-grid" style="grid-template-columns: 1fr;">
            <input id="wiegandData" type="text" placeholder="Wiegand Data">
          </div>
          <div class="toolbar" style="margin-top:12px;">
            <button class="btn" id="cancelIdentifyAddBtn">Cancel</button>
            <button class="btn primary" id="submitIdentifyAddBtn">Send</button>
          </div>
        </div>`;

      document.getElementById('cancelIdentifyAddBtn').onclick = clearForm;
      document.getElementById('submitIdentifyAddBtn').onclick = async () => {
        const badgeId = document.getElementById('badgeId').value.trim();
        const fileInput = document.getElementById('faceFile');
        const relayActive = Number(document.getElementById('relayActive').value);
        const relayStrike = Number(document.getElementById('relayStrike').value);
        const wiegandActive = Number(document.getElementById('wiegandActive').value);
        const wiegandLength = Number(document.getElementById('wiegandLength').value);
        const wiegandData = document.getElementById('wiegandData').value;

        if (!badgeId) {
          alert('Badge ID is required.');
          return;
        }

        if (Number.isNaN(relayActive) || Number.isNaN(relayStrike) || Number.isNaN(wiegandActive) || Number.isNaN(wiegandLength)) {
          alert('Relay, Wiegand, and length values must be numeric.');
          return;
        }

        if (!fileInput.files.length) {
          alert('Choose a face data file first.');
          return;
        }

        const faceDataBase64 = await readFileAsBase64(fileInput.files[0]);
        clearForm();
        await postJson('/api/user-identify-add', {
          badgeID: badgeId,
          faceDataBase64,
          relayActive,
          relayStrike,
          wiegandActive,
          wiegandData,
          wiegandLength
        }, 'User Identify Add');
      };
    }

    function readFileAsBase64(file) {
      return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => {
          const text = String(reader.result || '');
          const base64 = text.includes(',') ? text.split(',')[1] : text;
          resolve(base64 || '');
        };
        reader.onerror = () => reject(reader.error || new Error('Unable to read file.'));
        reader.readAsDataURL(file);
      });
    }

    async function handleAction(action) {
      clearForm();
      switch (action) {
        case 'system-check':
          await postEmpty('/api/system-check', 'System Check');
          break;
        case 'system-check-extra':
          await postEmpty('/api/system-check-extra', 'System Check Extra');
          break;
        case 'system-device-id-get':
          await postEmpty('/api/system-device-id-get', 'System Device ID Get');
          break;
        case 'system-description-get':
          await postEmpty('/api/system-description-get', 'System Description Get');
          break;
        case 'show-description-set':
          showDescriptionForm();
          break;
        case 'system-restart':
          if (confirm('System Restart will reboot the camera after it responds. Continue?')) {
            await postEmpty('/api/system-restart', 'System Restart');
          }
          break;
        case 'show-firmware-update':
          showFirmwareForm();
          break;
        case 'smartcard-desfire-set':
          showSmartcardDesfireSetForm();
          break;
        case 'smartcard-desfire-get':
          showTypeActionForm('Desfire Get', '/api/system-desfire-get', 'Smartcard Desfire Get', 0);
          break;
        case 'smartcard-desfire-secondary-set':
          showSmartcardDesfireSecondarySetForm();
          break;
        case 'smartcard-desfire-secondary-get':
          showTypeActionForm('Desfire Secondary Get', '/api/system-desfire-secondary-get', 'Smartcard Desfire Secondary Get', 0);
          break;
        case 'smartcard-mifare-set':
          showSmartcardMifareSetForm();
          break;
        case 'smartcard-mifare-get':
          showTypeActionForm('Mifare Get', '/api/system-mifare-get', 'Smartcard Mifare Get', 0);
          break;
        case 'smartcard-wiegand-set':
          showSmartcardWiegandSetForm();
          break;
        case 'smartcard-wiegand-get':
          showTypeActionForm('Wiegand Get', '/api/system-wiegand-get', 'Smartcard Wiegand Get', 0);
          break;
        case 'smartcard-detect':
          showTimeoutTypeForm('Smartcard Detect', '/api/smartcard-detect', 'Smartcard Detect');
          break;
        case 'card-uid-detect':
          showTypeActionForm('Card UID Detect', '/api/card-uid-detect', 'Card UID Detect', 0);
          break;
        case 'wiegand-detect':
          showTimeoutTypeForm('Wiegand Detect', '/api/wiegand-detect', 'Wiegand Detect');
          break;
        case 'smartcard-desfire-erase':
          showTimeoutTypeForm('Desfire Erase', '/api/smartcard-desfire-erase', 'Smartcard Desfire Erase');
          break;
        case 'smartcard-desfire-format':
          showTimeoutTypeForm('Desfire Format', '/api/smartcard-desfire-format', 'Smartcard Desfire Format');
          break;
        case 'smartcard-desfire-write':
          showSmartcardWriteForm('Desfire Write', '/api/smartcard-desfire-write', 'Smartcard Desfire Write', true, true, true);
          break;
        case 'smartcard-desfire-read':
          showTimeoutTypeForm('Desfire Read', '/api/smartcard-desfire-read', 'Smartcard Desfire Read');
          break;
        case 'smartcard-mifare-write':
          showSmartcardWriteForm('Mifare Write', '/api/smartcard-mifare-write', 'Smartcard Mifare Write', false, true, true);
          break;
        case 'smartcard-mifare-read':
          showTimeoutForm('Mifare Read', '/api/smartcard-mifare-read', 'Smartcard Mifare Read');
          break;
        case 'smartcard-mifare-badge-write':
          showBadgeActionForm('Mifare Badge Write', '/api/smartcard-mifare-badge-write', 'Smartcard Mifare Badge Write');
          break;
        case 'smartcard-mifare-badge-read':
          showTimeoutForm('Mifare Badge Read', '/api/smartcard-mifare-badge-read', 'Smartcard Mifare Badge Read');
          break;
        case 'smartcard-desfire-badge-create':
          showTimeoutForm('Desfire Badge Create', '/api/smartcard-desfire-badge-create', 'Smartcard Desfire Badge Create');
          break;
        case 'smartcard-desfire-badge-write':
          showBadgeActionForm('Desfire Badge Write', '/api/smartcard-desfire-badge-write', 'Smartcard Desfire Badge Write');
          break;
        case 'smartcard-desfire-badge-read':
          showTimeoutForm('Desfire Badge Read', '/api/smartcard-desfire-badge-read', 'Smartcard Desfire Badge Read');
          break;
        case 'smartcard-desfire-face-create':
          showTimeoutForm('Desfire Face Create', '/api/smartcard-desfire-face-create', 'Smartcard Desfire Face Create');
          break;
        case 'smartcard-desfire-face-write':
          showSmartcardWriteForm('Desfire Face Write', '/api/smartcard-desfire-face-write', 'Smartcard Desfire Face Write', false, false, true);
          break;
        case 'smartcard-desfire-face-read':
          showTimeoutForm('Desfire Face Read', '/api/smartcard-desfire-face-read', 'Smartcard Desfire Face Read');
          break;
        case 'smartcard-ask-read':
          showTimeoutForm('Ask Read', '/api/smartcard-ask-read', 'Smartcard Ask Read');
          break;
        case 'user-identify-count':
          await postEmpty('/api/user-identify-count', 'User Identify Count');
          break;
        case 'user-identify-list-all':
          await postEmpty('/api/user-identify-list-all', 'User Identify List All');
          break;
        case 'user-identify-add':
          showIdentifyAddForm();
          break;
        case 'user-identify-delete':
          showBadgeActionForm('Identify Delete', '/api/user-identify-delete', 'User Identify Delete');
          break;
        case 'user-identify-delete-all':
          showTypeActionForm('Identify Delete All', '/api/user-identify-delete-all', 'User Identify Delete All');
          break;
        case 'user-identify-list':
          showBadgeActionForm('Identify List', '/api/user-identify-list', 'User Identify List');
          break;
        case 'user-identify-check':
          showBadgeActionForm('Identify Check', '/api/user-identify-check', 'User Identify Check');
          break;
        case 'user-identify-template':
          showBadgeActionForm('Identify Template', '/api/user-identify-template', 'User Identify Template');
          break;
        case 'user-identify-activate':
          showBadgeActionForm('Identify Activate', '/api/user-identify-activate', 'User Identify Activate');
          break;
        case 'user-identify-deactivate':
          showBadgeActionForm('Identify Deactivate', '/api/user-identify-deactivate', 'User Identify Deactivate');
          break;
        case 'user-identify-activate-all':
          showTypeActionForm('Identify Activate All', '/api/user-identify-activate-all', 'User Identify Activate All');
          break;
        case 'user-identify-restrict-enable':
          showRestrictEnableForm();
          break;
        case 'user-identify-time-activate':
          showTimeActivateForm();
          break;
        case 'user-identify-time-deactivate':
          showBadgeActionForm('Identify Time Deactivate', '/api/user-identify-time-deactivate', 'User Identify Time Deactivate');
          break;
        case 'user-identify-time-deactivate-all':
          showTypeActionForm('Identify Time Deactivate All', '/api/user-identify-time-deactivate-all', 'User Identify Time Deactivate All');
          break;
        case 'user-smartcard-count':
          await postEmpty('/api/user-smartcard-count', 'User Smartcard Count');
          break;
        case 'user-smartcard-list-all':
          await postEmpty('/api/user-smartcard-list-all', 'User Smartcard List All');
          break;
        case 'user-elevator-count':
          await postEmpty('/api/user-elevator-count', 'User Elevator Count');
          break;
        case 'user-elevator-list-all':
          await postEmpty('/api/user-elevator-list-all', 'User Elevator List All');
          break;
        case 'user-restricted-count':
          await postEmpty('/api/user-restricted-count', 'User Restricted Count');
          break;
        case 'user-restricted-list-all':
          await postEmpty('/api/user-restricted-list-all', 'User Restricted List All');
          break;
        case 'user-schedule-count':
          await postEmpty('/api/user-schedule-count', 'User Schedule Count');
          break;
        case 'user-schedule-list-all':
          await postEmpty('/api/user-schedule-list-all', 'User Schedule List All');
          break;
      }
    }

    async function postEmpty(url, operation) {
      setLoading(operation);
      const response = await fetch(url, { method: 'POST' });
      await handleResponse(response, operation);
    }

    async function postJson(url, payload, operation) {
      setLoading(operation);
      const response = await fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      await handleResponse(response, operation);
    }

    async function postForm(url, formData, operation) {
      setLoading(operation);
      const response = await fetch(url, {
        method: 'POST',
        body: formData
      });
      await handleResponse(response, operation);
    }

    async function handleResponse(response, operation) {
      const text = await response.text();
      let data;
      try {
        data = text ? JSON.parse(text) : {};
      } catch {
        data = { errorMessage: text || 'Unexpected response', result: -1, rawXml: '' };
      }

      if (!response.ok) {
        const errorText = data.detail || data.error || data.title || text || `${operation} failed`;
        renderResult({
          operation,
          result: response.status,
          errorMessage: errorText,
          details: {},
          reportLabel: 'Report',
          reportText: '',
          rawXml: ''
        });
        return;
      }

      renderResult(data);
    }

    setStatus('READY', null);
  </script>
</body>
</html>
""";
}
