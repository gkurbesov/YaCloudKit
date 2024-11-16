using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox.Model.Responses;

public record YandexPostboxErrorResponse
{
    public string? Code { get; init; }
    
    public string? Message { get; init; }
}