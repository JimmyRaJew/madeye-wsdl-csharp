namespace MadeyeWsdlCSharp;

public sealed record SoapResult(
    string Operation,
    int Result,
    string ErrorMessage,
    IReadOnlyDictionary<string, string> Details,
    string ReportLabel,
    string ReportText,
    IReadOnlyList<string> ReportLines,
    string RawXml);

