using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox.Model.Requests;

public record SendEmailRequest(
    [property: JsonPropertyName("FromEmailAddress")]
    string FromEmailAddress,
    [property: JsonPropertyName("Destination")]
    EmailDestination Destination,
    [property: JsonPropertyName("Content")]
    EmailContent Content);

