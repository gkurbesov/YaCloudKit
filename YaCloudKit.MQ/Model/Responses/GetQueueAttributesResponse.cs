using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Responses
{
    public class GetQueueAttributesResponse : YandexMessageQueueResponse
    {
        /// <summary>
        /// Словарь перечисления атрибутов очереди.
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
    }
}
