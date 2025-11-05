using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace YaCloudKit.CertificateManager;

public abstract class BaseHttpServiceClient(HttpClient httpClient, IYandexCertificateManagerIamProvider iamProvider)
{
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
		var token = await iamProvider.GetIamTokenAsync();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		var response = await httpAction(httpClient);
		if (!response.IsSuccessStatusCode)
		{
			var exception = new HttpRequestException(HttpRequestError.InvalidResponse,
				statusCode: response.StatusCode);

			var content = await response.Content.ReadAsStringAsync();
			exception.Data["ResponseContent"] = content;

			throw exception;
		}

		var result = await deserializeResponse(response);
		return result;
	}
}