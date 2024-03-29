﻿using System;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для удаления сообщения из очереди. 
    /// Чтобы указать, какое сообщение следует удалить, используйте параметр <code>ReceiptHandle</code>. 
    /// Сообщение можно удалить, даже если сообщение получено и обрабатывается другим получателем. 
    /// Message Queue автоматически удаляет сообщения, если период, указанный в параметре <code>RetentionPeriod</code> закончился.
    /// </summary>
    public class DeleteMessageRequest : BaseRequest
    {
        /// <summary>
        /// URL очереди, в которой находится сообщение.
        /// </summary>
        public string QueueUrl { get; set; }
        /// <summary>
        /// Параметр <code>ReceiptHandle</code> из запроса <code>ReceiveMessage</code>.
        /// </summary>
        public string ReceiptHandle { get; set; }

        public DeleteMessageRequest()
            : base("DeleteMessage") { }

        /// <summary>
        /// url очереди из которой будет удалено сообщение
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DeleteMessageRequest SetQueueUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Queue url cannot was null or empty");
            QueueUrl = value;
            return this;
        }

        /// <summary>
        /// Параметр для удаления сообщения
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DeleteMessageRequest SetReceiptHandle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "ReceiptHandle cannot was null or empty");
            ReceiptHandle = value;
            return this;
        }

    }
}
