using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using YaCloudKit.IAM.Jwt;
using YaCloudKit.IAM.Rsa;

namespace YaCloudKit.IAM;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddYandexFilePrivateKeyProvider(
        this IServiceCollection services,
        string privateKeyFilePath,
        bool cacheResult = false)
    {
        services.TryAddSingleton<IYandexPrivateKeyProvider>(
            new YandexFilePrivateKeyProvider(privateKeyFilePath, cacheResult));
        return services;
    }

    public static IServiceCollection AddYandexStaticPrivateKeyProvider(
        this IServiceCollection services,
        string privateKey)
    {
        services.TryAddSingleton<IYandexPrivateKeyProvider>(
            new YandexStaticPrivateKeyProvider(privateKey));
        return services;
    }

    public static IServiceCollection AddYandexFuncPrivateKeyProvider(
        this IServiceCollection services,
        Func<IServiceProvider, CancellationToken, Task<char[]>> privateKeyFunc,
        bool cacheResult = false)
    {
        Func<IServiceProvider, IYandexPrivateKeyProvider> factory = sp =>
        {
            return new YandexFuncPrivateKeyProvider(
                ct => privateKeyFunc(sp, ct), cacheResult);
        };
        services.TryAddSingleton(factory);
        return services;
    }

    public static IServiceCollection AddYandexFuncPrivateKeyProvider(
        this IServiceCollection services,
        Func<CancellationToken, Task<char[]>> privateKeyFunc,
        bool cacheResult = false)
    {
        services.TryAddSingleton<IYandexPrivateKeyProvider>(
            new YandexFuncPrivateKeyProvider(privateKeyFunc, cacheResult));
        return services;
    }

    public static IServiceCollection AddYandexJwtGenerationServices(
        this IServiceCollection services,
        IConfiguration configuration,
        string optionsSectionName = YandexIamOptions.SectionName)
    {
        services.AddOptions<YandexIamOptions>()
            .Bind(configuration.GetSection(optionsSectionName));
        services.TryAddSingleton<IYandexRsaFactory, YandexRsaFactory>();
        services.TryAddSingleton<YandexJsonWebTokenGenerator>();
        return services;
    }

    public static IServiceCollection AddYandexIamServiceClient(
        this IServiceCollection services,
        TimeSpan? requestTimeout = null)
    {
        services
            .AddHttpClient<IYandexIamServiceClient, YandexIamServiceClient>(
                (sp, httpClient) =>
                {
                    httpClient.BaseAddress = new Uri(YandexIamOptions.ApiHost);
                    httpClient.Timeout = requestTimeout ?? TimeSpan.FromSeconds(10);
                });
        return services;
    }
    
    public static IServiceCollection AddDefaultYandexIamServiceClient(
        this IServiceCollection services,
        IConfiguration configuration,
        TimeSpan? requestTimeout = null)
    {
        services.AddYandexJwtGenerationServices(configuration);
        services.AddYandexIamServiceClient(requestTimeout);
        return services;
    }
}