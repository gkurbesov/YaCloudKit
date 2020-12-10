using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YaCloudKit.Http
{
    public class HttpServiceCaller : IHttpServiceCaller, IDisposable
    {
        private bool disposedValue;
        private HttpClient client;
        public async Task<T> CallService<T>(HttpClientOptions options, Func<HttpClient, Task<T>> func)
        {
            if (disposedValue)
                throw new ObjectDisposedException(this.GetType().Name);

            if (client == null)
            {
#if !NETCOREAPP
                ServicePointManager.DefaultConnectionLimit = options.DefaultConnectionLimit;
                var servicePoint = ServicePointManager.FindServicePoint(options.EndPoint);
                if (servicePoint != null)
                    servicePoint.ConnectionLeaseTimeout = options.ConnectionLeaseTimeoutMs;
#endif
                client = new HttpClient();
                client.Timeout = options.HttpClientTimeout;
            }
            return await func(client);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    client?.Dispose();
                }
                client = null;
                disposedValue = true;
            }
        }

        ~HttpServiceCaller()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
