namespace YaCloudKit.CertificateManager;

public record YandexCertificateManagerOptions
{
	public const string SectionName = "YandexCertificateManager";
    
	public int? TimeoutSeconds { get; init; }
}