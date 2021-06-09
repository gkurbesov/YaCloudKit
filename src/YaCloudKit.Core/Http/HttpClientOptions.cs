using System;

namespace YaCloudKit.Core.Http
{
    public class HttpClientOptions
    {
        public TimeSpan HttpClientTimeout { get; set; } = TimeSpan.FromMinutes(1);
#if !NETCOREAPP
        /// <summary>
        /// Указывает, сколько одновременных подключений можно создавать для каждого домена
        /// </summary>
        public int DefaultConnectionLimit { get; set; } = 5;
        public int ConnectionLeaseTimeoutMs { get; set; } = 60000;
        public Uri EndPoint { get; set; }
#endif
    }
}
