using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace YaCloudKit.MQ.Transport.Converters
{
    public class JsonMessageConverter : IMessageConverter
    {
        public T Deserialize<T>(string messageBody) where T : class
        {
            return JsonConvert.DeserializeObject<T>(messageBody);
        }

        public object Deserialize(string messageBody, Type type)
        {
            return JsonConvert.DeserializeObject(messageBody, type);
        }

        public string Serialize(object value)
        {
            return Serialize(value, Formatting.None);
        }

        public string Serialize(object value, Formatting formatting, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(value, formatting, settings);
        }
    }
}
