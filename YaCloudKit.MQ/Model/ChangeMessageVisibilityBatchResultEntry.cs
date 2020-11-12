using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model
{
    public class ChangeMessageVisibilityBatchResultEntry
    {
        /// <summary>
        /// Идентификатор сообщения, у которого изменен таймаут видимости.
        /// </summary>
        public string Id { get; set; }
    }
}
