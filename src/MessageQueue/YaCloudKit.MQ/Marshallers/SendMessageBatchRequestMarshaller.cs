using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ.Marshallers
{
    public class SendMessageBatchRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<SendMessageBatchRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
            Marshall((SendMessageBatchRequest)input);

        public IRequestContext Marshall(SendMessageBatchRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);

            if (input.IsSetBatchEntry())
            {
                var number = 1;
                foreach (var item in input.SendMessageBatchRequestEntry)
                {

                    context.AddParametr($"SendMessageBatchRequestEntry.{number}.Id", item.Id);
                    context.AddParametr($"SendMessageBatchRequestEntry.{number}.MessageBody", item.MessageBody);

                    if (item.IsSetMessageAttribute())
                        RequestAttributesBuilder.MessageAttributesBatchEntry(context, item.MessageAttribute, number);

                    if (item.DelaySeconds.HasValue)
                        context.AddParametr($"SendMessageBatchRequestEntry.{number}.DelaySeconds", item.DelaySeconds.ToString());

                    if (!string.IsNullOrWhiteSpace(item.MessageDeduplicationId))
                        context.AddParametr($"SendMessageBatchRequestEntry.{number}.MessageDeduplicationId", item.MessageDeduplicationId);
                    if (!string.IsNullOrWhiteSpace(item.MessageDeduplicationId))
                        context.AddParametr($"SendMessageBatchRequestEntry.{number}.MessageGroupId", item.MessageGroupId);

                    number++;
                }
            }
            return context;
        }
    }
}
