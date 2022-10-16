using Newtonsoft.Json;
using System;

namespace YaCloudKit.MQ.Transport;

public class JsonMessageConverter : IMessageConverter
{
    private readonly Formatting _formatting;
    private readonly JsonSerializerSettings _settings;
    public const string DefaultName = "json";

    public string Name => DefaultName;

    public JsonMessageConverter(Formatting formatting = Formatting.None, JsonSerializerSettings settings = null)
    {
        _formatting = formatting;
        _settings = settings;
    }

    public object Deserialize(string messageBody, Type type)
    {
        if (messageBody == null)
            throw new ArgumentNullException(nameof(messageBody));
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        return JsonConvert.DeserializeObject(messageBody, type);
    }

    public string Serialize(object value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        return JsonConvert.SerializeObject(value, _formatting, _settings);
    }
}