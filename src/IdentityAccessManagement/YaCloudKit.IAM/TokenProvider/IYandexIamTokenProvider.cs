using System.Security;

namespace YaCloudKit.IAM.TokenProvider;

public interface IYandexIamTokenProvider
{
    Task<string> GetServicesTokenAsync(CancellationToken cancellationToken = default);
}