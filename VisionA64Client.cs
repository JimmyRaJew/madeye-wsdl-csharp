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

