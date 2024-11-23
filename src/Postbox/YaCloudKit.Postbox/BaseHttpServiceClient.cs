using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using YaCloudKit.Postbox.Model.Responses;

namespace YaCloudKit.Postbox;

internal abstract class BaseHttpServiceClient(HttpClient httpClient)
{
    private readonly AsyncRetryPolicy<HttpResponseMessage> retryPolicy =
        HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500));

    protected async Task<TResponse> ExecuteJsonAsync<TResponse>(
        Func<HttpClient, Task<HttpResponseMessage>> httpAction,
        CancellationToken cancellationToken)
    {
        return await ExecuteAsync(
            httpAction,
            async response =>
                await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken) ??
                throw new InvalidOperationException($"Can't deserialize response as {typeof(TResponse).Name}"));
    }

    private async Task<TResponse> ExecuteAsync<TResponse>(
        Func<HttpClient, Task<HttpResponseMessage>> httpAction,
        Func<HttpResponseMessage, Task<TResponse>> deserializeResponse)
    {
        HttpResponseMessage? response = null;
        try
        {
            try
            {
                response = await retryPolicy.ExecuteAsync(async () => await httpAction(httpClient));
            }
            catch (Exception e)
            {
                throw new YandexPostboxServiceException("Error while executing request", e);
            }

            if (response.IsSuccessStatusCode)
            {
                var result = await deserializeResponse(response);
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<YandexPostboxErrorResponse>();

                throw new YandexPostboxServiceException(
                    errorResponse?.Code,
                    errorResponse?.Message ?? "Unknown error",
                    response.StatusCode);
            }
        }
        finally
        {
            response?.Dispose();
        }
    }
}