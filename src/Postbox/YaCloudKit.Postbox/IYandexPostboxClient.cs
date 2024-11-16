using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.Postbox.Model.Requests;
using YaCloudKit.Postbox.Model.Responses;

namespace YaCloudKit.Postbox;

public interface IYandexPostboxClient
{
    Task<SendEmailResponse> SendMailAsync(SendEmailRequest request, CancellationToken cancellationToken = default);
}