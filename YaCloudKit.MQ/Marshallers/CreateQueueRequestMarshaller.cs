using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ.Marshallers
{
    public class CreateQueueRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<CreateQueueRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
            Marshall((CreateQueueRequest)input);

        public IRequestContext Marshall(CreateQueueRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueName", input.QueueName);

            if (input.Attributes != null && input.Attributes.Count > 0)
                RequestAttributesBuilder.NamedAttributes(context, input.Attributes);

            return context;
        }
    }
}
