using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace YaCloudKit.MQ.Model.Responses
{
    public class ResponseMetadata
    {
        public string RequestId { get; set; }

        private IDictionary<string, string> _metadata;
        public IDictionary<string, string> Metadata
        {
            get
            {
                if (_metadata == null)
                    _metadata = new Dictionary<string, string>();

                return _metadata;
            }
        }
    }
}
