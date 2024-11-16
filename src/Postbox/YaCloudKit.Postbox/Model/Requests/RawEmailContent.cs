using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox.Model.Requests;

public record RawEmailContent(
    [property: JsonPropertyName("Data")] string Data
);