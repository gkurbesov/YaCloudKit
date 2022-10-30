namespace YaCloudKit.MQ.Transport.Tests;

public class MessageConverterComponentTests
{
    private readonly MessageConverterComponent _component;

    public MessageConverterComponentTests()
    {
        var messageTypes = new Dictionary<string, Type>()
        {
            ["test"] = typeof(TestMessage)
        };

        var converters = new IMessageConverter[]
        {
            new JsonMessageConverter()
        };

        var options = new MessageConverterComponentOptions(messageTypes, converters);
        
        _component = new MessageConverterComponent(options);
    }

    [Fact]
    public void Deserialize_MessageBodyIsEmpty_ThrowException()
    {
        var message = new Fixture()
            .Build<Message>()
            .Without(m => m.Body)
            .Create();

        var act = () => _component.Deserialize(message);

        act.Should()
            .Throw<MqTransportException>()
            .WithMessage($"Message {message.MessageId} has no body");
    }

    [Fact]
    public void Deserialize_WithoutAttributeMessageType_ThrowException()
    {
        var message = new Fixture()
            .Build<Message>()
            .Create();

        var act = () => _component.Deserialize(message);

        act.Should()
            .Throw<MqTransportException>()
            .WithMessage(
                $"Message {message.MessageId} has no message type attribute ({MqTransportDefaults.AttributeMessageType})");
    }

    [Fact]
    public void Deserialize_WithoutAttributeMessageConverter_ThrowException()
    {
        var message = new Fixture()
            .Build<Message>()
            .Create();

        message.MessageAttribute
            .Add(MqTransportDefaults.AttributeMessageType, new MessageAttributeValue()
            {
                StringValue = "message_type",
                DataType = AttributeValueType.String
            });

        var act = () => _component.Deserialize(message);

        act.Should()
            .Throw<MqTransportException>()
            .WithMessage(
                $"Message {message.MessageId} has no converter type attribute ({MqTransportDefaults.AttributeMessageConverter})");
    }


    [Fact]
    public void Deserialize_MessageTypeNotRegistered_ThrowException()
    {
        var messageTypeName = "message_type";

        var message = new Fixture()
            .Build<Message>()
            .Create();

        message.MessageAttribute
            .Add(MqTransportDefaults.AttributeMessageType, new MessageAttributeValue()
            {
                StringValue = messageTypeName,
                DataType = AttributeValueType.String
            });
        message.MessageAttribute
            .Add(MqTransportDefaults.AttributeMessageConverter, new MessageAttributeValue()
            {
                StringValue = "converter_name",
                DataType = AttributeValueType.String
            });

        var act = () => _component.Deserialize(message);

        act.Should()
            .Throw<MqTransportException>()
            .WithMessage($"Message type {messageTypeName} is not registered");
    }

    [Fact]
    public void Deserialize_ConverterNotRegistered_ThrowException()
    {
        var converterName = "converter_name";

        var message = new Fixture()
            .Build<Message>()
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
                StringValue = converterName,
                DataType = AttributeValueType.String
            });

        var act = () => _component.Deserialize(message);

        act.Should()
            .Throw<MqTransportException>()
            .WithMessage($"Converter {converterName} is not registered");
    }

    [Fact]
    public void Message_Deserialized()
    {
        var messageData = new Fixture().Create<TestMessage>();

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

        _component.Deserialize(message)
            .Should()
            .BeEquivalentTo(messageData);
    }

    [Fact]
    public void Message_Serialized()
    {
        var messageData = new Fixture().Create<TestMessage>();
        var serializedMessageData = new JsonMessageConverter().Serialize(messageData);
        var request = new SendMessageRequest();

        _component.Serialize(JsonMessageConverter.DefaultName, messageData, in request);

        request.MessageBody
            .Should()
            .BeEquivalentTo(serializedMessageData);
        request.MessageAttribute
            .Should()
            .Contain(x => x.Key == MqTransportDefaults.AttributeMessageType
                          && x.Value.StringValue == "test");
        request.MessageAttribute
            .Should()
            .Contain(x => x.Key == MqTransportDefaults.AttributeMessageConverter
                          && x.Value.StringValue == JsonMessageConverter.DefaultName);
    }
}