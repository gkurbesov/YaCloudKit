using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    public class ChangeMessageVisibilityBatchResponseUnmarshaller : ResponseUnmarshaller
    {
        public override T Unmarshall<T>(IResponseContext context)
        {
            try
            {
                var response = new ChangeMessageVisibilityBatchResponse();
                ResultUnmarshall(context, response);

                var xmlRootNode = GetXmlElement(context.ContentStream);

                if (TryGetXmlElements(xmlRootNode, "ChangeMessageVisibilityBatchResult/BatchResultErrorEntry", out var batchErrorList))
                    BatchResultErrorEntryUnmarshaller(batchErrorList, response.BatchResultErrorEntry);

                if (TryGetXmlElements(xmlRootNode, "ChangeMessageVisibilityBatchResult/ChangeMessageVisibilityBatchResultEntry", out var batchResultList))
                    ChangeMessageVisibilityBatchResultEntryUnmarshaller(batchResultList, response.ChangeMessageVisibilityBatchResultEntry);

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
