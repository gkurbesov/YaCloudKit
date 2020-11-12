using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YaCloudKit.MQ
{
    public interface IRequest
    {
        IDictionary<string, string> Headers { get; }
        IDictionary<string, string> Parameters { get; }
        long ContentLength { get; }
        Task<byte[]> ReadContentAsync();
    }
}
