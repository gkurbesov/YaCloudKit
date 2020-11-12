using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для задания атрибутов указанной очереди. 
    /// Изменение атрибутов может занять до 60 секунд. 
    /// Изменение атрибута <code>MessageRetentionPeriod</code> может занять до 15 минут.
    /// </summary>
    public class SetQueueAttributesRequest
    {
        /// <summary>
        /// URL очереди
        /// </summary>
        public string QueueUrl { get; set; }
        /// <summary>
        /// Список атрибутов очереди
        /// </summary>
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();
    }
}
