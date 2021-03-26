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

        public static bool TryGetMessage(this Message responseMessage, out object message)
        {
            YandexMqTrasport.ThrowIfNotInitialized();
            message = null;

            if (string.IsNullOrWhiteSpace(responseMessage.Body))
                return false;

            if (responseMessage.MessageAttribute.TryGetValue(YandexMqTrasport.ATTR_MESSAGE, out var attr) && !string.IsNullOrWhiteSpace(attr.StringValue))
            {
                var messageType = YandexMqTrasport.TypeProvider.GetMessageType(attr.StringValue);
                if (messageType == null)
                    throw new YandexMqTrasportException($"Message type ({attr.StringValue}) not registered ");

                var convertAttribute = responseMessage.MessageAttribute.TryGetValue(YandexMqTrasport.ATTR_CONVERTER, out var attr2) ?
                    attr2.StringValue : null;

                var converter = !string.IsNullOrWhiteSpace(convertAttribute) ?
                    YandexMqTrasport.ConverterProvider.GetConverter(convertAttribute) : YandexMqTrasport.ConverterProvider.FirstOrDefault();

                if (converter == null)
                    throw new YandexMqTrasportException("The required converter was not found" + (!string.IsNullOrWhiteSpace(convertAttribute) ? $": {convertAttribute}" : string.Empty));

                message = converter.Deserialize(responseMessage.Body, messageType);
                return true;
            }
            return false;
        }

        public static SendMessageRequest AddMessage<T>(this SendMessageRequest request, T message)
        {
            YandexMqTrasport.ThrowIfNotInitialized();

            var messageTypeName = AttributeHelper.GetPropertyName<MessageQueueAttribute>(message);
            var converterTypeName = AttributeHelper.GetPropertyName<MessageConverterAttribute>(message);

            var converter = !string.IsNullOrWhiteSpace(converterTypeName) ?
                YandexMqTrasport.ConverterProvider.GetConverter(converterTypeName) : YandexMqTrasport.ConverterProvider.FirstOrDefault();

            if (converter == null)
                throw new YandexMqTrasportException($"The required converter ({converterTypeName}) was not found");

            request.MessageBody = converter.Serialize(message);
            request.MessageAttribute.Add(YandexMqTrasport.ATTR_MESSAGE,
                new MessageAttributeValue()
                {
                    DataType = AttributeValueType.String,
                    StringValue = messageTypeName
                });
            if (!string.IsNullOrWhiteSpace(converterTypeName))
                request.MessageAttribute.Add(YandexMqTrasport.ATTR_CONVERTER,
                new MessageAttributeValue()
                {
                    DataType = AttributeValueType.String,
                    StringValue = converterTypeName
                });

            return request;
        }
    }
}
