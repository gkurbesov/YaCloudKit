using System;
using System.Xml;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    public class ReceiveMessageResponseUnmarshaller : ResponseUnmarshaller
    {
        public override T Unmarshall<T>(IResponseContext context)
        {
            try
            {
                var response = new ReceiveMessageResponse();
                ResultUnmarshall(context, response);

                var xmlRootNode = GetXmlElement(context.ContentStream);

                if (TryGetXmlElements(xmlRootNode, "ReceiveMessageResult/Message", out var messagesNodes))
                {
                    foreach (XmlNode node in messagesNodes)
                    {
                        var message = new Message()
                        {
                            Body = node.SelectSingleNode("Body")?.InnerText,
                            MD5OfBody = node.SelectSingleNode("MD5OfBody")?.InnerText,
                            MD5OfMessageAttributes = node.SelectSingleNode("MD5OfMessageAttributes")?.InnerText,
                            MessageId = node.SelectSingleNode("MessageId")?.InnerText,
                            ReceiptHandle = node.SelectSingleNode("ReceiptHandle")?.InnerText,
                        };

                        if (TryGetXmlElements(node, "Attribute", out var attributeList))
                            AttributeUnmarshall(attributeList, message.Attribute);

                        if (TryGetXmlElements(node, "MessageAttribute", out var messageAttributeList))
                            MessageAttributeUnmarshall(messageAttributeList, message.MessageAttribute);

                        response.Messages.Add(message);
                    }
                }

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
