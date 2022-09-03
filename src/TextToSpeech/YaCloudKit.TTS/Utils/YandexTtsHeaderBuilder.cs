using System.Net.Http.Headers;

namespace YaCloudKit.TTS
{
    public class YandexTtsHeaderBuilder
    {
        public const string HeadAuth = "Authorization";
        public const string HeadAuthBearer = "Bearer";
        public const string HeadAuthApikey = "Api-Key";
        public const string HeadRequestId = "x-client-request-id";
        public const string HeadRequestLog = "x-data-logging-enabled";

        public static void AddLoggingHeaders(IRequestContext context, string requestId)
        {
            if (!string.IsNullOrWhiteSpace(requestId))
            {
                context.AddHeader(HeadRequestId, requestId);
                context.AddHeader(HeadRequestLog, "true");
            }
        }

        public static void AddMainHeaders(IRequestContext context, YandexTtsConfig config)
        {
            if (!string.IsNullOrWhiteSpace(config.TokenIAM) && !string.IsNullOrWhiteSpace(config.FolderID))
                context.AddHeader(HeadAuth, HeadAuthBearer + " " + config.TokenIAM);
            else if (!string.IsNullOrWhiteSpace(config.ApiKey))
                context.AddHeader(HeadAuth, HeadAuthApikey + " " + config.ApiKey);
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