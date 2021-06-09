using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YaCloudKit.IAM
{
    /// <summary>
    /// Клиент для запроса IAM-токенов
    /// </summary>
    public interface ITokenRecipient
    {
        /// <summary>
        /// Запросить новый IAM-токен для сервисного аккаунта
        /// </summary>
        /// <param name="options">параметры запроса IAM-токена</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IamTokenCreateResult> GetIamToken(TokenRecipientOptions options, CancellationToken cancellationToken = default);
    }
}
