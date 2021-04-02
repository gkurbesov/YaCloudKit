using System;
using System.Collections.Generic;
using YaCloudKit.MQ.Model.Constants;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для приема от одного до десяти сообщений из указанной очереди. 
    /// С помощью параметра WaitTimeSeconds выполняются long-polling запросы.
    /// </summary>
    public class ReceiveMessageRequest : BaseRequest
    {
        /// <summary>
        /// Максимальное количество сообщений, которое будет принято. 
        /// Возможные значения: от 1 до 10. Значение по умолчанию: 1.
        /// </summary>
        public int? MaxNumberOfMessages { get; set; }
        /// <summary>
        /// Массив имен системныъх атрибутов сообщения, которые требуется вернуть в ответе на запрос. 
        /// Можно получить все атрибуты сразу, указав слово <code>All</code>.
        /// В классе <code>MessageSystemAttributeName</code> определены константы с именами атрибутов
        /// </summary>
        public List<string> AttributeNames { get; set; } = new List<string>();
        /// <summary>
        /// Массив имен атрибутов сообщения, которые требуется вернуть в ответе на запрос. 
        /// Имя может содержать буквы и цифры, а также дефисы, нижние подчеркивания и точки. 
        /// Имена атрибутов чувствительны к регистру и уникальны в пределах одного сообщения. 
        /// Имена атрибутов не могут начинаться или оканчиваться точками. 
        /// Имена атрибутов не должны содержать несколько точек подряд. 
        /// Максимальная длина имени атрибута — 256 символов. 
        /// Можно получить все атрибуты сразу, указав слово <code>All</code>. 
        /// Также можно использовать префиксы для получения нужных атрибутов.
        /// </summary>
        public List<string> MessageAttributeName { get; set; } = new List<string>();
        /// <summary>
        /// URL очереди, в которой находится сообщение
        /// </summary>
        public string QueueUrl { get; set; }
        /// <summary>
        /// Идентификатор для повтора попытки получения сообщений из FIFO-очереди. Подробнее см. Дедупликация.
        /// </summary>
        public string ReceiveRequestAttemptId { get; set; }
        /// <summary>
        /// Таймаут видимости получаемого сообщения.
        /// </summary>
        public int? VisibilityTimeout { get; set; }
        /// <summary>
        /// Время ожидания доставки сообщения в очередь в секундах. 
        /// Если в очереди появятся сообщения, вызов будет сделан раньше, чем указано в <code>WaitTimeSeconds</code>. 
        /// Если сообщения не появились после истечения <code>WaitTimeSeconds</code> будет возвращен пустой список.
        /// </summary>
        public int? WaitTimeSeconds { get; set; }

        public ReceiveMessageRequest()
            : base("ReceiveMessage") { }

        internal bool IsSetAttributeNames() =>
            AttributeNames != null && AttributeNames.Count > 0;

        internal bool IsSetMessageAttributeName() =>
            MessageAttributeName != null && MessageAttributeName.Count > 0;

        /// <summary>
        /// Установить макисимальное число получаемых сообщений
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public ReceiveMessageRequest SetMaxNumberOfMessage(int? count)
        {
            if (count.HasValue && count >= 1 && count <= 10)
                MaxNumberOfMessages = count;
            else
                MaxNumberOfMessages = null;
            return this;
        }

        /// <summary>
        /// запросить все пользовательские атрибуты сообщений
        /// </summary>
        /// <returns></returns>
        public ReceiveMessageRequest SetAllMessageAttribute()
        {
            MessageAttributeName.Clear();
            MessageAttributeName.Add(MessageSystemAttributeName.All);
            return this;
        }

        /// <summary>
        /// Добавить запрашиваемый атрибут
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public ReceiveMessageRequest SetMessageAttribute(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Name of attribute cannot was null or empty");
            MessageAttributeName.Add(value);
            return this;
        }
        /// <summary>
        /// устанавливает url очереди, из которой будут получены сообщения
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ReceiveMessageRequest SetQueueUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Queue url cannot was null or empty");
            QueueUrl = value;
            return this;
        }

        /// <summary>
        /// Время ожидания для получения сообщений из очереди
        /// </summary>
        /// <param name="seconds">количество секунд, но не более 20</param>
        /// <returns></returns>
        public ReceiveMessageRequest SetWaitTimeSeconds(int? seconds)
        {
            if (seconds.HasValue && seconds.Value >= 1 && seconds.Value <= 20)
                WaitTimeSeconds = seconds;
            else
                WaitTimeSeconds = null;
            return this;
        }
    }
}
