namespace YaCloudKit.IAM.Rsa;

public class YandexFilePrivateKeyProvider(string privateKeyFilePath, bool cacheResult = false)
    : YandexCachedPrivateKeyProvider(cacheResult)
{
    protected override async Task<char[]> GetPrivateKeyCoreAsync(CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(privateKeyFilePath);
        return (await reader.ReadToEndAsync(cancellationToken)).ToCharArray();
    }
}