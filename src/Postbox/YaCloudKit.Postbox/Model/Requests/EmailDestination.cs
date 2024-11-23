using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox.Model.Requests;

public record EmailDestination(
    [property: JsonPropertyName("ToAddresses")] string[] ToAddresses,
    [property: JsonPropertyName("CcAddresses")] string[] CcAddresses,
    [property: JsonPropertyName("BccAddresses")] string[] BccAddresses
);
