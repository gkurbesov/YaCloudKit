using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace YaCloudKit.TTS
{
    public class ResponseContext : IResponseContext
    {
        public HttpStatusCode StatusCode { get; }

        public HttpResponseHeaders Headers { get; }

        public Stream ContentStream { get; }

        public ResponseContext(HttpStatusCode httpStatusCode, HttpResponseHeaders headers, Stream content)
        {
            StatusCode = httpStatusCode;
            Headers = headers;
            ContentStream = content;
        }
    }
}
