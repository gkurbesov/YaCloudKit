using System;
using System.Collections.Concurrent;

namespace YaCloudKit.MQ.Transport
{
    /// <summary>
    /// Стандартный провайдер типов сообщений
    /// </summary>
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
        public string GetMessageTag<T>() =>
            this.GetMessageTag(typeof(T));

        public string GetMessageTag(Type type)
        {
            foreach (var item in types.ToArray())
            {
                if (item.Value.Equals(type))
                    return item.Key;
            }
            throw new YandexMqTrasportException($"Message type ({type.Name}) not registered");
        }
    }
}
