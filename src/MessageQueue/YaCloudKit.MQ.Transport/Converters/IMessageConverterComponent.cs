using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Transport;

public interface IMessageConverterComponent
{
    object Deserialize(Message message);

    void Serialize(string converterName, object value, in SendMessageRequest sendMessageRequest);
}