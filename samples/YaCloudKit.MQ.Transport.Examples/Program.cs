using YaCloudKit.MQ.Transport.Examples;

await JobProducer();

Console.WriteLine("Wait 5 seconds...");
await Task.Delay(TimeSpan.FromSeconds(5));

Console.WriteLine("Press Enter");
Console.ReadLine();

await JobConsumer();

Console.WriteLine("Completed!");

async Task JobProducer()
{
    var producer = new ProducerService();

    var notification = new NotificationDto()
    {
        Id = 7,
        Title = "Hello World!",
        Message = "This is test message notification"
    };

    Console.WriteLine("Send notification");
    await producer.Send(notification);

    await Task.Delay(TimeSpan.FromSeconds(2));
    Console.WriteLine("Send new user");
    var userRegistration = new UserRegistrationDto()
    {
        UserId = 1,
        Email = "example@gmail.com",
        RegistrationDateTime = DateTime.Now
    };

    await producer.Send(userRegistration);
}

async Task JobConsumer()
{
    var consumer = new ConsumerService();

    Console.WriteLine("Start receiving...");
    await consumer.Receive();
}
