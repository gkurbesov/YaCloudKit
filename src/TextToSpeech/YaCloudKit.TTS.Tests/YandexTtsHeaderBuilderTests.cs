namespace YaCloudKit.TTS.Tests;

public class YandexTtsHeaderBuilderTests
{
    [Fact]
    public void AddLoggingHeaders_HeadersHasParameters()
    {
        var context = new RequestContext();
        var id = Guid.NewGuid().ToString();
        
        YandexTtsHeaderBuilder.AddLoggingHeaders(context, id);

        Assert.True(context.Headers.TryGetValue(YandexTtsHeaderBuilder.HeadRequestId, out var requestId));
        Assert.Equal(id, requestId);
        Assert.True(context.Headers.TryGetValue(YandexTtsHeaderBuilder.HeadRequestLog, out var requestLogState));
        Assert.Equal("true", requestLogState);
    }

    [Fact]
    public void AddAuthorizationHeaders_HeadersHasBearerAuthorization()
    {
        var context = new RequestContext();
        var config = new YandexTtsConfig("iam_token", "folder_id");
        
        YandexTtsHeaderBuilder.AddAuthorizationHeaders(context, config);

        Assert.True(context.Headers.TryGetValue("Authorization", out var value));
        Assert.Equal("Bearer iam_token", value);
    }
    
    [Fact]
    public void AddAuthorizationHeaders_HeadersHasApiKeyAuthorization()
    {
        var context = new RequestContext();
        var config = new YandexTtsConfig("api_key");
        
        YandexTtsHeaderBuilder.AddAuthorizationHeaders(context, config);

        Assert.True(context.Headers.TryGetValue("Authorization", out var value));
        Assert.Equal("Api-Key api_key", value);
    }
}