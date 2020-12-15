using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    public class SendMessageBatchResponseUnmarshaller : ResponseUnmarshaller
    {
        public override T Unmarshall<T>(IResponseContext context)
        {
            try
            {
                var response = new SendMessageBatchResponse();
                ResultUnmarshall(context, response);

                var xmlRootNode = GetXmlElement(context.ContentStream);

                if (TryGetXmlElements(xmlRootNode, "SendMessageBatchResult/BatchResultErrorEntry", out var batchErrorList))
                    BatchResultErrorEntryUnmarshaller(batchErrorList, response.BatchResultErrorEntry);

                if (TryGetXmlElements(xmlRootNode, "SendMessageBatchResult/SendMessageBatchResultEntry", out var batchResultList))
                {
                    foreach (XmlNode item in batchResultList)
                    {
                        var resultEntry = new SendMessageBatchResultEntry();
                        resultEntry.Id = item.SelectSingleNode("Id")?.InnerText;
                        resultEntry.MD5OfMessageAttributes = item.SelectSingleNode("MD5OfMessageAttributes")?.InnerText;
                        resultEntry.MD5OfMessageBody = item.SelectSingleNode("MD5OfMessageBody")?.InnerText;
                        resultEntry.MessageId = item.SelectSingleNode("MessageId")?.InnerText;
                        resultEntry.SequenceNumber = item.SelectSingleNode("SequenceNumber")?.InnerText;
                        response.SendMessageBatchResultEntry.Add(resultEntry);
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
