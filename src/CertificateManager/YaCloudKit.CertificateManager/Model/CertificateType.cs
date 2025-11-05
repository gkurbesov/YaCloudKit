using System.Text.Json.Serialization;

namespace YaCloudKit.CertificateManager.Model;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CertificateType
{
	CERTIFICATE_TYPE_UNSPECIFIED,
	IMPORTED,
	MANAGED
}