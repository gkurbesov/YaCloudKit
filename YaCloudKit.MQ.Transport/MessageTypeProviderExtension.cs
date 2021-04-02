using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Transport.Attributes;

namespace YaCloudKit.MQ.Transport
{
    public static class MessageTypeProviderExtension
    {
        public static IMessageTypeProvider Register(this IMessageTypeProvider provider, Type type)
        {
            var tag = AttributeHelper.GetPropertyName<MessageQueueAttribute>(type, true);
            return provider.Register(tag, type);            
        }

        public static IMessageTypeProvider Register<T>(this IMessageTypeProvider provider)
        {
            var type = typeof(T);
            var tag = AttributeHelper.GetPropertyName<MessageQueueAttribute>(type, true);
            return provider.Register(tag, type);
        }

    }
}
