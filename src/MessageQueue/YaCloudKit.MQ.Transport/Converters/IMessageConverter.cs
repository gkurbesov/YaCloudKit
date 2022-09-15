using System;

namespace YaCloudKit.MQ.Transport;

public interface IMessageConverter
{
    string Name { get; }

    object Deserialize(string messageBody, Type type);

    string Serialize(object value);
}