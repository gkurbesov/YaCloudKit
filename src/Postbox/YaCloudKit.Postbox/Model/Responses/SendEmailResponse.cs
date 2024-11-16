using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox.Model.Responses;

public record SendEmailResponse : YandexPostboxBaseResponse
{
    [JsonPropertyName("MessageId")] public string? MessageId { get; init; }
}