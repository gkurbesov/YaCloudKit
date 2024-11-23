using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace YaCloudKit.Postbox;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddYandexPostboxIamProvider(
        this IServiceCollection services,
        Func<IServiceProvider, CancellationToken, Task<string>> iamTokenFunc)
    {
        
        Func<IServiceProvider, IYandexPostboxIamProvider> factory = sp =>
        {
            return new YandexPostboxIamProvider(
                ct => iamTokenFunc(sp, ct));
        };
        services.TryAddSingleton(factory);
        return services;
    }
    
    public static IServiceCollection AddYandexPostboxClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services.Any(x => x.ServiceType == typeof(IYandexPostboxIamProvider)) == false)
            throw new InvalidOperationException("IYandexPostboxIamProvider is not registered");

        services
            .AddOptions<YandexPostboxOptions>()
            .Bind(configuration.GetSection(YandexPostboxOptions.SectionName));

        services.AddHttpClient<IYandexPostboxClient, YandexPostboxClient>(
            (sp, httpClient) =>
            {
                var options = sp.GetRequiredService<IOptionsMonitor<YandexPostboxOptions>>();
                httpClient.BaseAddress = new Uri(YandexPostboxDefaults.PostboxApiHost);

                httpClient.Timeout = TimeSpan.FromSeconds(options.CurrentValue.TimeoutSeconds ?? 30);
            });
        return services;
    }
}