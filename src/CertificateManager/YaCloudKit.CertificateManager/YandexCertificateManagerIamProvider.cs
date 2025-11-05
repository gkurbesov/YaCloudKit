namespace YaCloudKit.CertificateManager;

public interface IYandexCertificateManagerIamProvider
{
	ValueTask<string> GetIamTokenAsync(CancellationToken cancellationToken = default);
}

public class YandexCertificateManagerIamProvider(Func<CancellationToken, ValueTask<string>> iamTokenFunc)
	: IYandexCertificateManagerIamProvider
{
	public async ValueTask<string> GetIamTokenAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			return await iamTokenFunc(cancellationToken);
		}
		catch (Exception e)
		{
			throw new InvalidOperationException("Error while getting IAM token", e);
		}
	}
}