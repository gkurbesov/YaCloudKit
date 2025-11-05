using YaCloudKit.CertificateManager.Model;

namespace YaCloudKit.CertificateManager;

public interface IYandexCertificateContentClient
{
	Task<CertificateContentDto> GetCertificateContentAsync(string certificateId,
		CancellationToken cancellationToken = default);
}

public class YandexCertificateContentClient(HttpClient httpClient, IYandexCertificateManagerIamProvider iamProvider)
	: BaseHttpServiceClient(httpClient, iamProvider), IYandexCertificateContentClient
{
	public async Task<CertificateContentDto> GetCertificateContentAsync(
		string certificateId,
		CancellationToken cancellationToken = default)
	{
		return await ExecuteJsonAsync<CertificateContentDto>(
			async client =>
			{
				var response = await client.GetAsync(
					string.Format(YandexCertificateManagerDefaults.GetCertificateContentUrl, certificateId),
					cancellationToken);
				return response;
			},
			cancellationToken);
	}
}