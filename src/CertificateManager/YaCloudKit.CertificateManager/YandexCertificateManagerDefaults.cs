namespace YaCloudKit.CertificateManager;

public static class YandexCertificateManagerDefaults
{
	public const string CertManagerApiHost = "https://certificate-manager.api.cloud.yandex.net";
	public const string CertManagerDataApiHost = "https://data.certificate-manager.api.cloud.yandex.net";
	
	public const string GetCertificateUrl = "/certificate-manager/v1/certificates/{0}";
	public const string GetCertificateContentUrl = "/certificate-manager/v1/certificates/{0}:getContent";
}