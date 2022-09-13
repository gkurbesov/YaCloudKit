using System.Net;
using Moq;
using Moq.Protected;
using YaCloudKit.TTS.Tests.TestsInfrastucture;

namespace YaCloudKit.TTS.Tests;

public class YandexTtsServiceTests
{
    private static readonly InvokeOptions TestInvokeOptions = new InvokeOptions()
    {
        AudioFormat = AudioFormat.Ogg,
        Voice = VoiceParameters.Alena,
        SSML = false,
        Text = "text"
    };

    [Fact]
    public void YandexTtsConfigIsNull_ThrowException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new YandexTtsServiceStub(null, null));
    }

    [Fact]
    public void HttpClientFactoryIsNull_ThrowException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new YandexTtsServiceStub(new YandexTtsConfig(), null));
    }

    [Fact]
    public async Task InvokeOptionsIsNull_ThrowException()
    {
        var service = CreateServiceStub();
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.InvokeAsync(null));
    }

    [Fact]
    public async Task ServiceDisposed_ThrowException()
    {
        var service = CreateServiceStub();
        service.Dispose();
        await Assert.ThrowsAsync<ObjectDisposedException>(() => service.InvokeAsync(new InvokeOptions()));
    }

    [Fact]
    public async Task CancellationRequested_ThrowException()
    {
        var service = CreateServiceStub();
        using var tokenSource = new CancellationTokenSource();
        tokenSource.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            service.InvokeAsync(new InvokeOptions(), tokenSource.Token));
    }

    [Fact]
    public async Task ResponseSuccessful_ReturnResult()
    {
        var httpClientMock = CreateHttpClientMock(new ByteArrayContent(new byte[] {0x01, 0x02}), HttpStatusCode.OK);
        var service = CreateServiceStub(
            config: new YandexTtsConfig("api_key")
            {
                LoggingEnabled = true
            },
            httpClientFactory: () => httpClientMock.Object);

        var result = await service.InvokeAsync(TestInvokeOptions);

        httpClientMock.Verify();

        Assert.NotNull(result);
        Assert.NotNull(result.RequestId);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(new byte[] {0x01, 0x02}, result.Content);
    }

    [Fact]
    public async Task ResponseUnsuccessful_ThrowException()
    {
        var httpContent = new StringContent("Error message");
        var httpResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = httpContent
        };

        var httpClientMock = new Mock<HttpClient>(MockBehavior.Strict);
        httpClientMock.Setup(c => c.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(httpResponse);

        var invokeOptions = new InvokeOptions()
        {
            AudioFormat = AudioFormat.Ogg,
            Voice = VoiceParameters.Alena,
            SSML = false,
            Text = "text"
        };

        var service = CreateServiceStub(
            config: new YandexTtsConfig("api_key")
            {
                LoggingEnabled = true
            },
            httpClientFactory: () => httpClientMock.Object);

        var exception = await Assert.ThrowsAsync<YandexTtsServiceException>(() => service.InvokeAsync(invokeOptions));

        httpClientMock.Verify();

        Assert.Equal("Error message", exception.Message);
        Assert.NotNull(exception.RequestId);
        Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    private static Mock<HttpClient> CreateHttpClientMock(HttpContent httpContent, HttpStatusCode statusCode)
    {
        var httpResponse = new HttpResponseMessage(statusCode)
        {
            Content = httpContent
        };

        var httpClientMock = new Mock<HttpClient>(MockBehavior.Strict);
        httpClientMock.Setup(client => client.SendAsync(
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(httpResponse);

        return httpClientMock;
    }

    private static YandexTtsServiceStub CreateServiceStub() =>
        CreateServiceStub(new YandexTtsConfig(), () => new HttpClient());

    private static YandexTtsServiceStub CreateServiceStub(YandexTtsConfig config, Func<HttpClient> httpClientFactory) =>
        new(config, httpClientFactory);
}