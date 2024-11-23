using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YaCloudKit.Postbox.Model.Requests;
using YaCloudKit.Postbox.Model.Responses;

namespace YaCloudKit.Postbox;

public class YandexPostboxClient(
    ILogger<YandexPostboxClient> logger,
    HttpClient httpClient,
    IYandexPostboxIamProvider iamProvider) : BaseHttpServiceClient(httpClient), IYandexPostboxClient
{
    public async Task<SendEmailResponse> SendMailAsync(
        SendEmailRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var iamToken = await iamProvider.GetIamTokenAsync(cancellationToken);
        
        LogSendEmailRequest(request);
        
        return await ExecuteJsonAsync<SendEmailResponse>(
            async client =>
            {
                client.DefaultRequestHeaders.Add("X-YaCloud-SubjectToken", iamToken);
                var response = await client.PostAsJsonAsync(YandexPostboxDefaults.SendEmailUrl, request, cancellationToken);
                return response;
            },
            cancellationToken);
    }
    
    private void LogSendEmailRequest(SendEmailRequest request)
    {
        var destination = string.Join(", ", request.Destination.ToAddresses);
        var subject = request.Content.Simple?.Subject.Data ?? "Empty";
        logger.LogDebug("Send email request: {FromEmailAddress} -> {Destination} Subject: {Subject}",
            request.FromEmailAddress,
            destination,
            subject);
    }
}