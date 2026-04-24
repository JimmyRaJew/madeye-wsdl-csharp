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
- `Users`
- `Logs`
- `Face`
- `Video`
- `Smartcard`

The current implemented operations are grouped below so the browser surface is documented in one place.

System core:

- `System Check`
- `System Check Extra`
- `SystemDeviceIDGet`
- `SystemDescriptionGet`
- `SystemDescriptionSet`
- `SystemRestart`
- `SystemFirmwareUpdate`

System smartcard configuration:

- `System Desfire Set`
- `System Desfire Get`
- `System Desfire Secondary Set`
- `System Desfire Secondary Get`
- `System Mifare Set`
- `System Mifare Get`
- `System Wiegand Set`
- `System Wiegand Get`
- `System Log Set`
- `System Log Get`

Logging:

- `Log Delete Event`
- `Log Get Event`
- `Log Delete App`
- `Log Get App`
- `Log Delete Sys`
- `Log Get Sys`

Face and video:

- `Face Extract`
- `Face Verify`
- `Face Identify`
- `Face Extract With Info`
- `Face Extract With Info + Rotation`
- `Face Extract Duplicate`
- `System Face Set`
- `System Face Get`
- `Video Face Capture`
- `Video Face Match`
- `System Video Set`
- `System Video Get`

Smartcard operations:

- `Smartcard Detect`
- `Smartcard Desfire Erase`
- `Smartcard Desfire Format`
- `Smartcard Desfire Write`
- `Smartcard Desfire Read`
- `Smartcard Mifare Write`
- `Smartcard Mifare Read`
- `Smartcard Mifare Badge Write`
- `Smartcard Mifare Badge Read`
- `Smartcard Desfire Badge Create`
- `Smartcard Desfire Badge Write`
- `Smartcard Desfire Badge Read`
- `Smartcard Desfire Face Create`
- `Smartcard Desfire Face Write`
- `Smartcard Desfire Face Read`
- `Smartcard Ask Read`
- `Wiegand Detect`
- `CardUIDDetect`

User management:

- `UserIdentifyCount`
- `UserIdentifyListAll`
- `UserIdentifyAdd`
- `UserIdentifyDelete`
- `UserIdentifyDeleteAll`
- `UserIdentifyList`
- `UserIdentifyCheck`
- `UserIdentifyTemplate`
- `UserIdentifyActivate`
- `UserIdentifyDeactivate`
- `UserIdentifyActivateAll`
- `UserIdentifyRestrictEnable`
- `UserIdentifyTimeActivate`
- `UserIdentifyTimeDeactivate`
- `UserIdentifyTimeDeactivateAll`
- `UserSmartcardCount`
- `UserSmartcardListAll`
- `UserSmartcardAdd`
- `UserSmartcardAddMulti`
- `UserSmartcardCheck`
- `UserSmartcardDelete`
- `UserSmartcardDeleteAll`
- `UserSmartcardList`
- `UserWiegandAdd`
- `UserWiegandAddMulti`
- `UserWiegandCheck`
- `UserWiegandCount`
- `UserWiegandDelete`
- `UserWiegandDeleteAll`
- `UserWiegandList`
- `UserWiegandListAll`
- `UserElevatorCount`
- `UserElevatorListAll`
- `UserElevatorAdd`
- `UserElevatorAddMulti`
- `UserElevatorCheck`
- `UserElevatorDelete`
- `UserElevatorDeleteAll`
- `UserRestrictedCount`
- `UserRestrictedListAll`
- `UserScheduleCount`
- `UserScheduleListAll`
- `UserIdentifyRestrictAdd`
- `UserIdentifyRestrictDelete`
- `UserIdentifyRestrictDeleteAll`
- `UserSeosAdd`
- `UserSeosCheck`
- `UserSeosCount`
- `UserSeosDelete`
- `UserSeosDeleteAll`

Latest OpenAPI v6 additions not yet surfaced in the browser UI:

Face template interop:

