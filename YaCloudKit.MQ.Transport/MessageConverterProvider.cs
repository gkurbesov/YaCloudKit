using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public class MessageConverterProvider : IMessageConverterProvider
    {
        private readonly Dictionary<string, IMessageConverter> converters = new Dictionary<string, IMessageConverter>();

        public MessageConverterProvider() { }

        public IMessageConverter GetConverter(string tag)
        {
            return converters.ContainsKey(tag) ? converters[tag] : null;
        }

        public void Register(string tag, IMessageConverter converter)
        {
            if (converters.ContainsKey(tag))
                converters[tag] = converter;
            else
                converters.Add(tag, converter);
        }
    }
}
