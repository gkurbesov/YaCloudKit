namespace YaCloudKit.MQ.Model
{
    /// <summary>
    /// Результат изменения видимости сообщения из группы
    /// </summary>
    public class ChangeMessageVisibilityBatchResultEntry
    {
        /// <summary>
        /// Идентификатор сообщения, у которого изменен таймаут видимости.
        /// </summary>
        public string Id { get; set; }
    }
}
