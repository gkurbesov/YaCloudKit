using System;
using Microsoft.Extensions.DependencyInjection;

namespace YaCloudKit.MQ.Extensions.DependencyInjection;

public static class YandexMqClientExtension
{
    public static IServiceCollection AddYandexMqClient(this IServiceCollection services, string accessKeyId, string secretAccessKey)
    {
        services.AddHttpClient<IYandexMq, YandexMqClient>(client =>
            new YandexMqClient(
                config: new YandexMqConfig(accessKeyId, secretAccessKey),
                httpClientFactory: () => client));
        
        return services;
    }
    
    public static IServiceCollection AddYandexMqClient(this IServiceCollection services, Func<IServiceProvider, YandexMqConfig> configFactory)
    {
        services.AddHttpClient<IYandexMq, YandexMqClient>((client, serviceProvider) =>
            new YandexMqClient(
                config: configFactory(serviceProvider),
                httpClientFactory: () => client));
        
        return services;
    }
}