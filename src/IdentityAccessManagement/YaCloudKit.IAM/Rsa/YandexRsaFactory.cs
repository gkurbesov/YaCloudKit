using System.Security.Cryptography;

namespace YaCloudKit.IAM.Rsa;

public interface IYandexRsaFactory
{
    Task<RSA> CreateRsaAsync(CancellationToken cancellationToken = default);
}

public class YandexRsaFactory(IYandexPrivateKeyProvider privateKeyProvider) : IYandexRsaFactory
{
    public async Task<RSA> CreateRsaAsync(CancellationToken cancellationToken = default)
    {
        var rsa = RSA.Create();
        var privateKey = await privateKeyProvider.GetPrivateKeyAsync(cancellationToken);
        rsa.ImportFromPem(privateKey);
        return rsa;
    }
}