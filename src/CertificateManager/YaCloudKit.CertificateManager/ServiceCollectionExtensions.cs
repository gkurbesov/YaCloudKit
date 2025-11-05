using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace YaCloudKit.CertificateManager;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddYandexCertificateManagerIamProvider(
		this IServiceCollection services,
		Func<IServiceProvider, CancellationToken, ValueTask<string>> iamTokenFunc)
	{
		Func<IServiceProvider, IYandexCertificateManagerIamProvider> factory = sp =>
		{
			return new YandexCertificateManagerIamProvider(ct => iamTokenFunc(sp, ct));
		};
		services.TryAddSingleton(factory);
		return services;
	}

	public static OptionsBuilder<YandexCertificateManagerOptions> AddYandexCertificateManagerOptions(
		this IServiceCollection services, IConfiguration configuration)
	{
		return services
			.AddOptions<YandexCertificateManagerOptions>()
			.Bind(configuration.GetSection(YandexCertificateManagerOptions.SectionName));
	}

	public static IHttpClientBuilder AddYandexCertificateManagerClient(this IServiceCollection services)
	{
		if (services.Any(x => x.ServiceType == typeof(IYandexCertificateManagerIamProvider)) == false)
			throw new InvalidOperationException("IYandexCertificateManagerIamProvider is not registered");

		return services
			.AddHttpClient<IYandexCertificateManagerClient, YandexCertificateManagerClient>((sp, httpClient) =>
			{
				var options = sp.GetRequiredService<IOptionsMonitor<YandexCertificateManagerOptions>>();
				httpClient.BaseAddress = new Uri(YandexCertificateManagerDefaults.CertManagerApiHost);

				httpClient.Timeout = TimeSpan.FromSeconds(options.CurrentValue.TimeoutSeconds ?? 30);
			});
	}

	public static IHttpClientBuilder AddYandexCertificateContentClient(this IServiceCollection services)
	{
		if (services.Any(x => x.ServiceType == typeof(IYandexCertificateManagerIamProvider)) == false)
			throw new InvalidOperationException("IYandexCertificateManagerIamProvider is not registered");

		return services
			.AddHttpClient<IYandexCertificateContentClient, YandexCertificateContentClient>((sp, httpClient) =>
			{
				var options = sp.GetRequiredService<IOptionsMonitor<YandexCertificateManagerOptions>>();
				httpClient.BaseAddress = new Uri(YandexCertificateManagerDefaults.CertManagerDataApiHost);

				httpClient.Timeout = TimeSpan.FromSeconds(options.CurrentValue.TimeoutSeconds ?? 30);
			});
	}
}