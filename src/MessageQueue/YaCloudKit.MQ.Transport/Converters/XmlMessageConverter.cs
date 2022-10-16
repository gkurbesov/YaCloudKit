using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace YaCloudKit.MQ.Transport;

public class XmlMessageConverter : IMessageConverter
{
    public const string DefaultName = "xml";

    public string Name => DefaultName;

    public object Deserialize(string messageBody, Type type)
    {
        if (messageBody == null)
            throw new ArgumentNullException(nameof(messageBody));
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        var serializer = new XmlSerializer(type);
        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(messageBody));
        var result = serializer.Deserialize(ms);
        return result;
    }

    public string Serialize(object value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        var serializer = new XmlSerializer(value.GetType());
        using var ms = new MemoryStream();
        serializer.Serialize(ms, value);
        return Encoding.UTF8.GetString(ms.ToArray());
    }
}