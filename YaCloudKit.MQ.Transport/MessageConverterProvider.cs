using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public class MessageConverterProvider : IMessageConverterProvider
    {
        private readonly ConcurrentDictionary<string, IMessageConverter> converters = new ConcurrentDictionary<string, IMessageConverter>();

        public MessageConverterProvider() { }

        public IMessageConverter FirstOrDefault() =>
            converters.Count > 0 ? converters.Values.FirstOrDefault() : null;

        public IMessageConverter GetConverter(string tag) =>
            converters.ContainsKey(tag) ? converters[tag] : null;

        public IMessageConverterProvider Register(string tag, IMessageConverter converter)
        {
            if (converters.ContainsKey(tag))
                converters[tag] = converter;
            else
                converters.TryAdd(tag, converter);

            return this;
        }
    }
}