- `FaceTemplateExtraction`
- `FaceTemplateMatch`
- `FaceTemplateMatchWiegand`
- `FaceTemplateMatchWiegandString`

System and device settings:

- `SystemBluetoothGet`
- `SystemBluetoothSet`
- `SystemBuzzerActivate`
- `SystemBuzzerGet`
- `SystemBuzzerSet`
- `SystemCommunicationSecurityGet`
- `SystemCommunicationSecuritySet`
- `SystemDatabaseReset`
- `SystemElevatorGet`
- `SystemElevatorSet`
- `SystemExitPushButtonGet`
- `SystemExitPushButtonSet`
- `SystemFaceA64CommGet`
- `SystemFaceA64CommSet`
- `SystemFortressLedGet`
- `SystemFortressLedSet`
- `SystemFortressSerialGet`
- `SystemFortressSerialSet`
- `SystemGetAll`
- `SystemLedBrightness`
- `SystemLedRotate`
- `SystemLedSet`
- `SystemModeGet`
- `SystemModeSet`
- `SystemNTPGet`
- `SystemNTPSet`
- `SystemNetworkGet`
- `SystemNetworkMAC8021XGet`
- `SystemNetworkMAC8021XSet`
- `SystemNetworkSet`
- `SystemOSDPGet`
- `SystemOSDPSet`
- `SystemPanelFeedbackGet`
- `SystemPanelFeedbackSet`
- `SystemPassword`
- `SystemPasswordSet`
- `SystemRS485Get`
- `SystemRS485Set`
- `SystemRadarGet`
- `SystemRadarSet`
- `SystemRelayGet`
- `SystemRelaySet`
- `SystemRtmCacheClear`
- `SystemRtmGet`
- `SystemRtmSet`
- `SystemSNMPGet`
- `SystemSNMPSet`
- `SystemSSLCertificate`
- `SystemTamperGet`
- `SystemTamperSet`
- `SystemTimeGet`
- `SystemTimeSet`
- `SystemVersion`
- `SystemWebConnectivity`
- `SystemWebEnrollGet`
- `SystemWebEnrollSet`
- `SystemWifiConnectivity`
- `SystemWifiGet`
- `SystemWifiMac`
- `SystemWifiSet`

User and card administration:

- `UserBadgeWiegandAdd`
- `UserBadgeWiegandDelete`
- `UserBadgeWiegandDeleteAll`
- `UserBadgeWiegandListAll`
- `UserDatabaseGet`
- `UserDatabaseSet`
- `UserIdentifyAddMulti`
- `UserImage`

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

System and maintenance:

- `POST /api/system-check`
- `POST /api/system-check-extra`
- `POST /api/system-device-id-get`
- `POST /api/system-description-get`
- `POST /api/system-description-set`
- `POST /api/system-restart`
- `POST /api/system-firmware-update`
- `POST /api/system-desfire-set`
- `POST /api/system-desfire-get`
- `POST /api/system-desfire-secondary-set`
- `POST /api/system-desfire-secondary-get`
- `POST /api/system-mifare-set`
- `POST /api/system-mifare-get`
- `POST /api/system-wiegand-set`
- `POST /api/system-wiegand-get`
- `POST /api/system-log-set`
- `POST /api/system-log-get`

Logging:

- `POST /api/log-delete-event`
- `POST /api/log-get-event`
- `POST /api/log-delete-app`
- `POST /api/log-get-app`
- `POST /api/log-delete-sys`
- `POST /api/log-get-sys`

Face:

- `POST /api/system-face-set`
- `POST /api/system-face-get`
- `POST /api/face-extract`
- `POST /api/face-verify`
- `POST /api/face-identify`
- `POST /api/face-extract-with-info`
- `POST /api/face-extract-with-info-and-rotation`
- `POST /api/face-extract-duplicate`

Video:

- `POST /api/system-video-set`
- `POST /api/system-video-get`
- `POST /api/video-face-capture`
- `POST /api/video-face-match`

Smartcard and card-detection:

