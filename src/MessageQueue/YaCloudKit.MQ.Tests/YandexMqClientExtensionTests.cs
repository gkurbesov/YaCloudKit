using Microsoft.Extensions.DependencyInjection;
using Xunit;
using YaCloudKit.MQ.Extensions.DependencyInjection;

namespace YaCloudKit.MQ.Tests;

public class YandexMqClientExtensionTests
{
    [Fact]
    public void AddWithParameters_ClientResolved()
    {
        var services = new ServiceCollection();
        services.AddYandexMqClient("access_id", "secret_key");
        var provider = services.BuildServiceProvider();

        var client = provider.GetRequiredService<IYandexMq>();
        
        Assert.NotNull(client);
    }
    
    [Fact]
    public void AddWithFactory_ClientResolved()
    {
        var services = new ServiceCollection();
        services.AddYandexMqClient(_ => new YandexMqConfig("access_id", "secret_key"));
        var provider = services.BuildServiceProvider();

        var client = provider.GetRequiredService<IYandexMq>();
        
        Assert.NotNull(client);
    }
}