# Madeye WSDL C# Console

This is the C# version of the VisionA64 camera console app.

It mirrors the Java app:

- `System Check`
- `System Check Extra`
- a small desktop UI
- SOAP calls to the VisionA64 camera

The UI is now built with **Avalonia**, so the same project can run on macOS and Windows.

## Requirements

- .NET 8 SDK
- Network access to the camera

## Current Camera Endpoint

The app currently targets:

```text
http://192.168.18.244:8080/
```

## Build And Run

From the repo folder:

```bash
dotnet build
dotnet run
```

This works on macOS and Windows as long as the Avalonia dependencies can be restored.

## What The App Does

The UI shows:

- result code
- error message
- structured detail fields
- multiline report text

The SOAP client sends `Type=1` for the two current actions.

## Project Files

```text
Program.cs
App.cs
MainWindow.cs
VisionA64Client.cs
SoapResult.cs
MadeyeWsdlCSharp.csproj
visionA64.wsdl
```

## Notes For Future Development

The next steps are the same as the Java app:

1. add one WSDL operation at a time
2. keep request/response handling in the client layer
3. keep screen rendering in the UI layer
4. document each camera action as it is added
