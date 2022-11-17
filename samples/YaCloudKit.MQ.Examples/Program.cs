using System.Net;
using YaCloudKit.MQ;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Model.Responses;

const string accessKeyId = "[access-key-id]";
const string secretAccessKey = "[secret-access-key]";

// Configure service instance
IYandexMq mq = new YandexMqClient(accessKeyId, secretAccessKey);


// Create queue
WriteTitle("Create test queue");
var createQueueRequest = new CreateQueueRequest();
createQueueRequest.QueueName = "TestQueue";

var createQueueResult = await mq.CreateQueueAsync(createQueueRequest);
WriteRequestResult(createQueueResult);

if (!ResponseStatusIsSuccess(createQueueResult))
    return;

var testQueueUrl = createQueueResult.QueueUrl;


// Send message
await Task.Delay(TimeSpan.FromSeconds(2));
WriteTitle("Send message");
var message = new SendMessageRequest()
    .SetQueueUrl(testQueueUrl)
    .SetMessageBody("My first message in Yandex Message Queue")
    .SetMessageAttribute("custom-attribute-string", "Attribute value")
    .SetMessageAttribute("custom-attribute-int", 123)
    .SetMessageAttribute("custom-attribute-bytes", new byte[] {0x10, 0xff, 0xb1});

var sendMessageResult = await mq.SendMessageAsync(message);
WriteRequestResult(sendMessageResult);

if (!ResponseStatusIsSuccess(sendMessageResult))
    return;


// Delay
WriteTitle("Wait 5 second...\r\n");
await Task.Delay(TimeSpan.FromSeconds(5));


// Receive messages
WriteTitle("Receive message");
var receiveRequest = new ReceiveMessageRequest()
    .SetQueueUrl(testQueueUrl)
    .SetWaitTimeSeconds(10)
    .SetAllMessageAttribute();

var receiveMessageResult = await mq.ReceiveMessageAsync(receiveRequest);
WriteRequestResult(receiveMessageResult);

if (!ResponseStatusIsSuccess(receiveMessageResult))
    return;

var receivedMessage = receiveMessageResult.Messages.SingleOrDefault();

if (receivedMessage != null)
{
    // Delete message
    await Task.Delay(TimeSpan.FromSeconds(2));
    Console.WriteLine("Delete message");
    var deleteMessageRequest = new DeleteMessageRequest()
        .SetQueueUrl(testQueueUrl)
        .SetReceiptHandle(receivedMessage.ReceiptHandle);

    var deleteMessageResult = await mq.DeleteMessageAsync(deleteMessageRequest);
    WriteRequestResult(deleteMessageResult);

    if (!ResponseStatusIsSuccess(deleteMessageResult))
        return;
}

// Delete queue
await Task.Delay(TimeSpan.FromSeconds(2));
Console.WriteLine("\r\nDelete queue");
var deleteQueueRequest = new DeleteQueueRequest();
deleteQueueRequest.QueueUrl = testQueueUrl;

var deleteQueueResult = await mq.DeleteQueueAsync(deleteQueueRequest);
WriteRequestResult(deleteQueueResult);


void WriteTitle(string message)
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(message);
    Console.ResetColor();
}

void WriteRequestResult(YandexMessageQueueResponse response)
{
    Console.Write($"Status code: ");
    Console.ForegroundColor = response.HttpStatusCode == HttpStatusCode.OK
        ? ConsoleColor.DarkGreen
        : ConsoleColor.DarkRed;
    Console.WriteLine(response.HttpStatusCode.ToString());
    Console.ResetColor();
    switch (response)
    {
        case CreateQueueResponse createQueueResponse:
            Console.WriteLine("\tQueueUrl:" + createQueueResponse.QueueUrl);
            break;
        case SendMessageResponse sendMessageResponse:
            Console.WriteLine("\tMessageId: " + sendMessageResponse.MessageId);
            Console.WriteLine("\tSequenceNumber: " + sendMessageResponse.SequenceNumber);
            Console.WriteLine("\tMD5OfMessageBody: " + sendMessageResponse.MD5OfMessageBody);
            break;
        case ReceiveMessageResponse receiveMessageResponse:
            var message = receiveMessageResponse.Messages.SingleOrDefault();

            if (message == null)
            {
                Console.WriteLine("No messages received...");
                break;
            }

            Console.WriteLine("\tMessageId: " + message.MessageId);
            Console.WriteLine("\tReceiptHandle: " + message.ReceiptHandle);
            Console.WriteLine("\tMD5OfBody: " + message.MD5OfBody);
            if (message.Attribute.Count > 0)
            {
                Console.WriteLine("\tAttribute:");
                foreach (var attribute in message.Attribute)
                {
                    Console.WriteLine($"\t\t{attribute.Key}: {attribute.Value}");
                }
            }

            if (message.MessageAttribute.Count > 0)
            {
                Console.WriteLine("\tMessageAttribute:");
                foreach (var attribute in message.MessageAttribute)
                {
                    var attributeValue = attribute.Value.DataType switch
                    {
                        AttributeValueType.Binary => BitConverter
                            .ToString(attribute.Value.BinaryValue)
                            .Replace("-", string.Empty),
                        _ => attribute.Value.StringValue,
                    };
                    Console.WriteLine($"\t\t[{attribute.Value.DataType}]\t{attribute.Key}: {attributeValue}");
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\tMessage body: ");
            Console.WriteLine(message.Body);
            Console.ResetColor();
            break;
    }

    WriteResponseMetadata(response);
    Console.WriteLine("\r\n");
}

void WriteResponseMetadata(YandexMessageQueueResponse response)
{
    Console.WriteLine("METADATA:");
    Console.WriteLine($"\tRequest ID: {response.ResponseMetadata.RequestId}");
    foreach (var metadata in response.ResponseMetadata.Metadata)
    {
        Console.WriteLine($"\t{metadata.Key}: {metadata.Value}");
    }
}

bool ResponseStatusIsSuccess(YandexMessageQueueResponse response) =>
    response.HttpStatusCode == HttpStatusCode.OK;