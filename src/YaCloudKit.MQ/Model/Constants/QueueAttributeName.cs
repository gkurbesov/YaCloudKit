namespace YaCloudKit.MQ.Model.Constants
{
    /// <summary>
    /// Класс содержащий константы имен атрибутов для очередей
    /// </summary>
    public class QueueAttributeName
    {
        /// <summary>
        /// Используется для запроса всех атрибутов очереди.
        /// </summary>
        public const string All = "All";
        /// <summary>
        /// Возвращает ориентировочное количество сообщений в очереди, которые готовы к получению.
        /// </summary>
        public const string ApproximateNumberOfMessages = "ApproximateNumberOfMessages";
        /// <summary>
        /// Возвращает ориентировочное количество отложенных сообщений в очереди, которые еще не готовы к приему.
        /// </summary>
        public const string ApproximateNumberOfMessagesDelayed = "ApproximateNumberOfMessagesDelayed";
        /// <summary>
        /// Возвращает ориентировочное количество сообщений, находящихся в процессе передачи — они уже отправлены получателю, 
        /// но еще не удалены из очереди или у них не закончился таймаут видимости.
        /// </summary>
        public const string ApproximateNumberOfMessagesNotVisible = "ApproximateNumberOfMessagesNotVisible";
        /// <summary>
        /// Возвращает таймстемп создания очереди в секундах (epoch time).
        /// </summary>
        public const string CreatedTimestamp = "CreatedTimestamp";
        /// <summary>
        /// Возвращает время последнего изменения очереди в секундах (epoch time).
        /// </summary>
        public const string LastModifiedTimestamp = "LastModifiedTimestamp";
        /// <summary>
        /// ARN очереди, используемый в атрибуте RedrivePolicy.
        /// </summary>
        public const string QueueArn = "QueueArn";
        /// <summary>
        /// Время в секундах, на которое сообщения будут скрыты после отправки. 
        /// Возможные значения от 0 до 900 секунд (15 минут). Значение по умолчанию: 0.
        /// </summary>
        public const string DelaySeconds = "DelaySeconds";
        /// <summary>
        /// Максимальный размер сообщения в байтах.
        /// Возможные значения: от 1024 байт (1 KБ) до 262144 байт (256 КБ). Значение по умолчанию: 262144 (256 КБ).
        /// </summary>
        public const string MaximumMessageSize = "MaximumMessageSize";
        /// <summary>
        /// Срок хранения сообщений в секундах. 
        /// Возможные значения: от 60 секунд (1 минуты) до 1209600 секунд (14 дней). Значение по умолчанию: 345600 (4 дня).
        /// </summary>
        public const string MessageRetentionPeriod = "MessageRetentionPeriod";
        /// <summary>
        /// Время ожидания для метода ReceiveMessage в секундах. Возможные значения: от 0 до 20 секунд. Значение по умолчанию: 0.
        /// </summary>
        public const string ReceiveMessageWaitTimeSeconds = "ReceiveMessageWaitTimeSeconds";
        /// <summary>
        /// Политика перенаправления сообщений в Dead Letter Queue. 
        /// Тип исходной очереди и очереди DLQ должны быть одинаковыми: для очередей FIFO очередь DLQ тоже должна быть очередью FIFO.
        /// </summary>
        public const string RedrivePolicy = "RedrivePolicy";
        /// <summary>
        /// Таймаут видимости сообщений в очереди в секундах. Возможные значения: от 0 до 43000 секунд. Значение по умолчанию: 30.
        /// </summary>
        public const string VisibilityTimeout = "VisibilityTimeout";
        /// <summary>
        /// Флаг, указывающий что создается очередь FIFO. 
        /// Возможные значения: true или false.
        /// </summary>
        public const string FifoQueue = "FifoQueue";
        /// <summary>
        /// Флаг, включающий дедупликацию по содержимому сообщения. 
        /// Возможные значения: true или false.
        /// </summary>
        public const string ContentBasedDeduplication = "ContentBasedDeduplication";
    }
}
