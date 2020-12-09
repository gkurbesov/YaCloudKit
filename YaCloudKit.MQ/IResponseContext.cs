using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace YaCloudKit.MQ
{
    public interface IResponseContext
    {
        HttpStatusCode StatusCode { get; }
        HttpResponseHeaders Headers { get; }
        Stream ContentStream { get; }
    }
}
