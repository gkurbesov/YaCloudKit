using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для установки таймаута видимости группе сообщений в указанной очереди. 
    /// Можно отправить до 10 вызовов <code>ChangeMessageVisibility</code> в одном вызове <code>ChangeMessageVisibilityBatch</code>.
    /// </summary>
    public class ChangeMessageVisibilityBatchRequest
    {
        /// <summary>
        /// URL очереди, в которой находится сообщение. 
        /// Чувствителен к регистру
        /// </summary>
        public string QueueUrl { get; set; }
        /// <summary>
        /// Массив <code>ChangeMessageVisibilityBatchRequestEntry</code>, содержащих параметры <code>ReceiptHandle</code> сообщений, которым требуется изменить таймауты видимости.
        /// </summary>
        public List<ChangeMessageVisibilityBatchRequestEntry> ChangeMessageVisibilityBatchRequestEntry { get; set; } = new List<ChangeMessageVisibilityBatchRequestEntry>();
    }
}
