using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace YaCloudKit.MQ.Transport;

public abstract class MessageHandlerBase
{
    public abstract Task Handle(object message, CancellationToken cancellationToken, ServiceFactory serviceFactory);
}