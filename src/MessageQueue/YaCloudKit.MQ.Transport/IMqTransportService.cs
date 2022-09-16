using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.MQ.Model;

namespace YaCloudKit.MQ.Transport;

public interface IMqTransportService
{
    Task ReceiveAsync(Message message, CancellationToken cancellationToken);

    Task SendAsync<TMessage>(IYandexMq mq, string queueUrl, string converterName, TMessage message, CancellationToken cancellationToken);
}