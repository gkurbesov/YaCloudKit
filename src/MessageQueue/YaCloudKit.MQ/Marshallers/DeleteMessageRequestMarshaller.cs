using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Marshallers
{
    public class DeleteMessageRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<DeleteMessageRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
            Marshall((DeleteMessageRequest)input);

        public IRequestContext Marshall(DeleteMessageRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);
            context.AddParametr("ReceiptHandle", input.ReceiptHandle);

            return context;
        }
    }
}
