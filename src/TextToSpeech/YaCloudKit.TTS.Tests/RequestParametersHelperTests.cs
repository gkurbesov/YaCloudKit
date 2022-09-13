namespace YaCloudKit.TTS.Tests;

public class RequestParametersHelperTests
{
    [Fact]
    public void AddTextParam_TextIsNullOrNull_ThrowArgumentNullException()
    {
        var context = new RequestContext();

        Assert.Throws<ArgumentNullException>(() => RequestParametersHelper.AddTextParam(context, null, false));
    }
    
    [Fact]
    public void AddTextParam_TooLongText_ThrowArgumentOutOfRangeException()
    {
        var context = new RequestContext();

        var text = new string('@', 5001);

        Assert.Throws<ArgumentOutOfRangeException>(() => RequestParametersHelper.AddTextParam(context, text, false));
    }
    
    [Fact]
    public void AddTextParam_SsmlAdded()
    {
        var context = new RequestContext();
        
        var text = Guid.NewGuid().ToString();
        RequestParametersHelper.AddTextParam(context, text, true);
        
        Assert.True(context.RequestParameters.ContainsKey("ssml"));
        Assert.Equal(text, context.RequestParameters["ssml"]);
    }
    
    [Fact]
    public void AddTextParam_TextAdded()
    {
        var context = new RequestContext();
        
        var text = Guid.NewGuid().ToString();
        RequestParametersHelper.AddTextParam(context, text, false);
        
        Assert.True(context.RequestParameters.ContainsKey("text"));
        Assert.Equal(text, context.RequestParameters["text"]);
    }
    
    [Fact]
    public void AddVoiceParam_ParamsIsNull_ThrowArgumentNullException()
    {
        var context = new RequestContext();

        Assert.Throws<ArgumentNullException>(() => RequestParametersHelper.AddVoiceParam(context, null));
    }

    [Fact]
    public void AddVoiceParam_ParamsAdded()
    {
        var context = new RequestContext();

        var voiceParameters = new VoiceParameters(VoiceName.Alena)
        {
            Language = VoiceLanguage.Russian,
            Emotion = VoiceEmotion.Neutral,
            Speed = "0.1"
        };

        RequestParametersHelper.AddVoiceParam(context, voiceParameters);
        
        Assert.True(context.RequestParameters.TryGetValue("voice", out var voice));
        Assert.Equal(VoiceName.Alena, voice);
        Assert.True(context.RequestParameters.TryGetValue("lang", out var lang));
        Assert.Equal(VoiceLanguage.Russian, lang);
        Assert.True(context.RequestParameters.TryGetValue("emotion", out var emotion));
        Assert.Equal(VoiceEmotion.Neutral, emotion);
        Assert.True(context.RequestParameters.TryGetValue("speed", out var speed));
        Assert.Equal("0.1", speed);
    }
    
    [Fact]
    public void AddFormatParam_FormatIsNull_ThrowArgumentNullException()
    {
        var context = new RequestContext();

        Assert.Throws<ArgumentException>(() => RequestParametersHelper.AddFormatParam(context, null));
    }
    
    [Fact]
    public void AddFormatParam_FormatAdded()
    {
        var context = new RequestContext();

        RequestParametersHelper.AddFormatParam(context, AudioFormat.Lpcm16000);
        
        Assert.True(context.RequestParameters.TryGetValue("format", out var format));
        Assert.Equal(AudioFormat.Lpcm16000.Format, format);
        Assert.True(context.RequestParameters.TryGetValue("sampleRateHertz", out var sampleRateHertz));
        Assert.Equal(AudioFormat.Lpcm16000.SampleRateHertz.ToString(), sampleRateHertz);
    }
    
    [Fact]
    public void AddFolderParam_ConfigIsNull_ThrowArgumentNullException()
    {
        var context = new RequestContext();

        Assert.Throws<ArgumentException>(() => RequestParametersHelper.AddFormatParam(context, null));
    }
    
    [Fact]
    public void AddFormatParam_ConfigTokenIAMIsEmpty_FolderIdNotSet()
    {
        var context = new RequestContext();
        var config = new YandexTtsConfig(Guid.NewGuid().ToString());

        RequestParametersHelper.AddFolderParam(context, config);
        
        Assert.False(context.RequestParameters.ContainsKey("folderId"));
    }
    
    [Fact]
    public void AddFormatParam_FolderIdAdded()
    {
        var context = new RequestContext();
        var folderId = Guid.NewGuid().ToString();
        var config = new YandexTtsConfig("token", folderId);

        RequestParametersHelper.AddFolderParam(context, config);
        
        Assert.True(context.RequestParameters.TryGetValue("folderId", out var folder));
        Assert.Equal(folderId, folder);
    }
}