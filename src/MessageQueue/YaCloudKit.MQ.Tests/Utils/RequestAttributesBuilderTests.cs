using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ.Tests;

public class RequestAttributesBuilderTests
{
    private const string TestKey = "key1";
    private const string TestKey2 = "key2";
    private const string TestValue = "value123";
    private const string TestValue2 = "value456";

    [Fact]
    public void NamedAttributes_RequestParametersContainsCorrectData()
    {
        var context = new RequestContext();

        var attributes = new Dictionary<string, string>
        {
            [TestKey] = TestValue,
            [TestKey2] = TestValue2
        };

        RequestAttributesBuilder.NamedAttributes(context, attributes);

        Assert.True(context.RequestParameters.TryGetValue("Attribute.1.Name", out var keyValue1));
        Assert.True(context.RequestParameters.TryGetValue("Attribute.2.Name", out var keyValue2));
        Assert.Equal(TestKey, keyValue1);
        Assert.Equal(TestKey2, keyValue2);

        Assert.True(context.RequestParameters.TryGetValue("Attribute.1.Value", out var dataValue1));
        Assert.True(context.RequestParameters.TryGetValue("Attribute.2.Value", out var dataValue2));
        Assert.Equal(TestValue, dataValue1);
        Assert.Equal(TestValue2, dataValue2);
    }

    [Fact]
    public void MessageAttributes_StringType_RequestParametersContainsCorrectData()
    {
        var context = new RequestContext();

        var attributes = new Dictionary<string, MessageAttributeValue>
        {
            [TestKey] = new MessageAttributeValue()
            {
                StringValue = TestValue,
                DataType = AttributeValueType.String
            }
        };

        RequestAttributesBuilder.MessageAttributes(context, attributes);


        Assert.True(context.RequestParameters.TryGetValue("MessageAttribute.1.Name", out var keyValue1));
        Assert.Equal(TestKey, keyValue1);

        Assert.True(context.RequestParameters.TryGetValue("MessageAttribute.1.Value.DataType", out var dataType));
        Assert.Equal(AttributeValueType.String.ToString(), dataType);

        Assert.True(
            context.RequestParameters.TryGetValue("MessageAttribute.1.Value.StringValue", out var dataValue));
        Assert.Equal(TestValue, dataValue);
    }

    [Fact]
    public void MessageAttributes_NumberType_RequestParametersContainsCorrectData()
    {
        const int value = 123;

        var context = new RequestContext();

        var attributes = new Dictionary<string, MessageAttributeValue>
        {
            [TestKey] = new MessageAttributeValue()
            {
                StringValue = value.ToString(),
                DataType = AttributeValueType.Number
            }
        };

        RequestAttributesBuilder.MessageAttributes(context, attributes);


        Assert.True(context.RequestParameters.TryGetValue("MessageAttribute.1.Name", out var keyValue1));
        Assert.Equal(TestKey, keyValue1);

        Assert.True(context.RequestParameters.TryGetValue("MessageAttribute.1.Value.DataType", out var dataType));
        Assert.Equal(AttributeValueType.Number.ToString(), dataType);

        Assert.True(
            context.RequestParameters.TryGetValue("MessageAttribute.1.Value.StringValue", out var dataValue));
        Assert.True(int.TryParse(dataValue, out var numberValue));
        Assert.Equal(value, numberValue);
    }

    [Fact]
    public void MessageAttributes_BinaryType_RequestParametersContainsCorrectData()
    {
        byte[] value = new byte[] {0x00, 0xFF, 0xA9};

        var context = new RequestContext();

        var attributes = new Dictionary<string, MessageAttributeValue>
        {
            [TestKey] = new MessageAttributeValue()
            {
                BinaryValue = value,
                DataType = AttributeValueType.Binary
            }
        };

        RequestAttributesBuilder.MessageAttributes(context, attributes);


        Assert.True(context.RequestParameters.TryGetValue("MessageAttribute.1.Name", out var keyValue1));
        Assert.Equal(TestKey, keyValue1);

        Assert.True(context.RequestParameters.TryGetValue("MessageAttribute.1.Value.DataType", out var dataType));
        Assert.Equal(AttributeValueType.Binary.ToString(), dataType);

        Assert.True(context.RequestParameters.TryGetValue("MessageAttribute.1.Value.BinaryValue",
            out var base64Value));
        Assert.NotEmpty(base64Value);

        var bytesValue = Convert.FromBase64String(base64Value);

        Assert.True(value.SequenceEqual(bytesValue));
    }

