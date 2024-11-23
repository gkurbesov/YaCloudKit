using YaCloudKit.IAM.Model;

namespace YaCloudKit.IAM;

public interface IYandexIamServiceClient
{
    Task<IamTokenResponse> GetIamForYandexAccountAsync(string yandexPassportOauthToken, CancellationToken cancellationToken = default);
    
    Task<IamTokenResponse> GetIamForServiceAccountAsync(CancellationToken cancellationToken = default);
}