namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для очистки очереди сообщений. Удаление сообщений занимает некоторое время. 
    /// Чтобы убедиться, что в очереди не осталось сообщений, приостановите отправку сообщений в очередь и вызовите метод GetQueueAttributes.
    /// Сообщения, отправленные в очередь до вызова <code>PurgeQueue</code> могут быть приняты получателями, но будут удалены из очереди в течение следующей минуты.
    /// Сообщения, отправленные в очередь после вызова <code>PurgeQueue</code> не будут удалены.
    /// </summary>
    public class PurgeQueueRequest : BaseRequest
    {
        /// <summary>
        /// URL очереди. Чувствителен к регистру
        /// </summary>
        public string QueueUrl { get; set; }

        public PurgeQueueRequest()
            : base("PurgeQueue") { }
    }
}
