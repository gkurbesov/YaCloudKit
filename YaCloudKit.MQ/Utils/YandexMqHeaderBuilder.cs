using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace YaCloudKit.MQ.Utils
{
    public static class YandexMqHeaderBuilder
    {
        public const string HEAD_CONENT_LEN = "Content-Length";
        public const string HEAD_CONENT_TYPE = "Content-Type";
        public const string HEAD_CONENT_TYPE_VALUE = "application/x-www-form-urlencoded";
        public const string X_Amz_Date = "X-Amz-Date";
        public const string HEAD_HOST = "Host";
        public const string HEAD_AUTH = "Authorization";

        public static void AddHeaders(IRequestContext context, Uri endpoint)
        {
            context.AddHeader(HEAD_CONENT_LEN, context.GetContent().Length.ToString());
            context.AddHeader(HEAD_CONENT_TYPE, HEAD_CONENT_TYPE_VALUE);
            context.AddHeader(X_Amz_Date, context.RequestDateTime.ToString(YandexMqSigner.ISO8601BasicFormat, CultureInfo.InvariantCulture));
            var hostHeader = endpoint.Host;
            if (!endpoint.IsDefaultPort)
                hostHeader += ":" + endpoint.Port;
            context.AddHeader(HEAD_HOST, hostHeader);
        }

        public static void AddHeaderAuthorization(IRequestContext context, string signature)
        {
            context.AddHeader(HEAD_AUTH, signature);
        }

    }
}
