using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace YaCloudKit.MQ.Model.Responses
{
    public abstract class YandexMessageQueueResponse
    {
        public ResponseMetadata ResponseMetadata { get; set; } = new ResponseMetadata();
        public long ContentLength { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
