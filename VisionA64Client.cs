using System.Net.Http.Headers;
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
