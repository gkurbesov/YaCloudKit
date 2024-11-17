namespace YaCloudKit.IAM.Rsa;

public class YandexFuncPrivateKeyProvider(
    Func<CancellationToken, Task<char[]>> privateKeyFunc,
    bool cacheResult = false)
    : YandexCachedPrivateKeyProvider(cacheResult)
{
    protected override Task<char[]> GetPrivateKeyCoreAsync(CancellationToken cancellationToken)
    {
        return privateKeyFunc(cancellationToken);
    }
}