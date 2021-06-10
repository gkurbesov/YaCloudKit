using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Model.Responses;
using YaCloudKit.MQ.Transport.Attributes;

namespace YaCloudKit.MQ.Transport
{
    public static class YandexMqExtension
    {
        /// <summary>
        /// Пытается получить объект из сообщения очереди
        /// </summary>
        /// <param name="responseMessage">Сообщение полученное из очереди</param>
        /// <param name="value">десериализованный объект сообщения</param>
        /// <returns>true - если удалось десериализовать данные в объект, иначе false</returns>
        public static bool TryGetMessage(this Message responseMessage, out object value)
        {
            YandexMqTrasport.ThrowIfNotInitialized();

            value = null;
            if (string.IsNullOrWhiteSpace(responseMessage.Body))
                return false;
            if (!responseMessage.MessageAttribute.TryGetValue(YandexMqTrasport.ATTR_MESSAGE, out var attr) || string.IsNullOrWhiteSpace(attr.StringValue))
                return false;
            if (!responseMessage.MessageAttribute.TryGetValue(YandexMqTrasport.ATTR_CONVERTER, out var attr2) || string.IsNullOrWhiteSpace(attr2.StringValue))
                return false;

            var messageType = YandexMqTrasport.TypeProvider.GetMessageType(attr.StringValue);
            if (messageType == null)
                return false; 
            
            var converter = YandexMqTrasport.ConverterProvider.GetConverter(attr2.StringValue);

            if (converter == null)
                return false;

            value = converter.Deserialize(responseMessage.Body, messageType);
            return true;
        }
        /// <summary>
        /// Получает объект из сообщения
        /// </summary>
        /// <param name="responseMessage">Сообщение полученное из очереди</param>
        /// <returns></returns>
        public static object GetMessage(this Message responseMessage)
        {
            YandexMqTrasport.ThrowIfNotInitialized();

            if (string.IsNullOrWhiteSpace(responseMessage.Body))
                throw new YandexMqTrasportException($"The message body does not by null or empty");

            if (!responseMessage.MessageAttribute.TryGetValue(YandexMqTrasport.ATTR_MESSAGE, out var attr) || string.IsNullOrWhiteSpace(attr.StringValue))
                throw new YandexMqTrasportException($"The message does not contain an attribute with an object type");
            if (!responseMessage.MessageAttribute.TryGetValue(YandexMqTrasport.ATTR_CONVERTER, out var attr2) || string.IsNullOrWhiteSpace(attr2.StringValue))
                throw new YandexMqTrasportException($"The message does not contain an attribute with an converter type");

            var messageType = YandexMqTrasport.TypeProvider.GetMessageType(attr.StringValue);
            if (messageType == null)
                throw new YandexMqTrasportException($"Message type ({attr.StringValue}) not registered ");

            var converter = YandexMqTrasport.ConverterProvider.GetConverter(attr2.StringValue);

            if (converter == null)
                throw new YandexMqTrasportException($"The required converter was not found {attr2.StringValue}");

            return converter.Deserialize(responseMessage.Body, messageType);
        }
        /// <summary>
        /// Сериализует объект и помещает данные в сообщение, а так же дополнительные атрибуты для распознания
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="request"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static SendMessageRequest AddMessage<T>(this SendMessageRequest request, T message)
        {
            YandexMqTrasport.ThrowIfNotInitialized();

            var messageTypeName = AttributeHelper.GetPropertyName<MessageQueueNameAttribute>(message) ?? YandexMqTrasport.TypeProvider.GetMessageTag<T>();
            var converterTypeName = AttributeHelper.GetPropertyName<MessageConverterAttribute>(message);

            var converter = !string.IsNullOrWhiteSpace(converterTypeName) ?
                YandexMqTrasport.ConverterProvider.GetConverter(converterTypeName) :
                YandexMqTrasport.ConverterProvider.GetDefault();

            if (converter == null)
                throw new YandexMqTrasportException($"The required converter ({converterTypeName}) was not found");

            request.MessageBody = converter.Serialize(message);
            request.MessageAttribute.Add(YandexMqTrasport.ATTR_MESSAGE,
                new MessageAttributeValue()
                {
                    DataType = AttributeValueType.String,
                    StringValue = messageTypeName
                });
            request.MessageAttribute.Add(YandexMqTrasport.ATTR_CONVERTER,
                new MessageAttributeValue()
                {
                    DataType = AttributeValueType.String,
                    StringValue = !string.IsNullOrWhiteSpace(converterTypeName) ?
                        converterTypeName : YandexMqTrasport.ConverterProvider.GetTag(converter)
                });

            return request;
        }
        /// <summary>
        /// Отправляет сообщение в очередь с сериализованным объектом
        /// </summary>
        /// <typeparam name="T">Тип объекта сообщения</typeparam>
        /// <param name="mq">клиент очереди</param>
        /// <param name="queueUrl">URL очереди</param>
        /// <param name="message">Экземпляр объекта для сообщения</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<SendMessageResponse> SendMessageAsync<T>(this IYandexMq mq, string queueUrl, T message, CancellationToken cancellationToken = default)
        {
            var request = new SendMessageRequest()
                .SetQueueUrl(queueUrl)
                .AddMessage(message);

            return mq.SendMessageAsync(request, cancellationToken);
        }
        /// <summary>
        /// Отправляет сообщение в очередь с сериализованным объектом
        /// </summary>
        /// <typeparam name="T">Тип объекта сообщения</typeparam>
        /// <param name="mq">клиент очереди</param>
        /// <param name="queueUrl">URL очереди</param>
        /// <param name="message">Экземпляр объекта для сообщения</param>
        /// <param name="attributes">Дополнительные атрибуты, которые необходимо поместить в сообщение</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<SendMessageResponse> SendMessageAsync<T>(this IYandexMq mq, string queueUrl, T message, Dictionary<string, MessageAttributeValue> attributes, CancellationToken cancellationToken = default)
        {
            var request = new SendMessageRequest()
                .SetQueueUrl(queueUrl)                
                .AddMessage(message);
            request.MessageAttribute = attributes;
            return mq.SendMessageAsync(request, cancellationToken);
        }
    }
}
