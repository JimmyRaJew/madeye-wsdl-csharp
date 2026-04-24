using System.Net.Http.Headers;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace MadeyeWsdlCSharp;

internal sealed class VisionA64Client
{
    private static readonly Uri Endpoint = new("http://192.168.18.244:8080/");
    private const string SoapNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
    private const string TargetNamespace = "http://tempuri.org/";

    private readonly HttpClient _client;

    public VisionA64Client()
    {
        _client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };
    }

    public Task<SoapResult> SystemCheckAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemCheck",
            "SystemCheckRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Report",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> SystemCheckExtraAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemCheckExtra",
            "SystemCheckExtraRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "CheckReport",
            new[]
            {
                "VersionReport",
                "ModeReport",
                "MACReport",
                "RTMReport",
                "UserCountReport",
                "LicenseCountReport"
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemDeviceIdGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "SystemDeviceIDGet",
            "SystemDeviceIDGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "DeviceID",
            "DeviceID",
            cancellationToken);
    }

    public Task<SoapResult> SystemDescriptionGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeDescriptionGetAsync(type, cancellationToken);
    }

    public Task<SoapResult> SystemDescriptionSetAsync(string label1, string label2, string label3, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemDescriptionSet",
            "SystemDescriptionSetRequest",
            new Dictionary<string, string>
            {
                ["Label1"] = label1,
                ["Label2"] = label2,
                ["Label3"] = label3
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemRestartAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemRestart",
            "SystemRestartRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> SystemFirmwareUpdateAsync(byte[] fileData, string fileMd5Sum, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemFirmwareUpdate",
            "SystemFirmwareUpdateRequest",
            new Dictionary<string, string>
            {
                ["FileData"] = Convert.ToBase64String(fileData),
                ["FileMD5Sum"] = fileMd5Sum
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemDesfireSetAsync(SystemDesfireSetRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemDesfireSet",
            "SystemDesfireSetRequest",
            new Dictionary<string, string>
            {
                ["KeyType"] = request.KeyType.ToString(),
                ["KeySize"] = request.KeySize.ToString(),
                ["KeyMaster"] = request.KeyMaster ?? string.Empty,
                ["KeyApplication"] = request.KeyApplication ?? string.Empty,
                ["KeyReadWrite"] = request.KeyReadWrite ?? string.Empty,
                ["KeyReadOnly"] = request.KeyReadOnly ?? string.Empty,
                ["ApplicationID"] = request.ApplicationID.ToString(),
                ["UserFileType"] = request.UserFileType.ToString(),
                ["UserFileNumber"] = request.UserFileNumber.ToString(),
                ["UserFileSize"] = request.UserFileSize.ToString(),
                ["FaceFileType"] = request.FaceFileType.ToString(),
                ["FaceFileNumber"] = request.FaceFileNumber.ToString(),
                ["FaceFileSize"] = request.FaceFileSize.ToString(),
                ["KeyMasterNumber"] = request.KeyMasterNumber.ToString(),
                ["KeyApplicationNumber"] = request.KeyApplicationNumber.ToString(),
                ["KeyReadWriteNumber"] = request.KeyReadWriteNumber.ToString(),
                ["KeyReadOnlyNumber"] = request.KeyReadOnlyNumber.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemDesfireGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemDesfireGet",
            "SystemDesfireGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "KeyType",
            new[]
            {
                "KeySize",
                "KeyMaster",
                "KeyApplication",
                "KeyReadWrite",
                "KeyReadOnly",
                "ApplicationID",
                "UserFileType",
                "UserFileNumber",
                "UserFileSize",
                "FaceFileType",
                "FaceFileNumber",
                "FaceFileSize",
                "KeyMasterNumber",
                "KeyApplicationNumber",
                "KeyReadWriteNumber",
                "KeyReadOnlyNumber"
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemDesfireSecondarySetAsync(SystemDesfireSecondarySetRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemDesfireSecondarySet",
            "SystemDesfireSecondarySetRequest",
            new Dictionary<string, string>
            {
                ["KeyReadWriteSecondary"] = request.KeyReadWriteSecondary ?? string.Empty,
                ["KeyReadOnlySecondary"] = request.KeyReadOnlySecondary ?? string.Empty,
                ["KeyReadWriteNumberSecondary"] = request.KeyReadWriteNumberSecondary.ToString(),
                ["KeyReadOnlyNumberSecondary"] = request.KeyReadOnlyNumberSecondary.ToString(),
                ["UserSecondary"] = request.UserSecondary.ToString(),
                ["FaceSecondary"] = request.FaceSecondary.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemDesfireSecondaryGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemDesfireSecondaryGet",
            "SystemDesfireSecondaryGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "KeyReadWriteSecondary",
            new[] { "KeyReadOnlySecondary", "KeyReadWriteNumberSecondary", "KeyReadOnlyNumberSecondary", "UserSecondary", "FaceSecondary" },
            cancellationToken);
    }

    public Task<SoapResult> SystemMifareSetAsync(SystemMifareSetRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemMifareSet",
            "SystemMifareSetRequest",
            new Dictionary<string, string>
            {
                ["KeyA"] = request.KeyA ?? string.Empty,
                ["KeyB"] = request.KeyB ?? string.Empty,
                ["UserStart"] = request.UserStart.ToString(),
                ["UserSize"] = request.UserSize.ToString(),
                ["UserKey"] = request.UserKey.ToString(),
                ["UserFormat"] = request.UserFormat.ToString(),
                ["FaceStart"] = request.FaceStart.ToString(),
                ["FaceSize"] = request.FaceSize.ToString(),
                ["FaceKey"] = request.FaceKey.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemMifareGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemMifareGet",
            "SystemMifareGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "KeyA",
            new[] { "KeyB", "UserStart", "UserSize", "UserKey", "UserFormat", "FaceStart", "FaceSize", "FaceKey" },
            cancellationToken);
    }

    public Task<SoapResult> SystemWiegandSetAsync(SystemWiegandSetRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemWiegandSet",
            "SystemWiegandSetRequest",
            new Dictionary<string, string>
            {
                ["InputEnable"] = request.InputEnable.ToString(),
                ["OutputEnable"] = request.OutputEnable.ToString(),
                ["OutputType"] = request.OutputType.ToString(),
                ["OutputPulseWidth"] = request.OutputPulseWidth.ToString(),
                ["OutputPulsePeriod"] = request.OutputPulsePeriod.ToString(),
                ["OutputFailEnable"] = request.OutputFailEnable.ToString(),
                ["OutputFailStartBit"] = request.OutputFailStartBit.ToString(),
                ["OutputFailLength"] = request.OutputFailLength.ToString(),
                ["OutputFailCode"] = request.OutputFailCode.ToString(),
                ["ServiceEndpoint"] = request.ServiceEndpoint ?? string.Empty,
                ["ServiceTimeout"] = request.ServiceTimeout.ToString(),
                ["WebOrTCP"] = request.WebOrTCP.ToString(),
                ["TCPAddress"] = request.TCPAddress ?? string.Empty,
                ["TCPPort"] = request.TCPPort.ToString(),
                ["CardNumberEnable"] = request.CardNumberEnable.ToString(),
                ["CardNumberStart"] = request.CardNumberStart.ToString(),
                ["CardNumberLength"] = request.CardNumberLength.ToString(),
                ["IdentifyFailCode"] = request.IdentifyFailCode ?? string.Empty,
                ["IdentifyFailLength"] = request.IdentifyFailLength.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemWiegandGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemWiegandGet",
            "SystemWiegandGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "InputEnable",
            new[]
            {
                "OutputEnable",
                "OutputType",
                "OutputPulseWidth",
                "OutputPulsePeriod",
                "OutputFailEnable",
                "OutputFailStartBit",
                "OutputFailLength",
                "OutputFailCode",
                "ServiceEndpoint",
                "ServiceTimeout",
                "WebOrTCP",
                "TCPAddress",
                "TCPPort",
                "CardNumberEnable",
                "CardNumberStart",
                "CardNumberLength",
                "IdentifyFailCode",
                "IdentifyFailLength"
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemLogSetAsync(SystemLogSetRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemLogSet",
            "SystemLogSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = request.Enable.ToString(),
                ["Type"] = request.Type.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemLogGetAsync(TypeRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "SystemLogGet",
            "SystemLogGetRequest",
            new Dictionary<string, string> { ["Type"] = request.Type.ToString() },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string enable = FirstText(document, "Enable");
                string type = FirstText(document, "Type");
                var details = new Dictionary<string, string>
                {
                    ["Enable"] = enable,
                    ["Type"] = type
                };
                var lines = new List<string>
                {
                    $"Enable={enable}",
                    $"Type={type}"
                };
                return new SoapResult("SystemLogGet", result, errorMessage, details, "Log Settings", string.Join(Environment.NewLine, lines), lines, xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> LogDeleteEventAsync(TypeRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "LogDeleteEvent",
            "LogDeleteEventRequest",
            new Dictionary<string, string> { ["Type"] = request.Type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> LogGetEventAsync(TypeRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "LogGetEvent",
            "LogGetEventRequest",
            new Dictionary<string, string> { ["Type"] = request.Type.ToString() },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string count = FirstText(document, "Count");
                string reportText = FirstText(document, "Report");
                var details = new Dictionary<string, string>
                {
                    ["Count"] = count
                };
                var lines = new List<string>
                {
                    $"Count={count}"
                };
                return new SoapResult("LogGetEvent", result, errorMessage, details, "Event Report", reportText, lines.Concat(SplitLines(reportText)).ToList(), xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> LogDeleteAppAsync(TypeRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "LogDeleteApp",
            "LogDeleteAppRequest",
            new Dictionary<string, string> { ["Type"] = request.Type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> LogGetAppAsync(TypeRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "LogGetApp",
            "LogGetAppRequest",
            new Dictionary<string, string> { ["Type"] = request.Type.ToString() },
            "Report",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> LogDeleteSysAsync(TypeRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "LogDeleteSys",
            "LogDeleteSysRequest",
            new Dictionary<string, string> { ["Type"] = request.Type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> LogGetSysAsync(TypeRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "LogGetSys",
            "LogGetSysRequest",
            new Dictionary<string, string> { ["Type"] = request.Type.ToString() },
            "Report",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> SystemFaceSetAsync(SystemFaceSetRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemFaceSet",
            "SystemFaceSetRequest",
            new Dictionary<string, string>
            {
                ["MatchThreshold"] = request.MatchThreshold.ToString(CultureInfo.InvariantCulture),
                ["DetectAGS"] = request.DetectAGS.ToString(CultureInfo.InvariantCulture),
                ["DetectPitch"] = request.DetectPitch.ToString(CultureInfo.InvariantCulture),
                ["DetectYaw"] = request.DetectYaw.ToString(CultureInfo.InvariantCulture),
                ["DetectRoll"] = request.DetectRoll.ToString(CultureInfo.InvariantCulture),
                ["DetectLiveness"] = request.DetectLiveness.ToString(CultureInfo.InvariantCulture),
                ["FaceAttempts"] = request.FaceAttempts.ToString(),
                ["FaceTimeout"] = request.FaceTimeout.ToString(),
                ["ExtractQualityOverride"] = request.ExtractQualityOverride.ToString(),
                ["License"] = request.License.ToString(),
                ["MultiFaces"] = request.MultiFaces.ToString(),
                ["WithMinimumSize"] = request.WithMinimumSize.ToString(),
                ["MinimumFaceSize"] = request.MinimumFaceSize.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemFaceGetAsync(TypeRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "SystemFaceGet",
            "SystemFaceGetRequest",
            new Dictionary<string, string> { ["Type"] = request.Type.ToString() },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string matchThreshold = FirstText(document, "MatchThreshold");
                string detectAGS = FirstText(document, "DetectAGS");
                string detectPitch = FirstText(document, "DetectPitch");
                string detectYaw = FirstText(document, "DetectYaw");
                string detectRoll = FirstText(document, "DetectRoll");
                string detectLiveness = FirstText(document, "DetectLiveness");
                string faceAttempts = FirstText(document, "FaceAttempts");
                string faceTimeout = FirstText(document, "FaceTimeout");
                string extractQualityOverride = FirstText(document, "ExtractQualityOverride");
                string license = FirstText(document, "License");
                string multiFaces = FirstText(document, "MultiFaces");
                string withMinimumSize = FirstText(document, "WithMinimumSize");
                string minimumFaceSize = FirstText(document, "MinimumFaceSize");
                var details = new Dictionary<string, string>
                {
                    ["MatchThreshold"] = matchThreshold,
                    ["DetectAGS"] = detectAGS,
                    ["DetectPitch"] = detectPitch,
                    ["DetectYaw"] = detectYaw,
                    ["DetectRoll"] = detectRoll,
                    ["DetectLiveness"] = detectLiveness,
                    ["FaceAttempts"] = faceAttempts,
                    ["FaceTimeout"] = faceTimeout,
                    ["ExtractQualityOverride"] = extractQualityOverride,
                    ["License"] = license,
                    ["MultiFaces"] = multiFaces,
                    ["WithMinimumSize"] = withMinimumSize,
                    ["MinimumFaceSize"] = minimumFaceSize
                };
                var lines = details.Select(pair => $"{pair.Key}={pair.Value}").ToList();
                return new SoapResult("SystemFaceGet", result, FirstText(document, "ErrorMessage"), details, "Face Settings", string.Join(Environment.NewLine, lines), lines, xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> FaceExtractAsync(FaceImageRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeBinaryValueAsync(
            "FaceExtract",
            "FaceExtractRequest",
            new Dictionary<string, string>
            {
                ["ImageType"] = request.ImageType.ToString(),
                ["ImageData"] = request.ImageDataBase64 ?? string.Empty
            },
            "FaceTemplate",
            cancellationToken);
    }

    public Task<SoapResult> FaceVerifyAsync(FaceVerifyRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "FaceVerify",
            "FaceVerifyRequest",
            new Dictionary<string, string>
            {
                ["FaceA"] = request.FaceA ?? string.Empty,
                ["FaceB"] = request.FaceB ?? string.Empty
            },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string score = FirstText(document, "Score");
                var details = new Dictionary<string, string> { ["Score"] = score };
                return new SoapResult("FaceVerify", result, errorMessage, details, "Score", score, SplitLines(score), xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> FaceIdentifyAsync(FaceImageRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "FaceIdentify",
            "FaceIdentifyRequest",
            new Dictionary<string, string>
            {
                ["ImageType"] = request.ImageType.ToString(),
                ["ImageData"] = request.ImageDataBase64 ?? string.Empty
            },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string valid = FirstText(document, "Valid");
                string score = FirstText(document, "Score");
                string index = FirstText(document, "Index");
                string badgeId = FirstText(document, "BadgeID");
                var details = new Dictionary<string, string>
                {
                    ["Valid"] = valid,
                    ["Score"] = score,
                    ["Index"] = index,
                    ["BadgeID"] = badgeId
                };
                var lines = details.Select(pair => $"{pair.Key}={pair.Value}").ToList();
                return new SoapResult("FaceIdentify", result, errorMessage, details, "Identify Result", string.Join(Environment.NewLine, lines), lines, xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> FaceExtractWithInfoAsync(FaceImageRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "FaceExtractWithInfo",
            "FaceExtractWithInfoRequest",
            new Dictionary<string, string>
            {
                ["ImageType"] = request.ImageType.ToString(),
                ["ImageData"] = request.ImageDataBase64 ?? string.Empty
            },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string faceTemplate = FirstText(document, "FaceTemplate");
                var details = new Dictionary<string, string>
                {
                    ["FaceTemplateLength"] = TryDecodeLength(faceTemplate).ToString(),
                    ["Gender"] = FirstText(document, "Gender"),
                    ["Age"] = FirstText(document, "Age"),
                    ["QualityScore"] = FirstText(document, "QualityScore"),
                    ["Pitch"] = FirstText(document, "Pitch"),
                    ["Yaw"] = FirstText(document, "Yaw"),
                    ["Roll"] = FirstText(document, "Roll"),
                    ["Glasses"] = FirstText(document, "Glasses"),
                    ["LeftEye"] = FirstText(document, "LeftEye"),
                    ["RightEye"] = FirstText(document, "RightEye"),
                    ["Smile"] = FirstText(document, "Smile"),
                    ["MouthOpen"] = FirstText(document, "MouthOpen"),
                    ["MouthOccluded"] = FirstText(document, "MouthOccluded"),
                    ["RectX"] = FirstText(document, "RectX"),
                    ["RectY"] = FirstText(document, "RectY"),
                    ["RectWidth"] = FirstText(document, "RectWidth"),
                    ["RectHeight"] = FirstText(document, "RectHeight")
                };
                var lines = details.Select(pair => $"{pair.Key}={pair.Value}").ToList();
                string summary = $"FaceTemplateLength={details["FaceTemplateLength"]}";
                return new SoapResult("FaceExtractWithInfo", result, errorMessage, details, "FaceTemplate", summary, lines.Prepend(summary).ToList(), xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> FaceExtractWithInfoAndRotationAsync(FaceImageRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "FaceExtractWithInfoAndRotation",
            "FaceExtractWithInfoAndRotationRequest",
            new Dictionary<string, string>
            {
                ["ImageType"] = request.ImageType.ToString(),
                ["ImageData"] = request.ImageDataBase64 ?? string.Empty
            },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string faceTemplate = FirstText(document, "FaceTemplate");
                var details = new Dictionary<string, string>
                {
                    ["FaceTemplateLength"] = TryDecodeLength(faceTemplate).ToString(),
                    ["Gender"] = FirstText(document, "Gender"),
                    ["Age"] = FirstText(document, "Age"),
                    ["QualityScore"] = FirstText(document, "QualityScore"),
                    ["Pitch"] = FirstText(document, "Pitch"),
                    ["Yaw"] = FirstText(document, "Yaw"),
                    ["Roll"] = FirstText(document, "Roll"),
                    ["Glasses"] = FirstText(document, "Glasses"),
                    ["LeftEye"] = FirstText(document, "LeftEye"),
                    ["RightEye"] = FirstText(document, "RightEye"),
                    ["Smile"] = FirstText(document, "Smile"),
                    ["MouthOpen"] = FirstText(document, "MouthOpen"),
                    ["MouthOccluded"] = FirstText(document, "MouthOccluded"),
                    ["RectX"] = FirstText(document, "RectX"),
                    ["RectY"] = FirstText(document, "RectY"),
                    ["RectWidth"] = FirstText(document, "RectWidth"),
                    ["RectHeight"] = FirstText(document, "RectHeight"),
                    ["Rotation"] = FirstText(document, "Rotation")
                };
                var lines = details.Select(pair => $"{pair.Key}={pair.Value}").ToList();
                string summary = $"FaceTemplateLength={details["FaceTemplateLength"]}, Rotation={details["Rotation"]}";
                return new SoapResult("FaceExtractWithInfoAndRotation", result, errorMessage, details, "FaceTemplate", summary, lines.Prepend(summary).ToList(), xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> FaceExtractDuplicateAsync(FaceExtractDuplicateRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "FaceExtractDuplicate",
            "FaceExtractDuplicateRequest",
            new Dictionary<string, string>
            {
                ["ImageData"] = request.ImageDataBase64 ?? string.Empty,
                ["Rotation"] = request.Rotation.ToString()
            },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string faceTemplate = FirstText(document, "FaceTemplate");
                var details = new Dictionary<string, string>
                {
                    ["FaceTemplateLength"] = TryDecodeLength(faceTemplate).ToString(),
                    ["DuplicateID1"] = FirstText(document, "DuplicateID1"),
                    ["DuplicateScore1"] = FirstText(document, "DuplicateScore1"),
                    ["DuplicateID2"] = FirstText(document, "DuplicateID2"),
                    ["DuplicateScore2"] = FirstText(document, "DuplicateScore2"),
                    ["DuplicateID3"] = FirstText(document, "DuplicateID3"),
                    ["DuplicateScore3"] = FirstText(document, "DuplicateScore3"),
                    ["DuplicateID4"] = FirstText(document, "DuplicateID4"),
                    ["DuplicateScore4"] = FirstText(document, "DuplicateScore4"),
                    ["DuplicateID5"] = FirstText(document, "DuplicateID5"),
                    ["DuplicateScore5"] = FirstText(document, "DuplicateScore5")
                };
                var lines = details.Select(pair => $"{pair.Key}={pair.Value}").ToList();
                string summary = $"FaceTemplateLength={details["FaceTemplateLength"]}, DuplicateID1={details["DuplicateID1"]}";
                return new SoapResult("FaceExtractDuplicate", result, errorMessage, details, "FaceTemplate", summary, lines.Prepend(summary).ToList(), xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemVideoSetAsync(SystemVideoSetRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemVideoSet",
            "SystemVideoSetRequest",
            new Dictionary<string, string>
            {
                ["DetectPeriod"] = request.DetectPeriod.ToString(),
                ["FrameWidth"] = request.FrameWidth.ToString(),
                ["FrameHeight"] = request.FrameHeight.ToString(),
                ["FrameRotatIon"] = request.FrameRotatIon.ToString(),
                ["CameraMode"] = request.CameraMode.ToString(),
                ["PowerMode"] = request.PowerMode.ToString(),
                ["WithCrop"] = request.WithCrop.ToString(),
                ["CropX"] = request.CropX.ToString(),
                ["CropY"] = request.CropY.ToString(),
                ["CropWidth"] = request.CropWidth.ToString(),
                ["CropHeight"] = request.CropHeight.ToString(),
                ["Liveness"] = request.Liveness.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemVideoGetAsync(TypeRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "SystemVideoGet",
            "SystemVideoGetRequest",
            new Dictionary<string, string> { ["Type"] = request.Type.ToString() },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                var details = new Dictionary<string, string>
                {
                    ["DetectPeriod"] = FirstText(document, "DetectPeriod"),
                    ["FrameWidth"] = FirstText(document, "FrameWidth"),
                    ["FrameHeight"] = FirstText(document, "FrameHeight"),
                    ["FrameRotatIon"] = FirstText(document, "FrameRotatIon"),
                    ["CameraMode"] = FirstText(document, "CameraMode"),
                    ["PowerMode"] = FirstText(document, "PowerMode"),
                    ["WithCrop"] = FirstText(document, "WithCrop"),
                    ["CropX"] = FirstText(document, "CropX"),
                    ["CropY"] = FirstText(document, "CropY"),
                    ["CropWidth"] = FirstText(document, "CropWidth"),
                    ["CropHeight"] = FirstText(document, "CropHeight"),
                    ["Liveness"] = FirstText(document, "Liveness")
                };
                var lines = details.Select(pair => $"{pair.Key}={pair.Value}").ToList();
                return new SoapResult("SystemVideoGet", result, errorMessage, details, "Video Settings", string.Join(Environment.NewLine, lines), lines, xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> VideoFaceCaptureAsync(VideoFaceCaptureRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "VideoFaceCapture",
            "VideoFaceCaptureRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = request.Timeout.ToString(),
                ["WithRGB"] = request.WithRGB.ToString(),
                ["WithNIR"] = request.WithNIR.ToString()
            },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string faceTemplate = FirstText(document, "FaceTemplate");
                string rgb = FirstText(document, "FaceImageRGB");
                string nir = FirstText(document, "FaceImageNIR");
                var details = new Dictionary<string, string>
                {
                    ["FaceTemplateLength"] = TryDecodeLength(faceTemplate).ToString(),
                    ["FaceImageRGBLength"] = TryDecodeLength(rgb).ToString(),
                    ["FaceImageNIRLength"] = TryDecodeLength(nir).ToString()
                };
                var lines = details.Select(pair => $"{pair.Key}={pair.Value}").ToList();
                string summary = $"FaceTemplateLength={details["FaceTemplateLength"]}, RGB={details["FaceImageRGBLength"]}, NIR={details["FaceImageNIRLength"]}";
                return new SoapResult("VideoFaceCapture", result, errorMessage, details, "FaceTemplate", summary, lines.Prepend(summary).ToList(), xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> VideoFaceMatchAsync(VideoFaceMatchRequest request, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "VideoFaceMatch",
            "VideoFaceMatchRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = request.Timeout.ToString(),
                ["UserTemplate"] = request.UserTemplateBase64 ?? string.Empty
            },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string score = FirstText(document, "Score");
                var details = new Dictionary<string, string>
                {
                    ["Score"] = score
                };
                return new SoapResult("VideoFaceMatch", result, errorMessage, details, "Score", score, SplitLines(score), xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDetectAsync(int timeout, int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardDetect",
            "SmartcardDetectRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = timeout.ToString(),
                ["Type"] = type.ToString()
            },
            "CardUID",
            new[] { "CardType" },
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDesfireEraseAsync(int timeout, int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardDesfireErase",
            "SmartcardDesfireEraseRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = timeout.ToString(),
                ["Type"] = type.ToString()
            },
            "CardUID",
            new[] { "CardType" },
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDesfireFormatAsync(int timeout, int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardDesfireFormat",
            "SmartcardDesfireFormatRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = timeout.ToString(),
                ["Type"] = type.ToString()
            },
            "CardUID",
            new[] { "CardType" },
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDesfireWriteAsync(int timeout, int type, string? userData, string? faceDataBase64, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardDesfireWrite",
            "SmartcardDesfireWriteRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = timeout.ToString(),
                ["Type"] = type.ToString(),
                ["UserData"] = userData ?? string.Empty,
                ["FaceData"] = faceDataBase64 ?? string.Empty
            },
            "CardUID",
            new[] { "CardType" },
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDesfireBadgeCreateAsync(int timeout, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardDesfireBadgeCreate",
            "SmartcardDesfireBadgeCreateRequest",
            new Dictionary<string, string> { ["Timeout"] = timeout.ToString() },
            "CSN",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDesfireBadgeWriteAsync(int timeout, string? badge, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardDesfireBadgeWrite",
            "SmartcardDesfireBadgeWriteRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = timeout.ToString(),
                ["Badge"] = badge ?? string.Empty
            },
            "CSN",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDesfireBadgeReadAsync(int timeout, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardDesfireBadgeRead",
            "SmartcardDesfireBadgeReadRequest",
            new Dictionary<string, string> { ["Timeout"] = timeout.ToString() },
            "Badge",
            new[] { "CSN" },
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDesfireFaceCreateAsync(int timeout, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardDesfireFaceCreate",
            "SmartcardDesfireFaceCreateRequest",
            new Dictionary<string, string> { ["Timeout"] = timeout.ToString() },
            "CSN",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDesfireFaceWriteAsync(int timeout, string? faceDataBase64, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardDesfireFaceWrite",
            "SmartcardDesfireFaceWriteRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = timeout.ToString(),
                ["Face"] = faceDataBase64 ?? string.Empty
            },
            "CSN",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDesfireFaceReadAsync(int timeout, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "SmartcardDesfireFaceRead",
            "SmartcardDesfireFaceReadRequest",
            new Dictionary<string, string> { ["Timeout"] = timeout.ToString() },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string faceData = FirstText(document, "Face");
                string csn = FirstText(document, "CSN");
                var details = new Dictionary<string, string>
                {
                    ["FaceLength"] = TryDecodeLength(faceData).ToString(),
                    ["CSN"] = csn
                };
                var lines = new List<string>
                {
                    $"FaceLength={details["FaceLength"]}",
                    $"CSN={csn}"
                };
                return new SoapResult("SmartcardDesfireFaceRead", result, errorMessage, details, "Face", faceData, lines, xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> SmartcardDesfireReadAsync(int timeout, int type, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "SmartcardDesfireRead",
            "SmartcardDesfireReadRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = timeout.ToString(),
                ["Type"] = type.ToString()
            },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string userData = FirstText(document, "UserData");
                string faceData = FirstText(document, "FaceData");
                string cardType = FirstText(document, "CardType");
                string cardUid = FirstText(document, "CardUID");
                var details = new Dictionary<string, string>
                {
                    ["UserData"] = userData,
                    ["FaceDataLength"] = TryDecodeLength(faceData).ToString(),
                    ["CardType"] = cardType,
                    ["CardUID"] = cardUid
                };
                var lines = new List<string>
                {
                    $"UserData={userData}",
                    $"FaceDataLength={details["FaceDataLength"]}",
                    $"CardType={cardType}",
                    $"CardUID={cardUid}"
                };
                return new SoapResult("SmartcardDesfireRead", result, errorMessage, details, "User Data", userData, lines, xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> SmartcardMifareWriteAsync(int timeout, string? userData, string? faceDataBase64, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardMifareWrite",
            "SmartcardMifareWriteRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = timeout.ToString(),
                ["UserData"] = userData ?? string.Empty,
                ["FaceData"] = faceDataBase64 ?? string.Empty
            },
            "CardUID",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> SmartcardMifareReadAsync(int timeout, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "SmartcardMifareRead",
            "SmartcardMifareReadRequest",
            new Dictionary<string, string> { ["Timeout"] = timeout.ToString() },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string userData = FirstText(document, "UserData");
                string faceData = FirstText(document, "FaceData");
                string cardUid = FirstText(document, "CardUID");
                var details = new Dictionary<string, string>
                {
                    ["UserData"] = userData,
                    ["FaceDataLength"] = TryDecodeLength(faceData).ToString(),
                    ["CardUID"] = cardUid
                };
                var lines = new List<string>
                {
                    $"UserData={userData}",
                    $"FaceDataLength={details["FaceDataLength"]}",
                    $"CardUID={cardUid}"
                };
                return new SoapResult("SmartcardMifareRead", result, errorMessage, details, "User Data", userData, lines, xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> SmartcardMifareBadgeWriteAsync(int timeout, string? badge, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardMifareBadgeWrite",
            "SmartcardMifareBadgeWriteRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = timeout.ToString(),
                ["Badge"] = badge ?? string.Empty
            },
            "CSN",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> SmartcardMifareBadgeReadAsync(int timeout, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SmartcardMifareBadgeRead",
            "SmartcardMifareBadgeReadRequest",
            new Dictionary<string, string> { ["Timeout"] = timeout.ToString() },
            "Badge",
            new[] { "CSN" },
            cancellationToken);
    }

    public Task<SoapResult> SmartcardAskReadAsync(int timeout, CancellationToken cancellationToken = default)
    {
        return InvokeCustomAsync(
            "SmartcardAskRead",
            "SmartcardAskReadRequest",
            new Dictionary<string, string> { ["Timeout"] = timeout.ToString() },
            (document, xml) =>
            {
                int result = ParseInt(FirstText(document, "Result"), "Result");
                string errorMessage = FirstText(document, "ErrorMessage");
                string wiegand = FirstText(document, "Wiegand");
                string face = FirstText(document, "Face");
                var details = new Dictionary<string, string>
                {
                    ["Wiegand"] = wiegand,
                    ["FaceLength"] = TryDecodeLength(face).ToString()
                };
                var lines = new List<string>
                {
                    $"Wiegand={wiegand}",
                    $"FaceLength={details["FaceLength"]}"
                };
                return new SoapResult("SmartcardAskRead", result, errorMessage, details, "Wiegand", wiegand, lines, xml);
            },
            cancellationToken);
    }

    public Task<SoapResult> WiegandDetectAsync(int timeout, int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "WiegandDetect",
            "WiegandDetectRequest",
            new Dictionary<string, string>
            {
                ["Timeout"] = timeout.ToString(),
                ["Type"] = type.ToString()
            },
            "WiegandStream",
            new[] { "WiegandType", "FacilityCode", "CardNumber", "IssueNumber" },
            cancellationToken);
    }

    public Task<SoapResult> CardUidDetectAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "CardUIDDetect",
            "CardUIDDetectRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "UID",
            "UID",
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyCountAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserIdentifyCount",
            "UserIdentifyCountRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Count",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyListAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserIdentifyListAll",
            "UserIdentifyListAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "IdentifyList",
            new[] { "IdentifyNumber" },
            cancellationToken);
    }

    public Task<SoapResult> UserSmartcardCountAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserSmartcardCount",
            "UserSmartcardCountRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Count",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> UserSmartcardListAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserSmartcardListAll",
            "UserSmartcardListAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "SmartcardList",
            new[] { "SmartcardNumber" },
            cancellationToken);
    }

    public Task<SoapResult> UserElevatorCountAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserElevatorCount",
            "UserElevatorCountRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Count",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> UserElevatorListAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserElevatorListAll",
            "UserElevatorListAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "List",
            new[] { "Number" },
            cancellationToken);
    }

    public Task<SoapResult> UserRestrictedCountAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserRestrictedCount",
            "UserRestrictedCountRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Count",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> UserRestrictedListAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserRestrictedListAll",
            "UserRestrictedListAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "List",
            new[] { "Number" },
            cancellationToken);
    }

    public Task<SoapResult> UserScheduleCountAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserScheduleCount",
            "UserScheduleCountRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Count",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> UserScheduleListAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserScheduleListAll",
            "UserScheduleListAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "List",
            new[] { "Number" },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyAddAsync(
        string badgeId,
        string faceDataBase64,
        int relayActive,
        int relayStrike,
        int wiegandActive,
        string wiegandData,
        int wiegandLength,
        CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "UserIdentifyAdd",
            "UserIdentifyAddRequest",
            new Dictionary<string, string>
            {
                ["BadgeID"] = badgeId,
                ["FaceData"] = faceDataBase64,
                ["RelayActive"] = relayActive.ToString(),
                ["RelayStrike"] = relayStrike.ToString(),
                ["WiegandActive"] = wiegandActive.ToString(),
                ["WiegandData"] = wiegandData,
                ["WiegandLength"] = wiegandLength.ToString()
            },
            "CardNumber",
            "CardNumber",
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyDeleteAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyDelete",
            "UserIdentifyDeleteRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyDeleteAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyDeleteAll",
            "UserIdentifyDeleteAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyListAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserIdentifyList",
            "UserIdentifyListRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            "WiegandData",
            new[] { "RelayActive", "RelayStrike", "WiegandActive", "WiegandLength" },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyCheckAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "UserIdentifyCheck",
            "UserIdentifyCheckRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            "IsPresent",
            "IsPresent",
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyTemplateAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeBinaryValueAsync(
            "UserIdentifyTemplate",
            "UserIdentifyTemplateRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            "FaceData",
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyRestrictEnableAsync(int status, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyRestrictEnable",
            "UserIdentifyRestrictEnableRequest",
            new Dictionary<string, string> { ["Status"] = status.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyActivateAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyActivate",
            "UserIdentifyActivateRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyDeactivateAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyDeactivate",
            "UserIdentifyDeactivateRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyActivateAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyActivateAll",
            "UserIdentifyActivateAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyTimeActivateAsync(
        string badgeId,
        string startTime,
        string endTime,
        CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyTimeActivate",
            "UserIdentifyTimeActivateRequest",
            new Dictionary<string, string>
            {
                ["BadgeID"] = badgeId,
                ["StartTime"] = startTime,
                ["EndTime"] = endTime
            },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyTimeDeactivateAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyTimeDeactivate",
            "UserIdentifyTimeDeactivateRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyTimeDeactivateAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyTimeDeactivateAll",
            "UserIdentifyTimeDeactivateAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> FaceTemplateExtractionAsync(byte[] imageData, CancellationToken cancellationToken = default)
    {
        return InvokeBinaryValueAsync(
            "FaceTemplateExtraction",
            "FaceTemplateExtractionRequest",
            new Dictionary<string, string> { ["ImageData"] = Convert.ToBase64String(imageData) },
            "FaceTemplate",
            cancellationToken);
    }

    public Task<SoapResult> FaceTemplateMatchAsync(byte[] templateData, int wiegandLength, int wiegandFacilityCode, int wiegandCardNumber, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "FaceTemplateMatch",
            "FaceTemplateMatchRequest",
            new Dictionary<string, string>
            {
                ["TemplateData"] = Convert.ToBase64String(templateData),
                ["WiegandLength"] = wiegandLength.ToString(),
                ["WiegandFacilityCode"] = wiegandFacilityCode.ToString(),
                ["WiegandCardNumber"] = wiegandCardNumber.ToString()
            },
            "MatchScore",
            "MatchScore",
            cancellationToken);
    }

    public Task<SoapResult> FaceTemplateMatchWiegandAsync(byte[] templateData, int wiegandLength, byte[] wiegandData, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "FaceTemplateMatchWiegand",
            "FaceTemplateMatchWiegandRequest",
            new Dictionary<string, string>
            {
                ["TemplateData"] = Convert.ToBase64String(templateData),
                ["WiegandLength"] = wiegandLength.ToString(),
                ["WiegandData"] = Convert.ToBase64String(wiegandData)
            },
            "MatchScore",
            "MatchScore",
            cancellationToken);
    }

    public Task<SoapResult> FaceTemplateMatchWiegandStringAsync(byte[] templateData, int wiegandLength, string wiegandData, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "FaceTemplateMatchWiegandString",
            "FaceTemplateMatchWiegandStringRequest",
            new Dictionary<string, string>
            {
                ["TemplateData"] = Convert.ToBase64String(templateData),
                ["WiegandLength"] = wiegandLength.ToString(),
                ["WiegandData"] = wiegandData
            },
            "MatchScore",
            "MatchScore",
            cancellationToken);
    }

    public Task<SoapResult> SystemBluetoothGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemBluetoothGet",
            "SystemBluetoothGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[] { "UserTimeout" },
            cancellationToken);
    }

    public Task<SoapResult> SystemBluetoothSetAsync(int enable, int userTimeout, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemBluetoothSet",
            "SystemBluetoothSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["UserTimeout"] = userTimeout.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemBuzzerActivateAsync(int count, int duration, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemBuzzerActivate",
            "SystemBuzzerActivateRequest",
            new Dictionary<string, string>
            {
                ["Count"] = count.ToString(),
                ["Duration"] = duration.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemBuzzerGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "SystemBuzzerGet",
            "SystemBuzzerGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            "Enable",
            cancellationToken);
    }

    public Task<SoapResult> SystemBuzzerSetAsync(int enable, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemBuzzerSet",
            "SystemBuzzerSetRequest",
            new Dictionary<string, string> { ["Enable"] = enable.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> SystemCommunicationSecurityGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemCommunicationSecurityGet",
            "SystemCommunicationSecurityGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "WithSSL",
            new[] { "PortNo", "DeviceNo" },
            cancellationToken);
    }

    public Task<SoapResult> SystemCommunicationSecuritySetAsync(int withSsl, int portNo, int deviceNo, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemCommunicationSecuritySet",
            "SystemCommunicationSecuritySetRequest",
            new Dictionary<string, string>
            {
                ["WithSSL"] = withSsl.ToString(),
                ["PortNo"] = portNo.ToString(),
                ["DeviceNo"] = deviceNo.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemElevatorGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "SystemElevatorGet",
            "SystemElevatorGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Status",
            "Status",
            cancellationToken);
    }

    public Task<SoapResult> SystemElevatorSetAsync(int status, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemElevatorSet",
            "SystemElevatorSetRequest",
            new Dictionary<string, string> { ["Status"] = status.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> SystemExitPushButtonGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemExitPushButtonGet",
            "SystemExitPushButtonGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[] { "ActiveState" },
            cancellationToken);
    }

    public Task<SoapResult> SystemExitPushButtonSetAsync(int enable, int activeState, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemExitPushButtonSet",
            "SystemExitPushButtonSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["ActiveState"] = activeState.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemFaceA64CommGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemFaceA64CommGet",
            "SystemFaceA64CommGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Port",
            new[] { "ServerIP", "Domain", "WithSSL" },
            cancellationToken);
    }

    public Task<SoapResult> SystemFaceA64CommSetAsync(int port, string serverIp, string domain, int withSsl, string? certCa, string? certClient, string? certServer, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemFaceA64CommSet",
            "SystemFaceA64CommSetRequest",
            new Dictionary<string, string>
            {
                ["Port"] = port.ToString(),
                ["ServerIP"] = serverIp,
                ["Domain"] = domain,
                ["WithSSL"] = withSsl.ToString(),
                ["CertCA"] = certCa ?? string.Empty,
                ["CertClient"] = certClient ?? string.Empty,
                ["CertServer"] = certServer ?? string.Empty
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemFortressLedGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "SystemFortressLedGet",
            "SystemFortressLedGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "WithWiegand",
            "WithWiegand",
            cancellationToken);
    }

    public Task<SoapResult> SystemFortressLedSetAsync(int withWiegand, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemFortressLedSet",
            "SystemFortressLedSetRequest",
            new Dictionary<string, string> { ["WithWiegand"] = withWiegand.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> SystemFortressSerialGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemFortressSerialGet",
            "SystemFortressSerialGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Baud",
            new[] { "Parity", "StopBits", "DataBits", "Method" },
            cancellationToken);
    }

    public Task<SoapResult> SystemFortressSerialSetAsync(int baud, int parity, int stopBits, int dataBits, int method, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemFortressSerialSet",
            "SystemFortressSerialSetRequest",
            new Dictionary<string, string>
            {
                ["Baud"] = baud.ToString(),
                ["Parity"] = parity.ToString(),
                ["StopBits"] = stopBits.ToString(),
                ["DataBits"] = dataBits.ToString(),
                ["Method"] = method.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemGetAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeBinaryValueAsync(
            "SystemGetAll",
            "SystemGetAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "AllSettings",
            cancellationToken);
    }

    public Task<SoapResult> SystemLedBrightnessAsync(int brightness, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemLedBrightness",
            "SystemLedBrightnessRequest",
            new Dictionary<string, string> { ["Brightness"] = brightness.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> SystemLedRotateAsync(int rotate, int period, int colour, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemLedRotate",
            "SystemLedRotateRequest",
            new Dictionary<string, string>
            {
                ["Rotate"] = rotate.ToString(),
                ["Period"] = period.ToString(),
                ["Colour"] = colour.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemLedSetAsync(int colour, int blink, int duration, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemLedSet",
            "SystemLedSetRequest",
            new Dictionary<string, string>
            {
                ["Colour"] = colour.ToString(),
                ["Blink"] = blink.ToString(),
                ["Duration"] = duration.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemModeGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "SystemModeGet",
            "SystemModeGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Mode",
            "Mode",
            cancellationToken);
    }

    public Task<SoapResult> SystemModeSetAsync(int mode, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemModeSet",
            "SystemModeSetRequest",
            new Dictionary<string, string> { ["Mode"] = mode.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> SystemNTPGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemNTPGet",
            "SystemNTPGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[] { "Server", "Zone" },
            cancellationToken);
    }

    public Task<SoapResult> SystemNTPSetAsync(int enable, string server, int zone, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemNTPSet",
            "SystemNTPSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["Server"] = server,
                ["Zone"] = zone.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemNetworkGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemNetworkGet",
            "SystemNetworkGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "isDHCP",
            new[] { "Address", "Gateway", "Netmask", "DNS", "MAC" },
            cancellationToken);
    }

    public Task<SoapResult> SystemNetworkSetAsync(int isDhcp, string address, string gateway, string netmask, string dns, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemNetworkSet",
            "SystemNetworkSetRequest",
            new Dictionary<string, string>
            {
                ["isDHCP"] = isDhcp.ToString(),
                ["Address"] = address,
                ["Gateway"] = gateway,
                ["Netmask"] = netmask,
                ["DNS"] = dns
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemNetworkMAC8021XGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemNetworkMAC8021XGet",
            "SystemNetworkMAC8021XGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "KeyManagement",
            new[] { "EAP", "Identity", "Password", "CACert", "PrivateKey", "PrivateKeyPassword", "Phase1", "Phase2" },
            cancellationToken);
    }

    public Task<SoapResult> SystemNetworkMAC8021XSetAsync(
        string keyManagement,
        string eap,
        string identity,
        string password,
        string caCert,
        string privateKey,
        string privateKeyPassword,
        string phase1,
        string phase2,
        CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemNetworkMAC8021XSet",
            "SystemNetworkMAC8021XSetRequest",
            new Dictionary<string, string>
            {
                ["KeyManagement"] = keyManagement,
                ["EAP"] = eap,
                ["Identity"] = identity,
                ["Password"] = password,
                ["CACert"] = caCert,
                ["PrivateKey"] = privateKey,
                ["PrivateKeyPassword"] = privateKeyPassword,
                ["Phase1"] = phase1,
                ["Phase2"] = phase2
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemOSDPGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemOSDPGet",
            "SystemOSDPGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[] { "Baud", "Parity", "StopBits", "DataBits", "Address", "WithPanelFeedback" },
            cancellationToken);
    }

    public Task<SoapResult> SystemOSDPSetAsync(int enable, int baud, int parity, int stopBits, int dataBits, int address, int withPanelFeedback, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemOSDPSet",
            "SystemOSDPSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["Baud"] = baud.ToString(),
                ["Parity"] = parity.ToString(),
                ["StopBits"] = stopBits.ToString(),
                ["DataBits"] = dataBits.ToString(),
                ["Address"] = address.ToString(),
                ["WithPanelFeedback"] = withPanelFeedback.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemPanelFeedbackGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemPanelFeedbackGet",
            "SystemPanelFeedbackGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[] { "Method" },
            cancellationToken);
    }

    public Task<SoapResult> SystemPanelFeedbackSetAsync(int enable, int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemPanelFeedbackSet",
            "SystemPanelFeedbackSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["Type"] = type.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemPasswordAsync(string password, string currentPassword, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemPassword",
            "SystemPasswordRequest",
            new Dictionary<string, string>
            {
                ["Password"] = password,
                ["CurrentPassword"] = currentPassword
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemPasswordSetAsync(string password, string currentPassword, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemPasswordSet",
            "SystemPasswordSetRequest",
            new Dictionary<string, string>
            {
                ["Password"] = password,
                ["CurrentPassword"] = currentPassword
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemRS485GetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemRS485Get",
            "SystemRS485GetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Baud",
            new[] { "Parity", "StopBits", "DataBits", "OutputBadgeID" },
            cancellationToken);
    }

    public Task<SoapResult> SystemRS485SetAsync(int baud, int parity, int stopBits, int dataBits, int outputBadgeId, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemRS485Set",
            "SystemRS485SetRequest",
            new Dictionary<string, string>
            {
                ["Baud"] = baud.ToString(),
                ["Parity"] = parity.ToString(),
                ["StopBits"] = stopBits.ToString(),
                ["DataBits"] = dataBits.ToString(),
                ["OutputBadgeID"] = outputBadgeId.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemRadarGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "SystemRadarGet",
            "SystemRadarGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            "Enable",
            cancellationToken);
    }

    public Task<SoapResult> SystemRadarSetAsync(int enable, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemRadarSet",
            "SystemRadarSetRequest",
            new Dictionary<string, string> { ["Enable"] = enable.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> SystemRelayGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemRelayGet",
            "SystemRelayGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[] { "StrikeTime" },
            cancellationToken);
    }

    public Task<SoapResult> SystemRelaySetAsync(int enable, int strikeTime, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemRelaySet",
            "SystemRelaySetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["StrikeTime"] = strikeTime.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemRtmCacheClearAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemRtmCacheClear",
            "SystemRtmCacheClearRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> SystemRtmGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemRtmGet",
            "SystemRtmGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[]
            {
                "Endpoint",
                "Timeout",
                "Attempts",
                "Period",
                "Method",
                "Fail",
                "CacheDisable",
                "WithoutImage",
                "WebOrTCP",
                "TCPAddress",
                "TCPPort",
                "FortressMask"
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemRtmSetAsync(
        int enable,
        string endpoint,
        int timeout,
        int attempts,
        int period,
        int method,
        int fail,
        int cacheDisable,
        int withoutImage,
        int webOrTcp,
        string tcpAddress,
        int tcpPort,
        int fortressMask,
        int fortressAlive,
        CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemRtmSet",
            "SystemRtmSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["Endpoint"] = endpoint,
                ["Timeout"] = timeout.ToString(),
                ["Attempts"] = attempts.ToString(),
                ["Period"] = period.ToString(),
                ["Method"] = method.ToString(),
                ["Fail"] = fail.ToString(),
                ["CacheDisable"] = cacheDisable.ToString(),
                ["WithoutImage"] = withoutImage.ToString(),
                ["WebOrTCP"] = webOrTcp.ToString(),
                ["TCPAddress"] = tcpAddress,
                ["TCPPort"] = tcpPort.ToString(),
                ["FortressMask"] = fortressMask.ToString(),
                ["FortressAlive"] = fortressAlive.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemSNMPGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemSNMPGet",
            "SystemSNMPGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[] { "Community" },
            cancellationToken);
    }

    public Task<SoapResult> SystemSNMPSetAsync(int enable, string community, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemSNMPSet",
            "SystemSNMPSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["Community"] = community
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemSSLCertificateAsync(byte[] certCa, byte[] certServer, byte[] certClient, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemSSLCertificate",
            "SystemSSLCertificateRequest",
            new Dictionary<string, string>
            {
                ["CertCA"] = Convert.ToBase64String(certCa),
                ["CertServer"] = Convert.ToBase64String(certServer),
                ["CertClient"] = Convert.ToBase64String(certClient)
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemTamperGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemTamperGet",
            "SystemTamperGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[] { "Sensitivity" },
            cancellationToken);
    }

    public Task<SoapResult> SystemTamperSetAsync(int enable, int sensitivity, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemTamperSet",
            "SystemTamperSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["Sensitivity"] = sensitivity.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemTimeGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "SystemTimeGet",
            "SystemTimeGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Time",
            "Time",
            cancellationToken);
    }

    public Task<SoapResult> SystemTimeSetAsync(string time, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "SystemTimeSet",
            "SystemTimeSetRequest",
            new Dictionary<string, string> { ["Time"] = time },
            "Type",
            "Type",
            cancellationToken);
    }

    public Task<SoapResult> SystemVersionAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemVersion",
            "SystemVersionRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "VersionApplication",
            new[] { "VersionOS", "VersionFace", "VersionCardReader" },
            cancellationToken);
    }

    public Task<SoapResult> SystemWebConnectivityAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemWebConnectivity",
            "SystemWebConnectivityRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> SystemWebEnrollGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemWebEnrollGet",
            "SystemWebEnrollGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[] { "DeviceID", "EndPoint", "ConnectTimeout", "ReadTimeout", "WriteTimeout", "PollPeriod" },
            cancellationToken);
    }

    public Task<SoapResult> SystemWebEnrollSetAsync(
        int enable,
        int deviceId,
        string endPoint,
        int connectTimeout,
        int readTimeout,
        int writeTimeout,
        int pollPeriod,
        CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemWebEnrollSet",
            "SystemWebEnrollSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["DeviceID"] = deviceId.ToString(),
                ["EndPoint"] = endPoint,
                ["ConnectTimeout"] = connectTimeout.ToString(),
                ["ReadTimeout"] = readTimeout.ToString(),
                ["WriteTimeout"] = writeTimeout.ToString(),
                ["PollPeriod"] = pollPeriod.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemWifiConnectivityAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemWifiConnectivity",
            "SystemWifiConnectivityRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> SystemWifiGetAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "SystemWifiGet",
            "SystemWifiGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Enable",
            new[]
            {
                "Network",
                "Password",
                "Address",
                "MAC",
                "IsStatic",
                "IPAddress",
                "IPGateway",
                "IPNetmask",
                "IPDNS",
                "Check",
                "CheckIP"
            },
            cancellationToken);
    }

    public Task<SoapResult> SystemWifiMacAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "SystemWifiMac",
            "SystemWifiMacRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "MacAddress",
            "MacAddress",
            cancellationToken);
    }

    public Task<SoapResult> SystemWifiSetAsync(
        int enable,
        string network,
        string password,
        int isStatic,
        string ipAddress,
        string ipGateway,
        string ipNetmask,
        string ipDns,
        int check,
        string checkIp,
        CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "SystemWifiSet",
            "SystemWifiSetRequest",
            new Dictionary<string, string>
            {
                ["Enable"] = enable.ToString(),
                ["Network"] = network,
                ["Password"] = password,
                ["IsStatic"] = isStatic.ToString(),
                ["IPAddress"] = ipAddress,
                ["IPGateway"] = ipGateway,
                ["IPNetmask"] = ipNetmask,
                ["IPDNS"] = ipDns,
                ["Check"] = check.ToString(),
                ["CheckIP"] = checkIp
            },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyRestrictAddAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyRestrictAdd",
            "UserIdentifyRestrictAddRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyRestrictDeleteAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyRestrictDelete",
            "UserIdentifyRestrictDeleteRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            cancellationToken);
    }

    public Task<SoapResult> UserIdentifyRestrictDeleteAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserIdentifyRestrictDeleteAll",
            "UserIdentifyRestrictDeleteAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> UserSeosAddAsync(
        string badgeId,
        byte[] faceData,
        string csn,
        int relayActive,
        int relayStrike,
        string pacsData,
        int pacsLength,
        CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserSeosAdd",
            "UserSeosAddRequest",
            new Dictionary<string, string>
            {
                ["BadgeID"] = badgeId,
                ["FaceData"] = Convert.ToBase64String(faceData),
                ["CSN"] = csn,
                ["RelayActive"] = relayActive.ToString(),
                ["RelayStrike"] = relayStrike.ToString(),
                ["PacsData"] = pacsData,
                ["PacsLength"] = pacsLength.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> UserSeosCheckAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "UserSeosCheck",
            "UserSeosCheckRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            "IsPresent",
            "IsPresent",
            cancellationToken);
    }

    public Task<SoapResult> UserSeosCountAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserSeosCount",
            "UserSeosCountRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Count",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> UserSeosDeleteAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserSeosDelete",
            "UserSeosDeleteRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            cancellationToken);
    }

    public Task<SoapResult> UserSeosDeleteAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserSeosDeleteAll",
            "UserSeosDeleteAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> UserSmartcardAddAsync(
        string badgeId,
        string csn,
        int cardType,
        byte[] faceData,
        int relayActive,
        int relayStrike,
        int wiegandActive,
        string wiegandData,
        int wiegandLength,
        CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserSmartcardAdd",
            "UserSmartcardAddRequest",
            new Dictionary<string, string>
            {
                ["BadgeID"] = badgeId,
                ["CSN"] = csn,
                ["CardType"] = cardType.ToString(),
                ["FaceData"] = Convert.ToBase64String(faceData),
                ["RelayActive"] = relayActive.ToString(),
                ["RelayStrike"] = relayStrike.ToString(),
                ["WiegandActive"] = wiegandActive.ToString(),
                ["WiegandData"] = wiegandData,
                ["WiegandLength"] = wiegandLength.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> UserSmartcardAddMultiAsync(int count, byte[] data, int overwrite, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "UserSmartcardAddMulti",
            "UserSmartcardAddMultiRequest",
            new Dictionary<string, string>
            {
                ["UserSmartcardCount"] = count.ToString(),
                ["UserSmartcardData"] = Convert.ToBase64String(data),
                ["UserSmartcardOverwrite"] = overwrite.ToString()
            },
            "TotalWrite",
            "TotalWrite",
            cancellationToken);
    }

    public Task<SoapResult> UserSmartcardCheckAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "UserSmartcardCheck",
            "UserSmartcardCheckRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            "IsPresent",
            "IsPresent",
            cancellationToken);
    }

    public Task<SoapResult> UserSmartcardDeleteAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserSmartcardDelete",
            "UserSmartcardDeleteRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            cancellationToken);
    }

    public Task<SoapResult> UserSmartcardDeleteAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserSmartcardDeleteAll",
            "UserSmartcardDeleteAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> UserSmartcardListAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserSmartcardList",
            "UserSmartcardListRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            "CSN",
            new[] { "CardType", "RelayActive", "RelayStrike", "WiegandActive", "WiegandData", "WiegandLength" },
            cancellationToken);
    }

    public Task<SoapResult> UserWiegandAddAsync(
        string badgeId,
        byte[] faceData,
        long facilityCode,
        int relayActive,
        int relayStrike,
        int wiegandActive,
        string wiegandData,
        int wiegandLength,
        CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserWiegandAdd",
            "UserWiegandAddRequest",
            new Dictionary<string, string>
            {
                ["BadgeID"] = badgeId,
                ["FaceData"] = Convert.ToBase64String(faceData),
                ["FacilityCode"] = facilityCode.ToString(),
                ["RelayActive"] = relayActive.ToString(),
                ["RelayStrike"] = relayStrike.ToString(),
                ["WiegandActive"] = wiegandActive.ToString(),
                ["WiegandData"] = wiegandData,
                ["WiegandLength"] = wiegandLength.ToString()
            },
            cancellationToken);
    }

    public Task<SoapResult> UserWiegandAddMultiAsync(int count, byte[] data, int overwrite, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "UserWiegandAddMulti",
            "UserWiegandAddMultiRequest",
            new Dictionary<string, string>
            {
                ["UserWiegandCount"] = count.ToString(),
                ["UserWiegandData"] = Convert.ToBase64String(data),
                ["UserSmartcardOverwrite"] = overwrite.ToString()
            },
            "TotalWrite",
            "TotalWrite",
            cancellationToken);
    }

    public Task<SoapResult> UserWiegandCheckAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeSingleValueAsync(
            "UserWiegandCheck",
            "UserWiegandCheckRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            "IsPresent",
            "IsPresent",
            cancellationToken);
    }

    public Task<SoapResult> UserWiegandCountAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserWiegandCount",
            "UserWiegandCountRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "Count",
            Array.Empty<string>(),
            cancellationToken);
    }

    public Task<SoapResult> UserWiegandDeleteAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserWiegandDelete",
            "UserWiegandDeleteRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            cancellationToken);
    }

    public Task<SoapResult> UserWiegandDeleteAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeResultOnlyAsync(
            "UserWiegandDeleteAll",
            "UserWiegandDeleteAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            cancellationToken);
    }

    public Task<SoapResult> UserWiegandListAsync(string badgeId, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserWiegandList",
            "UserWiegandListRequest",
            new Dictionary<string, string> { ["BadgeID"] = badgeId },
            "FacilityCode",
            new[] { "CardNumber", "IssueNumber", "RelayActive", "RelayStrike", "WiegandActive", "WiegandData", "WiegandLength" },
            cancellationToken);
    }

    public Task<SoapResult> UserWiegandListAllAsync(int type, CancellationToken cancellationToken = default)
    {
        return InvokeAsync(
            "UserWiegandListAll",
            "UserWiegandListAllRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() },
            "WiegandList",
            new[] { "WiegandNumber" },
            cancellationToken);
    }

    private async Task<SoapResult> InvokeAsync(
        string action,
        string requestElement,
        IReadOnlyDictionary<string, string> requestFields,
        string reportField,
        IEnumerable<string> detailFields,
        CancellationToken cancellationToken)
    {
        var envelope = BuildEnvelope(requestElement, requestFields);

        using var request = new HttpRequestMessage(HttpMethod.Post, Endpoint)
        {
            Content = new StringContent(envelope, Encoding.UTF8, "text/xml")
        };

        request.Headers.TryAddWithoutValidation("SOAPAction", $"\"{action}\"");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/soap+xml"));

        using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        string xml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        XDocument document = XDocument.Parse(xml);

        int result = ParseInt(FirstText(document, "Result"), "Result");
        string errorMessage = FirstText(document, "ErrorMessage");

        var details = new Dictionary<string, string>();
        foreach (string field in detailFields)
        {
            details[field] = FirstText(document, field);
        }

        string reportText = FirstText(document, reportField);
        List<string> reportLines = SplitLines(reportText);

        return new SoapResult(
            action,
            result,
            errorMessage,
            details,
            reportField,
            reportText,
            reportLines,
            xml);
    }

    private async Task<SoapResult> InvokeCustomAsync(
        string action,
        string requestElement,
        IReadOnlyDictionary<string, string> requestFields,
        Func<XDocument, string, SoapResult> parser,
        CancellationToken cancellationToken)
    {
        var envelope = BuildEnvelope(requestElement, requestFields);

        using var request = new HttpRequestMessage(HttpMethod.Post, Endpoint)
        {
            Content = new StringContent(envelope, Encoding.UTF8, "text/xml")
        };

        request.Headers.TryAddWithoutValidation("SOAPAction", $"\"{action}\"");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/soap+xml"));

        using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        string xml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        XDocument document = XDocument.Parse(xml);
        return parser(document, xml);
    }

    private async Task<SoapResult> InvokeResultOnlyAsync(
        string action,
        string requestElement,
        IReadOnlyDictionary<string, string> requestFields,
        CancellationToken cancellationToken)
    {
        var envelope = BuildEnvelope(requestElement, requestFields);

        using var request = new HttpRequestMessage(HttpMethod.Post, Endpoint)
        {
            Content = new StringContent(envelope, Encoding.UTF8, "text/xml")
        };

        request.Headers.TryAddWithoutValidation("SOAPAction", $"\"{action}\"");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/soap+xml"));

        using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        string xml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        XDocument document = XDocument.Parse(xml);

        int result = ParseInt(FirstText(document, "Result"), "Result");
        string errorMessage = FirstText(document, "ErrorMessage");

        return new SoapResult(
            action,
            result,
            errorMessage,
            new Dictionary<string, string>(),
            "Result",
            string.Empty,
            new List<string>(),
            xml);
    }

    private async Task<SoapResult> InvokeSingleValueAsync(
        string action,
        string requestElement,
        IReadOnlyDictionary<string, string> requestFields,
        string valueField,
        string detailsField,
        CancellationToken cancellationToken)
    {
        var envelope = BuildEnvelope(requestElement, requestFields);

        using var request = new HttpRequestMessage(HttpMethod.Post, Endpoint)
        {
            Content = new StringContent(envelope, Encoding.UTF8, "text/xml")
        };

        request.Headers.TryAddWithoutValidation("SOAPAction", $"\"{action}\"");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/soap+xml"));

        using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        string xml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        XDocument document = XDocument.Parse(xml);

        int result = ParseInt(FirstText(document, "Result"), "Result");
        string errorMessage = FirstText(document, "ErrorMessage");
        string value = FirstText(document, valueField);

        return new SoapResult(
            action,
            result,
            errorMessage,
            new Dictionary<string, string> { [detailsField] = value },
            detailsField,
            value,
            SplitLines(value),
            xml);
    }

    private async Task<SoapResult> InvokeBinaryValueAsync(
        string action,
        string requestElement,
        IReadOnlyDictionary<string, string> requestFields,
        string valueField,
        CancellationToken cancellationToken)
    {
        var envelope = BuildEnvelope(requestElement, requestFields);

        using var request = new HttpRequestMessage(HttpMethod.Post, Endpoint)
        {
            Content = new StringContent(envelope, Encoding.UTF8, "text/xml")
        };

        request.Headers.TryAddWithoutValidation("SOAPAction", $"\"{action}\"");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/soap+xml"));

        using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        string xml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        XDocument document = XDocument.Parse(xml);

        int result = ParseInt(FirstText(document, "Result"), "Result");
        string errorMessage = FirstText(document, "ErrorMessage");
        string value = FirstText(document, valueField);

        string preview = string.Empty;
        int byteCount = 0;
        try
        {
            byte[] bytes = Convert.FromBase64String(value);
            byteCount = bytes.Length;
            preview = bytes.Length == 0 ? string.Empty : Convert.ToBase64String(bytes.Take(12).ToArray());
        }
        catch
        {
            preview = value.Length > 24 ? value[..24] : value;
        }

        var details = new Dictionary<string, string>
        {
            [$"{valueField}Length"] = byteCount > 0 ? byteCount.ToString() : value.Length.ToString(),
            [$"{valueField}Preview"] = string.IsNullOrWhiteSpace(preview) ? "(empty)" : preview
        };

        var lines = new List<string>();
        lines.Add(byteCount > 0 ? $"Binary data length: {byteCount} bytes" : "Binary data received.");
        if (!string.IsNullOrWhiteSpace(preview))
        {
            lines.Add($"Preview: {preview}");
        }

        return new SoapResult(
            action,
            result,
            errorMessage,
            details,
            valueField,
            lines[0],
            lines,
            xml);
    }

    private async Task<SoapResult> InvokeDescriptionGetAsync(int type, CancellationToken cancellationToken)
    {
        var envelope = BuildEnvelope(
            "SystemDescriptionGetRequest",
            new Dictionary<string, string> { ["Type"] = type.ToString() });

        using var request = new HttpRequestMessage(HttpMethod.Post, Endpoint)
        {
            Content = new StringContent(envelope, Encoding.UTF8, "text/xml")
        };

        request.Headers.TryAddWithoutValidation("SOAPAction", "\"SystemDescriptionGet\"");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/soap+xml"));

        using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        string xml = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        XDocument document = XDocument.Parse(xml);

        int result = ParseInt(FirstText(document, "Result"), "Result");
        string errorMessage = FirstText(document, "ErrorMessage");
        string label1 = FirstText(document, "Label1");
        string label2 = FirstText(document, "Label2");
        string label3 = FirstText(document, "Label3");

        var details = new Dictionary<string, string>
        {
            ["Label1"] = label1,
            ["Label2"] = label2,
            ["Label3"] = label3
        };

        var lines = new List<string>();
        if (!string.IsNullOrWhiteSpace(label1)) lines.Add(label1);
        if (!string.IsNullOrWhiteSpace(label2)) lines.Add(label2);
        if (!string.IsNullOrWhiteSpace(label3)) lines.Add(label3);

        return new SoapResult(
            "SystemDescriptionGet",
            result,
            errorMessage,
            details,
            "Description",
            string.Join(Environment.NewLine, lines),
            lines,
            xml);
    }

    private static string BuildEnvelope(string requestElement, IReadOnlyDictionary<string, string> fields)
    {
        var xml = new StringBuilder();
        xml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        xml.Append("<soap:Envelope xmlns:soap=\"").Append(SoapNamespace).Append("\" xmlns:tns=\"").Append(TargetNamespace).Append("\">");
        xml.Append("<soap:Body>");
        xml.Append("<tns:").Append(requestElement).Append(">");

        foreach (KeyValuePair<string, string> field in fields)
        {
            xml.Append("<tns:").Append(field.Key).Append(">");
            xml.Append(EscapeXml(field.Value));
            xml.Append("</tns:").Append(field.Key).Append(">");
        }

        xml.Append("</tns:").Append(requestElement).Append(">");
        xml.Append("</soap:Body>");
        xml.Append("</soap:Envelope>");
        return xml.ToString();
    }

    private static string FirstText(XDocument document, string localName)
    {
        return document
            .Descendants()
            .FirstOrDefault(element => element.Name.LocalName == localName)
            ?.Value
            .Trim() ?? string.Empty;
    }

    private static int ParseInt(string text, string fieldName)
    {
        if (int.TryParse(text, out int value))
        {
            return value;
        }

        throw new InvalidOperationException($"Invalid integer for {fieldName}: '{text}'");
    }

    private static double ParseDouble(string text, string fieldName)
    {
        if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
        {
            return value;
        }

        throw new InvalidOperationException($"Invalid number for {fieldName}: '{text}'");
    }

    private static List<string> SplitLines(string text)
    {
        return string.IsNullOrWhiteSpace(text)
            ? new List<string>()
            : text
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .Where(line => line.Length > 0)
                .ToList();
    }

    private static int TryDecodeLength(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return 0;
        }

        try
        {
            return Convert.FromBase64String(value).Length;
        }
        catch
        {
            return value.Length;
        }
    }

    private static string EscapeXml(string value)
    {
        return value
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&apos;");
    }
}
