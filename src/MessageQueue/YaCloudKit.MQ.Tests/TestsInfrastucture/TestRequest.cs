using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Tests;

public class TestRequest : BaseRequest
{
    public TestRequest() : base("TestActionName")
    {
    }

    public TestRequest(string actionName) : base(actionName)
    {
    }
}