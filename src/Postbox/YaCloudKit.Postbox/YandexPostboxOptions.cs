using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox;

public record YandexPostboxOptions
{
    public const string SectionName = "YandexPostbox";
    
    public const string PostboxApiHost = "https://postbox.cloud.yandex.net";

    public string? IamToken { get; init; }
    
    public int? TimeoutSeconds { get; init; }
}