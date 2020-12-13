using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model
{
    public class Message
    {
        /// <summary>
        /// Набор атрибутов, указанных в запросе ReceiveMessage. Поддерживаемые атрибуты: ApproximateReceiveCount, ApproximateFirstReceiveTimestamp, MessageDeduplicationId, MessageGroupId, SenderId, SentTimestamp, SequenceNumber.
        /// </summary>
        public Dictionary<string, string> Attribute { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// Тело сообщения.
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// MD5-хэш тела сообщения.
        /// </summary>
        public string MD5OfBody { get; set; }
        /// <summary>
        /// MD5-хэш атрибутов сообщения.
        /// </summary>
        public string MD5OfMessageAttributes { get; set; }
        /// <summary>
        /// Массив MessageAttributeValue, содержащий пользовательские атрибуты сообщения: имя, тип и значение.
        /// </summary>
        public Dictionary<string, MessageAttributeValue> MessageAttribute { get; set; } = new Dictionary<string, MessageAttributeValue>();
        /// <summary>
        /// Уникальный идентификатор сообщения.
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// Идентификатор получения сообщения. При каждом получении сообщения ему назначается новый идентификатор получения. При удалении сообщения используйте последний идентификатор получения.
        /// </summary>
        public string ReceiptHandle { get; set; }
    }
}
