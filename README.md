# YaCloudKit ![](./assets/icon-main.png)
<p align="center">
    <img src="./assets/logo-main.png">
</p>
Набор инструментов для работы с сервисами Yandex.Cloud

[![Main Build](https://github.com/gkurbesov/YaCloudKit/actions/workflows/main.yml/badge.svg)](https://github.com/gkurbesov/YaCloudKit/actions/workflows/main.yml)
[![Nuget](https://img.shields.io/nuget/v/YaCloudKit.MQ?label=MQ)](https://www.nuget.org/packages/YaCloudKit.MQ)
[![Nuget](https://img.shields.io/nuget/v/YaCloudKit.TTS?label=TTS)](https://www.nuget.org/packages/YaCloudKit.TTS)
## Описание
YaCloudKit - это набор инструментов, который позволит взаимодействовать вашему приложению с сервисами облачной платформы Яндекс.Облако.

Поддерживаются .Net Standard 2.0 и выше

## Инструменты
В этом разделе представлен список инструментов, которые уже реализованы.

**Реализованно:**
- [YaCloudKit.MQ](./src/MessageQueue) - клиент для работы с очередью сообщений Яндекс.Облака
- [YaCloudKit.MQ.Transport](./src/MessageQueue) - расширение для клиента очереди сообщений, отвечает за де/сериализацию и обработку сообщений
- [YaCloudKit.TTS](./src/TextToSpeech) - клиент SpeechKit для синтеза речи из текста

## YaCloudKit.MQ

YaCloudKit.MQ - это клиент для работы с очередью сообщений Яндекс.Облака.
Полностью адаптирова под сервис Yandex Message Queue и не имеет лишних неиспользуемых зависимостей и кода.

### Использование

Клиент легко подкючить и использовать всего в три шага:
```csharp
// Step 1. create client instance
var client = new YandexMqClient("[Access Key Id]", "[Secret Access Key]");

// Step 2. Create message
var message = new SendMessageRequest()
    .SetQueueUrl("[Queue url]")
    .SetMessageBody("Message test example");

// Step 3. Send message
await client.SendMessageAsync(message);
```

Больше примеров можно [посмотреть тут](./samples/YaCloudKit.MQ.Examples)

## YaCloudKit.MQ.Transport

YaCloudKit.MQ.Transport - это набор сервисов и компонентов призваных упростить обработку сообщений из очереди сообщений.


### Использование

Создайте обработчик для сообщшения:
```csharp
public class CustomerRegistrationHandler : IMessageHandler<CustomerRegistrationDto>
{
    private readonly ILogger<CustomerRegistrationHandler> _logger;
    
    public CustomerRegistrationHandler(ILogger<CustomerRegistrationHandler> logger)
    {
        // You can use dependency injection
        _logger = logger;
    }

    public async Task HandleAsync(CustomerRegistrationDto message, CancellationToken cancellationToken)
    {
        // do work
    }
}
```

Сконфигурируйте зависимости:

```csharp
services.AddYandexMqClient("[Access Key Id]", "[Secret Access Key]");

services.AddMqTransport(configure =>
    {
        configure
            .WithMessageConverter(new JsonMessageConverter())
            .WithMessageType("customer_registration", typeof(CustomerRegistrationDto));
    },
    Assembly.GetExecutingAssembly());
```

Получите одбновление из очереди и обработайте их:
```csharp
IYandexMq client = ... // Get IYandexMq
IMqTransportService transport = ... // Get IMqTransportService

var request = new ReceiveMessageRequest()
    .SetQueueUrl("[Queue url]")
    .SetAllMessageAttribute();

var response = await client.ReceiveMessageAsync(request);

if(response.HttpStatusCode == HttpStatusCode.OK && response.Messages.Count > 0)
{
    foreach (var message in response.Messages)
    {
        await transport.HandleAsync(message, CancellationToken.None);
        await client.DeleteMessageAsync(
            new DeleteMessageRequest()
                .SetQueueUrl(YandexMqClientOptions.QueueUrl)
                .SetReceiptHandle(message.ReceiptHandle), CancellationToken.None);
    }
}
```

Больше примеров можно [посмотреть тут](./samples/YaCloudKit.MQ.Transport.Examples)

## Лицензия
YaCloudKit предоставляется как есть по лицензии MIT. Для получения дополнительной информации см. [LICENSE](./LICENSE).

