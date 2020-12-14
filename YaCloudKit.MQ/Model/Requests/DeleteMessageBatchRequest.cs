using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для удаления нескольких сообщений из очереди.
    /// Удалять можно не более 10 сообщений одновременно.
    /// </summary>
    public class DeleteMessageBatchRequest : BaseRequest
    {
        /// <summary>
        /// URL очереди, в которой находится сообщение
        /// </summary>
        public string QueueUrl { get; set; }
        /// <summary>
        /// Массив DeleteMessageBatchRequestEntry, содержащий параметры ReceiptHandle сообщений, которые требуется удалить.
        /// </summary>
        public List<DeleteMessageBatchRequestEntry> DeleteMessageBatchRequestEntry { get; set; } = new List<DeleteMessageBatchRequestEntry>();

        public DeleteMessageBatchRequest()
            : base("DeleteMessageBatch") { }

        internal bool IsSetBatchEntry() =>
            DeleteMessageBatchRequestEntry != null && DeleteMessageBatchRequestEntry.Count > 0;
    }
}
