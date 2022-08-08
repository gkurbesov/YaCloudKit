using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace YaCloudKit.TTS
{
    public class YandexTtsClient : YandexTtsService, IYandexTts
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iam">IAM-токен для авторизации</param>
        /// <param name="folderId">ID каталога ресурса</param>
        public YandexTtsClient(string iam, string folderId)
            : this(new YandexTtsConfig(iam, folderId))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey">API-ключ сервиснрнр аккаунта</param>
        public YandexTtsClient(string apiKey)
            : this(new YandexTtsConfig(apiKey))
        {
        }


        public YandexTtsClient(YandexTtsConfig config)
            : base(config, () =>
            {
                var client = new HttpClient();
#if !NETCOREAPP
                ServicePointManager.DefaultConnectionLimit = 5;
                var servicePoint = ServicePointManager.FindServicePoint(config.EndPoint);
                if (servicePoint != null)
                    servicePoint.ConnectionLeaseTimeout = 60000;
#endif
                client = new HttpClient();
                client.Timeout = config.HttpClientTimeout;
                return client;
            })
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">Настройки для выполнения запросов к api Yandex SpeechKit</param>
        public YandexTtsClient(YandexTtsConfig config, Func<HttpClient> httpClientFactory)
            : base(config, httpClientFactory)
        {
        }

        public async Task<YandexTtsResponse> TextToSpeechAsync(string text, VoiceParameters voice,
            FormatParameters format, CancellationToken cancellationToken = default) =>
            await InvokeAsync(text, false, voice, format, cancellationToken);

        public async Task<YandexTtsResponse> MarkupToSpeechAsync(string text, VoiceParameters voice,
            FormatParameters format, CancellationToken cancellationToken = default) =>
            await InvokeAsync(text, true, voice, format, cancellationToken);

        public async Task<YandexTtsResponse> InvokeAsync(string text, bool ssml, VoiceParameters voice,
            FormatParameters format, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));
            if (voice == null)
                throw new ArgumentNullException(nameof(voice));
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            var options = new InvokeOptions()
            {
                AudioFormat = format,
                Voice = voice,
                SSML = ssml,
                Text = text
            };

            return await InvokeAsync(options, cancellationToken);
        }
    }
}