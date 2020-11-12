using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YaCloudKit.MQ
{
    public class DefaultRequest : IRequest
    {
        public IDictionary<string, string> Headers { get; internal set; }

        public IDictionary<string, string> Parameters { get; internal set; }

        public long ContentLength { get; internal set; }

        public Task<byte[]> ReadContentAsync()
        {
            throw new NotImplementedException();
        }
    }
}
