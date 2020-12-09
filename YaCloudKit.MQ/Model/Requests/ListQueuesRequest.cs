using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для получения списка очередей в каталоге. 
    /// Максимальное количество очередей в ответе — 1000. 
    /// Очереди можно отфильтровать с помощью параметра <code>QueueNamePrefix</code>
    /// </summary>
    public class ListQueuesRequest : BaseRequest
    {
        /// <summary>
        /// Префикс для фильтрации имен очередей. Чувствителен к регистру
        /// </summary>
        public string QueueNamePrefix { get; set; }

        public ListQueuesRequest()
            : base("ListQueues") { }
    }
}
