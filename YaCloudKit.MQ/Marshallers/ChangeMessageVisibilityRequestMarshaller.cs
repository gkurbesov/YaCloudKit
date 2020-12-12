using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Marshallers
{
    public class ChangeMessageVisibilityRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<ChangeMessageVisibilityRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
            Marshall((ChangeMessageVisibilityRequest)input);

        public IRequestContext Marshall(ChangeMessageVisibilityRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);
            context.AddParametr("ReceiptHandle", input.ReceiptHandle);
            context.AddParametr("VisibilityTimeout", input.VisibilityTimeout.ToString());

            return context;
        }
    }
}
