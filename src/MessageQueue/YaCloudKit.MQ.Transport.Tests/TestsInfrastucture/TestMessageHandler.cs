namespace YaCloudKit.MQ.Transport.Tests;

public class TestMessageHandler : IMessageHandler<TestMessage>, IMessageHandler<object>
{
    public Task HandleAsync(TestMessage message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(object message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}