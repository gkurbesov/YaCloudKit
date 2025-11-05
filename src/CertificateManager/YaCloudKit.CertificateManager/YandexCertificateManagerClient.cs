using System.Net.Http.Json;
using YaCloudKit.CertificateManager.Model;

namespace YaCloudKit.CertificateManager;

public interface IYandexCertificateManagerClient
{
	Task<CertificateDto> GetCertificateAsync(string certificateId, CancellationToken cancellationToken = default);
}

public class YandexCertificateManagerClient(HttpClient httpClient, IYandexCertificateManagerIamProvider iamProvider)
	: BaseHttpServiceClient(httpClient, iamProvider), IYandexCertificateManagerClient
{
	public async Task<CertificateDto> GetCertificateAsync(string certificateId,
		CancellationToken cancellationToken = default)
	{
		return await ExecuteJsonAsync<CertificateDto>(
			async client =>
			{
				var response = await client.GetAsync(
					string.Format(YandexCertificateManagerDefaults.GetCertificateUrl, certificateId),
					cancellationToken);
				return response;
			},
			cancellationToken);
	}
}