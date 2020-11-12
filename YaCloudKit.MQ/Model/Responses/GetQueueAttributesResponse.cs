using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Responses
{
    public class GetQueueAttributesResponse : YandexMessageQueueResponse
    {
        private IDictionary<string, string> _attributes;
        public IDictionary<string, string> Attributes
        {
            get
            {
                if (_attributes == null)
                    _attributes = new Dictionary<string, string>();

                return _attributes;
            }
        }
    }
}
