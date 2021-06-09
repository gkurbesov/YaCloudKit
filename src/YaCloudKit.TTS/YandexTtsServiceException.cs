using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace YaCloudKit.TTS
{
    public class YandexTtsServiceException : Exception
    {
        /// <summary>
        /// UUID запроса, только если включены запросы с отладкой (см. Использование API -> Диагностика ошибок).
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// Статус-код результата выполнения HTTP запроса
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        public YandexTtsServiceException() { }
        public YandexTtsServiceException(string message)
            : base(message) { }

        public YandexTtsServiceException(string message, Exception innerException)
            : base(message, innerException) { }

        public YandexTtsServiceException(string message, Exception innerException, HttpStatusCode statusCode)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public YandexTtsServiceException(string message, string requestId, HttpStatusCode statusCode)
            : base(message)
        {
            RequestId = requestId;
            StatusCode = statusCode;
        }

        public YandexTtsServiceException(string message, Exception innerException, string requestId, HttpStatusCode statusCode)
            : base(message, innerException)
        {
            RequestId = requestId;
            StatusCode = statusCode;
        }
    }
}
