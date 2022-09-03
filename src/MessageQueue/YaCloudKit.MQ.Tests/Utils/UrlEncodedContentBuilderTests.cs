using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ.Tests;

public class UrlEncodedContentBuilderTests
{
    [Fact]
    public void Encode_Test()
    {
        var sourceValue = "hello@me friend";
        var expectedValue = "hello%40me+friend";

        var actualValue = UrlEncodedContentBuilder.Encode(sourceValue);

        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetContentByteArray_Test()
    {
        var values = new Dictionary<string, string>()
        {
            ["key1"] = "hello@me friend",
            ["key2"] = "value",
        };

        var expectedValue = Encoding.UTF8.GetBytes("key1=hello%40me+friend&key2=value");

        var content = UrlEncodedContentBuilder.GetContentByteArray(values);

        Assert.True(expectedValue.SequenceEqual(content));
    }
}