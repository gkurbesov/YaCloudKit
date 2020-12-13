using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.Http;
using YaCloudKit.MQ.Marshallers;
using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ
{
    public class YandexMqClient : YandexMqService, IYandexMq
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessKeyID">Идентификатор ключа сервисного аккаунта</param>
        /// <param name="secretAccessKey">Секретный ключ сервисного аккаунта</param>
        public YandexMqClient(string accessKeyID, string secretAccessKey) :
            this(new YandexMqConfig(accessKeyID, secretAccessKey), new HttpServiceCaller())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">Настройки для выполнения запросов к api Yandex Message Queue</param>
        public YandexMqClient(YandexMqConfig config, IHttpServiceCaller httpCaller) :
            base(config, httpCaller)
        { }

        public Task<CreateQueueResponse> CreateQueueAsync(CreateQueueRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var option = new InvokeOptions()
            {
                OriginalRequest = request,
                RequestMarshaller = new CreateQueueRequestMarshaller(),
                ResponseUnmarshaller = new CreateQueueResponseUnmarshaller()
            };

            return InvokeAsync<CreateQueueResponse>(option, cancellationToken);
        }

        public Task<DeleteQueueResponse> DeleteQueueAsync(DeleteQueueRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var option = new InvokeOptions()
            {
                OriginalRequest = request,
                RequestMarshaller = new DeleteQueueRequestMarshaller(),
                ResponseUnmarshaller = new DeleteQueueResponseUnmarshaller()
            };

            return InvokeAsync<DeleteQueueResponse>(option, cancellationToken);
        }

        public Task<GetQueueAttributesResponse> GetQueueAttributesAsync(GetQueueAttributesRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var option = new InvokeOptions()
            {
                OriginalRequest = request,
                RequestMarshaller = new GetQueueAttributesRequestMarshaller(),
                ResponseUnmarshaller = new GetQueueAttributesResponseUnmarshaller()
            };

            return InvokeAsync<GetQueueAttributesResponse>(option, cancellationToken);
        }

        public Task<GetQueueUrlResponse> GetQueueUrlAsync(GetQueueUrlRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var option = new InvokeOptions()
            {
                OriginalRequest = request,
                RequestMarshaller = new GetQueueUrlRequestMarshaller(),
                ResponseUnmarshaller = new GetQueueUrlResponseUnmarshaller()
            };

            return InvokeAsync<GetQueueUrlResponse>(option, cancellationToken);
        }

        public Task<ListQueuesResponse> ListQueuesAsync(ListQueuesRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var option = new InvokeOptions()
            {
                OriginalRequest = request,
                RequestMarshaller = new ListQueuesRequestMarshaller(),
                ResponseUnmarshaller = new ListQueuesResponseUnmarshaller()
            };

            return InvokeAsync<ListQueuesResponse>(option, cancellationToken);
        }

        public Task<PurgeQueueResponse> PurgeQueueAsync(PurgeQueueRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var option = new InvokeOptions()
            {
                OriginalRequest = request,
                RequestMarshaller = new PurgeQueueRequestMarshaller(),
                ResponseUnmarshaller = new PurgeQueueResponseUnmarshaller()
            };

            return InvokeAsync<PurgeQueueResponse>(option, cancellationToken);
        }

        public Task<SetQueueAttributesResponse> SetQueueAttributesAsync(SetQueueAttributesRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var option = new InvokeOptions()
            {
                OriginalRequest = request,
                RequestMarshaller = new SetQueueAttributesRequestMarshaller(),
                ResponseUnmarshaller = new SetQueueAttributesResponseUnmarshaller()
            };

            return InvokeAsync<SetQueueAttributesResponse>(option, cancellationToken);
        }

        public Task<ChangeMessageVisibilityResponse> ChangeMessageVisibilityAsync(ChangeMessageVisibilityRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var option = new InvokeOptions()
            {
                OriginalRequest = request,
                RequestMarshaller = new ChangeMessageVisibilityRequestMarshaller(),
                ResponseUnmarshaller = new ChangeMessageVisibilityResponseUnmarshaller()
            };

            return InvokeAsync<ChangeMessageVisibilityResponse>(option, cancellationToken);
        }

        public Task<ChangeMessageVisibilityBatchResponse> ChangeMessageVisibilityBatchAsync(ChangeMessageVisibilityBatchRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteMessageResponse> DeleteMessageAsync(DeleteMessageRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var option = new InvokeOptions()
            {
                OriginalRequest = request,
                RequestMarshaller = new DeleteMessageRequestMarshaller(),
                ResponseUnmarshaller = new DeleteMessageResponseUnmarshaller()
            };

            return InvokeAsync<DeleteMessageResponse>(option, cancellationToken);
        }

        public Task<DeleteMessageBatchResponse> DeleteMessageBatchAsync(DeleteMessageBatchRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ReceiveMessageResponse> ReceiveMessageAsync(ReceiveMessageRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SendMessageResponse> SendMessageAsync(SendMessageRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var option = new InvokeOptions()
            {
                OriginalRequest = request,
                RequestMarshaller = new SendMessageRequestMarshaller(),
                ResponseUnmarshaller = new SendMessageResponseUnmarshaller()
            };

            return InvokeAsync<SendMessageResponse>(option, cancellationToken);
        }

        public Task<SendMessageBatchResponse> SendMessageBatchAsync(SendMessageBatchRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
