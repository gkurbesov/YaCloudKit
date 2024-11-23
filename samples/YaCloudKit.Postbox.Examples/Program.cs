using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YaCloudKit.IAM;
using YaCloudKit.IAM.TokenProvider;
using YaCloudKit.Postbox;

var configurationSections = new Dictionary<string, string>
{
    { "YandexIam:ServiceAccountId", "your-service-account-id" },
    { "YandexIam:PublicKeyId", "your-public-key-id" }
};
var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection(configurationSections)
    .Build();

var services = new ServiceCollection();
services
    .AddYandexFilePrivateKeyProvider("your-private-key-file-path")
    .AddDefaultYandexIamTokenProvider()
    .AddDefaultYandexIamServiceClient(configuration);

services
    .AddYandexPostboxIamProvider(iamTokenFunc: async (provider, ct) =>
    {
        var iamProvider = provider.GetRequiredService<IYandexIamTokenProvider>();
        return await iamProvider.GetServicesTokenAsync(ct);
    })
    .AddYandexPostboxClient(configuration);

var serviceProvider = services.BuildServiceProvider();


var postbox = serviceProvider.GetRequiredService<IYandexPostboxClient>();

var request = new SendEmailRequestBuilder()
    .SetFromEmailAddress("noreply@example.com")
    .AddToAddress("to_address@example.com")
    .SetSubject("Test for YaCloudKit.Postbox")
    .SetTextBody("Hello, this is a test email from YaCloudKit.Postbox")
    .SetHtmlBody("<h1>Hello, this is a test email from YaCloudKit.Postbox</h1>")
    .Build();

try
{
    Console.WriteLine("Sending email...");
    var response = await postbox.SendMailAsync(request);
    Console.WriteLine("Email sent!");
    Console.WriteLine("Status code: " + response.HttpStatusCode);
    Console.WriteLine("MessageId: " + response.MessageId);
}
catch (YandexPostboxServiceException e)
{
    Console.WriteLine("Status code: " + e.StatusCode);
    Console.WriteLine("Error code: " + e.Code);
    Console.WriteLine(e.Message);
    if (e.InnerException != null)
        Console.WriteLine("\t" + e.InnerException.Message);
}
catch (Exception e)
{
    Console.WriteLine("Error: " + e.Message);
}