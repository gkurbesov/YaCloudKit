using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.Core.Http;
using Jose;
using System.Security.Cryptography;

namespace YaCloudKit.IAM
{
    public class TokenRecipient : ITokenRecipient, IDisposable
    {
        public HttpClientOptions HttpOptions { get; set; }
        private IHttpServiceCaller ServiceCaller { get; set; }

        public TokenRecipient()
            : this(new HttpClientOptions(), new HttpServiceCaller())
        { }

        public TokenRecipient(HttpClientOptions httpClientOptions, IHttpServiceCaller httpServiceCaller)
        {
            if (httpClientOptions == null)
                throw new ArgumentNullException(nameof(httpClientOptions));
            if (httpServiceCaller == null)
                throw new ArgumentNullException(nameof(httpServiceCaller));

            HttpOptions = httpClientOptions;
            ServiceCaller = httpServiceCaller;
        }

        public Task<string> GetIamToken(TokenRecipientOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
            //if (options == null)
            //    throw new ArgumentNullException(nameof(options));

            //ThrowIfDisposed();
            //cancellationToken.ThrowIfCancellationRequested();

            //IRequestContext requestContext = new RequestContext();

            //var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            //var headers = new Dictionary<string, object>()
            //{
            //    { "kid", options.KeyId }
            //};
            //var payload = new Dictionary<string, object>()
            //{
            //    { "aud", options.RequestPath },
            //    { "iss", options.ServiceAccountId },
            //    { "iat", now },
            //    { "exp", now + 3600 }
            //};
            //RsaPrivateCrtKeyParameters privateKeyParams;
            //using (var pemStream = File.OpenText("private.pem"))
            //{
            //    privateKeyParams = new PemReader(pemStream).ReadObject() as RsaPrivateCrtKeyParameters;
            //}

            //using (var rsa = new RSACryptoServiceProvider())
            //{
            //    rsa.ImportParameters()
            //    string encodedToken = Jose.JWT.Encode(payload, rsa, JwsAlgorithm.PS256, headers);
            //}
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
