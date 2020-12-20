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
        public static readonly Uri DEFAULT_ENDPOINT = new Uri("https://tts.api.cloud.yandex.net/speech/v1/tts:synthesize");

        /// <summary>
        /// Конечная точка для выполнения запросов
        /// </summary>
        public Uri EndPoint { get; set; }
        /// <summary>
        /// IAM-токен для авторизации запросов на Yandex.Cloud
        /// </summary>
        public string TokenIAM { get; set; }
        /// <summary>
        /// Идентификатор каталога, к которому у вас есть доступ. Требуется для авторизации с пользовательским аккаунтом (см. ресурс UserAccount ). 
        /// Не используйте это поле, если вы делаете запрос от имени сервисного аккаунта.
        /// </summary>
        public string FolderID { get; set; }
        /// <summary>
        ///  API-ключ сервиснрнр аккаунта для выполнения запросов.
        ///  Сервис использует каталог, в котором был создан сервисный аккаунт.
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// Включает в запросы заголовки для включения логирования со стороны сервиса Yandex SpeechKit
        /// </summary>
        public bool LoggingEnabled { get; set; } = false;
        /// <summary>
        /// Максимальный таймаут выполнения HTTP запроса
        /// </summary>
        public TimeSpan HttpClientTimeout { get; set; } = TimeSpan.FromMinutes(1);

        public YandexTtsConfig()
        {
            EndPoint = DEFAULT_ENDPOINT;
        }

        /// <summary>
        /// Создаст экземпляр настроек с указанными параметрами
        /// </summary>
        /// <param name="apiKey">API-ключ сервиснрнр аккаунта</param>
        public YandexTtsConfig(string apiKey) 
            : this(apiKey, DEFAULT_ENDPOINT) { }

        /// <summary>
        /// Создаст экземпляр настроек с указанными параметрами
        /// </summary>
        /// <param name="apiKey">API-ключ сервиснрнр аккаунта</param>
        /// <param name="endpoint">Конечная точка для выполнения запросов</param>
        public YandexTtsConfig(string apiKey, Uri endpoint)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            ApiKey = apiKey;

            if (endpoint == null)
                throw new ArgumentNullException(nameof(endpoint));
            EndPoint = endpoint;
        }

        public YandexTtsConfig(string iam, string folderId)
            : this(iam, folderId, DEFAULT_ENDPOINT) { }


        public YandexTtsConfig(string iam, string folderId, Uri endpoint)
        {
            if (string.IsNullOrWhiteSpace(iam))
                throw new ArgumentNullException(nameof(iam));
            ApiKey = iam;

            if (string.IsNullOrWhiteSpace(folderId))
                throw new ArgumentNullException(nameof(folderId));
            FolderID = folderId;

            if (endpoint == null)
                throw new ArgumentNullException(nameof(endpoint));
            EndPoint = endpoint;
        }
    }
}
