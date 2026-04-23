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
      margin-bottom: 18px;
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
      gap: 12px;
      margin-bottom: 14px;
    }

    .mini-card {
      background: var(--surface-soft);
      border: 1px solid var(--border);
      border-radius: 18px;
      padding: 16px;
      min-height: 128px;
    }

    .mini-label {
      color: var(--muted);
      font-size: 13px;
      margin-bottom: 16px;
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
      padding: 16px;
      margin-bottom: 14px;
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
      margin-bottom: 14px;
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
      margin-bottom: 14px;
    }

    .form-shell.active {
      display: grid;
    }

    .form-grid {
      display: grid;
      gap: 10px;
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

    .menu-drawer {
      position: fixed;
      left: 18px;
      top: 70px;
      width: 300px;
      max-width: calc(100vw - 36px);
      background: #fff;
      border: 1px solid var(--border);
      border-radius: 20px;
      box-shadow: var(--shadow);
      padding: 14px;
      display: none;
      z-index: 20;
    }

    .menu-drawer.open { display: block; }

    .menu-section + .menu-section { margin-top: 12px; }

    .menu-heading {
      color: var(--muted);
      font-size: 12px;
      font-weight: 800;
      margin: 2px 6px 8px;
      text-transform: uppercase;
      letter-spacing: 0.03em;
    }

    .menu-item {
      width: 100%;
      text-align: left;
      display: block;
      border: 1px solid var(--border);
      background: #fff;
      padding: 11px 12px;
      border-radius: 12px;
      margin-bottom: 8px;
      cursor: pointer;
      font: inherit;
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
    <div class="menu-drawer" id="drawer">
      <div class="menu-section">
        <div class="menu-heading">Quick Checks</div>
        <button class="menu-item" data-action="system-check">System Check</button>
        <button class="menu-item" data-action="system-check-extra">System Check Extra</button>
      </div>
      <div class="menu-section">
        <div class="menu-heading">System Settings</div>
        <button class="menu-item" data-action="system-device-id-get">Device ID Get</button>
        <button class="menu-item" data-action="system-description-get">Description Get</button>
        <button class="menu-item" data-action="show-description-set">Description Set</button>
      </div>
      <div class="menu-section">
        <div class="menu-heading">Maintenance</div>
        <button class="menu-item" data-action="system-restart">System Restart</button>
        <button class="menu-item" data-action="show-firmware-update">Firmware Update</button>
      </div>
    </div>

    <div class="topbar">
      <div class="brand">
        <button class="menu-btn" id="menuBtn">☰</button>
        <div>
          <h1>VisionA64 Web Console</h1>
          <div class="subtitle">Browser UI for the VisionA64 camera SOAP service</div>
        </div>
      </div>
      <div class="endpoint">Device endpoint 192.168.18.244:8080</div>
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
    const drawer = document.getElementById('drawer');
    const menuBtn = document.getElementById('menuBtn');
    const formShell = document.getElementById('formShell');
    const operationLabel = document.getElementById('operationLabel');
    const resultValue = document.getElementById('resultValue');
    const errorValue = document.getElementById('errorValue');
    const details = document.getElementById('details');
    const report = document.getElementById('report');
    const rawXml = document.getElementById('rawXml');
    const statusChip = document.getElementById('statusChip');
    const statusText = document.getElementById('statusText');

    menuBtn.addEventListener('click', () => {
      drawer.classList.toggle('open');
    });

    document.addEventListener('click', (event) => {
      if (!drawer.contains(event.target) && event.target !== menuBtn && !menuBtn.contains(event.target)) {
        drawer.classList.remove('open');
      }
    });

    drawer.querySelectorAll('[data-action]').forEach((button) => {
      button.addEventListener('click', async () => {
        const action = button.dataset.action;
        drawer.classList.remove('open');
        await handleAction(action);
      });
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
