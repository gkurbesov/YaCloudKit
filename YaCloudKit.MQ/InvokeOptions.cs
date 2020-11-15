using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Marshallers;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ
{
    public class InvokeOptions
    {
        public BaseRequest OriginalRequest { get; set; }
        public IMarshaller<BaseRequest> RequestMarshaller { get; set; }
    }
}
