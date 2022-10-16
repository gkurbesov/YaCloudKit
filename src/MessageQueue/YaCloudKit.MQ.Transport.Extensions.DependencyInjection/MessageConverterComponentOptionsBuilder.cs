using System;
using System.Collections.Generic;

namespace YaCloudKit.MQ.Transport.Extensions.DependencyInjection;

public class MessageConverterComponentOptionsBuilder
{
    private Dictionary<string, Type> _messageTypes = new();
    private List<IMessageConverter> _converters = new();

    public MessageConverterComponentOptionsBuilder WithMessageType(string name, Type type)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (type == null) throw new ArgumentNullException(nameof(type));
        
        _messageTypes.Add(name, type);

        return this;
    }

    public MessageConverterComponentOptionsBuilder WithMessageConverter(IMessageConverter converter)
    {
        if (converter == null) throw new ArgumentNullException(nameof(converter));
        _converters.Add(converter);
        return this;
    }

    public MessageConverterComponentOptions Build()
    {
        return new MessageConverterComponentOptions(_messageTypes, _converters);
    }
}