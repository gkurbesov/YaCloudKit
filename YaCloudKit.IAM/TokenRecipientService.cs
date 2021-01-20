using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.Core;
using YaCloudKit.Core.Http;

namespace YaCloudKit.IAM
{
    public class TokenRecipientService : IDisposable
    {
        public HttpClientOptions HttpOptions { get; set; }
        private IHttpServiceCaller ServiceCaller { get; set; }

        protected TokenRecipientService(HttpClientOptions httpOptions, IHttpServiceCaller httpServiceCaller)
        {
            if (httpOptions == null)
                throw new ArgumentNullException(nameof(httpOptions));
            if (httpServiceCaller == null)
                throw new ArgumentNullException(nameof(httpServiceCaller));

            HttpOptions = httpOptions;
            ServiceCaller = httpServiceCaller;
        }


        protected async Task<string> InvokeAsync(TokenRecipientOptions options, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            IRequestContext requestContext = new RequestContext();
            //YandexTtsHeaderBuilder.AddMainHeaders(requestContext, Config);
            //YandexTtsHeaderBuilder.AddLoggingHeaders(requestContext, requestId);

            //RequestParametersHelper.AddTextParam(requestContext, options.Text, options.SSML);
            //RequestParametersHelper.AddVoiceParam(requestContext, options.Voice);
            //RequestParametersHelper.AddFormatParam(requestContext, options.AudioFormat);
            //RequestParametersHelper.AddFolderParam(requestContext, Config);


            //var content = new FormUrlEncodedContent(requestContext.RequestParameters);
            //YandexTtsHeaderBuilder.AddHttpHeaders(requestContext, content.Headers);

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Config.EndPoint);
            //YandexTtsHeaderBuilder.AddHttpHeaders(requestContext, request.Headers);
            //request.Content = content;


            //return await ServiceCaller.CallService<YandexTtsResponse>(GetHttpOptions(), async (client) =>
            //{
            //    var httpResponse = await client.SendAsync(request, cancellationToken);

            //    var stream = await httpResponse.Content.ReadAsStreamAsync();
            //    if (httpResponse.IsSuccessStatusCode)
            //    {
            //        return new YandexTtsResponse()
            //        {
            //            RequestId = requestId,
            //            StatusCode = httpResponse.StatusCode,
            //            Content = stream
            //        };
            //    }
            //    else
            //    {
            //        var message = new StreamReader(stream).ReadToEnd();
            //        throw new YandexTtsServiceException(message, requestId, httpResponse.StatusCode);
            //    }
            //});
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
                    ServiceCaller?.Dispose();
                }
                HttpOptions = null;
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
