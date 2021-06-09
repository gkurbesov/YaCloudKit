using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.Core;
using YaCloudKit.Core.Http;
using YaCloudKit.MQ.Model.Responses;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ
{
    public abstract class YandexMqService : IDisposable
    {
        public YandexMqConfig Config { get; set; }
        private IHttpServiceCaller ServiceCaller { get; set; }

        protected YandexMqService(YandexMqConfig config, IHttpServiceCaller httpServiceCaller)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            if (httpServiceCaller == null)
                throw new ArgumentNullException(nameof(httpServiceCaller));

            Config = config;
            ServiceCaller = httpServiceCaller;
        }

        /// <summary>
        /// Асинхронно выполняет запрос к сервису 
        /// </summary>
        /// <typeparam name="TResponse">Тип ожидаемого ответа</typeparam>
        /// <param name="options">опчии для выполнения запроса</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<TResponse> InvokeAsync<TResponse>(InvokeOptions options, CancellationToken cancellationToken) where TResponse : YandexMessageQueueResponse, new()
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            IRequestContext requestContext = options.RequestMarshaller.Marshall(options.OriginalRequest);
            YandexMqHeaderBuilder.AddMainHeaders(requestContext, Config.EndPoint);

            var content = new ByteArrayContent(requestContext.GetContent());
            YandexMqHeaderBuilder.AddHttpHeaders(requestContext, content.Headers);

            YandexMqHeaderBuilder.AddAWSDateHeaders(requestContext);

            var signature = new YandexMqSigner(Config).Create(requestContext);
            YandexMqHeaderBuilder.AddHeaderAuthorization(requestContext, signature);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Config.EndPoint);
            YandexMqHeaderBuilder.AddHttpHeaders(requestContext, request.Headers);
            request.Content = content;


            return await ServiceCaller.CallService<TResponse>(GetHttpOptions(), async (client) =>
            {
                var httpResponse = await client.SendAsync(request, cancellationToken);

                var stream = await httpResponse.Content.ReadAsStreamAsync();
                IResponseContext responseContext = new ResponseContext(httpResponse.StatusCode, httpResponse.Headers, stream);

                if (httpResponse.IsSuccessStatusCode)
                    return options.ResponseUnmarshaller.Unmarshall<TResponse>(responseContext);
                else
                    throw options.ResponseUnmarshaller.UnmarshallException(responseContext);
            });
        }

        /// <summary>
        /// Получить новый экземпляр http-клиента
        /// </summary>
        /// <returns></returns>
        protected HttpClientOptions GetHttpOptions() => new HttpClientOptions()
        {
            HttpClientTimeout = Config.HttpClientTimeout,
#if !NETCOREAPP
            EndPoint = Config.EndPoint
#endif
        };

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
                if (disposing)
                {
                    ServiceCaller?.Dispose();
                }
                Config = null;
                ServiceCaller = null;
                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~YandexMqService()
        // {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
