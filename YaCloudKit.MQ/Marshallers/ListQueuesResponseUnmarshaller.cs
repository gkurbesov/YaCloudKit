using System;
using System.Collections.Generic;
using YaCloudKit.Core;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    public class ListQueuesResponseUnmarshaller : ResponseUnmarshaller
    {
        public override T Unmarshall<T>(IResponseContext context)
        {
            try
            {
                var response = new ListQueuesResponse();
                ResultUnmarshall(context, response);

                var xmlRootNode = GetXmlElement(context.ContentStream);
                var queueUrlNodes = xmlRootNode.SelectNodes("ListQueuesResult/QueueUrl");
                if (queueUrlNodes != null && queueUrlNodes.Count > 0)
                {
                    response.QueueUrls = new List<string>();
                    for (var i = 0; i < queueUrlNodes.Count; i++)
                        response.QueueUrls.Add(queueUrlNodes[i].InnerText);
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
