namespace YaCloudKit.IAM;

public record YandexIamOptions
{
    public const string SectionName = "YandexIam";
    
    public const string ApiHost = "https://iam.api.cloud.yandex.net";
    
    public string? ServiceAccountId { get; init; }

    public string? PublicKeyId { get; init; }
}