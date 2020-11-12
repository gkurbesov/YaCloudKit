using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model
{
    public class DeleteMessageBatchResultEntry
    {
        /// <summary>
        /// Идентификатор удаленного сообщения.
        /// </summary>
        public string Id { get; set; }
    }
}
