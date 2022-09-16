using System.Threading;
using System.Threading.Tasks;

namespace YaCloudKit.MQ.Transport;

public interface IMessageHandler<in TMessage> where TMessage : class, new()
{
    Task HandleAsync(TMessage message, CancellationToken cancellationToken);
}