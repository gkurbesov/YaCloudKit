using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ
{
    public class YandexMqConfig
    {
        public const string DEFAULT_SERVICE_NAME = "sqs";
        public const string DEFAULT_SERVICE_VERSION = "2012-11-05";
        public const string DEFAULT_REGION = "ru-central1";
        public const string DEFAULT_ENDPOINT = "https://message-queue.api.cloud.yandex.net/";

        /// <summary>
        /// Название сервиса AWS, используется для создания подписи AWS4
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Название региона AWS, используется для создания подписи AWS4
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// Конечная точка для выполнения запросов
        /// </summary>
        public Uri EndPoint { get; set; }
        /// <summary>
        /// Идентификатор ключа сервисного аккаунта
        /// </summary>
        public string AccessKeyID { get; set; }
        /// <summary>
        /// Секретный ключ сервисного аккаунта
        /// </summary>
        public string SecretAccessKey { get; set; }

        public YandexMqConfig()
        {
            ServiceName = DEFAULT_SERVICE_NAME;
            Region = DEFAULT_REGION;
            EndPoint = new Uri(DEFAULT_ENDPOINT);
        }

        /// <summary>
        /// Создаст экземпляр настроек с указанными параметрами сервисного аккаунта и значениями по умолчанию
        /// </summary>
        /// <param name="accessKeyId">Идентификатор ключа сервисного аккаунта</param>
        /// <param name="secretAccessKey">Секретный ключ сервисного аккаунта</param>
        public YandexMqConfig(string accessKeyId, string secretAccessKey)
        {
            if (string.IsNullOrWhiteSpace(accessKeyId))
                throw new ArgumentNullException(nameof(accessKeyId));
            AccessKeyID = accessKeyId;

            if (string.IsNullOrWhiteSpace(secretAccessKey))
                throw new ArgumentNullException(nameof(secretAccessKey));
            SecretAccessKey = secretAccessKey; 
            
            ServiceName = DEFAULT_SERVICE_NAME;
            Region = DEFAULT_REGION;
            EndPoint = new Uri(DEFAULT_ENDPOINT);
        }

        /// <summary>
        /// Создаст экземпляр настроек с указанными параметрами
        /// </summary>
        /// <param name="accessKeyId">Идентификатор ключа сервисного аккаунта</param>
        /// <param name="secretAccessKey">Секретный ключ сервисного аккаунта</param>
        /// <param name="region">Название региона AWS, используется для создания подписи AWS4</param>
        /// <param name="endpoint">Конечная точка для выполнения запросов</param>
        public YandexMqConfig(string accessKeyId, string secretAccessKey, string region, string endpoint)
        {
            if (string.IsNullOrWhiteSpace(accessKeyId))
                throw new ArgumentNullException(nameof(accessKeyId));
            AccessKeyID = accessKeyId;

            if (string.IsNullOrWhiteSpace(secretAccessKey))
                throw new ArgumentNullException(nameof(secretAccessKey));
            SecretAccessKey = secretAccessKey;

            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentNullException(nameof(region));
            Region = region;

            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentNullException(nameof(endpoint));
            EndPoint = new Uri(endpoint);

            ServiceName = DEFAULT_SERVICE_NAME;
        }
    }
}
