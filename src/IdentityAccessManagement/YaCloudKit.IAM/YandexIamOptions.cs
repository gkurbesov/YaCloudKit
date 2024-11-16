namespace YaCloudKit.IAM;

public record YandexIamOptions
{
    public const string SectionName = "YandexIam";
    
    public string? ServiceAccountId { get; init; }

    public string? PublicKeyId { get; init; }
}