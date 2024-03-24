using System;

namespace YaCloudKit.TTS;

public class VoiceName
{
    public static readonly VoiceName Lea = new("lea");
    public static readonly VoiceName Alena = new("alena");
    public static readonly VoiceName Filipp = new("filipp");
    public static readonly VoiceName Jane = new("jane");
    public static readonly VoiceName Omazh = new("omazh");
    public static readonly VoiceName Zahar = new("zahar");
    public static readonly VoiceName Ermil = new("ermil");
    public static readonly VoiceName Amira = new("amira");
    public static readonly VoiceName John = new("john");
    public static readonly VoiceName Madi = new("madi");
    public static readonly VoiceName Madirus = new("madirus");
    public static readonly VoiceName Nigora = new("nigora");

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