using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox.Model.Requests;

public record EmailContent(
    [property: JsonPropertyName("Simple")] SimpleEmailContent? Simple,
    [property: JsonPropertyName("Raw")] RawEmailContent? Raw
);