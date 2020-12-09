using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для установки таймаута видимости сообщению, находящемуся в обработке. 
    /// Суммарная длительность таймаута не может быть более 12 часов.
    /// </summary>
    public class ChangeMessageVisibilityRequest : BaseRequest
    {
        /// <summary>
        /// URL очереди, в которой находится сообщение
        /// </summary>
        public string QueueUrl { get; set; }
        /// <summary>
        /// Параметр <code>ReceiptHandle</code> из запроса <code>ReceiveMessage</code>
        /// </summary>
        public string ReceiptHandle { get; set; }
        /// <summary>
        /// Новое значение таймаута видимости сообщений в очереди в секундах. 
        /// Возможные значения: от 0 до 43200 секунд. 
        /// Значение по умолчанию: 30
        /// </summary>
        public int VisibilityTimeout { get; set; } = 30;

        public ChangeMessageVisibilityRequest()
            : base("ChangeMessageVisibility") { }
    }
}
