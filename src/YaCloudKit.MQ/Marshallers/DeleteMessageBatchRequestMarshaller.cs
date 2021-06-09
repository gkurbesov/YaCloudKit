using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.Core;

namespace YaCloudKit.MQ.Marshallers
{
    public class DeleteMessageBatchRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<DeleteMessageBatchRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
             Marshall((DeleteMessageBatchRequest)input);

        public IRequestContext Marshall(DeleteMessageBatchRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);

            if (input.IsSetBatchEntry())
            {
                var number = 1;
                foreach (var item in input.DeleteMessageBatchRequestEntry)
                {
                    if (!string.IsNullOrWhiteSpace(item.Id) && !string.IsNullOrWhiteSpace(item.ReceiptHandle))
                    {
                        context.AddParametr($"DeleteMessageBatchRequestEntry.{number}.Id", item.Id);
                        context.AddParametr($"DeleteMessageBatchRequestEntry.{number}.ReceiptHandle", item.ReceiptHandle);
                        number++;
                    }
                }
            }

            return context;
        }

    }
}
