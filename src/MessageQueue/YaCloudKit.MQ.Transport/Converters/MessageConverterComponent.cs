using System;
using System.Collections.Generic;
using System.Linq;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Transport;

public class MessageConverterComponent : IMessageConverterComponent
{
    private readonly MessageConverterComponentOptions _options;

    public MessageConverterComponent(MessageConverterComponentOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public object Deserialize(Message message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        if (string.IsNullOrWhiteSpace(message.Body))
            throw new MqTransportException($"Message {message.MessageId} has no body");

        if (!message.MessageAttribute.TryGetValue(MqTransportDefaults.AttributeMessageType,
                out var attrMessageType)
            || string.IsNullOrWhiteSpace(attrMessageType.StringValue))
            throw new MqTransportException(
                $"Message {message.MessageId} has no message type attribute ({MqTransportDefaults.AttributeMessageType})");

        if (!message.MessageAttribute.TryGetValue(MqTransportDefaults.AttributeMessageConverter,
                out var attrConverter)
            || string.IsNullOrWhiteSpace(attrConverter.StringValue))
            throw new MqTransportException(
                $"Message {message.MessageId} has no converter type attribute ({MqTransportDefaults.AttributeMessageConverter})");

        if (!_options.MessageTypes.TryGetValue(attrMessageType.StringValue, out var messageType))
            throw new MqTransportException($"Message type {attrMessageType.StringValue} is not registered");

        if (!_options.Converters.TryGetValue(attrConverter.StringValue, out var converter))
            throw new MqTransportException($"Converter {attrConverter.StringValue} is not registered");

        return converter.Deserialize(message.Body, messageType);
    }

    public void Serialize(string converterName, object value, in SendMessageRequest request)
    {
        if (string.IsNullOrWhiteSpace(converterName))
            throw new ArgumentNullException(nameof(converterName));

        if (value == null)
            throw new ArgumentNullException(nameof(value));

        var messageType = value.GetType();
        var messageTypeName = _options.MessageTypes.SingleOrDefault(t => t.Value == messageType).Key;

        if (string.IsNullOrWhiteSpace(messageTypeName))
            throw new MqTransportException($"Message type name for {messageType.Name} is not registered");

        if (!_options.Converters.TryGetValue(converterName, out var converter))
            throw new MqTransportException($"Converter {converterName} is not registered");

        request.MessageBody = converter.Serialize(value);
        request.MessageAttribute.Add(MqTransportDefaults.AttributeMessageType,
            new MessageAttributeValue()
            {
                DataType = AttributeValueType.String,
                StringValue = messageTypeName
            });
        request.MessageAttribute.Add(MqTransportDefaults.AttributeMessageConverter,
            new MessageAttributeValue()
            {
                DataType = AttributeValueType.String,
                StringValue = converter.Name
            });
    }
}