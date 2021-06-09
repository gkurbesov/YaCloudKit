using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Utils;
using YaCloudKit.Core;

namespace YaCloudKit.MQ.Marshallers
{
    public class SetQueueAttributesRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<SetQueueAttributesRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
              Marshall((SetQueueAttributesRequest)input);

        public IRequestContext Marshall(SetQueueAttributesRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);

            if (input.Attributes != null && input.Attributes.Count > 0)
                RequestAttributesBuilder.NamedAttributes(context, input.Attributes);

            return context;
        }
    }
}
