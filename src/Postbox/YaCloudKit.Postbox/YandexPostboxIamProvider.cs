using System;
using System.Threading;
using System.Threading.Tasks;

namespace YaCloudKit.Postbox;

public interface IYandexPostboxIamProvider
{
    Task<string> GetIamTokenAsync(CancellationToken cancellationToken = default);
}

public class YandexPostboxIamProvider(Func<CancellationToken, Task<string>> iamTokenFunc) : IYandexPostboxIamProvider
{
    public async Task<string> GetIamTokenAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await iamTokenFunc(cancellationToken);
        }
        catch (Exception e)
        {
            throw new YandexPostboxServiceException("Error while getting IAM token", e);
        }
    }
}