namespace YaCloudKit.IAM.Rsa;

public abstract class YandexCachedPrivateKeyProvider(bool cache = false) : IYandexPrivateKeyProvider, IDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private char[]? _privateKey;

    protected abstract Task<char[]> GetPrivateKeyCoreAsync(CancellationToken cancellationToken);


    public async Task<char[]> GetPrivateKeyAsync(CancellationToken cancellation)
    {
        if (_privateKey is not null)
        {
            return _privateKey;
        }

        await _semaphore.WaitAsync(cancellation);
        try
        {
            if (_privateKey is not null)
            {
                return _privateKey;
            }

            var localPrivateKey = await GetPrivateKeyCoreAsync(cancellation);
            if (cache)
            {
                _privateKey = localPrivateKey;
            }

            return _privateKey ?? localPrivateKey;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Dispose()
    {
        _semaphore.Dispose();
    }
}