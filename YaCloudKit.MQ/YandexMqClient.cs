using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ
{
    public class YandexMqClient : YandexMqService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessKeyID">Идентификатор ключа сервисного аккаунта</param>
        /// <param name="secretAccessKey">Секретный ключ сервисного аккаунта</param>
        public YandexMqClient(string accessKeyID, string secretAccessKey) :
            this(new YandexMqConfig(accessKeyID, secretAccessKey))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">Настройки для выполнения запросов к api Yandex Message Queue</param>
        public YandexMqClient(YandexMqConfig config) :
            base(config)
        { }
    }
}
