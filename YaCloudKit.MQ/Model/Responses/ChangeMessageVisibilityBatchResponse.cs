using System.Collections.Generic;

namespace YaCloudKit.MQ.Model.Responses
{
    public class ChangeMessageVisibilityBatchResponse : YandexMessageQueueResponse
    {
        /// <summary>
        /// Массив BatchResultErrorEntry, содержащий ошибки выполнения запроса.
        /// </summary>
        public List<BatchResultErrorEntry> BatchResultErrorEntry { get; set; } = new List<BatchResultErrorEntry>();
        /// <summary>
        /// Массив ChangeMessageVisibilityBatchResultEntry содержащий идентификаторы сообщений с успешно измененным таймаутом.
        /// </summary>
        public List<ChangeMessageVisibilityBatchResultEntry> ChangeMessageVisibilityBatchResultEntry { get; set; } = new List<ChangeMessageVisibilityBatchResultEntry>();
    }
}
