using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using YaCloudKit.MQ.Model.Responses;

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

                var paramNodes = xmlRootNode.SelectNodes("GetQueueAttributesResult/Attribute");

                if(paramNodes != null && paramNodes.Count > 0)
                {
                    foreach(XmlNode node in paramNodes)
                    {
                        var name = node.SelectSingleNode("Name")?.InnerText;
                        var value = node.SelectSingleNode("Value")?.InnerText;
                        if(!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
                            response.Attributes.Add(name, value);
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
