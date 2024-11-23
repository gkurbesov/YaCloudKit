namespace YaCloudKit.IAM.TokenProvider;

public class DefaultYandexIamTokenProvider(IYandexIamServiceClient client) : IYandexIamTokenProvider, IDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private string? _iamToken;
    private DateTime? _expiresAt;

    public async Task<string> GetServicesTokenAsync(CancellationToken cancellationToken = default)
    {
        if (_iamToken != null && _expiresAt != null && DateTime.UtcNow < _expiresAt)
            return _iamToken;
        
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            if(_iamToken != null && _expiresAt != null && DateTime.UtcNow < _expiresAt)
                return _iamToken;

            var response = await client.GetIamForServiceAccountAsync(cancellationToken);
            _iamToken = response.IamToken;
            _expiresAt = response.ExpiresAt;

            return _iamToken;
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