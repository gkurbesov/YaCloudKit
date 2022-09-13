namespace YaCloudKit.TTS.Tests;

public class YandexTtsClientTests
{
    [Fact]
    public async Task TextIsNullOrEmpty_ThrowException()
    {
        var client = new YandexTtsClient(new YandexTtsConfig());
        
        await Assert.ThrowsAsync<ArgumentNullException>( () =>
            client.TextToSpeechAsync(null, VoiceParameters.Alena, AudioFormat.Ogg));
       await Assert.ThrowsAsync<ArgumentNullException>( () =>
           client.TextToSpeechAsync(string.Empty, VoiceParameters.Alena, AudioFormat.Ogg));
       await Assert.ThrowsAsync<ArgumentNullException>( () =>
           client.MarkupToSpeechAsync(null, VoiceParameters.Alena, AudioFormat.Ogg));
       await Assert.ThrowsAsync<ArgumentNullException>( () =>
           client.MarkupToSpeechAsync(string.Empty, VoiceParameters.Alena, AudioFormat.Ogg));
    }
    
    [Fact]
    public async Task VoiceParametersIsNull_ThrowException()
    {
        var client = new YandexTtsClient(new YandexTtsConfig());
        
        await Assert.ThrowsAsync<ArgumentNullException>( () =>
            client.TextToSpeechAsync("test", null, AudioFormat.Ogg));
        await Assert.ThrowsAsync<ArgumentNullException>( () =>
            client.MarkupToSpeechAsync("test", null, AudioFormat.Ogg));
    }
    
    [Fact]
    public async Task AudioFormatIsNull_ThrowException()
    {
        var client = new YandexTtsClient(new YandexTtsConfig());
        
        await Assert.ThrowsAsync<ArgumentNullException>( () =>
            client.TextToSpeechAsync("test", VoiceParameters.Alena, null));
        await Assert.ThrowsAsync<ArgumentNullException>( () =>
            client.MarkupToSpeechAsync("test", VoiceParameters.Alena, null));
    }
}