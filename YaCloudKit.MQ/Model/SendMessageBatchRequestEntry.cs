using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model
{
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
    }
}
