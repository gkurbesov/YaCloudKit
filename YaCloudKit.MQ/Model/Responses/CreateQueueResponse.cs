using System;
using System.Collections.Generic;
using System.Text;

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
