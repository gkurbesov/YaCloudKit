using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.Monitoring
{
    /// <summary>
    /// Настройки клиента для работы с сервисом
    /// </summary>
    public class MonitoringConfig
    {
        public static readonly Uri DEFAULT_ENDPOINT = new Uri("https://monitoring.api.cloud.yandex.net/monitoring/v2/data/write");

        /// <summary>
        /// IAM-токен для авторизации запросов на Yandex.Cloud
        /// </summary>
        public string IamToken { get; internal set; }
        /// <summary>
        /// Ресурс для выполнения запроса
        /// </summary>
        public Uri EndPoint { get; set; } = DEFAULT_ENDPOINT;
        /// <summary>
        /// Максимальный таймаут выполнения HTTP запроса
        /// </summary>
        public TimeSpan HttpClientTimeout { get; set; } = TimeSpan.FromMinutes(1);

        private MonitoringConfig() { }
    }
}
