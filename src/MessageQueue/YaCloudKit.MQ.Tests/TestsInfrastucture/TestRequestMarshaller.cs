using YaCloudKit.MQ.Marshallers;
using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ.Tests;

public class TestRequestMarshaller : IMarshaller<BaseRequest>
{
    public IRequestContext Marshall(BaseRequest input)
    {
        IRequestContext context = new RequestContext();
        context.AddParametr("Action", input.ActionName);
        context.AddParametr("Version", YandexMqConfig.DEFAULT_SERVICE_VERSION);
        return context;
    }
}