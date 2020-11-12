using System;
using System.Collections.Generic;
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
        public const string X_Amz_Date = "X-Amz-Date";
        public const string CanonicalUri = "/";
        public const string CanonicalQuery = "";
        public Uri EndpointUri { get; set; }
        public string HttpMethod { get; set; } = "POST";
        protected static HashAlgorithm payloadHash = HashAlgorithm.Create("SHA-256");

        protected static readonly Regex CompressWhitespaceRegex = new Regex("\\s+");


        private readonly YandexMqConfig config;

        public YandexMqSigner(YandexMqConfig configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            config = configuration;
        }

        public string Sign(byte[] body, IDictionary<string, string> headers)
        {
            return string.Empty;
        }
    }
}
