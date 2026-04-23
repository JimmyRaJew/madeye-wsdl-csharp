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
