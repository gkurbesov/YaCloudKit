using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для отправки сообщения в указанную очередь. В теле сообщения можно передавать только XML, JSON и неформатированный текст
    /// </summary>
    public class SendMessageRequest
    {
        /// <summary>
        /// Время в секундах, на которое сообщение будет скрыто после отправки. 
        /// Возможные значения: от 0 до 900. 
        /// Если параметр не указан, используется значение параметра из очереди. 
        /// Параметр не работает для сообщений, отправляемых в очереди FIFO — в этом случае используется параметр из очереди.
        /// </summary>
        public int? DelaySeconds { get; set; }
        /// <summary>
        /// Массив имен и соответствующих им значений пользовательских атрибутов сообщения. См. тип данных Message.
        /// </summary>
        public List<MessageAttributeValue> MessageAttribute { get; set; } = new List<MessageAttributeValue>();
        /// <summary>
        /// URL очереди, в которой находится сообщение
        /// </summary>
        public string QueueUrl { get; set; }
        /// <summary>
        /// Тело отправляемого сообщения. Максимальный размер — 256 КБ. 
        /// Может содержать структуры XML, JSON и неформатированный текст.
        /// </summary>
        public string MessageBody { get; set; }
        /// <summary>
        /// Идентификатор токена для дедупликации сообщений, используется в очередях FIFO.
        /// Каждое сообщение должно иметь уникальный MessageDeduplicationId. 
        /// Если MessageDeduplicationId не указан, отправка сообщения в очередь не будет выполнена. 
        /// Максимальная длина — 128 символов. Разрешено использование цифр, больших и маленьких латинских букв и знаков пунктуации. 
        /// Подробнее см. Дедупликация.
        /// </summary>
        public string MessageDeduplicationId { get; set; }
        /// <summary>
        /// Идентификатор группы сообщений, используется в очередях FIFO. 
        /// Максимальная длина — 128 символов. 
        /// Разрешено использование цифр, больших и маленьких латинских букв и знаков пунктуации. 
        /// Подробнее см. Дедупликация.
        /// </summary>
        public string MessageGroupId { get; set; }
    }
}
