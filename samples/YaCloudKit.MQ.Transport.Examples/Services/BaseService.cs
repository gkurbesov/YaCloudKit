using Microsoft.Extensions.DependencyInjection;

namespace YaCloudKit.MQ.Transport.Examples;

public abstract class BaseService
{
    private IServiceProvider? _serviceProvider;

    protected IServiceProvider ServiceProvider =>
        _serviceProvider ?? throw new InvalidOperationException("Services not configured");

    public BaseService()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    protected abstract void ConfigureServices(IServiceCollection services);
}