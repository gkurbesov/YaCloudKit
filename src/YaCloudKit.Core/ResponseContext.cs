using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace YaCloudKit.Core
{
    public class ResponseContext : IResponseContext
    {
        public HttpStatusCode StatusCode { get; private set; }

        public HttpResponseHeaders Headers { get; private set; }

        public Stream ContentStream { get; private set; }

        public ResponseContext(HttpStatusCode httpStatusCode, HttpResponseHeaders headers, Stream content)
        {
            StatusCode = httpStatusCode;
            Headers = headers;
            ContentStream = content;
        }
    }
}
