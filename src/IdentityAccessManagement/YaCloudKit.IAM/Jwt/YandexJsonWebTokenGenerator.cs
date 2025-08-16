using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YaCloudKit.IAM.Rsa;

namespace YaCloudKit.IAM.Jwt;

public class YandexJsonWebTokenGenerator(IOptionsMonitor<YandexIamOptions> options, IYandexRsaFactory rsaFactory)
	: IDisposable
{
	private bool _disposed;
	private RSA? _rsa;

	public async Task<string> GenerateJwtAsync(CancellationToken cancellationToken = default)
	{
		if (_disposed)
			throw new ObjectDisposedException(nameof(YandexJsonWebTokenGenerator));

		var optionsValue = options.CurrentValue;
		ArgumentNullException.ThrowIfNull(optionsValue.ServiceAccountId);
		ArgumentNullException.ThrowIfNull(optionsValue.PublicKeyId);

		_rsa ??= await rsaFactory.CreateRsaAsync(cancellationToken);
		var header = new JwtHeader(new SigningCredentials(new RsaSecurityKey(_rsa), SecurityAlgorithms.RsaSsaPssSha256))
		{
			{ "kid", optionsValue.PublicKeyId }
		};

		var now = DateTimeOffset.UtcNow;
		var claims = new List<Claim>
		{
			new(JwtRegisteredClaimNames.Aud, "https://iam.api.cloud.yandex.net/iam/v1/tokens"),
			new(JwtRegisteredClaimNames.Iss, optionsValue.ServiceAccountId),
			new("iat", now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer),
			new("exp", now.AddSeconds(3600).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer)
		};
		var payload = new JwtPayload(claims);
		var jwtSecurityToken = new JwtSecurityToken(header, payload);
		var handler = new JwtSecurityTokenHandler();
		return handler.WriteToken(jwtSecurityToken);
	}

	public void Dispose()
	{
		if (_disposed)
			return;
		_rsa?.Dispose();
		_disposed = true;
	}
}