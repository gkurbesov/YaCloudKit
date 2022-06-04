using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.Core.Http;
using YaCloudKit.IAM.Utils;

namespace YaCloudKit.IAM
{
    public class TokenRecipient : ITokenRecipient, IDisposable
    {
        private IHttpServiceCaller ServiceCaller { get; set; }

        public TokenRecipient()
             : this(new HttpServiceCaller())
        { }

        public TokenRecipient(IHttpServiceCaller httpServiceCaller)
        {
            if (httpServiceCaller == null)
                throw new ArgumentNullException(nameof(httpServiceCaller));

            ServiceCaller = httpServiceCaller;
        }


        public async Task<IamTokenCreateResult> GetIamToken(TokenRecipientOptions options, CancellationToken cancellationToken = default)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            var jsonContent = !string.IsNullOrWhiteSpace(options.JwtToken) ?
                JsonBodyHelper.JwtBody(options.JwtToken) : JsonBodyHelper.OauthBody(options.OauthToken);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, options.EndPoint);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            request.Content = content;


            var httpOptions = new HttpClientOptions()
            {
                HttpClientTimeout = options.HttpClientTimeout,
#if !NETCOREAPP
                EndPoint = options.EndPoint
#endif
            };


            return await ServiceCaller.CallService<IamTokenCreateResult>(httpOptions, async (client) =>
            {
                var httpResponse = await client.SendAsync(request, cancellationToken);

                var stream = await httpResponse.Content.ReadAsStreamAsync();
                var resultContent = await new StreamReader(stream).ReadToEndAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    try
                    {
                        return JsonBodyHelper.DeserializeResult(resultContent);
                    }
                    catch (Exception ex)
                    {
                        throw new YandexIamServiceException(resultContent, ex, httpResponse.StatusCode);
                    }
                }
                else
                {
                    throw new YandexIamServiceException(resultContent, httpResponse.StatusCode);
                }
            });
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
