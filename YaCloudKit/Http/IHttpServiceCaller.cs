using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace YaCloudKit.Http
{
    public interface IHttpServiceCaller : IDisposable
    {
        Task<T> CallService<T>(HttpClientOptions options, Func<HttpClient, Task<T>> func);
    }
}
