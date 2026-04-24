namespace MadeyeWsdlCSharp;

public sealed record DescriptionSetRequest(string? Label1, string? Label2, string? Label3);
public sealed record UserIdentifyAddRequest(
    string? BadgeID,
    string? FaceDataBase64,
    int RelayActive,
    int RelayStrike,
    int WiegandActive,
    string? WiegandData,
    int WiegandLength);

public sealed record UserBadgeRequest(string? BadgeID);
public sealed record UserTypeRequest(int Type);
public sealed record UserIdentifyRestrictEnableRequest(int Status);
public sealed record UserIdentifyTimeActivateRequest(string? BadgeID, string? StartTime, string? EndTime);
public sealed record SystemDesfireSetRequest(
    int KeyType,
    int KeySize,
    string? KeyMaster,
    string? KeyApplication,
    string? KeyReadWrite,
    string? KeyReadOnly,
    int ApplicationID,
    int UserFileType,
    int UserFileNumber,
    int UserFileSize,
    int FaceFileType,
    int FaceFileNumber,
    int FaceFileSize,
    int KeyMasterNumber,
    int KeyApplicationNumber,
    int KeyReadWriteNumber,
    int KeyReadOnlyNumber);

public sealed record SystemDesfireSecondarySetRequest(
    string? KeyReadWriteSecondary,
    string? KeyReadOnlySecondary,
    int KeyReadWriteNumberSecondary,
    int KeyReadOnlyNumberSecondary,
    int UserSecondary,
    int FaceSecondary);

public sealed record UserIdentifyAddMultiRequest(int UserIdentifyCount, string? UserIdentifyDataBase64, int UserIdentifyOverwrite);
public sealed record UserDatabaseSetRequest(string? SqlDataBase64, string? SqlChecksum);
public sealed record UserImageRequest(string? BadgeID, string? ImageDataBase64);
public sealed record UserElevatorAddRequest(string? BadgeID, string? ModuleAddress, string? ModuleType, string? RelayList);
public sealed record UserElevatorAddMultiRequest(string? Badges, string? ModuleAddresses, string? ModuleTypes, string? RelayLists, int Count, int Overwrite);
public sealed record UserBadgeWiegandAddRequest(string? BadgeID, string? WiegandData, int WiegandLength);
public sealed record UserBadgeWiegandDeleteRequest(int Type, string? BadgeID, string? WiegandData);

public sealed record SystemMifareSetRequest(
    string? KeyA,
    string? KeyB,
    int UserStart,
    int UserSize,
    int UserKey,
    int UserFormat,
    int FaceStart,
    int FaceSize,
    int FaceKey);

public sealed record SystemWiegandSetRequest(
    int InputEnable,
    int OutputEnable,
    int OutputType,
    int OutputPulseWidth,
    int OutputPulsePeriod,
    int OutputFailEnable,
    int OutputFailStartBit,
    int OutputFailLength,
    long OutputFailCode,
    string? ServiceEndpoint,
    int ServiceTimeout,
    int WebOrTCP,
    string? TCPAddress,
    int TCPPort,
    int CardNumberEnable,
    int CardNumberStart,
    int CardNumberLength,
    string? IdentifyFailCode,
    int IdentifyFailLength);

public sealed record SystemLogSetRequest(int Enable, int Type);
public sealed record TypeRequest(int Type);
public sealed record SystemFaceSetRequest(
    double MatchThreshold,
    double DetectAGS,
    double DetectPitch,
    double DetectYaw,
    double DetectRoll,
    double DetectLiveness,
    int FaceAttempts,
    int FaceTimeout,
    int ExtractQualityOverride,
    int License,
    int MultiFaces,
    int WithMinimumSize,
    int MinimumFaceSize);
public sealed record FaceImageRequest(int ImageType, string? ImageDataBase64);
public sealed record FaceVerifyRequest(string? FaceA, string? FaceB);
public sealed record FaceExtractDuplicateRequest(string? ImageDataBase64, int Rotation);
public sealed record SystemVideoSetRequest(
    int DetectPeriod,
    int FrameWidth,
    int FrameHeight,
    int FrameRotatIon,
    int CameraMode,
    int PowerMode,
    int WithCrop,
    int CropX,
    int CropY,
    int CropWidth,
    int CropHeight,
    int Liveness);
public sealed record VideoFaceCaptureRequest(int Timeout, int WithRGB, int WithNIR);
public sealed record VideoFaceMatchRequest(int Timeout, string? UserTemplateBase64);

public sealed record SmartcardTimeoutTypeRequest(int Timeout, int Type);
public sealed record SmartcardTimeoutRequest(int Timeout);
public sealed record SmartcardDesfireWriteRequest(int Timeout, int Type, string? UserData, string? FaceDataBase64);
public sealed record SmartcardMifareWriteRequest(int Timeout, string? UserData, string? FaceDataBase64);
public sealed record SmartcardBadgeWriteRequest(int Timeout, string? Badge);
public sealed record SmartcardFaceWriteRequest(int Timeout, string? FaceDataBase64);
public sealed record SmartcardTypeRequest(int Type);
