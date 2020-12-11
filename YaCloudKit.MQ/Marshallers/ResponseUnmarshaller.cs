using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    public class ResponseUnmarshaller : IUnmarshaller
    {
        public static XmlElement GetXmlElement(Stream stream)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            return doc.DocumentElement;
        }

        public static void ResultUnmarshall(IResponseContext context, YandexMessageQueueResponse response)
        {
            response.ContentLength = context.ContentStream.Length;
            response.HttpStatusCode = context.StatusCode;
        }

        public static YandexMqServiceException ErrorUnmarshall(IResponseContext context)
        {
            try
            {
                var rootNode = GetXmlElement(context.ContentStream);
                var errorNode = rootNode.SelectSingleNode("Error");
                var requestIdNode = rootNode.SelectSingleNode("RequestId");

                var errorType = errorNode.SelectSingleNode("Type")?.InnerText ?? string.Empty;
                var errorCode = errorNode.SelectSingleNode("Code")?.InnerText ?? string.Empty;
                var errorMessage = errorNode.SelectSingleNode("Message")?.InnerText ?? string.Empty;
                var requestId = requestIdNode.InnerText;

                return new YandexMqServiceException(errorMessage, errorType, errorCode, requestId, context.StatusCode);
            }
            catch (Exception ex)
            {
                var message = new StreamReader(context.ContentStream).ReadToEnd(); 
                YandexMqServiceException exception = new YandexMqServiceException(message, ex)
                {
                    StatusCode = context.StatusCode
                };
                return exception;
            }
        }

        public static YandexMqServiceException ErrorUnmarshall(string message, Exception innerException, HttpStatusCode httpStatusCode)
        {
            YandexMqServiceException exception = new YandexMqServiceException(message, innerException)
            {
                StatusCode = httpStatusCode
            };
            return exception;
        }

        public virtual T Unmarshall<T>(IResponseContext context) where T : YandexMessageQueueResponse, new()
        {
            throw new NotImplementedException();
        }

        public YandexMqServiceException UnmarshallException(IResponseContext context)
        {
            try
            {
                return ErrorUnmarshall(context);
            }
            catch (Exception ex)
            {
                return ErrorUnmarshall(ex.Message, ex, context.StatusCode);
            }
        }
    }
}
