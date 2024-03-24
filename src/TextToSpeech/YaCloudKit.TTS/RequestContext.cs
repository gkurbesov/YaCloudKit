using System;
using System.Collections.Generic;


namespace YaCloudKit.TTS
{
    public class RequestContext : IRequestContext
    {
        public IDictionary<string, string> RequestParameters { get; } = new Dictionary<string, string>();
        public IDictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        
        public DateTime RequestDateTime { get; } = DateTime.UtcNow;
        
        private byte[] _content;

        public RequestContext() { }
        public RequestContext(IDictionary<string, string> requestParameters)
        {
            RequestParameters = requestParameters ?? throw new ArgumentNullException(nameof(requestParameters));
        }

        public RequestContext(IDictionary<string, string> requestParameters, IDictionary<string, string> headers)
        {
            RequestParameters = requestParameters ?? throw new ArgumentNullException(nameof(requestParameters));
            Headers = headers ?? throw new ArgumentNullException(nameof(headers));
        }

        public IRequestContext AddParameter(string key, string value)
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
            return _content ??= UrlEncodedContentBuilder.GetContentByteArray(RequestParameters);
        }
    }
}
