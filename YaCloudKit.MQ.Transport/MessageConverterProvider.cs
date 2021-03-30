using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Transport.Converters;

namespace YaCloudKit.MQ.Transport
{
    public class MessageConverterProvider : IMessageConverterProvider
    {
        private readonly ConcurrentDictionary<string, IMessageConverter> converters = new ConcurrentDictionary<string, IMessageConverter>();

        public MessageConverterProvider() { }

        public IEnumerable<IMessageConverter> Values { get => converters.Values; }

        public IMessageConverter FirstOrDefault() =>
            converters.Count > 0 ? converters.Values.FirstOrDefault() : null;

        public IMessageConverter GetConverter(string tag) =>
            converters.ContainsKey(tag) ? converters[tag] : null;

        public string GetTag(IMessageConverter converter)
        {
            foreach (var item in converters)
                if (item.Value.Equals(converter))
                    return item.Key;
            return null;
        }

        public IMessageConverterProvider Register(string tag, IMessageConverter converter)
        {
            if (converters.ContainsKey(tag))
                converters[tag] = converter;
            else
                converters.TryAdd(tag, converter);

            return this;
        }

        public IMessageConverter GetDefault() => 
            GetConverter(JsonMessageConverter.TAG) ?? GetConverter(XmlMessageConverter.TAG);
    }
}
