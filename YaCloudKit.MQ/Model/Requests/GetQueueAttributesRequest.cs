using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для получения атрибутов указанной очереди.
    /// Если в конце названия очереди стоит суффикс <code>.fifo</code> — запрашиваемая очередь имеет тип FIFO.
    /// </summary>
    public class GetQueueAttributesRequest : BaseRequest
    {
        /// <summary>
        /// URL очереди
        /// </summary>
        public string QueueUrl { get; set; }
        /// <summary>
        /// Список атрибутов очереди
        /// Атрибут <code>All</code> - возвращает все атрибуты очереди.
        /// </summary>
        public List<string> AttributeNames = new List<string>();

        public GetQueueAttributesRequest()
            : base("GetQueueAttributes") { }
    }
}
