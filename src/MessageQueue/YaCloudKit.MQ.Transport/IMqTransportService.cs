using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ.Transport;

public interface IMqTransportService
{
    Task HandleAsync(Message message, CancellationToken cancellationToken);

    Task<SendMessageRequest> TransformAsync<TMessage>(TMessage message, string converterName, CancellationToken cancellationToken);
}