using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace YaCloudKit.Core.Http
{
    public interface IHttpServiceCaller : IDisposable
    {
        Task<T> CallService<T>(HttpClientOptions options, Func<HttpClient, Task<T>> func);
    }
}
