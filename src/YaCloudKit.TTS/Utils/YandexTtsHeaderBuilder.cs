using System.Net.Http.Headers;

namespace YaCloudKit.TTS.Utils
{
    public class YandexTtsHeaderBuilder
    {
        public const string HEAD_AUTH = "Authorization";
        public const string HEAD_AUTH_BEARER = "Bearer";
        public const string HEAD_AUTH_APIKEY = "Api-Key";
        public const string HEAD_REQUEST_ID = "x-client-request-id";
        public const string HEAD_REQUEST_LOG = "x-data-logging-enabled";

        public static void AddLoggingHeaders(IRequestContext context, string requestId)
        {
            if (!string.IsNullOrWhiteSpace(requestId))
            {
                context.AddHeader(HEAD_REQUEST_ID, requestId);
                context.AddHeader(HEAD_REQUEST_LOG, "true");
            }
        }

        public static void AddMainHeaders(IRequestContext context, YandexTtsConfig config)
        {
            if (!string.IsNullOrWhiteSpace(config.TokenIAM) && !string.IsNullOrWhiteSpace(config.FolderID))
                context.AddHeader(HEAD_AUTH, HEAD_AUTH_BEARER + " " + config.TokenIAM);
            else if (!string.IsNullOrWhiteSpace(config.ApiKey))
                context.AddHeader(HEAD_AUTH, HEAD_AUTH_APIKEY + " " + config.ApiKey);
            else
                throw new YandexTtsServiceException("Параметры авторизации не заданы");
        }

        /// <summary>
        /// Добавляет недостающие заголовки из контекста в класс HttpHeaders запроса или http-контента
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="values"></param>
        public static void AddHttpHeaders(IRequestContext context, HttpHeaders headers)
        {
            foreach (var headItem in context.Headers)
                headers.TryAddWithoutValidation(headItem.Key, headItem.Value);
        }
    }
}
