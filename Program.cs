using MadeyeWsdlCSharp;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5080");
builder.Services.AddSingleton<VisionA64Client>();

var app = builder.Build();

app.MapGet("/", () => Results.Content(WebUi.IndexHtml, "text/html; charset=utf-8"));
app.MapGet("/api/health", () => Results.Ok(new { status = "ok" }));

app.MapPost("/api/system-check", ExecuteSystemCheck);
app.MapPost("/api/system-check-extra", ExecuteSystemCheckExtra);
app.MapPost("/api/system-device-id-get", ExecuteSystemDeviceIdGet);
app.MapPost("/api/system-description-get", ExecuteSystemDescriptionGet);
app.MapPost("/api/system-description-set", ExecuteSystemDescriptionSet);
app.MapPost("/api/system-restart", ExecuteSystemRestart);
app.MapPost("/api/system-firmware-update", ExecuteSystemFirmwareUpdate);
app.MapPost("/api/system-desfire-set", ExecuteSystemDesfireSet);
app.MapPost("/api/system-desfire-get", ExecuteSystemDesfireGet);
app.MapPost("/api/system-desfire-secondary-set", ExecuteSystemDesfireSecondarySet);
app.MapPost("/api/system-desfire-secondary-get", ExecuteSystemDesfireSecondaryGet);
app.MapPost("/api/system-mifare-set", ExecuteSystemMifareSet);
app.MapPost("/api/system-mifare-get", ExecuteSystemMifareGet);
app.MapPost("/api/system-wiegand-set", ExecuteSystemWiegandSet);
app.MapPost("/api/system-wiegand-get", ExecuteSystemWiegandGet);
app.MapPost("/api/smartcard-detect", ExecuteSmartcardDetect);
app.MapPost("/api/smartcard-desfire-erase", ExecuteSmartcardDesfireErase);
app.MapPost("/api/smartcard-desfire-format", ExecuteSmartcardDesfireFormat);
app.MapPost("/api/smartcard-desfire-write", ExecuteSmartcardDesfireWrite);
app.MapPost("/api/smartcard-desfire-read", ExecuteSmartcardDesfireRead);
app.MapPost("/api/smartcard-mifare-write", ExecuteSmartcardMifareWrite);
app.MapPost("/api/smartcard-mifare-read", ExecuteSmartcardMifareRead);
app.MapPost("/api/smartcard-mifare-badge-write", ExecuteSmartcardMifareBadgeWrite);
app.MapPost("/api/smartcard-mifare-badge-read", ExecuteSmartcardMifareBadgeRead);
app.MapPost("/api/smartcard-desfire-badge-create", ExecuteSmartcardDesfireBadgeCreate);
app.MapPost("/api/smartcard-desfire-badge-write", ExecuteSmartcardDesfireBadgeWrite);
app.MapPost("/api/smartcard-desfire-badge-read", ExecuteSmartcardDesfireBadgeRead);
app.MapPost("/api/smartcard-desfire-face-create", ExecuteSmartcardDesfireFaceCreate);
app.MapPost("/api/smartcard-desfire-face-write", ExecuteSmartcardDesfireFaceWrite);
app.MapPost("/api/smartcard-desfire-face-read", ExecuteSmartcardDesfireFaceRead);
app.MapPost("/api/smartcard-ask-read", ExecuteSmartcardAskRead);
app.MapPost("/api/wiegand-detect", ExecuteWiegandDetect);
app.MapPost("/api/card-uid-detect", ExecuteCardUidDetect);
app.MapPost("/api/user-identify-count", ExecuteUserIdentifyCount);
app.MapPost("/api/user-identify-list-all", ExecuteUserIdentifyListAll);
app.MapPost("/api/user-identify-add", ExecuteUserIdentifyAdd);
app.MapPost("/api/user-identify-delete", ExecuteUserIdentifyDelete);
app.MapPost("/api/user-identify-delete-all", ExecuteUserIdentifyDeleteAll);
app.MapPost("/api/user-identify-list", ExecuteUserIdentifyList);
app.MapPost("/api/user-identify-check", ExecuteUserIdentifyCheck);
app.MapPost("/api/user-identify-template", ExecuteUserIdentifyTemplate);
app.MapPost("/api/user-identify-restrict-enable", ExecuteUserIdentifyRestrictEnable);
app.MapPost("/api/user-identify-activate", ExecuteUserIdentifyActivate);
app.MapPost("/api/user-identify-deactivate", ExecuteUserIdentifyDeactivate);
app.MapPost("/api/user-identify-activate-all", ExecuteUserIdentifyActivateAll);
app.MapPost("/api/user-identify-time-activate", ExecuteUserIdentifyTimeActivate);
app.MapPost("/api/user-identify-time-deactivate", ExecuteUserIdentifyTimeDeactivate);
app.MapPost("/api/user-identify-time-deactivate-all", ExecuteUserIdentifyTimeDeactivateAll);
app.MapPost("/api/user-smartcard-count", ExecuteUserSmartcardCount);
app.MapPost("/api/user-smartcard-list-all", ExecuteUserSmartcardListAll);
app.MapPost("/api/user-elevator-count", ExecuteUserElevatorCount);
app.MapPost("/api/user-elevator-list-all", ExecuteUserElevatorListAll);
app.MapPost("/api/user-restricted-count", ExecuteUserRestrictedCount);
app.MapPost("/api/user-restricted-list-all", ExecuteUserRestrictedListAll);
app.MapPost("/api/user-schedule-count", ExecuteUserScheduleCount);
app.MapPost("/api/user-schedule-list-all", ExecuteUserScheduleListAll);

