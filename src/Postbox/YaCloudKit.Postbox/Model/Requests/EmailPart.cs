using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox.Model.Requests;

public record EmailDataPart(
    [property: JsonPropertyName("Data")] string Data,
    [property: JsonPropertyName("Charset")] string Charset
);