using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YaCloudKit.Postbox.Model.Requests;
using YaCloudKit.Postbox.Model.Responses;

namespace YaCloudKit.Postbox;

public class YandexPostboxClient(
    ILogger<YandexPostboxClient> logger,
    HttpClient httpClient) : IYandexPostboxClient
{
    public Task<SendEmailResponse> SendMailAsync(
        SendEmailRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}