namespace YaCloudKit.TTS.Tests;

public class UrlEncodedContentBuilderTests
{
    [Fact]
    public void GetContentByteArray_Works()
    {
        var values = new Dictionary<string, string>()
        {
            ["key1"] = "value",
            ["key2"] = "value"
        };
        var expected = new byte[]
        {
            0x6b, 0x65, 0x79, 0x31, 0x3d, 0x76, 0x61, 0x6c, 0x75, 0x65, 0x26, 0x6b, 0x65, 0x79, 0x32, 0x3d, 0x76,
            0x61, 0x6c, 0x75, 0x65
        };

        var result = UrlEncodedContentBuilder.GetContentByteArray(values);
        
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Encode_SpacesAreReplaced()
    {
        var result = UrlEncodedContentBuilder.Encode("First second");
        Assert.Equal("First+second", result);
    }
}