namespace YaCloudKit.IAM.Rsa;

public interface IYandexPrivateKeyProvider
{
    Task<char[]> GetPrivateKeyAsync(CancellationToken cancellationToken = default);
}