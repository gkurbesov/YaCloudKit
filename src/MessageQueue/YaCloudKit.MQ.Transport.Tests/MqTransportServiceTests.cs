using AutoFixture;
using FluentAssertions;
using Moq;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Transport.Converters;

namespace YaCloudKit.MQ.Transport.Tests;

public class MqTransportServiceTests
{
    private readonly MessageConverterComponent _component;
    private readonly Mock<IMessageHandler<TestMessage>> _messageHandlerMock;

    public MqTransportServiceTests()
    {
        var messageTypes = new Dictionary<string, Type>()
        {
            ["test"] = typeof(TestMessage)
        };

        var converters = new IMessageConverter[]
        {
            new JsonMessageConverter()
        };

        _component = new MessageConverterComponent(messageTypes, converters);
        _messageHandlerMock = new Mock<IMessageHandler<TestMessage>>(MockBehavior.Strict);
    }

    [Fact]
    public async Task HandleAsync_Worked()
    {
        var messageData = new Fixture().Create<TestMessage>();
        _messageHandlerMock.Setup(handler => handler.HandleAsync(
                It.Is<TestMessage>(m => m.UserName == messageData.UserName && m.Age == messageData.Age),
                It.IsAny<CancellationToken>()
            ))
            .Returns(Task.CompletedTask);
        var service = new MqTransportService(_component, _ => _messageHandlerMock.Object);

        var message = new Fixture()
            .Build<Message>()
            .With(m => m.Body, () => new JsonMessageConverter().Serialize(messageData))
            .Create();

        message.MessageAttribute
            .Add(MqTransportDefaults.AttributeMessageType, new MessageAttributeValue()
            {
                StringValue = "test",
                DataType = AttributeValueType.String
            });
        message.MessageAttribute
            .Add(MqTransportDefaults.AttributeMessageConverter, new MessageAttributeValue()
            {
                StringValue = JsonMessageConverter.DefaultName,
                DataType = AttributeValueType.String
            });

        await service.HandleAsync(message, CancellationToken.None);

        _messageHandlerMock.Verify();
    }
    
    [Fact]
    public async Task TransformAsync_Worked()
    {
        var messageData = new Fixture().Create<TestMessage>();
        var serializedMessageData = new JsonMessageConverter().Serialize(messageData);
        var service = new MqTransportService(_component, _ => null);

        var result = await service.TransformAsync(messageData, JsonMessageConverter.DefaultName, CancellationToken.None);
        
        result.MessageBody
            .Should()
            .BeEquivalentTo(serializedMessageData);
        result.MessageAttribute
            .Should()
            .Contain(x => x.Key == MqTransportDefaults.AttributeMessageType
                          && x.Value.StringValue == "test");
        result.MessageAttribute
            .Should()
            .Contain(x => x.Key == MqTransportDefaults.AttributeMessageConverter
                          && x.Value.StringValue == JsonMessageConverter.DefaultName);
    }
}