using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.TTS.Utils;

namespace YaCloudKit.TTS
{
    public abstract class YandexTtsService : IDisposable
    {
        public YandexTtsConfig Config { get; set; }
        private Lazy<HttpClient> httpClientLazy { get; set; }
        private HttpClient Client => httpClientLazy.Value;

        protected YandexTtsService(YandexTtsConfig config, Func<HttpClient> httpClientFactory)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            httpClientLazy = new Lazy<HttpClient>(httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory)));
        }

        /// <summary>
        /// Асинхронно выполняет запрос к сервису 
        /// </summary>
        /// <param name="options">опчии для выполнения запроса</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task<YandexTtsResponse> InvokeAsync(InvokeOptions options, CancellationToken cancellationToken)
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
            
            var httpResponse = await Client.SendAsync(request, cancellationToken);
            
            if (httpResponse.IsSuccessStatusCode)
            {
                return new YandexTtsResponse()
                {
                    RequestId = requestId,
                    StatusCode = httpResponse.StatusCode,
                    Content = await httpResponse.Content.ReadAsByteArrayAsync()
                };
            }
            else
            {
	            var message = await httpResponse.Content.ReadAsStringAsync();
                throw new YandexTtsServiceException(message, requestId, httpResponse.StatusCode);
            }
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
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && httpClientLazy.IsValueCreated)
                {
                    Client?.Dispose();
                }
                httpClientLazy = null;
                Config = null;
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
