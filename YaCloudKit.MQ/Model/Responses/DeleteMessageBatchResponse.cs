using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Responses
{
    public class DeleteMessageBatchResponse : YandexMessageQueueResponse
    {
        /// <summary>
        /// Массив BatchResultErrorEntry, содержащий ошибки выполнения запроса.
        /// </summary>
        public List<BatchResultErrorEntry> BatchResultErrorEntry { get; set; } = new List<BatchResultErrorEntry>();
        /// <summary>
        /// Массив DeleteMessageBatchResultEntry содержащий идентификаторы успешно удаленных сообщений.
        /// </summary>
        public List<DeleteMessageBatchResultEntry> DeleteMessageBatchResultEntry { get; set; } = new List<DeleteMessageBatchResultEntry>();
    }
}
