using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ.Marshallers
{
    public class GetQueueAttributesRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<GetQueueAttributesRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
              Marshall((GetQueueAttributesRequest)input);

        public IRequestContext Marshall(GetQueueAttributesRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);

            if (input.AttributeNames != null && input.AttributeNames.Count > 0)
                RequestAttributesBuilder.ListAttributes(context, input.AttributeNames);

            return context;
        }
    }
}
