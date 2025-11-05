using System.Text.Json.Serialization;

namespace YaCloudKit.CertificateManager.Model;

public record CertificateDto
{
	[JsonPropertyName("id")]
	public required string Id { get; init; }

	[JsonPropertyName("folderId")]
	public required string FolderId { get; init; }

	[JsonPropertyName("createdAt")]
	public required DateTime CreatedAt { get; init; }

	[JsonPropertyName("name")]
	public required string Name { get; init; }

	[JsonPropertyName("description")]
	public string? Description { get; init; }

	[JsonPropertyName("labels")]
	public IReadOnlyDictionary<string, string>? Labels { get; init; }

	[JsonPropertyName("type")]
	public CertificateType Type { get; init; }

	[JsonPropertyName("domains")]
	public IReadOnlyCollection<string>? Domains { get; init; }

	[JsonPropertyName("status")]
	public CertificateStatus Status { get; init; }

	[JsonPropertyName("issuer")]
	public required string Issuer { get; init; }

	[JsonPropertyName("subject")]
	public required string Subject { get; init; }

	[JsonPropertyName("serial")]
	public required string Serial { get; init; }

	[JsonPropertyName("updatedAt")]
	public DateTime? UpdatedAt { get; init; }

	[JsonPropertyName("issuedAt")]
	public DateTime? IssuedAt { get; init; }

	[JsonPropertyName("notAfter")]
	public DateTime? NotAfter { get; init; }

	[JsonPropertyName("notBefore")]
	public DateTime? NotBefore { get; init; }

	[JsonPropertyName("deletionProtection")]
	public bool DeletionProtection { get; init; }

	[JsonPropertyName("incompleteChain")]
	public bool IncompleteChain { get; init; }
}