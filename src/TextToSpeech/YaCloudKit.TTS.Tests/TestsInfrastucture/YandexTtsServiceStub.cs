namespace YaCloudKit.TTS.Tests.TestsInfrastucture;

public class YandexTtsServiceStub : YandexTtsService
{
    public YandexTtsServiceStub(YandexTtsConfig config, Func<HttpClient> httpClientFactory)
        : base(config, httpClientFactory)
    {
    }

    public new Task<YandexTtsResponse> InvokeAsync(InvokeOptions options, CancellationToken cancellationToken = default)
    {
        return base.InvokeAsync(options, cancellationToken);
    }
}