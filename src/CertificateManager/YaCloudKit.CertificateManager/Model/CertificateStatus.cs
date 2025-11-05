using System.Text.Json.Serialization;

namespace YaCloudKit.CertificateManager.Model;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CertificateStatus
{
	STATUS_UNSPECIFIED,
	VALIDATING,
	INVALID,
	ISSUED,
	REVOKED,
	RENEWING,
	RENEWAL_FAILED
}