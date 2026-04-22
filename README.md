# Madeye WSDL C# Console

This is the C# version of the VisionA64 camera console app.

It mirrors the Java app:

- `System Check`
- `System Check Extra`
- a small desktop UI
- SOAP calls to the VisionA64 camera

## Requirements

- .NET 8 SDK
- Windows desktop support for WinForms
- Network access to the camera

## Current Camera Endpoint

The app currently targets:

```text
http://192.168.18.244:8080/
```

## Build And Run

On Windows:

```bash
dotnet build
dotnet run
```

If you are building on a non-Windows machine, you may need:

```bash
dotnet build -p:EnableWindowsTargeting=true
```

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
MainForm.cs
VisionA64Client.cs
SoapResult.cs
MadeyeWsdlCSharp.csproj
visionA64.wsdl
```

## Notes For Future Development

The next steps are the same as the Java app:

1. add one WSDL operation at a time
2. keep request/response handling in the client layer
3. keep screen rendering in the form layer
4. document each camera action as it is added

