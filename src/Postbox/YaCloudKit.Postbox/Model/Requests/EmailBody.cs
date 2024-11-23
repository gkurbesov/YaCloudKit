using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox.Model.Requests;

public record EmailBody(
    [property: JsonPropertyName("Text")] EmailDataPart? Text,
    [property: JsonPropertyName("Html")] EmailDataPart? Html
);