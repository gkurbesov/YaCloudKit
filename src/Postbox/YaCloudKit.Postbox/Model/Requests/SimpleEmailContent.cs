using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox.Model.Requests;

public record SimpleEmailContent(
    [property: JsonPropertyName("Subject")] EmailDataPart Subject,
    [property: JsonPropertyName("Body")] EmailBody Body
);