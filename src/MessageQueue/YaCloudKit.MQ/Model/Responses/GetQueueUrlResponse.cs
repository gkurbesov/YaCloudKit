namespace YaCloudKit.MQ.Model.Responses
{
    public class GetQueueUrlResponse : YandexMessageQueueResponse
    {
        /// <summary>
        /// URL очереди
        /// </summary>
        public string QueueUrl { get; set; }
    }
}
