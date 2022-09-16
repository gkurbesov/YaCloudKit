using System;
using System.Threading;
using System.Threading.Tasks;

namespace YaCloudKit.MQ.Transport;

public class MessageHandlerWrapper<TMessage> : MessageHandlerBase where TMessage: class, new()
{
    public override async Task Handle(object message, CancellationToken cancellationToken, ServiceFactory serviceFactory)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));
        if (serviceFactory == null)
            throw new ArgumentNullException(nameof(serviceFactory));
        
        await Handle((TMessage)message, cancellationToken, serviceFactory).ConfigureAwait(false);
    }

    private Task Handle(TMessage message, CancellationToken cancellationToken, ServiceFactory serviceFactory)
    {
        var handler = GetHandler<IMessageHandler<TMessage>>(serviceFactory);

        return handler.HandleAsync(message, cancellationToken);
    }
    
    private static THandler GetHandler<THandler>(ServiceFactory factory)
    {
        THandler handler;

        try
        {
            handler = factory.GetInstance<THandler>();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(
                $"Error constructing handler for message of type {typeof(THandler)}. Register your handlers with the container.",
                e);
        }

        if (handler == null)
        {
            throw new InvalidOperationException(
                $"Handler was not found for message of type {typeof(THandler)}. Register your handlers with the container.");
        }

        return handler;
    }
}