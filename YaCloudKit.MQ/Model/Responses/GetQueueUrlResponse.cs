using System;
using System.Collections.Generic;
using System.Text;

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
