using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace YaCloudKit.Monitoring
{
    public class YandexMonitoringServiceException : Exception
    {
        /// <summary>
        /// Статус-код результата выполнения HTTP запроса
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        public YandexMonitoringServiceException() { }
        public YandexMonitoringServiceException(string message)
            : base(message) { }

        public YandexMonitoringServiceException(string message, Exception innerException)
            : base(message, innerException) { }

        public YandexMonitoringServiceException(string message, Exception innerException, HttpStatusCode statusCode)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
