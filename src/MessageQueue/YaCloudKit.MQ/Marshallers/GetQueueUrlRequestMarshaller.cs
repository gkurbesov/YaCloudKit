using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Marshallers
{
    public class GetQueueUrlRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<GetQueueUrlRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
            Marshall((GetQueueUrlRequest)input);

        public IRequestContext Marshall(GetQueueUrlRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueName", input.QueueName);

            return context;
        }
    }
}
