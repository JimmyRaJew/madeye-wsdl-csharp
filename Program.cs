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
