using Microsoft.Extensions.DependencyInjection;
using YaCloudKit.MQ.Extensions.DependencyInjection;
using YaCloudKit.MQ.Model.Responses;
using YaCloudKit.MQ.Transport.Extensions.DependencyInjection;

namespace YaCloudKit.MQ.Transport.Examples;

public class ProducerService : BaseService
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
        });
    }

    public async Task Send(object dto)
    {
        var transport = ServiceProvider.GetRequiredService<IMqTransportService>();
        var mq = ServiceProvider.GetRequiredService<IYandexMq>();

        var message = await transport.TransformAsync(dto, JsonMessageConverter.DefaultName, CancellationToken.None);

        message.SetQueueUrl(YandexMqClientOptions.QueueUrl);
        
        await mq.SendMessageAsync(message, CancellationToken.None);
    }
}