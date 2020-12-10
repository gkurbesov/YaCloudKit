using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ
{
    internal class RequestContext : IRequestContext
    {
        public IDictionary<string, string> RequestParameters { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public DateTime RequestDateTime { get; set; } = DateTime.UtcNow;
        private byte[] content;

        public RequestContext() { }
        public RequestContext(IDictionary<string, string> requestParameters)
        {
            if (requestParameters == null)
                throw new ArgumentNullException(nameof(requestParameters));

            RequestParameters = requestParameters;
        }

        public RequestContext(IDictionary<string, string> requestParameters, IDictionary<string, string> headers)
        {
            if (requestParameters == null)
                throw new ArgumentNullException(nameof(requestParameters));

            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            RequestParameters = requestParameters;
            Headers = headers;
        }

        public IRequestContext AddParametr(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(key);

            RequestParameters.Add(key, value);

            return this;
        }

        public IRequestContext AddHeader(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(key);

            Headers.Add(key, value);

            return this;
        }

        public byte[] GetContent()
        {
            if (content == null)
                content = UrlEncodedContentBuilder.GetContentByteArray(RequestParameters);
            return content;
        }
    }
}
