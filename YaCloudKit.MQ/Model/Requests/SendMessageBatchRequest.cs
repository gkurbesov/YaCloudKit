using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для одновременной отправки до десяти сообщений в указанную очередь. 
    /// При отправке сообщений в очередь FIFO они будут поступать в порядке отправления.
    /// </summary>
    public class SendMessageBatchRequest
    {
        /// <summary>
        /// Массив сообщений представленный сущностью <code>SendMessageBatchRequestEntry</code>.
        /// </summary>
        public List<SendMessageBatchRequestEntry> SendMessageBatchRequestEntry = new List<SendMessageBatchRequestEntry>();
        /// <summary>
        /// URL очереди, в которую посылаются сообщения
        /// </summary>
        public string QueueUrl { get; set; }
    }
}