app.Run();

static async Task<IResult> ExecuteSystemCheck(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemCheckAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteSystemCheckExtra(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemCheckExtraAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteSystemDeviceIdGet(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemDeviceIdGetAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteSystemDescriptionGet(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemDescriptionGetAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteSystemDescriptionSet(VisionA64Client client, DescriptionSetRequest request, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(request.Label1) &&
        string.IsNullOrWhiteSpace(request.Label2) &&
        string.IsNullOrWhiteSpace(request.Label3))
    {
        return Results.BadRequest(new { error = "At least one label must be provided." });
    }

    return await ExecuteSoap(client, cancellationToken, c => c.SystemDescriptionSetAsync(
        request.Label1 ?? string.Empty,
        request.Label2 ?? string.Empty,
        request.Label3 ?? string.Empty,
        cancellationToken));
}

static async Task<IResult> ExecuteSystemRestart(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemRestartAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteSystemFirmwareUpdate(VisionA64Client client, HttpRequest request, CancellationToken cancellationToken)
{
    if (!request.HasFormContentType)
    {
        return Results.BadRequest(new { error = "Multipart form-data is required." });
    }

    var form = await request.ReadFormAsync(cancellationToken);
    var file = form.Files.GetFile("file");
    var md5 = form["md5"].ToString().Trim();

    if (file is null)
    {
        return Results.BadRequest(new { error = "Firmware ZIP file is required." });
    }

    if (string.IsNullOrWhiteSpace(md5) || md5.Length < 32)
    {
        return Results.BadRequest(new { error = "MD5 value must contain at least 32 characters." });
    }

    byte[] fileData;
    await using (var stream = file.OpenReadStream())
    {
        using var memory = new MemoryStream();
        await stream.CopyToAsync(memory, cancellationToken);
        fileData = memory.ToArray();
    }

    return await ExecuteSoap(client, cancellationToken, c => c.SystemFirmwareUpdateAsync(fileData, md5, cancellationToken));
}

static async Task<IResult> ExecuteSystemDesfireSet(VisionA64Client client, SystemDesfireSetRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemDesfireSetAsync(request, cancellationToken));
}

static async Task<IResult> ExecuteSystemDesfireGet(VisionA64Client client, SmartcardTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemDesfireGetAsync(request.Type, cancellationToken));
}

static async Task<IResult> ExecuteSystemDesfireSecondarySet(VisionA64Client client, SystemDesfireSecondarySetRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemDesfireSecondarySetAsync(request, cancellationToken));
}

static async Task<IResult> ExecuteSystemDesfireSecondaryGet(VisionA64Client client, SmartcardTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemDesfireSecondaryGetAsync(request.Type, cancellationToken));
}

