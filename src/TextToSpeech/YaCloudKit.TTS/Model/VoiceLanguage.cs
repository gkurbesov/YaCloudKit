using System;

namespace YaCloudKit.TTS;

public class VoiceLanguage
{
    public static readonly VoiceLanguage Russian = new VoiceLanguage("ru-RU");
    public static readonly VoiceLanguage Kazakh = new VoiceLanguage("kk-KK");
    public static readonly VoiceLanguage English = new VoiceLanguage("en-US");
    
    public string Value { get; }

    public VoiceLanguage(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(VoiceLanguage emotion) => emotion.ToString();
}