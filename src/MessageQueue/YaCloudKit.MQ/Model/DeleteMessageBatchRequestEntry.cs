namespace YaCloudKit.MQ.Model
{
    /// <summary>
    /// Запрос удаления сообщения в группе
    /// </summary>
    public class DeleteMessageBatchRequestEntry
    {
        /// <summary>
        /// Идентификатор для ReceiptHandle. Параметр должен быть уникален в пределах одного запроса.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Идентификатор получения сообщения.
        /// </summary>
        public string ReceiptHandle { get; set; }
    }
}