static async Task<IResult> ExecuteSystemMifareSet(VisionA64Client client, SystemMifareSetRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemMifareSetAsync(request, cancellationToken));
}

static async Task<IResult> ExecuteSystemMifareGet(VisionA64Client client, SmartcardTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemMifareGetAsync(request.Type, cancellationToken));
}

static async Task<IResult> ExecuteSystemWiegandSet(VisionA64Client client, SystemWiegandSetRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemWiegandSetAsync(request, cancellationToken));
}

static async Task<IResult> ExecuteSystemWiegandGet(VisionA64Client client, SmartcardTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SystemWiegandGetAsync(request.Type, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDetect(VisionA64Client client, SmartcardTimeoutTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDetectAsync(request.Timeout, request.Type, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDesfireErase(VisionA64Client client, SmartcardTimeoutTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDesfireEraseAsync(request.Timeout, request.Type, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDesfireFormat(VisionA64Client client, SmartcardTimeoutTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDesfireFormatAsync(request.Timeout, request.Type, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDesfireWrite(VisionA64Client client, SmartcardDesfireWriteRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDesfireWriteAsync(request.Timeout, request.Type, request.UserData, request.FaceDataBase64, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDesfireRead(VisionA64Client client, SmartcardTimeoutTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDesfireReadAsync(request.Timeout, request.Type, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardMifareWrite(VisionA64Client client, SmartcardMifareWriteRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardMifareWriteAsync(request.Timeout, request.UserData, request.FaceDataBase64, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardMifareRead(VisionA64Client client, SmartcardTimeoutRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardMifareReadAsync(request.Timeout, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardMifareBadgeWrite(VisionA64Client client, SmartcardBadgeWriteRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardMifareBadgeWriteAsync(request.Timeout, request.Badge, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardMifareBadgeRead(VisionA64Client client, SmartcardTimeoutRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardMifareBadgeReadAsync(request.Timeout, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDesfireBadgeCreate(VisionA64Client client, SmartcardTimeoutRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDesfireBadgeCreateAsync(request.Timeout, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDesfireBadgeWrite(VisionA64Client client, SmartcardBadgeWriteRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDesfireBadgeWriteAsync(request.Timeout, request.Badge, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDesfireBadgeRead(VisionA64Client client, SmartcardTimeoutRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDesfireBadgeReadAsync(request.Timeout, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDesfireFaceCreate(VisionA64Client client, SmartcardTimeoutRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDesfireFaceCreateAsync(request.Timeout, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDesfireFaceWrite(VisionA64Client client, SmartcardFaceWriteRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDesfireFaceWriteAsync(request.Timeout, request.FaceDataBase64, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardDesfireFaceRead(VisionA64Client client, SmartcardTimeoutRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardDesfireFaceReadAsync(request.Timeout, cancellationToken));
}

static async Task<IResult> ExecuteSmartcardAskRead(VisionA64Client client, SmartcardTimeoutRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.SmartcardAskReadAsync(request.Timeout, cancellationToken));
}

static async Task<IResult> ExecuteWiegandDetect(VisionA64Client client, SmartcardTimeoutTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.WiegandDetectAsync(request.Timeout, request.Type, cancellationToken));
}

static async Task<IResult> ExecuteCardUidDetect(VisionA64Client client, SmartcardTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.CardUidDetectAsync(request.Type, cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyCount(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyCountAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyListAll(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyListAllAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyAdd(VisionA64Client client, UserIdentifyAddRequest request, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(request.BadgeID))
    {
        return Results.BadRequest(new { error = "BadgeID is required." });
    }

    if (string.IsNullOrWhiteSpace(request.FaceDataBase64))
    {
        return Results.BadRequest(new { error = "Face data is required." });
    }

    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyAddAsync(
        request.BadgeID.Trim(),
        request.FaceDataBase64.Trim(),
        request.RelayActive,
        request.RelayStrike,
        request.WiegandActive,
        request.WiegandData ?? string.Empty,
        request.WiegandLength,
        cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyDelete(VisionA64Client client, UserBadgeRequest request, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(request.BadgeID))
    {
        return Results.BadRequest(new { error = "BadgeID is required." });
    }

    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyDeleteAsync(request.BadgeID.Trim(), cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyDeleteAll(VisionA64Client client, UserTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyDeleteAllAsync(request.Type, cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyList(VisionA64Client client, UserBadgeRequest request, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(request.BadgeID))
    {
        return Results.BadRequest(new { error = "BadgeID is required." });
    }

    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyListAsync(request.BadgeID.Trim(), cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyCheck(VisionA64Client client, UserBadgeRequest request, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(request.BadgeID))
    {
        return Results.BadRequest(new { error = "BadgeID is required." });
    }

    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyCheckAsync(request.BadgeID.Trim(), cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyTemplate(VisionA64Client client, UserBadgeRequest request, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(request.BadgeID))
    {
        return Results.BadRequest(new { error = "BadgeID is required." });
    }

    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyTemplateAsync(request.BadgeID.Trim(), cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyRestrictEnable(VisionA64Client client, UserIdentifyRestrictEnableRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyRestrictEnableAsync(request.Status, cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyActivate(VisionA64Client client, UserBadgeRequest request, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(request.BadgeID))
    {
        return Results.BadRequest(new { error = "BadgeID is required." });
    }

    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyActivateAsync(request.BadgeID.Trim(), cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyDeactivate(VisionA64Client client, UserBadgeRequest request, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(request.BadgeID))
    {
        return Results.BadRequest(new { error = "BadgeID is required." });
    }

    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyDeactivateAsync(request.BadgeID.Trim(), cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyActivateAll(VisionA64Client client, UserTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyActivateAllAsync(request.Type, cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyTimeActivate(VisionA64Client client, UserIdentifyTimeActivateRequest request, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(request.BadgeID) ||
        string.IsNullOrWhiteSpace(request.StartTime) ||
        string.IsNullOrWhiteSpace(request.EndTime))
    {
        return Results.BadRequest(new { error = "BadgeID, StartTime, and EndTime are required." });
    }

    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyTimeActivateAsync(
        request.BadgeID.Trim(),
        request.StartTime.Trim(),
        request.EndTime.Trim(),
        cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyTimeDeactivate(VisionA64Client client, UserBadgeRequest request, CancellationToken cancellationToken)
{
    if (string.IsNullOrWhiteSpace(request.BadgeID))
    {
        return Results.BadRequest(new { error = "BadgeID is required." });
    }

    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyTimeDeactivateAsync(request.BadgeID.Trim(), cancellationToken));
}

static async Task<IResult> ExecuteUserIdentifyTimeDeactivateAll(VisionA64Client client, UserTypeRequest request, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserIdentifyTimeDeactivateAllAsync(request.Type, cancellationToken));
}

static async Task<IResult> ExecuteUserSmartcardCount(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserSmartcardCountAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteUserSmartcardListAll(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserSmartcardListAllAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteUserElevatorCount(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserElevatorCountAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteUserElevatorListAll(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserElevatorListAllAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteUserRestrictedCount(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserRestrictedCountAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteUserRestrictedListAll(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserRestrictedListAllAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteUserScheduleCount(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserScheduleCountAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteUserScheduleListAll(VisionA64Client client, CancellationToken cancellationToken)
{
    return await ExecuteSoap(client, cancellationToken, c => c.UserScheduleListAllAsync(1, cancellationToken));
}

static async Task<IResult> ExecuteSoap(
    VisionA64Client client,
    CancellationToken cancellationToken,
    Func<VisionA64Client, Task<SoapResult>> operation)
{
    try
    {
        SoapResult result = await operation(client).ConfigureAwait(false);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
}
