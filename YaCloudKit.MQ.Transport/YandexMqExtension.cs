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

        public static object GetMessage(this Message responseMessage)
        {
            YandexMqTrasport.ThrowIfNotInitialized();

            if (string.IsNullOrWhiteSpace(responseMessage.Body))
                return false;

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

        public static SendMessageRequest AddMessage<T>(this SendMessageRequest request, T message)
        {
            YandexMqTrasport.ThrowIfNotInitialized();

            var messageTypeName = AttributeHelper.GetPropertyName<MessageQueueAttribute>(message);
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
    }
}
