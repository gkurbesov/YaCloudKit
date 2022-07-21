using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.MQ.Model.Responses;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ
{
    public abstract class YandexMqService : IDisposable
    {
        private YandexMqConfig _config;
        private Lazy<HttpClient> _httpClientLazy;
        private HttpClient Client => _httpClientLazy.Value;

        protected YandexMqService(YandexMqConfig config, Func<HttpClient> httpClientFactory)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _httpClientLazy = new Lazy<HttpClient>(httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory)));
        }

        /// <summary>
        /// Асинхронно выполняет запрос к сервису 
        /// </summary>
        /// <typeparam name="TResponse">Тип ожидаемого ответа</typeparam>
        /// <param name="options">опчии для выполнения запроса</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<TResponse> InvokeAsync<TResponse>(InvokeOptions options,
            CancellationToken cancellationToken) where TResponse : YandexMessageQueueResponse, new()
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            IRequestContext requestContext = options.RequestMarshaller.Marshall(options.OriginalRequest);
            YandexMqHeaderBuilder.AddMainHeaders(requestContext, _config.EndPoint);

            var content = new ByteArrayContent(requestContext.GetContent());
            YandexMqHeaderBuilder.AddHttpHeaders(requestContext, content.Headers);

            YandexMqHeaderBuilder.AddAWSDateHeaders(requestContext);

            var signature = new YandexMqSigner(_config).Create(requestContext);
            YandexMqHeaderBuilder.AddHeaderAuthorization(requestContext, signature);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _config.EndPoint);
            YandexMqHeaderBuilder.AddHttpHeaders(requestContext, request.Headers);
            request.Content = content;


            var httpResponse = await Client.SendAsync(request, cancellationToken);

            using var stream = await httpResponse.Content.ReadAsStreamAsync();
            IResponseContext responseContext =
                new ResponseContext(httpResponse.StatusCode, httpResponse.Headers, stream);

            if (httpResponse.IsSuccessStatusCode)
                return options.ResponseUnmarshaller.Unmarshall<TResponse>(responseContext);
            else
                throw options.ResponseUnmarshaller.UnmarshallException(responseContext);
        }

        /// <summary>
        /// Проверяет не проведена ли очистка ресурсов
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (disposedValue)
                throw new ObjectDisposedException(GetType().Name);
        }

        #region IDisposable Support

        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _httpClientLazy.IsValueCreated)
                {
                    Client?.Dispose();
                }

                _httpClientLazy = null;
                _config = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}