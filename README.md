# YaCloudKit ![](./assets/icon-main.png)
<p align="center">
    <img src="./assets/logo-main.png">
</p>

A set of tools for working with Yandex.Cloud services.

[![Main Build](https://github.com/gkurbesov/YaCloudKit/actions/workflows/main.yml/badge.svg)](https://github.com/gkurbesov/YaCloudKit/actions/workflows/main.yml)
[![Nuget](https://img.shields.io/nuget/v/YaCloudKit.MQ?label=MQ)](https://www.nuget.org/packages/YaCloudKit.MQ)
[![Nuget](https://img.shields.io/nuget/v/YaCloudKit.TTS?label=TTS)](https://www.nuget.org/packages/YaCloudKit.TTS)
[![Nuget](https://img.shields.io/nuget/v/YaCloudKit.IAM?label=IAM)](https://www.nuget.org/packages/YaCloudKit.IAM)
[![Nuget](https://img.shields.io/nuget/v/YaCloudKit.Postbox?label=Postbox)](https://www.nuget.org/packages/YaCloudKit.Postbox)
## Description
YaCloudKit is a set of tools that enables your application to interact with Yandex.Cloud platform services.

Supports .Net 8.0 and higher.

## Tools
This section lists the tools that are currently implemented.

**Implemented:**
- [YaCloudKit.MQ](./src/MessageQueue) - a client for working with Yandex.Cloud Message Queue.
- [YaCloudKit.MQ.Transport](./src/MessageQueue) - an extension for the message queue client, responsible for message serialization/deserialization and processing.
- [YaCloudKit.TTS](./src/TextToSpeech) - a SpeechKit client for text-to-speech synthesis.

## YaCloudKit.MQ

YaCloudKit.MQ is a client for working with Yandex.Cloud Message Queue.
It is fully tailored for the Yandex Message Queue service without unnecessary dependencies or code.

### Usage

The client can be easily connected and used in three simple steps:

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

More examples can be found [here](./samples/YaCloudKit.MQ.Examples)

## YaCloudKit.MQ.Transport
YaCloudKit.MQ.Transport is a set of services and components designed to simplify message processing from the message queue.

### Usage

Create a handler for your message:
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

Configure dependencies:

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

Receive updates from the queue and process them:

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

More examples can be found [here](./samples/YaCloudKit.MQ.Transport.Examples)


## License
YaCloudKit is provided "as is" under the MIT License.

For more information, see [LICENSE](./LICENSE).


