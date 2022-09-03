using System;

namespace YaCloudKit.TTS;

public class VoiceEmotion
{
    public static readonly VoiceEmotion Neutral = new VoiceEmotion("neutral");
    public static readonly VoiceEmotion Good = new VoiceEmotion("good");
    public static readonly VoiceEmotion Evil = new VoiceEmotion("evil");
    
    public string Value { get; }

    public VoiceEmotion(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(VoiceEmotion emotion) => emotion.ToString();
}