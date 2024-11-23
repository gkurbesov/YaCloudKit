namespace YaCloudKit.IAM.Rsa;

public class YandexStaticPrivateKeyProvider : IYandexPrivateKeyProvider
{
    private readonly char[] _privateKey;

    public YandexStaticPrivateKeyProvider(string privateKey)
    {
        ArgumentNullException.ThrowIfNull(privateKey);
        _privateKey = privateKey.ToCharArray();
    }

    public Task<char[]> GetPrivateKeyAsync(CancellationToken _) => Task.FromResult(_privateKey);
}