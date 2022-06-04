using System;

namespace YaCloudKit.IAM
{
    public class TokenRecipientOptions
    {
        public static readonly Uri DEFAULT_ENDPOINT = new Uri("https://iam.api.cloud.yandex.net/iam/v1/tokens");

        /// <summary>
        /// OAuth-токен для аккаунта в Яндекса.Паспорте. Подробнее см. в разделе OAuth-токен.
        /// </summary>
        public string OauthToken { get; internal set; }
        /// <summary>
        /// JSON Web Token (JWT) для сервисного аккаунта. Подробнее см. в разделе Получить IAM-токен для сервисного аккаунта.
        /// </summary>
        public string JwtToken { get; internal set; }
        /// <summary>
        /// Ресурс для выполнения запроса
        /// </summary>
        public Uri EndPoint { get; set; } = DEFAULT_ENDPOINT;
        /// <summary>
        /// Максимальный таймаут выполнения HTTP запроса
        /// </summary>
        public TimeSpan HttpClientTimeout { get; set; } = TimeSpan.FromMinutes(1);

        private TokenRecipientOptions() { }

        /// <summary>
        /// Создание экземпляра натсроек с использованием OAuth токена 
        /// </summary>
        /// <param name="oauthToken">OAuth-токен для аккаунта в Яндекса.Паспорте</param>
        /// <returns></returns>
        public static TokenRecipientOptions WithOAuthToken(string oauthToken)
        {
            if (string.IsNullOrWhiteSpace(oauthToken))
                throw new ArgumentNullException(nameof(oauthToken));

            return new TokenRecipientOptions()
            {
                OauthToken = oauthToken,
            };
        }
        /// <summary>
        /// Создание экземпляра натсроек с использованием JWT токена 
        /// </summary>
        /// <param name="jwt">JSON Web Token (JWT) для сервисного аккаунта</param>
        /// <returns></returns>
        public static TokenRecipientOptions WithJwtToken(string jwt)
        {
            if (string.IsNullOrWhiteSpace(jwt))
                throw new ArgumentNullException(nameof(jwt));

            return new TokenRecipientOptions()
            {
                JwtToken = jwt
            };
        }
    }
}
