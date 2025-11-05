using System.Text.Json.Serialization;

namespace YaCloudKit.CertificateManager.Model;

public record CertificateContentDto
{
	[JsonPropertyName("certificateId")]
	public required string CertificateId { get; init; }

	[JsonPropertyName("certificateChain")]
	public required IReadOnlyCollection<string> CertificateChain { get; init; }

	[JsonPropertyName("privateKey")]
	public required string PrivateKey { get; init; }
}