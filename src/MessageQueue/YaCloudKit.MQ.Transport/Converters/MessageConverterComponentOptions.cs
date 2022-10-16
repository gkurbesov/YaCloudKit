using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YaCloudKit.MQ.Transport;

public class MessageConverterComponentOptions
{
    public IReadOnlyDictionary<string, Type> MessageTypes { get; }

    public IReadOnlyDictionary<string, IMessageConverter> Converters { get; }

    public MessageConverterComponentOptions(
        IReadOnlyDictionary<string, Type> messageTypes,
        IEnumerable<IMessageConverter> converters)
    {
        if (messageTypes == null) throw new ArgumentNullException(nameof(messageTypes));
        if (!messageTypes.Any())
            throw new InvalidOperationException(
                $"Sequence contains no elements ({nameof(messageTypes)})");

        MessageTypes = messageTypes;

        if (converters == null) throw new ArgumentNullException(nameof(converters));
        if (!converters.Any())
            throw new InvalidOperationException($"Sequence contains no elements ({nameof(converters)})");

        var _converters = new Dictionary<string, IMessageConverter>();
        foreach (var converter in converters)
        {
            if (string.IsNullOrWhiteSpace(converter.Name))
                throw new InvalidOperationException($"Converter {converter.GetType()} returned empty name");

            _converters.Add(converter.Name, converter);
        }

        Converters = _converters;
    }
}