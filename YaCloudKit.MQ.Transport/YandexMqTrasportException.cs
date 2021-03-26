using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public class YandexMqTrasportException : Exception
    {
        public YandexMqTrasportException() { }
        public YandexMqTrasportException(string message)
            : base(message) { }

        public YandexMqTrasportException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
