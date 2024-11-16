using System.Net;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Transport.DependencyInjection;

namespace YaCloudKit.MQ.Transport.Examples;

public class ConsumerService : BaseService
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddYandexMqClient(YandexMqClientOptions.AccessKeyId, YandexMqClientOptions.SecretAccessKey);

        services.AddMqTransport(configure =>
            {
                configure
                    .WithMessageConverter(new JsonMessageConverter())
                    .WithMessageType("notification", typeof(NotificationDto))
                    .WithMessageType("registration", typeof(UserRegistrationDto));
            },
            Assembly.GetExecutingAssembly());

        services.AddTransient<MusicService>();
    }


    public async Task Receive()
    {
        var mq = ServiceProvider.GetRequiredService<IYandexMq>();

        var request = new ReceiveMessageRequest()
            .SetQueueUrl(YandexMqClientOptions.QueueUrl)
            .SetAllMessageAttribute()
            .SetMaxNumberOfMessage(2)
            .SetWaitTimeSeconds(5);

        var transport = ServiceProvider.GetRequiredService<IMqTransportService>();

        var response = await mq.ReceiveMessageAsync(request, CancellationToken.None);

        while (response.HttpStatusCode == HttpStatusCode.OK && response.Messages.Count > 0)
        {
            foreach (var message in response.Messages)
            {
                await transport.HandleAsync(message, CancellationToken.None);
                await mq.DeleteMessageAsync(
                    new DeleteMessageRequest()
                        .SetQueueUrl(YandexMqClientOptions.QueueUrl)
                        .SetReceiptHandle(message.ReceiptHandle), CancellationToken.None);
            }
            
            response = await mq.ReceiveMessageAsync(request, CancellationToken.None);
        }
    }
}