using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model
{
    public class DeleteMessageBatchRequestEntry
    {
        /// <summary>
        /// Идентификатор для ReceiptHandle. Параметр должен быть уникален в пределах одного запроса.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Идентификатор получения сообщения.
        /// </summary>
        public string ReceiptHandle { get; set; }
    }
}
