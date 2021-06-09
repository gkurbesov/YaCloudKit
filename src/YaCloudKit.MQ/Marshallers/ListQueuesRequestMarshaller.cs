using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.Core;

namespace YaCloudKit.MQ.Marshallers
{
    public class ListQueuesRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<ListQueuesRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
            Marshall((ListQueuesRequest)input);

        public IRequestContext Marshall(ListQueuesRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            if (!string.IsNullOrWhiteSpace(input.QueueNamePrefix))
                context.AddParametr("QueueNamePrefix", input.QueueNamePrefix);

            return context;
        }
    }
}
