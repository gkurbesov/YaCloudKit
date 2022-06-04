using System;
using YaCloudKit.MQ.Transport.Attributes;

namespace YaCloudKit.MQ.Transport
{
    public static class MessageTypeProviderExtension
    {
        /// <summary>
        /// Регистрируем тип сообщения
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IMessageTypeProvider Register(this IMessageTypeProvider provider, Type type)
        {
            var tag = AttributeHelper.GetPropertyName<MessageQueueNameAttribute>(type, true);
            return provider.Register(tag, type);
        }
        /// <summary>
        /// Регистрирует тип сообщения
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IMessageTypeProvider Register<T>(this IMessageTypeProvider provider)
        {
            var type = typeof(T);
            var tag = AttributeHelper.GetPropertyName<MessageQueueNameAttribute>(type, true);
            return provider.Register(tag, type);
        }

    }
}
