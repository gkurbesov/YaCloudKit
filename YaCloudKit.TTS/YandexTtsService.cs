using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.Http;
using YaCloudKit.TTS.Utils;

namespace YaCloudKit.TTS
{
    public abstract class YandexTtsService : IDisposable
    {
        public YandexTtsConfig Config { get; set; }
        private IHttpServiceCaller ServiceCaller { get; set; }

        protected YandexTtsService(YandexTtsConfig config, IHttpServiceCaller httpServiceCaller)
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
        protected async Task<YandexTtsResponse> InvokeAsync<TResponse>(InvokeOptions options, CancellationToken cancellationToken)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            IRequestContext requestContext = new RequestContext();
            var requestId = Config.LoggingEnabled ? Guid.NewGuid().ToString() : null;

            YandexTtsHeaderBuilder.AddMainHeaders(requestContext, Config);
            YandexTtsHeaderBuilder.AddLoggingHeaders(requestContext, requestId);

            RequestParametersHelper.AddTextParam(requestContext, options.Text, options.SSML);
            RequestParametersHelper.AddVoiceParam(requestContext, options.Voice);
            RequestParametersHelper.AddFormatParam(requestContext, options.AudioFormat);
            RequestParametersHelper.AddFolderParam(requestContext, Config);


            var content = new FormUrlEncodedContent(requestContext.RequestParameters);
            YandexTtsHeaderBuilder.AddHttpHeaders(requestContext, content.Headers);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Config.EndPoint);
            YandexTtsHeaderBuilder.AddHttpHeaders(requestContext, request.Headers);
            request.Content = content;


            return await ServiceCaller.CallService<YandexTtsResponse>(GetHttpOptions(), async (client) =>
            {
                var httpResponse = await client.SendAsync(request, cancellationToken);

                var stream = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    return new YandexTtsResponse()
                    {
                        RequestId = requestId,
                        StatusCode = httpResponse.StatusCode,
                        Content = stream
                    };
                }
                else
                {
                    var message = new StreamReader(stream).ReadToEnd();
                    throw new YandexTtsServiceException(message, requestId, httpResponse.StatusCode);
                }
            });
        }

        /// <summary>
        /// Получить новый экземпляр http-клиента
        /// </summary>
        /// <returns></returns>
        protected HttpClientOptions GetHttpOptions() => new HttpClientOptions()
        {
            HttpClientTimeout = Config.HttpClientTimeout,
            EndPoint = Config.EndPoint
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
                    ServiceCaller.Dispose();
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
