# Madeye WSDL C# Console

This is the C# counterpart to the Java VisionA64 console.

It is meant to be a clean, maintainable desktop SOAP client for the camera, with the same long-term goal as the Java app:

- easy to extend one WSDL operation at a time
- clear separation between SOAP logic and UI
- a handoff-friendly README for the next developer

## Current State

The app currently uses Avalonia and a white desktop layout with:

- a top-left burger menu
- a compact response dashboard
- a clean card-based results view

Current implemented actions:

- `System Check`
- `System Check Extra`
- `SystemDeviceIDGet`
- `SystemDescriptionGet`
- `SystemDescriptionSet`
- `SystemRestart`
- `SystemFirmwareUpdate`

The menu is split into:

- `Quick Checks`
- `System Settings`
- `Maintenance`

The rest of the larger WSDL surface area will be added later in the same pattern as the Java version.

## Camera Endpoint

The app currently targets:

```text
http://192.168.18.244:8080/
```

That endpoint is hard-coded in:

- [VisionA64Client.cs](VisionA64Client.cs)

If the camera IP changes, update the endpoint there first.

## Requirements

- .NET 8 SDK
- Network access to the camera

## Build And Run

From the repo folder:

```bash
dotnet build
dotnet run
```

The project is cross-platform because the UI uses Avalonia. It should run on macOS and Windows as long as the .NET 8 desktop dependencies are available.

## Project Layout

```text
Program.cs           # App startup
App.cs               # Avalonia application bootstrap
MainWindow.cs        # UI, menu, and response rendering
VisionA64Client.cs   # SOAP client and XML parsing
SoapResult.cs        # Parsed response model
MadeyeWsdlCSharp.csproj
visionA64.wsdl       # Captured WSDL for reference
README.md            # This handoff guide
```

## How The App Is Structured

Keep these responsibilities separate:

- `MainWindow` owns the UI, menu, dialogs, and status display.
- `VisionA64Client` owns SOAP request construction, HTTP calls, and XML parsing.
- `SoapResult` carries the parsed camera response back to the UI.

This split is important when the app grows. Add new WSDL actions in the client first, then wire them into the menu.

## SOAP Notes

The client currently sends:

- SOAP 1.1 envelopes
- `POST` requests
- `Content-Type: text/xml; charset=utf-8`
- `SOAPAction` headers matching the WSDL action name

The existing read-style actions send `Type=1`.
`SystemDescriptionSet` sends the three label fields.
`SystemFirmwareUpdate` sends the ZIP payload as base64 plus the provided MD5 string.

## Extending The App

When you add another WSDL function:

1. Add a method to `VisionA64Client`.
2. Parse the response into `SoapResult` or another small model if needed.
3. Add the action to the menu in `MainWindow`.
4. Update this README with the request and response details.

Keep menu labels short and use WSDL exact names in the client layer.

## Troubleshooting

### `dotnet run` starts but no window appears

Check that the app is still running in the terminal and that the window is not behind another desktop or minimized.

### Build fails on macOS or Windows

Make sure the .NET 8 SDK is installed and restore can reach NuGet.

### Camera request fails

Check:

- the camera is reachable on the network
- port `8080` is open
- the endpoint in `VisionA64Client` matches the device
- the SOAP action name matches the WSDL exactly

### Response fields are missing

That usually means the device returned a fault, an unexpected payload, or a response shape that needs a parser update.

## Notes For Future Developers

- This repo is the C# reference implementation.
- The UI is intentionally minimal while we are adding the first WSDL actions.
- Keep SOAP integration logic out of the UI layer.
- Update this README as soon as you add a new operation so the handoff stays useful.
