using System;
using System.Globalization;
using System.Net.Http.Headers;
using YaCloudKit.Core;

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

        /// <summary>
        /// Добавляет заголовки в контекст запроса
        /// </summary>
        /// <param name="context"></param>
        /// <param name="endpoint"></param>
        public static void AddMainHeaders(IRequestContext context, Uri endpoint)
        {
            context.AddHeader(HEAD_CONENT_LEN, context.GetContent().Length.ToString());
            context.AddHeader(HEAD_CONENT_TYPE, HEAD_CONENT_TYPE_VALUE);

            var hostHeader = endpoint.Host;
            if (!endpoint.IsDefaultPort)
                hostHeader += ":" + endpoint.Port;
            context.AddHeader(HEAD_HOST, hostHeader);
        }

        /// <summary>
        /// Добавляет заголовки даты AWS в контекст запроса
        /// </summary>
        /// <param name="context"></param>
        public static void AddAWSDateHeaders(IRequestContext context)
        {
            context.AddHeader(X_Amz_Date, context.RequestDateTime.ToString(YandexMqSigner.ISO8601BasicFormat, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Добавляет заголовок авторизации в контекст
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signature"></param>
        public static void AddHeaderAuthorization(IRequestContext context, string signature)
        {
            context.AddHeader(HEAD_AUTH, signature);
        }

        /// <summary>
        /// Добавляет недостающие заголовки из контекста в класс HttpHeaders запроса или http-контента
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="values"></param>
        public static void AddHttpHeaders(IRequestContext context, HttpHeaders headers)
        {
            foreach (var headItem in context.Headers)
                headers.TryAddWithoutValidation(headItem.Key, headItem.Value);
        }

    }
}
