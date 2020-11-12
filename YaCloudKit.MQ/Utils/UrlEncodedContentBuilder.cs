using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Utils
{
    internal static class UrlEncodedContentBuilder
    {
        public static byte[] GetContentByteArray(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            if (nameValueCollection == null)
                throw new ArgumentNullException(nameof(nameValueCollection));

            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in nameValueCollection)
            {
                if (builder.Length > 0)
                    builder.Append('&');

                builder.Append(Encode(pair.Key));
                builder.Append('=');
                builder.Append(Encode(pair.Value));
            }
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public static string Encode(string data)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;
            return Uri.EscapeDataString(data).Replace("%20", "+");
        }
    }
}
