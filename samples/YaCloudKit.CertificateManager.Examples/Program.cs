using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YaCloudKit.CertificateManager;

const string yourIamToken = "<IAM_TOKEN>";
const string certificateId = "<CERTIFICATE_ID>";

var configuration = new ConfigurationBuilder()
	.AddInMemoryCollection()
	.Build();

var services = new ServiceCollection();

services
	.AddYandexCertificateManagerOptions(configuration);
services
	.AddYandexCertificateManagerIamProvider(iamTokenFunc: async (_, __) => yourIamToken)
	.AddYandexCertificateManagerClient();

var serviceProvider = services.BuildServiceProvider();

var certManagerClient = serviceProvider.GetRequiredService<IYandexCertificateManagerClient>();

var certificate = await certManagerClient.GetCertificateAsync(certificateId);

Console.WriteLine($"Certificate ID: {certificate.Id}");
Console.WriteLine($"Certificate Name: {certificate.Name}");
Console.WriteLine($"Certificate Created At: {certificate.CreatedAt}");
Console.WriteLine($"Certificate Status: {certificate.Status}");
Console.WriteLine($"Certificate Domains: {string.Join(", ", certificate.Domains ?? [])}");