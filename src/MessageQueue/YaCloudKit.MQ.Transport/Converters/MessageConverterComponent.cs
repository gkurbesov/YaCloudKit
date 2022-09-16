using System;
using System.Collections.Generic;
using System.Linq;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Transport.Converters;

public class MessageConverterComponent : IMessageConverterComponent
{
    private readonly IDictionary<string, Type> _messageTypes;
    private readonly Dictionary<string, IMessageConverter> _converters = new();

    public MessageConverterComponent(IDictionary<string, Type> messageTypes, IEnumerable<IMessageConverter> converters)
    {
        if (converters == null)
            throw new ArgumentNullException(nameof(converters));

        _messageTypes = messageTypes ?? throw new ArgumentNullException(nameof(messageTypes));

        foreach (var converter in converters)
        {
            if (string.IsNullOrWhiteSpace(converter.Name))
                throw new InvalidOperationException($"Converter {converter.GetType()} returned empty name");

            _converters.Add(converter.Name, converter);
        }
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

        if (!_messageTypes.TryGetValue(attrMessageType.StringValue, out var messageType))
            throw new MqTransportException($"Message type {attrMessageType.StringValue} is not registered");

        if (!_converters.TryGetValue(attrConverter.StringValue, out var converter))
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
        var messageTypeName = _messageTypes.SingleOrDefault(t => t.Value == messageType).Key;
        
        if(string.IsNullOrWhiteSpace(messageTypeName))
            throw new MqTransportException($"Message type name for {messageType.Name} is not registered");
        
        if(!_converters.TryGetValue(converterName, out var converter))
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