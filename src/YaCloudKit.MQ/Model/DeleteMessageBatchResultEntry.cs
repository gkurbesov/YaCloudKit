namespace YaCloudKit.MQ.Model
{
    /// <summary>
    /// Результат удаления сообщения в группе
    /// </summary>
    public class DeleteMessageBatchResultEntry
    {
        /// <summary>
        /// Идентификатор удаленного сообщения.
        /// </summary>
        public string Id { get; set; }
    }
}