- `POST /api/smartcard-detect`
- `POST /api/smartcard-desfire-erase`
- `POST /api/smartcard-desfire-format`
- `POST /api/smartcard-desfire-write`
- `POST /api/smartcard-desfire-read`
- `POST /api/smartcard-mifare-write`
- `POST /api/smartcard-mifare-read`
- `POST /api/smartcard-mifare-badge-write`
- `POST /api/smartcard-mifare-badge-read`
- `POST /api/smartcard-desfire-badge-create`
- `POST /api/smartcard-desfire-badge-write`
- `POST /api/smartcard-desfire-badge-read`
- `POST /api/smartcard-desfire-face-create`
- `POST /api/smartcard-desfire-face-write`
- `POST /api/smartcard-desfire-face-read`
- `POST /api/smartcard-ask-read`
- `POST /api/wiegand-detect`
- `POST /api/card-uid-detect`

User management:

- `POST /api/user-identify-count`
- `POST /api/user-identify-list-all`
- `POST /api/user-identify-add`
- `POST /api/user-identify-delete`
- `POST /api/user-identify-delete-all`
- `POST /api/user-identify-list`
- `POST /api/user-identify-check`
- `POST /api/user-identify-template`
- `POST /api/user-identify-restrict-enable`
- `POST /api/user-identify-activate`
- `POST /api/user-identify-deactivate`
- `POST /api/user-identify-activate-all`
- `POST /api/user-identify-time-activate`
- `POST /api/user-identify-time-deactivate`
- `POST /api/user-identify-time-deactivate-all`
- `POST /api/user-smartcard-count`
- `POST /api/user-smartcard-list-all`
- `POST /api/user-smartcard-add`
- `POST /api/user-smartcard-add-multi`
- `POST /api/user-smartcard-check`
- `POST /api/user-smartcard-delete`
- `POST /api/user-smartcard-delete-all`
- `POST /api/user-smartcard-list`
- `POST /api/user-wiegand-add`
- `POST /api/user-wiegand-add-multi`
- `POST /api/user-wiegand-check`
- `POST /api/user-wiegand-count`
- `POST /api/user-wiegand-delete`
- `POST /api/user-wiegand-delete-all`
- `POST /api/user-wiegand-list`
- `POST /api/user-wiegand-list-all`
- `POST /api/user-elevator-count`
- `POST /api/user-elevator-list-all`
- `POST /api/user-elevator-add`
- `POST /api/user-elevator-add-multi`
- `POST /api/user-elevator-check`
- `POST /api/user-elevator-delete`
- `POST /api/user-elevator-delete-all`
- `POST /api/user-restricted-count`
- `POST /api/user-restricted-list-all`
- `POST /api/user-schedule-count`
- `POST /api/user-schedule-list-all`
- `POST /api/user-identify-restrict-add`
- `POST /api/user-identify-restrict-delete`
- `POST /api/user-identify-restrict-delete-all`
- `POST /api/user-seos-add`
- `POST /api/user-seos-check`
- `POST /api/user-seos-count`
- `POST /api/user-seos-delete`
- `POST /api/user-seos-delete-all`

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

The current Users section now includes both overview and identify-management actions:

- count operations use `Type=1`
- list-all operations also use `Type=1`
- identify add accepts a badge ID plus base64 face data and relay/wiegand settings
- identify delete, check, activate, deactivate, and template operations use `BadgeID`
- identify delete-all, activate-all, and time-deactivate-all use `Type`
- identify time-activate uses `BadgeID`, `StartTime`, and `EndTime`
- identify restrict-enable uses `Status`

Other common request shapes are:

- `Timeout` and `Type` for card-detection and smartcard-maintenance actions
- `UserData`, `FaceData`, and `Badge` for smartcard read/write actions
- `ImageData` for face extraction and identify calls
- `CurrentPassword` and `Password` for password changes
- `Enable`, `Mode`, `Status`, `WithSSL`, and similar toggles for system settings
- `StartTime` and `EndTime` for time-based access windows

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
