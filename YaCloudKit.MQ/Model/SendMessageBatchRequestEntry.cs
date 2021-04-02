using System;
using System.Collections.Generic;

namespace YaCloudKit.MQ.Model
{
    /// <summary>
    /// Сообщений для отправки группой
    /// </summary>
    public class SendMessageBatchRequestEntry
    {
        /// <summary>
        /// Время в секундах, на которое будет отложена отправка сообщения.
        /// </summary>
        public int? DelaySeconds { get; set; }
        /// <summary>
        /// Идентификатор сообщения в списке.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Массив имен и соответствующих им значений пользовательских атрибутов сообщения. См. тип данных Message.
        /// </summary>
        public Dictionary<string, MessageAttributeValue> MessageAttribute { get; set; } = new Dictionary<string, MessageAttributeValue>();
        /// <summary>
        /// Тело сообщения.
        /// </summary>
        public string MessageBody { get; set; }
        /// <summary>
        /// Идентификатор для дедупликации сообщений. Подробнее см. Дедупликация.
        /// </summary>
        public string MessageDeduplicationId { get; set; }
        /// <summary>
        /// Идентификатор группы сообщений, используется только в очередях FIFO.
        /// </summary>
        public string MessageGroupId { get; set; }


        internal bool IsSetMessageAttribute() =>
            MessageAttribute != null && MessageAttribute.Count > 0;

        /// <summary>
        /// Текст сообещния
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SendMessageBatchRequestEntry SetMessageBody(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Message cannot was null or empty");
            MessageBody = value;
            return this;
        }

        /// <summary>
        /// добавляет пользовательский атрибут с ссответствующим типом
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SendMessageBatchRequestEntry SetMessageAttribute(string attributeName, string value)
        {
            var attr = new MessageAttributeValue() { DataType = AttributeValueType.String, StringValue = value };
            if (MessageAttribute.ContainsKey(attributeName))
                MessageAttribute[attributeName] = attr;
            else
                MessageAttribute.Add(attributeName, attr);
            return this;
        }

        /// <summary>
        /// добавляет пользовательский атрибут с ссответствующим типом
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SendMessageBatchRequestEntry SetMessageAttribute(string attributeName, int value)
        {
            var attr = new MessageAttributeValue() { DataType = AttributeValueType.Number, StringValue = value.ToString() };
            if (MessageAttribute.ContainsKey(attributeName))
                MessageAttribute[attributeName] = attr;
            else
                MessageAttribute.Add(attributeName, attr);
            return this;
        }

        /// <summary>
        /// добавляет пользовательский атрибут с ссответствующим типом
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SendMessageBatchRequestEntry SetMessageAttribute(string attributeName, byte[] value)
        {
            var attr = new MessageAttributeValue() { DataType = AttributeValueType.Binary, BinaryValue = value };
            if (MessageAttribute.ContainsKey(attributeName))
                MessageAttribute[attributeName] = attr;
            else
                MessageAttribute.Add(attributeName, attr);
            return this;
        }
    }
}
