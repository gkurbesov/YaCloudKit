using System.Net.Http.Json;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using YaCloudKit.IAM.Jwt;
using YaCloudKit.IAM.Model;

namespace YaCloudKit.IAM;

public class YandexIamServiceClient(HttpClient httpClient, YandexJsonWebTokenGenerator? jsonWebTokenGenerator)
    : IYandexIamServiceClient
{
    private const string RequestUri = "iam/v1/tokens";
    
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy =
        HttpPolicyExtensions.HandleTransientHttpError().RetryAsync(2);

    public async Task<IamTokenResponse> GetIamForYandexAccountAsync(
        string yandexPassportOauthToken,
        CancellationToken cancellationToken = default)
    {
        var payload = new Dictionary<string, object>
        {
            { "yandexPassportOauthToken", yandexPassportOauthToken }
        };

        return await ExecuteJsonAsync<IamTokenResponse>(
            async client => await client.PostAsJsonAsync(RequestUri, payload, cancellationToken),
            cancellationToken);
    }

    public async Task<IamTokenResponse> GetIamForServiceAccountAsync(CancellationToken cancellationToken = default)
    {
        if(jsonWebTokenGenerator is null)
            throw new InvalidOperationException("JsonWebTokenGenerator is not provided");
        var jwt = await jsonWebTokenGenerator.GenerateJwtAsync(cancellationToken);

        var payload = new Dictionary<string, object>
        {
            { "jwt", jwt }
        };

        return await ExecuteJsonAsync<IamTokenResponse>(
            async client => await client.PostAsJsonAsync(RequestUri, payload, cancellationToken),
            cancellationToken);
    }

    private async Task<TResponse> ExecuteJsonAsync<TResponse>(
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
        using var response = await _retryPolicy.ExecuteAsync(
            async () => await httpAction(httpClient));

        response.EnsureSuccessStatusCode();

        var result = await deserializeResponse(response);

        return result;
    }
}