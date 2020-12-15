using System.Collections.Generic;

namespace YaCloudKit.MQ.Model.Responses
{
    public class ReceiveMessageResponse : YandexMessageQueueResponse
    {
        /// <summary>
        /// Список полученных сообщений
        /// </summary>
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
