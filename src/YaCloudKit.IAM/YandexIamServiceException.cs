using System;
using System.Net;

namespace YaCloudKit.IAM
{
    public class YandexIamServiceException : Exception
    {
        /// <summary>
        /// Статус-код результата выполнения HTTP запроса
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        public YandexIamServiceException() { }
        public YandexIamServiceException(string message)
            : base(message) { }

        public YandexIamServiceException(string message, Exception innerException)
            : base(message, innerException) { }

        public YandexIamServiceException(string message, Exception innerException, HttpStatusCode statusCode)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public YandexIamServiceException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
