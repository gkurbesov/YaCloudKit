using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public interface IMessageConverter
    {
        T Deserialize<T>(string messageBody);
        object Deserialize(string messageBody, Type type);
        string Serialize(object value);
    }
}
