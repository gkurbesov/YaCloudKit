using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace YaCloudKit.Postbox;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddYandexPostboxClient(
        this IServiceCollection services,
        string iamToken,
        TimeSpan? timeout = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddHttpClient<IYandexPostboxClient, YandexPostboxClient>(
            (sp, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(YandexPostboxOptions.PostboxApiHost);
                httpClient.DefaultRequestHeaders.Add("X-YaCloud-SubjectToken", iamToken);
                httpClient.Timeout = timeout ?? TimeSpan.FromSeconds(30);
            });
        return services;
    }

    public static IServiceCollection AddYandexPostboxClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<YandexPostboxOptions>()
            .Bind(configuration.GetSection(YandexPostboxOptions.SectionName));

        services.AddHttpClient<IYandexPostboxClient, YandexPostboxClient>(
            (sp, httpClient) =>
            {
                var options = sp.GetRequiredService<IOptionsMonitor<YandexPostboxOptions>>();
                httpClient.BaseAddress = new Uri(YandexPostboxOptions.PostboxApiHost);

                var iamToken = options.CurrentValue.IamToken ??
                               throw new InvalidOperationException("IAM Token is required for Yandex Postbox API");

                httpClient.DefaultRequestHeaders.Add("X-YaCloud-SubjectToken", iamToken);
                httpClient.Timeout = TimeSpan.FromSeconds(options.CurrentValue.TimeoutSeconds ?? 30);
            });
        return services;
    }
}