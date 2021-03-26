using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YaCloudKit.MQ.Transport.Converters;

namespace YaCloudKit.MQ.Transport
{
    public static class YandexMqTrasport
    {
        internal const string ATTR_MESSAGE = "ya-cloud-kit-mq-message";
        internal const string ATTR_CONVERTER = "ya-cloud-kit-mq-converter";

        public static readonly IMessageConverterProvider ConverterProvider = new MessageConverterProvider();
        public static readonly IMessageTypeProvider TypeProvider = new MessageTypeProvider();

        public static void Initialize(Action<IMessageTypeProvider> configure)
        {
            ConverterProvider.Register(JsonMessageConverter.TAG, new JsonMessageConverter());
            ConverterProvider.Register(XmlMessageConverter.TAG, new XmlMessageConverter());

            configure(TypeProvider);
        }

        public static void Initialize(Action<IMessageConverterProvider, IMessageTypeProvider> configure)
        {
            configure(ConverterProvider, TypeProvider);
        }

        internal static void ThrowIfNotInitialized()
        {
            if (ConverterProvider.Values.Count() == 0)
                throw new OperationCanceledException("Yandex Message Queue Trasport not initialized");
        }
    }
}
