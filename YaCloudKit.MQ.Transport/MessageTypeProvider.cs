using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public class MessageTypeProvider : IMessageTypeProvider
    {
        private readonly ConcurrentDictionary<string, Type> types = new ConcurrentDictionary<string, Type>();

        public MessageTypeProvider() { }

        public Type GetMessageType(string tag)
        {
            return types.ContainsKey(tag) ? types[tag] : null;
        }

        public IMessageTypeProvider Register<T>(string tag)
        {
            Register(tag, typeof(T));
            return this;
        }

        public IMessageTypeProvider Register(string tag, Type type)
        {
            if (types.ContainsKey(tag))
                types[tag] = type;
            else
                types.TryAdd(tag, type);

            return this;
        }
    }
}
