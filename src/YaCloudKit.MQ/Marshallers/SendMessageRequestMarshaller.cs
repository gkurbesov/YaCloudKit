using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Utils;
using YaCloudKit.Core;

namespace YaCloudKit.MQ.Marshallers
{
    public class SendMessageRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<SendMessageRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
            Marshall((SendMessageRequest)input);

        public IRequestContext Marshall(SendMessageRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);
            context.AddParametr("MessageBody", input.MessageBody);

            if (input.MessageAttribute != null && input.MessageAttribute.Count > 0)
                RequestAttributesBuilder.MessageAttributes(context, input.MessageAttribute);

            if (input.DelaySeconds.HasValue)
                context.AddParametr("DelaySeconds", input.DelaySeconds.ToString());

            if (!string.IsNullOrWhiteSpace(input.MessageDeduplicationId))
                context.AddParametr("MessageDeduplicationId", input.MessageDeduplicationId);
            if (!string.IsNullOrWhiteSpace(input.MessageDeduplicationId))
                context.AddParametr("MessageGroupId", input.MessageGroupId);

            return context;
        }
    }
}
