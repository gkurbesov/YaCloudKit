namespace YaCloudKit.MQ.Transport.Tests;

public class MessageConverterTests
{
    [Fact]
    public void Xml_should_serialize_then_deserialize_entirely()
    {
        var converter = new XmlMessageConverter();
        var value = new Fixture().Create<TestMessage>();
        
        var xml = converter.Serialize(value);
        var deserializedValue = converter.Deserialize(xml, typeof(TestMessage));
        
        deserializedValue.Should().BeEquivalentTo(value);
    }
    
    [Fact]
    public void Json_should_serialize_then_deserialize_entirely()
    {
        var converter = new JsonMessageConverter();
        var value = new Fixture().Create<TestMessage>();
        
        var xml = converter.Serialize(value);
        var deserializedValue = converter.Deserialize(xml, typeof(TestMessage));
        
        deserializedValue.Should().BeEquivalentTo(value);
    }
}