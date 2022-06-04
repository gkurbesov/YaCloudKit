using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ
{
    public static class YandexMqClientExtension
    {
        /// <summary>
        /// Метод для отправки сообщения в указанную очередь. В теле сообщения можно передавать только XML, JSON и неформатированный текст
        /// </summary>
        /// <param name="mq"></param>
        /// <param name="queueUrl"></param>
        /// <param name="messageBody"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<SendMessageResponse> SendMessageAsync(this IYandexMq mq, string queueUrl, string messageBody, CancellationToken cancellationToken = default)
        {
            var request = new SendMessageRequest()
            {
                QueueUrl = queueUrl,
                MessageBody = messageBody
            };
            return mq.SendMessageAsync(request, cancellationToken);
        }

        /// <summary>
        /// Метод для отправки сообщения в указанную очередь. В теле сообщения можно передавать только XML, JSON и неформатированный текст
        /// </summary>
        /// <param name="mq"></param>
        /// <param name="queueUrl"></param>
        /// <param name="messageBody"></param>
        /// <param name="attributes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<SendMessageResponse> SendMessageAsync(this IYandexMq mq, string queueUrl, string messageBody, Dictionary<string, MessageAttributeValue> attributes, CancellationToken cancellationToken = default)
        {
            var request = new SendMessageRequest()
            {
                QueueUrl = queueUrl,
                MessageBody = messageBody,
                MessageAttribute = attributes
            };
            return mq.SendMessageAsync(request, cancellationToken);
        }

        /// <summary>
        /// Метод для удаления сообщения из очереди. Чтобы указать, какое сообщение следует удалить, используйте параметр ReceiptHandle
        /// </summary>
        /// <param name="mq"></param>
        /// <param name="queueUrl"></param>
        /// <param name="receiptHandle"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<DeleteMessageResponse> DeleteMessageAsync(this IYandexMq mq, string queueUrl, string receiptHandle, CancellationToken cancellationToken = default)
        {
            var request = new DeleteMessageRequest()
            {
                QueueUrl = queueUrl,
                ReceiptHandle = receiptHandle
            };
            return mq.DeleteMessageAsync(request, cancellationToken);
        }
    }
}
