namespace YaCloudKit.MQ.Transport.Tests;

public class MqTransportServiceExtensionsTests
{
    [Fact]
    public void AddHandlersFromAssemblies_AllAreRegistered()
    {
        var services = new ServiceCollection();
        services.AddHandlersFromAssemblies(Assembly.GetExecutingAssembly());

        var provider = services.BuildServiceProvider();

        Assert.NotNull(provider.GetService(typeof(IMessageHandler<TestMessage>)));
        Assert.NotNull(provider.GetService(typeof(IMessageHandler<object>)));
    }
}