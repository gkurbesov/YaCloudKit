using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Marshallers
{
    public class DeleteQueueRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<DeleteQueueRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
             Marshall((DeleteQueueRequest)input);

        public IRequestContext Marshall(DeleteQueueRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);

            return context;
        }

    }
}
