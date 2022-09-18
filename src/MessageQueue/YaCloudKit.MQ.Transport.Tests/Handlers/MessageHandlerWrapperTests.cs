using AutoFixture;
using FluentAssertions;
using Moq;

namespace YaCloudKit.MQ.Transport.Tests.Handlers;

public class MessageHandlerWrapperTests
{
    [Fact]
    public async Task HandlerNotRegistered_ThrowException()
    {
        var wrapper = new MessageHandlerWrapper<TestMessage>();
        var message = new Fixture().Create<TestMessage>();

        await wrapper.Awaiting(w => w.Handle(message, CancellationToken.None, _ => null))
            .Should()
            .ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Handle_Worked()
    {
        var wrapper = new MessageHandlerWrapper<TestMessage>();
        var message = new Fixture().Create<TestMessage>();


        var handlerMock = new Mock<IMessageHandler<TestMessage>>(MockBehavior.Strict);
        handlerMock.Setup(handler => handler.HandleAsync(
                It.Is<TestMessage>(inputMessage => inputMessage == message),
                It.IsAny<CancellationToken>()
            ))
            .Returns( Task.CompletedTask);

        await wrapper.Handle(message, CancellationToken.None, _ => handlerMock.Object);

        handlerMock.Verify();
    }
}