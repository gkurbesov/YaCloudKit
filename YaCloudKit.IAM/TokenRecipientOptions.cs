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
        public string RequestPath { get; }
        /// <summary>
        /// Идентификатор сервисного аккаунта, чьим ключом подписывается JWT для получения IAM-токена.
        /// </summary>
        public string ServiceAccountId { get; }
        /// <summary>
        /// Идентификатор открытого ключа, полученный при создании авторизованных ключей. 
        /// Ключ должен принадлежать сервисному аккаунту, для которого запрашивается IAM-токен.
        /// </summary>
        public string KeyId { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId">Идентификатор сервисного аккаунта</param>
        /// <param name="keyId">Идентификатор открытого ключа</param>
        public TokenRecipientOptions(string accountId, string keyId)
            : this(accountId, keyId, DEFAULT_REQUEST_PATH) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId">Идентификатор сервисного аккаунта</param>
        /// <param name="keyId">Идентификатор открытого ключа</param>
        /// <param name="path">Cсылка, по которой будет запрашиваться IAM-токен</param>
        public TokenRecipientOptions(string accountId, string keyId, string path)
        {
            if (string.IsNullOrWhiteSpace(accountId))
                throw new ArgumentNullException(nameof(accountId));
            if (string.IsNullOrWhiteSpace(keyId))
                throw new ArgumentNullException(nameof(keyId));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            ServiceAccountId = accountId;
            KeyId = keyId;
            RequestPath = path;
        }
    }
}
