namespace YaCloudKit.MQ.Model.Responses
{
    public class CreateQueueResponse : YandexMessageQueueResponse
    {
        /// <summary>
        /// URL созданной очереди.
        /// </summary>
        public string QueueUrl { get; set; }
    }
}
