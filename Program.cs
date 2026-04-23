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
