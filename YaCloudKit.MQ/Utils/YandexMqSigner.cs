using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace YaCloudKit.MQ.Utils
{
    internal class YandexMqSigner
    {
        public const string ALGORITHM = "AWS4-HMAC-SHA256";
        public const string TERMINATOR = "aws4_request";
        public const string ISO8601BasicFormat = "yyyyMMddTHHmmssZ";
        public const string DateStringFormat = "yyyyMMdd";
        public const string CanonicalUri = "/";
        public const string CanonicalQuery = "";
        public const string HttpMethod = "POST";

        protected static HashAlgorithm payloadHash = HashAlgorithm.Create("SHA-256");
        protected static readonly Regex CompressWhitespaceRegex = new Regex("\\s+");

        private readonly YandexMqConfig config;

        public YandexMqSigner(YandexMqConfig configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            config = configuration;
        }

        public string Create(IRequestContext context)
        {
            var requestDateTime = context.RequestDateTime;
            var dateTimeStamp = requestDateTime.ToString(ISO8601BasicFormat, CultureInfo.InvariantCulture);
            var dateStamp = requestDateTime.ToString(DateStringFormat, CultureInfo.InvariantCulture);


            var canonicalHeaders = CanonicalizeHeaders(context.Headers);
            var signedHeaders = CanonicalizeHeaderNames(context.Headers);
            var bodyHash = ToHexString(payloadHash.ComputeHash(context.GetContent()));
            var canonicalRequest = CanonicalizeRequest(HttpMethod, canonicalHeaders, signedHeaders, bodyHash);


            var scope = $"{dateStamp}/{config.Region}/{config.ServiceName}/{TERMINATOR}";
            var canonicalRequestHash = ToHexString(payloadHash.ComputeHash(Encoding.UTF8.GetBytes(canonicalRequest)));
            var stringToSign = $"{ALGORITHM}\n{dateTimeStamp}\n{scope}\n{canonicalRequestHash}";
            var signingKey = GetSignatureKey(config.SecretAccessKey, dateStamp, config.Region, config.ServiceName);
            var signature = ToHexString(HmacSHA256(stringToSign, signingKey));


            var authString = new StringBuilder();
            authString.Append(ALGORITHM);
            authString.AppendFormat(" Credential={0}/{1}, ", config.AccessKeyID, scope);
            authString.AppendFormat("SignedHeaders={0}, ", signedHeaders);
            authString.AppendFormat("Signature={0}", signature);

            return authString.ToString();
        }

        protected string CanonicalizeRequest(string httpMethod, string canonicalizedHeaders, string signedHeaders, string bodyHash)
        {
            var canonicalRequest = new StringBuilder();

            canonicalRequest.AppendFormat("{0}\n", httpMethod);
            canonicalRequest.AppendFormat("{0}\n", CanonicalUri);
            canonicalRequest.AppendFormat("{0}\n", CanonicalQuery);

            canonicalRequest.AppendFormat("{0}\n", canonicalizedHeaders);
            canonicalRequest.AppendFormat("{0}\n", signedHeaders);

            canonicalRequest.Append(bodyHash);

            return canonicalRequest.ToString();
        }
        protected virtual string CanonicalizeHeaders(IDictionary<string, string> headers)
        {
            if (headers == null || headers.Count == 0)
                return string.Empty;
            var sortedHeaderMap = new SortedDictionary<string, string>();
            foreach (var header in headers.Keys)
                sortedHeaderMap.Add(header.ToLower(), headers[header]);
            var sb = new StringBuilder();
            foreach (var header in sortedHeaderMap.Keys)
            {
                var headerValue = CompressWhitespaceRegex.Replace(sortedHeaderMap[header], " ");
                sb.AppendFormat("{0}:{1}\n", header, headerValue.Trim());
            }
            return sb.ToString();
        }
        protected string CanonicalizeHeaderNames(IDictionary<string, string> headers)
        {
            var headersToSign = new List<string>(headers.Keys);
            headersToSign.Sort(StringComparer.OrdinalIgnoreCase);

            var sb = new StringBuilder();
            foreach (var header in headersToSign)
            {
                if (sb.Length > 0)
                    sb.Append(";");
                sb.Append(header.ToLower());
            }
            return sb.ToString();
        }
        public static byte[] HmacSHA256(string data, byte[] key)
        {
            var algorithm = "HmacSHA256";
            KeyedHashAlgorithm kha = KeyedHashAlgorithm.Create(algorithm);
            kha.Key = key;

            return kha.ComputeHash(Encoding.UTF8.GetBytes(data));
        }
        public static byte[] GetSignatureKey(string key, string dateStamp, string regionName, string serviceName)
        {
            byte[] kSecret = Encoding.UTF8.GetBytes(("AWS4" + key).ToCharArray());
            byte[] kDate = HmacSHA256(dateStamp, kSecret);
            byte[] kRegion = HmacSHA256(regionName, kDate);
            byte[] kService = HmacSHA256(serviceName, kRegion);
            byte[] kSigning = HmacSHA256(TERMINATOR, kService);

            return kSigning;
        }
        public static string ToHexString(byte[] data, bool lowercase = true)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString(lowercase ? "x2" : "X2"));
            }
            return sb.ToString();
        }
    }
}
