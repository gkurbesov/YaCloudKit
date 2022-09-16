using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Transport.Converters;

namespace YaCloudKit.MQ.Transport;

public class MqTransportService : IMqTransportService
{
    private readonly IMessageConverterComponent _messageConverterComponent;
    private readonly ServiceFactory _serviceFactory;
    private static readonly ConcurrentDictionary<Type, MessageHandlerBase> _handlers = new();

    public MqTransportService(IMessageConverterComponent messageConverterComponent, ServiceFactory serviceFactory)
    {
        _messageConverterComponent = messageConverterComponent ?? throw new ArgumentNullException(nameof(messageConverterComponent));
        _serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
    }

    public Task ReceiveAsync(Message message, CancellationToken cancellationToken)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        var deserializedMessage = _messageConverterComponent.Deserialize(message);
        var messageType = deserializedMessage.GetType();

        var handler = _handlers.GetOrAdd(messageType,
            static t =>
                (MessageHandlerBase) (Activator.CreateInstance(typeof(MessageHandlerWrapper<>).MakeGenericType(t))
                                      ?? throw new InvalidOperationException(
                                          $"Could not create wrapper type for {t}")));

        return handler.Handle(deserializedMessage, cancellationToken, _serviceFactory);
    }

    public Task SendAsync<TMessage>(IYandexMq mq, string queueUrl, string converterName, TMessage message, CancellationToken cancellationToken)
    {
        if (mq == null)
            throw new ArgumentNullException(nameof(mq));
        if (string.IsNullOrWhiteSpace(queueUrl))
            throw new ArgumentNullException(nameof(queueUrl));
        if (string.IsNullOrWhiteSpace(converterName))
            throw new ArgumentNullException(nameof(converterName));
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        var request = new SendMessageRequest().SetQueueUrl(queueUrl);
        
        _messageConverterComponent.Serialize(converterName, message, request);

        return mq.SendMessageAsync(request, cancellationToken);
    }
}