using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YaCloudKit.IAM;

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
    .AddDefaultYandexIamServiceClient(configuration);

var serviceProvider = services.BuildServiceProvider();

var yandexIamServiceClient = serviceProvider.GetRequiredService<IYandexIamServiceClient>();

var iamTokenResponse = await yandexIamServiceClient.GetIamForServiceAccountAsync();

Console.WriteLine("IAM token: " + iamTokenResponse.IamToken.Substring(0, 20) + "...");
Console.WriteLine("Expires at: " + iamTokenResponse.ExpiresAt);