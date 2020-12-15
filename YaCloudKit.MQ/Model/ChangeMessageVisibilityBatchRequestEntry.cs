namespace YaCloudKit.MQ.Model
{
    /// <summary>
    /// Параметры запроса изменения видимости для сообщения в группе
    /// </summary>
    public class ChangeMessageVisibilityBatchRequestEntry
    {
        /// <summary>
        /// Идентификатор для ReceiptHandle. Параметр должен быть уникален в пределах одного запроса.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Идентификатор получения сообщения.
        /// </summary>
        public string ReceiptHandle { get; set; }
        /// <summary>
        /// Новое значение таймаута сообщения в секундах.
        /// </summary>
        public int? VisibilityTimeout { get; set; }
    }
}
