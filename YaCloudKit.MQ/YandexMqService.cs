using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ
{
    public abstract class YandexMqService : IDisposable
    {
        public YandexMqConfig Config { get; set; }

        protected YandexMqService(YandexMqConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            Config = config;
        }

        /// <summary>
        /// Асинхронно выполняет запрос к сервису 
        /// </summary>
        /// <typeparam name="TResponse">Тип ожидаемого ответа</typeparam>
        /// <param name="options">опчии для выполнения запроса</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<TResponse> InvokeAsync<TResponse>(InvokeOptions options, CancellationToken cancellationToken)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));


            ThrowIfDisposed();

            IRequestContext requestContext = options.RequestMarshaller.Marshall(options.OriginalRequest);
            YandexMqHeaderBuilder.AddHeaders(requestContext, Config.EndPoint);

            var signature = new YandexMqSigner(Config).Create(requestContext);
            YandexMqHeaderBuilder.AddHeaderAuthorization(requestContext, signature);

            var content = new ByteArrayContent(requestContext.GetContent());
            YandexMqHeaderBuilder.AddHttpHeaders(requestContext, content.Headers);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Config.EndPoint);
            request.Content = content;
            YandexMqHeaderBuilder.AddHttpHeaders(requestContext, request.Headers);

            var httpResponse = await GetHttpClient().SendAsync(request, cancellationToken);
            var response = await options.ResponseUnmarshaller.UnmarshallAsync<TResponse>(httpResponse);

            return response;
        }

        /// <summary>
        /// Получить новый экземпляр http-клиента
        /// </summary>
        /// <returns></returns>
        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.Timeout = Config.HttpClientTimeout;
            return client;
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
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }
                Config = null;
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
