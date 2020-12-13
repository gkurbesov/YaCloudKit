using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    public class SendMessageResponseUnmarshaller : ResponseUnmarshaller
    {
        public override T Unmarshall<T>(IResponseContext context)
        {
            try
            {
                var response = new SendMessageResponse();
                ResultUnmarshall(context, response);

                var xmlRootNode = GetXmlElement(context.ContentStream);

                response.MD5OfMessageAttributes = xmlRootNode.SelectSingleNode("SendMessageResult/MD5OfMessageAttributes")?.InnerText;
                response.MD5OfMessageBody = xmlRootNode.SelectSingleNode("SendMessageResult/MD5OfMessageBody")?.InnerText;
                response.MessageId = xmlRootNode.SelectSingleNode("SendMessageResult/MessageId")?.InnerText;
                response.SequenceNumber = xmlRootNode.SelectSingleNode("SendMessageResult/SequenceNumber")?.InnerText;

                response.ResponseMetadata.RequestId = xmlRootNode.SelectSingleNode("ResponseMetadata/RequestId")?.InnerText;

                return response as T;
            }
            catch (Exception ex)
            {
                throw ErrorUnmarshall(ex.Message, ex, context.StatusCode);
            }
        }
    }
}
