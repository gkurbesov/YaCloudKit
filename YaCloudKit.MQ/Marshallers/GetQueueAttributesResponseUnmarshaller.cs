using System;
using YaCloudKit.MQ.Model.Responses;
using YaCloudKit.Core;

namespace YaCloudKit.MQ.Marshallers
{
    public class GetQueueAttributesResponseUnmarshaller : ResponseUnmarshaller
    {
        public override T Unmarshall<T>(IResponseContext context)
        {
            try
            {
                var response = new GetQueueAttributesResponse();
                ResultUnmarshall(context, response);

                var xmlRootNode = GetXmlElement(context.ContentStream);

                if (TryGetXmlElements(xmlRootNode, "GetQueueAttributesResult/Attribute", out var paramNodes))
                    AttributeUnmarshall(paramNodes, response.Attributes);

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
