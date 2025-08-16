using System.Text;
using System.Text.RegularExpressions;

namespace YaCloudKit.IAM.Rsa;

public partial class YandexFilePrivateKeyProvider(string privateKeyFilePath, bool cacheResult = false)
	: YandexCachedPrivateKeyProvider(cacheResult)
{
	protected override async Task<char[]> GetPrivateKeyCoreAsync(CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(privateKeyFilePath))
			throw new ArgumentNullException(nameof(privateKeyFilePath), "Path is null or empty.");

		if (!Path.IsPathFullyQualified(privateKeyFilePath))
			throw new ArgumentException("Path must be absolute.", nameof(privateKeyFilePath));

		if (!File.Exists(privateKeyFilePath))
			throw new FileNotFoundException("Private key file not found.", privateKeyFilePath);

		var fi = new FileInfo(privateKeyFilePath);
		if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
			throw new ArgumentException("Path points to a directory, not a file.", nameof(privateKeyFilePath));

		// Open safely; honor cancellation on read.
		await using var fs = new FileStream(
			privateKeyFilePath,
			FileMode.Open,
			FileAccess.Read,
			FileShare.Read,
			bufferSize: 4096,
			options: FileOptions.Asynchronous | FileOptions.SequentialScan);

		// Size checks.
		if (fs.Length == 0)
			throw new InvalidDataException("Private key file is empty.");

		using var reader = new StreamReader(fs, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 4096,
			leaveOpen: false);
		var content = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);

		if (string.IsNullOrWhiteSpace(content))
			throw new InvalidDataException("Private key content is empty or whitespace.");

		// Basic PEM validation.
		if (!PemHeaderRegex().IsMatch(content))
			throw new FormatException("Private key does not look like a PEM PRIVATE KEY.");

		// Convert to char[] (note: string will stay in memory; avoid logging it).
		return content.ToCharArray();
	}

	[GeneratedRegex(@"^-{5}BEGIN [A-Z ]*PRIVATE KEY-{5}\s", RegexOptions.Multiline | RegexOptions.CultureInvariant)]
	private static partial Regex PemHeaderRegex();
}