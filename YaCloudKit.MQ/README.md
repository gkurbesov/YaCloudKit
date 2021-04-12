# YaCloudKit.MQ

YaCloudKit.MQ - неофицальный клиент для работы с очередью сообщений Yandex Message Queue, используемый для обмена сообщениями между компонентами распределённых приложений и микросервисов.

Библиотека YaCloudKit.MQ полностью адаптирова под сервис Yandex Message Queue и, в отлиции от AWS.SQS, не имеет лишних неиспользуемых зависимостей и кода, что значительно уменьшает используемые ресурсы.

Вы с легкостью можете создать клиент, без указания лишних параметров. Просто создайте новый экземпляр YandexMqClient с указанием идентификатора ключа доступа и секретного ключа и начинайте отправлять или принимать сообщения: 
```csharp
var mq = new YandexMqClient("AWS_ACCESS_KEY_ID", "AWS_SECRET_ACCESS_KEY");

// some work...
```

### Возможности

YaCloudKit.MQ реализует полный функционал описанный в [документации сервиса](https://cloud.yandex.ru/docs/message-queue/api-ref/) для работы с очередями и сообщениями и позволяет выполнять следующие операции:

* Создавать очереди сообщений
* Удалять очереди сообщений
* Запрашивать атрибуты очереди сообщений
* Изменять атрибуты очереди сообщений
* Запрашивать URL очереди сообщений
* Запрашивать список очередей сообщений
* Очищать очередь сообщений
* Отправлять сообения
* Запрашивать сообщения
* Устанавливать тайм-аут видимости обрабатываемого сообщения/сообщений
* Удалять сообщения

### Начало работы

YaCloudKit.MQ устанавливается из NuGet. Для работы библиотеки нужно так же установить основную зависимость:


```
Install-Package YaCloudKit.Core
Install-Package Serilog.MQ
```

После установки вы можете приступать к использованияю клиента. Следующий пример демонстрирует быстрое создание и конфигурирование клиента для последующей отправки сообщения в очередь:
```csharp
var mq = new YandexMqClient("AWS_ACCESS_KEY_ID", "AWS_SECRET_ACCESS_KEY");

var sendRequest = new SendMessageRequest()
{
    QueueUrl = "https://message-queue.api.cloud.yandex.net/...",
    MessageBody = "Hello message!"
};

try
{
    var sendResponse = await mq.SendMessageAsync(sendRequest);
    Console.WriteLine("Status code: " + sendResponse.HttpStatusCode);
    Console.WriteLine("Message id: " + sendResponse.MessageId);
    Console.WriteLine("MD5: " + sendResponse.MD5OfMessageBody);
}
catch (YandexMqServiceException ex)
{
    Console.WriteLine("Status code: " + ex.StatusCode);
    Console.WriteLine("Request id: " + ex.RequestId);
    Console.WriteLine("Type: " + ex.ErrorType);
    Console.WriteLine("Error code: " + ex.ErrorCode);
    Console.WriteLine("Message: " + ex.Message);
}
```

Если вам необходимо получить сообщения из очереди вы можете сделать это следующим образом:

```csharp
 var mq = new YandexMqClient("AWS_ACCESS_KEY_ID", "AWS_SECRET_ACCESS_KEY");

var receiveRequest = new ReceiveMessageRequest()
{
    QueueUrl = "https://message-queue.api.cloud.yandex.net/...",
    WaitTimeSeconds = 20 // установка значения ожидания для long-polling запроса
};

try
{
    var receiveResponse = await mq.ReceiveMessageAsync(receiveRequest);
    Console.WriteLine("Status code: " + receiveResponse.HttpStatusCode);
    foreach(var message in receiveResponse.Messages)
    {
        Console.WriteLine("\r\nMessage id: " + message.MessageId);
        Console.WriteLine("\tReceiptHandle: " + message.ReceiptHandle);
        Console.WriteLine("\tMD5: " + message.MD5OfBody);
        Console.WriteLine("\tBody: " + message.Body);

        // Удаляем сообщение из очереди после обработки
        var deleteRequest = new DeleteMessageRequest() {
            QueueUrl = "https://message-queue.api.cloud.yandex.net/...",
            ReceiptHandle = message.ReceiptHandle,
        };
        _ = await mq.DeleteMessageAsync(deleteRequest);                    
    }
}
catch (YandexMqServiceException ex)
{
    Console.WriteLine("Status code: " + ex.StatusCode);
    Console.WriteLine("Request id: " + ex.RequestId);
    Console.WriteLine("Type: " + ex.ErrorType);
    Console.WriteLine("Error code: " + ex.ErrorCode);
    Console.WriteLine("Message: " + ex.Message);
}
```



