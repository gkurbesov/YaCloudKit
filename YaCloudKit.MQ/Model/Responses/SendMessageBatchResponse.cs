using System.Collections.Generic;

namespace YaCloudKit.MQ.Model.Responses
{
    public class SendMessageBatchResponse : YandexMessageQueueResponse
    {
        /// <summary>
        /// Массив BatchResultErrorEntry, содержащих сведения о сообщениях, которые не удалось добавить в очередь.
        /// </summary>
        public List<BatchResultErrorEntry> BatchResultErrorEntry { get; set; } = new List<BatchResultErrorEntry>();
        /// <summary>
        /// Массив SendMessageBatchResultEntry, содержащий сведения об успешно отправленных в очередь сообщениях.
        /// </summary>
        public List<SendMessageBatchResultEntry> SendMessageBatchResultEntry { get; set; } = new List<SendMessageBatchResultEntry>();
    }
}
