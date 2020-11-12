using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для удаления очереди. 
    /// Если указанная очередь не существует, Message Queue сообщит об успешном выполнении запроса.
    /// 
    /// Процесс удаления очереди занимает до 60 секунд.
    /// В течение этого времени некоторые запросы, например, отправка сообщений в очередь, могут выполняться, но очередь все равно будет удалена вместе со всеми сообщениями.
    /// </summary>
    public class DeleteQueueRequest
    {
        /// <summary>
        /// URL очереди. Чувствителен к регистру
        /// </summary>
        public string QueueUrl { get; set; }
    }
}
