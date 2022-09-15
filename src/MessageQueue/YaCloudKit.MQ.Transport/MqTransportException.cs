using System;

namespace YaCloudKit.MQ.Transport
{
    public class MqTransportException : Exception
    {
        public MqTransportException() { }
        public MqTransportException(string message)
            : base(message) { }

        public MqTransportException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
