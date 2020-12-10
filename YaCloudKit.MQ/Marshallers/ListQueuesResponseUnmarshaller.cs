using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    public class ListQueuesResponseUnmarshaller : IUnmarshaller
    {
        public T Unmarshall<T>(IResponseContext context) where T : YandexMessageQueueResponse, new()
        {
            try
            {
                var response = new ListQueuesResponse();
                response.ContentLength = context.ContentStream.Length;
                response.HttpStatusCode = context.StatusCode;

                var xmlRootNode = ResponseUnmarshaller.GetXmlElement(context.ContentStream);
                var queueUrlNodes = xmlRootNode.SelectNodes("ListQueuesResult/QueueUrl");

                if (queueUrlNodes != null && queueUrlNodes.Count > 0)
                {
                    response.QueueUrls = new List<string>();
                    for (var i = 0; i < queueUrlNodes.Count; i++)
                    {
                        response.QueueUrls.Add(queueUrlNodes[i].InnerText);
                    }
                }

                response.ResponseMetadata = new ResponseMetadata()
                {
                    RequestId = xmlRootNode.SelectSingleNode("ResponseMetadata/RequestId")?.InnerText
                };


                return response as T;
            }
            catch (Exception ex)
            {
                throw ResponseUnmarshaller.ErrorUnmarshall(ex.Message, ex, context.StatusCode);
            }
        }

        public YandexMqServiceException UnmarshallException(IResponseContext context)
        {
            try
            {
                return ResponseUnmarshaller.ErrorUnmarshall(context);
            }
            catch (Exception ex)
            {
                return ResponseUnmarshaller.ErrorUnmarshall(ex.Message, ex, context.StatusCode);
            }
        }
    }
}
