using System;
using System.Collections.Generic;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для отправки сообщения в указанную очередь. В теле сообщения можно передавать только XML, JSON и неформатированный текст
    /// </summary>
    public class SendMessageRequest : BaseRequest
    {
        /// <summary>
        /// Время в секундах, на которое сообщение будет скрыто после отправки. 
        /// Возможные значения: от 0 до 900. 
        /// Если параметр не указан, используется значение параметра из очереди. 
        /// Параметр не работает для сообщений, отправляемых в очереди FIFO — в этом случае используется параметр из очереди.
        /// </summary>
        public int? DelaySeconds { get; set; }
        /// <summary>
        /// Массив имен и соответствующих им значений пользовательских атрибутов сообщения. См. тип данных Message.
        /// </summary>
        public Dictionary<string, MessageAttributeValue> MessageAttribute { get; set; } = new Dictionary<string, MessageAttributeValue>();
        /// <summary>
        /// URL очереди, в которой находится сообщение
        /// </summary>
        public string QueueUrl { get; set; }
        /// <summary>
        /// Тело отправляемого сообщения. Максимальный размер — 256 КБ. 
        /// Может содержать структуры XML, JSON и неформатированный текст.
        /// </summary>
        public string MessageBody { get; set; }
        /// <summary>
        /// Идентификатор токена для дедупликации сообщений, используется в очередях FIFO.
        /// Каждое сообщение должно иметь уникальный MessageDeduplicationId. 
        /// Если MessageDeduplicationId не указан, отправка сообщения в очередь не будет выполнена. 
        /// Максимальная длина — 128 символов. Разрешено использование цифр, больших и маленьких латинских букв и знаков пунктуации. 
        /// Подробнее см. Дедупликация.
        /// </summary>
        public string MessageDeduplicationId { get; set; }
        /// <summary>
        /// Идентификатор группы сообщений, используется в очередях FIFO. 
        /// Максимальная длина — 128 символов. 
        /// Разрешено использование цифр, больших и маленьких латинских букв и знаков пунктуации. 
        /// Подробнее см. Дедупликация.
        /// </summary>
        public string MessageGroupId { get; set; }

        public SendMessageRequest()
            : base("SendMessage") { }

        /// <summary>
        /// url очереди в которую будет отправлено сообщение
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SendMessageRequest SetQueueUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Queue url cannot was null or empty");
            QueueUrl = value;
            return this;
        }

        /// <summary>
        /// Текст сообещния
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SendMessageRequest SetMessageBody(string value)
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
        public SendMessageRequest SetMessageAttribute(string attributeName, string value)
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
        public SendMessageRequest SetMessageAttribute(string attributeName, int value)
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
        public SendMessageRequest SetMessageAttribute(string attributeName, byte[] value)
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
