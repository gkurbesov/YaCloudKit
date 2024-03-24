using System;

namespace YaCloudKit.TTS;

public class VoiceLanguage
{
    public static readonly VoiceLanguage Russian = new("ru-RU");
    public static readonly VoiceLanguage Kazakh = new("kk-KK");
    public static readonly VoiceLanguage English = new("en-US");
    public static readonly VoiceLanguage German = new("de-DE");
    public static readonly VoiceLanguage Uzbek = new("uz-UZ");
    
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