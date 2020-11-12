using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model
{
    public class BatchResultErrorEntry
    {
        /// <summary>
        /// Код ошибки, произошедшей при выполнении запроса.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Идентификатор сообщения в группе.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Сообщение с описанием возникшей ошибки
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Флаг, указывающий, что ошибка возникла на стороне отправителя
        /// </summary>
        public bool SenderFault { get; set; }
    }
}
