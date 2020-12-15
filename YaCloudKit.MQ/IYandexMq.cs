using System;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ
{
    public interface IYandexMq : IDisposable
    {
        /// <summary>
        /// Метод для создания новой стандартной или FIFO очереди.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CreateQueueResponse> CreateQueueAsync(CreateQueueRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для удаления очереди. Если указанная очередь не существует, Message Queue сообщит об успешном выполнении запроса.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeleteQueueResponse> DeleteQueueAsync(DeleteQueueRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для получения атрибутов указанной очереди.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<GetQueueAttributesResponse> GetQueueAttributesAsync(GetQueueAttributesRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для получения URL указанной очереди. Укажите имя очереди, чтобы получить ее URL
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<GetQueueUrlResponse> GetQueueUrlAsync(GetQueueUrlRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для получения списка очередей в каталоге. Максимальное количество очередей в ответе — 1000. Очереди можно отфильтровать с помощью параметра QueueNamePrefix
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ListQueuesResponse> ListQueuesAsync(ListQueuesRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для очистки очереди сообщений. Удаление сообщений занимает некоторое время.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PurgeQueueResponse> PurgeQueueAsync(PurgeQueueRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для задания атрибутов указанной очереди. Изменение атрибутов может занять до 60 секунд. Изменение атрибута MessageRetentionPeriod может занять до 15 минут
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SetQueueAttributesResponse> SetQueueAttributesAsync(SetQueueAttributesRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для установки таймаута видимости сообщению, находящемуся в обработке. Суммарная длительность таймаута не может быть более 12 часов
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChangeMessageVisibilityResponse> ChangeMessageVisibilityAsync(ChangeMessageVisibilityRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для установки таймаута видимости группе сообщений в указанной очереди. Можно отправить до 10 вызовов ChangeMessageVisibility в одном вызове ChangeMessageVisibilityBatch
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChangeMessageVisibilityBatchResponse> ChangeMessageVisibilityBatchAsync(ChangeMessageVisibilityBatchRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для удаления сообщения из очереди. Чтобы указать, какое сообщение следует удалить, используйте параметр ReceiptHandle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeleteMessageResponse> DeleteMessageAsync(DeleteMessageRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для удаления нескольких сообщений из очереди. Удалять можно не более 10 сообщений одновременно
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeleteMessageBatchResponse> DeleteMessageBatchAsync(DeleteMessageBatchRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для приема от одного до десяти сообщений из указанной очереди. С помощью параметра WaitTimeSeconds выполняются long-polling запросы.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ReceiveMessageResponse> ReceiveMessageAsync(ReceiveMessageRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для отправки сообщения в указанную очередь. В теле сообщения можно передавать только XML, JSON и неформатированный текст
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SendMessageResponse> SendMessageAsync(SendMessageRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Метод для одновременной отправки до десяти сообщений в указанную очередь. При отправке сообщений в очередь FIFO они будут поступать в порядке отправления.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SendMessageBatchResponse> SendMessageBatchAsync(SendMessageBatchRequest request, CancellationToken cancellationToken = default);
    }
}
