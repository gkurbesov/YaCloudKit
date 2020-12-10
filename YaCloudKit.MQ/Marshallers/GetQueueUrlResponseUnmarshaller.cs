using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    public class GetQueueUrlResponseUnmarshaller : IUnmarshaller
    {
        public T Unmarshall<T>(IResponseContext context) where T : YandexMessageQueueResponse, new()
        {
            try
            {
                var response = new GetQueueUrlResponse();
                response.ContentLength = context.ContentStream.Length;
                response.HttpStatusCode = context.StatusCode;

                var xmlRootNode = ResponseUnmarshaller.GetXmlElement(context.ContentStream);

                response.QueueUrl = xmlRootNode.SelectSingleNode("GetQueueUrlResult/QueueUrl")?.InnerText;
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
