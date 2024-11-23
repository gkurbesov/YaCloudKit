using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox;

public record YandexPostboxOptions
{
    public const string SectionName = "YandexPostbox";
    
    public int? TimeoutSeconds { get; init; }
}