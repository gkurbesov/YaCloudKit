using System;
using Microsoft.Extensions.DependencyInjection;

namespace YaCloudKit.TTS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddYandexTtsClient(this IServiceCollection services, string apiKey)
        {
            services.AddHttpClient<IYandexTts, YandexTtsClient>(client =>
                new YandexTtsClient(
                    config: new YandexTtsConfig(apiKey),
                    httpClientFactory: () => client));

            return services;
        }

        public static IServiceCollection AddYandexTtsClient(this IServiceCollection services, string iam, string folderId)
        {
            services.AddHttpClient<IYandexTts, YandexTtsClient>(client =>
                new YandexTtsClient(
                    config: new YandexTtsConfig(iam, folderId),
                    httpClientFactory: () => client));

            return services;
        }

        public static IServiceCollection AddYandexTtsClient(this IServiceCollection services,
            Func<IServiceProvider, YandexTtsConfig> configFactory)
        {
            services.AddHttpClient<IYandexTts, YandexTtsClient>((client, serviceProvider) =>
                new YandexTtsClient(
                    config: configFactory(serviceProvider),
                    httpClientFactory: () => client));

            return services;
        }
    }
}