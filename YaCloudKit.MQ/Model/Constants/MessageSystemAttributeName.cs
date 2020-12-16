using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Constants
{
    /// <summary>
    /// Класс содержащий константы имен атрибутов для сообщений
    /// </summary>
    public class MessageSystemAttributeName
    {
        /// <summary>
        /// Используется для запроса всех атрибутов сообщения.
        /// </summary>
        public const string All = "All";
        /// <summary>
        /// Время получения сообщения из очереди.
        /// </summary>
        public const string ApproximateFirstReceiveTimestamp = "ApproximateFirstReceiveTimestamp";
        /// <summary>
        /// Число получений сообщения из очереди без его удаления.
        /// </summary>
        public const string ApproximateReceiveCount = "ApproximateReceiveCount";
        /// <summary>
        /// Идентификатор отправителя — субъекта IAM.
        /// </summary>
        public const string SenderId = "SenderId";
        /// <summary>
        /// Время отправки сообщения в очередь.
        /// </summary>
        public const string SentTimestamp = "SentTimestamp";
        /// <summary>
        /// Идентификатор токена для дедупликации сообщений, используется в очередях FIFO.
        /// Каждое сообщение должно иметь уникальный MessageDeduplicationId. 
        /// Если MessageDeduplicationId не указан, отправка сообщения в очередь не будет выполнена. 
        /// Максимальная длина — 128 символов. Разрешено использование цифр, больших и маленьких латинских букв и знаков пунктуации. 
        /// Подробнее см. Дедупликация.
        /// </summary>
        public const string MessageDeduplicationId = "MessageDeduplicationId";
        /// <summary>
        /// Идентификатор группы сообщений, используется в очередях FIFO. Подробнее см. Дедупликация.
        /// </summary>
        public const string MessageGroupId = "MessageGroupId";
        /// <summary>
        /// Номер сообщения, используется в очередях FIFO в рамках группы сообщений с одинаковым MessageGroupId.
        /// </summary>
        public const string SequenceNumber = "SequenceNumber";
    }
}
