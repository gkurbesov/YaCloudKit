using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.IAM
{
    public class TokenRecipientOptions
    {
        public const string DEFAULT_REQUEST_PATH = "https://iam.api.cloud.yandex.net/iam/v1/tokens";

        /// <summary>
        /// Cсылка, по которой будет запрашиваться IAM-токен
        /// </summary>
        public string RequestPath { get; internal set; }
        /// <summary>
        /// OAuth-токен для аккаунта в Яндекса.Паспорте. Подробнее см. в разделе OAuth-токен.
        /// </summary>
        public string OauthToken { get; internal set; }
        /// <summary>
        /// JSON Web Token (JWT) для сервисного аккаунта. Подробнее см. в разделе Получить IAM-токен для сервисного аккаунта.
        /// </summary>
        public string JwtToken { get; internal set; }

        private TokenRecipientOptions() { }

        /// <summary>
        /// Создание экземпляра натсроек с использованием OAuth токена 
        /// </summary>
        /// <param name="oauthToken">OAuth-токен для аккаунта в Яндекса.Паспорте</param>
        /// <returns></returns>
        public static TokenRecipientOptions WithOAuthToken(string oauthToken) =>
            WithOAuthToken(oauthToken, DEFAULT_REQUEST_PATH);
        /// <summary>
        /// Создание экземпляра натсроек с использованием OAuth токена 
        /// </summary>
        /// <param name="oauthToken">OAuth-токен для аккаунта в Яндекса.Паспорте</param>
        /// <param name="path">Cсылка, по которой будет запрашиваться IAM-токен</param>
        /// <returns></returns>
        public static TokenRecipientOptions WithOAuthToken(string oauthToken, string path)
        {
            if (string.IsNullOrWhiteSpace(oauthToken))
                throw new ArgumentNullException(nameof(oauthToken));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            return new TokenRecipientOptions()
            {
                RequestPath = path,
                OauthToken = oauthToken,
            };
        }
        /// <summary>
        /// Создание экземпляра натсроек с использованием JWT токена 
        /// </summary>
        /// <param name="jwt">JSON Web Token (JWT) для сервисного аккаунта</param>
        /// <returns></returns>
        public static TokenRecipientOptions WithJwtToken(string jwt) =>
            WithJwtToken(jwt, DEFAULT_REQUEST_PATH);
        /// <summary>
        /// Создание экземпляра натсроек с использованием JWT токена 
        /// </summary>
        /// <param name="jwt">JSON Web Token (JWT) для сервисного аккаунта</param>
        /// <param name="path">Cсылка, по которой будет запрашиваться IAM-токен</param>
        /// <returns></returns>
        public static TokenRecipientOptions WithJwtToken(string jwt, string path)
        {
            if (string.IsNullOrWhiteSpace(jwt))
                throw new ArgumentNullException(nameof(jwt));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            return new TokenRecipientOptions()
            {
                RequestPath = path,
                JwtToken = jwt
            };
        }
    }
}
