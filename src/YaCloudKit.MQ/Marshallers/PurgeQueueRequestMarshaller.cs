using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Marshallers
{
    public class PurgeQueueRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<PurgeQueueRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
            Marshall((PurgeQueueRequest)input);

        public IRequestContext Marshall(PurgeQueueRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);

            return context;
        }
    }
}
