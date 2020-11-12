using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model
{
    public class SendMessageBatchResultEntry
    {
        /// <summary>
        /// Идентификатор сообщения в группе.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// MD5-хэш атрибутов сообщения.
        /// </summary>
        public string MD5OfMessageAttributes { get; set; }
        /// <summary>
        /// MD5-хэш тела сообщения.
        /// </summary>
        public string MD5OfMessageBody { get; set; }
        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// Номер сообщения, используется в очередях FIFO в рамках группы сообщений с одинаковым MessageGroupId. 
        /// Длина параметра SequenceNumber — 128 бит.
        /// </summary>
        public string SequenceNumber { get; set; }
    }
}
