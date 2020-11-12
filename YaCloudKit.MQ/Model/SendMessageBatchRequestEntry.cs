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
        public string DelaySeconds { get; set; }
        /// <summary>
        /// Идентификатор сообщения в списке.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Атрибуты сообщения: имя, тип и значение.
        /// </summary>
        public string MessageAttribute { get; set; }
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
    }
}
