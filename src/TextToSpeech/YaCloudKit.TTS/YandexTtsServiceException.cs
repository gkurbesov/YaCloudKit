﻿using System;
using System.Net;

namespace YaCloudKit.TTS
{
    public class YandexTtsServiceException : Exception
    {
        /// <summary>
        /// UUID запроса, только если включены запросы с отладкой (см. Использование API -> Диагностика ошибок).
        /// </summary>
        public string RequestId { get; }
        /// <summary>
        /// Статус-код результата выполнения HTTP запроса
        /// </summary>
        public HttpStatusCode StatusCode { get; }

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
