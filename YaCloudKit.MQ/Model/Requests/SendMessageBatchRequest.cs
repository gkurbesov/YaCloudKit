using System;
using System.Collections.Generic;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для одновременной отправки до десяти сообщений в указанную очередь. 
    /// При отправке сообщений в очередь FIFO они будут поступать в порядке отправления.
    /// </summary>
    public class SendMessageBatchRequest : BaseRequest
    {
        /// <summary>
        /// Массив сообщений представленный сущностью <code>SendMessageBatchRequestEntry</code>.
        /// </summary>
        public List<SendMessageBatchRequestEntry> SendMessageBatchRequestEntry { get; set; } = new List<SendMessageBatchRequestEntry>();
        /// <summary>
        /// URL очереди, в которую посылаются сообщения
        /// </summary>
        public string QueueUrl { get; set; }

        public SendMessageBatchRequest()
            : base("SendMessageBatch") { }

        internal bool IsSetBatchEntry() =>
            SendMessageBatchRequestEntry != null && SendMessageBatchRequestEntry.Count > 0;


        /// <summary>
        /// url очереди в которую будет отправлено сообщение
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SendMessageBatchRequest SetQueueUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Queue url cannot was null or empty");
            QueueUrl = value;
            return this;
        }

        /// <summary>
        /// добавить сообщение в запрос на отправку
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public SendMessageBatchRequest SetSendMessageBatchRequestEntry(SendMessageBatchRequestEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry), "Entry cannot was null");
            SendMessageBatchRequestEntry.Add(entry);
            return this;
        }

    }
}
