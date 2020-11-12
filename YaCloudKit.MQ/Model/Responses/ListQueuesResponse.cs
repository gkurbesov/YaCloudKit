using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model.Responses
{
    public class ListQueuesResponse : YandexMessageQueueResponse
    {
        private IList<string> _queueUrls;
        public IList<string> QueueUrls
        {
            get
            {
                if (_queueUrls == null)
                    return new List<string>();
                else
                    return _queueUrls;
            }
            set { _queueUrls = value; }
        }
    }
}
