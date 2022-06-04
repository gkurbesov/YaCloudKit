using System.Collections.Generic;

namespace YaCloudKit.MQ.Model.Responses
{
    public class ResponseMetadata
    {
        public string RequestId { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    }
}
