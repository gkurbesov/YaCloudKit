using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Marshallers
{
    public class ChangeMessageVisibilityBatchRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<ChangeMessageVisibilityBatchRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
             Marshall((ChangeMessageVisibilityBatchRequest)input);

        public IRequestContext Marshall(ChangeMessageVisibilityBatchRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);

            if (input.IsSetBatchEntry())
            {
                var number = 1;
                foreach (var item in input.ChangeMessageVisibilityBatchRequestEntry)
                {
                    if (!string.IsNullOrWhiteSpace(item.Id) && !string.IsNullOrWhiteSpace(item.ReceiptHandle))
                    {
                        context.AddParametr($"ChangeMessageVisibilityBatchRequestEntry.{number}.Id", item.Id);
                        context.AddParametr($"ChangeMessageVisibilityBatchRequestEntry.{number}.ReceiptHandle", item.ReceiptHandle);
                        if (item.VisibilityTimeout.HasValue)
                            context.AddParametr($"ChangeMessageVisibilityBatchRequestEntry.{number}.VisibilityTimeout", item.VisibilityTimeout.ToString());
                        number++;
                    }
                }
            }

            return context;
        }

    }
}
