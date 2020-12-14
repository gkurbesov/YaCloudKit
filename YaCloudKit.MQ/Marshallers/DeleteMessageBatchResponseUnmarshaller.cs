using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    public class DeleteMessageBatchResponseUnmarshaller : ResponseUnmarshaller
    {
        public override T Unmarshall<T>(IResponseContext context)
        {
            try
            {
                var response = new DeleteMessageBatchResponse();
                ResultUnmarshall(context, response);

                var xmlRootNode = GetXmlElement(context.ContentStream);

                if (TryGetXmlElements(xmlRootNode, "DeleteMessageBatchResult/BatchResultErrorEntry", out var batchErrorList))
                    BatchResultErrorEntryUnmarshaller(batchErrorList, response.BatchResultErrorEntry);

                if (TryGetXmlElements(xmlRootNode, "DeleteMessageBatchResult/DeleteMessageBatchResultEntry", out var batchResultList))
                    DeleteMessageBatchResultEntryUnmarshaller(batchResultList, response.DeleteMessageBatchResultEntry);

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
