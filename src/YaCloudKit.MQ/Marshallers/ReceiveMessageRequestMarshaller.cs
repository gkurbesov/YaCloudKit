using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Utils;
using YaCloudKit.Core;

namespace YaCloudKit.MQ.Marshallers
{
    public class ReceiveMessageRequestMarshaller : IMarshaller<BaseRequest>, IMarshaller<ReceiveMessageRequest>
    {
        public IRequestContext Marshall(BaseRequest input) =>
            Marshall((ReceiveMessageRequest)input);

        public IRequestContext Marshall(ReceiveMessageRequest input)
        {
            IRequestContext context = new RequestContext();
            context.AddParametr("Action", input.ActionName);
            context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);

            context.AddParametr("QueueUrl", input.QueueUrl);

            if (input.MaxNumberOfMessages.HasValue)
                context.AddParametr("MaxNumberOfMessages", input.MaxNumberOfMessages.ToString());
            if (!string.IsNullOrWhiteSpace(input.ReceiveRequestAttemptId))
                context.AddParametr("ReceiveRequestAttemptId", input.ReceiveRequestAttemptId);
            if (input.VisibilityTimeout.HasValue)
                context.AddParametr("VisibilityTimeout", input.VisibilityTimeout.ToString());
            if (input.WaitTimeSeconds.HasValue)
                context.AddParametr("WaitTimeSeconds", input.WaitTimeSeconds.ToString());

            if (input.IsSetAttributeNames())
                RequestAttributesBuilder.ListAttributes(context, input.AttributeNames);
            if (input.IsSetMessageAttributeName())
                RequestAttributesBuilder.ListMessageAttributes(context, input.MessageAttributeName);

            return context;
        }
    }
}
