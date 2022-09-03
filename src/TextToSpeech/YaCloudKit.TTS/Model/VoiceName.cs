using System;

namespace YaCloudKit.TTS;

public class VoiceName
{
    public static readonly VoiceName Alena = new VoiceName("alena");
    public static readonly VoiceName Filipp = new VoiceName("filipp");
    public static readonly VoiceName Jane = new VoiceName("jane");
    public static readonly VoiceName Omazh = new VoiceName("omazh");
    public static readonly VoiceName Zahar = new VoiceName("zahar");
    public static readonly VoiceName Ermil = new VoiceName("ermil");
    public static readonly VoiceName Amira = new VoiceName("amira");
    public static readonly VoiceName John = new VoiceName("john");
    
    public string Value { get; }

    public VoiceName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(VoiceName emotion) => emotion.ToString();
}