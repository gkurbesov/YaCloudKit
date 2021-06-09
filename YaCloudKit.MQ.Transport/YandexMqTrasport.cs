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

        internal static volatile bool isInitialized = false;

        /// <summary>
        /// Инициализация расширения функционала клиента очереди сообщений
        /// </summary>
        /// <param name="configure">Действие для регистрации используемых типов сообщений</param>
        public static void Initialize(Action<IMessageTypeProvider> configure)
        {
            if (isInitialized)
                throw new OperationCanceledException("Yandex Message Queue Trasport is initialized");

            ConverterProvider.Register(JsonMessageConverter.TAG, new JsonMessageConverter());
            ConverterProvider.Register(XmlMessageConverter.TAG, new XmlMessageConverter());

            configure(TypeProvider);

            isInitialized = true;
        }
        /// <summary>
        /// Инициализация расширения функционала клиента очереди сообщений
        /// </summary>
        /// <param name="configure">Действие для регистрации используемых конвертеров и типов сообщений</param>
        public static void Initialize(Action<IMessageConverterProvider, IMessageTypeProvider> configure)
        {
            if (isInitialized)
                throw new OperationCanceledException("Yandex Message Queue Trasport is initialized");

            configure(ConverterProvider, TypeProvider);

            isInitialized = true;
        }

        internal static void ThrowIfNotInitialized()
        {
            if (!isInitialized)
                throw new OperationCanceledException("Yandex Message Queue Trasport not initialized");
        }
    }
}
