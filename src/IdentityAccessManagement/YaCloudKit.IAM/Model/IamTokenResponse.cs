using System.Text.Json.Serialization;

namespace YaCloudKit.IAM.Model;

public record IamTokenResponse
{
    [JsonPropertyName("iamToken")]
    public required string IamToken { get; init; }
    
    [JsonPropertyName("expiresAt")]
    public required DateTime ExpiresAt { get; init; }
}