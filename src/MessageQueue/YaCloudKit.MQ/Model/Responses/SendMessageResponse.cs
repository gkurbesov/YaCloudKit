namespace YaCloudKit.MQ.Model.Responses
{
    public class SendMessageResponse : YandexMessageQueueResponse
    {
        /// <summary>
        /// MD5-хэш строки атрибута.
        /// </summary>
        public string MD5OfMessageAttributes { get; set; }
        /// <summary>
        /// MD5-хэш тела сообщения.
        /// </summary>
        public string MD5OfMessageBody { get; set; }
        /// <summary>
        /// Идентификатор отправленного сообщения.
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// Номер сообщения, используется в очередях FIFO в рамках группы сообщений с одинаковым MessageGroupId. 
        /// Длина номера — 128 бит, SequenceNumber наращивается в пределах группы с одинаковым MessageGroupId.
        /// </summary>
        public string SequenceNumber { get; set; }
    }
}
