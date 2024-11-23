using System;
using System.Net;

namespace YaCloudKit.Postbox;

public class YandexPostboxServiceException : Exception
{
    public string? Code { get; private set; }
    public HttpStatusCode? StatusCode { get; private set; }

    public YandexPostboxServiceException()
    {
    }

    public YandexPostboxServiceException(string message)
        : base(message)
    {
    }

    public YandexPostboxServiceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public YandexPostboxServiceException(string message, Exception innerException, HttpStatusCode statusCode)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    public YandexPostboxServiceException(string? code, string message, HttpStatusCode statusCode)
        : base(message)
    {
        Code = code;
        StatusCode = statusCode;
    }
}