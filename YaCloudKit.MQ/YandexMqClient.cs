using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ
{
    public class YandexMqClient : YandexMqService
    {
        public YandexMqClient(string accessKeyID, string secretAccessKey) :
            this(new YandexMqConfig(accessKeyID, secretAccessKey))
        { }

        public YandexMqClient(YandexMqConfig config) :
            base(config)
        { }
    }
}
