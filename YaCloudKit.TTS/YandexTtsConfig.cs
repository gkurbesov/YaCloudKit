using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.TTS
{
    /// <summary>
    /// Параметры и настройки для выполнения запросов к Yandex SpeechKit (TTS)
    /// </summary>
    public class YandexTtsConfig
    {
        public const string DEFAULT_ENDPOINT = "https://tts.api.cloud.yandex.net/speech/v1/tts:synthesize";

        /// <summary>
        /// Конечная точка для выполнения запросов
        /// </summary>
        public Uri EndPoint { get; set; }
        /// <summary>
        ///  API-ключ сервиснрнр аккаунта для выполнения запросов.
        ///  Сервис использует каталог, в котором был создан сервисный аккаунт.
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// Максимальный таймаут выполнения HTTP запроса
        /// </summary>
        public TimeSpan HttpClientTimeout { get; set; } = TimeSpan.FromMinutes(1);

        public YandexTtsConfig()
        {
            EndPoint = new Uri(DEFAULT_ENDPOINT);
        }

        /// <summary>
        /// Создаст экземпляр настроек с указанными параметрами
        /// </summary>
        /// <param name="apiKey">API-ключ сервиснрнр аккаунта</param>
        public YandexTtsConfig(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            ApiKey = apiKey;

            EndPoint = new Uri(DEFAULT_ENDPOINT);
        }

        /// <summary>
        /// Создаст экземпляр настроек с указанными параметрами
        /// </summary>
        /// <param name="apiKey">API-ключ сервиснрнр аккаунта</param>
        /// <param name="endpoint">Конечная точка для выполнения запросов</param>
        public YandexTtsConfig(string apiKey, string endpoint)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            ApiKey = apiKey;

            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentNullException(nameof(endpoint));
            EndPoint = new Uri(endpoint);
        }
    }
}
