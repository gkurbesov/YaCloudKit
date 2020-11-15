using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace YaCloudKit.MQ
{
    public class YandexMqServiceException : Exception
    {
        /// <summary>
        /// Тип ошибки, указывающий, на чьей стороне произошла ошибка: отправителя или получателя.
        /// </summary>
        public string ErrorType { get; set; }
        /// <summary>
        /// Идентификатор ошибки.
        /// Перечень стандартных ошибок смотрите в разделе "Стандартные ошибки" документации к API Yandex Message Queue.
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// UUID запроса.
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// Статус-код результата выполнения HTTP запроса
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        public YandexMqServiceException() { }
        public YandexMqServiceException(string message)
            : base(message) { }

        public YandexMqServiceException(string message, Exception innerException)
            : base(message, innerException) { }

        public YandexMqServiceException(string message, Exception innerException, HttpStatusCode statusCode)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public YandexMqServiceException(string message, string errorType, string errorCode, string requestId, HttpStatusCode statusCode)
            :base(message)
        {
            ErrorType = errorType;
            ErrorCode = errorCode;
            RequestId = requestId;
            StatusCode = statusCode;
        }

        public YandexMqServiceException(string message, Exception innerException, string errorType, string errorCode, string requestId, HttpStatusCode statusCode)
            : base(message, innerException)
        {
            ErrorType = errorType;
            ErrorCode = errorCode;
            RequestId = requestId;
            StatusCode = statusCode;
        }
    }
}
