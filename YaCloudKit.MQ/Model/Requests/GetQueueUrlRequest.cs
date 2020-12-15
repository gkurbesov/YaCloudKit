namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для получения URL указанной очереди. 
    /// Укажите имя очереди, чтобы получить ее URL.
    /// </summary>
    public class GetQueueUrlRequest : BaseRequest
    {
        /// <summary>
        /// Имя очереди. Максимальная длина — 80 символов. 
        /// В имени можно использовать цифры, буквы, нижние подчеркивания и дефисы. 
        /// Чувствительно к регистру.
        /// </summary>
        public string QueueName { get; set; }

        public GetQueueUrlRequest()
            : base("GetQueueUrl") { }
    }
}
