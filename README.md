# Madeye WSDL C# Web Console

This repo contains the C# web client for the VisionA64 camera.

It is the browser-based version of the same SOAP integration we built in Java:

- a clean white UI
- a top-left burger menu
- grouped camera actions
- structured SOAP responses rendered in the browser

## Current State

The app is now an ASP.NET Core web app served locally at:

```text
http://localhost:5080
```

Open that URL in a browser after running the app.

The current menu groups are:

- `Quick Checks`
- `System Settings`
- `Maintenance`

The current implemented actions are:

- `System Check`
- `System Check Extra`
- `SystemDeviceIDGet`
- `SystemDescriptionGet`
- `SystemDescriptionSet`
- `SystemRestart`
- `SystemFirmwareUpdate`

## Camera Endpoint

The app currently targets:

```text
http://192.168.18.244:8080/
```

That endpoint is hard-coded in [VisionA64Client.cs](VisionA64Client.cs).

If the camera IP changes, update it there first.

## Requirements

- .NET 8 SDK
- Network access to the camera

## Build And Run

From the repo folder:

```bash
dotnet build
dotnet run
```

Then open:

```text
http://localhost:5080
```

## How The App Is Structured

Keep the responsibilities split this way:

- `Program.cs` hosts the web server and API routes.
- `WebUi.cs` contains the browser HTML, CSS, and client-side JavaScript.
- `VisionA64Client.cs` owns SOAP request construction, HTTP calls, and XML parsing.
- `SoapResult.cs` carries parsed camera responses back to the browser UI.

This split matters. Add new WSDL operations in the SOAP client first, then wire them into the browser menu and forms.

## API Routes

Current routes:

- `GET /` returns the browser UI
- `GET /api/health` returns a simple status object
- `POST /api/system-check`
- `POST /api/system-check-extra`
- `POST /api/system-device-id-get`
- `POST /api/system-description-get`
- `POST /api/system-description-set`
- `POST /api/system-restart`
- `POST /api/system-firmware-update`

The browser UI calls those routes with `fetch`.

## SOAP Notes

The client currently sends:

- SOAP 1.1 envelopes
- `POST` requests
- `Content-Type: text/xml; charset=utf-8`
- `SOAPAction` headers matching the WSDL action name

Read-style actions send `Type=1`.

`SystemDescriptionSet` sends the three label fields.

`SystemFirmwareUpdate` sends the ZIP payload as base64 plus the provided MD5 string.

## Extending The App

When you add another WSDL function:

1. Add a method to `VisionA64Client`.
2. Add an API route in `Program.cs`.
3. Add a menu item or form in `WebUi.cs`.
4. Update this README with the request and response details.

Keep the browser labels short and the SOAP method names exact.

## Browser UI Notes

The browser UI currently includes:

- a burger menu in the top-left
- grouped action buttons
- a dynamic form area for the actions that need input
- result code, error message, detail rows, report text, and raw XML

## Troubleshooting

### The browser page does not load

Check that the app is still running and that you are visiting:

```text
http://localhost:5080
```

### Camera request fails

Check:

- the camera is reachable on the network
- port `8080` is open
- the endpoint in `VisionA64Client` matches the device
- the SOAP action name matches the WSDL exactly

### API returns an error

That usually means:

- the request shape does not match the WSDL
- the camera returned a SOAP fault
- the device is offline or rebooting

## Notes For Future Developers

- This repo is the C# web reference implementation.
- Keep SOAP integration logic out of the browser UI.
- Add one WSDL operation at a time and update the README as you go.
- The browser UI is intentionally simple so it can grow into a fuller operator console later.