    [Fact]
    public void MessageAttributesBatchEntry_StringType_RequestParametersContainsCorrectData()
    {
        var context = new RequestContext();

        var attributes = new Dictionary<string, MessageAttributeValue>
        {
            [TestKey] = new MessageAttributeValue()
            {
                StringValue = TestValue,
                DataType = AttributeValueType.String
            }
        };

        var entryNumber = 10;
        RequestAttributesBuilder.MessageAttributesBatchEntry(context, attributes, entryNumber);


        Assert.True(context.RequestParameters.TryGetValue(
            $"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.1.Name", out var keyValue1));
        Assert.Equal(TestKey, keyValue1);

        Assert.True(context.RequestParameters.TryGetValue(
            $"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.1.Value.DataType", out var dataType));
        Assert.Equal(AttributeValueType.String.ToString(), dataType);

        Assert.True(context.RequestParameters.TryGetValue(
            $"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.1.Value.StringValue", out var dataValue));
        Assert.Equal(TestValue, dataValue);
    }

    [Fact]
    public void MessageAttributesBatchEntry_NumberType_RequestParametersContainsCorrectData()
    {
        const int value = 123;

        var context = new RequestContext();

        var attributes = new Dictionary<string, MessageAttributeValue>
        {
            [TestKey] = new MessageAttributeValue()
            {
                StringValue = value.ToString(),
                DataType = AttributeValueType.Number
            }
        };

        var entryNumber = 10;
        RequestAttributesBuilder.MessageAttributesBatchEntry(context, attributes, entryNumber);


        Assert.True(context.RequestParameters.TryGetValue(
            $"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.1.Name", out var keyValue1));
        Assert.Equal(TestKey, keyValue1);

        Assert.True(context.RequestParameters.TryGetValue(
            $"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.1.Value.DataType", out var dataType));
        Assert.Equal(AttributeValueType.Number.ToString(), dataType);

        Assert.True(context.RequestParameters.TryGetValue(
            $"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.1.Value.StringValue", out var dataValue));
        Assert.True(int.TryParse(dataValue, out var numberValue));
        Assert.Equal(value, numberValue);
    }

    [Fact]
    public void MessageAttributesBatchEntry_BinaryType_RequestParametersContainsCorrectData()
    {
        byte[] value = new byte[] {0x00, 0xFF, 0xA9};

        var context = new RequestContext();

        var attributes = new Dictionary<string, MessageAttributeValue>
        {
            [TestKey] = new MessageAttributeValue()
            {
                BinaryValue = value,
                DataType = AttributeValueType.Binary
            }
        };


        var entryNumber = 10;
        RequestAttributesBuilder.MessageAttributesBatchEntry(context, attributes, entryNumber);


        Assert.True(context.RequestParameters.TryGetValue(
            $"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.1.Name", out var keyValue1));
        Assert.Equal(TestKey, keyValue1);

        Assert.True(context.RequestParameters.TryGetValue(
            $"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.1.Value.DataType", out var dataType));
        Assert.Equal(AttributeValueType.Binary.ToString(), dataType);

        Assert.True(context.RequestParameters.TryGetValue(
            $"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.1.Value.BinaryValue",
            out var base64Value));
        Assert.NotEmpty(base64Value);

        var bytesValue = Convert.FromBase64String(base64Value);

        Assert.True(value.SequenceEqual(bytesValue));
    }

    [Fact]
    public void ListAttributes_RequestParametersContainsCorrectData()
    {
        var context = new RequestContext();

        var values = new List<string>()
        {
            TestValue,
            TestValue2
        };

        RequestAttributesBuilder.ListAttributes(context, values);

        Assert.True(context.RequestParameters.TryGetValue("AttributeName.1", out var value1));
        Assert.Equal(TestValue, value1);
        Assert.True(context.RequestParameters.TryGetValue("AttributeName.2", out var value2));
        Assert.Equal(TestValue2, value2);
    }

    [Fact]
    public void ListMessageAttributes_RequestParametersContainsCorrectData()
    {
        var context = new RequestContext();

        var values = new List<string>()
        {
            TestValue,
            TestValue2
        };

        RequestAttributesBuilder.ListMessageAttributes(context, values);

        Assert.True(context.RequestParameters.TryGetValue("MessageAttributeName.1", out var value1));
        Assert.Equal(TestValue, value1);
        Assert.True(context.RequestParameters.TryGetValue("MessageAttributeName.2", out var value2));
        Assert.Equal(TestValue2, value2);
    }
}